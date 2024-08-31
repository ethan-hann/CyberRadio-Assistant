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
using System.Timers;
using AetherUtils.Core.Extensions;
using AetherUtils.Core.Logging;
using AetherUtils.Core.WinForms.Controls;
using AetherUtils.Core.WinForms.Models;
using RadioExt_Helper.config;
using RadioExt_Helper.models;
using RadioExt_Helper.nexus_api;
using RadioExt_Helper.Properties;
using RadioExt_Helper.user_controls;
using RadioExt_Helper.utility;
using Timer = System.Timers.Timer;

namespace RadioExt_Helper.forms;

/// <summary>
///     Represents the main form of the RadioExt-Helper application.
/// </summary>
public sealed partial class MainForm : Form
{
    private readonly ImageComboBox<ImageComboBoxItem> _languageComboBox = new();
    private readonly List<ImageComboBoxItem> _languages = [];
    private readonly NoStationsCtl _noStationsCtrl = new();
    private readonly Timer _resizeTimer;
    private readonly ImageList _stationImageList = new();

    private StationEditor? _currentEditor;
    private DirectoryWatcher? _directoryWatcher;
    private bool _ignoreSelectedIndexChanged;

    private bool _isAppClosing;
    private bool _isExportInProgress;
    private bool _isSyncInProgress;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MainForm" /> class.
    /// </summary>
    public MainForm()
    {
        InitializeComponent();

        GlobalData.InitializeComboBoxTemplate();

        // Set up timer for resizing; this is needed to prevent the application from saving the window size too often.
        _resizeTimer = new Timer(500) // 500 ms delay
        {
            AutoReset = false // Ensure it only ticks once after being reset
        };

        SetImageList();
        InitializeLanguageDropDown();
        InitializeEvents();

        SelectLanguage();

        lbStations.DataSource = StationManager.Instance.StationsAsBindingList;
        lbStations.DisplayMember = "TrackedObject.MetaData";

        //Add the icons folder to the protected folders list
        StationManager.Instance.AddProtectedFolder(Path.Combine(StagingPath, "icons"));

        SetupDirectoryWatcher();
    }

    /// <summary>
    ///     Gets the base path of the game from the configuration.
    /// </summary>
    private static string GameBasePath => GlobalData.ConfigManager.Get("gameBasePath") as string ?? string.Empty;

    /// <summary>
    ///     Gets the staging path from the configuration.
    /// </summary>
    private static string StagingPath => GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;

    private void InitializeEvents()
    {
        _languageComboBox.SelectedIndexChanged += CmbLanguageSelect_SelectedIndexChanged;

        StationManager.Instance.StationUpdated += OnStationUpdated;
        StationManager.Instance.SyncProgressChanged += OnStationSyncProgressChanged;
        StationManager.Instance.SyncStatusChanged += OnStationSyncStatusChanged;
        StationManager.Instance.StationsSynchronized += OnStationsSynchronized;

        lbStations.StationImported += LbStations_StationImported;

        // Save the configuration when resizing has stopped
        _resizeTimer.Elapsed += (_, _) => { SaveWindowSize(); };
    }

    private void LbStations_StationImported(object? sender, TrackableObject<Station> e)
    {
        MessageBox.Show(this, "Station imported: " + e.TrackedObject.MetaData.DisplayName);
        //// Handle custom icon
        //SaveCustomIcon(e.IconFilePath, e.IconFileName);
        //AuLogger.GetCurrentLogger<MainForm>("StationImported")
        //    .Info($"Station imported: {e.Station.TrackedObject.MetaData.DisplayName}");
    }

    //private void SaveCustomIcon(string? iconFilePath, string? iconFileName)
    //{
    //    if (string.IsNullOrEmpty(iconFilePath) || string.IsNullOrEmpty(iconFileName)) return;

    //    var archivePath = Path.Combine(StagingPath, "archive", iconFileName);
    //    FileHelper.CreateDirectories(Path.GetDirectoryName(archivePath));
    //    File.Copy(iconFilePath, archivePath, true);
    //}

    /// <summary>
    /// Set the flag indicating whether the application is currently performing an export operation.
    /// </summary>
    /// <param name="isInProgress"></param>
    public void SetExportInProgress(bool isInProgress)
    {
        _isExportInProgress = isInProgress;
    }

