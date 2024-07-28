// ExportWindow.cs : RadioExt-Helper
// Copyright (C) 2024  Ethan Hann
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.ComponentModel;
using System.Diagnostics;
using AetherUtils.Core.Extensions;
using AetherUtils.Core.Files;
using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

/// <summary>
///     Represents the export window form, allowing users to export radio stations to the game or staging area.
/// </summary>
public partial class ExportWindow : Form
{
    private readonly ImageList _imageList = new();

    private readonly Json<MetaData> _metaDataJson = new();
    private readonly Json<List<Song>> _songListJson = new();
    private readonly List<TrackableObject<Station>> _stationsToExport;

    private readonly string _statusString =
        GlobalData.Strings.GetString("ExportingStationStatus") ?? "Exporting station: {0}";

    private DirectoryCopier? _dirCopier;
    private bool _exportToGameComplete;
    private bool _exportToStagingComplete;
    private bool _isCancelling;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ExportWindow" /> class with the specified stations to export.
    /// </summary>
    public ExportWindow()
    {
        InitializeComponent();
        _stationsToExport = StationManager.Instance.StationsAsList;

        SetImageList();
    }

    private static string GameBasePath => GlobalData.ConfigManager.Get("gameBasePath") as string ?? string.Empty;

    private static string StagingPath => GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;

    private static bool ShouldAutoExportToGame => (bool)(GlobalData.ConfigManager.Get("autoExportToGame") ?? false);

    public event EventHandler? OnExportToStagingComplete;
    public event EventHandler? OnExportToGameComplete;

    /// <summary>
    ///     Handles the Load event of the ExportWindow form.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
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

    private void SetImageList()
    {
        _imageList.Images.Add("enabled", Resources.enabled);
        _imageList.Images.Add("disabled", Resources.disabled);
        _imageList.ImageSize = new Size(16, 16);
        lvStations.SmallImageList = _imageList;
    }

