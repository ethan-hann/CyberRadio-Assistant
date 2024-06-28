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
        PopulateListView();

        if (Settings.Default.GameBasePath.Equals(string.Empty))
        {
            btnExportToGame.Enabled = false;
            btnOpenGameFolder.Enabled = false;
        }

        if (Settings.Default.StagingPath.Equals(string.Empty))
        {
            btnExportToStaging.Enabled = false;
            btnOpenStagingFolder.Enabled = false;
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

        // ListView Translations
        lvStations.Columns[0].Text = GlobalData.Strings.GetString("LVDisplayName");
        lvStations.Columns[1].Text = GlobalData.Strings.GetString("LVStationIcon");
        lvStations.Columns[2].Text = GlobalData.Strings.GetString("LVSongCount");
        lvStations.Columns[3].Text = GlobalData.Strings.GetString("LVStreamURL");
        lvStations.Columns[4].Text = GlobalData.Strings.GetString("LVProposedPath");
    }

    private void PopulateListView()
    {
        var radioExtPath = PathHelper.GetRadiosPath(Settings.Default.GameBasePath);

        foreach (var lvItem in from station in _stationsToExport
                 let customIconString = station.CustomIcon.UseCustom
                     ? GlobalData.Strings.GetString("CustomIcon")
                     : station.MetaData.Icon
                 let songString = station.MetaData.StreamInfo.IsStream
                     ? GlobalData.Strings.GetString("IsStream")
                     : station.Songs.Count.ToString()
                 let streamString = station.MetaData.StreamInfo.IsStream
                     ? station.MetaData.StreamInfo.StreamUrl
                     : GlobalData.Strings.GetString("UsingSongs")
                 let proposedPath = Path.Combine(radioExtPath, station.MetaData.DisplayName)
                 select new ListViewItem([
                         station.MetaData.DisplayName,
                         customIconString ?? string.Empty,
                         songString ?? string.Empty,
                         streamString ?? string.Empty,
                         proposedPath
                     ])
                     { Tag = station })
            lvStations.Items.Add(lvItem);

        lvStations.ResizeColumns();
    }

    private void btnExportToStaging_Click(object sender, EventArgs e)
    {
        if (!bgWorkerExport.CancellationPending && !bgWorkerExport.IsBusy)
            bgWorkerExport.RunWorkerAsync();
    }

    private void btnExportToGame_Click(object sender, EventArgs e)
    {
        if (!ShowNoModDialogIfRequired()) return;

        if (!bgWorkerExportGame.CancellationPending && !bgWorkerExportGame.IsBusy)
            bgWorkerExportGame.RunWorkerAsync();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        if (!bgWorkerExport.CancellationPending && bgWorkerExport.IsBusy)
        {
            _isCancelling = true;
            bgWorkerExport.CancelAsync();
        }
    }

    private bool ShowNoModDialogIfRequired()
    {
        if (PathHelper.GetRadioExtPath(Settings.Default.GameBasePath).Equals(string.Empty))
        {
            var caption = GlobalData.Strings.GetString("NoModInstalled") ?? "No Mod Installed";
            var text = GlobalData.Strings.GetString("NoRadioExtMsg") ??
                       "You do not have the radioExt mod installed. Can't export radio stations to game.";
            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        var statusString = GlobalData.Strings.GetString("ExportingStationStatus") ?? "Exporting station: {0}";

        ToggleButtons();

        //Remove station folders from staging if not present in current station list
        RemoveDeletedStations();

        for (var i = 0; i < _stationsToExport.Count; i++)
        {
            if (bgWorkerExport.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            var station = _stationsToExport[i];
            UpdateStatus(string.Format(statusString, station.MetaData.DisplayName));
            var progressPercentage = (int)(i / (float)_stationsToExport.Count * 100);
            bgWorkerExport.ReportProgress(progressPercentage);

            var stationPath = CreateStationDirectory(station);
            if (string.IsNullOrEmpty(stationPath)) continue;

            if (!CreateMetaDataJson(stationPath, station)) continue;

            if (station.Songs.Count <= 0) continue;
            
            _ = CreateSongListJson(stationPath, station);
            CopySongsToStaging(stationPath, station);
        }
    }

    //private void CreateStationListJson()
    //{
    //    List<Station> activeStations = _stationsToExport.Where(s => s.IsActive).ToList();
    //}

    private void RemoveDeletedStations()
    {
        var stationNames = _stationsToExport.Select(station => station.MetaData.DisplayName);
        var directories = Directory.GetDirectories(Settings.Default.StagingPath);
        var directoriesToDelete = directories
            .Where(dir => !stationNames.Contains(Path.GetFileName(dir), StringComparer.OrdinalIgnoreCase))
            .ToList();

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

    private string CreateStationDirectory(Station station)
    {
        if (string.IsNullOrEmpty(Settings.Default.StagingPath)) return string.Empty;

        var stationPath = Path.Combine(Settings.Default.StagingPath, station.MetaData.DisplayName);
        FileHelper.CreateDirectories(stationPath);
        return stationPath;
    }

    private bool CreateMetaDataJson(string stationPath, Station station)
    {
        if (string.IsNullOrEmpty(Settings.Default.StagingPath)) return false;

        var mdPath = Path.Combine(stationPath, "metadata.json");
        return _metaDataJson.SaveJson(mdPath, station.MetaData);
    }

    private bool CreateSongListJson(string stationPath, Station station)
    {
        if (string.IsNullOrEmpty(Settings.Default.StagingPath)) return false;

        var songPath = Path.Combine(stationPath, "songs.sgls");
        return _songListJson.SaveJson(songPath, station.Songs);
    }

    private static void CopySongsToStaging(string stationPath, Station station)
    {
        if (string.IsNullOrEmpty(Settings.Default.StagingPath)) return;

        foreach (var file in Directory.GetFiles(stationPath))
        {
            if (FileHelper.GetExtension(file, false).Equals(".json") ||
                FileHelper.GetExtension(file, false).Equals(".sgls"))
                continue;
            File.Delete(file);
        }

        foreach (var song in station.Songs)
        {
            var songPath = Path.Combine(stationPath, Path.GetFileName(song.OriginalFilePath));
            if (FileHelper.DoesFileExist(song.OriginalFilePath))
                File.Copy(song.OriginalFilePath, songPath, true);
        }
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

    private DirectoryCopier? _dirCopier;
    private readonly string _statusString = GlobalData.Strings.GetString("ExportingStationStatus")
                                  ?? "Exporting station: {0}";
    private void bgWorkerExportGame_DoWork(object sender, DoWorkEventArgs e)
    {
        ToggleButtons();
        _dirCopier = new DirectoryCopier((BackgroundWorker)sender);
        var radiosPath = PathHelper.GetRadiosPath(Settings.Default.GameBasePath);
        
        if (bgWorkerExportGame.CancellationPending)
            e.Cancel = true;
        _dirCopier.CopyDirectory(Settings.Default.StagingPath, radiosPath, true);
    }

    private void bgWorkerExportGame_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        pgExportProgress.Value = e.ProgressPercentage;
        UpdateStatus(string.Format(_statusString, _dirCopier?.CurrentFile));
    }

    private void bgWorkerExportGame_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        _exportToGameComplete = true;
        pgExportProgress.Value = 100;
        ToggleButtons();
        UpdateStatus(GlobalData.Strings.GetString("ExportToGameComplete") ?? "Exported to Game!");
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
        if (_isCancelling)
        {
            btnExportToGame.Enabled = false;
            btnExportToStaging.Enabled = true;
            btnCancel.Visible = false;
        }
        else if (_exportToStagingComplete && !_exportToGameComplete)
        {
            btnExportToGame.Enabled = true;
            btnExportToStaging.Enabled = false;
            btnCancel.Visible = false;
        }
        else if (_exportToGameComplete && _exportToStagingComplete)
        {
            btnExportToGame.Enabled = false;
            btnExportToStaging.Enabled = false;
            btnCancel.Visible = false;
        }
        else
        {
            btnExportToGame.Enabled = false;
            btnExportToStaging.Enabled = false;
            btnCancel.Visible = true;
        }
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
        if (!ShowNoModDialogIfRequired()) return;

        Process.Start("explorer.exe", PathHelper.GetRadiosPath(Settings.Default.GameBasePath));
    }

    private void ExportWindow_HelpButtonClicked(object sender, CancelEventArgs e)
    {
        "https://ethan-hann.github.io/CyberRadio-Assistant/docs/export.html".OpenUrl();
    }
}