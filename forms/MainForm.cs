using Newtonsoft.Json;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;
using RadioExt_Helper.Properties;
using System.ComponentModel;
using RadioExt_Helper.user_controls;
using static RadioExt_Helper.utility.CEventArgs;
using AetherUtils.Core.Files;
using System.Globalization;
using System.Windows.Forms;
using AetherUtils.Core.Extensions;
using AetherUtils.Core.WinForms.Controls;
using AetherUtils.Core.WinForms.Models;
using AetherUtils.Core.Reflection;
using Org.BouncyCastle.Asn1.Ocsp;

namespace RadioExt_Helper.forms
{
    public partial class MainForm : Form
    {
        private readonly BindingList<Station> _stations = [];

        private readonly List<StationEditor> _stationEditors = [];
        private readonly NoStationsCtl _noStationsCtrl = new();

        private readonly Json<MetaData> metaDataJson = new();
        private readonly Json<SongList> songListJson = new();

        private readonly ImageComboBox<ImageComboBoxItem> _languageComboBox = new();
        private readonly List<ImageComboBoxItem> _languages = [];

        public MainForm()
        {
            InitializeComponent();

            GlobalData.Initialize();

            InitializeLanguageDropDown();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Add(_noStationsCtrl);

            ApplyFontsToControls(this);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            SelectLanguage();
            Translate();
            CheckGamePath();

            PopulateStations();
            HandleUserControlVisibility();
        }

        private void InitializeLanguageDropDown()
        {
            //Populate the language combo box
            _languages.Add(new ImageComboBoxItem("English (en)", Resources.united_kingdom));
            _languages.Add(new ImageComboBoxItem("Español (es)", Resources.spain));
            _languages.Add(new ImageComboBoxItem("Français (fr)", Resources.france));
            
            foreach (var language in _languages)
                _languageComboBox.Items.Add(language);

            _languageComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _languageComboBox.SelectedIndexChanged += cmbLanguageSelect_SelectedIndexChanged;

            // Create a ToolStripControlHost to host the ImageComboBox
            ToolStripControlHost toolStripControlHost = new(_languageComboBox);

            // Add the ToolStripControlHost to the "Language" tool strip menu
            languageToolStripMenuItem.DropDownItems.Add(toolStripControlHost);
        }

        private void SelectLanguage()
        {
            if (!Settings.Default.SelectedLanguage.Equals(string.Empty))
                _languageComboBox.SelectedIndex = _languageComboBox.Items.IndexOf(
                    _languages.Find(l => l.Text.Equals(Settings.Default.SelectedLanguage)));
            else
                _languageComboBox.SelectedIndex = 0;

            cmbLanguageSelect_SelectedIndexChanged(_languageComboBox, EventArgs.Empty);
        }

        private void Translate()
        {
            Text = GlobalData.Strings.GetString("MainTitle");
            fileToolStripMenuItem.Text = GlobalData.Strings.GetString("File");
            exportStationsToolStripMenuItem.Text = GlobalData.Strings.GetString("ExportToStaging");
            exportToGameToolStripMenuItem.Text = GlobalData.Strings.GetString("ExportToGame");
            languageToolStripMenuItem.Text = GlobalData.Strings.GetString("Language");
            helpToolStripMenuItem.Text = GlobalData.Strings.GetString("Help");
            pathsToolStripMenuItem.Text = GlobalData.Strings.GetString("GamePaths");
            refreshStationsToolStripMenuItem.Text = GlobalData.Strings.GetString("RefreshStations");
            howToUseToolStripMenuItem.Text = GlobalData.Strings.GetString("HowToUse");
            radioExtGitHubToolStripMenuItem.Text = GlobalData.Strings.GetString("RadioExtGithub");
            radioExtOnNexusModsToolStripMenuItem.Text = GlobalData.Strings.GetString("RadioExtNexusMods");
            aboutToolStripMenuItem.Text = GlobalData.Strings.GetString("About");

            grpStations.Text = GlobalData.Strings.GetString("Stations");

            //Buttons
            btnAddStation.Text = GlobalData.Strings.GetString("NewStation");
            btnDeleteStation.Text = GlobalData.Strings.GetString("DeleteStation");

        }

        private void HandleUserControlVisibility()
        {
            if (_stations.Count <= 0)
            {
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(_noStationsCtrl);
                _noStationsCtrl.Visible = true;
            }
        }

        private void ApplyFontsToControls(Control control)
        {
            if (control is IUserControl userControl)
                userControl.ApplyFonts();
            else
            {
                switch (control)
                {
                    case MenuStrip:
                    case GroupBox:
                    case Button:
                        FontHandler.Instance.ApplyFont(control, "CyberPunk_Regular", 9, FontStyle.Bold);
                        break;
                    case TabControl:
                        FontHandler.Instance.ApplyFont(control, "CyberPunk_Regular", 12, FontStyle.Bold);
                        break;
                    case Label:
                        FontHandler.Instance.ApplyFont(control, "CyberPunk_Regular", 9, FontStyle.Regular);
                        break;
                }

                foreach (Control child in control.Controls)
                    ApplyFontsToControls(child);
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
                _stationEditors.Clear();

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
                        s.SongsAsList = [.. songList];

                    _stations.Add(s);
                    StationEditor editor = new(s);
                    editor.StationUpdated += UpdateStation;
                    editor.PreLoad();

                    _stationEditors.Add(editor);
                }
            }

            lbStations.DataSource = _stations;
            if (lbStations.Items.Count > 0)
            {
                lbStations.SelectedIndex = 0;
                lbStations_SelectedIndexChanged(lbStations, EventArgs.Empty);
            }

            HandleUserControlVisibility();

            lbStations.EndUpdate();
        }

