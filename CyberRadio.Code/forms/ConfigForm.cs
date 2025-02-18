// ConfigForm.cs : RadioExt-Helper
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

using AetherUtils.Core.Extensions;
using AetherUtils.Core.Logging;
using RadioExt_Helper.config;
using RadioExt_Helper.nexus_api;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

/// <summary>
///     Represents a configuration form.
/// </summary>
public sealed partial class ConfigForm : Form
{
    private readonly Dictionary<string, CompressionLevel> _localizedCompressionLevels = new();
    private readonly ImageList _tabImages = new();

    private Dictionary<string, ListViewGroup> _forbiddenGroups = new();

    /// <summary>
    ///     Initializes a new instance of the <see cref="ConfigForm" /> class.
    /// <param name="tabName">The initial tab to open. If empty, or invalid, defaults to first tab.</param>
    /// </summary>
    public ConfigForm(string tabName)
    {
        InitializeComponent();

        if (string.IsNullOrEmpty(tabName) || !tabConfigs.TabPages.ContainsKey(tabName))
            tabConfigs.SelectedIndex = 0;
        else
            tabConfigs.SelectedTab = tabConfigs.TabPages[tabName];
    }

    /// <summary>
    /// Occurs when the configuration is saved to disk.
    /// </summary>
    public event EventHandler? ConfigSaved;

    /// <summary>
    /// Occurs whenever the game path is changed.
    /// </summary>
    public event EventHandler? GamePathChanged;

    /// <summary>
    /// Occurs whenever the staging path is changed.
    /// </summary>
    public event EventHandler? StagingPathChanged;

    /// <summary>
    ///     Handles the Load event of the ConfigForm control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void ConfigForm_Load(object sender, EventArgs e)
    {
        _tabImages.Images.Add("general", Resources.settings__16x16);
        _tabImages.Images.Add("paths", Resources.folder_no_fill__16x16);
        _tabImages.Images.Add("logging", Resources.log__16x16);
        _tabImages.Images.Add("nexus_api", Resources.api_16x16);
        tabConfigs.ImageList = _tabImages;

        tabGeneral.ImageKey = @"general";
        tabPathSetup.ImageKey = @"paths";
        tabLogging.ImageKey = @"logging";
        tabNexus.ImageKey = @"nexus_api";

        tabConfigs.TabPages
            .Remove(tabNexus); //TODO: Disable the Nexus API tab for now. Enable when feature is implemented to download mods.

        Translate();
        AddCompressionOptions();
        SetValues();
    }

    /// <summary>
    ///     Translates the text of the form and its controls based on the language settings.
    /// </summary>
    private void Translate()
    {
        Text = Strings.Configuration;

        //Tabs
        tabGeneral.Text = Strings.GeneralSettings;
        tabPathSetup.Text = Strings.PathSettings;
        tabLogging.Text = Strings.LogSettings;

        //General Tab
        chkCheckForUpdates.Text = Strings.CheckForUpdatesOption;
        chkAutoExportToGame.Text = Strings.AutoExportOption;
        chkWatchForChanges.Text = Strings.WatchForChangesOption;
        lblBackupCompressionLvl.Text = Strings.BackupCompressionLevel;
        chkCopySongFilesToBackup.Text = Strings.CopySongFilesToBackupOption;
        btnEditDefaultSongLocation.Text = Strings.DefaultSongLocationOption;

        //Forbidden Paths Tab
        btnAddKeyword.Text = Strings.ForbiddenKeywordInput_Title;
        btnDeleteSelectedKeyword.Text = Strings.DeleteSelectedKeywords;
        btnEditPaths.Text = Strings.EditPathsOption;

        //Logging Tab
        lblLogPathLabel.Text = Strings.LogPathLabel;
        lblCurrentLogPath.Text = Strings.NoLogPathSet;
        chkNewFileEveryLaunch.Text = Strings.NewLogFileOption;
        btnEditLogsPath.Text = Strings.EditLogsPathOption;

        //Buttons
        btnSaveAndClose.Text = Strings.SaveAndClose;
        btnResetToDefault.Text = Strings.ResetToDefaults;
        btnCancel.Text = Strings.Cancel;

        //TODO: Add translations for new "Nexus API" tab when feature is fully implemented.
    }

