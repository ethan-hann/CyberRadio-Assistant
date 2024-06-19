using AetherUtils.Core.Extensions;
using AetherUtils.Core.Files;
using AetherUtils.Core.Reflection;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;
using System.ComponentModel;
using System.Diagnostics;

namespace RadioExt_Helper.forms
{
    public partial class ExportWindow : Form
    {
        private readonly List<Station> _stationsToExport;

        private readonly Json<MetaData> metaDataJson = new();
        private readonly Json<SongList> songListJson = new();

        private bool isCancelling = false;
        private bool exportToStagingComplete = false;
        private bool exportToGameComplete = false;

        public ExportWindow(List<Station> stations)
        {
            InitializeComponent();
            _stationsToExport = stations;
        }

        private void ExportWindow_Load(object sender, EventArgs e)
        {
            ApplyFonts(this);
            Translate();
            PopulateListView();
        }

        private void Translate()
        {
            GlobalData.SetCulture(Settings.Default.SelectedLanguage);

            Text = GlobalData.Strings.GetString("Export");
            lblConfirm.Text = GlobalData.Strings.GetString("ExportHelp");
            btnCancel.Text = GlobalData.Strings.GetString("Cancel");
            btnExportToGame.Text = GlobalData.Strings.GetString("ExportToGame");
            btnExportToStaging.Text = GlobalData.Strings.GetString("ExportToStaging");
            btnOpenStagingFolder.Text = GlobalData.Strings.GetString("OpenStagingFolder");
            btnOpenGameFolder.Text = GlobalData.Strings.GetString("OpenGameFolder");
            lblStatus.Text = GlobalData.Strings.GetString("Ready");

            //ListView Translations
            lvStations.Columns[0].Text = GlobalData.Strings.GetString("LVDisplayName");
            lvStations.Columns[1].Text = GlobalData.Strings.GetString("LVStationIcon");
            lvStations.Columns[2].Text = GlobalData.Strings.GetString("LVSongCount");
            lvStations.Columns[3].Text = GlobalData.Strings.GetString("LVStreamURL");
            lvStations.Columns[4].Text = GlobalData.Strings.GetString("LVProposedPath");
        }

        private void ApplyFonts(Control control)
        {
            switch (control)
            {
                case MenuStrip:
                case GroupBox:
                case Button:
                    FontHandler.Instance.ApplyFont(control, "CyberPunk_Regular", 10, FontStyle.Bold);
                    break;
                case Label:
                    FontHandler.Instance.ApplyFont(control, "CyberPunk_Regular", 13, FontStyle.Regular);
                    break;
            }

            foreach (Control child in control.Controls)
                ApplyFonts(child);
        }

        private void PopulateListView()
        {
            var radioExtPath = PathHelper.GetRadiosPath(Settings.Default.GameBasePath);

            foreach (Station s in _stationsToExport)
            {
                var customIconString = s.CustomIcon.UseCustom ? GlobalData.Strings.GetString("CustomIcon") : s.MetaData.Icon;
                var songString = s.MetaData.StreamInfo.IsStream ? GlobalData.Strings.GetString("IsStream") : s.Songs.Count.ToString();
                var streamString = s.MetaData.StreamInfo.IsStream ? s.MetaData.StreamInfo.StreamUrl : GlobalData.Strings.GetString("UsingSongs");
                var proposedPath = Path.Combine(radioExtPath, s.MetaData.DisplayName);

                ListViewItem lvItem = new ListViewItem(new string[]
                {
                    s.MetaData.DisplayName,
                    customIconString ?? "",
                    songString ?? "",
                    streamString ?? "",
                    proposedPath
                })
                {
                    Tag = s
                };

                lvStations.Items.Add(lvItem);
            }

            lvStations.ResizeColumns();
        }

        private void btnExportToStaging_Click(object sender, EventArgs e)
        {
            if (!bgWorkerExport.CancellationPending && !bgWorkerExport.IsBusy)
                bgWorkerExport.RunWorkerAsync();
        }

        private void btnExportToGame_Click(object sender, EventArgs e)
        {
            if (!bgWorkerExportGame.CancellationPending && !bgWorkerExportGame.IsBusy)
                bgWorkerExportGame.RunWorkerAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!bgWorkerExport.CancellationPending && bgWorkerExport.IsBusy)
            {
                isCancelling = true;
                bgWorkerExport.CancelAsync();
            }
        }

        private void Reset()
        {
            ToggleButtons();
            UpdateStatus(GlobalData.Strings.GetString("ExportCanceled"));
            pgExportProgress.Value = 0;

            isCancelling = false;
            exportToStagingComplete = false;
            exportToGameComplete = false;
        }

