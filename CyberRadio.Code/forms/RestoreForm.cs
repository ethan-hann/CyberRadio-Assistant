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

        // Backup manager instance; used to restore backups where the compression level is ignored.
        private readonly BackupManager _backupManager = new(CompressionLevel.Normal);

        private readonly string _backupFilePath;
        private readonly string _restorePath;

        public RestoreForm(string backupFilePath, string restorePath)
        {
            InitializeComponent();

            _backupFilePath = backupFilePath;
            _restorePath = restorePath;

            //Setup events
            _backupManager.StatusChanged += BackupManager_StatusChanged;
            _backupManager.ProgressChanged += BackupManager_ProgressChanged;
            _backupManager.RestoreCompleted += BackupManager_RestoreCompleted;
        }

        private void RestoreForm_Load(object sender, EventArgs e)
        {
            //TODO: translations
            Text = $"Restore Backup - {_backupFilePath}";
        }

        private void RestoreForm_Shown(object sender, EventArgs e)
        {
            _ = _backupManager.RestoreBackupAsync(_backupFilePath, _restorePath);
        }

        private void BackupManager_RestoreCompleted(bool isSuccessful, string restorePath)
        {
            RestoreCompleted?.Invoke(this, (isSuccessful, restorePath));
            Close();
        }

        private void BackupManager_ProgressChanged(int progress)
        {
            this.SafeInvoke(() => pgProgress.Value = progress);
        }

        private void BackupManager_StatusChanged(string status)
        {
            this.SafeInvoke(() => lblStatus.Text = status);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _backupManager.CancelRestore();
            Close();
        }
    }
}
