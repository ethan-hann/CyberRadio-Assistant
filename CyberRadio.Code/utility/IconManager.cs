using System.Diagnostics;
using System.Drawing.Imaging;
using System.Reflection;
using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.utility.event_args;
using Icon = RadioExt_Helper.models.Icon;

namespace RadioExt_Helper.utility;

/// <summary>
/// Manager class responsible for handling the generation and management of custom icons for radio stations.
/// This class is a singleton and as such should be accessed via <see cref="Instance"/>.
/// <para>This manager can both extract icons from <c>.archive</c> files (export) and create <c>.archive</c> files from raw PNGs (import). In both cases,
/// a custom <see cref="Icon"/> is added to the station's data and tracked.</para>
/// <para>Most methods will require a station ID to operate on.</para>
/// <para>All long-running operations support cancellation via the <see cref="CancelOperation"/> method.</para>
/// <para>Events can be subscribed to in order to track the progress of the running operations.</para>
/// </summary>
public class IconManager : IDisposable
{
    private static readonly object Lock = new();
    private static IconManager? _instance;

    private string? _wolvenKitCliDownloadUrl;
    private string? _inkAtlasExe;
    private string? _wolvenKitCliExe;
    private bool _isWolvenKitDownloaded;
    private bool _isWolvenKitExtracted;
    private bool _isInitialized;
    private CancellationTokenSource? _cancellationTokenSource;

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
    
    #region Import Events
    public event EventHandler<IconManagerEventArgs>? IconImportStarted;
    public event EventHandler<IconManagerEventArgs>? IconImportProgress;
    public event EventHandler<IconManagerEventArgs>? IconImportStatus;
    public event EventHandler<IconManagerEventArgs>? IconImportFinished;
    #endregion

    #region Export Events
    public event EventHandler<IconManagerEventArgs>? IconExportStarted;
    public event EventHandler<IconManagerEventArgs>? IconExportProgress;
    public event EventHandler<IconManagerEventArgs>? IconExportStatus;
    public event EventHandler<IconManagerEventArgs>? IconExportFinished;
    #endregion

    #region Properties
    /// <summary>
    /// Get or set the current version of WolvenKit to download. Defaults to <c>8.14.0</c>.
    /// </summary>
    public string? WolvenKitVersion { get; set; } = "8.14.0";

    /// <summary>
    /// Get the working directory for the icon generator. Defaults to <c>%LOCALAPPDATA%\RadioExt-Helper\tools</c>.
    /// </summary>
    public string? WorkingDirectory { get; private set; }

    /// <summary>
    /// Get the temporary directory for the icon generator. Defaults to <c>%TEMP%\{random folder name}</c>.
    /// </summary>
    public string? WolvenKitTempDirectory { get; private set; }

    /// <summary>
    /// Get or set the path to the staging icons folder. Defaults to <c>null</c>.
    /// </summary>
    public string? StagingIconsPath { get; private set; }

    /// <summary>
    /// Get the path to the image import directory. Defaults to <c>%LOCALAPPDATA%\RadioExt-Helper\tools\imported</c>.
    /// <para>This directory is where .png files that have been imported are stored. The file names are GUIDs.</para>
    /// </summary>
    public string? ImageImportDirectory { get; private set; }

    /// <summary>
    /// Get a value indicating if the icon manager has been initialized.
    /// </summary>
    public bool IsInitialized => _isInitialized;

    #endregion

