using RadioExt_Helper.config;
using RadioExt_Helper.utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadioExt_Helper.forms
{
    public partial class ConfigForm : Form
    {
        private CyberConfig? _config;

        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            _config = GlobalData.ConfigManager?.GetConfig();
            _config ??= new();

            Translate();
            SetValues();
        }

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

            //Logging Tab
            chkNewFileEveryLaunch.Text = GlobalData.Strings.GetString("NewLogFileOption");
            lblNewFileEveryLaunchHelp.Text = GlobalData.Strings.GetString("NewLogFileOptionHelp");

            //Buttons
            btnSaveAndClose.Text = GlobalData.Strings.GetString("SaveAndClose");
            btnResetToDefault.Text = GlobalData.Strings.GetString("ResetToDefaults");
            btnCancel.Text = GlobalData.Strings.GetString("Cancel");
        }

        private void SetValues()
        {
            if (_config == null) return;

            chkCheckForUpdates.Checked = _config.AutomaticallyCheckForUpdates;
            chkAutoExportToGame.Checked = _config.AutoExportToGame;
            chkNewFileEveryLaunch.Checked = _config.LogOptions.NewFileEveryLaunch;
        }

        private async Task<bool> SetConfig()
        {
            if (_config == null) return false;

            GlobalData.ConfigManager?.Set("automaticallyCheckForUpdates", _config.AutomaticallyCheckForUpdates);
            GlobalData.ConfigManager?.Set("autoExportToGame", _config.AutoExportToGame);
            GlobalData.ConfigManager?.Set("newFileEveryLaunch", _config.LogOptions.NewFileEveryLaunch);

            return await GlobalData.ConfigManager?.SaveAsync();
        }

        private void chkCheckForUpdates_CheckedChanged(object sender, EventArgs e)
        {
            if (_config == null) return;

            _config.AutomaticallyCheckForUpdates = chkCheckForUpdates.Checked;
        }

        private void chkAutoExportToGame_CheckedChanged(object sender, EventArgs e)
        {
            if (_config == null) return;

            _config.AutoExportToGame = chkAutoExportToGame.Checked;
        }

        private void btnEditPaths_Click(object sender, EventArgs e) => new PathSettings().ShowDialog();

        private void chkNewFileEveryLaunch_CheckedChanged(object sender, EventArgs e)
        {
            if (_config == null) return;

            _config.LogOptions.NewFileEveryLaunch = chkNewFileEveryLaunch.Checked;
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
                Debug.WriteLine("Error saving config");
            }
        }

        private void btnResetToDefault_Click(object sender, EventArgs e)
        {
            var text = GlobalData.Strings.GetString("ConfigResetConfirm");
            var caption = GlobalData.Strings.GetString("Confirm");

            var result = MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                _config = new();
                SetValues();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
