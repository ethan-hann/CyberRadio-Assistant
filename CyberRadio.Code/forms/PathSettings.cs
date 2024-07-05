using AetherUtils.Core.Logging;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

public partial class PathSettings : Form
{
    private bool _gameFolderChanged;
    private bool _stageFolderChanged;
    
    private static string GameBasePath => GlobalData.ConfigManager.Get("gameBasePath") as string ?? string.Empty;
    private static string StagingPath => GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;

    public PathSettings()
    {
        InitializeComponent();
    }

    private void PathSettings_Load(object sender, EventArgs e)
    {
        SetLabels();
        Translate();
    }

    private void Translate()
    {
        Text = GlobalData.Strings.GetString("GamePaths");

        label1.Text = GlobalData.Strings.GetString("GameBasePath");
        label2.Text = GlobalData.Strings.GetString("RadioStationPath");
        label4.Text = GlobalData.Strings.GetString("StagingPath");

        btnChangeGameBasePath.Text = GlobalData.Strings.GetString("Change") + GlobalData.Strings.GetString("DotDotDot");
        btnChangeBackUpPath.Text = GlobalData.Strings.GetString("Change") + GlobalData.Strings.GetString("DotDotDot");

        SetLabels();
    }

    private void SetLabels()
    {
        lblGameBasePath.Text = !GameBasePath.Equals(string.Empty)
            ? GameBasePath
            : GlobalData.Strings.GetString("GameBasePathPlaceholder");

        lblBackupPath.Text = !StagingPath.Equals(string.Empty)
            ? StagingPath
            : GlobalData.Strings.GetString("StagingPathPlaceholder");

        var radioPath = PathHelper.GetRadioExtPath(GameBasePath);
        lblRadioPath.Text = radioPath.Equals(string.Empty)
            ? GlobalData.Strings.GetString("RadioExtPathPlaceholder")
            : radioPath;
    }

    private void btnChangeGameBasePath_Click(object sender, EventArgs e)
    {
        var basePath = PathHelper.GetGamePath();
        if (basePath == null || basePath.Equals(string.Empty)) return;

        if (!GameBasePath.Equals(basePath))
            _gameFolderChanged = true;

        if (GlobalData.ConfigManager.Set("gameBasePath", basePath))
        {
            if (GlobalData.ConfigManager.Save())
                AuLogger.GetCurrentLogger<PathSettings>("ChangeGameBasePath").Info($"Updated game base path: {basePath}");
            else
                AuLogger.GetCurrentLogger<PathSettings>("ChangeGameBasePath").Warn("Couldn't save updated configuration after changing base path.");
        }
        SetLabels();
    }

    private void btnChangeBackUpPath_Click(object sender, EventArgs e)
    {
        var stagingPath = PathHelper.GetStagingPath();
        if (stagingPath.Equals(string.Empty) || stagingPath.Equals(StagingPath)) return;

        if (!StagingPath.Equals(stagingPath))
            _stageFolderChanged = true;

        if (GlobalData.ConfigManager.Set("stagingPath", stagingPath))
        {
            if (GlobalData.ConfigManager.Save())
                AuLogger.GetCurrentLogger<PathSettings>("ChangeStagingPath").Info($"Updated staging path: {stagingPath}");
            else
                AuLogger.GetCurrentLogger<PathSettings>("ChangeStagingPath").Warn("Couldn't save updated configuration after changing staging path.");
        }
        SetLabels();
    }

    private void PathSettings_FormClosed(object sender, FormClosedEventArgs e)
    {
        if (_stageFolderChanged || _gameFolderChanged)
            DialogResult = DialogResult.OK;
        else
            DialogResult = DialogResult.Cancel;
    }
}