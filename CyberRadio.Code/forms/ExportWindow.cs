using System.ComponentModel;
using System.Diagnostics;
using AetherUtils.Core.Extensions;
using AetherUtils.Core.Files;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

/// <summary>
/// Represents the export window form, allowing users to export radio stations to the game or staging area.
/// </summary>
public partial class ExportWindow : Form
{
    private readonly Json<MetaData> _metaDataJson = new();
    private readonly Json<SongList> _songListJson = new();
    private readonly List<Station> _stationsToExport;

    private readonly string _statusString =
        GlobalData.Strings.GetString("ExportingStationStatus") ?? "Exporting station: {0}";

    private DirectoryCopier? _dirCopier;
    private bool _exportToGameComplete;
    private bool _exportToStagingComplete;
    private bool _isCancelling;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExportWindow"/> class with the specified stations to export.
    /// </summary>
    /// <param name="stations">The list of stations to be exported.</param>
    public ExportWindow(List<Station> stations)
    {
        InitializeComponent();
        _stationsToExport = stations;
    }

    /// <summary>
    /// Handles the Load event of the ExportWindow form.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void ExportWindow_Load(object sender, EventArgs e)
    {
        Translate();

        // Enable owner drawing
        lvStations.OwnerDraw = true;
        lvStations.DrawColumnHeader += (_, args) => args.DrawDefault = true;
        lvStations.DrawSubItem += LvStations_DrawSubItem;

        PopulateListView();
        ConfigureButtons();
    }

