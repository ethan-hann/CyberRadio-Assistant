using System.ComponentModel;
using AetherUtils.Core.Extensions;
using AetherUtils.Core.Files;
using AetherUtils.Core.Reflection;
using AetherUtils.Core.WinForms.Controls;
using AetherUtils.Core.WinForms.Models;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.user_controls;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

public partial class MainForm : Form
{
    private readonly ImageComboBox<ImageComboBoxItem> _languageComboBox = new();
    private readonly List<ImageComboBoxItem> _languages = [];

    private readonly Json<MetaData> _metaDataJson = new();
    private readonly NoStationsCtl _noStationsCtrl = new();
    private readonly Json<SongList> _songListJson = new();

    private readonly List<StationEditor> _stationEditors = [];
    private readonly BindingList<Station> _stations = [];
    private bool _ignoreSelectedIndexChanged;

    private int _newStationCount = 1;

    private int _previousStationIndex = -1;

    public MainForm()
    {
        InitializeComponent();

        GlobalData.Initialize();

        InitializeLanguageDropDown();
        SelectLanguage();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        _noStationsCtrl.PathsSet += RefreshAfterPathsChanged;
        splitContainer1.Panel2.Controls.Add(_noStationsCtrl);
        
        Translate();
    }

    private void MainForm_Shown(object sender, EventArgs e)
    {
        SelectLanguage();
        Translate();
        //CheckStagingPath();
        //CheckGamePath();

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
        exportToGameToolStripMenuItem.Text = GlobalData.Strings.GetString("ExportStations");
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
        if (_stations.Count > 0) return;
        
        splitContainer1.Panel2.Controls.Clear();
        splitContainer1.Panel2.Controls.Add(_noStationsCtrl);
        _noStationsCtrl.Visible = true;
    }

    private void CheckGamePath()
    {
        if (!Settings.Default.GameBasePath.Equals(string.Empty)) return;

        var caption = GlobalData.Strings.GetString("NoGamePath");
        var text = GlobalData.Strings.GetString("NoExeFound");
        var result = MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        if (result is not (DialogResult.OK or DialogResult.Cancel)) return;

        var basePath = PathHelper.GetGamePath(true);
        if (basePath != null && basePath.Equals(string.Empty)) return;

        Settings.Default.GameBasePath = basePath;
        Settings.Default.Save();
    }

    private void CheckStagingPath()
    {
        if (!Settings.Default.StagingPath.Equals(string.Empty)) return;

        var caption = GlobalData.Strings.GetString("NoStagingPath");
        var text = GlobalData.Strings.GetString("NoStagingPathFound");
        var result = MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        if (result is not (DialogResult.OK or DialogResult.Cancel)) return;

        var stagingPath = PathHelper.GetStagingPath(true);
        if (stagingPath.Equals(string.Empty)) return;

        Settings.Default.StagingPath = stagingPath;
        Settings.Default.Save();
        
    }

    //private void PopulateStations()
    //{
    //    lbStations.BeginUpdate();

    //    if (!Settings.Default.BackupPath.Equals(string.Empty))
    //    {
    //        _stations.Clear();
    //        _stationEditors.Clear();

    //        foreach (var directory in Directory.EnumerateDirectories(Settings.Default.BackupPath))
    //        {
    //            MetaData? metaData = null;
    //            SongList? songList = null;

    //            foreach (var file in Directory.EnumerateFiles(directory))
    //            {
    //                var extension = FileHelper.GetExtension(file);

    //                switch (extension)
    //                {
    //                    case ".json":
    //                        metaData = _metaDataJson.LoadJson(file);
    //                        break;
    //                    case ".sgls":
    //                        songList = _songListJson.LoadJson(file);
    //                        break;
    //                    default:
    //                        continue;
    //                }
    //            }

    //            Station s = new();
    //            if (metaData != null)
    //                s.MetaData = metaData;

    //            if (songList != null)
    //                s.SongsAsList = [.. songList];

    //            _stations.Add(s);
    //            StationEditor editor = new(s);

    //            _stationEditors.Add(editor);
    //        }
    //    }

    //    lbStations.DataSource = _stations;
    //    if (lbStations.Items.Count > 0)
    //    {
    //        lbStations.SelectedIndex = 0;
    //        lbStations_SelectedIndexChanged(lbStations, EventArgs.Empty);
    //    }

    //    HandleUserControlVisibility();

    //    lbStations.EndUpdate();
    //}

