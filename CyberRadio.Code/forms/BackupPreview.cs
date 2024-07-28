using AetherUtils.Core.Extensions;
using AetherUtils.Core.Files;
using AetherUtils.Core.Logging;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms
{
    public partial class BackupPreview : Form
    {
        private readonly BackupManager _backupManager;
        private readonly ImageList _treeImages;

        private List<FilePreview> _previews = [];
        private long _totalSize;
        private long _estimatedCompressedSize;

        public BackupPreview(CompressionLevel compressionLevel)
        {
            InitializeComponent();

            _backupManager = new BackupManager(compressionLevel);

            _backupManager.PreviewProgressChanged += _backupManager_PreviewProgressChanged;
            _backupManager.PreviewStatusChanged += _backupManager_PreviewStatusChanged;
            _backupManager.BackupPreviewCompleted += _backupManager_BackupPreviewCompleted;

            _treeImages = new ImageList();
            _treeImages.Images.Add("folder", Resources.folder__16x16);
            _treeImages.Images.Add("music_file", Resources.music_file_16x16);
            _treeImages.Images.Add("file", Resources.file__16x16);
            tvFiles.ImageList = _treeImages;
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
            pgPreviewProgress.Visible = true;
            lblPreviewStatusLabel.Text = $"Loading preview. Using {_backupManager.BackupCompressionLevel} compression...";

            _ = StartPreviewLoading();
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
            _previews.Add(obj.Item1);
            _totalSize += obj.Item1.Size;
            _estimatedCompressedSize = obj.Item2;

            AddItemToTreeView(obj.Item1);
            SetSizeLabels();
        }

        private void _backupManager_BackupPreviewCompleted((List<FilePreview> Previews, long TotalSize, long EstimatedCompressedSize) obj)
        {
            if (_previews.Count != obj.Previews.Count)
                throw new Exception("Preview data mismatch!");

            if (_totalSize != obj.TotalSize)
                throw new Exception("Total size mismatch!");

            if (_estimatedCompressedSize != obj.EstimatedCompressedSize)
                throw new Exception("Estimated compressed size mismatch!");

            _previews = obj.Previews;
            _totalSize = obj.TotalSize;
            _estimatedCompressedSize = obj.EstimatedCompressedSize;
            SetSizeLabels();

            this.SafeInvoke(() =>
            {
                pgPreviewProgress.Visible = false;
                lblPreviewStatusLabel.Text = "Preview completed.";
            });
        }

        private void AddItemToTreeView(FilePreview preview)
        {
            this.SafeInvoke(() =>
            {
                tvFiles.BeginUpdate();
                var parts = preview.FileName?.Split(Path.DirectorySeparatorChar);
                if (parts == null) return;

                TreeNode? currentNode = null;
                var currentNodeCollection = tvFiles.Nodes;

                foreach (var part in parts)
                {
                    var existingNode = currentNodeCollection.Cast<TreeNode>().FirstOrDefault(n => n.Text.Equals(part));
                    if (existingNode == null)
                    {
                        var isRoot = currentNode == null;
                        var imageKey = GetImageKey(part, isRoot);
                        var node = new TreeNode(part)
                        { 
                            Tag = new List<FilePreview>(),
                            ImageKey = imageKey,
                            SelectedImageKey = imageKey
                        };

                        currentNodeCollection.Add(node);
                        currentNode = node;
                    }
                    else
                    {
                        currentNode = existingNode;
                    }
                    currentNodeCollection = currentNode.Nodes;
                }

                if (currentNode?.Parent?.Tag is List<FilePreview> previews)
                    previews.Add(preview);

                tvFiles.EndUpdate();
            });
        }

        private void PopulateListView(TreeNode? node)
        {
            this.SafeInvoke(() =>
            {
                if (node == null) return;

                lvFilePreviews.BeginUpdate();
                lvFilePreviews.Items.Clear();

                if (node.Tag is List<FilePreview?> previews)
                {
                    foreach (var preview in previews)
                    {
                        if (preview == null) continue;

                        // Remove the root directory from the file name
                        var displayFileName = preview?.FileName?.Replace(node.Text + Path.DirectorySeparatorChar, "");

                        var size = ((ulong)(preview?.Size ?? 0)).FormatSize();

                        lvFilePreviews.Items.Add(new ListViewItem(
                        [
                            displayFileName ?? string.Empty,
                            size
                        ])
                        { Tag = preview });
                    }
                }

                lvFilePreviews.ResizeColumns();
                lvFilePreviews.EndUpdate();
            });
        }

        private void SetSizeLabels()
        {
            this.SafeInvoke(() =>
            {
                lblTotalSize.Text = ((ulong)_totalSize).FormatSize();
                lblEstimatedSize.Text = ((ulong)_estimatedCompressedSize).FormatSize();
            });
        }

        private string GetImageKey(string fileName, bool isRoot)
        {
            if (isRoot)
                return "folder";

            return PathHelper.IsValidAudioFile(fileName) ? "music_file" : "file";
        }

        private void TvFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Parent == null)
                PopulateListView(e.Node);
        }
    }
}
