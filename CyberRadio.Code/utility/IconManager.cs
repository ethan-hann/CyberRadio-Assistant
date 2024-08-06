using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.nexus_api;

namespace RadioExt_Helper.utility;

/// <summary>
/// Manager class responsible for handling the generation and management of custom icons for radio stations.
/// This class is a singleton and as such should be accessed via <see cref="Instance"/>.
/// <para>Most of the operations in this class are long-running operations and should be utilized with care!</para>
/// </summary>
public class IconManager : IDisposable
{
    private static readonly object Lock = new();
    private static IconManager? _instance;

    public static IconManager Instance
    {
        get
        {
            lock (Lock)
            {
                _instance ??= new IconManager();
                return _instance;
            }
        }
    }

    public event Action<int>? ProgressChanged;
    public event Action<string>? StatusChanged;
    public event Action<string>? ErrorOccurred;
    public event Action<string>? WarningOccurred;

    /// <summary>
    /// Get or set the current version of WolvenKit to download. Defaults to <c>8.14.0</c>.
    /// </summary>
    public string WolvenKitVersion { get; set; } = "8.14.0";

    private readonly string _wolvenKitCliDownloadUrl;

    private readonly string _inkAtlasExe;
    private readonly string _wolvenKitCliExe;

    private bool _isCancelling;

    /// <summary>
    /// Get the working directory for the icon generator. Defaults to <c>%LOCALAPPDATA%\RadioExt-Helper\tools</c>.
    /// </summary>
    public string WorkingDirectory { get; set; }

    /// <summary>
    /// Get the temporary directory for the icon generator. Defaults to <c>%TEMP%\{random folder name}</c>.
    /// </summary>
    public string WolvenKitTempDirectory { get; }

    /// <summary>
    /// Get or set the path to the staging icons folder. Defaults to <c>null</c>.
    /// </summary>
    public string? StagingIconsPath { get; }

    /// <summary>
    /// Get the path to the image import directory. Defaults to <c>%LOCALAPPDATA%\RadioExt-Helper\tools\imported</c>.
    /// <para>This directory is where .png files that have been imported are stored. The file names are GUIDs.</para>
    /// </summary>
    public string ImageImportDirectory { get; }

    private bool _isWolvenKitDownloaded;
    private bool _isWolvenKitExtracted;

    private IconManager()
    {
        StagingIconsPath = GlobalData.ConfigManager.Get("stagingPath") is string stagingPath ? 
            Path.Combine(stagingPath, "icons") : null;
        WorkingDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "RadioExt-Helper", "tools");
        ImageImportDirectory = Path.Combine(WorkingDirectory, "imported");

        WolvenKitTempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        _wolvenKitCliDownloadUrl = $"https://github.com/WolvenKit/WolvenKit/releases/download/{WolvenKitVersion}/WolvenKit.Console-{WolvenKitVersion}.zip";
        _inkAtlasExe = ExtractEmbeddedResource("RadioExt_Helper.tools.InkAtlas.generate_inkatlas.exe");
        _wolvenKitCliExe = Path.Combine(WolvenKitTempDirectory, "WolvenKit.CLI.exe");

        Directory.CreateDirectory(WorkingDirectory);
        Directory.CreateDirectory(WolvenKitTempDirectory);

        var zipFile = Path.Combine(WorkingDirectory, $"WolvenKit.Console-{WolvenKitVersion}.zip");

        if (!_isWolvenKitDownloaded)
            _ = DownloadWolvenKit(zipFile);

