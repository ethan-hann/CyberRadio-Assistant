using AetherUtils.Core.Extensions;
using AetherUtils.Core.Logging;
using Org.BouncyCastle.Utilities;
using RadioExt_Helper.models;
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
    public partial class BackupPreview : Form
    {
        private BackupManager _backupManager = new();

        private List<FilePreview> Previews = [];
        private long TotalSize;
        private long EstimatedCompressedSize;

        public BackupPreview()
        {
            InitializeComponent();

            _backupManager.PreviewProgressChanged += _backupManager_PreviewProgressChanged;
            _backupManager.PreviewStatusChanged += _backupManager_PreviewStatusChanged;
            _backupManager.BackupPreviewCompleted += _backupManager_BackupPreviewCompleted;
        }

        public BackupPreview(List<FilePreview> Previews, long TotalSize, long EstimatedCompressedSize) : this()
        {
            this.Previews = Previews;
            this.TotalSize = TotalSize;
            this.EstimatedCompressedSize = EstimatedCompressedSize;
        }

        private void lvFilePreviews_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if the clicked column is already the column that is being sorted.
            if (lvFilePreviews.ListViewItemSorter is ListViewItemComparer sorter && sorter.Column == e.Column)
                // Reverse the current sort direction for this column.
                sorter.Order = sorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            else
                // Set the column number that is to be sorted; default to ascending.
                lvFilePreviews.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Ascending);

            // Perform the sort with these new sort options.
            lvFilePreviews.Sort();
        }

        private void BackupPreview_Load(object sender, EventArgs e)
        {
            var backupManager = new BackupManager();

            pgPreviewProgress.Visible = true;
            lblPreviewStatusLabel.Text = 
                $"Loading preview. Using {GlobalData.ConfigManager.Get("backupCompressionLevel") ?? CompressionLevel.Normal} compression...";

            _ = StartPreviewLoading();
            //var bgTask = Task.Run(async () => 
            //{
            //    var data = await backupManager.GetBackupPreviewAsync(GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty);
            //    Previews = data.Previews;
            //    TotalSize = data.TotalSize;
            //    EstimatedCompressedSize = data.EstimatedCompressedSize;
            //});

            //Debug.WriteLine("Loading preview...");
            //bgTask.Wait();

            //if (bgTask.Status == TaskStatus.RanToCompletion)
            //{
            //    PopulateTreeView();
            //    SetSizeLabels();
            //}
        }

        private async Task StartPreviewLoading()
        {
            try
            {
                await _backupManager.GetBackupPreviewAsync(GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty);
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<BackupPreview>("StartPreviewLoading").Error(ex, "Error loading backup preview.");
                this.SafeInvoke(() =>
                {
                    var result = MessageBox.Show(this, "An error occurred while loading the backup preview.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                });
            }
        }

        private void _backupManager_PreviewProgressChanged(int obj)
        {
            this.SafeInvoke(() => pgPreviewProgress.Value = obj);
        }

        private void _backupManager_PreviewStatusChanged((FilePreview, long) obj)
        {
            Previews.Add(obj.Item1);
            TotalSize += obj.Item1.Size;
            EstimatedCompressedSize = obj.Item2;

            AddItemToListView(obj.Item1);
            SetSizeLabels();
        }

        private void _backupManager_BackupPreviewCompleted((List<FilePreview> Previews, long TotalSize, long EstimatedCompressedSize) obj)
        {
            if (Previews.Count != obj.Previews.Count)
                throw new Exception("Preview data mismatch!");

            if (TotalSize != obj.TotalSize)
                throw new Exception("Total size mismatch!");

            if (EstimatedCompressedSize != obj.EstimatedCompressedSize)
                throw new Exception("Estimated compressed size mismatch!");

            Previews = obj.Previews;
            TotalSize = obj.TotalSize;
            EstimatedCompressedSize = obj.EstimatedCompressedSize;
            SetSizeLabels();

            this.SafeInvoke(() =>
            {
                pgPreviewProgress.Visible = false;
                lblPreviewStatusLabel.Text = "Preview completed.";
            });
        }

        private void AddItemToListView(FilePreview preview)
        {
            this.SafeInvoke(() =>
            {
                lvFilePreviews.BeginUpdate();
                lvFilePreviews.Items.Add(new ListViewItem(
                [
                    preview.FileName ?? string.Empty,
                    ((ulong)preview.Size).FormatSize()
                ]) { Tag = preview });

                lvFilePreviews.ResizeColumns();
                lvFilePreviews.EndUpdate();
            });
        }

        private void PopulateTreeView()
        {

        }

        private void SetSizeLabels()
        {
            this.SafeInvoke(() =>
            {
                lblTotalSize.Text = ((ulong)TotalSize).FormatSize();
                lblEstimatedSize.Text = ((ulong)EstimatedCompressedSize).FormatSize();
            });
        }
    }
}