    private void OnDirectoryWatcherError(object? sender, Exception e)
    {
        if (_isExportInProgress) return;

        AuLogger.GetCurrentLogger<MainForm>("DirectoryWatcher")
            .Error(e, $"An error occurred while watching for changes in {GameBasePath}");
    }

    private void OnDirectoryWatcherFileCreated(object? sender, string path)
    {
        if (_isExportInProgress) return;

        this.SafeInvoke(() =>
        {
            _ = StationManager.Instance.SynchronizeStationsAsync(StagingPath, GameBasePath);
            AuLogger.GetCurrentLogger<MainForm>("DirectoryWatcher")
                .Info($"File created: {path}");
        });
    }

    private void OnDirectoryWatcherFileChanged(object? sender, string path)
    {
        if (_isExportInProgress) return;

        this.SafeInvoke(() =>
        {
            _ = StationManager.Instance.SynchronizeStationsAsync(StagingPath, GameBasePath);
            AuLogger.GetCurrentLogger<MainForm>("DirectoryWatcher")
                .Info($"File changed: {path}");
        });
    }

    private void OnDirectoryWatcherFileDeleted(object? sender, string path)
    {
        if (_isExportInProgress) return;

        this.SafeInvoke(() =>
        {
            AuLogger.GetCurrentLogger<MainForm>("DirectoryWatcher")
                .Info($"File deleted: {path}");
        });
    }

    private void OnDirectoryWatcherFileRenamed(object? sender, (string OldPath, string NewPath) e)
    {
        if (_isExportInProgress) return;

        this.SafeInvoke(() =>
        {
            AuLogger.GetCurrentLogger<MainForm>("DirectoryWatcher")
                .Info($"File renamed from {e.OldPath} to {e.NewPath}");
        });
    }

    private void CleanupEvents()
    {
        _languageComboBox.SelectedIndexChanged -= CmbLanguageSelect_SelectedIndexChanged;

        //_backupManager.ProgressChanged -= OnBackupManagerProgressChanged;
        //_backupManager.StatusChanged -= OnBackupManagerStatusChanged;
        //_backupManager.BackupCompleted -= OnBackupManagerBackupCompleted;

        //StationManager.Instance.StationNameDuplicate -= OnStationNameDuplicateEvent;
        StationManager.Instance.StationUpdated -= OnStationUpdated;
        StationManager.Instance.SyncProgressChanged -= OnStationSyncProgressChanged;
        StationManager.Instance.SyncStatusChanged -= OnStationSyncStatusChanged;
        StationManager.Instance.StationsSynchronized -= OnStationsSynchronized;

        _resizeTimer.Elapsed -= resizeTimerOnElapsed;
    }

    private void resizeTimerOnElapsed(object? o, ElapsedEventArgs elapsedEventArgs)
    {
        SaveWindowSize();
    }

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
    /// Set's up and starts (or stops) the directory watcher based on the configuration.
    /// The directory watcher is used to watch for changes in the game's radios directory.
    /// </summary>
    private void SetupDirectoryWatcher()
    {
        if (_directoryWatcher != null)
        {
            _directoryWatcher.FileCreated -= OnDirectoryWatcherFileCreated;
            _directoryWatcher.FileChanged -= OnDirectoryWatcherFileChanged;
            _directoryWatcher.FileRenamed -= OnDirectoryWatcherFileRenamed;
            _directoryWatcher.FileDeleted -= OnDirectoryWatcherFileDeleted;
            _directoryWatcher.Error -= OnDirectoryWatcherError;
        }

        _directoryWatcher?.Stop();
        _directoryWatcher = null;

        if (GlobalData.ConfigManager.Get("watchForGameChanges") as bool? != true) return;

        _directoryWatcher = new DirectoryWatcher(PathHelper.GetRadiosPath(GameBasePath), TimeSpan.FromSeconds(5));
        _directoryWatcher.FileCreated += OnDirectoryWatcherFileCreated;
        _directoryWatcher.FileChanged += OnDirectoryWatcherFileChanged;
        _directoryWatcher.FileRenamed += OnDirectoryWatcherFileRenamed;
        _directoryWatcher.FileDeleted += OnDirectoryWatcherFileDeleted;
        _directoryWatcher.Error += OnDirectoryWatcherError;

        _directoryWatcher.Start();
    }

