using Newtonsoft.Json;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;
using RadioExt_Helper.Properties;
using System.ComponentModel;
using RadioExt_Helper.user_controls;
using static RadioExt_Helper.utility.CEventArgs;
using AetherUtils.Core.Files;

namespace RadioExt_Helper.forms
{
    public partial class MainForm : Form
    {
        private readonly BindingList<Station> _stations = [];
        private readonly StationEditor _stationEditorCtrl = new();
        private readonly NoStationsCtl _noStationsCtrl = new();

        private readonly Json<MetaData> metaDataJson = new Json<MetaData>();
        private readonly Json<SongList> songListJson = new Json<SongList>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //ApplyFonts();
            GlobalData.Initialize();

            _stationEditorCtrl.StationUpdated += UpdateStation;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Translate();
            CheckGamePath();
            PopulateStations();

            splitContainer1.Panel2.Controls.Add(_noStationsCtrl);
            splitContainer1.Panel2.Controls.Add(_stationEditorCtrl);

            _noStationsCtrl.Visible = _stations.Count <= 0;
            _stationEditorCtrl.Visible = !_noStationsCtrl.Visible;
        }

        private void Translate()
        {
            Text = GlobalData.Strings.GetString("MainTitle");
            fileToolStripMenuItem.Text = GlobalData.Strings.GetString("File");
            languageToolStripMenuItem.Text = GlobalData.Strings.GetString("Language");
            helpToolStripMenuItem.Text = GlobalData.Strings.GetString("Help");
            pathsToolStripMenuItem.Text = GlobalData.Strings.GetString("GamePaths");
            refreshStationsToolStripMenuItem.Text = GlobalData.Strings.GetString("RefreshStations");
            howToUseToolStripMenuItem.Text = GlobalData.Strings.GetString("HowToUse");
            radioExtGitHubToolStripMenuItem.Text = GlobalData.Strings.GetString("RadioExtGithub");
            radioExtOnNexusModsToolStripMenuItem.Text = GlobalData.Strings.GetString("RadioExtNexusMods");
            aboutToolStripMenuItem.Text = GlobalData.Strings.GetString("About");

            //Buttons
            btnAddStation.Text = GlobalData.Strings.GetString("NewStation");
            btnDeleteStation.Text = GlobalData.Strings.GetString("DeleteStation");

        }

        private void ApplyFonts()
        {
            FontLoader.Initialize();
            foreach (Control control in Controls)
            {
                if (control.GetType() == typeof(MenuStrip))
                {
                    FontLoader.ApplyCustomFont(control, 10, true);
                }
                else
                {
                    FontLoader.ApplyCustomFont(control, 12);
                }
            }
        }

        private void CheckGamePath()
        {
            if (!Settings.Default.GameBasePath.Equals(string.Empty)) return;

            var caption = GlobalData.Strings.GetString("NoGamePath");
            var text = GlobalData.Strings.GetString("NoExeFound");
            var result = MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if (result is not (DialogResult.OK or DialogResult.Cancel)) return;

            var basePath = PathHelper.GetGamePath(fdlgOpenGameExe, true);
            if (basePath != null && basePath.Equals(string.Empty)) return;

            Settings.Default.GameBasePath = basePath;
            Settings.Default.Save();
        }

        private void PopulateStations()
        {
            lbStations.BeginUpdate();

            if (!Settings.Default.BackupPath.Equals(string.Empty))
            {
                _stations.Clear();

                foreach (var directory in Directory.EnumerateDirectories(Settings.Default.BackupPath))
                {
                    MetaData? metaData = null;
                    SongList? songList = null;

                    foreach (var file in Directory.EnumerateFiles(directory))
                    {
                        var extension = FileHelper.GetExtension(file);
                        
                        switch (extension)
                        {
                            case ".json":
                                metaData = metaDataJson.LoadJson(file);
                                break;
                            case ".sgls":
                                songList = songListJson.LoadJson(file);
                                break;
                            default:
                                continue;
                        }
                    }

                    Station s = new();
                    if (metaData != null)
                        s.MetaData = metaData;

                    if (songList != null)
                        s.SongList = songList;

                    _stations.Add(s);
                }
            }

            lbStations.DataSource = _stations;
            lbStations.EndUpdate();
        }

        #region File Menu
        private void pathsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PathSettings().ShowDialog();
        }

        private void refreshStationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopulateStations();
        }
        #endregion

        #region Help Menu
        private void radioExtHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalData.OpenUrl("https://github.com/justarandomguyintheinternet/CP77_radioExt");
        }

        private void radioExtOnNexusModsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalData.OpenUrl("https://www.nexusmods.com/cyberpunk2077/mods/4591");
        }

        #endregion

        private void lbStations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbStations.SelectedItem is not Station station) return;
            _stationEditorCtrl.SetMetaData(station.MetaData, station.SongList); //TODO: change to use song list read from disk
        }

        private int _newStationCount = 1;
        private void btnAddStation_Click(object sender, EventArgs e)
        {
            Station blankStation = new()
            {
                MetaData = new MetaData() { DisplayName = $"{GlobalData.Strings.GetString("NewStationListBoxEntry")} {_newStationCount}" },
                SongList = []
            };

            _stations.Add(blankStation);
            lbStations.SelectedItem = blankStation;
            _newStationCount++;

            //Re-show our station editor if the station count has increased again.
            if (_stations.Count <= 0) return;
            
            _noStationsCtrl.Visible = false;
            _stationEditorCtrl.Visible = true;
        }

        private void btnDeleteStation_Click(object sender, EventArgs e)
        {
            if (lbStations.SelectedItem is not Station station) return;
            
            _stations.Remove(station);

            //If the station to be removed contains "[New Station]" in the name, decrement our new station count.
            if (station.MetaData.DisplayName.Contains(
                    GlobalData.Strings.GetString("NewStationListBoxEntry") ??
                    throw new InvalidOperationException()))
                _newStationCount--;

            //Reset new station count if there are no more "New stations" in the list box.
            if (!_stations.Any(s => s.MetaData.DisplayName.Contains(
                    GlobalData.Strings.GetString("NewStationListBoxEntry") ?? 
                    throw new InvalidOperationException())))
                _newStationCount = 1;

            //Hide the station editor (and reset it) if there are no stations to edit.
            if (_stations.Count > 0) return;
            
            _stationEditorCtrl.Visible = false;
            _stationEditorCtrl.SetMetaData(new MetaData(), new SongList());
            _noStationsCtrl.Visible = true;
        }

        private void UpdateStation(object? sender, EventArgs e)
        {
            if (e is StationUpdatedEventArgs args)
            {
                
                //TODO: Update station metadata with changed version
            }
        }

        private void cmbLanguageSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLanguageSelect.SelectedItem is string culture)
            {
                GlobalData.SetCulture(culture);
                Translate();
                foreach (Control c in splitContainer1.Panel2.Controls)
                {
                    switch (c)
                    {
                        case StationEditor se:
                            se.Translate();
                            break;
                        case NoStationsCtl nsc:
                            nsc.Translate();
                            break;
                        default:
                            continue;
                    }
                }
            }

            cmbLanguageSelect.DroppedDown = false;
        }

        
    }
}
