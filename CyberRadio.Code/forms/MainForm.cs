// MainForm.cs : RadioExt-Helper
// Copyright (C) 2024  Ethan Hann
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

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
using Timer = System.Timers.Timer;

namespace RadioExt_Helper.forms;

/// <summary>
///     Represents the main form of the RadioExt-Helper application.
/// </summary>
public partial class MainForm : Form
{
    private readonly ImageComboBox<ImageComboBoxItem> _languageComboBox = new();
    private readonly List<ImageComboBoxItem> _languages = [];
    private readonly Json<MetaData> _metaDataJson = new();
    private readonly NoStationsCtl _noStationsCtrl = new();
    private readonly Timer _resizeTimer;
    private readonly Json<SongList> _songListJson = new();
    private readonly Dictionary<Guid, StationEditor> _stationEditorsDict = [];
    private readonly ImageList _stationImageList = new();
    private readonly BindingList<TrackableObject<Station>> _stations = [];

    private StationEditor? _currentEditor;
    private bool _ignoreSelectedIndexChanged;
    private int _newStationCount = 1;

    private string _stationCountFormat =
        GlobalData.Strings.GetString("EnabledStationsCount") ?? "Enabled Stations: {0} / {1}";

    /// <summary>
    ///     Initializes a new instance of the <see cref="MainForm" /> class.
    /// </summary>
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
        _resizeTimer = new Timer(500) // 500 ms delay
        {
            AutoReset = false // Ensure it only ticks once after being reset
        };

        // Save the configuration when resizing has stopped
        _resizeTimer.Elapsed += (_, _) => { SaveWindowSize(); };