    /// <summary>
    ///     Handles the Load event of the MainForm.
    /// </summary>
    private void MainForm_Load(object sender, EventArgs e)
    {
        _noStationsCtrl.PathsSet += OnPathsChanged;
        _noStationsCtrl.RestoringFromBackup += OnRestoringFromBackup;
        splitContainer1.Panel2.Controls.Add(_noStationsCtrl);

        var windowSize = GlobalData.ConfigManager.Get("windowSize") as WindowSize
                         ?? new WindowSize(0, 0);

        if (!windowSize.IsEmpty())
            Size = new Size(windowSize.Width, windowSize.Height);

        Translate();

        //SetApiStatus(this, EventArgs.Empty); //TODO: Re-enable this when the API feature is fully implemented
    }

    /// <summary>
    ///     Handles the Shown event of the MainForm.
    /// </summary>
    private void MainForm_Shown(object sender, EventArgs e)
    {
        SelectLanguage();
        Translate();

        var missingStationsCount = StationManager.CheckGameForExistingStations(StagingPath, GameBasePath);
        if (missingStationsCount > 0)
        {
            var text = string.Format(GlobalData.Strings.GetString("MissingStations") ??
                                     "There are {0} station(s) in the game's folder missing from the staging folder. " +
                                     "Would you like to synchronize the stations?",
                missingStationsCount);
            var caption = GlobalData.Strings.GetString("MissingStationsCaption") ?? "Missing Stations";
            if (MessageBox.Show(this, text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                DialogResult.Yes)
                _ = SyncStationsAsync();
        }
        else
        {
            PopulateStations();
        }

        HandleUserControlVisibility();
    }

    /// <summary>
    ///     Initializes the language drop-down with supported languages and images.
    /// </summary>
    private void InitializeLanguageDropDown()
    {
        //TODO: Add [New Station] translations in the list box when the language is changed.
        // Populate the language combo box
        _languages.Add(new ImageComboBoxItem("English (en)", Resources.united_kingdom));
        _languages.Add(new ImageComboBoxItem("Español (es)", Resources.spain));
        _languages.Add(new ImageComboBoxItem("Français (fr)", Resources.france));

        foreach (var language in _languages)
            _languageComboBox.Items.Add(language);

        _languageComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

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
        backupStagingFolderToolStripMenuItem.Text = GlobalData.Strings.GetString("BackupStagingFolder");
        openStagingPathToolStripMenuItem.Text = GlobalData.Strings.GetString("OpenStagingFolder");
        openGamePathToolStripMenuItem.Text = GlobalData.Strings.GetString("OpenGameFolder");
        openLogFolderToolStripMenuItem.Text = GlobalData.Strings.GetString("OpenLogFolder");
        exportToGameToolStripMenuItem.Text = GlobalData.Strings.GetString("ExportStations");
        synchronizeStationsToolStripMenuItem.Text = GlobalData.Strings.GetString("SynchronizeStations");
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

        grpStations.Text = GlobalData.Strings.GetString("Stations");

        // Buttons
        btnAddStation.Text = GlobalData.Strings.GetString("NewStation");
        btnDeleteStation.Text = GlobalData.Strings.GetString("DeleteStation");
        btnEnableSelected.Text = GlobalData.Strings.GetString("EnableSelectedStation");
        btnEnableAll.Text = GlobalData.Strings.GetString("EnableAllStations");
        btnDisableSelected.Text = GlobalData.Strings.GetString("DisableSelectedStation");
        btnDisableAll.Text = GlobalData.Strings.GetString("DisableAllStations");

        lblBackupStatus.Text = GlobalData.Strings.GetString("BackupReady") ?? "Backup Ready";

        UpdateEnabledStationCount();
    }

    /// <summary>
    ///     Handles the visibility of the user controls when there are no stations.
    /// </summary>
    private void HandleUserControlVisibility()
    {
        if (!StationManager.Instance.IsEmpty)
        {
            _noStationsCtrl.Visible = false;
            return;
        }

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
                StationManager.Instance.LoadStations(StagingPath);

            UpdateUiAfterPopulation();

            lbStations.EndUpdate();
        });
    }

    /// <summary>
    ///     Updates the UI after populating the stations.
    /// </summary>
    private void UpdateUiAfterPopulation()
    {
        if (lbStations.Items.Count > 0)
            SelectListBoxItem(0, false);

        HandleUserControlVisibility();
        UpdateEnabledStationCount();
    }

    /// <summary>
    /// Selects a listbox item from the station listbox based on the index. Also, updates the station editor and title bar.
    /// </summary>
    /// <param name="index">The index to select in the listbox.</param>
    /// <param name="userDriven">Indicate whether the selection was driven by the user.</param>
    private void SelectListBoxItem(int index, bool userDriven)
    {
        if (index < 0 || index >= lbStations.Items.Count) return;

        if (userDriven)
        {
            if (_ignoreSelectedIndexChanged)
            {
                _ignoreSelectedIndexChanged = false;
                return;
            }
        }
        else
        {
            lbStations.SelectedIndex = index;
        }

        if (lbStations.SelectedItem is not TrackableObject<Station> station) return;
        SelectStationEditor(station.Id);
        UpdateTitleBar(station.Id);
    }

    /// <summary>
    ///     Refreshes the UI after the paths have changed from the <see cref="PathSettings" /> or the <see cref="ConfigForm"/>.
    ///     <param name="sender">The event sender.</param>
    ///     <param name="e">The event arguments.</param>
    /// </summary>
    private void OnPathsChanged(object? sender, EventArgs e)
    {
        SetupDirectoryWatcher();
        PopulateStations();
    }

    /// <summary>
    ///     Updates the enabled station count label.
    /// </summary>
    private void UpdateEnabledStationCount()
    {
        this.SafeInvoke(() => { lblStationCount.Text = StationManager.Instance.GetStationCount(); });
    }

    /// <summary>
    ///     Handles the SelectedIndexChanged event of the lbStations list box.
    ///     <param name="sender">The event sender.</param>
    ///     <param name="e">The event arguments.</param>
    /// </summary>
    private void LbStations_SelectedIndexChanged(object? sender, EventArgs e)
    {
        this.SafeInvoke(() => SelectListBoxItem(lbStations.SelectedIndex, true));
    }

    /// <summary>
    ///     Updates the currently displayed station editor.
    ///     <param name="editor">The editor to display in the split panel.</param>
    /// </summary>
    private void UpdateStationEditor(StationEditor? editor)
    {
        try
        {
            if (_currentEditor == editor) return;
            if (editor == null) return;

            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(editor);
            splitContainer1.Panel2.ResumeLayout();

            _currentEditor = editor;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<MainForm>("UpdateStationEditor")
                .Error(ex, "An error occurred while updating the station editor.");
        }
    }

    private void RevertChangesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lbStations.SelectedItem is not TrackableObject<Station> station) return;

        //We only want to revert the changes if there is a pending save. Otherwise, the wrong icon is drawn in the list box.
        if (!station.IsPendingSave) return;

        if (StationManager.Instance.IsNewStation(station.Id)) return; //Don't allow reverting changes on new stations.

        StationManager.Instance.RemoveStation(station.Id); //Remove station from manager
        station.DeclineChanges(); // Revert the changes made to the station's properties since the last save.
        StationManager.Instance.AddStation(station, true); //Re-add the station to the manager after reverting changes.

        OnStationUpdated(sender, station.Id); //Update the UI to reflect the changes.
        SelectStationEditor(station.Id); //Update the editor to reflect the changes.
    }

    /// <summary>
    ///     Handles the Click event of the btnEnableStation button.
    ///     <param name="sender">The event sender.</param>
    ///     <param name="e">The event arguments.</param>
    /// </summary>
    private void BtnEnableStation_Click(object sender, EventArgs e)
    {
        if (lbStations.SelectedItem is not TrackableObject<Station> s) return;

        SetStationStatus(true, false, s.Id);
    }

    /// <summary>
    ///     Handles the Click event of the btnDisableStation button.
    ///     <param name="sender">The event sender.</param>
    ///     <param name="e">The event arguments.</param>
    /// </summary>
    private void BtnDisableStation_Click(object sender, EventArgs e)
    {
        if (lbStations.SelectedItem is not TrackableObject<Station> s) return;

        SetStationStatus(false, false, s.Id);
    }

    /// <summary>
    ///     Handles the Click event of the btnEnableAll button.
    ///     <param name="sender">The event sender.</param>
    ///     <param name="e">The event arguments.</param>
    /// </summary>
    private void BtnEnableAll_Click(object sender, EventArgs e)
    {
        SetStationStatus(true, true, lbStations.Items.Cast<TrackableObject<Station>>().Select(s => s.Id).ToArray());
    }

    /// <summary>
    ///     Handles the Click event of the btnDisableAll button.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void BtnDisableAll_Click(object sender, EventArgs e)
    {
        SetStationStatus(false, true, lbStations.Items.Cast<TrackableObject<Station>>().Select(s => s.Id).ToArray());
    }

    /// <summary>
    /// Sets the status of the station(s) to the new status.
    /// </summary>
    /// <param name="newStatus">The new status to change the station to: <c>true</c> = enabled; <c>false</c> = disabled</param>
    /// <param name="setAllStations">Indicate whether to set all stations to the same status.</param>
    /// <param name="stationIds">The station ID(s) to change the status of.</param>
    private void SetStationStatus(bool newStatus, bool setAllStations, params Guid[] stationIds)
    {
        lbStations.BeginUpdate();
        if (!setAllStations)
        {
            StationManager.Instance.ChangeStationStatus(stationIds[0], newStatus);
            StationManager.Instance.CheckStatus(stationIds[0]);
        }
        else
        {
            foreach (var stationId in stationIds)
            {
                StationManager.Instance.ChangeStationStatus(stationId, newStatus);
                StationManager.Instance.CheckStatus(stationId);
            }
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

        var id = StationManager.Instance.AddStation();

        lbStations.SelectedItem = StationManager.Instance.GetStation(id)?.Key;
        SelectStationEditor(id);
        UpdateEnabledStationCount();
        HandleUserControlVisibility();
    }

    /// <summary>
    /// Stops all music players and updates the UI with the correct station editor based on the station's ID.
    /// </summary>
    /// <param name="stationId">The ID of the station to get the editor of.</param>
    private void SelectStationEditor(Guid? stationId)
    {
        if (stationId == null) return;

        StationManager.Instance.StopAllMusicPlayers();
        UpdateStationEditor(StationManager.Instance.GetStationEditor(stationId));
    }

    /// <summary>
    /// Updates the title bar with the app name followed by the relative path to the station.
    /// </summary>
    /// <param name="stationId"></param>
    private void UpdateTitleBar(Guid? stationId)
    {
        this.SafeInvoke(() =>
        {
            if (stationId == null)
            {
                Text = GlobalData.Strings.GetString("MainTitle");
                return;
            }

            var path = StationManager.Instance.GetStationPath(stationId);
            Text = $"{GlobalData.Strings.GetString("MainTitle")} - {path}";
        });
    }

    /// <summary>
    ///     Handles the station updated event. Checks for pending save status on a station, duplication in station names, and updates the list box.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="stationId">The event arguments.</param>
    private void OnStationUpdated(object? sender, Guid stationId)
    {
        if (lbStations.SelectedItem is not TrackableObject<Station> station) return;

        StationManager.Instance.OnStationUpdated(station.Id);
        lbStations.BeginUpdate();
        lbStations.Invalidate();
        lbStations.EndUpdate();
    }

    private void BtnDeleteStation_Click(object sender, EventArgs e)
    {
        if (GameBasePath.Equals(string.Empty) || StagingPath.Equals(string.Empty))
            return;

        if (lbStations.SelectedItem is not TrackableObject<Station> station) return;
        StationManager.Instance.RemoveStation(station.Id);
        UpdateEnabledStationCount();
        HandleUserControlVisibility();
    }

    private void CmbLanguageSelect_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_languageComboBox.SelectedItem is not ImageComboBoxItem culture) return;

        SuspendLayout();
        GlobalData.SetCulture(culture.Text);

        Translate();

        StationManager.Instance.TranslateEditors();
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
        //Don't allow exporting if we are currently synchronizing stations.
        if (_isSyncInProgress)
        {
            var text = GlobalData.Strings.GetString("SyncInProgress") ??
                       "Synchronization is in progress. Please wait...";
            var caption = GlobalData.Strings.GetString("SyncAbbrev") ?? "Sync";
            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var missingSongs = StationManager.Instance.CheckForMissingSongs();
        if (missingSongs.Values.Any(p => p.Key))
        {
            var count = missingSongs.Count(p => p.Value.Key);
            var totalSongCount = missingSongs.Values.Where(p => p.Key).Sum(p => p.Value);

            var text = string.Format(GlobalData.Strings.GetString("ExportToGameMissingSongs") ??
                                     "There are {0} station(s) with a total of {1} invalid song path(s). Do you want to continue exporting?",
                count, totalSongCount);
            var caption = GlobalData.Strings.GetString("SongsMissingPaths") ?? "Songs Missing Paths";

            var result = MessageBox.Show(this, text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;
        }

        var exportWindow = new ExportWindow();
        exportWindow.OnExportToStagingComplete += (_, _) => { PopulateStations(); };
        exportWindow.ShowDialog(this);
    }

    private void ConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
    {
        OpenConfigForm("tabGeneral");
    }

    private void OpenConfigForm(string tabName)
    {
        var configForm = new ConfigForm(tabName);
        //configForm.ConfigSaved += SetApiStatus; //TODO: Re-enable this when the API feature is fully implemented
        configForm.StagingPathChanged += OnPathsChanged;

        configForm.ShowDialog(this);
    }

    //TODO: Hidden until the API feature is fully implemented
    //private async void SetApiStatus(object? sender, EventArgs e)
    //{
    //    if (!NexusApi.IsAuthenticated)
    //    {
    //        this.SafeInvoke(() =>
    //        {
    //            apiStatusToolStripMenuItem.Text = GlobalData.Strings.GetString("ApiStatus") ?? "API Not Authenticated";
    //            apiStatusToolStripMenuItem.Image = null;
    //            modsToolStripMenuItem.Visible = false;
    //        });
    //        return;
    //    }

    //    var image = await NexusApi.GetUserImage();
    //    if (NexusApi.CurrentApiUser != null)
    //    {
    //        this.SafeInvoke(() =>
    //        {
    //            modsToolStripMenuItem.Visible = true;
    //            apiStatusToolStripMenuItem.Text = NexusApi.CurrentApiUser.Name;
    //            apiStatusToolStripMenuItem.Image = image;
    //        });
    //    }
    //}

    //TODO: Hidden until the API feature is fully implemented
    private void ApiStatusToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (NexusApi.CurrentApiUser == null)
            OpenConfigForm("tabNexus");
        else
            $"https://next.nexusmods.com/profile/{NexusApi.CurrentApiUser.Name}/about-me".OpenUrl();
    }

    private void DownloadRadioModsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        new ModDownloader().ShowDialog(this);
    }

    private void PathsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var pathDialog = new PathSettings();
        pathDialog.StagingPathChanged += PathDialog_StagingPathChanged;
        pathDialog.ShowDialog(this);
    }

    private void PathDialog_StagingPathChanged(object? sender, EventArgs e)
    {
        OnPathsChanged(sender, e);
    }

    private void RefreshStationsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var text = GlobalData.Strings.GetString("ConfirmRefreshStations");
        var caption = GlobalData.Strings.GetString("Confirm");
        if (MessageBox.Show(this, text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            PopulateStations();
    }

    private void SynchronizeStationsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        StartStationSync(true);
    }

    /// <summary>
    /// Start the station synchronization operation. Displays a message to the user to confirm the operation depending on the context.
    /// </summary>
    /// <param name="userInitiated">Indicate whether the sync operation was initiated from user interaction or the file system watcher.</param>
    private void StartStationSync(bool userInitiated)
    {
        if (string.IsNullOrEmpty(StagingPath)) return;
        if (string.IsNullOrEmpty(GameBasePath)) return;

        if (userInitiated)
        {
            var text = GlobalData.Strings.GetString("ConfirmSyncStations") ??
                       "Are you sure you want to synchronize the stations?" +
                       " This will overwrite any modifications to stations that haven't been exported.";
            var caption = GlobalData.Strings.GetString("Confirm");
            var result = MessageBox.Show(this, text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No) return;
        }
        else if (GlobalData.ConfigManager.Get("watchForChanges") as bool? == true)
        {
            var text = GlobalData.Strings.GetString("ConfirmSyncStations") ??
                       "Changes were made to the game's radios directory." +
                       " Do you want to synchronize your staging and game directories?";
            var caption = GlobalData.Strings.GetString("Confirm");
            var result = MessageBox.Show(this, text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
        }

        // Initialize ProgressBar
        pgBackupProgress.Value = 0;
        statusStripBackup.Visible = true;

        _ = SyncStationsAsync();
    }

    private async Task SyncStationsAsync()
    {
        if (_isAppClosing) return;

        try
        {
            _isSyncInProgress = true;
            _isExportInProgress = true; //to prevent directory watcher from firing events during sync
            await StationManager.Instance.SynchronizeStationsAsync(StagingPath, GameBasePath);
        }
        catch (Exception ex)
        {
            var text = string.Format(
                GlobalData.Strings.GetString("SyncFailedException") ?? "Synchronization failed due to an error: {0}",
                ex.Message);
            var caption = GlobalData.Strings.GetString("SyncAbbrev") ?? "Sync";
            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            AuLogger.GetCurrentLogger<MainForm>("SyncStationsAsync")
                .Error(ex, "An error occurred while synchronizing stations from game to staging.");
        }
        finally
        {
            _isExportInProgress = false;
        }
    }

    private void OnStationsSynchronized(bool success)
    {
        if (_isAppClosing) return;

        this.SafeInvoke(() =>
        {
            statusStripBackup.Visible = false;
            _isSyncInProgress = false;
            if (success)
            {
                PopulateStations();
                _isExportInProgress = false;

                AuLogger.GetCurrentLogger<MainForm>("OnStationsSynchronized")
                    .Info("Stations synchronized from game to staging successfully.");
            }
            else
            {
                var text = string.Format(
                    GlobalData.Strings.GetString("SyncFailed") ?? "Synchronization Failed!");
                var caption = GlobalData.Strings.GetString("SyncAbbrev") ?? "Sync";
                MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                AuLogger.GetCurrentLogger<MainForm>("OnStationsSynchronized")
                    .Error("An unknown error occurred while synchronizing stations from game to staging.");
            }

            // Clear the status message after a short delay
            Task.Delay(2000).ContinueWith(_ => { this.SafeInvoke(() => lblBackupStatus.Text = string.Empty); });
        });
    }

    private void OnStationSyncStatusChanged(string status)
    {
        if (!_isAppClosing)
            this.SafeInvoke(() => lblBackupStatus.Text = status);
    }

    private void OnStationSyncProgressChanged(int progress)
    {
        if (!_isAppClosing)
            this.SafeInvoke(() => pgBackupProgress.Value = progress);
    }

    private void BackupStagingFolderToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(StagingPath)) return;

        //Check for sync in progress to prevent backup during sync
        if (_isSyncInProgress)
        {
            var text = GlobalData.Strings.GetString("SyncInProgress") ??
                       "Synchronization is in progress. Please wait...";
            var caption = GlobalData.Strings.GetString("SyncAbbrev") ?? "Sync";
            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var compressionLevel = GlobalData.ConfigManager.GetConfig()?.BackupCompressionLevel ?? CompressionLevel.Normal;
        new BackupPreview(compressionLevel).ShowDialog();
    }

    private void RestoreStagingFolderToolStripMenuItem_Click(object sender, EventArgs e)
    {
        //Check for sync in progress to prevent restore during sync
        if (_isSyncInProgress)
        {
            var text = GlobalData.Strings.GetString("SyncInProgress") ??
                       "Synchronization is in progress. Please wait...";
            var caption = GlobalData.Strings.GetString("SyncAbbrev") ?? "Sync";
            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        //TODO: translations
        if (_isExportInProgress)
        {
            var text = GlobalData.Strings.GetString("ExportInProgress") ??
                       "Export is in progress. Please wait...";
            var caption = GlobalData.Strings.GetString("ExportAbbrev") ?? "Exporting";
            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var confirmText = GlobalData.Strings.GetString("ConfirmRestore") ??
                   "Are you sure you want to restore the staging folder? This will overwrite your existing stations!";
        var confirmCaption = GlobalData.Strings.GetString("Confirm");
        if (MessageBox.Show(this, confirmText, confirmCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            return;

        var fileBrowser = new OpenFileDialog
        {
            Filter = "Backup files (*.zip)|*.zip",
            Title = GlobalData.Strings.GetString("SelectBackupFile") ?? "Select Backup File"
        };

        if (fileBrowser.ShowDialog(this) != DialogResult.OK) return;
        RestoreFromBackup(fileBrowser.FileName);
    }

    private void OnRestoringFromBackup(object? sender, string backupFilePath) => RestoreFromBackup(backupFilePath);

    private void RestoreFromBackup(string backupFile)
    {
        _isExportInProgress = true; //to prevent directory watcher from firing events.

        var restoreWindow = new RestoreForm(backupFile, StagingPath);
        restoreWindow.RestoreCompleted += (_, _) =>
        {
            _isExportInProgress = false;
            PopulateStations();
        };
        restoreWindow.ShowDialog(this);
    }

    private void ClearAllDataToolStripMenuItem_Click(object sender, EventArgs e)
    {
        //Check for sync in progress to prevent restore during sync
        if (_isSyncInProgress)
        {
            var text = GlobalData.Strings.GetString("SyncInProgress") ??
                       "Synchronization is in progress. Please wait...";
            var caption = GlobalData.Strings.GetString("SyncAbbrev") ?? "Sync";
            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        //TODO: translations
        if (_isExportInProgress)
        {
            var text = GlobalData.Strings.GetString("ExportInProgress") ??
                       "Export is in progress. Please wait...";
            var caption = GlobalData.Strings.GetString("ExportAbbrev") ?? "Exporting";
            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var confirmText = GlobalData.Strings.GetString("ConfirmClearAllData") ??
                   "**Destructive Operation**\nAre you sure you want to clear all data? This will remove all stations immediately from staging.";
        var confirmCaption = GlobalData.Strings.GetString("Confirm");
        if (MessageBox.Show(this, confirmText, confirmCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            return;

        //Remove all stations and the folders from the staging directory.
        StationManager.Instance.ClearStations(true);

        //Populate the (what should be the empty) stations list.
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

    private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            Close();
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<MainForm>("ExitToolStripMenuItem_Click")
                .Error(ex, "An error occurred while closing the application.");
        }
    }

    /// <summary>
    /// Get a value indicating if there are stations pending save and the user confirmed to quit the application.
    /// </summary>
    /// <returns><c>true</c> if there are no pending saves or the user confirmed exit;
    /// <c>false</c> if there are pending changes and the user denied exit.</returns>
    private bool CheckForPendingSaveStations()
    {
        var pendingSave = StationManager.Instance.CheckPendingSave();
        if (pendingSave.Values.All(p => p != true)) return true;

        var count = pendingSave.Count(p => p.Value);
        var text = string.Format(GlobalData.Strings.GetString("ConfirmExit")
                                 ?? "There are {0} stations pending export. Are you sure you want to quit?", count);
        var caption = GlobalData.Strings.GetString("Confirm");

        return MessageBox.Show(this, text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.No;
    }

    private void MainForm_Resize(object sender, EventArgs e)
    {
        // Restart the Timer each time the Resize event is triggered
        _resizeTimer.Stop();
        _resizeTimer.Start();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        _isAppClosing = true;

        if (e.CloseReason is CloseReason.TaskManagerClosing or CloseReason.WindowsShutDown) return;

        if (!CheckForPendingSaveStations())
            e.Cancel = true;
        else
            CleanupEvents(); // Clean up events (unsubscribe) before closing the application
    }

    private void IconGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (lbStations.SelectedItem is not TrackableObject<Station> station) return;
        if (!station.TrackedObject.CustomIcon.UseCustom ||
            (station.TrackedObject.CustomIcon.InkAtlasPath.Equals(string.Empty)
             && station.TrackedObject.CustomIcon.InkAtlasPart.Equals(string.Empty))) return;

        var managerForm = new IconManagerForm(station);
        managerForm.IconAdded += ManagerFormOnIconUpdated;
        managerForm.IconDeleted += ManagerFormOnIconUpdated;
        managerForm.IconUpdated += ManagerFormOnIconUpdated;
        managerForm.ShowDialog(this);

        //new IconGeneratorForm().ShowDialog(this);
        //PythonEngine.Initialize();
        //using (Py.GIL())
        //{
        //    dynamic pillow = Py.Import("pillow");
        //    //TODO: Implement icon generator
        //    //See https://wiki.redmodding.org/wolvenkit/wolvenkit-cli/usage/command-list#pack
        //    //See https://github.com/DoctorPresto/Cyberpunk-Helper-Scripts/blob/main/generate_inkatlas.py
        //}
    }

    private void ManagerFormOnIconUpdated(object? sender, Guid e)
    {
        try
        {
            if (lbStations.SelectedItem is not TrackableObject<Station> station) return;

            var icon = StationManager.Instance.GetStationActiveIcon(station.Id);
            if (icon == null) return;

            var editor = StationManager.Instance.GetStationEditor(station.Id);
            editor?.UpdateIcon(icon);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<MainForm>("ManagerFormOnIconUpdated")
                .Error(ex, "An error occurred while updating the station icon.");
        }
    }
}