    public void PopulateStations()
    {
        lbStations.BeginUpdate();

        if (!string.IsNullOrEmpty(Settings.Default.StagingPath))
        {
            _stations.Clear();
            _stationEditors.Clear();

            foreach (var directory in Directory.EnumerateDirectories(Settings.Default.StagingPath))
            {
                var files = Directory.EnumerateFiles(directory).ToList();

                var metaData = files
                    .Where(file => FileHelper.GetExtension(file) == ".json")
                    .Select(_metaDataJson.LoadJson)
                    .FirstOrDefault() ?? new MetaData();

                var songList = files
                    .Where(file => FileHelper.GetExtension(file) == ".sgls")
                    .Select(_songListJson.LoadJson)
                    .FirstOrDefault() ?? [];

                var station = new Station
                {
                    MetaData = metaData,
                    SongsAsList = [.. songList]
                };

                _stations.Add(station);
                _stationEditors.Add(new StationEditor(station));
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

    private void RefreshAfterPathsChanged(object? sender, EventArgs e)
    {
        PopulateStations();
        if (_stations.Count > 0)
        {
            lbStations.SelectedIndex = 0;
            lbStations_SelectedIndexChanged(this, EventArgs.Empty);
        }
    }

    private void lbStations_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_ignoreSelectedIndexChanged)
        {
            _ignoreSelectedIndexChanged = false;
            return;
        }

        if (lbStations.SelectedItem is not Station station) return;

        _stationEditors.ForEach(editor => { editor.GetMusicPlayer().StopStream(); });

        if (lbStations.SelectedIndex != ListBox.NoMatches)
            _previousStationIndex = lbStations.SelectedIndex;

        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.Panel2.Controls.Clear();
        splitContainer1.Panel2.Controls.Add(_stationEditors.Find(
            s => s.Station.MetaData.DisplayName.Equals(station.MetaData.DisplayName)));
        splitContainer1.Panel2.ResumeLayout();
    }

    private void btnAddStation_Click(object sender, EventArgs e)
    {
        if (Settings.Default.GameBasePath.Equals(string.Empty) || Settings.Default.StagingPath.Equals(string.Empty))
            return;

        Station blankStation = new()
        {
            MetaData =
            {
                DisplayName = $"{GlobalData.Strings.GetString("NewStationListBoxEntry")} {_newStationCount}"
            }
        };

        _stations.Add(blankStation);
        _stationEditors.Add(new StationEditor(blankStation));
        lbStations.SelectedItem = blankStation;
        _newStationCount++;

        if (_stations.Count <= 0) return;

        //Re-show our station editor if the station count has increased again.
        _noStationsCtrl.Visible = false;
        lbStations_SelectedIndexChanged(this, EventArgs.Empty);
    }

    private void btnDeleteStation_Click(object sender, EventArgs e)
    {
        if (Settings.Default.GameBasePath.Equals(string.Empty) || Settings.Default.StagingPath.Equals(string.Empty))
            return;

        if (lbStations.SelectedItem is not Station station) return;

        _stations.Remove(station);
        _stationEditors.Remove(_stationEditors
            .First(s => s.Station.MetaData.DisplayName
                .Equals(station.MetaData.DisplayName)));

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

        switch (_stations.Count)
        {
            case > 0:
                return;
            case <= 0:
                _newStationCount = 1;
                break;
        }

        //Hide the station editor (and reset it) if there are no stations to edit.
        HandleUserControlVisibility();
    }

    private void cmbLanguageSelect_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_languageComboBox.SelectedItem is not ImageComboBoxItem culture) return;
        
        SuspendLayout();
        GlobalData.SetCulture(culture.Text);

        Translate();

        foreach (var se in _stationEditors)
            se.Translate();

        _noStationsCtrl.Translate();

        Focus(); //re-focus the main form

        languageToolStripMenuItem.HideDropDown();
        ResumeLayout();

        Settings.Default.SelectedLanguage = culture.Text;
        Settings.Default.Save();
    }

    private void lbStations_MouseDown(object sender, MouseEventArgs e)
    {
        var index = lbStations.IndexFromPoint(e.Location);
        if (index == ListBox.NoMatches)
            _ignoreSelectedIndexChanged = true;
        else
            _previousStationIndex = lbStations.SelectedIndex;
    }

    private void exportToGameToolStripMenuItem_Click(object sender, EventArgs e)
    {
        new ExportWindow([.. _stations]).ShowDialog();
    }

    private void pathsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        new PathSettings().ShowDialog();
    }

    private void refreshStationsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        PopulateStations();
    }

    private void radioExtHelpToolStripMenuItem_Click(object sender, EventArgs e)
    {
        "https://github.com/justarandomguyintheinternet/CP77_radioExt".OpenUrl();
    }

    private void radioExtOnNexusModsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        "https://www.nexusmods.com/cyberpunk2077/mods/4591".OpenUrl();
    }
}