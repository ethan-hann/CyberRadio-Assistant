using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AetherUtils.Core.Logging;
using RadioExt_Helper.nexus_api;

namespace RadioExt_Helper.utility;

public class IconManager : IDisposable
{
    private static object _lock = new object();
    private static IconManager? _instance = null;

    public static IconManager Instance
    {
        get
        {
            lock (_lock)
            {
                _instance ??= new IconManager();
                return _instance;
            }
        }
    }

    public event EventHandler<string>? StatusChanged;
    public event EventHandler<int>? ProgressChanged;

    /// <summary>
    /// Get or set the current version of WolvenKit to download. Defaults to <c>8.14.0</c>.
    /// </summary>
    public string WolvenKitVersion { get; set; } = "8.14.0";

    private readonly string _wolvenKitCliDownloadUrl;

    private readonly string _inkAtlasExe;
    private readonly string _wolvenKitCliExe;

    /// <summary>
    /// Get the working directory for the icon generator. Defaults to <c>%LOCALAPPDATA%\RadioExt-Helper\tools</c>.
    /// </summary>
    public string WorkingDirectory { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "RadioExt-Helper", "tools");

    /// <summary>
    /// Get the temporary directory for the icon generator. Defaults to <c>%TEMP%\{random folder name}</c>.
    /// </summary>
    public string WolvenKitTempDirectory { get; }

    private IconManager()
    {
        WolvenKitTempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        _wolvenKitCliDownloadUrl = $"https://github.com/WolvenKit/WolvenKit/releases/download/{WolvenKitVersion}/WolvenKit.Console-{WolvenKitVersion}.zip";
        _inkAtlasExe = ExtractEmbeddedResource("RadioExt_Helper.tools.InkAtlas.generate_inkatlas.exe");
        _wolvenKitCliExe = Path.Combine(WolvenKitTempDirectory, "WolvenKit.CLI.exe");

        Directory.CreateDirectory(WorkingDirectory);
        Directory.CreateDirectory(WolvenKitTempDirectory);

        var zipFile = Path.Combine(WorkingDirectory, "wolvenKit.zip");
        if (!File.Exists(zipFile))
        {
            _ = DownloadWolvenKit();
        }

        if (!File.Exists(_wolvenKitCliExe))
        {
            _ = ExtractWolvenKit();
        }
    }

    private async Task DownloadWolvenKit()
    {
        var zipFile = Path.Combine(WorkingDirectory, "wolvenKit.zip");

        await PathHelper.DownloadFileAsync(_wolvenKitCliDownloadUrl, zipFile);
    }

    private async Task ExtractWolvenKit()
    {
        var zipFile = Path.Combine(WorkingDirectory, "wolvenKit.zip");
        await PathHelper.ExtractZipFileAsync(zipFile, WolvenKitTempDirectory);
    }

    ~IconManager() => CleanupResources();

    public void Dispose() => CleanupResources();

    private void CleanupResources()
    {
        try
        {
            Directory.Delete(WolvenKitTempDirectory, true);
        }
        catch (Exception e)
        {
            AuLogger.GetCurrentLogger<IconManager>().Error(e.Message);
        }
    }

