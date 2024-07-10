using System.ComponentModel;
using AetherUtils.Core.Files;

namespace RadioExt_Helper.utility;

/// <summary>
///     Provides methods to copy all directories and files within a parent directory to a different directory.
/// </summary>
public class DirectoryCopier
{
    private readonly BackgroundWorker _worker;
    private int _copiedFiles;
    private int _totalFiles;

    /// <summary>
    ///     Initializes the DirectoryCopier with a BackgroundWorker for progress reporting.
    /// </summary>
    /// <param name="worker">The BackgroundWorker to use for progress reporting.</param>
    public DirectoryCopier(BackgroundWorker worker)
    {
        _worker = worker;
    }

    public string CurrentFile { get; private set; } = string.Empty;

    /// <summary>
    ///     Copies all directories and files from the source directory to the destination directory.
    /// </summary>
    /// <param name="sourceDir">The source directory path.</param>
    /// <param name="destDir">The destination directory path.</param>
    /// <param name="copySubDirs">Indicates whether to copy subdirectories and their contents.</param>
    /// <exception cref="DirectoryNotFoundException">Thrown when the source directory does not exist or cannot be found.</exception>
    public void CopyDirectory(string sourceDir, string destDir, bool copySubDirs)
    {
        // Ensure the source directory exists
        if (!Directory.Exists(sourceDir))
            throw new DirectoryNotFoundException($"Source directory does not exist or could not be found: {sourceDir}");

        // Initialize file counts
        _totalFiles = FileHelper.SafeEnumerateFiles(sourceDir, "*.*", SearchOption.AllDirectories).Count();
        _copiedFiles = 0;

        // Create the destination directory if it doesn't exist
        Directory.CreateDirectory(destDir);

        // Copy all files using LINQ
        var files = FileHelper.SafeEnumerateFiles(sourceDir)
            .Select(file => new FileInfo(file));

        foreach (var file in files)
        {
            if (_worker.CancellationPending) return;

            var destFile = Path.Combine(destDir, file.Name);
            CurrentFile = file.Name;
            file.CopyTo(destFile, true);
            _copiedFiles++;
            ReportProgress();
        }

        if (!copySubDirs) return;

        // Copy all subdirectories recursively if specified
        var subDirs = FileHelper.SafeEnumerateDirectories(sourceDir)
            .Select(dir => new DirectoryInfo(dir));

        foreach (var subDir in subDirs)
        {
            if (_worker.CancellationPending) return;

            var destSubDir = Path.Combine(destDir, subDir.Name);
            CopyDirectory(subDir.FullName, destSubDir, copySubDirs);
        }
    }

    private void ReportProgress()
    {
        var progressPercentage = (int)((double)_copiedFiles / _totalFiles * 100);
        _worker.ReportProgress(progressPercentage);
    }
}