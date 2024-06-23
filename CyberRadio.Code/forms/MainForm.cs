using System.ComponentModel;
using AetherUtils.Core.Extensions;
using AetherUtils.Core.Files;
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

        if (Settings.Default.WindowSize != Size.Empty)
            Size = Settings.Default.WindowSize;

        Translate();
    }

    private void MainForm_Shown(object sender, EventArgs e)
    {
        SelectLanguage();
        Translate();

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

    private void PopulateStations()
    {
        lbStations.BeginUpdate();
        lbStations.DataSource = null;

        if (!string.IsNullOrEmpty(Settings.Default.StagingPath))
        {
            _stations.Clear();
            _stationEditors.Clear();

            var validExtensions = EnumHelper.GetEnumDescriptions<ValidAudioFiles>();

            foreach (var directory in FileHelper.SafeEnumerateDirectories(Settings.Default.StagingPath))
            {
                var files = FileHelper.SafeEnumerateFiles(directory).ToList();

                var metaData = files
                    .Where(file => file.EndsWith("metadata.json"))
                    .Select(_metaDataJson.LoadJson)
                    .FirstOrDefault();

                var songList = files
                    .Where(file => file.EndsWith("songs.sgls"))
                    .Select(_songListJson.LoadJson)
                    .FirstOrDefault() ?? [];

                //Get the actual audio files in the directory if they exist.
                var songFiles = files
                    .Where(file => validExtensions.Contains(Path.GetExtension(file).ToLower()))
                    .ToList();

                if (metaData == null) continue;

                if (songList.Count == 0)
                    songFiles.ForEach(path =>
                    {
                        var song = Song.ParseFromFile(path);
                        if (song != null)
                            songList.Add(song);
                    });

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
        lbStations.DisplayMember = "MetaData";
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
    }

    private void exportToGameToolStripMenuItem_Click(object sender, EventArgs e)
    {
        new ExportWindow([.. _stations]).ShowDialog();
    }

    private void pathsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var result = new PathSettings().ShowDialog();
        if (result == DialogResult.OK)
            PopulateStations();
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
    private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
    {
        "https://ethan-hann.github.io/CyberRadio-Assistant/index.html".OpenUrl();
    }

    private void MainForm_HelpButtonClicked(object sender, CancelEventArgs e)
    {
        "https://ethan-hann.github.io/CyberRadio-Assistant/index.html".OpenUrl();
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        new AboutBox().ShowDialog();
    }

    private void MainForm_Resize(object sender, EventArgs e)
    {
        Settings.Default.WindowSize = Size;
        Settings.Default.Save();
    }
}