        lbStations.DataSource = _stations;
        lbStations.DisplayMember = "TrackedObject.MetaData";
    }

    /// <summary>
    ///     Gets the base path of the game from the configuration.
    /// </summary>
    private static string GameBasePath => GlobalData.ConfigManager.Get("gameBasePath") as string ?? string.Empty;

    /// <summary>
    ///     Gets the staging path from the configuration.
    /// </summary>
    private static string StagingPath => GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;

    /// <summary>
    ///     Sets up the image list for the station list.
    /// </summary>
    private void SetImageList()
    {
        _stationImageList.Images.Add("disabled", Resources.disabled);
        _stationImageList.Images.Add("enabled", Resources.enabled);
        _stationImageList.Images.Add("edited_station", Resources.save_pending);
        _stationImageList.Images.Add("saved_station", Resources.disk);
        _stationImageList.ImageSize = new Size(16, 16);
        lbStations.ImageList = _stationImageList;
    }

    /// <summary>
    ///     Saves the window size to the configuration.
    /// </summary>
    private void SaveWindowSize()
    {
        this.SafeInvoke(() =>
        {
            GlobalData.ConfigManager.Set("windowSize", new WindowSize(Size.Width, Size.Height));
            GlobalData.ConfigManager.SaveAsync();
        });
    }

    /// <summary>
    ///     Handles the Load event of the MainForm.
    /// </summary>
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

    /// <summary>
    ///     Handles the Shown event of the MainForm.
    /// </summary>
    private void MainForm_Shown(object sender, EventArgs e)
    {
        SelectLanguage();
        Translate();

        PopulateStations();
        HandleUserControlVisibility();
    }

    /// <summary>
    ///     Initializes the language drop-down with supported languages and images.
    /// </summary>
    private void InitializeLanguageDropDown()
    {
        // Populate the language combo box
        _languages.Add(new ImageComboBoxItem("English (en)", Resources.united_kingdom));
        _languages.Add(new ImageComboBoxItem("Español (es)", Resources.spain));
        _languages.Add(new ImageComboBoxItem("Français (fr)", Resources.france));

        foreach (var language in _languages)
            _languageComboBox.Items.Add(language);

        _languageComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _languageComboBox.SelectedIndexChanged += CmbLanguageSelect_SelectedIndexChanged;

        // Create a ToolStripControlHost to host the ImageComboBox
        ToolStripControlHost toolStripControlHost = new(_languageComboBox);

        // Add the ToolStripControlHost to the "Language" tool strip menu
        languageToolStripMenuItem.DropDownItems.Add(toolStripControlHost);
    }

    /// <summary>
    ///     Selects the language based on the configuration.
    /// </summary>
    private void SelectLanguage()
    {
        var language = GlobalData.ConfigManager.Get("language") as string ?? string.Empty;
        if (!language.Equals(string.Empty))
            _languageComboBox.SelectedIndex = _languageComboBox.Items.IndexOf(
                _languages.Find(l => l.Text.Equals(language)));
        else
            _languageComboBox.SelectedIndex = 0;

        CmbLanguageSelect_SelectedIndexChanged(_languageComboBox, EventArgs.Empty);
    }

    /// <summary>
    ///     Translates the UI elements based on the current culture.
    /// </summary>
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
        revertChangesToolStripMenuItem.Text = GlobalData.Strings.GetString("RevertChanges");

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

    /// <summary>
    ///     Handles the visibility of the user controls when there are no stations.
    /// </summary>
    private void HandleUserControlVisibility()
    {
        if (_stations.Count > 0) return;

        splitContainer1.Panel2.Controls.Clear();
        splitContainer1.Panel2.Controls.Add(_noStationsCtrl);
        _noStationsCtrl.Visible = true;
    }

    /// <summary>
    ///     Populates the stations list box, stations list, and station editors.
    /// </summary>
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

            UpdateUiAfterPopulation();

            lbStations.EndUpdate();
        });
    }

    /// <summary>
    ///     Initializes the data lists by clearing them.
    /// </summary>
    private void InitializeData()
    {
        _stations.Clear();
        _stationEditorsDict.Clear();
    }

    /// <summary>
    ///     Loads the stations from the staging directory.
    /// </summary>
    private void LoadStationsFromDirectories()
    {
        var validExtensions = EnumHelper<ValidAudioFiles>.GetEnumDescriptions();
        var extensions = validExtensions as string[] ?? validExtensions.ToArray();

        foreach (var directory in FileHelper.SafeEnumerateDirectories(StagingPath))
            ProcessDirectory(directory, extensions);
    }

    /// <summary>
    ///     Processes a directory and loads the station data.
    /// </summary>
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

        if (metaData == null) return;

        if (songList.Count == 0)
            songFiles.ForEach(path =>
            {
                var song = Song.FromFile(path);
                if (song != null)
                    songList.Add(song);
            });

        var station = CreateStation(metaData, songList);
        var trackedStation = new TrackableObject<Station>(station);
        _stations.Add(trackedStation);
        AddEditor(trackedStation);
    }

    /// <summary>
    ///     Adds a station editor to the dictionary.
    /// </summary>
    private void AddEditor(TrackableObject<Station> station)
    {
        lock (_stationEditorsDict)
        {
            var editor = new StationEditor(station);
            editor.StationUpdated += StationUpdatedEvent;
            _stationEditorsDict[station.Id] = editor;
        }
    }

    /// <summary>
    ///     Removes a station editor from the dictionary.
    /// </summary>
    private void RemoveEditor(TrackableObject<Station> station)
    {
        lock (_stationEditorsDict)
        {
            if (!_stationEditorsDict.TryGetValue(station.Id, out var editor)) return;
            editor.StationUpdated -= StationUpdatedEvent;
            _stationEditorsDict.Remove(station.Id);
        }
    }

    /// <summary>
    ///     Creates a station object from the metadata and song list.
    ///     <param name="metaData">The metadata for the station.</param>
    ///     <param name="songList">The song list for the station.</param>
    /// </summary>
    private static Station CreateStation(MetaData metaData, SongList songList)
    {
        return new Station
        {
            MetaData = metaData,
            Songs = songList
        };
    }

    /// <summary>
    ///     Updates the UI after populating the stations.
    /// </summary>
    private void UpdateUiAfterPopulation()
    {
        if (lbStations.Items.Count > 0)
        {
            lbStations.SelectedIndex = 0;
            LbStations_SelectedIndexChanged(lbStations, EventArgs.Empty);
        }

        HandleUserControlVisibility();
        UpdateEnabledStationCount();
    }

    /// <summary>
    ///     Refreshes the UI after the paths have changed from the <see cref="PathSettings" /> or the <see cref="ConfigForm" />
    ///     .
    ///     <param name="sender">The event sender.</param>
    ///     <param name="e">The event arguments.</param>
    /// </summary>
    private void RefreshAfterPathsChanged(object? sender, EventArgs e)
    {
        PopulateStations();
    }

    /// <summary>
    ///     Updates the enabled station count label.
    /// </summary>
    private void UpdateEnabledStationCount()
    {
        this.SafeInvoke(() =>
        {
            var enabledCount = _stations.Count(s => s.TrackedObject.MetaData.IsActive);
            lblStationCount.Text = string.Format(_stationCountFormat, enabledCount, _stations.Count);
        });
    }

    /// <summary>
    ///     Handles the SelectedIndexChanged event of the lbStations list box.
    ///     <param name="sender">The event sender.</param>
    ///     <param name="e">The event arguments.</param>
    /// </summary>
    private void LbStations_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_ignoreSelectedIndexChanged)
        {
            _ignoreSelectedIndexChanged = false;
            return;
        }

        if (lbStations.SelectedItem is not TrackableObject<Station> station) return;

        // Stop music players
        foreach (var sEditor in _stationEditorsDict.Values) sEditor.GetMusicPlayer().StopStream();

        // Get the editor from the dictionary
        if (_stationEditorsDict.TryGetValue(station.Id, out var editor)) UpdateStationEditor(editor);
    }

    /// <summary>
    ///     Updates the currently displayed station editor.
    ///     <param name="editor">The editor to display in the split panel.</param>
    /// </summary>
    private void UpdateStationEditor(StationEditor editor)
    {
        if (_currentEditor == editor) return;

        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.Panel2.Controls.Clear();
        splitContainer1.Panel2.Controls.Add(editor);
        splitContainer1.Panel2.ResumeLayout();

        _currentEditor = editor;
    }

    private void RevertChangesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lbStations.SelectedItem is not TrackableObject<Station> station) return;

        //We only want to revert the changes if there is a pending save. Otherwise, the wrong icon is drawn in the list box.
        if (!station.IsPendingSave) return;

        station.DeclineChanges(); // Revert the changes made to the station's properties since the last save.
        StationUpdatedEvent(sender, e); //Update the UI to reflect the changes.
    }

    /// <summary>
    ///     Handles the Click event of the btnEnableStation button.
    ///     <param name="sender">The event sender.</param>
    ///     <param name="e">The event arguments.</param>
    /// </summary>
    private void BtnEnableStation_Click(object sender, EventArgs e)
    {
        if (lbStations.SelectedItem is not TrackableObject<Station> s) return;

        s.TrackedObject.MetaData.IsActive = true;
        s.CheckPendingSaveStatus();

        lbStations.BeginUpdate();
        lbStations.Invalidate();
        lbStations.EndUpdate();
        UpdateEnabledStationCount();
    }

    /// <summary>
    ///     Handles the Click event of the btnEnableAll button.
    ///     <param name="sender">The event sender.</param>
    ///     <param name="e">The event arguments.</param>
    /// </summary>
    private void BtnEnableAll_Click(object sender, EventArgs e)
    {
        lbStations.BeginUpdate();
        foreach (var station in lbStations.Items)
            if (station is TrackableObject<Station> s)
            {
                s.TrackedObject.MetaData.IsActive = true;
                s.CheckPendingSaveStatus();
            }

        lbStations.Invalidate();
        lbStations.EndUpdate();
        UpdateEnabledStationCount();
    }

    /// <summary>
    ///     Handles the Click event of the btnDisableStation button.
    ///     <param name="sender">The event sender.</param>
    ///     <param name="e">The event arguments.</param>
    /// </summary>
    private void BtnDisableStation_Click(object sender, EventArgs e)
    {
        if (lbStations.SelectedItem is not TrackableObject<Station> s) return;

        s.TrackedObject.MetaData.IsActive = false;
        s.CheckPendingSaveStatus();

        lbStations.BeginUpdate();
        lbStations.Invalidate();
        lbStations.EndUpdate();
        UpdateEnabledStationCount();
    }

    /// <summary>
    ///     Handles the Click event of the btnDisableAll button.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void BtnDisableAll_Click(object sender, EventArgs e)
    {
        lbStations.BeginUpdate();
        foreach (var station in lbStations.Items)
            if (station is TrackableObject<Station> s)
            {
                s.TrackedObject.MetaData.IsActive = false;
                s.CheckPendingSaveStatus();
            }

        lbStations.Invalidate();
        lbStations.EndUpdate();
        UpdateEnabledStationCount();
    }

    /// <summary>
    ///     Handles the Click event of the btnDisableAll button.
    ///     <param name="sender">The event sender.</param>
    ///     <param name="e">The event arguments.</param>
    /// </summary>
    private void BtnAddStation_Click(object sender, EventArgs e)
    {
        if (GameBasePath.Equals(string.Empty) || StagingPath.Equals(string.Empty))
            return;

        Station blankStation = new()
        {
            MetaData =
            {
                DisplayName = $"{GlobalData.Strings.GetString("NewStationListBoxEntry")} {_newStationCount}"
            }
        };

        var trackedStation = new TrackableObject<Station>(blankStation);
        _stations.Add(trackedStation);
        AddEditor(trackedStation);

        lbStations.SelectedItem = trackedStation;
        _newStationCount++;

        if (_stations.Count <= 0) return;

        // Re-show our station editor if the station count has increased again.
        _noStationsCtrl.Visible = false;
        LbStations_SelectedIndexChanged(this, EventArgs.Empty);

        UpdateEnabledStationCount();
    }

    /// <summary>
    ///     Handles the station updated event. Checks for pending save status on a station and updates the list box.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void StationUpdatedEvent(object? sender, EventArgs e)
    {
        if (lbStations.SelectedItem is not TrackableObject<Station> station) return;

        station.CheckPendingSaveStatus();
        lbStations.BeginUpdate();
        lbStations.Invalidate();
        lbStations.EndUpdate();
    }


    private void BtnDeleteStation_Click(object sender, EventArgs e)
    {
        if (GameBasePath.Equals(string.Empty) || StagingPath.Equals(string.Empty))
            return;

        if (lbStations.SelectedItem is not TrackableObject<Station> station) return;

        var newStationPrefix = GlobalData.Strings.GetString("NewStationListBoxEntry") ?? "[New Station]";

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

        if (_stations.Count > 0) return;

        _newStationCount = 1;
        HandleUserControlVisibility();
    }

    private void CmbLanguageSelect_SelectedIndexChanged(object? sender, EventArgs e)
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

    private void LbStations_MouseDown(object sender, MouseEventArgs e)
    {
        var index = lbStations.IndexFromPoint(e.Location);
        if (index == ListBox.NoMatches)
            _ignoreSelectedIndexChanged = true;

        if (e.Button != MouseButtons.Right) return;

        //Show the "Revert Changes" context menu if the station is selected and was right-clicked.
        if (lbStations.SelectedIndex == index)
            cmsRevertStationChanges.Show(Cursor.Position);
    }

    private void OpenStagingPathToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Process.Start("explorer.exe", StagingPath);
    }

    private void OpenGamePathToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!ExportWindow.ShowNoModDialogIfRequired()) return;

        Process.Start("explorer.exe", PathHelper.GetRadiosPath(GameBasePath));
    }

    private void OpenLogFolderToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Process.Start("explorer.exe", GlobalData.GetLogPath());
    }

    private void ExportToGameToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var exportWindow = new ExportWindow([.. _stations]);
        exportWindow.OnExportToStagingComplete += (_, _) => { PopulateStations(); };
        exportWindow.ShowDialog(this);
    }

    private void ConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
    {
        new ConfigForm().ShowDialog(this);
    }

    private void PathsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var result = new PathSettings().ShowDialog(this);
        if (result == DialogResult.OK)
            PopulateStations();
    }

    private void RefreshStationsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var text = GlobalData.Strings.GetString("ConfirmRefreshStations");
        var caption = GlobalData.Strings.GetString("Confirm");
        if (MessageBox.Show(this, text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            PopulateStations();
    }

    private void RadioExtHelpToolStripMenuItem_Click(object sender, EventArgs e)
    {
        "https://github.com/justarandomguyintheinternet/CP77_radioExt".OpenUrl();
    }

    private void RadioExtOnNexusModsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        "https://www.nexusmods.com/cyberpunk2077/mods/4591".OpenUrl();
    }

    private void HowToUseToolStripMenuItem_Click(object sender, EventArgs e)
    {
        "https://ethan-hann.github.io/CyberRadio-Assistant/index.html".OpenUrl();
    }

    private void MainForm_HelpButtonClicked(object sender, CancelEventArgs e)
    {
        "https://ethan-hann.github.io/CyberRadio-Assistant/index.html".OpenUrl();
    }

    private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        new AboutBox().ShowDialog();
    }

    private void CheckForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _ = Updater.CheckForUpdates();
    }

    private void MainForm_Resize(object sender, EventArgs e)
    {
        // Restart the Timer each time the Resize event is triggered
        _resizeTimer.Stop();
        _resizeTimer.Start();
    }
}