        #region File Menu
        private void exportStationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbStations.Items.Count <= 0) return;

            //TODO: Export radio stations
            foreach (var item in lbStations.Items)
            {
                if (item is not Station station) continue;

                //Create the directory(ies) for the station in the staging path
                var stationPath = CreateStationDirectory(station);

                if (stationPath.Equals(string.Empty)) continue;

                //Create the metadata json
                var metadataSaved = CreateMetaDataJSON(stationPath, station);
                if (!metadataSaved) continue;

                //Create the song list json (if needed)
                if (station.Songs.Count <= 0) continue;
                var songListSaved = CreateSongListJSON(stationPath, station);

                //Copy songs to staging folder
                CopySongsToStaging(stationPath, station);
            }

            //Refresh stations list box
            PopulateStations();
        }

        private string CreateStationDirectory(Station station)
        {
            if (Settings.Default.BackupPath.Equals(string.Empty)) return string.Empty;

            //string cleanName = FileHelper.RemoveInvalidFileNameChars(station.MetaData.DisplayName);
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

        private void ExportFromStagingToLive()
        {
            //TODO: export stations from staging to live radioExt directory
            throw new NotImplementedException();
        }

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
            "https://github.com/justarandomguyintheinternet/CP77_radioExt".OpenUrl();
        }

        private void radioExtOnNexusModsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            "https://www.nexusmods.com/cyberpunk2077/mods/4591".OpenUrl();
        }

        #endregion

        private void lbStations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbStations.SelectedItem is not Station station) return;

            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(_stationEditors.Find(
                s => s.Station.MetaData.DisplayName.Equals(station.MetaData.DisplayName)));
            splitContainer1.Panel2.ResumeLayout();
            //_stationEditorCtrl.SetMetaData(station.MetaData, station.SongList);
        }

        private int _newStationCount = 1;
        private void btnAddStation_Click(object sender, EventArgs e)
        {
            Station blankStation = new();
            blankStation.MetaData.DisplayName = $"{GlobalData.Strings.GetString("NewStationListBoxEntry")} {_newStationCount}";

            _stations.Add(blankStation);
            _stationEditors.Add(new StationEditor(blankStation));
            lbStations.SelectedItem = blankStation;
            _newStationCount++;
            
            if (_stations.Count <= 0) return;

            //Re-show our station editor if the station count has increased again.
            _noStationsCtrl.Visible = false;
            lbStations_SelectedIndexChanged(this, EventArgs.Empty);

            //var editor = _stationEditors.Find(s => s.Station.MetaData.DisplayName.Equals(blankStation.MetaData.DisplayName));
            //if (editor != null)
            //    editor.Visible = true;
        }

        private void btnDeleteStation_Click(object sender, EventArgs e)
        {
            if (lbStations.SelectedItem is not Station station) return;

            _stations.Remove(station);
            _stationEditors.Remove(_stationEditors
                .Where(s => s.Station.MetaData.DisplayName
                .Equals(station.MetaData.DisplayName))
                .First());

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

            lbStations_SelectedIndexChanged(this, EventArgs.Empty);

            if (_stations.Count > 0) return;

            if (_stations.Count <= 0)
                _newStationCount = 1;

            //Hide the station editor (and reset it) if there are no stations to edit.
            HandleUserControlVisibility();

            //_stationEditorCtrl.Visible = false;
            //_stationEditorCtrl.SetMetaData(new MetaData(), new SongList());
            //_noStationsCtrl.Visible = true;
        }

        private void UpdateStation(object? sender, EventArgs e)
        {
            //if (e is StationUpdatedEventArgs args)
            //{
            //    int index = _stations.IndexOf(_stations.Where(s => s.MetaData.DisplayName.Equals(args.PreviousStationName)).FirstOrDefault());
            //    if (index != -1)
            //        _stations[index] = args.UpdatedStation;
            //    //RefreshListBox();
            //    //if (lbStations.SelectedItem is Station station)
            //    //{
            //    //    lbStations.BeginUpdate();
            //    //    station.MetaData = args.UpdatedStation.MetaData;
            //    //    station.StreamInfo = args.UpdatedStation.StreamInfo;
            //    //    station.CustomIcon = args.UpdatedStation.CustomIcon;
            //    //    station.SongsAsList = args.UpdatedStation.SongsAsList;
            //    //    lbStations.EndUpdate();
            //    //}
            //}
        }

        private void RefreshListBox()
        {
            var selectedIndex = lbStations.SelectedIndex;
            lbStations.BeginUpdate();
            lbStations.DataSource = null;
            lbStations.DataSource = _stations;
            lbStations.DisplayMember = "MetaData";
            lbStations.SelectedIndex = selectedIndex;
            lbStations.EndUpdate();
        }

        private void cmbLanguageSelect_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_languageComboBox.SelectedItem is ImageComboBoxItem culture)
            {
                SuspendLayout();
                GlobalData.SetCulture(culture.Text);

                Translate();

                foreach (StationEditor se in _stationEditors)
                    se.Translate();

                _noStationsCtrl.Translate();

                //foreach (Control c in splitContainer1.Panel2.Controls)
                //{
                //    if (c is IUserControl userControl)
                //        userControl.Translate();
                //    //switch (c)
                //    //{
                //    //    case StationEditor se:
                //    //        se.Translate();
                //    //        break;
                //    //    case NoStationsCtl nsc:
                //    //        nsc.Translate();
                //    //        break;
                //    //    default:
                //    //        continue;
                //    //}
                //}

                

                Focus(); //re-focus the main form

                languageToolStripMenuItem.HideDropDown();
                ResumeLayout();

                Settings.Default.SelectedLanguage = culture.Text;
                Settings.Default.Save();
            }
        }
    }
}
