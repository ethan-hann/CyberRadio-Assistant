using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms
{
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
            //ApplyFonts();
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
            if (!Settings.Default.GameBasePath.Equals(string.Empty))
                lblGameBasePath.Text = Settings.Default.GameBasePath;
            else
                lblGameBasePath.Text = GlobalData.Strings.GetString("GameBasePathPlaceholder");

            if (!Settings.Default.BackupPath.Equals(string.Empty))
                lblBackupPath.Text = Settings.Default.BackupPath;
            else
                lblBackupPath.Text = GlobalData.Strings.GetString("BackupPathPlaceholder");

            var radioPath = PathHelper.GetRadioExtPath(Settings.Default.GameBasePath);
            lblRadioPath.Text = radioPath.Equals(string.Empty) ? 
                GlobalData.Strings.GetString("RadioExtPathPlaceholder") : radioPath;
        }

        private void ApplyFonts()
        {
            //FontLoader.Initialize();
            //foreach (Control control in Controls)
            //{
            //    FontLoader.ApplyCustomFont(control, 11);
            //}
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
}
