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

using AetherUtils.Core.Logging;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

/// <summary>
///     Represents a configuration form.
/// </summary>
public partial class ConfigForm : Form
{
    private readonly ImageList _tabImages = new();

    /// <summary>
    ///     Initializes a new instance of the <see cref="ConfigForm" /> class.
    /// </summary>
    public ConfigForm()
    {
        InitializeComponent();
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
        tabConfigs.ImageList = _tabImages;
        tabGeneral.ImageKey = @"general";
        tabLogging.ImageKey = @"logging";

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

        //Set the values from the current config for the paths to the default config
        defaultConfig.StagingPath = currentConfig.StagingPath;
        defaultConfig.GameBasePath = currentConfig.GameBasePath;

        //Finally, save the configuration changes and set the values on the form to use the new, default config
        GlobalData.ConfigManager.Save();
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
        new PathSettings().ShowDialog();
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

    /// <summary>
    ///     Handles the Click event of the btnSaveAndClose control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void BtnSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SetConfig())
        {
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
    ///     Handles the Click event of the btnCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}