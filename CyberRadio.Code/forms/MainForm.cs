using System.ComponentModel;
using System.Diagnostics;
using AetherUtils.Core.Extensions;
using AetherUtils.Core.Files;
using AetherUtils.Core.WinForms.Controls;
using AetherUtils.Core.WinForms.Models;
using RadioExt_Helper.config;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.user_controls;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

public partial class MainForm : Form
{
    private readonly ImageComboBox<ImageComboBoxItem> _languageComboBox = new();
    private readonly List<ImageComboBoxItem> _languages = new();

    private readonly Json<MetaData> _metaDataJson = new();
    private readonly NoStationsCtl _noStationsCtrl = new();
    private readonly Json<SongList> _songListJson = new();

    private Dictionary<Guid, StationEditor> _stationEditorsDict = [];
    private StationEditor? _currentEditor;

    private readonly BindingList<TrackableObject<Station>> _stations = new();
    private readonly System.Timers.Timer resizeTimer;

    private readonly ImageList _stationImageList = new();

    private bool _ignoreSelectedIndexChanged;
    private int _newStationCount = 1;

    private string GameBasePath => GlobalData.ConfigManager.Get("gameBasePath") as string ?? string.Empty;
    private string StagingPath => GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;

    private string _stationCountFormat =
        GlobalData.Strings.GetString("EnabledStationsCount") ?? "Enabled Stations: {0} / {1}";

    public MainForm()
    {
        InitializeComponent();

        SetImageList();

        GlobalData.Initialize();

        InitializeLanguageDropDown();
        SelectLanguage();

        if (GlobalData.ConfigManager.Get("autoCheckForUpdates") as bool? ?? true)
            _ = Updater.CheckForUpdates();

        // Set up timer for resizing; this is needed to prevent the application from saving the window size too often.
        resizeTimer = new(500) // 500 ms delay
        {
            AutoReset = false // Ensure it only ticks once after being reset
        };

        // Save the configuration when resizing has stopped
        resizeTimer.Elapsed += (sender, args) => { SaveWindowSize(); };

        lbStations.DataSource = _stations;
        lbStations.DisplayMember = "TrackedObject.MetaData";
    }

    private void SetImageList()
    {
        _stationImageList.Images.Add("disabled", Resources.disabled);
        _stationImageList.Images.Add("enabled", Resources.enabled);
        _stationImageList.Images.Add("edited_station", Resources.save_pending);
        _stationImageList.Images.Add("saved_station", Resources.disk);
        _stationImageList.ImageSize = new Size(16, 16);
        lbStations.ImageList = _stationImageList;
    }

