using AetherUtils.Core.Logging;
using RadioExt_Helper.config;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

public partial class ConfigForm : Form
{
    private readonly ImageList _tabImages = new();

    public ConfigForm()
    {
        InitializeComponent();
    }

    private void ConfigForm_Load(object sender, EventArgs e)
    {
        _tabImages.Images.Add("general", Properties.Resources.settings__16x16);
        _tabImages.Images.Add("logging", Properties.Resources.log__16x16);
        tabConfigs.ImageList = _tabImages;
        tabGeneral.ImageKey = "general";
        tabLogging.ImageKey = "logging";

        Translate();
        SetValues();
    }

    /// <summary>
    /// Translates the text of the form and its controls based on the language settings.
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
    /// Set the values in the form to the configuration values.
    /// </summary>
    private void SetValues()
    {
        var config = GlobalData.ConfigManager.GetConfig();
        if (config == null) return;

        chkCheckForUpdates.Checked = config.AutomaticallyCheckForUpdates;
        chkAutoExportToGame.Checked = config.AutoExportToGame;
        chkNewFileEveryLaunch.Checked = config.LogOptions.NewFileEveryLaunch;

        lblCurrentLogPath.Text = config.LogOptions.LogFileDirectory == string.Empty 
            ? lblCurrentLogPath.Text : config.LogOptions.LogFileDirectory;
    }

    /// <summary>
    /// Resets the configuration to the default values.
    /// </summary>
    private void ResetConfig()
    {
        var currentConfig = GlobalData.ConfigManager.GetConfig();
        if (currentConfig == null) return;

        //Create a new default config
        if (GlobalData.ConfigManager.CreateDefaultConfig())
        {
            var defaultConfig = GlobalData.ConfigManager.GetConfig();
            if (defaultConfig == null) return;

            //Set the values from the current config for the paths to the default config
            defaultConfig.StagingPath = currentConfig.StagingPath;
            defaultConfig.GameBasePath = currentConfig.GameBasePath;

            //Finally, save the configuration changes and set the values on the form to use the new, default config
            GlobalData.ConfigManager.SaveAsync();
            SetValues();
        }
    }

    /// <summary>
    /// Sets the configuration values based on the form inputs.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. The task result is a boolean indicating whether the configuration was successfully saved.</returns>
    private async Task<bool> SetConfig()
    {
        GlobalData.ConfigManager.Set("automaticallyCheckForUpdates", chkCheckForUpdates.Checked);
        GlobalData.ConfigManager.Set("autoExportToGame", chkAutoExportToGame.Checked);
        GlobalData.ConfigManager.Set("newFileEveryLaunch", chkNewFileEveryLaunch.Checked);

        if (!lblCurrentLogPath.Text.Equals(GlobalData.Strings.GetString("NoLogPathSet")))
            GlobalData.ConfigManager.Set("logFileDirectory", lblCurrentLogPath.Text);

        return await GlobalData.ConfigManager.SaveAsync();
    }

    private void btnEditPaths_Click(object sender, EventArgs e)
    {
        new PathSettings().ShowDialog();
    }

    private void btnEditLogsPath_Click(object sender, EventArgs e)
    {
        fldrOpenLogPath.Description = GlobalData.Strings.GetString("SelectLogPathDesc") ?? "Select the location to store log files";
        fldrOpenLogPath.SelectedPath = lblCurrentLogPath.Text;

        if (fldrOpenLogPath.ShowDialog() == DialogResult.OK)
            lblCurrentLogPath.Text = fldrOpenLogPath.SelectedPath;
    }

    private void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SetConfig().Result)
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

    private void btnResetToDefault_Click(object sender, EventArgs e)
    {
        var text = GlobalData.Strings.GetString("ConfigResetConfirm");
        var caption = GlobalData.Strings.GetString("Confirm");

        var result = MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result != DialogResult.Yes) return;

        ResetConfig();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}