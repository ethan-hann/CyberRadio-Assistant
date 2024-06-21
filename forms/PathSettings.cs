using AetherUtils.Core.Reflection;
using RadioExt_Helper.Properties;
using RadioExt_Helper.user_controls;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

public partial class PathSettings : Form
{
    public PathSettings()
    {
        InitializeComponent();
    }

    private void btnChangeGameBasePath_Click(object sender, EventArgs e)
    {
        var basePath = PathHelper.GetGamePath(fdlgOpenGameExe);
        if (basePath != null && basePath.Equals(string.Empty)) return;

        Settings.Default.GameBasePath = basePath;
        Settings.Default.Save();
    }

    private void PathSettings_Load(object sender, EventArgs e)
    {
        SetLabels();
        ApplyFontsToControls(this);
        Translate();
    }

    private void Translate()
    {
        Text = GlobalData.Strings.GetString("GamePaths");

        label1.Text = GlobalData.Strings.GetString("GameBasePath");
        label2.Text = GlobalData.Strings.GetString("RadioStationPath");
        label4.Text = GlobalData.Strings.GetString("BackUpPath");

        btnChangeGameBasePath.Text = GlobalData.Strings.GetString("Change") + GlobalData.Strings.GetString("DotDotDot");
        btnChangeBackUpPath.Text = GlobalData.Strings.GetString("Change") + GlobalData.Strings.GetString("DotDotDot");
        btnClearBackupPath.Text = GlobalData.Strings.GetString("Clear");

        fdlgBackupPath.Description = GlobalData.Strings.GetString("BackupPathHelp") ?? string.Empty;
        fdlgOpenGameExe.Title = GlobalData.Strings.GetString("Open");

        SetLabels();
    }

    private void SetLabels()
    {
        lblGameBasePath.Text = !Settings.Default.GameBasePath.Equals(string.Empty) ? 
            Settings.Default.GameBasePath : GlobalData.Strings.GetString("GameBasePathPlaceholder");

        lblBackupPath.Text = !Settings.Default.BackupPath.Equals(string.Empty) ? 
            Settings.Default.BackupPath : GlobalData.Strings.GetString("BackupPathPlaceholder");

        var radioPath = PathHelper.GetRadioExtPath(Settings.Default.GameBasePath);
        lblRadioPath.Text = radioPath.Equals(string.Empty)
            ? GlobalData.Strings.GetString("RadioExtPathPlaceholder")
            : radioPath;
    }
    
    private static void ApplyFontsToControls(Control control)
    {
        if (control is IUserControl userControl)
        {
            userControl.ApplyFonts();
        }
        else
        {
            switch (control)
            {
                case MenuStrip:
                case GroupBox:
                case Button:
                    FontHandler.Instance.ApplyFont(control, "CyberPunk_Regular", 9, FontStyle.Bold);
                    break;
                case TabControl:
                    FontHandler.Instance.ApplyFont(control, "CyberPunk_Regular", 12, FontStyle.Bold);
                    break;
                case Label:
                    FontHandler.Instance.ApplyFont(control, "CyberPunk_Regular", 9, FontStyle.Regular);
                    break;
            }

            foreach (Control child in control.Controls)
                ApplyFontsToControls(child);
        }
    }

    private void btnChangeBackUpPath_Click(object sender, EventArgs e)
    {
        if (fdlgBackupPath.ShowDialog() != DialogResult.OK) return;

        Settings.Default.BackupPath = fdlgBackupPath.SelectedPath;
        Settings.Default.Save();
        lblBackupPath.Text = fdlgBackupPath.SelectedPath;
    }

    private void btnClearBackupPath_Click(object sender, EventArgs e)
    {
        if (lblBackupPath.Text.Equals(GlobalData.Strings.GetString("BackupPathPlaceholder"))) return;

        var text = GlobalData.Strings.GetString("AreYouSure");
        var caption = GlobalData.Strings.GetString("Confirm");
        if (MessageBox.Show(this, text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) !=
            DialogResult.Yes) return;

        Settings.Default.BackupPath = string.Empty;
        Settings.Default.Save();
        SetLabels();
    }
}