    /// <summary>
    ///     Set the values in the form to the configuration values.
    /// </summary>
    private void SetValues()
    {
        var config = GlobalData.ConfigManager.GetConfig();
        if (config == null) return;

        chkCheckForUpdates.Checked = config.AutoCheckForUpdates;
        chkAutoExportToGame.Checked = config.AutoExportToGame;
        chkNewFileEveryLaunch.Checked = config.LogOptions.NewFileEveryLaunch;
        chkWatchForChanges.Checked = config.WatchForGameChanges;
        chkCopySongFilesToBackup.Checked = config.CopySongFilesToBackup;
        lblDefaultSongLocation.Text = config.DefaultSongLocation;

        lblCurrentLogPath.Text = config.LogOptions.LogFileDirectory == string.Empty
            ? lblCurrentLogPath.Text
            : config.LogOptions.LogFileDirectory;

        txtApiKey.Text = config.NexusApiKey;

        //SetApiAuthStatus(); //TODO: Re-enable this line when the feature is fully implemented.

        // Set the combo box to the current compression level
        var compressionLevel = config.BackupCompressionLevel;
        cmbCompressionLevels.SelectedItem = GetLocalizedName(compressionLevel);

        //Set the forbidden keywords
        SetupForbiddenPathsListView(config);
    }

    private void SetupForbiddenPathsListView(ApplicationConfig? config)
    {
        lvForbiddenPaths.BeginUpdate();
        lvForbiddenPaths.Columns.Add(Strings.ForbiddenKeywordListViewColumn, 120, HorizontalAlignment.Left);

        // Clear existing items
        lvForbiddenPaths.Items.Clear();
        lvForbiddenPaths.Groups.Clear(); // Also clear existing groups to avoid duplicates

        // Retrieve the list from configuration
        var keywords = config?.ForbiddenKeywords ?? [];

        foreach (var keyword in keywords)
        {
            // Ensure the group exists
            if (!_forbiddenGroups.TryGetValue(keyword.Group, out var group))
            {
                group = new ListViewGroup(keyword.Group, HorizontalAlignment.Left);
                lvForbiddenPaths.Groups.Add(group);
                _forbiddenGroups[keyword.Group] = group;
            }

            // Create and add item to the ListView
            var item = new ListViewItem(keyword.Keyword)
            {
                Checked = keyword.IsForbidden,
                Group = group,
                Tag = keyword
            };

            lvForbiddenPaths.Items.Add(item);
        }

        //Setup always forbidden items (not in the configuration as we don't want to allow removal)
        var alwaysForbidden = PathHelper.GetAlwaysForbiddenPaths();
        foreach (var keyword in alwaysForbidden)
        {
            // Ensure the group exists
            if (!_forbiddenGroups.TryGetValue(keyword.Group, out var group))
            {
                group = new ListViewGroup(keyword.Group, HorizontalAlignment.Left);
                lvForbiddenPaths.Groups.Add(group);
                _forbiddenGroups[keyword.Group] = group;
            }
            // Create and add item to the ListView (don't add tag as it's not a config item)
            var item = new ListViewItem(keyword.Keyword)
            {
                Checked = keyword.IsForbidden,
                Group = group
            };

            lvForbiddenPaths.Items.Add(item);
        }

        // Resize the columns to fit the content
        lvForbiddenPaths.ResizeColumns();

        lvForbiddenPaths.EndUpdate();
    }

    private void btnAddKeyword_Click(object sender, EventArgs e)
    {
        ForbiddenKeywordInput inputForm = new(lvForbiddenPaths.Groups);
        inputForm.ForbiddenKeywordAdded += InputForm_ForbiddenKeywordAdded;
        inputForm.ShowDialog(this);
    }

    private void InputForm_ForbiddenKeywordAdded(object? sender, ForbiddenKeyword e)
    {
        lvForbiddenPaths.BeginUpdate();

        // Ensure the group exists
        if (!_forbiddenGroups.TryGetValue(e.Group, out var group))
        {
            group = new ListViewGroup(e.Group, HorizontalAlignment.Left);
            lvForbiddenPaths.Groups.Add(group);
            _forbiddenGroups[e.Group] = group;
        }

        // Add the new keyword to the list view
        var item = new ListViewItem(e.Keyword)
        {
            Checked = e.IsForbidden,
            Group = group,
            Tag = e
        };

        lvForbiddenPaths.Items.Add(item);

        // Resize the columns to fit the content
        lvForbiddenPaths.ResizeColumns();

        lvForbiddenPaths.EndUpdate();
    }

