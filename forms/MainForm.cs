using Newtonsoft.Json;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;
using System.Diagnostics;
using RadioExt_Helper.Properties;
using System.ComponentModel;
using RadioExt_Helper.user_controls;
using static RadioExt_Helper.utility.CEventArgs;

namespace RadioExt_Helper.forms
{
    public partial class MainForm : Form
    {
        private readonly BindingList<MetaData> _stations = [];
        private readonly StationEditor _stationEditorCtrl = new();
        private readonly NoStationsCtl _noStationsCtrl = new();

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
            if (basePath.Equals(string.Empty)) return;

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
                    foreach (var file in Directory.EnumerateFiles(directory))
                    {
                        if (!Path.GetExtension(file).Equals(".json")) { continue; }

                        var json = File.ReadAllText(file);
                        var md = JsonConvert.DeserializeObject<MetaData>(json);
                        if (md != null)
                            _stations.Add(md);
                    }
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
            if (lbStations.SelectedItem is not MetaData station) return;
            _stationEditorCtrl.SetMetaData(station);
        }

        private int _newStationCount = 1;
        private void btnAddStation_Click(object sender, EventArgs e)
        {
            MetaData blankStation = new();
            blankStation.DisplayName = $"{GlobalData.Strings.GetString("NewStationListBoxEntry")} {_newStationCount}";

            _stations.Add(blankStation);
            lbStations.SelectedItem = blankStation;
            _newStationCount++;

            //Reshow our station editor if the station count has increased again.
            if (_stations.Count > 0)
            {
                _noStationsCtrl.Visible = false;
                _stationEditorCtrl.Visible = true;
            }
        }

        private void btnDeleteStation_Click(object sender, EventArgs e)
        {
            if (lbStations.SelectedItem is MetaData station)
            {
                _stations.Remove(station);

                //If the station to be removed contains "[New Station]" in the name, decrement our new station count.
                if (station.DisplayName.Contains(GlobalData.Strings.GetString("NewStationListBoxEntry")))
                    _newStationCount--;

                //Reset new station count if there are no more "New stations" in the list box.
                if (!_stations.Where(s => s.DisplayName.Contains(GlobalData.Strings.GetString("NewStationListBoxEntry"))).Any())
                    _newStationCount = 1;

                //Hide the station editor (and reset it) if there are no stations to edit.
                if (_stations.Count <= 0)
                {
                    _stationEditorCtrl.Visible = false;
                    _stationEditorCtrl.SetMetaData(new MetaData());
                    _noStationsCtrl.Visible = true;
                }
            }
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
                    if (c is StationEditor se)
                        se.Translate();
                    else if (c is NoStationsCtl nsc)
                        nsc.Translate();
                    else
                        continue;
                }
            }

            cmbLanguageSelect.DroppedDown = false;
        }

        
    }
}