    /// <summary>
    /// Draws the sub-items of the stations ListView, including custom icons.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="DrawListViewSubItemEventArgs"/> instance containing the event data.</param>
    private void LvStations_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e)
    {
        if (e.ColumnIndex == 0) // Assuming the icon is in the first column
        {
            if (e.Item != null && lvStations.SmallImageList != null && e.Item.Tag is Station station)
            {
                var image = lvStations.SmallImageList.Images[station.GetStatus() ? "enabled" : "disabled"];
                if (image != null)
                {
                    // Calculate the position to center the image in the cell
                    var iconX = e.Bounds.Left + (e.Bounds.Width - image.Width) / 2;
                    var iconY = e.Bounds.Top + (e.Bounds.Height - image.Height) / 2;
                    e.Graphics.DrawImage(image, iconX, iconY);
                }
            }
        }
        else
        {
            e.DrawDefault = true;
        }
    }

    /// <summary>
    /// Translates the UI elements of the form based on the selected language.
    /// </summary>
    private void Translate()
    {
        GlobalData.SetCulture(Settings.Default.SelectedLanguage);

        Text = GlobalData.Strings.GetString("Export");
        btnCancel.Text = GlobalData.Strings.GetString("Cancel");
        btnExportToGame.Text = GlobalData.Strings.GetString("ExportToGame");
        btnExportToStaging.Text = GlobalData.Strings.GetString("ExportToStaging");
        btnOpenStagingFolder.Text = GlobalData.Strings.GetString("OpenStagingFolder");
        btnOpenGameFolder.Text = GlobalData.Strings.GetString("OpenGameFolder");
        lblStatus.Text = GlobalData.Strings.GetString("Ready");

        lvStations.Columns[0].Text = GlobalData.Strings.GetString("LVStationStatus");
        lvStations.Columns[1].Text = GlobalData.Strings.GetString("LVDisplayName");
        lvStations.Columns[2].Text = GlobalData.Strings.GetString("LVStationIcon");
        lvStations.Columns[3].Text = GlobalData.Strings.GetString("LVSongCount");
        lvStations.Columns[4].Text = GlobalData.Strings.GetString("LVStreamURL");
        lvStations.Columns[5].Text = GlobalData.Strings.GetString("LVProposedPath");
    }

    /// <summary>
    /// Populates the ListView with the stations to be exported.
    /// </summary>
    private void PopulateListView()
    {
        var radioExtPath = PathHelper.GetRadiosPath(Settings.Default.GameBasePath);

        foreach (var station in _stationsToExport)
        {
            var isActive = station.GetStatus();
            var customIconString = station.CustomIcon.UseCustom
                ? GlobalData.Strings.GetString("CustomIcon")
                : station.MetaData.Icon;
            var songString = station.MetaData.StreamInfo.IsStream
                ? GlobalData.Strings.GetString("IsStream")
                : station.Songs.Count.ToString();
            var streamString = station.MetaData.StreamInfo.IsStream
                ? station.MetaData.StreamInfo.StreamUrl
                : GlobalData.Strings.GetString("UsingSongs");
            var proposedPath = isActive
                ? Path.Combine(radioExtPath, station.MetaData.DisplayName)
                : GlobalData.Strings.GetString("DisabledStation");

            var lvItem = new ListViewItem(new[]
            {
                string.Empty, // Placeholder for the icon column
                station.MetaData.DisplayName,
                customIconString ?? string.Empty,
                songString ?? string.Empty,
                streamString ?? string.Empty,
                proposedPath ?? string.Empty
            }) { Tag = station };

            lvStations.Items.Add(lvItem);
        }

        lvStations.ResizeColumns();
    }

    /// <summary>
    /// Configures the state of the buttons based on the current settings.
    /// </summary>
    private void ConfigureButtons()
    {
        btnExportToGame.Enabled = !string.IsNullOrEmpty(Settings.Default.GameBasePath);
        btnOpenGameFolder.Enabled = btnExportToGame.Enabled;

        btnExportToStaging.Enabled = !string.IsNullOrEmpty(Settings.Default.StagingPath);
        btnOpenStagingFolder.Enabled = btnExportToStaging.Enabled;
    }

    /// <summary>
    /// Handles the Click event of the btnExportToStaging button.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void BtnExportToStaging_Click(object sender, EventArgs e)
    {
        if (!bgWorkerExport.CancellationPending && !bgWorkerExport.IsBusy)
            bgWorkerExport.RunWorkerAsync();
    }

    /// <summary>
    /// Handles the Click event of the btnExportToGame button.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void BtnExportToGame_Click(object sender, EventArgs e)
    {
        if (ShowNoModDialogIfRequired() && !bgWorkerExportGame.CancellationPending && !bgWorkerExportGame.IsBusy)
            bgWorkerExportGame.RunWorkerAsync();
    }

    /// <summary>
    /// Handles the Click event of the btnCancel button.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (!bgWorkerExport.CancellationPending && bgWorkerExport.IsBusy)
        {
            _isCancelling = true;
            bgWorkerExport.CancelAsync();
        }

        if (!bgWorkerExportGame.CancellationPending && bgWorkerExportGame.IsBusy)
        {
            _isCancelling = true;
            bgWorkerExportGame.CancelAsync();
        }
    }

    /// <summary>
    /// Shows a dialog if the radioExt mod is not installed.
    /// </summary>
    /// <returns><c>true</c> if the mod is installed; otherwise, <c>false</c>.</returns>
    public static bool ShowNoModDialogIfRequired()
    {
        if (string.IsNullOrEmpty(PathHelper.GetRadioExtPath(Settings.Default.GameBasePath)))
        {
            MessageBox.Show(GlobalData.Strings.GetString("NoRadioExtMsg") ??
                            "You do not have the radioExt mod installed. Can't export radio stations to game.",
                GlobalData.Strings.GetString("NoModInstalled") ?? "No Mod Installed",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        return true;
    }

    /// <summary>
    /// Resets the form to its initial state.
    /// </summary>
    private void Reset()
    {
        ToggleButtons();
        UpdateStatus(GlobalData.Strings.GetString("ExportCanceled") ?? "Export Canceled");
        pgExportProgress.Value = 0;

        _isCancelling = false;
        _exportToStagingComplete = false;
        _exportToGameComplete = false;
    }

    /// <summary>
    /// Handles the DoWork event of the bgWorkerExport background worker.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
    private void BgWorkerExport_DoWork(object sender, DoWorkEventArgs e)
    {
        ToggleButtons();

        for (var i = 0; i < _stationsToExport.Count; i++)
        {
            if (bgWorkerExport.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            var station = _stationsToExport[i];
            UpdateStatus(string.Format(_statusString, station.MetaData.DisplayName));
            bgWorkerExport.ReportProgress((int)(i / (float)_stationsToExport.Count * 100));

            var stationPath = CreateStationDirectory(station);
            if (string.IsNullOrEmpty(stationPath)) continue;

            if (!CreateMetaDataJson(stationPath, station)) continue;
            if (station.Songs.Count <= 0) continue;

            if (!CreateSongListJson(stationPath, station))
                Debug.WriteLine("Couldn't save songs.sgls file.");
        }

        RemoveDeletedStations();
    }

    /// <summary>
    /// Handles the ProgressChanged event of the bgWorkerExport background worker.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="ProgressChangedEventArgs"/> instance containing the event data.</param>
    private void RemoveDeletedStations()
    {
        var stationNames = new HashSet<string>(_stationsToExport.Select(station => station.MetaData.DisplayName), StringComparer.OrdinalIgnoreCase);
        var directoriesToDelete = Directory.EnumerateDirectories(Settings.Default.StagingPath)
            .Where(dir => !stationNames.Contains(Path.GetFileName(dir)));

        foreach (var directory in directoriesToDelete)
        {
            try
            {
                Directory.Delete(directory, true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to delete {directory}: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Handles the RunWorkerCompleted event of the bgWorkerExport background worker.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
    private static string CreateStationDirectory(Station station)
    {
        if (string.IsNullOrEmpty(Settings.Default.StagingPath)) return string.Empty;

        var stationPath = Path.Combine(Settings.Default.StagingPath, station.MetaData.DisplayName);
        FileHelper.CreateDirectories(stationPath);
        return stationPath;
    }

    /// <summary>
    /// Creates a metadata JSON file for a station in the specified path.
    /// </summary>
    /// <param name="stationPath">The path where the metadata JSON file will be created.</param>
    /// <param name="station">The station object containing the metadata to be saved.</param>
    /// <returns>True if the file was successfully saved; otherwise, false.</returns>
    private bool CreateMetaDataJson(string stationPath, Station station)
    {
        var mdPath = Path.Combine(stationPath, "metadata.json");
        return _metaDataJson.SaveJson(mdPath, station.MetaData);
    }

    /// <summary>
    /// Creates a song list JSON file for a station in the specified path.
    /// </summary>
    /// <param name="stationPath">The path where the song list JSON file will be created.</param>
    /// <param name="station">The station object containing the songs to be saved.</param>
    /// <returns>True if the file was successfully saved; otherwise, false.</returns>
    private bool CreateSongListJson(string stationPath, Station station)
    {
        var songPath = Path.Combine(stationPath, "songs.sgls");
        return _songListJson.SaveJson(songPath, station.Songs);
    }

    /// <summary>
    /// Handles the ProgressChanged event of the background worker for exporting.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data containing the progress percentage.</param>
    private void BgWorkerExport_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        if (pgExportProgress.Value != e.ProgressPercentage)
            pgExportProgress.Value = e.ProgressPercentage;
    }

    /// <summary>
    /// Handles the RunWorkerCompleted event of the background worker for exporting.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data indicating the operation has completed.</param>
    private void BgWorkerExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (_isCancelling)
        {
            Reset();
        }
        else
        {
            _exportToStagingComplete = true;
            pgExportProgress.Value = 100;
            ToggleButtons();
            UpdateStatus(GlobalData.Strings.GetString("ExportCompleteStatus") ?? "Exported to Staging!");
        }
    }

    /// <summary>
    /// Performs the work of exporting stations to the game directory in the background.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data for the background operation.</param>
    private void BgWorkerExportGame_DoWork(object sender, DoWorkEventArgs e)
    {
        ToggleButtons();
        _dirCopier = new DirectoryCopier((BackgroundWorker)sender);
        var radiosPath = PathHelper.GetRadiosPath(Settings.Default.GameBasePath);

        if (bgWorkerExportGame.CancellationPending)
        {
            e.Cancel = true;
            return;
        }

        var activeStations = _stationsToExport.Where(s => s.GetStatus()).ToList();
        var activeStationNames = activeStations.Select(s => s.MetaData.DisplayName).ToList();

        var stagingPaths = FileHelper.SafeEnumerateDirectories(Settings.Default.StagingPath).ToList();

        var activeStationPaths = stagingPaths
            .Where(path => activeStationNames.Any(name => path.Contains(name, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        CopyDirectoriesToGame(radiosPath, activeStationPaths);

        var liveStationPaths = FileHelper.SafeEnumerateDirectories(radiosPath).ToList();

        var inactiveStationPaths = liveStationPaths
            .Where(path => !activeStationNames.Any(name => path.Contains(name, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        DeleteInactiveDirectories(inactiveStationPaths);
    }

    /// <summary>
    /// Copies directories of active stations to the game directory.
    /// </summary>
    /// <param name="radiosPath">The game's radio directory path.</param>
    /// <param name="activeStationPaths">List of paths of active stations to be copied.</param>
    private void CopyDirectoriesToGame(string radiosPath, List<string> activeStationPaths)
    {
        foreach (var path in activeStationPaths)
        {
            var targetPath = Path.Combine(radiosPath, Path.GetFileName(path));
            _dirCopier?.CopyDirectory(path, targetPath, true);
            CopySongsToGame(path, targetPath); //copy songs to game based on .sgls file (if present)

            Invoke(() => pgExportProgress.Value = 0); //reset progress bar after copy operation

            if (bgWorkerExportGame.CancellationPending)
            {
                bgWorkerExportGame.CancelAsync();
                return;
            }
        }
    }

    /// <summary>
    /// Copies songs specified in the .sgls file from the original path to the target path.
    /// </summary>
    /// <param name="originalPath">The original path of the station.</param>
    /// <param name="targetPath">The target path in the game directory.</param>
    /// <returns>True if songs were successfully copied; otherwise, false.</returns>
    private bool CopySongsToGame(string originalPath, string targetPath)
    {
        var songFile = Directory.GetFiles(originalPath).FirstOrDefault(file => Path.GetExtension(file).Equals(".sgls", StringComparison.OrdinalIgnoreCase));
        if (songFile == null)
            return false;

        SongList? songs = _songListJson.LoadJson(songFile);
        if (songs == null)
            return false;

        var songPathsInSgls = songs.Select(s => Path.Combine(targetPath, Path.GetFileName(s.OriginalFilePath))).ToList();

        // Delete songs not present in the .sgls file
        var existingFiles = Directory.GetFiles(targetPath)
            .Where(file => !Path.GetExtension(file).Equals(".sgls"))
            .Where(file => !Path.GetExtension(file).Equals(".json"));

        foreach (var file in existingFiles)
        {
            if (!songPathsInSgls.Contains(file, StringComparer.OrdinalIgnoreCase))
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to delete {file}: {ex.Message}");
                }
            }
        }

        // Copy songs from the .sgls file
        foreach (var song in songs)
        {
            var sourcePath = song.OriginalFilePath;
            var targetFilePath = Path.Combine(targetPath, Path.GetFileName(sourcePath));
            try
            {
                File.Copy(sourcePath, targetFilePath, true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to copy {sourcePath} to {targetFilePath}: {ex.Message}");
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Deletes directories of inactive stations from the game directory.
    /// </summary>
    /// <param name="inactiveStationPaths">List of paths of inactive stations to be deleted.</param>
    private void DeleteInactiveDirectories(List<string> inactiveStationPaths)
    {
        foreach (var path in inactiveStationPaths)
            try
            {
                Directory.Delete(path, true);

                if (bgWorkerExportGame.CancellationPending)
                {
                    bgWorkerExportGame.CancelAsync();
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to delete directory {path}: {ex.Message}");
            }
    }

    /// <summary>
    /// Handles the ProgressChanged event of the background worker for exporting to the game.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data containing the progress percentage.</param>
    private void BgWorkerExportGame_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        if (pgExportProgress.Value != e.ProgressPercentage)
        {
            pgExportProgress.Value = e.ProgressPercentage;
            UpdateStatus(string.Format(_statusString, _dirCopier?.CurrentFile));
        }
    }

    /// <summary>
    /// Handles the RunWorkerCompleted event of the background worker for exporting to the game.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data indicating the operation has completed.</param>
    private void BgWorkerExportGame_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (_isCancelling)
        {
            Reset();
        }
        else
        {
            _exportToGameComplete = true;
            pgExportProgress.Value = 100;
            ToggleButtons();
            UpdateStatus(GlobalData.Strings.GetString("ExportToGameComplete") ?? "Exported to Game!");
        }
    }

    /// <summary>
    /// Toggles the enabled state of the buttons based on the current operation status.
    /// </summary>
    private void ToggleButtons()
    {
        if (InvokeRequired)
            Invoke(ToggleButtonsImplementation);
        else
            ToggleButtonsImplementation();
    }

    private void ToggleButtonsImplementation()
    {
        btnExportToGame.Enabled = !bgWorkerExport.IsBusy && _exportToStagingComplete && !_exportToGameComplete;
        btnExportToStaging.Enabled = !bgWorkerExport.IsBusy && !_exportToStagingComplete;
        btnCancel.Visible = bgWorkerExport.IsBusy || bgWorkerExportGame.IsBusy;
    }

    /// <summary>
    /// Updates the status label with the specified status message.
    /// </summary>
    /// <param name="status">The status message to display.</param>
    private void UpdateStatus(string status)
    {
        if (InvokeRequired)
            Invoke(() => lblStatus.Text = status);
        else
            lblStatus.Text = status;
    }

    /// <summary>
    /// Handles the Click event of the btnOpenStagingFolder button.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void BtnOpenStagingFolder_Click(object sender, EventArgs e)
    {
        Process.Start("explorer.exe", Settings.Default.StagingPath);
    }

    /// <summary>
    /// Handles the Click event of the btnOpenGameFolder button.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void BtnOpenGameFolder_Click(object sender, EventArgs e)
    {
        if (ShowNoModDialogIfRequired())
            Process.Start("explorer.exe", PathHelper.GetRadiosPath(Settings.Default.GameBasePath));
    }

    /// <summary>
    /// Handles the HelpButtonClicked event of the ExportWindow form.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    private void ExportWindow_HelpButtonClicked(object sender, CancelEventArgs e)
    {
        "https://ethan-hann.github.io/CyberRadio-Assistant/docs/export.html".OpenUrl();
    }
}