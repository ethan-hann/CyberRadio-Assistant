using AetherUtils.Core.Extensions;
using AetherUtils.Core.Logging;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms
{
    public partial class RestoreForm : Form
    {
        /// <summary>
        /// Occurs when the restore operation is completed.
        /// <para>Event data is a flag indicating success and the restored path.</para>
        /// </summary>
        public event EventHandler<(bool, string)>? RestoreCompleted;

        // Backup manager instance; used to restore backups.
        private readonly BackupManager _backupManager = new(CompressionLevel.Normal);

        private readonly string _backupFilePath;
        private readonly string _restorePath;
        private bool _isRestoreInProgress;
        private List<FilePreview> _previews = [];
        private long _totalSize;

        public RestoreForm(string backupFilePath, string restorePath)
        {
            InitializeComponent();

            _backupFilePath = backupFilePath;
            _restorePath = restorePath;

            //Setup events
            _backupManager.PreviewProgressChanged += OnPreviewProgressChanged;
            _backupManager.PreviewStatusChanged += OnPreviewStatusChanged;
            _backupManager.RestorePreviewCompleted += OnRestorePreviewCompleted;
            _backupManager.StatusChanged += OnBackupStatusChanged;
            _backupManager.ProgressChanged += OnBackupProgressChanged;
            _backupManager.RestoreCompleted += OnBackupRestoreCompleted;

            //Setup UI
            var treeImages = new ImageList();
            treeImages.Images.Add("folder", Resources.folder__16x16);
            treeImages.Images.Add("png_file", Resources.png_file_16x16);
            treeImages.Images.Add("music_file", Resources.music_file_16x16);
            treeImages.Images.Add("file", Resources.file__16x16);
            treeImages.Images.Add("archive_file", Resources.box_16x16);
            tvFiles.ImageList = treeImages;

            Translate();
        }

        ~RestoreForm()
        {
            //Cleanup events
            _backupManager.PreviewProgressChanged -= OnPreviewProgressChanged;
            _backupManager.PreviewStatusChanged -= OnPreviewStatusChanged;
            _backupManager.RestorePreviewCompleted -= OnRestorePreviewCompleted;
            _backupManager.StatusChanged -= OnBackupStatusChanged;
            _backupManager.ProgressChanged -= OnBackupProgressChanged;
            _backupManager.RestoreCompleted -= OnBackupRestoreCompleted;
        }

        private void Translate()
        {
            //TODO translations
            Text = $"Restore Backup - {_backupFilePath}";
        }

        private void RestoreForm_Load(object sender, EventArgs e)
        {
            pgProgress.Visible = false;
            btnCancel.Visible = false;
            btnStartRestore.Visible = false;

            _ = StartPreviewLoading();
        }

        private async Task StartPreviewLoading()
        {
            try
            {
                await _backupManager.GetRestorePreviewAsync(_backupFilePath);
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<RestoreForm>("StartPreviewLoading").Error(ex,
                    "An error occured while trying to load the restore preview.");
                Close();
            }
            finally
            {
                this.SafeInvoke(() => btnStartRestore.Visible = true);
            }
        }

        private void OnRestorePreviewCompleted((List<FilePreview>, long) previewTuple)
        {
            if (_previews.Count != previewTuple.Item1.Count)
                throw new InvalidOperationException("Preview data mismatch!");

            if (_totalSize != previewTuple.Item2)
                throw new InvalidOperationException("Total size mismatch!");

            _previews = previewTuple.Item1;
            _totalSize = previewTuple.Item2;
            SetSizeLabels();

            this.SafeInvoke(() =>
            {
                pgProgress.Value = 0;
                pgProgress.Visible = false;
                lblStatus.Text = "Restore preview completed. Ready to start restore operation.";
                btnStartRestore.Enabled = true;
                tvFiles.SelectedNode = tvFiles.Nodes[0];
            });
        }

        /// <summary>
        /// Occurs whenever the restore preview progress changes.
        /// </summary>
        /// <param name="progress">The current progress percentage.</param>
        private void OnPreviewProgressChanged(int progress)
        {
            try
            {
                this.SafeInvoke(() => pgProgress.Value = progress);
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<RestoreForm>("OnPreviewProgressChanged")
                    .Error(ex, "Error updating restore preview progress.");
            }
        }

        private void OnPreviewStatusChanged((FilePreview, long) obj)
        {
            try
            {
                _previews.Add(obj.Item1);
                _totalSize += obj.Item1.Size;

                AddItemToTreeView(obj.Item1);
                SetSizeLabels();
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<RestoreForm>("OnPreviewStatusChanged").Error(ex,
                    "An error occured while trying to update the preview status.");
            }

        }

        private void OnBackupRestoreCompleted(bool isSuccessful, string restorePath)
        {
            _isRestoreInProgress = false;
            RestoreCompleted?.Invoke(this, (isSuccessful, restorePath));
            Close();
        }

        private void OnBackupProgressChanged(int progress)
        {
            this.SafeInvoke(() => pgProgress.Value = progress);
        }

        private void OnBackupStatusChanged(string status)
        {
            this.SafeInvoke(() => lblStatus.Text = status);
        }

        private void btnStartRestore_Click(object sender, EventArgs e)
        {
            pgProgress.Value = 0;
            pgProgress.Visible = true;
            btnStartRestore.Enabled = false;
            btnCancel.Visible = true;
            btnCancel.Enabled = true;

            //Start the restore operation
            _ = StartRestoreAsync(_backupFilePath, _restorePath);
        }

        private async Task StartRestoreAsync(string backupFile, string restorePath)
        {
            try
            {
                _isRestoreInProgress = true;
                await _backupManager.RestoreBackupAsync(backupFile, restorePath);
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<RestoreForm>("StartRestoreAsync").Error(ex, "An error occured while trying to start the restore operation.");
                _isRestoreInProgress = false;
                pgProgress.Value = 0;
                pgProgress.Visible = false;
                btnStartRestore.Enabled = true;
                this.SafeInvoke(() => lblStatus.Text = "An error occured while trying to start the restore operation.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _backupManager.CancelRestore();
            MessageBox.Show("Restore operation has been cancelled.", "Restore Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Safely add a <see cref="FilePreview"/> item to the tree view from a different thread.
        /// </summary>
        /// <param name="preview">The <see cref="FilePreview"/> to add.</param>
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

        /// <summary>
        /// Safely populates the list view with file previews from a different thread.
        /// </summary>
        /// <param name="node">The <see cref="TreeNode"/> to use when populating the list view.</param>
        private void PopulateListView(TreeNode? node)
        {
            this.SafeInvoke(() =>
            {
                if (node == null) return;

                lvFilePreviews.BeginUpdate();
                lvFilePreviews.Items.Clear();

                if (node.Tag is List<FilePreview?> previews)
                    foreach (var preview in previews)
                    {
                        if (preview == null) continue;

                        // Remove the root directory from the file name
                        var displayFileName = preview.FileName?.Replace(node.Text + Path.DirectorySeparatorChar, "");

                        var size = ((ulong)preview.Size).FormatSize();

                        lvFilePreviews.Items.Add(new ListViewItem(
                            [
                                displayFileName ?? string.Empty,
                                size
                            ])
                        { Tag = preview });
                    }

                lvFilePreviews.ResizeColumns();
                lvFilePreviews.EndUpdate();
            });
        }

        /// <summary>
        /// Safely sets the size labels from a different thread.
        /// </summary>
        private void SetSizeLabels()
        {
            this.SafeInvoke(() =>
            {
                lblRestoreSize.Text = "Restored Size: " + ((ulong)_totalSize).FormatSize();
            });
        }

        /// <summary>
        /// Get the correct image key for the file preview based on the file name.
        /// </summary>
        /// <param name="fileName">The file name to get the image of.</param>
        /// <param name="isRoot">Indicates whether the file preview we are getting an image of is a root node in the <see cref="TreeView"/>.</param>
        /// <returns></returns>
        private static string GetImageKey(string fileName, bool isRoot)
        {
            if (isRoot)
                return "folder";

            return PathHelper.IsValidAudioFile(fileName) ? "music_file" :
                PathHelper.IsValidArchiveFile(fileName) ? "archive_file" :
                PathHelper.IsValidImageFile(fileName) ? "png_file" : "file";
        }

        /// <summary>
        /// Handles sorting the list view when a column is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Populates the list view with file previews when a node is selected in the tree view; only populates the list view if the selected node is a root node.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Parent == null)
                PopulateListView(e.Node);
        }

        private void RestoreForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isRestoreInProgress) 
                _backupManager.CancelRestore();
        }
    }
}