    /// <summary>
    ///     Draws the sub-items of the stations ListView, including custom icons.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="DrawListViewSubItemEventArgs" /> instance containing the event data.</param>
    private void LvStations_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e)
    {
        if (e.ColumnIndex == 0) // Assuming the icon is in the first column
        {
            if (e.Item == null || lvStations.SmallImageList == null ||
                e.Item.Tag is not TrackableObject<Station> station) return;

            var image = lvStations.SmallImageList.Images[station.TrackedObject.GetStatus() ? "enabled" : "disabled"];
            if (image == null) return;

            // Calculate the position to center the image in the cell
            var iconX = e.Bounds.Left + (e.Bounds.Width - image.Width) / 2;
            var iconY = e.Bounds.Top + (e.Bounds.Height - image.Height) / 2;
            e.Graphics.DrawImage(image, iconX, iconY);
        }
        else
        {
            e.DrawDefault = true;
        }
    }

    /// <summary>
    ///     Translates the UI elements of the form based on the selected language.
    /// </summary>
    private void Translate()
    {
        Text = GlobalData.Strings.GetString("Export");
        btnCancel.Text = GlobalData.Strings.GetString("Cancel");
        btnExportToGame.Text = GlobalData.Strings.GetString("ExportToGame");
        btnExportToStaging.Text = GlobalData.Strings.GetString("ExportToStaging");
        btnOpenStagingFolder.Text = GlobalData.Strings.GetString("OpenStagingFolder");
        btnOpenGameFolder.Text = GlobalData.Strings.GetString("OpenGameFolder");
        lblStatus.Text = GlobalData.Strings.GetString("Ready");
        lblTip.Text = GlobalData.Strings.GetString("ExportWindowTip");

        lvStations.Columns[0].Text = GlobalData.Strings.GetString("LVStationStatus");
        lvStations.Columns[1].Text = GlobalData.Strings.GetString("LVDisplayName");
        lvStations.Columns[2].Text = GlobalData.Strings.GetString("LVStationIcon");
        lvStations.Columns[3].Text = GlobalData.Strings.GetString("LVSongCount");
        lvStations.Columns[4].Text = GlobalData.Strings.GetString("LVStreamURL");
        lvStations.Columns[5].Text = GlobalData.Strings.GetString("LVProposedPath");
    }

    /// <summary>
    ///     Populates the ListView with the stations to be exported.
    /// </summary>
    private void PopulateListView()
    {
        var radioExtPath = PathHelper.GetRadiosPath(GameBasePath);

        lvStations.SuspendLayout();
        foreach (var lvItem in from station in _stationsToExport
                 let isActive = station.TrackedObject.GetStatus()
                 let customIconString = station.TrackedObject.CustomIcon.UseCustom
                     ? GlobalData.Strings.GetString("CustomIcon")
                     : station.TrackedObject.MetaData.Icon
                 let songString = station.TrackedObject.MetaData.StreamInfo.IsStream
                     ? GlobalData.Strings.GetString("IsStream")
                     : station.TrackedObject.Songs.Count.ToString()
                 let streamString = station.TrackedObject.MetaData.StreamInfo.IsStream
                     ? station.TrackedObject.MetaData.StreamInfo.StreamUrl
                     : GlobalData.Strings.GetString("UsingSongs")
                 let proposedPath = isActive
                     ? Path.Combine(radioExtPath, station.TrackedObject.MetaData.DisplayName)
                     : GlobalData.Strings.GetString("DisabledStation")
                 select new ListViewItem([
                     string.Empty, // Placeholder for the icon column
                     station.TrackedObject.MetaData.DisplayName,
                     customIconString ?? string.Empty,
                     songString ?? string.Empty,
                     streamString ?? string.Empty,
                     proposedPath ?? string.Empty
                 ])
                 {
                     Tag = station
                 })
            lvStations.Items.Add(lvItem);

        lvStations.ResizeColumns();
        lvStations.ResumeLayout();
    }

    /// <summary>
    ///     Configures the state of the buttons based on the current settings.
    /// </summary>
    private void ConfigureButtons()
    {
        btnExportToGame.Enabled = false;
        btnOpenGameFolder.Enabled = !string.IsNullOrEmpty(GameBasePath);

        btnExportToStaging.Enabled = !string.IsNullOrEmpty(StagingPath);
        btnOpenStagingFolder.Enabled = btnExportToStaging.Enabled;
    }

    /// <summary>
    ///     Handles the Click event of the btnExportToStaging button.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void BtnExportToStaging_Click(object sender, EventArgs e)
    {
        if (!bgWorkerExport.CancellationPending && !bgWorkerExport.IsBusy)
            bgWorkerExport.RunWorkerAsync();
    }

    /// <summary>
    ///     Handles the Click event of the btnExportToGame button.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void BtnExportToGame_Click(object sender, EventArgs e)
    {
        if (ShowNoModDialogIfRequired() && !bgWorkerExportGame.CancellationPending && !bgWorkerExportGame.IsBusy)
            bgWorkerExportGame.RunWorkerAsync();
    }

    /// <summary>
    ///     Handles the Click event of the btnCancel button.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (!bgWorkerExport.CancellationPending && bgWorkerExport.IsBusy)
        {
            _isCancelling = true;
            bgWorkerExport.CancelAsync();
        }

        if (bgWorkerExportGame.CancellationPending || !bgWorkerExportGame.IsBusy) return;

        _isCancelling = true;
        bgWorkerExportGame.CancelAsync();
    }

    /// <summary>
    ///     Shows a dialog if the radioExt mod is not installed.
    /// </summary>
    /// <returns><c>true</c> if the mod is installed; otherwise, <c>false</c>.</returns>
    public static bool ShowNoModDialogIfRequired()
    {
        if (!string.IsNullOrEmpty(PathHelper.GetRadioExtPath(GameBasePath))) return true;

        MessageBox.Show(GlobalData.Strings.GetString("NoRadioExtMsg") ??
                        "You do not have the radioExt mod installed. Can't export radio stations to game.",
            GlobalData.Strings.GetString("NoModInstalled") ?? "No Mod Installed",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
    }

    /// <summary>
    ///     Resets the form to its initial state.
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
    ///     Handles the DoWork event of the bgWorkerExport background worker.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="DoWorkEventArgs" /> instance containing the event data.</param>
    private void BgWorkerExport_DoWork(object sender, DoWorkEventArgs e)
    {
        ToggleButtons();

        // Get the list of existing directories before exporting
        var existingDirectories = FileHelper.SafeEnumerateDirectories(StagingPath).ToList();
        var songDirectoryMap = MapSongsToDirectories(existingDirectories, _stationsToExport);

        for (var i = 0; i < _stationsToExport.Count; i++)
        {
            if (bgWorkerExport.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            var station = _stationsToExport[i];
            UpdateStatus(string.Format(_statusString, station.TrackedObject.MetaData.DisplayName));
            bgWorkerExport.ReportProgress((int)(i / (float)_stationsToExport.Count * 100));

            var newStationPath = CreateStationDirectory(station);
            if (string.IsNullOrEmpty(newStationPath)) continue;

            if (!CreateMetaDataJson(newStationPath, station)) continue;
            if (station.TrackedObject.Songs.Count <= 0) continue;

            // Copy song files from old directory to new directory
            CopySongFiles(songDirectoryMap, newStationPath, station);

            if (!CreateSongListJson(newStationPath, station))
                AuLogger.GetCurrentLogger<ExportWindow>("BG_ExportStaging")
                    .Error(
                        "Couldn't save the songs.sgls file. This means that CRA doesn't know where your station's songs are located.");
        }

        RemoveDeletedStations(existingDirectories);
        AuLogger.GetCurrentLogger<ExportWindow>("BG_ExportToStaging")
            .Info($"Exported {_stationsToExport.Count} stations to staging directory: {StagingPath}");
    }

    /// <summary>
    /// Maps song directories to the station directories. Used to keep track of where the songs are located, in case the station name is updated.
    /// </summary>
    /// <param name="existingDirectories">The existing station directories in the staging folder.</param>
    /// <param name="stations">The list of <see cref="TrackableObject{Station}"/> stations to use for mapping.</param>
    /// <returns>A dictionary containing the directory-song mappings.</returns>
    private static Dictionary<string, string> MapSongsToDirectories(List<string> existingDirectories,
        List<TrackableObject<Station>> stations)
    {
        var songDirectoryMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var station in stations)
        foreach (var song in station.TrackedObject.Songs)
        {
            var songDirectory = existingDirectories
                .FirstOrDefault(dir => song.FilePath.StartsWith(dir, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(songDirectory)) songDirectoryMap[song.FilePath] = songDirectory;
        }

        return songDirectoryMap;
    }

    /// <summary>
    /// Copy song files from the old station directory to the new station directory.
    /// </summary>
    /// <param name="songDirectoryMap">A dictionary containing the mapping between the old station name and the song.</param>
    /// <param name="newStationPath">The path to the new station's directory.</param>
    /// <param name="station">The station to copy songs for.</param>
    private void CopySongFiles(Dictionary<string, string> songDirectoryMap, string newStationPath,
        TrackableObject<Station> station)
    {
        foreach (var song in station.TrackedObject.Songs)
        {
            // Check if the song file is within the station's directory
            if (!songDirectoryMap.TryGetValue(song.FilePath, out var oldStationPath) ||
                !IsFileInDirectory(song.FilePath, oldStationPath) ||
                !PathHelper.IsValidAudioFile(song.FilePath)) continue;

            var oldFilePath = Path.Combine(oldStationPath, Path.GetFileName(song.FilePath));
            var newFilePath = Path.Combine(newStationPath, Path.GetFileName(song.FilePath));

            try
            {
                if (!File.Exists(oldFilePath)) continue;

                File.Copy(oldFilePath, newFilePath, true);
                song.FilePath = newFilePath; //Update file path of the song to the new station's directory

                AuLogger.GetCurrentLogger<ExportWindow>("CopySongFiles")
                    .Info($"Copied song from old station folder: {oldFilePath} to new station folder: {newFilePath}.");
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<ExportWindow>("CopySongFiles")
                    .Error(ex, $"Failed to copy {oldFilePath} to {newFilePath}.");
            }
        }
    }

    /// <summary>
    /// Get a value indicating whether the specified file is in the specified directory.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="directoryPath">The directory path to check against.</param>
    /// <returns><c>true</c> if the file is in the directory; <c>false</c> otherwise.</returns>
    private static bool IsFileInDirectory(string filePath, string directoryPath)
    {
        var fileUri = new Uri(filePath);
        var directoryUri = new Uri(directoryPath);

        return fileUri.AbsolutePath.StartsWith(directoryUri.AbsolutePath, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Removes deleted station directories from the staging path.
    /// </summary>
    private void RemoveDeletedStations(List<string> existingDirectories)
    {
        var stationNames = new HashSet<string>(
            _stationsToExport.Select(station => station.TrackedObject.MetaData.DisplayName),
            StringComparer.OrdinalIgnoreCase);

        var directoriesToDelete = existingDirectories
            .Where(dir => !stationNames.Contains(Path.GetFileName(dir)))
            .ToList();

        foreach (var directory in directoriesToDelete)
        {
            try
            {
                Directory.Delete(directory, true);
                AuLogger.GetCurrentLogger<ExportWindow>("RemoveDeletedStations")
                    .Info($"Deleted station directory: {directory}");
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<ExportWindow>("RemoveDeletedStations")
                    .Error(ex, $"Failed to delete {directory}.");
            }
        }
    }

    /// <summary>
    ///     Creates the station directory in the staging path for the specified <see cref="Station" />.
    /// </summary>
    /// <param name="station">The station to create the directory for.</param>
    /// <returns>The path to the station's directory.</returns>
    private static string CreateStationDirectory(TrackableObject<Station> station)
    {
        if (string.IsNullOrEmpty(StagingPath)) return string.Empty;

        var stationPath = Path.Combine(StagingPath, station.TrackedObject.MetaData.DisplayName);
        FileHelper.CreateDirectories(stationPath);
        return stationPath;
    }

    /// <summary>
    ///     Creates a metadata JSON file for a station in the specified path.
    /// </summary>
    /// <param name="stationPath">The path where the metadata JSON file will be created.</param>
    /// <param name="station">The station object containing the metadata to be saved.</param>
    /// <returns>True if the file was successfully saved; otherwise, false.</returns>
    private bool CreateMetaDataJson(string stationPath, TrackableObject<Station> station)
    {
        var mdPath = Path.Combine(stationPath, "metadata.json");
        return _metaDataJson.SaveJson(mdPath, station.TrackedObject.MetaData);
    }

    /// <summary>
    ///     Creates a song list JSON file for a station in the specified path.
    /// </summary>
    /// <param name="stationPath">The path where the song list JSON file will be created.</param>
    /// <param name="station">The station object containing the songs to be saved.</param>
    /// <returns>True if the file was successfully saved; otherwise, false.</returns>
    private bool CreateSongListJson(string stationPath, TrackableObject<Station> station)
    {
        var songPath = Path.Combine(stationPath, "songs.sgls");
        return _songListJson.SaveJson(songPath, station.TrackedObject.Songs);
    }

    /// <summary>
    ///     Handles the ProgressChanged event of the background worker for exporting.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data containing the progress percentage.</param>
    private void BgWorkerExport_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        if (pgExportProgress.Value != e.ProgressPercentage)
            pgExportProgress.Value = e.ProgressPercentage;
    }

    /// <summary>
    ///     Handles the RunWorkerCompleted event of the background worker for exporting.
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
            _stationsToExport.ForEach(s => s.AcceptChanges());
            StationManager.Instance.ResetNewStations();

            OnExportToStagingComplete?.Invoke(this, EventArgs.Empty);

            if (ShouldAutoExportToGame)
                BtnExportToGame_Click(this, EventArgs.Empty);
        }
    }

    /// <summary>
    ///     Performs the work of exporting stations to the game directory in the background.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data for the background operation.</param>
    private void BgWorkerExportGame_DoWork(object sender, DoWorkEventArgs e)
    {
        ToggleButtons();
        _dirCopier = new DirectoryCopier((BackgroundWorker)sender);
        var radiosPath = PathHelper.GetRadiosPath(GameBasePath);

        if (bgWorkerExportGame.CancellationPending)
        {
            e.Cancel = true;
            return;
        }

        var activeStations = _stationsToExport.Where(s => s.TrackedObject.GetStatus()).ToList();
        var activeStationNames = activeStations.Select(s => s.TrackedObject.MetaData.DisplayName).ToList();

        var stagingPaths = FileHelper.SafeEnumerateDirectories(StagingPath).ToList();

        var activeStationPaths = stagingPaths
            .Where(path => activeStationNames.Any(name => path.Contains(name, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        CopyDirectoriesToGame(radiosPath, activeStationPaths);

        var liveStationPaths = FileHelper.SafeEnumerateDirectories(radiosPath).ToList();

        var inactiveStationPaths = liveStationPaths
            .Where(path => !activeStationNames.Any(name => path.Contains(name, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        DeleteInactiveDirectories(inactiveStationPaths);

        AuLogger.GetCurrentLogger<ExportWindow>("BG_ExportToGame")
            .Info($"Exported {activeStations.Count} stations to game radios directory: {radiosPath}");
    }

    /// <summary>
    ///     Copies directories of active stations to the game directory.
    /// </summary>
    /// <param name="radiosPath">The game's radio directory path.</param>
    /// <param name="activeStationPaths">List of paths of active stations to be copied.</param>
    private void CopyDirectoriesToGame(string radiosPath, List<string> activeStationPaths)
    {
        foreach (var path in activeStationPaths)
        {
            var targetPath = Path.Combine(radiosPath, Path.GetFileName(path));
            _dirCopier?.CopyDirectory(path, targetPath, true);
            _ = CopySongsToGame(path, targetPath); //copy songs to game based on .sgls file (if present)

            Invoke(() => pgExportProgress.Value = 0); //reset progress bar after copy operation

            if (!bgWorkerExportGame.CancellationPending) continue;

            bgWorkerExportGame.CancelAsync();
            return;
        }
    }

    /// <summary>
    ///     Copies songs specified in the .sgls file from the original path to the target path.
    /// </summary>
    /// <param name="originalPath">The original path of the station.</param>
    /// <param name="targetPath">The target path in the game directory.</param>
    /// <returns>True if songs were successfully copied; otherwise, false.</returns>
    private bool CopySongsToGame(string originalPath, string targetPath)
    {
        var songFile = Directory.GetFiles(originalPath).FirstOrDefault(file =>
            Path.GetExtension(file).Equals(".sgls", StringComparison.OrdinalIgnoreCase));
        if (songFile == null)
            return false;

        var songs = _songListJson.LoadJson(songFile);
        if (songs == null)
            return false;

        var songPathsInSgls =
            songs.Select(s => Path.Combine(targetPath, Path.GetFileName(s.FilePath))).ToList();

        // Delete songs not present in the .sgls file
        var existingFiles = Directory.GetFiles(targetPath)
            .Where(file => !Path.GetExtension(file).Equals(".sgls"))
            .Where(file => !Path.GetExtension(file).Equals(".json"));

        foreach (var file in existingFiles)
        {
            if (songPathsInSgls.Contains(file, StringComparer.OrdinalIgnoreCase)) continue;

            try
            {
                File.Delete(file);
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<ExportWindow>("CopySongsToGame")
                    .Error(ex, $"Failed to delete {file}.");
            }
        }

        // Copy songs from the .sgls file
        foreach (var song in songs)
        {
            var sourcePath = song.FilePath;
            var targetFilePath = Path.Combine(targetPath, Path.GetFileName(sourcePath));
            try
            {
                File.Copy(sourcePath, targetFilePath, true);
                AuLogger.GetCurrentLogger<ExportWindow>("CopySongsToGame")
                    .Info($"Copied song: {sourcePath} to {targetFilePath}");
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<ExportWindow>("CopySongsToGame")
                    .Error(ex, $"Failed to copy {sourcePath} to {targetFilePath}");
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Deletes directories of inactive stations from the game directory.
    /// </summary>
    /// <param name="inactiveStationPaths">List of paths of inactive stations to be deleted.</param>
    private void DeleteInactiveDirectories(List<string> inactiveStationPaths)
    {
        foreach (var path in inactiveStationPaths)
        {
            try
            {
                Directory.Delete(path, true);

                if (!bgWorkerExportGame.CancellationPending) continue;

                bgWorkerExportGame.CancelAsync();
                return;
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<ExportWindow>("DeleteInactiveDirectories")
                    .Error(ex, $"Failed to delete directory {path}");
            }
        }
    }

    /// <summary>
    ///     Handles the ProgressChanged event of the background worker for exporting to the game.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Event data containing the progress percentage.</param>
    private void BgWorkerExportGame_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        if (pgExportProgress.Value == e.ProgressPercentage) return;

        pgExportProgress.Value = e.ProgressPercentage;
        UpdateStatus(string.Format(_statusString, _dirCopier?.CurrentFile));
    }

    /// <summary>
    ///     Handles the RunWorkerCompleted event of the background worker for exporting to the game.
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

            OnExportToGameComplete?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    ///     Toggles the enabled state of the buttons based on the current operation status.
    /// </summary>
    private void ToggleButtons()
    {
        this.SafeInvoke(() =>
        {
            btnExportToGame.Enabled = !bgWorkerExport.IsBusy && _exportToStagingComplete && !_exportToGameComplete;
            btnExportToStaging.Enabled = !bgWorkerExport.IsBusy && !_exportToStagingComplete;
            btnCancel.Visible = bgWorkerExport.IsBusy || bgWorkerExportGame.IsBusy;
        });
    }

    /// <summary>
    ///     Updates the status label with the specified status message.
    /// </summary>
    /// <param name="status">The status message to display.</param>
    private void UpdateStatus(string status)
    {
        this.SafeInvoke(() => { lblStatus.Text = status; });
    }

    /// <summary>
    ///     Handles the Click event of the btnOpenStagingFolder button.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void BtnOpenStagingFolder_Click(object sender, EventArgs e)
    {
        Process.Start("explorer.exe", StagingPath);
    }

    /// <summary>
    ///     Handles the Click event of the btnOpenGameFolder button.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void BtnOpenGameFolder_Click(object sender, EventArgs e)
    {
        if (ShowNoModDialogIfRequired())
            Process.Start("explorer.exe", PathHelper.GetRadiosPath(GameBasePath));
    }

    /// <summary>
    ///     Handles the HelpButtonClicked event of the ExportWindow form.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="CancelEventArgs" /> instance containing the event data.</param>
    private void ExportWindow_HelpButtonClicked(object sender, CancelEventArgs e)
    {
        "https://ethan-hann.github.io/CyberRadio-Assistant/docs/export.html".OpenUrl();
    }
}