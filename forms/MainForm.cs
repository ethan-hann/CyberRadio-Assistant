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
        private BindingList<MetaData> _stations = [];

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //ApplyFonts();

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            CheckGamePath();
            PopulateStations();
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
            if (Settings.Default.GameBasePath.Equals(string.Empty))
            {
                DialogResult result = MessageBox.Show("The path to the Cyberpunk 2077 executable has not been set." +
                                                      "\nPlease set it now.", "No Game Path", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (result is DialogResult.OK or DialogResult.Cancel)
                {
                    string basePath = PathHelper.GetGamePath(fdlgOpenGameExe, true);
                    if (!basePath.Equals(string.Empty))
                    {
                        Settings.Default.GameBasePath = basePath;
                        Settings.Default.Save();
                    }
                }
            }
        }

        private void PopulateStations()
        {
            lbStations.BeginUpdate();

            if (!Settings.Default.BackupPath.Equals(string.Empty))
            {
                _stations.Clear();
                foreach (string directory in Directory.EnumerateDirectories(Settings.Default.BackupPath))
                {
                    foreach (string file in Directory.EnumerateFiles(directory))
                    {
                        if (!Path.GetExtension(file).Equals(".json")) { continue; }

                        string json = File.ReadAllText(file);
                        MetaData? md = JsonConvert.DeserializeObject<MetaData>(json);
                        if (md != null)
                            _stations.Add(md);
                    }
                }
            }
            lbStations.DataSource = _stations;
            lbStations.EndUpdate();
        }

        private void pathsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PathSettings().ShowDialog();
        }

        private void refreshStationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopulateStations();
        }

        private void lbStations_SelectedIndexChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            if (lbStations.SelectedItem is MetaData station)
            {
                StationEditor editorControl = new(station);
                splitContainer1.Panel2.Controls.Add(editorControl);
                editorControl.StationUpdated += UpdateStation;
            }
            
        }

        private void UpdateStation(object? sender, EventArgs e)
        {
            if (e is StationUpdatedEventArgs args)
            {
                //TODO: Update station metadata with changed version
            }
        }
    }
}
