// BackupPreview.cs : RadioExt-Helper
// Copyright (C) 2024  Ethan Hann
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

using System.Diagnostics;
using AetherUtils.Core.Extensions;
using AetherUtils.Core.Logging;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

public sealed partial class BackupPreview : Form
{
    private readonly BackupManager _backupManager;
    private long _estimatedCompressedSize;
    private bool _isBackupInProgress;

    private List<FilePreview> _previews = [];
    private long _totalSize;

    public BackupPreview(CompressionLevel compressionLevel)
    {
        InitializeComponent();

        _backupManager = new BackupManager(compressionLevel);

        _backupManager.PreviewProgressChanged += OnPreviewProgressChanged;
        _backupManager.PreviewStatusChanged += OnPreviewStatusChanged;
        _backupManager.BackupPreviewCompleted += OnBackupPreviewCompleted;
        _backupManager.StatusChanged += OnBackupStatusChanged;
        _backupManager.ProgressChanged += OnBackupProgressChanged;
        _backupManager.BackupCompleted += OnBackupCompleted;

        var treeImages = new ImageList();
        treeImages.Images.Add("folder", Resources.folder__16x16);
        treeImages.Images.Add("music_file", Resources.music_file_16x16);
        treeImages.Images.Add("file", Resources.file__16x16);
        tvFiles.ImageList = treeImages;

        Translate();
    }

    /// <summary>
    /// Destructor responsible for cleaning up resources.
    /// </summary>
    ~BackupPreview()
    {
        _backupManager.PreviewProgressChanged -= OnPreviewProgressChanged;
        _backupManager.PreviewStatusChanged -= OnPreviewStatusChanged;
        _backupManager.BackupPreviewCompleted -= OnBackupPreviewCompleted;
        _backupManager.StatusChanged -= OnBackupStatusChanged;
        _backupManager.ProgressChanged -= OnBackupProgressChanged;
        _backupManager.BackupCompleted -= OnBackupCompleted;
    }

    /// <summary>
    /// Form load event which will start asynchronously loading the backup preview.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BackupPreview_Load(object sender, EventArgs e)
    {
        pgProgress.Visible = true;
        btnStartBackup.Enabled = false;
        lblCompressionLevel.Text =
            string.Format(GlobalData.Strings.GetString("UsingCompressionLevel") ?? "Using compression level: {0}",
                _backupManager.BackupCompressionLevel);

        _ = StartPreviewLoading();
    }

    /// <summary>
    /// Translate the form and its controls into the current language.
    /// </summary>
    private void Translate()
    {
        Text = GlobalData.Strings.GetString("BackupStagingFolder");
        lblStatus.Text = GlobalData.Strings.GetString("CreatingPreview");
        lblTotalSizeLbl.Text = GlobalData.Strings.GetString("TotalFileSize");
        lblEstimatedSizeLbl.Text = GlobalData.Strings.GetString("EstimatedBackupSize");
        lblEstimatedDisclaimer.Text = GlobalData.Strings.GetString("EstimatedBackupSizeDisclaimer");
        btnStartBackup.Text = GlobalData.Strings.GetString("StartBackup");

        lvFilePreviews.Columns[0].Text = GlobalData.Strings.GetString("FileName");
        lvFilePreviews.Columns[1].Text = GlobalData.Strings.GetString("SongFileSizeHeader");
    }

    /// <summary>
    /// Load the backup preview asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task StartPreviewLoading()
    {
        try
        {
            await _backupManager.GetBackupPreviewAsync(GlobalData.ConfigManager.Get("stagingPath") as string ??
                                                       string.Empty);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<BackupPreview>("StartPreviewLoading").Error(ex, "Error loading backup preview.");
            this.SafeInvoke(() =>
            {
                var text = GlobalData.Strings.GetString("BackupPreviewError") ??
                           "An error occurred while loading the backup preview.";
                var caption = GlobalData.Strings.GetString("Error") ?? "Error";
                MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            });
        }
    }