        if (!_isWolvenKitExtracted)
            _ = ExtractWolvenKit(zipFile);
    }

    /// <summary>
    /// Asynchronously downloads the WolvenKit CLI from the specified URL.
    /// </summary>
    /// <param name="zipFile">The zip file to save.</param>
    /// <returns>A task representing the asynchronous download operation.</returns>
    public async Task DownloadWolvenKit(string zipFile)
    {
        if (_isWolvenKitDownloaded) return;

        await PathHelper.DownloadFileAsync(_wolvenKitCliDownloadUrl, zipFile);
        _isWolvenKitDownloaded = File.Exists(zipFile);
    }

    /// <summary>
    /// Asynchronously extracts the WolvenKit CLI from the specified zip file.
    /// </summary>
    /// <param name="zipFile">The zip file to extract.</param>
    /// <returns>A task representing the asynchronous extraction operation.</returns>
    public async Task ExtractWolvenKit(string zipFile)
    {
        if (!_isWolvenKitDownloaded) return;

        await PathHelper.ExtractZipFileAsync(zipFile, WolvenKitTempDirectory);
        _isWolvenKitExtracted = File.Exists(_wolvenKitCliExe);
    }

    ~IconManager() => CleanupResources();

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        CleanupResources();
    }

    /// <summary>
    /// Cleans up any resources used by the icon manager. This mainly involves deleting the temporary directory: <see cref="WolvenKitTempDirectory"/>.
    /// </summary>
    public void CleanupResources()
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
    /// Save an icon to a station's metadata and optionally compress it into an archive (for use in the game).
    /// </summary>
    /// <param name="station">The trackable station object.</param>
    /// <param name="atlasName">The atlas name for the icon's inkatlas.</param>
    /// <param name="image">The image to save.</param>
    /// <param name="compressToArchive">Indicates if <see cref="ArchiveFromPngs"/> and <see cref="LoadIconFromFile"/>
    /// should be called (on a background thread) to create the <c>.archive</c> file and associate it with the station.</param>
    /// <returns>A <see cref="CustomIcon"/> object representing the loaded icon. Note: if <paramref name="compressToArchive"/> is false, this <see cref="CustomIcon"/> is meaningless.</returns>
    public CustomIcon SaveIconToStation(TrackableObject<Station> station, string atlasName, Image image, bool compressToArchive = false)
    {
        _isCancelling = true;
        var pngGuid = Guid.NewGuid().ToString();
        var tempPath = Path.Combine(ImageImportDirectory, station.TrackedObject.MetaData.DisplayName);
        Directory.CreateDirectory(tempPath);
        image.Save(Path.Combine(tempPath, $"{pngGuid}.png"), ImageFormat.Png);

        var icon = new CustomIcon
        {
            InkAtlasPath = $"base\\icon\\{atlasName}.inkatlas",
            InkAtlasPart = "icon",
            UseCustom = true
        };

        station.TrackedObject.MetaData.CustomData.Add("pngGuid", pngGuid);

        if (compressToArchive)
        {
            Task.Run(() =>
            {
                var result = ArchiveFromPngs(tempPath, atlasName);
                LoadIconFromFile(station, result["FinalArchivePath"].output);

            });
        }

        _isCancelling = false;
        return icon;
    }

    /// <summary>
    /// Get a list of .png images from an <c>.archive</c> file.
    /// </summary>
    /// <param name="archivePath">The full path to the archive file.</param>
    /// <returns>A list of <see cref="Image"/> objects representing the <c>.png</c> files from the archive.</returns>
    public List<Image> PngsFromArchive(string archivePath)
    {
        try
        {
            List<Image> result = [];
            var outputDir = Path.GetDirectoryName(archivePath);
            if (outputDir == null)
            {
                AuLogger.GetCurrentLogger<IconManager>("PngsFromArchive").Error("Output directory is null.");
                return result;
            }

            var unpackedDir = Path.Combine(outputDir, "unpacked");
            Directory.CreateDirectory(unpackedDir);

            OnStatusChanged("Unpacking archive...");
            var unpackResult = UnpackArchive(archivePath, unpackedDir);
            OnStatusChanged(unpackResult.output);

            var xbmFiles = Directory.GetFiles(unpackedDir, "*.xbm", SearchOption.AllDirectories);

            OnStatusChanged("Exporting PNG files...");
            foreach (var xbmFile in xbmFiles)
            {
                var pngResult = ExportPng(xbmFile, unpackedDir);
                OnStatusChanged(pngResult.output);
                var pngFiles = Directory.GetFiles(unpackedDir, "*.png", SearchOption.AllDirectories);
                foreach (var pngFile in pngFiles)
                {
                    var image = LoadImage(pngFile);
                    if (image != null)
                    {
                        result.Add(image);
                    }
                }
            }
            return result;
        } catch (Exception e)
        {
            AuLogger.GetCurrentLogger<IconManager>("PngsFromArchive").Error(e.Message);
            return [];
        }
    }

    /// <summary>
    /// Create a new icon archive from the specified folder of .png files.
    /// </summary>
    /// <param name="inputFolderWithPngs"></param>
    /// <param name="atlasName"></param>
    /// <returns></returns>
    public Dictionary<string, (string output, string error)> ArchiveFromPngs(string inputFolderWithPngs, string atlasName)
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

            //Add the final path to the .archive file to the results
            results.Add("FinalArchivePath", (Path.Combine(finalOutputFolder, $"{atlasName}.archive"), string.Empty));

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
    /// Unpack (un-bundle) a <c>.archive</c> file to the specified output folder.
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
    public (string output, string error) ExportPng(string xbmFilePath, string outputFolder)
    {
        var arguments = $"export --uext png -p \"{xbmFilePath}\" -o \"{outputFolder}\"";
        return ExecuteCommand(_wolvenKitCliExe, arguments);
    }

    /// <summary>
    /// Loads a custom icon from a file and stores the file name and hash in the station's metadata.
    /// </summary>
    /// <param name="station">The station to load the icon into.</param>
    /// <param name="iconFilePath">The full file path to the icon archive file.</param>
    /// <param name="extract">Indicates whether the icon should be extracted into a .png file and saved to the temporary directory.</param>
    /// <returns>If <paramref name="extract"/> is <c>true</c>, returns the result of the <see cref="PngsFromArchive"/> method which extracts the PNG files from an <c>.archive</c> file.
    /// Otherwise, returns <c>null</c>.</returns>
    public List<Image>? LoadIconFromFile(TrackableObject<Station> station, string? iconFilePath, bool extract = false)
    {
        try
        {
            if (StagingIconsPath == null)
            { 
                AuLogger.GetCurrentLogger<StationManager>("LoadIconFromFile").Warn("Staging icons folder is null. This could indicate a configuration issue.");
                return null;
            }

            if (iconFilePath == null)
            {
                AuLogger.GetCurrentLogger<StationManager>("LoadIconFromFile").Warn("Icon file path is null. This could indicate a configuration issue.");
                return null;
            }

            // Ensure the icons folder exists
            if (!Directory.Exists(StagingIconsPath))
                Directory.CreateDirectory(StagingIconsPath);

            // Copy the icon to the icons folder
            var iconFileName = Path.GetFileName(iconFilePath);
            var destIconPath = Path.Combine(StagingIconsPath, iconFileName);
            File.Copy(iconFilePath, destIconPath, true);

            // Calculate the hash of the icon file
            var fileHash = PathHelper.ComputeSha256Hash(destIconPath, true);

            // Store the file name and hash in the CustomData dictionary
            station.TrackedObject.MetaData.CustomData["IconFileName"] = iconFileName;
            station.TrackedObject.MetaData.CustomData["IconFileHash"] = fileHash;

            AuLogger.GetCurrentLogger<StationManager>("LoadIconFromFile").Info($"Custom icon loaded for station: {station.TrackedObject.MetaData.DisplayName}");

            return extract ? PngsFromArchive(iconFilePath) : null;

        } catch (Exception e)
        {
            AuLogger.GetCurrentLogger<StationManager>("LoadIconFromFile").Error(e.Message);
        }
        return null;
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
            if (error == string.Empty) return result;

            AuLogger.GetCurrentLogger<IconManager>("ExecuteCommand").Error(error);
            OnErrorChanged(error);

            return result;
        }
        catch (Exception ex) 
        {
            AuLogger.GetCurrentLogger<IconManager>("ExecuteCommand").Error(ex);
            return (string.Empty, $"Couldn't execute command: {executable} {arguments}");
        }
    }

    private void OnStatusChanged(string message) => StatusChanged?.Invoke(message);

    private void OnProgressChanged(int progressPercentage) => ProgressChanged?.Invoke(progressPercentage);

    private void OnErrorChanged(string message) => ErrorOccurred?.Invoke(message);

    private void OnWarningChanged(string message) => WarningOccurred?.Invoke(message);

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

    public void CancelIconImport()
    {
        if (_isCancelling)
        {
            _isCancelling = true;
        }
    }
}