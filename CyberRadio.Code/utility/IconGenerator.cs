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

public class IconGenerator
{
    private readonly string _wolvenKitCliDownloadUrl = "https://github.com/WolvenKit/WolvenKit/releases/download/8.14.0/WolvenKit.Console-8.14.0.zip";

    private readonly string _inkAtlasExe;
    private readonly string _wolvenKitCliExe;

    /// <summary>
    /// Get or set the working directory for the icon generator.
    /// </summary>
    public string WorkingDirectory { get; set; } = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

    private string WolvenKitTempDirectory { get; } = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

    public IconGenerator()
    {
        Directory.CreateDirectory(WorkingDirectory);
        Directory.CreateDirectory(WolvenKitTempDirectory);

        _inkAtlasExe = ExtractEmbeddedResource("RadioExt_Helper.tools.InkAtlas.generate_inkatlas.exe");
        _wolvenKitCliExe = Path.Combine(WolvenKitTempDirectory, "WolvenKit.CLI.exe");

        if (!File.Exists(_wolvenKitCliExe))
        {
            _ = DownloadWolvenKit();
        }
    }

    private async Task DownloadWolvenKit()
    {
        var zipFile = Path.Combine(WolvenKitTempDirectory, "wolvenKit.zip");

        await NexusApi.DownloadFileAsync(_wolvenKitCliDownloadUrl, zipFile);
        await NexusApi.ExtractZipFileAsync(zipFile, WolvenKitTempDirectory);
    }

    ~IconGenerator()
    {
        try
        {
            Directory.Delete(WolvenKitTempDirectory, true);
        } catch (Exception e)
        {
            AuLogger.GetCurrentLogger<IconGenerator>().Error(e.Message);
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
        var outputFolderPath = Path.Combine(iconFolderPath, "source");
        var arguments = $"\"{iconFolderPath}\" \"{outputFolderPath}\" \"{atlasName}\"";
        return ExecuteCommand(_inkAtlasExe, arguments);
    }

    public (string output, string error) ConvertToInkAtlas(string jsonFilePath)
    {
        var arguments = $"convert deserialize \"{jsonFilePath}\"";
        return ExecuteCommand(_wolvenKitCliExe, arguments);
    }

    //public void ImportXBM(string sourcePath)
    //{
    //    string command = $"dotnet {Path.Combine(wolvenKitPath, "cp77tools.dll")} import -p \"{sourcePath}\"";
    //    ExecuteCommand("cmd.exe", "/c " + command);
    //}

    //public void PackArchive(string modPath)
    //{
    //    string command = $"dotnet {Path.Combine(wolvenKitPath, "cp77tools.dll")} pack -p \"{modPath}\"";
    //    ExecuteCommand("cmd.exe", "/c " + command);
    //}

    //public void UnpackArchive(string archivePath, string outputPath)
    //{
    //    string command = $"dotnet {Path.Combine(wolvenKitPath, "cp77tools.dll")} unbundle -p \"{archivePath}\" -o \"{outputPath}\"";
    //    ExecuteCommand("cmd.exe", "/c " + command);
    //}

    //public void ExportPNG(string xbmFilePath, string outputPath)
    //{
    //    string command = $"dotnet {Path.Combine(wolvenKitPath, "cp77tools.dll")} export --uext png -p \"{xbmFilePath}\" -o \"{outputPath}\"";
    //    ExecuteCommand("cmd.exe", "/c " + command);
    //}

    private (string output, string error) ExecuteCommand(string executable, string arguments)
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
}