    private void btnDeleteSelectedKeyword_Click(object sender, EventArgs e)
    {
        lvForbiddenPaths.BeginUpdate();

        // Copy selected items to avoid modification exception
        var selectedItems = lvForbiddenPaths.SelectedItems.Cast<ListViewItem>().ToList();

        // Remove each selected item from the ListView
        foreach (var item in selectedItems)
        {
            if (item.Tag is ForbiddenKeyword) // Don't remove always forbidden items. We can just check if the item has a tag to determine if it's a config item.
                lvForbiddenPaths.Items.Remove(item);
        }

        // Cleanup any empty groups
        var groupsToRemove = lvForbiddenPaths.Groups.Cast<ListViewGroup>()
            .Where(group => lvForbiddenPaths.Items.Cast<ListViewItem>().All(item => item.Group != group))
            .ToList();

        foreach (var group in groupsToRemove.Where(group => !group.Header.Equals(Strings.SystemPaths, StringComparison.CurrentCulture)))
        {
            _forbiddenGroups.Remove(group.Header);
            lvForbiddenPaths.Groups.Remove(group);
        }

        lvForbiddenPaths.EndUpdate();
    }

    private void lvForbiddenPaths_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
        if (e.Item.Tag is not ForbiddenKeyword keyword)
        {
            e.Item.Checked = true;
            return;
        }