    private void SaveWindowSize()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(SaveWindowSize));
        }
        else
        {
            GlobalData.ConfigManager.Set("windowSize", new WindowSize(Size.Width, Size.Height));
            GlobalData.ConfigManager.SaveAsync();
        }
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        _noStationsCtrl.PathsSet += RefreshAfterPathsChanged;
        splitContainer1.Panel2.Controls.Add(_noStationsCtrl);

        var windowSize = GlobalData.ConfigManager.Get("windowSize") as WindowSize
                         ?? new WindowSize(0, 0);

        if (!windowSize.IsEmpty())
            Size = new Size(windowSize.Width, windowSize.Height);

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
        // Populate the language combo box
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
        var language = GlobalData.ConfigManager.Get("language") as string ?? string.Empty;
        if (!language.Equals(string.Empty))
            _languageComboBox.SelectedIndex = _languageComboBox.Items.IndexOf(
                _languages.Find(l => l.Text.Equals(language)));
        else
            _languageComboBox.SelectedIndex = 0;

        cmbLanguageSelect_SelectedIndexChanged(_languageComboBox, EventArgs.Empty);
    }

    private void Translate()
    {
        Text = GlobalData.Strings.GetString("MainTitle");
        fileToolStripMenuItem.Text = GlobalData.Strings.GetString("File");
        openStagingPathToolStripMenuItem.Text = GlobalData.Strings.GetString("OpenStagingFolder");
        openGamePathToolStripMenuItem.Text = GlobalData.Strings.GetString("OpenGameFolder");
        openLogFolderToolStripMenuItem.Text = GlobalData.Strings.GetString("OpenLogFolder");
        exportToGameToolStripMenuItem.Text = GlobalData.Strings.GetString("ExportStations");
        languageToolStripMenuItem.Text = GlobalData.Strings.GetString("Language");
        helpToolStripMenuItem.Text = GlobalData.Strings.GetString("Help");
        configurationToolStripMenuItem.Text = GlobalData.Strings.GetString("Configuration");
        pathsToolStripMenuItem.Text = GlobalData.Strings.GetString("GamePaths");
        refreshStationsToolStripMenuItem.Text = GlobalData.Strings.GetString("RefreshStations");
        howToUseToolStripMenuItem.Text = GlobalData.Strings.GetString("HowToUse");
        radioExtGitHubToolStripMenuItem.Text = GlobalData.Strings.GetString("RadioExtGithub");
        radioExtOnNexusModsToolStripMenuItem.Text = GlobalData.Strings.GetString("RadioExtNexusMods");
        aboutToolStripMenuItem.Text = GlobalData.Strings.GetString("About");
        checkForUpdatesToolStripMenuItem.Text = GlobalData.Strings.GetString("CheckForUpdates");

        _stationCountFormat = GlobalData.Strings.GetString("EnabledStationsCount") ?? "Enabled Stations: {0} / {1}";
        grpStations.Text = GlobalData.Strings.GetString("Stations");

        // Buttons
        btnAddStation.Text = GlobalData.Strings.GetString("NewStation");
        btnDeleteStation.Text = GlobalData.Strings.GetString("DeleteStation");
        btnEnableSelected.Text = GlobalData.Strings.GetString("EnableSelectedStation");
        btnEnableAll.Text = GlobalData.Strings.GetString("EnableAllStations");
        btnDisableSelected.Text = GlobalData.Strings.GetString("DisableSelectedStation");
        btnDisableAll.Text = GlobalData.Strings.GetString("DisableAllStations");

        UpdateEnabledStationCount();
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
        this.SafeInvoke(() =>
        {
            lbStations.BeginUpdate();

            if (!string.IsNullOrEmpty(StagingPath))
            {
                InitializeData();
                LoadStationsFromDirectories();
            }

            UpdateUIAfterPopulation();

            lbStations.EndUpdate();
        });
    }

    private void InitializeData()
    {
        _stations.Clear();
        _stationEditorsDict.Clear();
    }

    private void LoadStationsFromDirectories()
    {
        var validExtensions = EnumHelper.GetEnumDescriptions<ValidAudioFiles>();

        foreach (var directory in FileHelper.SafeEnumerateDirectories(StagingPath))
        {
            ProcessDirectory(directory, validExtensions);
        }
    }

    private void ProcessDirectory(string directory, IEnumerable<string?> validExtensions)
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

        var songFiles = files
            .Where(file => validExtensions.Contains(Path.GetExtension(file).ToLower()))
            .ToList();

        if (metaData != null)
        {
            if (songList.Count == 0)
            {
                songFiles.ForEach(path =>
                {
                    var song = Song.ParseFromFile(path);
                    if (song != null)
                        songList.Add(song);
                });
            }

            var station = CreateStation(metaData, songList);
            var trackedStation = new TrackableObject<Station>(station);
            _stations.Add(trackedStation);
            AddEditor(trackedStation);
        }
    }

    private void AddEditor(TrackableObject<Station> station)
    {
        var editor = new StationEditor(station);
        editor.StationUpdated += StationUpdatedEvent;
        lock (_stationEditorsDict)
        {
            _stationEditorsDict[station.Id] = editor;
        }
    }

    private void RemoveEditor(TrackableObject<Station> station) 
    {
        if (_stationEditorsDict.TryGetValue(station.Id, out var editor))
        {
            editor.StationUpdated -= StationUpdatedEvent;
            _stationEditorsDict.Remove(station.Id);
        }
    }

    private Station CreateStation(MetaData metaData, SongList songList)
    {
        return new Station
        {
            MetaData = metaData,
            Songs = songList
        };
    }

    private void UpdateUIAfterPopulation()
    {
        if (lbStations.Items.Count > 0)
        {
            lbStations.SelectedIndex = 0;
            lbStations_SelectedIndexChanged(lbStations, EventArgs.Empty);
        }

        HandleUserControlVisibility();
        UpdateEnabledStationCount();
    }

    private void RefreshAfterPathsChanged(object? sender, EventArgs e)
    {
        PopulateStations();
        if (_stations.Count <= 0) return;
    }

    private void UpdateEnabledStationCount()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(UpdateEnabledStationCount));
        }
        else
        {
            var enabledCount = _stations.Count(s => s.TrackedObject.MetaData.IsActive);
            lblStationCount.Text = string.Format(_stationCountFormat, enabledCount, _stations.Count);
        }
    }

    private void lbStations_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_ignoreSelectedIndexChanged)
        {
            _ignoreSelectedIndexChanged = false;
            return;
        }

        if (lbStations.SelectedItem is not TrackableObject<Station> station) return;

        // Stop music players
        foreach (var sEditor in _stationEditorsDict.Values)
        {
            sEditor.GetMusicPlayer().StopStream();
        }

        // Get the editor from the dictionary
        if (_stationEditorsDict.TryGetValue(station.Id, out var editor))
        {
            UpdateStationEditor(editor);
        }
    }

    private void UpdateStationEditor(StationEditor editor)
    {
        if (_currentEditor == editor) return;

        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.Panel2.Controls.Clear();
        splitContainer1.Panel2.Controls.Add(editor);
        splitContainer1.Panel2.ResumeLayout();

        _currentEditor = editor;
    }

    private void btnEnableStation_Click(object sender, EventArgs e)
    {
        if (lbStations.SelectedItem is not TrackableObject<Station> s) return;

        s.TrackedObject.MetaData.IsActive = true;
        s.CheckPendingSaveStatus();

        lbStations.BeginUpdate();
        lbStations.Invalidate();
        lbStations.EndUpdate();
        UpdateEnabledStationCount();
    }

    private void btnEnableAll_Click(object sender, EventArgs e)
    {
        lbStations.BeginUpdate();
        foreach (var station in lbStations.Items)
        {
            if (station is TrackableObject<Station> s)
            {
                s.TrackedObject.MetaData.IsActive = true;
                s.CheckPendingSaveStatus();
            }
        }

        lbStations.Invalidate();
        lbStations.EndUpdate();
        UpdateEnabledStationCount();
    }

    private void btnDisableStation_Click(object sender, EventArgs e)
    {
        if (lbStations.SelectedItem is not TrackableObject<Station> s) return;

        s.TrackedObject.MetaData.IsActive = false;
        s.CheckPendingSaveStatus();

        lbStations.BeginUpdate();
        lbStations.Invalidate();
        lbStations.EndUpdate();
        UpdateEnabledStationCount();
    }

    private void btnDisableAll_Click(object sender, EventArgs e)
    {
        lbStations.BeginUpdate();
        foreach (var station in lbStations.Items)
        {
            if (station is TrackableObject<Station> s)
            {
                s.TrackedObject.MetaData.IsActive = false;
                s.CheckPendingSaveStatus();
            }
        }

        lbStations.Invalidate();
        lbStations.EndUpdate();
        UpdateEnabledStationCount();
    }

    private void btnAddStation_Click(object sender, EventArgs e)
    {
        if (GameBasePath.Equals(string.Empty) || StagingPath.Equals(string.Empty))
            return;

        Station blankStation = new()
        {
            MetaData =
            {
                DisplayName = $"{GlobalData.Strings.GetString("NewStationListBoxEntry")} {_newStationCount}"
            },
        };

        var trackedStation = new TrackableObject<Station>(blankStation);
        _stations.Add(trackedStation);
        AddEditor(trackedStation);

        lbStations.SelectedItem = trackedStation;
        _newStationCount++;

        if (_stations.Count <= 0) return;

        // Re-show our station editor if the station count has increased again.
        _noStationsCtrl.Visible = false;
        lbStations_SelectedIndexChanged(this, EventArgs.Empty);

        UpdateEnabledStationCount();
    }

    private void StationUpdatedEvent(object? sender, EventArgs e)
    {
        if (lbStations.SelectedItem is TrackableObject<Station> station)
        {
            station.CheckPendingSaveStatus();
            lbStations.BeginUpdate();
            lbStations.Invalidate();
            lbStations.EndUpdate();
        }
    }

    private void btnDeleteStation_Click(object sender, EventArgs e)
    {
        if (GameBasePath.Equals(string.Empty) || StagingPath.Equals(string.Empty))
            return;

        if (lbStations.SelectedItem is not TrackableObject<Station> station) return;

        string newStationPrefix = GlobalData.Strings.GetString("NewStationListBoxEntry") ?? "[New Station]";

        // If the station to be removed contains "[New Station]" in the name, decrement our new station count.
        if (station.TrackedObject.MetaData.DisplayName.Contains(newStationPrefix))
            _newStationCount--;

        // Reset new station count if there are no more "New stations" in the list box.
        if (!_stations.Select(s => s.TrackedObject.MetaData.DisplayName.Contains(newStationPrefix)).Contains(true))
            _newStationCount = 1;

        var stationToRemove = _stations.First(s => s.Id == station.Id);
        _stations.Remove(stationToRemove);

        RemoveEditor(stationToRemove);

        UpdateEnabledStationCount();

        if (_stations.Count <= 0)
        {
            _newStationCount = 1;
            HandleUserControlVisibility();
        }
    }

    private void cmbLanguageSelect_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_languageComboBox.SelectedItem is not ImageComboBoxItem culture) return;

        SuspendLayout();
        GlobalData.SetCulture(culture.Text);

        Translate();

        foreach (var se in _stationEditorsDict.Values)
            se.Translate();

        _noStationsCtrl.Translate();

        Focus(); // re-focus the main form

        languageToolStripMenuItem.HideDropDown();
        ResumeLayout();

        GlobalData.ConfigManager.Set("language", culture.Text);
        GlobalData.ConfigManager.Save();
    }

    private void lbStations_MouseDown(object sender, MouseEventArgs e)
    {
        var index = lbStations.IndexFromPoint(e.Location);
        if (index == ListBox.NoMatches)
            _ignoreSelectedIndexChanged = true;
    }

    private void openStagingPathToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Process.Start("explorer.exe", StagingPath);
    }

    private void openGamePathToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!ExportWindow.ShowNoModDialogIfRequired()) return;

        Process.Start("explorer.exe", PathHelper.GetRadiosPath(GameBasePath));
    }

    private void openLogFolderToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Process.Start("explorer.exe", GlobalData.GetLogPath());
    }

    private void exportToGameToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var exportWindow = new ExportWindow([.. _stations]);
        exportWindow.OnExportToStagingComplete += (sender, args) => { PopulateStations(); };
        exportWindow.ShowDialog(this);
    }

    private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
    {
        new ConfigForm().ShowDialog(this);
    }

    private void pathsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var result = new PathSettings().ShowDialog(this);
        if (result == DialogResult.OK)
            PopulateStations();
    }

    private void refreshStationsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var text = GlobalData.Strings.GetString("ConfirmRefreshStations");
        var caption = GlobalData.Strings.GetString("Confirm");
        if (MessageBox.Show(this, text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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

    private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _ = Updater.CheckForUpdates();
    }

    private void MainForm_Resize(object sender, EventArgs e)
    {
        // Restart the Timer each time the Resize event is triggered
        resizeTimer.Stop();
        resizeTimer.Start();
    }
}
