using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms
{
    public partial class IconGeneratorForm : Form
    {
        public IconGeneratorForm()
        {
            InitializeComponent();
        }

        private void IconGeneratorForm_Load(object sender, EventArgs e)
        {
            //IconManager.Instance.StatusChanged += IconManager_StatusChanged;
            //IconManager.Instance.ProgressChanged += Instance_ProgressChanged;
        }

        private void Instance_ProgressChanged(int e)
        {
            this.SafeInvoke(() => lblPercent.Text = $"Percent Done: {e}%");
            this.SafeInvoke(() => pbProgress.Value = e);
        }

        private void IconManager_StatusChanged(string e)
        {
            this.SafeInvoke(() =>
            {
                richTextBox1.Text += e + "\n";
            });
        }
        private bool _isGenerating = false;

        private void button1_Click(object sender, EventArgs e)
        {
            if (_isGenerating) return;
            if (bgWorker.IsBusy) return;

            if (textBox1.Text.Equals(string.Empty))
            {
                MessageBox.Show("Please enter the atlas name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (fldrBrowserInput.ShowDialog() == DialogResult.Cancel) return;

            pbProgress.Value = 0;
            if (!bgWorker.IsBusy)
                bgWorker.RunWorkerAsync();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (string.IsNullOrEmpty(fldrBrowserInput.SelectedPath) || string.IsNullOrEmpty(textBox1.Text)) return;
            if (!bgWorker.CancellationPending)
            {
                _isGenerating = true;
                //this.SafeInvoke(() => pictureBox1.Load(Directory.GetFiles(fldrBrowserInput.SelectedPath).FirstOrDefault() ?? string.Empty));
                //e.Result = IconManager.Instance.ArchiveFromPngs(fldrBrowserInput.SelectedPath, textBox1.Text);
            }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblPercent.Text = string.Format("Percent Done: {0}%", e.ProgressPercentage);
            pbProgress.Value = e.ProgressPercentage;
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null) return;
            var result = (Dictionary<string, (string output, string error)>)e.Result;
            this.SafeInvoke(() => richTextBox1.Text += "=======================================\n");
            this.SafeInvoke(() => richTextBox1.Text += "Scripts Output: \n\n");
            foreach (var item in result)
            {
                this.SafeInvoke(() => richTextBox1.Text += string.Concat("\n", item.Key, "\nOutput: ", item.Value.output, "\nError: ", item.Value.error));
            }
            _isGenerating = false;
            pbProgress.Value = 100;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_isGenerating) return;
            if (bgUnpacker.IsBusy) return;

            if (ofdArchiveDialog.ShowDialog() == DialogResult.Cancel) return;

            pbProgress.Value = 0;
            if (!bgUnpacker.IsBusy)
                bgUnpacker.RunWorkerAsync();
        }

        private void bgUnpacker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!bgUnpacker.CancellationPending)
            {
                var result = new Dictionary<string, (string output, string error)>();
                //result.Add("Unpack", IconManager.Instance.UnpackArchive(ofdArchiveDialog.FileName, Path.Combine(IconManager.Instance.WorkingDirectory, "unpackTest")));
                var unpackedDir = Path.Combine(IconManager.Instance.WorkingDirectory, "unpackTest");
                var xmbFiles = Directory.GetFiles(unpackedDir, "*.xbm", SearchOption.AllDirectories);
                //result.Add("Convert", IconManager.Instance.ExportPng(xmbFiles.First(), unpackedDir));

                var pngFiles = Directory.GetFiles(unpackedDir).Where(file => Path.GetExtension(file).Equals(".png"));
                this.SafeInvoke(() => pictureBox1.Load(pngFiles.First()));
            }
        }

        private void bgUnpacker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblPercent.Text = string.Format("Percent Done: {0}%", e.ProgressPercentage);
            pbProgress.Value = e.ProgressPercentage;
        }

        private void bgUnpacker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null) return;
            var result = (Dictionary<string, (string output, string error)>)e.Result;
            this.SafeInvoke(() => richTextBox1.Text += "=======================================\n");
            this.SafeInvoke(() => richTextBox1.Text += "Scripts Output: \n\n");
            foreach (var item in result)
            {
                this.SafeInvoke(() => richTextBox1.Text += string.Concat("\n", item.Key, "\nOutput: ", item.Value.output, "\nError: ", item.Value.error));
            }
            pbProgress.Value = 100;
        }
    }
}
