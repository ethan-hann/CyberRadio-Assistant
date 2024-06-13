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

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //ApplyFonts();
            GlobalData.Initialize();
            cmbLanguageSelect.SelectedIndex = 0;

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Translate();
            CheckGamePath();
            PopulateStations();
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
            splitContainer1.Panel2.Controls.Clear();
            if (lbStations.SelectedItem is not MetaData station) return;

            StationEditor editorControl = new(station);
            splitContainer1.Panel2.Controls.Add(editorControl);
            editorControl.StationUpdated += UpdateStation;

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
            var selectedCulture = cmbLanguageSelect.SelectedItem;

            var culture = selectedCulture?.ToString();
            if (culture != null)
                GlobalData.SetCulture(culture);
            cmbLanguageSelect.DroppedDown = false;
            Translate();
        }
    }
}
