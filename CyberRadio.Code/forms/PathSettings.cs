using AetherUtils.Core.Reflection;
using RadioExt_Helper.Properties;
using RadioExt_Helper.user_controls;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

public partial class PathSettings : Form
{
    private bool _stageFolderChanged = false;
    private bool _gameFolderChanged = false;

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
        lblGameBasePath.Text = !Settings.Default.GameBasePath.Equals(string.Empty) ?
            Settings.Default.GameBasePath : GlobalData.Strings.GetString("GameBasePathPlaceholder");

        lblBackupPath.Text = !Settings.Default.StagingPath.Equals(string.Empty) ?
            Settings.Default.StagingPath : GlobalData.Strings.GetString("StagingPathPlaceholder");

        var radioPath = PathHelper.GetRadioExtPath(Settings.Default.GameBasePath);
        lblRadioPath.Text = radioPath.Equals(string.Empty)
            ? GlobalData.Strings.GetString("RadioExtPathPlaceholder")
            : radioPath;
    }

    private void btnChangeGameBasePath_Click(object sender, EventArgs e)
    {
        var basePath = PathHelper.GetGamePath(false);
        if (basePath == null || basePath.Equals(string.Empty)) return;

        if (!Settings.Default.GameBasePath.Equals(basePath))
            _gameFolderChanged = true;

        Settings.Default.GameBasePath = basePath;
        Settings.Default.Save();
        SetLabels();
    }

    private void btnChangeBackUpPath_Click(object sender, EventArgs e)
    {
        var stagingPath = PathHelper.GetStagingPath(false);
        if (stagingPath.Equals(string.Empty) || stagingPath.Equals(Settings.Default.StagingPath)) return;

        if (!Settings.Default.StagingPath.Equals(stagingPath))
            _stageFolderChanged = true;

        Settings.Default.StagingPath = stagingPath;
        Settings.Default.Save();
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