    /// <summary>
    /// Occurs whenever the backup preview progress changes.
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
            AuLogger.GetCurrentLogger<BackupPreview>("OnPreviewProgressChanged")
                .Error(ex, "Error updating backup preview progress.");
        }
    }

    /// <summary>
    /// Occurs whenever the backup preview status changes.
    /// </summary>
    /// <param name="previewTuple">A tuple containing the current <see cref="FilePreview"/> and the total size up to this point.</param>
    private void OnPreviewStatusChanged((FilePreview, long) previewTuple)
    {
        try
        {
            _previews.Add(previewTuple.Item1);
            _totalSize += previewTuple.Item1.Size;
            _estimatedCompressedSize = previewTuple.Item2;

            AddItemToTreeView(previewTuple.Item1);
            SetSizeLabels();
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<BackupPreview>("OnPreviewStatusChanged")
                .Error(ex, "Error updating backup preview status.");
        }
    }

    /// <summary>
    /// Occurs whenever the backup preview is completed.
    /// </summary>
    /// <param name="previewTuple">A tuple containing the list of <see cref="FilePreview"/>s, the total size of the files, and the total estimated compressed size.</param>
    /// <exception cref="InvalidOperationException">Thrown if the values for the previews, the total size, or the estimated compressed size do not match this classes tracked values.</exception>
    private void OnBackupPreviewCompleted(
        (List<FilePreview> Previews, long TotalSize, long EstimatedCompressedSize) previewTuple)
    {
        if (_previews.Count != previewTuple.Previews.Count)
            throw new InvalidOperationException("Preview data mismatch!");

        if (_totalSize != previewTuple.TotalSize)
            throw new InvalidOperationException("Total size mismatch!");

        if (_estimatedCompressedSize != previewTuple.EstimatedCompressedSize)
            throw new InvalidOperationException("Estimated compressed size mismatch!");

        _previews = previewTuple.Previews;
        _totalSize = previewTuple.TotalSize;
        _estimatedCompressedSize = previewTuple.EstimatedCompressedSize;
        SetSizeLabels();

        this.SafeInvoke(() =>
        {
            pgProgress.Value = 0;
            pgProgress.Visible = false;
            lblStatus.Text = GlobalData.Strings.GetString("BackupPreviewCompleted");
            btnStartBackup.Enabled = true;
        });
    }

    /// <summary>
    /// Handles the click event for the start backup button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnStartBackup_Click(object sender, EventArgs e)
    {
        var backupPath = GetBackupPath();
        var stagingPath = GlobalData.ConfigManager.Get("stagingPath") as string;
        var gameBasePath = GlobalData.ConfigManager.Get("gameBasePath") as string;

        if (stagingPath == null || gameBasePath == null)
        {
            AuLogger.GetCurrentLogger<BackupPreview>("BtnStartBackup_Click")
                .Error("Staging path or game base path is null.");
            return;
        }

        if (string.IsNullOrEmpty(backupPath)) return;

        // Check if the backup path is a sub-path of the staging path (i.e., the backup path is within the staging path)
        if (PathHelper.IsSubPath(stagingPath, backupPath))
        {
            var text = GlobalData.Strings.GetString("BackupPathIsSubpath") ??
                       "Backup path cannot be within the staging path.";
            var caption = GlobalData.Strings.GetString("Backup") ?? "Backup";
            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        //Check if the backup path is a sub-path of the game path (i.e., the backup path is within the game path)
        if (PathHelper.IsSubPath(gameBasePath, backupPath))
        {
            var text = GlobalData.Strings.GetString("BackupPathIsSubpathGame") ??
                       "Backup path cannot be within the game path.";
            var caption = GlobalData.Strings.GetString("Backup") ?? "Backup";
            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        pgProgress.Value = 0;
        pgProgress.Visible = true;
        btnStartBackup.Enabled = false;

        // Start backup
        _ = StartBackupAsync(stagingPath, backupPath);
    }

    /// <summary>
    /// Starts the backup process asynchronously.
    /// </summary>
    /// <param name="stagingPath">The staging path.</param>
    /// <param name="backupPath">The backup path.</param>
    /// <returns>A task that represents the asynchronous backup operation.</returns>
    private async Task StartBackupAsync(string stagingPath, string backupPath)
    {
        try
        {
            _isBackupInProgress = true;
            await _backupManager.BackupStagingFolderAsync(stagingPath, backupPath);
        }
        catch (Exception ex)
        {
            var text = string.Format(
                GlobalData.Strings.GetString("BackupFailedException") ?? "Backup failed due to an error: {0}",
                ex.Message);
            var caption = GlobalData.Strings.GetString("Backup") ?? "Backup";
            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            AuLogger.GetCurrentLogger<MainForm>("StartBackupAsync")
                .Error(ex, "An error occurred during staging folder backup.");
        }
    }

    /// <summary>
    /// Occurs whenever the backup progress changes.
    /// </summary>
    /// <param name="progress">The current backup progress.</param>
    private void OnBackupProgressChanged(int progress)
    {
        try
        {
            this.SafeInvoke(() => pgProgress.Value = progress);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<BackupPreview>("OnBackupProgressChanged")
                .Error(ex, "Error updating backup progress.");
        }
    }

    /// <summary>
    /// Occurs whenever the backup status changes.
    /// </summary>
    /// <param name="status">The current backup status.</param>
    private void OnBackupStatusChanged(string status)
    {
        try
        {
            this.SafeInvoke(() => lblStatus.Text = status);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<BackupPreview>("OnBackupStatusChanged")
                .Error(ex, "Error updating backup status.");
        }
    }

    /// <summary>
    /// Occurs whenever the backup is completed.
    /// </summary>
    /// <param name="isSuccess">A flag indicating if the operation was completed successfully.</param>
    /// <param name="backupPath">The path to the backup folder.</param>
    /// <param name="backupFileName">The full file name for the backup .zip file.</param>
    private void OnBackupCompleted(bool isSuccess, string backupPath, string backupFileName)
    {
        this.SafeInvoke(() =>
        {
            if (isSuccess)
            {
                _isBackupInProgress = false;

                var text = GlobalData.Strings.GetString("BackupCompleted") ?? "Backup completed successfully.";
                var caption = GlobalData.Strings.GetString("Backup") ?? "Backup";
                MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                AuLogger.GetCurrentLogger<BackupPreview>("BackupManager_BackupCompleted")
                    .Info($"Backup completed successfully: {backupFileName}");
                Process.Start("explorer.exe", backupPath);
            }
            else
            {
                var text = GlobalData.Strings.GetString("BackupFailed") ?? "Backup failed.";
                var caption = GlobalData.Strings.GetString("Backup") ?? "Backup";
                MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                AuLogger.GetCurrentLogger<BackupPreview>("BackupManager_BackupCompleted")
                    .Error($"Backup failed: {backupFileName}");
            }

            Task.Delay(2000).ContinueWith(_ =>
            {
                this.SafeInvoke(() => lblStatus.Text = GlobalData.Strings.GetString("BackupCompleted"));
            });
        });
    }

    /// <summary>
    /// Shows a folder browser dialog to select a folder to save the backup to.
    /// </summary>
    /// <returns>The full path to the backup folder.</returns>
    private static string GetBackupPath()
    {
        FolderBrowserDialog folderBrowserDialog = new()
        {
            Description = GlobalData.Strings.GetString("BackupFolderDesc") ?? "Select a folder to save the backup to",
            ShowNewFolderButton = true,
            UseDescriptionForTitle = true
        };

        return folderBrowserDialog.ShowDialog() == DialogResult.OK ? folderBrowserDialog.SelectedPath : string.Empty;
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
            lblTotalSize.Text = ((ulong)_totalSize).FormatSize();
            lblEstimatedSize.Text = ((ulong)_estimatedCompressedSize).FormatSize();
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

        return PathHelper.IsValidAudioFile(fileName) ? "music_file" : "file";
    }

    /// <summary>
    /// Handles sorting the list view when a column is clicked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LvFilePreviews_ColumnClick(object sender, ColumnClickEventArgs e)
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
    private void TvFiles_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (e.Node?.Parent == null)
            PopulateListView(e.Node);
    }

    private void BackupPreview_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (_isBackupInProgress) _backupManager.CancelBackup();
    }
}