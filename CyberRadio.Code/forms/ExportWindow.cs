using System.ComponentModel;
using System.Diagnostics;
using AetherUtils.Core.Extensions;
using AetherUtils.Core.Files;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

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

    public ExportWindow(List<Station> stations)
    {
        InitializeComponent();
        _stationsToExport = stations;
    }

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

    private void ConfigureButtons()
    {
        btnExportToGame.Enabled = !string.IsNullOrEmpty(Settings.Default.GameBasePath);
        btnOpenGameFolder.Enabled = btnExportToGame.Enabled;

        btnExportToStaging.Enabled = !string.IsNullOrEmpty(Settings.Default.StagingPath);
        btnOpenStagingFolder.Enabled = btnExportToStaging.Enabled;
    }

    private void btnExportToStaging_Click(object sender, EventArgs e)
    {
        if (!bgWorkerExport.CancellationPending && !bgWorkerExport.IsBusy)
            bgWorkerExport.RunWorkerAsync();
    }

    private void btnExportToGame_Click(object sender, EventArgs e)
    {
        if (ShowNoModDialogIfRequired() && !bgWorkerExportGame.CancellationPending && !bgWorkerExportGame.IsBusy)
            bgWorkerExportGame.RunWorkerAsync();
    }

    private void btnCancel_Click(object sender, EventArgs e)
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

    private void Reset()
    {
        ToggleButtons();
        UpdateStatus(GlobalData.Strings.GetString("ExportCanceled") ?? "Export Canceled");
        pgExportProgress.Value = 0;

        _isCancelling = false;
        _exportToStagingComplete = false;
        _exportToGameComplete = false;
    }

    private void bgWorkerExport_DoWork(object sender, DoWorkEventArgs e)
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

            if (CreateSongListJson(stationPath, station))
                CopySongsToStaging(stationPath, station);
            else
                Debug.WriteLine("Couldn't save songs.sgls file.");
        }

        RemoveDeletedStations();
    }

    private void RemoveDeletedStations()
    {
        var stationNames = _stationsToExport.Select(station => station.MetaData.DisplayName);
        var directoriesToDelete = Directory.GetDirectories(Settings.Default.StagingPath)
            .Where(dir => !stationNames.Contains(Path.GetFileName(dir), StringComparer.OrdinalIgnoreCase))
            .ToList();

        foreach (var directory in directoriesToDelete)
            try
            {
                Directory.Delete(directory, true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to delete {directory}: {ex.Message}");
            }
    }

    private string CreateStationDirectory(Station station)
    {
        if (string.IsNullOrEmpty(Settings.Default.StagingPath)) return string.Empty;

        var stationPath = Path.Combine(Settings.Default.StagingPath, station.MetaData.DisplayName);
        FileHelper.CreateDirectories(stationPath);
        return stationPath;
    }

    private bool CreateMetaDataJson(string stationPath, Station station)
    {
        var mdPath = Path.Combine(stationPath, "metadata.json");
        return _metaDataJson.SaveJson(mdPath, station.MetaData);
    }

    private bool CreateSongListJson(string stationPath, Station station)
    {
        var songPath = Path.Combine(stationPath, "songs.sgls");
        return _songListJson.SaveJson(songPath, station.Songs);
    }

    private static void CopySongsToStaging(string stationPath, Station station)
    {
        var existingSongFiles = Directory.GetFiles(stationPath)
            .Where(file => !FileHelper.GetExtension(file, false).Equals(".json") &&
                           !FileHelper.GetExtension(file, false).Equals(".sgls"))
            .ToList();

        var songFilesToCopy = station.Songs
            .Select(song => new
            {
                SourcePath = song.OriginalFilePath,
                TargetPath = Path.Combine(stationPath, Path.GetFileName(song.OriginalFilePath))
            })
            .ToList();

        // Identify songs to delete by excluding those that match the source and target paths
        var filesToDelete = existingSongFiles
            .Where(existingFile =>
                !songFilesToCopy.Any(s => s.TargetPath.Equals(existingFile, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        // Delete old song files
        foreach (var fileToDelete in filesToDelete) File.Delete(fileToDelete);

        // Copy new or updated song files
        foreach (var songFile in songFilesToCopy)
            if (FileHelper.DoesFileExist(songFile.SourcePath))
                if (!songFile.SourcePath.Equals(songFile.TargetPath, StringComparison.OrdinalIgnoreCase))
                    File.Copy(songFile.SourcePath, songFile.TargetPath, true);
    }

    private void bgWorkerExport_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        pgExportProgress.Value = e.ProgressPercentage;
    }

    private void bgWorkerExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

    private void bgWorkerExportGame_DoWork(object sender, DoWorkEventArgs e)
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

    private void CopyDirectoriesToGame(string radiosPath, List<string> activeStationPaths)
    {
        foreach (var path in activeStationPaths)
        {
            var targetPath = Path.Combine(radiosPath, Path.GetFileName(path));
            _dirCopier?.CopyDirectory(path, targetPath, true);

            Invoke(() => pgExportProgress.Value = 0); //reset progress bar after copy operation

            if (bgWorkerExportGame.CancellationPending)
            {
                bgWorkerExportGame.CancelAsync();
                return;
            }
        }
    }

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

    private void bgWorkerExportGame_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        pgExportProgress.Value = e.ProgressPercentage;
        UpdateStatus(string.Format(_statusString, _dirCopier?.CurrentFile));
    }

    private void bgWorkerExportGame_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

    private void UpdateStatus(string status)
    {
        if (InvokeRequired)
            Invoke(() => lblStatus.Text = status);
        else
            lblStatus.Text = status;
    }

    private void btnOpenStagingFolder_Click(object sender, EventArgs e)
    {
        Process.Start("explorer.exe", Settings.Default.StagingPath);
    }

    private void btnOpenGameFolder_Click(object sender, EventArgs e)
    {
        if (ShowNoModDialogIfRequired())
            Process.Start("explorer.exe", PathHelper.GetRadiosPath(Settings.Default.GameBasePath));
    }

    private void ExportWindow_HelpButtonClicked(object sender, CancelEventArgs e)
    {
        "https://ethan-hann.github.io/CyberRadio-Assistant/docs/export.html".OpenUrl();
    }
}