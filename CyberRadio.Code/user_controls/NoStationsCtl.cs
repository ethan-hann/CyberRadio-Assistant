using RadioExt_Helper.forms;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.user_controls;

public sealed partial class NoStationsCtl : UserControl, IUserControl
{
    public NoStationsCtl()
    {
        InitializeComponent();
        Dock = DockStyle.Fill;
    }

    public Station Station => new();

    public void Translate()
    {
        lblNoStations.Text = GlobalData.Strings.GetString("NoStationsYet");
        lblNoGamePath.Text = GlobalData.Strings.GetString("NoExeFound");
        lblNoStagingPath.Text = GlobalData.Strings.GetString("NoStagingPathFound");

        btnPaths.Text = GlobalData.Strings.GetString("Paths");
        btnPaths.Text = GlobalData.Strings.GetString("Paths");
    }

    public event EventHandler? PathsSet;

    private void NoStationsCtl_Load(object sender, EventArgs e)
    {
        Translate();
        CheckPaths();
    }

    private void CheckPaths()
    {
        var showNoGamePath = false;
        var showNoStagePath = false;

        var gameBasePath = GlobalData.ConfigManager.Get("gameBasePath") as string ?? string.Empty;
        var stagingPath = GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;

        if (gameBasePath.Equals(string.Empty))
            showNoGamePath = true;

        if (stagingPath.Equals(string.Empty))
            showNoStagePath = true;

        ToggleControls(showNoGamePath, showNoStagePath);
    }

    private void ToggleControls(bool gamePath, bool stagePath)
    {
        tlpNoGamePath.Visible = gamePath;
        tlpNoStagingPath.Visible = stagePath;

        //Trigger population of stations
        if (!tlpNoGamePath.Visible && !tlpNoStagingPath.Visible)
            PathsSet?.Invoke(this, EventArgs.Empty);
    }

    private void btnPaths_Click(object sender, EventArgs e)
    {
        var result = new PathSettings().ShowDialog();

        if (result == DialogResult.OK)
            CheckPaths();
    }
}