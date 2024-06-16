using AetherUtils.Core.Extensions;
using AetherUtils.Core.Reflection;
using RadioExt_Helper.models;
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
using static System.Collections.Specialized.BitVector32;

namespace RadioExt_Helper.forms
{
    public partial class ExportWindow : Form
    {
        private readonly List<Station> _stationsToExport;
        private bool isCancelling = false;
        private bool exportToStagingComplete = false;

        public ExportWindow(List<Station> stations)
        {
            InitializeComponent();
            _stationsToExport = stations;
        }

        private void ExportWindow_Load(object sender, EventArgs e)
        {
            ApplyFonts(this);
            Translate();
            PopulateListView();
        }

        private void Translate()
        {
            GlobalData.SetCulture(Settings.Default.SelectedLanguage);

            Text = GlobalData.Strings.GetString("Export");
            lblConfirm.Text = GlobalData.Strings.GetString("ExportHelp");
            btnCancel.Text = GlobalData.Strings.GetString("Cancel");
            btnExportToGame.Text = GlobalData.Strings.GetString("ExportToGame");
            btnExportToStaging.Text = GlobalData.Strings.GetString("ExportToStaging");
            lblStatus.Text = GlobalData.Strings.GetString("Ready");

            //ListView Translations
            lvStations.Columns[0].Text = GlobalData.Strings.GetString("LVDisplayName");
            lvStations.Columns[1].Text = GlobalData.Strings.GetString("LVStationIcon");
            lvStations.Columns[2].Text = GlobalData.Strings.GetString("LVSongCount");
            lvStations.Columns[3].Text = GlobalData.Strings.GetString("LVStreamURL");
            lvStations.Columns[4].Text = GlobalData.Strings.GetString("LVProposedPath");
        }

        private void ApplyFonts(Control control)
        {
            switch (control)
            {
                case MenuStrip:
                case GroupBox:
                case Button:
                    FontHandler.Instance.ApplyFont(control, "CyberPunk_Regular", 10, FontStyle.Bold);
                    break;
                case Label:
                    FontHandler.Instance.ApplyFont(control, "CyberPunk_Regular", 12, FontStyle.Bold);
                    break;
                //case ListView:
                //    FontHandler.Instance.ApplyFont(control, "CyberPunk_Regular", 10, FontStyle.Bold);
                //    break;
            }

            foreach (Control child in control.Controls)
                ApplyFonts(child);
        }

        private void PopulateListView()
        {
            var radioExtPath = PathHelper.GetRadiosPath(Settings.Default.GameBasePath);

            foreach (Station s in _stationsToExport)
            {
                var customIconString = s.CustomIcon.UseCustom ? GlobalData.Strings.GetString("CustomIcon") : s.MetaData.Icon;
                var songString = s.MetaData.StreamInfo.IsStream ? GlobalData.Strings.GetString("IsStream") : s.Songs.Count.ToString();
                var streamString = s.MetaData.StreamInfo.IsStream ? s.MetaData.StreamInfo.StreamUrl : GlobalData.Strings.GetString("UsingSongs");
                var proposedPath = Path.Combine(radioExtPath, s.MetaData.DisplayName);

                ListViewItem lvItem = new ListViewItem(new string[]
                {
                    s.MetaData.DisplayName,
                    customIconString ?? "",
                    songString ?? "",
                    streamString ?? "",
                    proposedPath
                })
                {
                    Tag = s
                };

                lvStations.Items.Add(lvItem);
            }

            lvStations.ResizeColumns();
        }

        private void btnExportToStaging_Click(object sender, EventArgs e)
        {
            if (!bgWorkerExport.CancellationPending && !bgWorkerExport.IsBusy)
                bgWorkerExport.RunWorkerAsync();
        }

        private void btnExportToGame_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!bgWorkerExport.CancellationPending && bgWorkerExport.IsBusy)
            {
                isCancelling = true;
                bgWorkerExport.CancelAsync();
            }
        }

        private void Reset()
        {
            ToggleButtons();
            //btnExportToStaging.Enabled = true;
            UpdateStatus(GlobalData.Strings.GetString("ExportCanceled"));
            //btnCancel.Enabled = false;
            pgExportProgress.Value = 0;

            isCancelling = false;
            exportToStagingComplete = false;
        }

        private void bgWorkerExport_DoWork(object sender, DoWorkEventArgs e)
        {
            ToggleButtons();
            for (int i = 0; i < 10000; i++)
            {
                if (bgWorkerExport.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                var statusString = GlobalData.Strings.GetString("ExportingStationStatus");
                UpdateStatus(string.Format(statusString, i));
                //UpdateStatus($"Exporting station {i}");
                int progressPercentage = (int)((i / (float)10000) * 100);
                bgWorkerExport.ReportProgress(progressPercentage);
            }
        }

        private void bgWorkerExport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pgExportProgress.Value = e.ProgressPercentage;
        }

        private void bgWorkerExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isCancelling)
            {
                Reset();
            }
            else
            {
                exportToStagingComplete = true;
                pgExportProgress.Value = 100;
                ToggleButtons();
                UpdateStatus(GlobalData.Strings.GetString("ExportCompleteStatus"));
            }
        }

        private void ToggleButtons()
        {
            if (InvokeRequired)
            {
                Invoke(() =>
                {
                    if (isCancelling)
                    {
                        btnExportToGame.Enabled = false;
                        btnExportToStaging.Enabled = true;
                        btnCancel.Visible = false;
                    }
                    else if (exportToStagingComplete)
                    {
                        btnExportToGame.Enabled = true;
                        btnExportToStaging.Enabled = false;
                        btnCancel.Visible = false;
                    }
                    else
                    {
                        btnExportToGame.Enabled = false;
                        btnExportToStaging.Enabled = false;
                        btnCancel.Visible = true;
                    }
                });
            }
            else
            {
                if (isCancelling)
                {
                    btnExportToGame.Enabled = false;
                    btnExportToStaging.Enabled = true;
                    btnCancel.Visible = false;
                }
                else if (exportToStagingComplete)
                {
                    btnExportToGame.Enabled = true;
                    btnExportToStaging.Enabled = false;
                    btnCancel.Visible = false;
                }
            }
        }

        private void UpdateStatus(string? status)
        {
            if (status == null) return;

            if (InvokeRequired)
            {
                Invoke(() =>
                {
                    lblStatus.Text = status;
                });
            }
            else
                lblStatus.Text = status;
        }
    }
}