    private IconManager()
    {
        try
        {
            if (_instance != null)
                throw new InvalidOperationException("An instance of IconManager already exists. Use IconManager.Instance to access it.");
        } 
        catch (Exception e)
        {
            AuLogger.GetCurrentLogger<IconManager>("Constructor").Error(e.Message);
        }
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
    private void CleanupResources()
    {
        try
        {
            if (Directory.Exists(WolvenKitTempDirectory))
                Directory.Delete(WolvenKitTempDirectory, true);
        }
        catch (Exception e)
        {
            AuLogger.GetCurrentLogger<IconManager>("CleanupResources").Error(e.Message);
        }
    }

    /// <summary>
    /// Initialize the icon manager. This method will set up the required paths and download the WolvenKit CLI if required. Only
    /// needs to be called once.
    /// </summary>
    public async Task Initialize()
    {
        if (_isInitialized) return;

        try
        {
            SetupRequiredPaths();

            _wolvenKitCliDownloadUrl = $"https://github.com/WolvenKit/WolvenKit/releases/download/{WolvenKitVersion}/WolvenKit.Console-{WolvenKitVersion}.zip";
            _inkAtlasExe = Assembly.GetExecutingAssembly().ExtractEmbeddedResource("RadioExt_Helper.tools.InkAtlas.generate_inkatlas.exe");

            if (WolvenKitTempDirectory == null)
                throw new InvalidOperationException("The WolvenKit temp directory is null.");

            _wolvenKitCliExe = Path.Combine(WolvenKitTempDirectory, "WolvenKit.CLI.exe");

            await DownloadWolvenKitIfRequiredAsync();

            if (!File.Exists(_inkAtlasExe))
                throw new FileNotFoundException("The inkatlas executable could not be found.", _inkAtlasExe);

            if (!File.Exists(_wolvenKitCliExe))
                throw new FileNotFoundException("The WolvenKit CLI executable could not be found.", _wolvenKitCliExe);

            _isInitialized = true;
        } 
        catch (Exception e)
        {
            AuLogger.GetCurrentLogger<IconManager>("Initialize").Error(e.Message);
        }
    }

    private void SetupRequiredPaths()
    {
        try
        {
            StagingIconsPath = GlobalData.ConfigManager.Get("stagingPath") is string stagingPath ? 
                Path.Combine(stagingPath, "icons") : null;
            WorkingDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "RadioExt-Helper", "working");
            WolvenKitTempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            ImageImportDirectory = Path.Combine(WorkingDirectory, "imported");

            Directory.CreateDirectory(WorkingDirectory);
            Directory.CreateDirectory(WolvenKitTempDirectory);
        } 
        catch (Exception e)
        {
            AuLogger.GetCurrentLogger<IconManager>("SetupPaths").Error(e.Message);
        }
    }

    #region WolvenKit Download and Extraction

    /// <summary>
    /// Downloads the WolvenKit CLI if it has not been downloaded yet and extracts it to the temporary directory: <see cref="WolvenKitTempDirectory"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Occurs if either <see cref="WorkingDirectory"/> or <see cref="WolvenKitTempDirectory"/> are <c>null</c>.</exception>
    private async Task DownloadWolvenKitIfRequiredAsync()
    {
        if (_isWolvenKitDownloaded && _isWolvenKitExtracted) return;

        try
        {
            if (WorkingDirectory == null || WolvenKitTempDirectory == null)
                throw new InvalidOperationException("Working directory or WolvenKit temp directory is null.");

            var zipFile = Path.Combine(WorkingDirectory, $"WolvenKit.Console-{WolvenKitVersion}.zip");

            if (File.Exists(zipFile))
            {
                _isWolvenKitDownloaded = true;
                if (!await ExtractWolvenKitAsync(zipFile))
                    throw new InvalidOperationException("The WolvenKit CLI could not be extracted.");
            }
            else
            {
                if (!await DownloadWolvenKitAsync(zipFile))
                    throw new InvalidOperationException("The WolvenKit CLI could not be downloaded.");

                if (!await ExtractWolvenKitAsync(zipFile))
                    throw new InvalidOperationException("The WolvenKit CLI could not be extracted.");
            }
        }
        catch (Exception e)
        {
            AuLogger.GetCurrentLogger<IconManager>("DownloadWolvenKitIfRequired").Error(e.Message);
        }
    }

    /// <summary>
    /// Asynchronously downloads the WolvenKit CLI from the specified URL.
    /// </summary>
    /// <param name="zipFile">The zip file to save.</param>
    /// <returns>A task representing the asynchronous download operation.</returns>
    private async Task<bool> DownloadWolvenKitAsync(string zipFile)
    {
        if (_wolvenKitCliDownloadUrl == null)
            throw new InvalidOperationException("The WolvenKit CLI download URL is null.");

        await PathHelper.DownloadFileAsync(_wolvenKitCliDownloadUrl, zipFile);
        if (File.Exists(zipFile))
        {
            _isWolvenKitDownloaded = true;
            AuLogger.GetCurrentLogger<IconManager>().Info("WolvenKit CLI downloaded successfully.");
        }
        return _isWolvenKitDownloaded;
    }

