// BackupManager.cs : RadioExt-Helper
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

using AetherUtils.Core.Files;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace RadioExt_Helper.utility;

/// <summary>
/// Represents a class for managing the backup of files and folders. Subscribe to the event handlers to track the backup operation.
/// </summary>
public class BackupManager(CompressionLevel level)
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
    /// Occurs whenever the progress of the backup preview operation changes.
    /// <para>Event data includes the current progress percentage.</para>
    /// </summary>
    public event Action<int>? PreviewProgressChanged;

    /// <summary>
    /// Occurs whenever the status of the backup preview operation changes.
    /// <para>Event data is a tuple containing the current <see cref="FilePreview"/> object and the current estimated backup size, in bytes.</para>
    /// </summary>
    public event Action<(FilePreview, long)>? PreviewStatusChanged;

    /// <summary>
    /// Occurs whenever the backup preview operation is completed.
    /// <para>Event data is a tuple with the list of previews, the total size of the files, and the estimated compressed size.</para>
    /// </summary>
    public event Action<(List<FilePreview> Previews, long TotalSize, long EstimatedCompressedSize)>? BackupPreviewCompleted;

    /// <summary>
    /// Get or set the compression level used for the backup operation.
    /// </summary>
    public CompressionLevel BackupCompressionLevel { get; } = level;

    /// <summary>
    /// Dictionary containing the mapping between compression levels and their corresponding compression ratios.
    /// </summary>
    private readonly Dictionary<CompressionLevel, double> _compressionRatios = new()
    {
        {CompressionLevel.None, 1.0},
        {CompressionLevel.Fastest, 0.9},
        {CompressionLevel.Fast, 0.8},
        {CompressionLevel.SuperFast, 0.7},
        {CompressionLevel.Normal, 0.6},
        {CompressionLevel.High, 0.5},
        {CompressionLevel.Maximum, 0.4},
        {CompressionLevel.Ultra, 0.3},
        {CompressionLevel.Extreme, 0.25},
        {CompressionLevel.Ultimate, 0.2} 
    };

    /*
     *None = 0,          // No compression (ratio: 1.0)
       Fastest = 1,     // Very low compression (ratio: 0.9)
       Fast = 2,       // Low compression (ratio: 0.8)
       SuperFast = 3,          // Medium-low compression (ratio: 0.7)
       Normal = 4,        // Medium compression (ratio: 0.6)
       High = 5,          // Medium-high compression (ratio: 0.5)
       Maximum = 6,       // High compression (ratio: 0.4)
       Ultra = 7,         // Very high compression (ratio: 0.3)
       Extreme = 8,       // Maximum compression (ratio: 0.25)
       Ultimate = 9       // Ultra compression (ratio: 0.2)
     */

    /// <summary>
    /// Asynchronously get a preview of the files that will be backed up from the staging folder.
    /// <para>The preview includes a list of <see cref="FilePreview"/> objects, the total size of the files, and the estimated compressed size.</para>
    /// </summary>
    /// <param name="stagingPath">The path to preview the backup of.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Occurs if the <paramref name="stagingPath"/> is <c>null</c> or empty.</exception>
    public async Task GetBackupPreviewAsync(string stagingPath)
    {
        if (string.IsNullOrEmpty(stagingPath))
            throw new ArgumentNullException(nameof(stagingPath));

        // Default to ratio of 0.6 if compression level is not found in the dictionary
        var compressionRatio = _compressionRatios.GetValueOrDefault(BackupCompressionLevel, 0.6);

        var files = FileHelper.SafeEnumerateFiles(stagingPath, "*.*", SearchOption.AllDirectories).ToArray();

        var previews = new List<FilePreview>();
        var totalSize = 0L;

        await Task.Run(() =>
        {
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                previews.Add(new FilePreview
                {
                    FileName = file[(stagingPath.Length + 1)..],
                    Size = fileInfo.Length
                });

                PreviewProgressChanged?.Invoke((int)((float)previews.Count / files.Length * 100));
                totalSize += fileInfo.Length;

                PreviewStatusChanged?.Invoke((previews.Last(), (long)(totalSize * compressionRatio)));
            }
        });

        BackupPreviewCompleted?.Invoke((previews, totalSize, (long)(totalSize * compressionRatio)));
    }

    /// <summary>
    /// Asynchronously backs up the contents of the staging folder to a zip file in the backup folder.
    /// </summary>
    /// <param name="stagingPath">The path to the staging folder.</param>
    /// <param name="backupPath">The path to a backup folder.</param>
    /// <returns>A <see cref="Task"/> representing the backup operation.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stagingPath"/> or <paramref name="backupPath"/> are <c>null</c> or empty.</exception>
    /// <exception cref="ArgumentException">If the backup path is the same as the staging path.</exception>
    /// <exception cref="ArgumentException">If the compression level is not between 0 and 9.</exception>
    public async Task BackupStagingFolderAsync(string stagingPath, string backupPath)
    {
        if (string.IsNullOrEmpty(stagingPath)) throw new ArgumentNullException(nameof(stagingPath));
        if (string.IsNullOrEmpty(backupPath)) throw new ArgumentNullException(nameof(backupPath));
        if (stagingPath.Equals(backupPath))
            throw new ArgumentException("Backup path cannot be the same as the staging path.");

        var backupFileName = Path.Combine(backupPath, $"radio_stations-{DateTime.Now:yy-MM-dd-hh-mm-ss}.zip");

        try
        {
            await Task.Run(() =>
            {
                using var zipOutputStream = new ZipOutputStream(File.Create(backupFileName));

                zipOutputStream.SetLevel((int)BackupCompressionLevel);

                var buffer = new byte[4096];
                var fileCount = 0;
                var files = FileHelper.SafeEnumerateFiles(stagingPath, "*.*", SearchOption.AllDirectories).ToArray();

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
                    var status =
                        string.Format(GlobalData.Strings.GetString("BackupProgressChanged") ?? "Backing up... {0}%",
                            progress);
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