    /// <summary>
    /// Create a new icon archive from the specified folder of .png files.
    /// </summary>
    /// <param name="inputFolderWithPngs"></param>
    /// <param name="atlasName"></param>
    /// <returns></returns>
    public Dictionary<string, (string output, string error)> CreateIcon(string inputFolderWithPngs, string atlasName)
    {
        var results = new Dictionary<string, (string output, string error)>();
        var currentProgress = 0;
        try
        {
            // Generate Ink Atlas JSON
            //OnStatusChanged("Generating Ink Atlas JSON...");
            currentProgress += 3;
            OnProgressChanged(currentProgress);
            var commandResult = GenerateInkAtlasJson(inputFolderWithPngs, atlasName);
            currentProgress += 5;
            OnProgressChanged(currentProgress);
            results.Add("GenerateInkAtlasJSON", commandResult);

            // Convert JSON to Ink Atlas file
            //OnStatusChanged("Converting JSON to Ink Atlas...");
            currentProgress += 2;
            OnProgressChanged(currentProgress);
            var jsonFilePath = Path.Combine(WorkingDirectory, atlasName, "source", "raw", $"{atlasName}.inkatlas.json");
            var convertResult = ConvertToInkAtlas(jsonFilePath);
            OnProgressChanged(currentProgress);
            results.Add("ConvertToInkAtlas", convertResult);

            // Import (convert) raw files to XBM format
            //OnStatusChanged("Importing raw files to XBM format...");
            currentProgress += 5;
            OnProgressChanged(currentProgress);
            var xbmFolderPath = Path.Combine(WorkingDirectory, atlasName, "source", "raw");
            var importResult = ImportXbm(xbmFolderPath);
            currentProgress += 5;
            OnProgressChanged(currentProgress);
            results.Add("ImportRawXBM", importResult);

            // Create faux project directory
            OnStatusChanged("Creating project directory...");
            currentProgress += 5;
            OnProgressChanged(currentProgress);
            var projectIconDirectory = Path.Combine(WorkingDirectory, atlasName, "base", "icon");
            Directory.CreateDirectory(projectIconDirectory);

            // Copy the required files to the project directory
            OnStatusChanged("Copying required files to project directory...");
            currentProgress += 20;
            OnProgressChanged(currentProgress);
            var xbmFiles = Directory.GetFiles(xbmFolderPath, "*.xbm");
            foreach (var xbmFile in xbmFiles)
            {
                var fileName = Path.GetFileName(xbmFile);
                var destination = Path.Combine(projectIconDirectory, fileName);
                File.Copy(xbmFile, destination);
                currentProgress += ((1 / xbmFiles.Length) * 50) / 10;
                OnProgressChanged(currentProgress);
            }
            var inkAtlasFile = Path.Combine(WorkingDirectory, atlasName, "source", "raw", $"{atlasName}.inkatlas");
            File.Copy(inkAtlasFile, Path.Combine(projectIconDirectory, $"{atlasName}.inkatlas"));
            currentProgress += 5;

            // Finally, pack the result into an .archive file
            //OnStatusChanged($"Packing files into {atlasName}.archive...");
            OnProgressChanged(currentProgress);
            var finalOutputFolder = Path.Combine(WorkingDirectory, atlasName, "output");
            Directory.CreateDirectory(finalOutputFolder);
            currentProgress += 10;
            OnProgressChanged(currentProgress);
            var finalInputDirectory = Path.Combine(WorkingDirectory, atlasName);
            var packResult = PackArchive(finalInputDirectory, finalOutputFolder);
            results.Add("PackArchive", packResult);
            currentProgress = 100;
            OnProgressChanged(currentProgress);

            return results;
        } catch (Exception e)
        {
            AuLogger.GetCurrentLogger<IconManager>().Error(e.Message);
            return results;
        }
    }

    /// <summary>
    /// Generate a new <c>.inkatlas.json</c> file from the .png files in the specified folder.
    /// </summary>
    /// <param name="iconFolderPath">The path to a folder containing .png files.</param>
    /// <param name="atlasName">The name the atlas should be generated with.</param>
    /// <returns>A tuple containing the output from the script and the errors (if any).</returns>
    /// <remarks>
    ///     The <c>generate_inkatlas.exe</c> is a tool that generates an <c>.inkatlas.json</c> file from a folder of <c>.png</c> files.
    ///     It was built from a modified version of the original <see href="https://github.com/DoctorPresto/Cyberpunk-Helper-Scripts/blob/main/generate_inkatlas.py">generate_inkatlas.py</see> script and allows for command line arguments to be passed in.
    ///     The modified tool is embedded as a resource in this project and is extracted to a temporary location before being executed.
    /// </remarks>
    public (string output, string error) GenerateInkAtlasJson(string iconFolderPath, string atlasName)
    {
        OnStatusChanged("Generating Ink Atlas JSON...");
        var outputFolderPath = Path.Combine(WorkingDirectory, atlasName, "source");
        var arguments = $"\"{iconFolderPath}\" \"{outputFolderPath}\" \"{atlasName}\"";
        return ExecuteCommand(_inkAtlasExe, arguments);
    }

    /// <summary>
    /// Convert a <c>.json</c> file to an <c>.inkatlas</c> file.
    /// </summary>
    /// <param name="jsonFilePath">The full path to the <c>.inkatlas.json</c> file.</param>
    /// <returns>A tuple containing the output from the script and the errors (if any).</returns>
    public (string output, string error) ConvertToInkAtlas(string jsonFilePath)
    {
        OnStatusChanged("Converting JSON to Ink Atlas...");
        var arguments = $"convert deserialize \"{jsonFilePath}\"";
        return ExecuteCommand(_wolvenKitCliExe, arguments);
    }

