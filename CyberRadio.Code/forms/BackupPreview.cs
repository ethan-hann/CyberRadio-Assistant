using AetherUtils.Core.Extensions;
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
        private List<FilePreview> Previews = [];
        private long TotalSize;
        private long EstimatedCompressedSize;

        public BackupPreview()
        {
            InitializeComponent();
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
            var bgTask = Task.Run(async () => 
            {
                var data = await backupManager.GetBackupPreviewAsync(GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty);
                Previews = data.Previews;
                TotalSize = data.TotalSize;
                EstimatedCompressedSize = data.EstimatedCompressedSize;
            });

            Debug.WriteLine("Loading preview...");
            bgTask.Wait();

            if (bgTask.Status == TaskStatus.RanToCompletion)
            {
                PopulateListView();
                PopulateTreeView();
                SetSizeLabels();
            }
        }

        private void PopulateListView()
        {
            this.SafeInvoke(() =>
            {
                lvFilePreviews.SuspendLayout();
                lvFilePreviews.Items.Clear();

                foreach (var lvItem in Previews
                             .Select(preview => new ListViewItem([
                                 preview.FileName ?? string.Empty,
                             ((ulong)preview.Size).FormatSize()
                                 ])
                             { Tag = preview }))
                    lvFilePreviews.Items.Add(lvItem);

                lvFilePreviews.ResizeColumns();
                lvFilePreviews.ResumeLayout();
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
