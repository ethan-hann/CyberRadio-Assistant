// RestoreForm.cs : RadioExt-Helper
// Copyright (C) 2026  Ethan Hann
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

#region

using AetherUtils.Core.Extensions;
using AetherUtils.Core.Logging;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;

#endregion

namespace RadioExt_Helper.forms;

public partial class RestoreForm : Form
{
    private readonly string _backupFilePath;

    // Backup manager instance; used to restore backups. Compression level doesn't matter here since we're only restoring, but we need to specify it to create the instance.
    private readonly BackupManager _backupManager = new(CompressionLevel.Normal);
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
        ImageList treeImages = new();
        treeImages.Images.Add("folder", Resources.folder__16x16);
        treeImages.Images.Add("png_file", Resources.png_file_16x16);
        treeImages.Images.Add("music_file", Resources.music_file_16x16);
        treeImages.Images.Add("file", Resources.file__16x16);
        treeImages.Images.Add("archive_file", Resources.box_16x16);
        tvFiles.ImageList = treeImages;

        Translate();
    }

    /// <summary>
    ///     Occurs when the restore operation is completed.
    ///     <para>Event data is a flag indicating success and the restored path.</para>
    /// </summary>
    public event EventHandler<(bool, string)>? RestoreCompleted;

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
        Text = string.Format(Strings.RestoreFormTitle, _backupFilePath);
        lblStatus.Text = Strings.Ready;
        btnCancel.Text = Strings.Cancel;
        btnStartRestore.Text = Strings.StartRestore;
        lblDescription.Text = Strings.RestoreDescription;
        lblRestoreSize.Text = string.Format(Strings.EstimatedRestoreSizeFormat, ((ulong)0).FormatSize());

        lvFilePreviews.Columns[0].Text = Strings.FileName;
        lvFilePreviews.Columns[1].Text = Strings.SongFileSizeHeader;
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
            AuLogger.GetCurrentLogger<RestoreForm>("StartPreviewLoading")
                .Error(ex, "An error occured while trying to load the restore preview.");
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
            lblStatus.Text = Strings.RestoreForm_PreviewCompleted;
            btnStartRestore.Enabled = true;
            tvFiles.SelectedNode = tvFiles.Nodes[0];
        });
    }

    /// <summary>
    ///     Occurs whenever the restore preview progress changes.
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
            AuLogger.GetCurrentLogger<RestoreForm>("OnPreviewStatusChanged")
                .Error(ex, "An error occured while trying to update the preview status.");
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
            _isRestoreInProgress = false;
            pgProgress.Value = 0;
            pgProgress.Visible = false;
            btnStartRestore.Enabled = true;
            this.SafeInvoke(() => lblStatus.Text = Strings.RestoreForm_ErrorWhileRestoring);
            AuLogger.GetCurrentLogger<RestoreForm>("StartRestoreAsync")
                .Error(ex, "An error occured while trying to start the restore operation.");
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        _backupManager.CancelRestore();
        MessageBox.Show(this, Strings.RestoreForm_RestoreCancelled, Strings.RestoreForm_RestoreCancelledCaption,
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    ///     Safely add a <see cref="FilePreview" /> item to the tree view from a different thread.
    /// </summary>
    /// <param name="preview">The <see cref="FilePreview" /> to add.</param>
    private void AddItemToTreeView(FilePreview preview)
    {
        this.SafeInvoke(() =>
        {
            tvFiles.BeginUpdate();
            var parts = preview.FileName?.Split(['\\', '/'], StringSplitOptions.RemoveEmptyEntries);
            if (parts == null) return;

            TreeNode? currentNode = null;
            var currentNodeCollection = tvFiles.Nodes;

            for (var i = 0; i < parts.Length; i++)
            {
                var part = parts[i];
                var existingNode = currentNodeCollection.Cast<TreeNode>().FirstOrDefault(n => n.Text.Equals(part));
                if (existingNode == null)
                {
                    var isDirectory = i < parts.Length - 1;
                    var imageKey = GetImageKey(part, isDirectory);
                    TreeNode node = new(part)
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

            TreeNode? targetNode = currentNode?.Parent;
            if (parts.Length >= 3 && parts[0].Equals("replaced-stations", StringComparison.OrdinalIgnoreCase))
            {
                var replacedStationsNode = tvFiles.Nodes.Cast<TreeNode>()
                    .FirstOrDefault(n => n.Text.Equals(parts[0], StringComparison.OrdinalIgnoreCase));
                targetNode = replacedStationsNode?.Nodes.Cast<TreeNode>()
                    .FirstOrDefault(n => n.Text.Equals(parts[1], StringComparison.OrdinalIgnoreCase));
            }

            if (targetNode?.Tag is List<FilePreview> previews)
                previews.Add(preview);

            tvFiles.EndUpdate();
        });
    }

    /// <summary>
    ///     Safely populates the list view with file previews from a different thread.
    /// </summary>
    /// <param name="node">The <see cref="TreeNode" /> to use when populating the list view.</param>
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

                    var displayFileName = preview.FileName;
                    var normalizedNodePrefix = node.FullPath.Replace('\\', '/').TrimEnd('/') + '/';
                    var normalizedDisplayFileName = displayFileName?.Replace('\\', '/');
                    if (!string.IsNullOrEmpty(normalizedDisplayFileName) &&
                        normalizedDisplayFileName.StartsWith(normalizedNodePrefix, StringComparison.OrdinalIgnoreCase))
                        displayFileName = normalizedDisplayFileName[normalizedNodePrefix.Length..];

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
    ///     Safely sets the size labels from a different thread.
    /// </summary>
    private void SetSizeLabels()
    {
        this.SafeInvoke(() =>
        {
            lblRestoreSize.Text =
                string.Format(Strings.EstimatedRestoreSizeFormat, ((ulong)_totalSize).FormatSize());
        });
    }

    /// <summary>
    ///     Get the correct image key for the file preview based on the file name.
    /// </summary>
    /// <param name="fileName">The file name to get the image of.</param>
    /// <param name="isDirectory">
    ///     Indicates whether the file preview we are getting an image of is a directory node in the
    ///     <see cref="TreeView" />.
    /// </param>
    /// <returns></returns>
    private static string GetImageKey(string fileName, bool isDirectory)
    {
        if (isDirectory)
            return "folder";

        return PathHelper.IsValidAudioFile(fileName) ? "music_file" :
            PathHelper.IsValidArchiveFile(fileName) ? "archive_file" :
            PathHelper.IsValidImageFile(fileName) ? "png_file" : "file";
    }

    /// <summary>
    ///     Handles sorting the list view when a column is clicked.
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
    ///     Populates the list view with file previews when a node is selected in the tree view; only populates the list view
    ///     if the selected node is a root node.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void tvFiles_AfterSelect(object sender, TreeViewEventArgs e)
    {
        var isRootNode = e.Node?.Parent == null;
        var isReplacementStationRoot = e.Node?.Parent != null &&
                                       e.Node.Parent.Parent == null &&
                                       e.Node.Parent.Text.Equals("replaced-stations",
                                           StringComparison.OrdinalIgnoreCase);

        if (isRootNode || isReplacementStationRoot)
            PopulateListView(e.Node);
    }

    private void RestoreForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (_isRestoreInProgress)
            _backupManager.CancelRestore();
    }
}