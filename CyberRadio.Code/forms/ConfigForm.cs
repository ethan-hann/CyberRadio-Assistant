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
using RadioExt_Helper.nexus_api;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

/// <summary>
///     Represents a configuration form.
/// </summary>
public partial class ConfigForm : Form
{
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

    private readonly ImageList _tabImages = new();

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
    ///     Handles the Load event of the ConfigForm control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void ConfigForm_Load(object sender, EventArgs e)
    {
        _tabImages.Images.Add("general", Resources.settings__16x16);
        _tabImages.Images.Add("logging", Resources.log__16x16);
        _tabImages.Images.Add("nexus_api", Resources.api_16x16);
        tabConfigs.ImageList = _tabImages;

        tabGeneral.ImageKey = @"general";
        tabLogging.ImageKey = @"logging";
        tabNexus.ImageKey = @"nexus_api";

        tabConfigs.TabPages.Remove(tabNexus); //TODO: Disable the Nexus API tab for now. Enable when feature is implemented to download mods.

        Translate();
        SetValues();
    }

    /// <summary>
    ///     Translates the text of the form and its controls based on the language settings.
    /// </summary>
    private void Translate()
    {
        Text = GlobalData.Strings.GetString("Configuration");

        //Tabs
        tabGeneral.Text = GlobalData.Strings.GetString("GeneralSettings");
        tabLogging.Text = GlobalData.Strings.GetString("LogSettings");

        //General Tab
        chkCheckForUpdates.Text = GlobalData.Strings.GetString("CheckForUpdatesOption");
        chkAutoExportToGame.Text = GlobalData.Strings.GetString("AutoExportOption");
        btnEditPaths.Text = GlobalData.Strings.GetString("EditPathsOption");
        lblUpdatesHelp.Text = GlobalData.Strings.GetString("CheckForUpdatesOptionHelp");
        lblAutoExportHelp.Text = GlobalData.Strings.GetString("AutoExportOptionHelp");
        lblEditPathsHelp.Text = GlobalData.Strings.GetString("EditPathsOptionHelp");
        lblLogPathLabel.Text = GlobalData.Strings.GetString("LogPathLabel");
        lblCurrentLogPath.Text = GlobalData.Strings.GetString("NoLogPathSet");

        //Logging Tab
        chkNewFileEveryLaunch.Text = GlobalData.Strings.GetString("NewLogFileOption");
        lblNewFileEveryLaunchHelp.Text = GlobalData.Strings.GetString("NewLogFileOptionHelp");
        btnEditLogsPath.Text = GlobalData.Strings.GetString("EditLogsPathOption");
        lblEditLogPathHelp.Text = GlobalData.Strings.GetString("EditLogsPathOptionHelp");

        //Buttons
        btnSaveAndClose.Text = GlobalData.Strings.GetString("SaveAndClose");
        btnResetToDefault.Text = GlobalData.Strings.GetString("ResetToDefaults");
        btnCancel.Text = GlobalData.Strings.GetString("Cancel");

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

        lblCurrentLogPath.Text = config.LogOptions.LogFileDirectory == string.Empty
            ? lblCurrentLogPath.Text
            : config.LogOptions.LogFileDirectory;

        txtApiKey.Text = config.NexusApiKey;

        //SetApiAuthStatus(); //TODO: Re-enable this line when the feature is fully implemented.
    }

    /// <summary>
    /// Set the status of the API authentication on the UI.
    /// </summary>
    private void SetApiAuthStatus()
    {
        btnClearApiKey.Enabled = txtApiKey.Text.Length > 0;

        var isSameKey = txtApiKey.Text.Equals(GlobalData.ConfigManager.Get("nexusApiKey") as string);

        var authText = GlobalData.Strings.GetString("ApiAuthenticated") ?? "Authenticated";
        var notAuthText = GlobalData.Strings.GetString("ApiNotAuthenticated") ?? "Not Authenticated";

        lblAuthenticatedStatus.Text = isSameKey && !txtApiKey.Text.Equals(string.Empty) ? authText: notAuthText;
        picApiStatus.Image = isSameKey && !txtApiKey.Text.Equals(string.Empty) ? Resources.enabled__16x16 : Resources.disabled__16x16;
        lblAuthenticatedStatus.ForeColor = isSameKey && !txtApiKey.Text.Equals(string.Empty) ? Color.DarkGreen : Color.Red;
        btnAuthenticate.Enabled = !isSameKey && !txtApiKey.Text.Equals(string.Empty);
    }

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

        if (NexusApi.IsAuthenticated)
            saved &= GlobalData.ConfigManager.Set("nexusApiKey", txtApiKey.Text);
        else
            saved &= GlobalData.ConfigManager.Set("nexusApiKey", string.Empty);

        if (!chkNewFileEveryLaunch.Checked)
            saved &= GlobalData.ConfigManager.Set("includeDateTime", false);
        else
            saved &= GlobalData.ConfigManager.Set("includeDateTime", true);

        if (!lblCurrentLogPath.Text.Equals(GlobalData.Strings.GetString("NoLogPathSet")))
            saved &= GlobalData.ConfigManager.Set("logFileDirectory", lblCurrentLogPath.Text);

        return saved && GlobalData.ConfigManager.Save();
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
        fldrOpenLogPath.Description = GlobalData.Strings.GetString("SelectLogPathDesc") ??
                                      "Select the location to store log files";
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
            lblAuthenticatedStatus.Text = GlobalData.Strings.GetString("ApiAuthenticated") ?? "Authenticated";
            lblAuthenticatedStatus.ForeColor = Color.DarkGreen;
            AuLogger.GetCurrentLogger<ConfigForm>("AuthenticateApi").Info("Successfully authenticated with NexusMods!");
        }
        else
        {
            picApiStatus.Image = Resources.disabled__16x16;
            lblAuthenticatedStatus.Text = GlobalData.Strings.GetString("ApiNotAuthenticated") ?? "Not Authenticated";
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
            var text = GlobalData.Strings.GetString("ConfigSaveSuccess");
            var caption = GlobalData.Strings.GetString("Success");
            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
        else
        {
            var text = GlobalData.Strings.GetString("ConfigSaveError");
            var caption = GlobalData.Strings.GetString("Error");
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
        var text = GlobalData.Strings.GetString("ConfigResetConfirm");
        var caption = GlobalData.Strings.GetString("Confirm");

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

        //var text = GlobalData.Strings.GetString("ApiUnsavedChanges") 
        //           ?? "You have unsaved changes to your API key. Please save or clear the key before closing.";
        //var caption = GlobalData.Strings.GetString("UnsavedChanges") ?? "Unsaved Changes";
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
        @"https://next.nexusmods.com/settings/api-keys#:~:text=Request%20Api%20Key-,Personal%20API%20Key,-If%20you%20are".OpenUrl();
    }

    private void LnkApiAcceptableUse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        @"https://help.nexusmods.com/article/114-api-acceptable-use-policy".OpenUrl();
    }

    private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (e.CloseReason is CloseReason.WindowsShutDown or CloseReason.TaskManagerClosing) return;

        if (!NoUnsavedApiChanges())
        {
            e.Cancel = true;
        }
    }
}