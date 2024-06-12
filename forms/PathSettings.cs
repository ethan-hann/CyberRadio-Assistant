using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadioExt_Helper.forms
{
    public partial class PathSettings : Form
    {
        private readonly string _radioExtPlaceholder = "<no path set; radioExt is not installed>";
        private readonly string _backupPlaceholder = "<no path set; radio stations will NOT be backed up>";

        public PathSettings()
        {
            InitializeComponent();
        }

        private void btnChangeGameBasePath_Click(object sender, EventArgs e)
        {
            string basePath = PathHelper.GetGamePath(fdlgOpenGameExe, false);
            if (!basePath.Equals(string.Empty))
            {
                Settings.Default.GameBasePath = basePath;
                Settings.Default.Save();
            }
        }

        private void PathSettings_Load(object sender, EventArgs e)
        {
            SetLabels();
            //ApplyFonts();
        }

        private void SetLabels()
        {
            if (!Settings.Default.GameBasePath.Equals(string.Empty))
                lblGameBasePath.Text = Settings.Default.GameBasePath;

            if (!Settings.Default.BackupPath.Equals(string.Empty))
                lblBackupPath.Text = Settings.Default.BackupPath;

            string radioPath = PathHelper.GetRadioExtPath(Settings.Default.GameBasePath);
            if (radioPath.Equals(string.Empty))
                lblRadioPath.Text = _radioExtPlaceholder;
            else
                lblRadioPath.Text = radioPath;
        }

        private void ApplyFonts()
        {
            FontLoader.Initialize();
            foreach (Control control in Controls)
            {
                FontLoader.ApplyCustomFont(control, 11);
            }
        }

        private void btnChangeBackUpPath_Click(object sender, EventArgs e)
        {
            if (fdlgBackupPath.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.BackupPath = fdlgBackupPath.SelectedPath;
                Settings.Default.Save();
                lblBackupPath.Text = fdlgBackupPath.SelectedPath;
            }
        }

        private void btnClearBackupPath_Click(object sender, EventArgs e)
        {
            if (lblBackupPath.Text.Equals(_backupPlaceholder)) return;

            if (MessageBox.Show(this, "Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Settings.Default.BackupPath = string.Empty;
                Settings.Default.Save();
                lblBackupPath.Text = _backupPlaceholder;
            }
        }
    }
}
