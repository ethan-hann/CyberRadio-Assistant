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

using System.IO.Compression;
using System.Text;
using AetherUtils.Core.Files;
using AetherUtils.Core.Logging;

namespace RadioExt_Helper.utility;

//TODO: refactor this to use the new SharpCompress library

/// <summary>
/// Represents a class for managing the backup of files and folders. Subscribe to the event handlers to track the backup operation.
/// </summary>
public class BackupManager(CompressionLevel level)
{
    /// <summary>
    /// Dictionary containing the mapping between compression levels and their corresponding compression ratios.
    /// </summary>
    private readonly Dictionary<CompressionLevel, double> _compressionRatios = new()
    {
        { CompressionLevel.None, 1.0 },
        { CompressionLevel.Fastest, 0.9 },
        { CompressionLevel.Fast, 0.85 },
        { CompressionLevel.SuperFast, 0.8 },
        { CompressionLevel.Normal, 0.75 },
        { CompressionLevel.High, 0.7 },
        { CompressionLevel.Maximum, 0.65 },
        { CompressionLevel.Ultra, 0.6 },
        { CompressionLevel.Extreme, 0.55 },
        { CompressionLevel.Ultimate, 0.5 }
    };

    private bool _isCancelling;

    /// <summary>
    /// Get or set the compression level used for the backup operation.
    /// </summary>
    public CompressionLevel BackupCompressionLevel { get; } = level;

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
    /// Occurs whenever the restore operation is completed.
    /// <para>Event data includes a flag indicating success and the restore path.</para>
    /// </summary>
    public event Action<bool, string>? RestoreCompleted;

    /// <summary>
    /// Occurs whenever the progress of the backup preview or restore preview operation changes.
    /// <para>Event data includes the current progress percentage.</para>
    /// </summary>
    public event Action<int>? PreviewProgressChanged;

    /// <summary>
    /// Occurs whenever the status of the backup preview operation changes.
    /// <para>Event data is a tuple containing the current <see cref="FilePreview"/> object and the current estimated backup size, in bytes.</para>
    /// </summary>
    public event Action<(FilePreview, long)>? PreviewStatusChanged;

    /// <summary>
    /// Occurs whenever the status of the restore preview operation changes.
    /// <para>Event data is a tuple containing the current <see cref="FilePreview"/> object and the current estimated restore size, in bytes.</para>
    /// </summary>
    public event Action<(List<FilePreview>, long)>? RestorePreviewCompleted;

    /// <summary>
    /// Occurs whenever the backup preview operation is completed.
    /// <para>Event data is a tuple with the list of previews, the total size of the files, and the estimated compressed size.</para>
    /// </summary>
    public event Action<(List<FilePreview> Previews, long TotalSize, long EstimatedCompressedSize)>?
        BackupPreviewCompleted;

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

        if (_isCancelling) return;

        // Default to ratio of 0.75 (Normal) if compression level is not found in the dictionary
        var compressionRatio = _compressionRatios.GetValueOrDefault(BackupCompressionLevel, 0.75);

        var files = FileHelper.SafeEnumerateFiles(stagingPath, "*.*", SearchOption.AllDirectories).ToArray();

        var previews = new List<FilePreview>();
        var totalSize = 0L;

        await Task.Run(() =>
        {
            foreach (var file in files)
            {
                if (_isCancelling) return;

                var fileInfo = new FileInfo(file);
                previews.Add(new FilePreview
                {
                    FileName = file[(stagingPath.Length + 1)..],
                    Size = fileInfo.Length
                });

                if (_isCancelling) return;
                PreviewProgressChanged?.Invoke((int)((float)previews.Count / files.Length * 100));
                totalSize += fileInfo.Length;

                if (_isCancelling) return;
                PreviewStatusChanged?.Invoke((previews.Last(), (long)(totalSize * compressionRatio)));
            }
        });

        if (_isCancelling) return;