    /// <summary>
    /// Import a folder of raw files to the XBM format.
    /// </summary>
    /// <param name="sourcePath">The full path to the raw input files.</param>
    /// <returns>A tuple containing the output from the script and the errors (if any).</returns>
    public (string output, string error) ImportXbm(string sourcePath)
    {
        OnStatusChanged("Importing raw files to XBM format...");
        var arguments = $"import -p \"{sourcePath}\"";
        return ExecuteCommand(_wolvenKitCliExe, arguments);
    }

    /// <summary>
    /// Pack the contents of a directory into a <c>.archive</c> file. This essentially "builds" the faux mod.
    /// </summary>
    /// <param name="modPath">The path to a mod directory.</param>
    /// <param name="outputFolder">The output folder to store the packed <c>.archive</c> file in.</param>
    /// <returns>A tuple containing the output from the script and the errors (if any).</returns>
    public (string output, string error) PackArchive(string modPath, string outputFolder)
    {
        OnStatusChanged($"Packing archive for \"{modPath}\"...");
        var arguments = $"pack -p \"{modPath}\" -o \"{outputFolder}\"";
        return ExecuteCommand(_wolvenKitCliExe, arguments);
    }

    /// <summary>
    /// Unpack (unbundle) a <c>.archive</c> file to the specified output folder.
    /// </summary>
    /// <param name="archivePath">The full path to the <c>.archive</c> file.</param>
    /// <param name="outputFolder">The output folder to store the extracted contents in.</param>
    /// <returns>A tuple containing the output from the script and the errors (if any).</returns>
    public (string output, string error) UnpackArchive(string archivePath, string outputFolder)
    {
        OnStatusChanged($"Unpacking archive \"{archivePath}\"...");
        var arguments = $"unbundle -p \"{archivePath}\" -o \"{outputFolder}\"";
        return ExecuteCommand(_wolvenKitCliExe, arguments);
    }

    /// <summary>
    /// Export an <c>.xbm</c> file to a <c>.png</c> file.
    /// </summary>
    /// <param name="xbmFilePath">The path to the <c>.xbm</c> file to export.</param>
    /// <param name="outputFolder">The output folder to store the exported <c>.png</c> file.</param>
    /// <returns>A tuple containing the output from the script and the errors (if any).</returns>
    public (string output, string error) ExportPNG(string xbmFilePath, string outputFolder)
    {
        var arguments = $"export --uext png -p \"{xbmFilePath}\" -o \"{outputFolder}\"";
        return ExecuteCommand(_wolvenKitCliExe, arguments);
    }

    private (string output, string error) ExecuteCommand(string executable, string arguments)
    {
        try
        {
            var processInfo = new ProcessStartInfo(executable, arguments)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process();
            process.StartInfo = processInfo;

            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            var result = (output, error);
            return result;
        }
        catch (Exception ex) 
        {
            AuLogger.GetCurrentLogger<IconManager>("ExecuteCommand").Error(ex);
            return (string.Empty, $"Couldn't execute command: {executable} {arguments}");
        }
    }

    private void OnStatusChanged(string message) => StatusChanged?.Invoke(this, message);

    private void OnProgressChanged(int progressPercentage) => ProgressChanged?.Invoke(this, progressPercentage);

    private string ExtractEmbeddedResource(string resourceName)
    {
        string tempPath = Path.Combine(Path.GetTempPath(), resourceName);
        using (Stream resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
        {
            using (FileStream file = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
            {
                resource.CopyTo(file);
            }
        }
        return tempPath;
    }

    /// <summary>
    /// Get a value indicating whether the specified file is a .png file. Checks if the file exists and has a .png extension.
    /// </summary>
    /// <param name="file">The path to a file on disk.</param>
    /// <returns><c>true</c> if the file is a .png; <c>false</c> otherwise.</returns>
    public bool IsPngFile(string file) => Path.Exists(file) && Path.GetExtension(file).Equals(".png", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Get an image from a file.
    /// </summary>
    /// <param name="file">The path to an image on disk.</param>
    /// <returns>A <see cref="Image"/> object or <c>null</c> if the image couldn't be loaded.</returns>
    public Image? LoadImage(string file)
    {
        try
        {
            return Image.FromFile(file);
        }
        catch (Exception e)
        {
            AuLogger.GetCurrentLogger<IconManager>().Error(e.Message);
            return null;
        }
    }
}