    /// <summary>
    /// Asynchronously extracts the WolvenKit CLI from the specified zip file.
    /// </summary>
    /// <param name="zipFile">The zip file to extract.</param>
    /// <returns>A task representing the asynchronous extraction operation.</returns>
    private async Task<bool> ExtractWolvenKitAsync(string zipFile)
    {
        if (WolvenKitTempDirectory == null)
            throw new InvalidOperationException("The WolvenKit temp directory is null.");

        await PathHelper.ExtractZipFileAsync(zipFile, WolvenKitTempDirectory);
        if (File.Exists(_wolvenKitCliExe))
        {
            _isWolvenKitExtracted = true;
            AuLogger.GetCurrentLogger<IconManager>().Info("WolvenKit CLI extracted successfully.");
        }
        return _isWolvenKitExtracted;
    }

    #endregion

    #region Creating a faux project

    /// <summary>
    /// Create the necessary directories for the import operation based on the station ID and atlas name. Optionally, overwrite the existing directories.
    /// </summary>
    /// <param name="stationId">The ID for the station.</param>
    /// <param name="atlasName">The name that icon atlas will be generated with.</param>
    /// <param name="overwrite">Indicates whether existing directories should be overwritten if they exist.</param>
    /// <returns>A tuple containing the path to the directory for imported PNG files, the path to the 'raw' files for the project,
    /// and the path to the folder holding the .inkatlas and .xbm files.</returns>
    private (string importedPngsPath, string rawProjectFiles, string iconFilesPath) 
        CreateImportDirectories(Guid stationId, string atlasName, bool overwrite = false)
    {
        try
        {
            var station = StationManager.Instance.GetStation(stationId);
            if (!station.HasValue)
                throw new InvalidOperationException("The station could not be found in the station manager.");

            if (WorkingDirectory == null)
                throw new InvalidOperationException("The working directory is null.");

            if (ImageImportDirectory == null)
                throw new InvalidOperationException("The image import directory is null.");

            //Create the path that imported PNGs are stored: %LOCALAPPDATA%\RadioExt-Helper\tools\imported\{stationId}
            var importedPngsPath = Path.Combine(ImageImportDirectory, stationId.ToString());
            if (overwrite && Directory.Exists(importedPngsPath))
                Directory.Delete(importedPngsPath, true);
            Directory.CreateDirectory(importedPngsPath);

            //Create the path that the import project files will be stored: %LOCALAPPDATA%\RadioExt-Helper\tools\{atlasName}\source\raw
            var projectPath = Path.Combine(WorkingDirectory, "tools", atlasName, "source", "raw");
            if (overwrite && Directory.Exists(projectPath))
                Directory.Delete(projectPath, true);
            Directory.CreateDirectory(projectPath);

            //Create the path for the .inkatlas and .xbm files to be stored in.
            var iconFilesPath = Path.Combine(WorkingDirectory, "tools", atlasName, "archive", "base", "icon");
            if (overwrite && Directory.Exists(iconFilesPath))
                Directory.Delete(iconFilesPath, true);
            Directory.CreateDirectory(iconFilesPath);

            return (importedPngsPath, projectPath, iconFilesPath);
        } catch (Exception e)
        {
            AuLogger.GetCurrentLogger<IconManager>("CreateImportDirectories").Error(e.Message);
        }
        return (string.Empty, string.Empty, string.Empty);
    }

    #endregion

    #region Import - Creating .archive files

    private int _currentProgress;

    /// <summary>
    /// Import a PNG image file as a custom icon for a station.
    /// </summary>
    /// <param name="stationId">The station ID to associate with the icon.</param>
    /// <param name="imagePath">The path to the image file on disk.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task ImportIconImageAsync(Guid stationId, string imagePath)
    {
        if (!_isInitialized)
            throw new InvalidOperationException("The icon manager has not been initialized.");

        _cancellationTokenSource = new CancellationTokenSource();
        var token = _cancellationTokenSource.Token;

        try
        {
            var status = GlobalData.Strings.GetString("IconManager_ImportStarted") ?? "The icon import operation has started.";
            OnIconImportStarted(new IconManagerEventArgs(status, _currentProgress, false));
            await Task.Run(() => CreateIcon(stationId, imagePath, token), token);
        }
        catch (OperationCanceledException)
        {
            AuLogger.GetCurrentLogger<IconManager>("ImportIconImageAsync").Info("The icon import operation was cancelled.");
            var status = GlobalData.Strings.GetString("IconManager_ImportCancelled") ?? "The icon import operation was cancelled.";
            OnIconImportStatus(new IconManagerEventArgs(status, _currentProgress, false));
        }
        catch (Exception e)
        {
            AuLogger.GetCurrentLogger<IconManager>("ImportIconAsync").Error(e.Message);
            var status = GlobalData.Strings.GetString("IconManager_ImportError") ?? "An error occurred during the icon import operation.";
            OnIconImportStatus(new IconManagerEventArgs(status, _currentProgress, true, e.Message));
        }
    }