        BackupPreviewCompleted?.Invoke((previews, totalSize, (long)(totalSize * compressionRatio)));
    }

    /// <summary>
    /// Asynchronously backs up the contents of the staging folder to a zip file in the backup folder.
    /// </summary>
    /// <param name="stagingPath">The path to the staging folder.</param>
    /// <param name="backupPath">The path to a backup folder.</param>
    /// <param name="shouldCopySongFiles">Indicate whether the actual song files should be included in the backed up file or not.</param>
    /// <returns>A <see cref="Task"/> representing the backup operation.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="stagingPath"/> or <paramref name="backupPath"/> are <c>null</c> or empty.</exception>
    /// <exception cref="ArgumentException">If the backup path is the same as the staging path.</exception>
    /// <exception cref="ArgumentException">If the compression level is not between 0 and 9.</exception>
    public async Task BackupStagingFolderAsync(string stagingPath, string backupPath, bool shouldCopySongFiles)
    {
        if (string.IsNullOrEmpty(stagingPath)) throw new ArgumentNullException(nameof(stagingPath));
        if (string.IsNullOrEmpty(backupPath)) throw new ArgumentNullException(nameof(backupPath));
        if (stagingPath.Equals(backupPath))
            throw new ArgumentException("Backup path cannot be the same as the staging path.");

        var backupFileName = Path.Combine(backupPath, $"cra_stations-{DateTime.Now:yy-MM-dd-hh-mm-ss}.zip");

        if (_isCancelling) return;

        try
        {
            await Task.Run(() =>
            {
                if (_isCancelling) return;

                var songPathMappings = new Dictionary<string, string>();
                var files = shouldCopySongFiles ? GetFilesIncludingSongs(stagingPath) : GetFilesOnly(stagingPath);

                using var zipArchive = ZipFile.Open(backupFileName, ZipArchiveMode.Create, Encoding.UTF8);

                var fileCount = 0;
                foreach (var file in files)
                {
                    if (_isCancelling) return;

                    string entryName;
                    var isInsideStaging = PathHelper.IsSubPath(stagingPath, file);

                    if (isInsideStaging)
                    {
                        if (file.EndsWith("metadata.json", StringComparison.OrdinalIgnoreCase) ||
                            file.EndsWith("songs.sgls", StringComparison.OrdinalIgnoreCase))
                        {
                            var stationFolder = Path.GetDirectoryName(file)?.Replace(stagingPath, "")
                                .TrimStart(Path.DirectorySeparatorChar);
                            entryName = Path.Combine(stationFolder ?? string.Empty, Path.GetFileName(file));
                        }
                        else if (file.EndsWith(".archive", StringComparison.OrdinalIgnoreCase))
                        {
                            entryName = Path.Combine("icons", Path.GetFileName(file));
                        }
                        else
                        {
                            entryName = file[(stagingPath.Length + 1)..];
                        }
                    }
                    else
                    {
                        entryName = Path.Combine("external", Path.GetFileName(file));
                        songPathMappings[Path.GetFileName(file)] = file;
                    }

                    entryName = PathHelper.SanitizePath(entryName);
                    zipArchive.CreateEntryFromFile(file, entryName,
                        System.IO.Compression.CompressionLevel.SmallestSize);

                    fileCount++;
                    var progress = (int)((float)fileCount / files.Length * 100);
                    ProgressChanged?.Invoke(progress);

                    var status = string.Format(Strings.BackupProgressChanged, progress);
                    StatusChanged?.Invoke(status);
                }

                if (songPathMappings.Count > 0)
                {
                    var songPathsContent = new StringBuilder();
                    foreach (var kvp in songPathMappings) songPathsContent.AppendLine($"{kvp.Key}|{kvp.Value}");

                    var songPathsBytes = Encoding.UTF8.GetBytes(songPathsContent.ToString());
                    var songPathsEntry = zipArchive.CreateEntry("externalPaths.txt");

                    using var songPathsStream = songPathsEntry.Open();
                    songPathsStream.Write(songPathsBytes, 0, songPathsBytes.Length);
                }
            });

            if (File.Exists(backupFileName))
            {
                var status = Strings.BackupCompleted;
                if (_isCancelling) return;
                StatusChanged?.Invoke(status);

                if (_isCancelling) return;
                BackupCompleted?.Invoke(true, backupPath, backupFileName);
            }
            else
            {
                var status = Strings.BackupFailed;
                if (_isCancelling) return;
                StatusChanged?.Invoke(status);

                if (_isCancelling) return;
                BackupCompleted?.Invoke(false, backupPath, backupFileName);
            }
        }
        catch (Exception ex)
        {
            var status = string.Format(Strings.BackupFailedException, ex.Message);

            if (_isCancelling) return;
            StatusChanged?.Invoke(status);

            if (_isCancelling) return;
            BackupCompleted?.Invoke(false, backupPath, backupFileName);
            throw;
        }
    }

    /// <summary>
    /// Asynchronously get a preview of the files that will be restored to the staging folder.
    /// <para>The preview includes a list of <see cref="FilePreview"/> objects and the total size of the files.</para>
    /// </summary>
    /// <param name="backupFilePath">The path of the backed up .zip to preview.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Occurs if the <paramref name="backupFilePath"/> is <c>null</c> or empty.</exception>
    public async Task GetRestorePreviewAsync(string backupFilePath)
    {
        if (string.IsNullOrEmpty(backupFilePath)) throw new ArgumentNullException(nameof(backupFilePath));
        if (!File.Exists(backupFilePath))
            throw new FileNotFoundException("Backup file not found.", backupFilePath);

        var previews = new List<FilePreview>();
        var totalSize = 0L;

        await Task.Run(() =>
        {
            using var zipArchive = ZipFile.OpenRead(backupFilePath);
            foreach (var entry in zipArchive.Entries)
            {
                if (_isCancelling) return;

                // Skip directories
                if (string.IsNullOrEmpty(entry.Name)) continue;

                var preview = new FilePreview
                {
                    FileName = entry.FullName,
                    Size = entry.Length
                };

                previews.Add(preview);
                totalSize += entry.Length;

                // Notify UI of progress and status
                var progress = (int)((float)previews.Count / zipArchive.Entries.Count * 100);
                PreviewProgressChanged?.Invoke(progress);
                PreviewStatusChanged?.Invoke((preview, totalSize));
                StatusChanged?.Invoke(string.Format(Strings.RestoreBackupLoadingPreview, progress));

                if (_isCancelling) return;
            }
        });

        RestorePreviewCompleted?.Invoke((previews, totalSize));
    }

    /// <summary>
    /// Asynchronously restores the contents of a backup zip file to the specified restore path, handling external song files.
    /// </summary>
    /// <param name="backupFilePath">The path to the backup zip file.</param>
    /// <param name="restorePath">The path to the directory the .zip file should be restored to.</param>
    /// <returns>A task representing the restore operation.</returns>
    public async Task RestoreBackupAsync(string backupFilePath, string restorePath)
    {
        //TODO: translations
        if (string.IsNullOrEmpty(backupFilePath)) throw new ArgumentNullException(nameof(backupFilePath));
        if (string.IsNullOrEmpty(restorePath)) throw new ArgumentNullException(nameof(restorePath));
        if (!File.Exists(backupFilePath)) throw new FileNotFoundException("Backup file not found.", backupFilePath);

        if (_isCancelling) return;

        try
        {
            await Task.Run(() =>
            {
                if (_isCancelling) return;

                var externalSongMappings = new Dictionary<string, string>();

                using var zipArchive = ZipFile.OpenRead(backupFilePath);

                foreach (var entry in zipArchive.Entries)
                {
                    if (_isCancelling) return;

                    var entryName = entry.FullName;

                    // Skip directories and external directory
                    if (string.IsNullOrEmpty(entry.Name)) continue;
                    if (entryName.StartsWith("external\\")) continue;

                    if (entryName.StartsWith("externalPaths.txt"))
                    {
                        using var reader = new StreamReader(entry.Open(), Encoding.UTF8);
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            if (line == null) continue;
                            var parts = line.Split('|');
                            if (parts.Length == 2) externalSongMappings[parts[0]] = parts[1];
                        }

                        continue;
                    }

                    var destinationPath = Path.Combine(restorePath, entryName);

                    // Ensure the directory exists
                    var destinationDirectory = Path.GetDirectoryName(destinationPath);
                    if (destinationDirectory != null && !Directory.Exists(destinationDirectory))
                        Directory.CreateDirectory(destinationDirectory);

                    var progress = (int)((float)zipArchive.Entries.Count / zipArchive.Entries.Count * 100);
                    ProgressChanged?.Invoke(progress);
                    var status = string.Format(Strings.RestoreProgressChanged, entryName);
                    StatusChanged?.Invoke(status);

                    entry.ExtractToFile(destinationPath, true);
                }

                // Restore external songs
                foreach (var (songFileName, originalPath) in externalSongMappings)
                {
                    var entry = zipArchive.GetEntry(Path.Combine("external", songFileName));

                    if (entry == null) continue;

                    var destinationPath = Path.Combine(originalPath);
                    var destinationDirectory = Path.GetDirectoryName(destinationPath);
                    if (destinationDirectory != null && !Directory.Exists(destinationDirectory))
                        Directory.CreateDirectory(destinationDirectory);

                    var progress = (int)((float)externalSongMappings.Count / externalSongMappings.Count * 100);
                    ProgressChanged?.Invoke(progress);
                    StatusChanged?.Invoke(string.Format(Strings.RestoreSongProgressChanged, songFileName));

                    entry.ExtractToFile(destinationPath, true);
                }
            });

            var status = Strings.RestoreCompleted;
            if (_isCancelling) return;
            StatusChanged?.Invoke(status);

            if (_isCancelling) return;
            RestoreCompleted?.Invoke(true, restorePath);
        }
        catch (Exception ex)
        {
            var status = string.Format(Strings.RestoreFailedException, ex.Message);

            if (_isCancelling) return;
            StatusChanged?.Invoke(status);

            if (_isCancelling) return;
            RestoreCompleted?.Invoke(false, restorePath);
            throw;
        }
    }

    private string[] GetFilesIncludingSongs(string stagingPath)
    {
        var files = GetFilesOnly(stagingPath).ToList();

        StationManager.Instance.StationsAsList.ForEach(station =>
        {
            if (station.TrackedObject.Songs.Count > 0)
                station.TrackedObject.Songs.ForEach(song =>
                {
                    if (string.IsNullOrEmpty(song.FilePath)) return;

                    if (File.Exists(song.FilePath))
                        files.Add(song.FilePath);
                });
        });

        return [.. files];
    }

    /// <summary>
    /// Retrieve the files from the staging folder, excluding audio files.
    /// </summary>
    /// <param name="stagingPath">The staging path.</param>
    /// <returns>An array of file paths.</returns>
    private string[] GetFilesOnly(string stagingPath)
    {
        try
        {
            return FileHelper.SafeEnumerateFiles(stagingPath, "*.*", SearchOption.AllDirectories)
                .Where(file => !StationManager.Instance.ValidAudioExtensions.Contains(Path.GetExtension(file)))
                .ToArray();
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<BackupManager>("GetFilesOnly")
                .Error(ex, "Failed to get files from staging folder.");
            return [];
        }
    }

    public void CancelBackup()
    {
        _isCancelling = true;
    }

    public void CancelRestore()
    {
        _isCancelling = true;
    }
}