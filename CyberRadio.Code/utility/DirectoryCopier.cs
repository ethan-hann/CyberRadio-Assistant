// DirectoryCopier.cs : RadioExt-Helper
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
using System.ComponentModel;

namespace RadioExt_Helper.utility;

/// <summary>
///     Provides methods to copy all directories and files within a parent directory to a different directory.
/// </summary>
/// <remarks>
///     Initializes the <see cref="DirectoryCopier"/> with a <see cref="BackgroundWorker"/> for progress reporting.
/// </remarks>
/// <param name="worker">The BackgroundWorker to use for progress reporting.</param>
public class DirectoryCopier(BackgroundWorker worker)
{
    private int _copiedFiles;
    private int _totalFiles;

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
            if (worker.CancellationPending) return;

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
            if (worker.CancellationPending) return;

            var destSubDir = Path.Combine(destDir, subDir.Name);
            CopyDirectory(subDir.FullName, destSubDir, copySubDirs);
        }
    }

    private void ReportProgress()
    {
        var progressPercentage = (int)((double)_copiedFiles / _totalFiles * 100);
        worker.ReportProgress(progressPercentage);
    }
}