        private void bgWorkerExport_DoWork(object sender, DoWorkEventArgs e)
        {
            var statusString = GlobalData.Strings.GetString("ExportingStationStatus") ?? "Exporting station: {0}";

            ToggleButtons();
            for (int i = 0; i < _stationsToExport.Count; i++)
            {
                if (bgWorkerExport.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                Station station = _stationsToExport[i];
                UpdateStatus(string.Format(statusString, station.MetaData.DisplayName));
                int progressPercentage = (int)((i / (float)_stationsToExport.Count) * 100);
                bgWorkerExport.ReportProgress(progressPercentage);

                //Create the directory(ies) for the station in the staging path
                var stationPath = CreateStationDirectory(station);
                if (stationPath.Equals(string.Empty)) continue;

                //Create the metadata json
                var metaDataSaved = CreateMetaDataJSON(stationPath, station);
                if (!metaDataSaved) continue;

                //Create the song list json (if needed)
                if (station.Songs.Count > 0)
                {
                    _ = CreateSongListJSON(stationPath, station);

                    //Copy songs to staging folder
                    CopySongsToStaging(stationPath, station);
                }
            }
        }

        private string CreateStationDirectory(Station station)
        {
            if (Settings.Default.BackupPath.Equals(string.Empty)) return string.Empty;

            var stationPath = Path.Combine(Settings.Default.BackupPath, station.MetaData.DisplayName);
            FileHelper.CreateDirectories(stationPath);
            return stationPath;
        }

        private bool CreateMetaDataJSON(string stationPath, Station station)
        {
            if (Settings.Default.BackupPath.Equals(string.Empty)) return false;

            var mdPath = Path.Combine(stationPath, "metadata.json");
            return metaDataJson.SaveJson(mdPath, station.MetaData);
        }

        private bool CreateSongListJSON(string stationPath, Station station)
        {
            if (Settings.Default.BackupPath.Equals(string.Empty)) return false;

            var songPath = Path.Combine(stationPath, "songs.sgls");
            return songListJson.SaveJson(songPath, station.Songs);
        }

        private void CopySongsToStaging(string stationPath, Station station)
        {
            if (Settings.Default.BackupPath.Equals(string.Empty)) return;

            var files = Directory.GetFiles(stationPath);

            //Remove original song files from staging folder
            foreach (var file in files)
            {
                if (FileHelper.GetExtension(file, false).Equals(".json") || FileHelper.GetExtension(file, false).Equals(".sgls"))
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
            if (isCancelling)
            {
                Reset();
            }
            else
            {
                exportToStagingComplete = true;
                pgExportProgress.Value = 100;
                ToggleButtons();
                UpdateStatus(GlobalData.Strings.GetString("ExportCompleteStatus"));
            }
        }

        private void bgWorkerExportGame_DoWork(object sender, DoWorkEventArgs e)
        {
            ToggleButtons();
            for (int i = 0; i < 10000; i++)
            {
                if (bgWorkerExportGame.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                //TODO: actucally export files!
                var statusString = GlobalData.Strings.GetString("ExportingStationStatus");
                UpdateStatus(string.Format(statusString, i));
                //UpdateStatus($"Exporting station {i}");
                int progressPercentage = (int)((i / (float)10000) * 100);
                bgWorkerExportGame.ReportProgress(progressPercentage);
            }
        }

        private void bgWorkerExportGame_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pgExportProgress.Value = e.ProgressPercentage;
        }

        private void bgWorkerExportGame_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            exportToGameComplete = true;
            pgExportProgress.Value = 100;
            ToggleButtons();
            UpdateStatus(GlobalData.Strings.GetString("ExportToGameComplete"));
        }

        private void ToggleButtons()
        {
            if (InvokeRequired)
            {
                Invoke(() =>
                {
                    if (isCancelling)
                    {
                        btnExportToGame.Enabled = false;
                        btnExportToStaging.Enabled = true;
                        btnCancel.Visible = false;
                    }
                    else if (exportToStagingComplete && !exportToGameComplete)
                    {
                        btnExportToGame.Enabled = true;
                        btnExportToStaging.Enabled = false;
                        btnCancel.Visible = false;
                    }
                    else if (exportToGameComplete && exportToStagingComplete)
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
                });
            }
            else
            {
                if (isCancelling)
                {
                    btnExportToGame.Enabled = false;
                    btnExportToStaging.Enabled = true;
                    btnCancel.Visible = false;
                }
                else if (exportToStagingComplete && !exportToGameComplete)
                {
                    btnExportToGame.Enabled = true;
                    btnExportToStaging.Enabled = false;
                    btnCancel.Visible = false;
                }
                else if (exportToGameComplete)
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
        }

        private void UpdateStatus(string? status)
        {
            if (status == null) return;

            if (InvokeRequired)
            {
                Invoke(() =>
                {
                    lblStatus.Text = status;
                });
            }
            else
                lblStatus.Text = status;
        }

        private void btnOpenStagingFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Settings.Default.BackupPath);
        }

        private void btnOpenGameFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", PathHelper.GetRadiosPath(Settings.Default.GameBasePath));
        }
    }
}
