using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace RadioExt_Helper.utility
{
    /// <summary>
    /// Represents a class for managing the backup of files and folders. Subscribe to the event handlers to track the backup operation.
    /// </summary>
    public class BackupManager
    {
        /// <summary>
        /// Occurs whenever the progress of the backup operation changes.
        /// <para>Event data includes the current progress percentage.</para>
        /// </summary>
        public event Action<int>? ProgressChanged;

        /// <summary>
        /// Occurs whenever the status of the backup operation changes.
        /// <para>Event data includes a message describing the current status.</para>
        /// </summary>
        public event Action<string>? StatusChanged;

        /// <summary>
        /// Occurs whenever the backup operation is completed.
        /// <para>Event data includes a flag indicating success, the path to the backup folder, and backup file name.</para>
        /// </summary>
        public event Action<bool, string, string>? BackupCompleted;

        /// <summary>
        /// Asynchronously backs up the contents of the staging folder to a zip file in the backup folder.
        /// </summary>
        /// <param name="stagingPath">The path to the staging folder.</param>
        /// <param name="backupPath">The path to a backup folder.</param>
        /// <returns>A <see cref="Task"/> representing the backup operation.</returns>
        /// <exception cref="ArgumentNullException">If either <paramref name="stagingPath"/> or <paramref name="backupPath"/> are <c>null</c> or empty.</exception>
        /// <exception cref="ArgumentException">If the backup path is the same as the staging path.</exception>
        public async Task BackupStagingFolderAsync(string stagingPath, string backupPath)
        {
            if (string.IsNullOrEmpty(stagingPath)) throw new ArgumentNullException(nameof(stagingPath));
            if (string.IsNullOrEmpty(backupPath)) throw new ArgumentNullException(nameof(backupPath));
            if (stagingPath.Equals(backupPath)) throw new ArgumentException("Backup path cannot be the same as the staging path.");

            var backupFileName = Path.Combine(backupPath, $"radio_stations-{DateTime.Now:yy-MM-dd-hh-mm-ss}.zip");

            try
            {
                await Task.Run(() =>
                {
                    using var zipOutputStream = new ZipOutputStream(File.Create(backupFileName));

                    zipOutputStream.SetLevel(3); // Compression level 0-9

                    var buffer = new byte[4096];
                    var fileCount = 0;
                    var files = Directory.GetFiles(stagingPath, "*.*", SearchOption.AllDirectories);

                    foreach (var file in files)
                    {
                        var entryName = file[(stagingPath.Length + 1)..];
                        var entry = new ZipEntry(entryName)
                        {
                            DateTime = File.GetLastWriteTime(file)
                        };
                        zipOutputStream.PutNextEntry(entry);

                        using (var fs = File.OpenRead(file))
                        {
                            StreamUtils.Copy(fs, zipOutputStream, buffer);
                        }

                        zipOutputStream.CloseEntry();
                        fileCount++;

                        // Report progress
                        var progress = (int)((float)fileCount / files.Length * 100);
                        ProgressChanged?.Invoke(progress);
                        var status = string.Format(GlobalData.Strings.GetString("BackupProgressChanged") ?? "Backing up... {0}%", progress);
                        StatusChanged?.Invoke(status);
                    }

                    zipOutputStream.Finish();
                    zipOutputStream.Close();
                });

                if (File.Exists(backupFileName))
                {
                    var status = GlobalData.Strings.GetString("BackupCompleted") ?? "Backup completed successfully.";
                    StatusChanged?.Invoke(status);
                    BackupCompleted?.Invoke(true, backupPath, backupFileName);
                }
                else
                {
                    var status = GlobalData.Strings.GetString("BackupFailed") ?? "Backup failed.";
                    StatusChanged?.Invoke(status);
                    BackupCompleted?.Invoke(false, backupPath, backupFileName);
                }
            }
            catch (Exception ex)
            {
                var status =
                    string.Format(
                        GlobalData.Strings.GetString("BackupFailedException") ?? "Backup failed due to an error: {0}",
                        ex.Message);
                StatusChanged?.Invoke(status);
                BackupCompleted?.Invoke(false, backupPath, backupFileName);
                throw;
            }
        }
    }
}