    private void CreateIcon(Guid stationId, string imagePath, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        SetProgress(5, 100);

        //var icon = await CreateIconAsync(imagePath);
        //if (icon == null)
        //    throw new InvalidOperationException("The icon could not be created.");

        //var station = StationManager.Instance.GetStation(stationId);
        //if (!station.HasValue)
        //    throw new InvalidOperationException("The station could not be found in the station manager.");

        //station.Value.Key.TrackedObject.Icons.Add(icon);
    }

    /// <summary>
    /// Cancel the currently running operation.
    /// </summary>
    public void CancelOperation() => _cancellationTokenSource?.Cancel();

    #endregion

    #region Helper Methods

    /// <summary>
    /// Determines if the specified file is a PNG file.
    /// </summary>
    /// <param name="file">The file to check.</param>
    /// <returns><c>true</c> if the file is a .png; <c>false</c> otherwise.</returns>
    public bool IsPngFile(string file) => Path.GetExtension(file).Equals(".png", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Load an image from the specified file.
    /// </summary>
    /// <param name="file">The file to load as an image.</param>
    /// <returns>The <see cref="Image"/> represented by the path or <c>null</c> if the image couldn't be loaded.</returns>
    /// <exception cref="InvalidOperationException">Occurs if the image to load was not a PNG file.</exception>
    public Image? LoadImage(string file)
    {
        try
        {
            if (!IsPngFile(file))
                throw new InvalidOperationException("The file is not a PNG file.");

            return Image.FromFile(file);
        }
        catch (Exception e)
        {
            AuLogger.GetCurrentLogger<IconManager>("LoadImage").Error(e.Message);
        }
        return null;
    }

    /// <summary>
    /// Sets the current progress percentage of the operation.
    /// </summary>
    /// <param name="progress">The current progress percentage.</param>
    private void SetProgress(int progress) => _currentProgress = progress > 100 ? 100 : progress;

    /// <summary>
    /// Sets the current progress percentage of the operation based on the current and total values.
    /// </summary>
    /// <param name="current">The current count.</param>
    /// <param name="total">The total count.</param>
    private void SetProgress(int current, int total) => SetProgress((int) Math.Round((double) current / total * 100));

    /// <summary>
    /// Sets the current progress percentage of the operation based on the current and total values and a maximum value.
    /// </summary>
    /// <param name="current">The current count.</param>
    /// <param name="total">The total count.</param>
    /// <param name="max">The maximum amount that the progress can be.</param>
    private void SetProgress(int current, int total, int max) => SetProgress((int) Math.Round((double) current / total * max));

    /// <summary>
    /// Reset the current progress percentage to 0.
    /// </summary>
    private void ResetProgress() => _currentProgress = 0;
    #endregion

    #region Event Handlers
    private void OnIconImportStarted(IconManagerEventArgs e) => IconImportStarted?.Invoke(this, e);
    private void OnIconImportProgress(IconManagerEventArgs e) => IconImportProgress?.Invoke(this, e);
    private void OnIconImportStatus(IconManagerEventArgs e) => IconImportStatus?.Invoke(this, e);
    private void OnIconImportFinished(IconManagerEventArgs e) => IconImportFinished?.Invoke(this, e);
    private void OnIconExportStarted(IconManagerEventArgs e) => IconExportStarted?.Invoke(this, e);
    private void OnIconExportProgress(IconManagerEventArgs e) => IconExportProgress?.Invoke(this, e);
    private void OnIconExportStatus(IconManagerEventArgs e) => IconExportStatus?.Invoke(this, e);
    private void OnIconExportFinished(IconManagerEventArgs e) => IconExportFinished?.Invoke(this, e);
    #endregion
}