        keyword.IsForbidden = e.Item.Checked;
    }

    /// <summary>
    /// Add the available compression levels to the combo box.
    /// </summary>
    private void AddCompressionOptions()
    {
        _localizedCompressionLevels.Clear();
        cmbCompressionLevels.Items.Clear();

        foreach (CompressionLevel level in Enum.GetValues(typeof(CompressionLevel)))
        {
            var localizedName = GetLocalizedName(level);
            _localizedCompressionLevels[localizedName] = level;
            cmbCompressionLevels.Items.Add(localizedName);
        }

        cmbCompressionLevels.SelectedIndex = 0;
    }

    /// <summary>
    /// Get the localized name for the backup compression level.
    /// </summary>
    /// <param name="level">The <see cref="CompressionLevel"/> to localize.</param>
    /// <returns>A string localized into the current UI culture.</returns>
    private string GetLocalizedName(CompressionLevel level)
    {
        return level switch
        {
            CompressionLevel.Normal => Strings.Normal,
            CompressionLevel.None => Strings.None,
            CompressionLevel.Fastest => Strings.Fastest,
            CompressionLevel.Fast => Strings.Fast,
            CompressionLevel.SuperFast => Strings.SuperFast,
            CompressionLevel.High => Strings.High,
            CompressionLevel.Maximum => Strings.Maximum,
            CompressionLevel.Ultra => Strings.Ultra,
            CompressionLevel.Extreme => Strings.Extreme,
            CompressionLevel.Ultimate => Strings.Ultimate,
            _ => Strings.Normal
        };
    }

    //TODO: Uncomment the below when the API feature is implemented
    ///// <summary>
    ///// Set the status of the API authentication on the UI.
    ///// </summary>
    //private void SetApiAuthStatus()
    //{
    //    btnClearApiKey.Enabled = txtApiKey.Text.Length > 0;

    //    var isSameKey = txtApiKey.Text.Equals(GlobalData.ConfigManager.Get("nexusApiKey") as string);

    //    var authText = Strings.ApiAuthenticated ?? "Authenticated";
    //    var notAuthText = Strings.ApiNotAuthenticated ?? "Not Authenticated";

    //    lblAuthenticatedStatus.Text = isSameKey && !txtApiKey.Text.Equals(string.Empty) ? authText : notAuthText;
    //    picApiStatus.Image = isSameKey && !txtApiKey.Text.Equals(string.Empty)
    //        ? Resources.enabled__16x16
    //        : Resources.disabled__16x16;
    //    lblAuthenticatedStatus.ForeColor =
    //        isSameKey && !txtApiKey.Text.Equals(string.Empty) ? Color.DarkGreen : Color.Red;
    //    btnAuthenticate.Enabled = !isSameKey && !txtApiKey.Text.Equals(string.Empty);
    //}

    /// <summary>
    ///     Resets the configuration to the default values.
    /// </summary>
    private void ResetConfig()
    {
        var currentConfig = GlobalData.ConfigManager.GetConfig();
        if (currentConfig == null) return;

        //Create a new default config
        if (!GlobalData.ConfigManager.CreateDefaultConfig()) return;

        var defaultConfig = GlobalData.ConfigManager.GetConfig();
        if (defaultConfig == null) return;

        //Clear the API key from the current config
        NexusApi.ClearAuthentication();

        //Set the values from the current config for the paths to the default config
        defaultConfig.StagingPath = currentConfig.StagingPath;
        defaultConfig.GameBasePath = currentConfig.GameBasePath;

        //Finally, save the configuration changes and set the values on the form to use the new, default config
        if (GlobalData.ConfigManager.Save())
            ConfigSaved?.Invoke(this, EventArgs.Empty);

        SetValues();
    }

    /// <summary>
    ///     Sets the configuration values based on the form inputs.
    /// </summary>
    /// <returns><c>true</c> if the configuration is saved; <c>false</c> otherwise.</returns>
    private bool SetConfig()
    {
        var saved = GlobalData.ConfigManager.Set("autoCheckForUpdates", chkCheckForUpdates.Checked);
        saved &= GlobalData.ConfigManager.Set("autoExportToGame", chkAutoExportToGame.Checked);
        saved &= GlobalData.ConfigManager.Set("newFileEveryLaunch", chkNewFileEveryLaunch.Checked);
        saved &= GlobalData.ConfigManager.Set("watchForGameChanges", chkWatchForChanges.Checked);
        saved &= GlobalData.ConfigManager.Set("copySongFilesToBackup", chkCopySongFilesToBackup.Checked);
        saved &= GlobalData.ConfigManager.Set("defaultSongLocation", lblDefaultSongLocation.Text);

        var selectedLocalizedName = cmbCompressionLevels.SelectedItem?.ToString();
        if (selectedLocalizedName != null &&
            _localizedCompressionLevels.TryGetValue(selectedLocalizedName, out var compressionLevel))
            saved &= GlobalData.ConfigManager.Set("backupCompressionLevel", compressionLevel);
        else
            saved &= GlobalData.ConfigManager.Set("backupCompressionLevel", CompressionLevel.Normal);

        if (NexusApi.IsAuthenticated)
            saved &= GlobalData.ConfigManager.Set("nexusApiKey", txtApiKey.Text);
        else
            saved &= GlobalData.ConfigManager.Set("nexusApiKey", string.Empty);

        if (!chkNewFileEveryLaunch.Checked)
            saved &= GlobalData.ConfigManager.Set("includeDateTime", false);
        else
            saved &= GlobalData.ConfigManager.Set("includeDateTime", true);

        if (!lblCurrentLogPath.Text.Equals(Strings.NoLogPathSet))
            saved &= GlobalData.ConfigManager.Set("logFileDirectory", lblCurrentLogPath.Text);

        //TODO: set the values for the forbidden keywords
        List<ForbiddenKeyword> updatedKeywords = [];
        foreach (ListViewItem item in lvForbiddenPaths.Items)
        {
            if (item.Tag is ForbiddenKeyword keyword)
                updatedKeywords.Add(keyword);
        }

        saved &= GlobalData.ConfigManager.Set("forbiddenKeywords", updatedKeywords);

        return saved && GlobalData.ConfigManager.Save();
    }

    /// <summary>
    ///     Handles the Click event of the btnEditDefaultSongLocation control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void BtnEditDefaultSongLocation_Click(object sender, EventArgs e)
    {
        fldrOpenDefaultMusicPath.Description = Strings.SelectDefaultSongLocationDesc;
        fldrOpenDefaultMusicPath.SelectedPath = lblDefaultSongLocation.Text;

        if (fldrOpenDefaultMusicPath.ShowDialog(this) == DialogResult.OK)
            lblDefaultSongLocation.Text = fldrOpenDefaultMusicPath.SelectedPath;
    }

    /// <summary>
    ///     Handles the Click event of the btnEditPaths control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void BtnEditPaths_Click(object sender, EventArgs e)
    {
        var pathDialog = new PathSettings();
        pathDialog.GameBasePathChanged += (_, _) => GamePathChanged?.Invoke(this, EventArgs.Empty);
        pathDialog.StagingPathChanged += (_, _) => StagingPathChanged?.Invoke(this, EventArgs.Empty);
        pathDialog.ShowDialog(this);
    }

    /// <summary>
    ///     Handles the Click event of the btnEditLogsPath control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void BtnEditLogsPath_Click(object sender, EventArgs e)
    {
        fldrOpenLogPath.Description = Strings.SelectLogPathDesc;
        fldrOpenLogPath.SelectedPath = lblCurrentLogPath.Text;

        if (fldrOpenLogPath.ShowDialog() == DialogResult.OK)
            lblCurrentLogPath.Text = fldrOpenLogPath.SelectedPath;
    }

    private void TxtApiKey_TextChanged(object sender, EventArgs e)
    {
        //SetApiAuthStatus(); //TODO: Re-enable this line when the feature is fully implemented.
    }

    private async void BtnAuthenticate_Click(object sender, EventArgs e)
    {
        _ = await NexusApi.AuthenticateApiKey(txtApiKey.Text);

        if (NexusApi.IsAuthenticated)
        {
            picApiStatus.Image = Resources.enabled__16x16;
            lblAuthenticatedStatus.Text = Strings.ApiAuthenticated;
            lblAuthenticatedStatus.ForeColor = Color.DarkGreen;
            AuLogger.GetCurrentLogger<ConfigForm>("AuthenticateApi").Info("Successfully authenticated with NexusMods!");
        }
        else
        {
            picApiStatus.Image = Resources.disabled__16x16;
            lblAuthenticatedStatus.Text = Strings.ApiNotAuthenticated;
            lblAuthenticatedStatus.ForeColor = Color.Red;
            AuLogger.GetCurrentLogger<ConfigForm>("AuthenticateApi").Error("Could not authenticate API key.");
        }

        btnAuthenticate.Enabled = !NexusApi.IsAuthenticated;
    }

    private void BtnClearApiKey_Click(object sender, EventArgs e)
    {
        NexusApi.ClearAuthentication();
        txtApiKey.Text = string.Empty;
        //SetApiAuthStatus(); //TODO: Re-enable this line when the feature is fully implemented.
    }

    /// <summary>
    ///     Handles the Click event of the btnSaveAndClose control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void BtnSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SetConfig())
        {
            ConfigSaved?.Invoke(this, EventArgs.Empty);
            var text = Strings.ConfigSaveSuccess;
            var caption = Strings.Success;
            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
        else
        {
            var text = Strings.ConfigSaveError;
            var caption = Strings.Error;
            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            AuLogger.GetCurrentLogger<ConfigForm>("SaveAndClose").Error("Could not save configuration file.");
        }
    }

    /// <summary>
    ///     Handles the Click event of the btnResetToDefault control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void BtnResetToDefault_Click(object sender, EventArgs e)
    {
        var text = Strings.ConfigResetConfirm;
        var caption = Strings.Confirm;

        var result = MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result != DialogResult.Yes) return;

        ResetConfig();
    }

    /// <summary>
    /// Checks if there are unsaved changes to the API key.
    /// </summary>
    /// <returns><c>true</c> if there are no unsaved changes; <c>false</c> otherwise.</returns>
    private bool NoUnsavedApiChanges()
    {
        return true; //TODO: Remove this line when the feature is fully implemented and uncomment the code below.
        //var isSameKey = txtApiKey.Text.Equals(GlobalData.ConfigManager.Get("nexusApiKey") as string);
        //if (isSameKey || !NexusApi.IsAuthenticated) return true;

        //var text = Strings.ApiUnsavedChanges 
        //           ?? "You have unsaved changes to your API key. Please save or clear the key before closing.";
        //var caption = Strings.UnsavedChanges ?? "Unsaved Changes";
        //MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

        //return false;
    }

    /// <summary>
    ///     Handles the Click event of the btnCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (NoUnsavedApiChanges())
            Close();
    }

    private void LnkNexusApiKeyPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        @"https://next.nexusmods.com/settings/api-keys#:~:text=Request%20Api%20Key-,Personal%20API%20Key,-If%20you%20are"
            .OpenUrl();
    }

    private void LnkApiAcceptableUse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        @"https://help.nexusmods.com/article/114-api-acceptable-use-policy".OpenUrl();
    }

    private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (e.CloseReason is CloseReason.WindowsShutDown or CloseReason.TaskManagerClosing) return;

        if (!NoUnsavedApiChanges()) e.Cancel = true;
    }

    private void ControlMouseEnter(object sender, EventArgs e)
    {
        this.SafeInvoke(() =>
        {
            if (sender is not Control hoveredControl) return;

            var helpKey = hoveredControl.Tag as string ?? string.Empty;
            if (!string.IsNullOrEmpty(helpKey))
                lblHelpText.Text = GlobalData.Strings.GetString(helpKey);
        });
    }

    private void ControlMouseLeave(object sender, EventArgs e)
    {
        this.SafeInvoke(() => lblHelpText.Text = Strings.Ready);
    }
}