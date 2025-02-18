// PathHelper.cs : RadioExt-Helper
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
using AetherUtils.Core.Logging;
using RadioExt_Helper.config;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Security.AccessControl;
using System.Security.Principal;
using Image = SixLabors.ImageSharp.Image;

namespace RadioExt_Helper.utility;

/// <summary>
///     Helper class to get the various paths associated with the game and provides some helper methods for working with paths.
/// </summary>
public static class PathHelper
{
    /// <summary>
    ///    The list of paths that are always forbidden for use as the staging path.
    /// </summary>
    private static readonly List<string> AlwaysForbiddenPaths =
    [
        Environment.GetFolderPath(Environment.SpecialFolder.Windows).ToLowerInvariant(),
        Environment.GetFolderPath(Environment.SpecialFolder.System).ToLowerInvariant(),
        Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).ToLowerInvariant(),
        Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86).ToLowerInvariant(),
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToLowerInvariant(),
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).ToLowerInvariant(),
        Path.GetPathRoot(Environment.SystemDirectory)?.ToLowerInvariant() ?? string.Empty
    ];

    /// <summary>
    /// Retrieves the base game path (the folder containing <c>bin</c>) from the Cyberpunk 2077 executable by showing a file dialog.
    /// </summary>
    /// <returns>The base path of the game or <see cref="string.Empty" /> if path couldn't be determined.</returns>
    public static string GetGamePath()
    {
        return GetGamePath(false);
    }

    /// <summary>
    /// Retrieves the base game path (the folder containing <c>bin</c>) from the Cyberpunk 2077 executable. Optionally, indicate if the dialog should loop until a valid file is selected.
    /// </summary>
    /// <param name="shouldLoop">Indicates whether the file dialog should continue showing until a valid file is selected.</param>
    /// <returns>The base path of the game or <see cref="string.Empty" /> if path couldn't be determined.</returns>
    public static string GetGamePath(bool shouldLoop)
    {
        OpenFileDialog dialog = new()
        {
            Filter = Strings.GamePathFilter + @"|Cyberpunk2077.exe",
            Title = Strings.OpenGameExecutable
        };

        try
        {
            var gamePath = string.Empty;
            do
            {
                if (dialog.ShowDialog() != DialogResult.OK) continue;

                if (dialog.FileName.Contains("Cyberpunk2077"))
                    gamePath = dialog.FileName;
                else
                    MessageBox.Show(Strings.NonCyberpunkExe);
            } while (gamePath.Equals(string.Empty) && shouldLoop);

            var name = Directory.GetParent(gamePath)?.FullName;
            if (name != null)
            {
                var fullName = Directory.GetParent(name)?.FullName;
                if (fullName != null)
                {
                    var basePath = Directory.GetParent(fullName)?.FullName;
                    return basePath ?? string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("PathHelper.GetGamePath")
                .Error(ex, "Error retrieving base game path.");
            return string.Empty;
        }

        return string.Empty;
    }

    /// <summary>
    /// Retrieves the staging path (the folder containing radio stations before copied to the game) by showing a folder dialog.
    /// </summary>
    /// <returns>The base path of the game or <see cref="string.Empty" /> if path couldn't be determined.</returns>
    public static string GetStagingPath()
    {
        return GetStagingPath(false);
    }

    /// <summary>
    ///     Retrieves the staging path (the folder containing radio stations before copied to the game). Optionally, indicate if the dialog should loop until a valid file is selected.
    /// </summary>
    /// <param name="shouldLoop">Indicates whether the file dialog should continue showing until a valid file is selected.</param>
    /// <returns>The base path of the game or <see cref="string.Empty" /> if path couldn't be determined.</returns>
    public static string GetStagingPath(bool shouldLoop)
    {
        FolderBrowserDialog dialog = new()
        {
            Description = Strings.StagingPathHelp,
            UseDescriptionForTitle = true
        };

        try
        {
            var stagingPath = string.Empty;
            do
            {
                if (dialog.ShowDialog() != DialogResult.OK) continue;

                stagingPath = dialog.SelectedPath;
            } while (stagingPath.Equals(string.Empty) && shouldLoop);

            return stagingPath;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("PathHelper.GetStagingPath")
                .Error(ex, "Error retrieving staging path.");
            return string.Empty;
        }
    }

    /// <summary>
    ///     Checks for and retrieves (if it exists) the path to the <c>radioExt</c> folder where custom radios are placed.
    /// </summary>
    /// <returns>The path to the radioExt folder, or <see cref="string.Empty" /> if the path couldn't be determined.</returns>
    public static string GetRadioExtPath(string gameBasePath)
    {
        var path = Path.Combine(gameBasePath, "bin", "x64", "plugins", "cyber_engine_tweaks", "mods", "radioExt");
        try
        {
            return Path.Exists(path) ? path : string.Empty;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("PathHelper.GetRadioExtPath")
                .Error(ex, "Error retrieving path to radioExt mod's folder.");
            return string.Empty;
        }
    }

    /// <summary>
    ///     Get the path to the <c>radios</c> folder under the radioExt mod folder. This method does not check if the path
    ///     exists.
    /// </summary>
    /// <param name="gameBasePath">The base path of the game.</param>
    /// <returns>The path to the <c>radios</c> folder, based on the base path of the game.</returns>
    public static string GetRadiosPath(string gameBasePath)
    {
        return Path.Combine(GetRadioExtPath(gameBasePath), "radios");
    }

    /// <summary>
    /// Determines if a path is a sub-path (i.e., starts with) of another path.
    /// </summary>
    /// <param name="basePath">The base path to check against.</param>
    /// <param name="subPath">The path to check against the base path.</param>
    /// <returns><c>true</c> if the sub path is part of the base path; <c>false</c> otherwise or if an error occured.</returns>
    public static bool IsSubPath(string basePath, string subPath)
    {
        try
        {
            // Check if the paths are valid
            if (string.IsNullOrEmpty(basePath) || string.IsNullOrEmpty(subPath) || !Directory.Exists(basePath))
                return false;

            // Get the full paths
            var fullBasePath =
                Path.GetFullPath(basePath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) +
                Path.DirectorySeparatorChar;
            var fullSubPath = Path.GetFullPath(subPath);

            // Check if the fullSubPath starts with fullBasePath
            return fullSubPath.StartsWith(fullBasePath, StringComparison.OrdinalIgnoreCase);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("PathHelper.IsSubPath")
                .Error(ex, "An error occurred while checking if the path is a subpath.");
            return false;
        }
    }

    /// <summary>
    /// Get a relative path to the <paramref name="fullPath"/> from the <paramref name="stagingPath"/>.
    /// </summary>
    /// <param name="fullPath">The full path.</param>
    /// <param name="stagingPath">The path to the staging folder.</param>
    /// <returns></returns>
    public static string GetRelativePath(string fullPath, string stagingPath)
    {
        try
        {
            Uri fullUri = new(fullPath);
            Uri stagingUri = new(stagingPath);

            if (fullUri.Scheme != stagingUri.Scheme)
                return fullPath;

            var relativeUri = stagingUri.MakeRelativeUri(fullUri);
            var relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            return string.IsNullOrEmpty(relativePath)
                ? fullPath
                : relativePath.Replace('/', '\\'); // Replace forward slashes with backslashes for Windows paths
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("PathHelper.GetRelativePath")
                .Error(ex, "An error occurred while getting the relative path.");
            return fullPath;
        }
    }

    /// <summary>
    /// Get a value indicating whether the specified file is a valid audio file based on the extension and <see cref="models.ValidAudioFiles"/>.
    /// </summary>
    /// <param name="filePath">The path to the file to check.</param>
    /// <returns><c>true</c> if the file is a valid audio file; <c>false</c> otherwise.</returns>
    public static bool IsValidAudioFile(string filePath)
    {
        var extension = FileHelper.GetExtension(filePath);
        return StationManager.Instance.ValidAudioExtensions.Contains(extension);
    }

    /// <summary>
    /// Get a value indicating whether the specified file is a valid archive file based on the extension and <see cref="models.ValidArchiveFiles"/>.
    /// </summary>
    /// <param name="filePath">The path to the file to check.</param>
    /// <returns><c>true</c> if the file is a valid archive file; <c>false</c> otherwise.</returns>
    public static bool IsValidArchiveFile(string filePath)
    {
        var extension = FileHelper.GetExtension(filePath);
        return StationManager.Instance.ValidArchiveExtensions.Contains(extension);
    }

    /// <summary>
    /// Get a value indicating whether the specified file is a valid image file based on the extension and <see cref="models.ValidImageFiles"/>.
    /// </summary>
    /// <param name="filePath">The path to the file to check.</param>
    /// <returns><c>true</c> if the file is a valid image file; <c>false</c> otherwise.</returns>
    public static bool IsValidImageFile(string filePath)
    {
        var extension = FileHelper.GetExtension(filePath);
        return StationManager.Instance.ValidImageExtensions.Contains(extension);
    }

    /// <summary>
    /// Replaces invalid path characters and specific characters (like the apostrophe (') which can causing issues with pathing.
    /// </summary>
    /// <param name="path">The path to sanitize.</param>
    /// <returns>The sanitized path.</returns>
    public static string SanitizePath(string path)
    {
        try
        {
            path = Path.GetInvalidPathChars().Aggregate(path, (current, c) => current.Replace(c, '_'));
            path = path.Replace("'", "_"); // Replace specific characters causing issues
            return path;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("PathHelper.SanitizePath")
                .Error(ex, "An error occurred while sanitizing the path.");
            return path;
        }
    }

    /// <summary>
    /// Get the list of always forbidden paths as a list of <see cref="ForbiddenKeyword"/> objects. These paths are the system directories and program files directories.
    /// </summary>
    /// <returns>A list of <see cref="ForbiddenKeyword"/> objects defining the path.</returns>
    public static List<ForbiddenKeyword> GetAlwaysForbiddenPaths() => AlwaysForbiddenPaths.Select(path => new ForbiddenKeyword { Group = Strings.SystemPaths, Keyword = path, IsForbidden = true }).ToList();

    /// <summary>
    /// Download a file from the specified URL to the specified destination file path.
    /// </summary>
    /// <param name="fileUrl">The URL of the file to download.</param>
    /// <param name="destinationFilePath">The path to save the file on disk, including file name.</param>
    /// <returns>A task that represents the current async operation.</returns>
    public static async Task DownloadFileAsync(string fileUrl, string destinationFilePath)
    {
        using var client = new HttpClient();
        using var response = await client.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        await using var contentStream = await response.Content.ReadAsStreamAsync();
        var fileStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192,
            true);
        await using var stream = fileStream.ConfigureAwait(false);

        await contentStream.CopyToAsync(fileStream);
    }

    /// <summary>
    /// Download an image from the specified URL and convert it to a <see cref="Bitmap"/>.
    /// </summary>
    /// <param name="imageUrl">The URL of the image to download.</param>
    /// <returns>A task, that when completed, contains the downloaded image as a bitmap or the default missing image if the image could not be downloaded.</returns>
    public static async Task<Bitmap> DownloadImageAsync(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            return ConvertToBitmap(null);

        using var client = new HttpClient();
        using var response = await client.GetAsync(imageUrl, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        await using var contentStream = await response.Content.ReadAsStreamAsync();
        var image = await Image.LoadAsync<Rgba32>(contentStream);
        return ConvertToBitmap(image);
    }

    /// <summary>
    /// Convert a <see cref="Image"/> to a <see cref="Bitmap"/>.
    /// </summary>
    /// <param name="image">The <see cref="Image"/> to convert.</param>
    /// <returns>If <paramref name="image"/> is <c>null</c>, returns the default missing image;
    /// otherwise, returns a <see cref="Bitmap"/> representing the <see cref="Image"/>.</returns>
    public static Bitmap ConvertToBitmap(Image<Rgba32>? image)
    {
        if (image == null)
            return Resources.missing_16x16;

        using var memoryStream = new MemoryStream();

        image.SaveAsBmp(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new Bitmap(memoryStream);
    }

    /// <summary>
    /// Extract a <c>.zip</c> file to the specified destination directory.
    /// </summary>
    /// <param name="zipFilePath">The fully qualified path to a <c>.zip</c> file.</param>
    /// <param name="destinationDirectory">The directory to extract the contents of the <c>.zip</c> file into.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If the <paramref name="zipFilePath"/> or the <paramref name="destinationDirectory"/> is <c>null</c> or empty.</exception>
    /// <exception cref="FileNotFoundException">If the <paramref name="zipFilePath"/> did not exist on the filesystem.</exception>
    public static async Task ExtractZipFileAsync(string zipFilePath, string destinationDirectory)
    {
        if (string.IsNullOrEmpty(zipFilePath)) throw new ArgumentNullException(nameof(zipFilePath));
        if (string.IsNullOrEmpty(destinationDirectory)) throw new ArgumentNullException(nameof(destinationDirectory));
        if (!File.Exists(zipFilePath)) throw new FileNotFoundException("Zip file not found.", zipFilePath);

        Directory.CreateDirectory(destinationDirectory);

        await Task.Run(() =>
        {
            using var archive = ZipArchive.Open(zipFilePath);

            foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
            {
                var entryKey = entry.Key;
                if (string.IsNullOrEmpty(entryKey)) continue;

                var destinationPath = Path.Combine(destinationDirectory, entryKey);
                var destinationDir = Path.GetDirectoryName(destinationPath);

                if (string.IsNullOrEmpty(destinationDir)) continue;

                Directory.CreateDirectory(destinationDir);
                entry.WriteToFile(destinationPath, new ExtractionOptions { ExtractFullPath = true, Overwrite = true });
            }
        });
    }

    /// <summary>
    /// Clear all files and folders from the specified directory.
    /// </summary>
    /// <param name="directoryPath">The path to the directory clear.</param>
    public static void ClearDirectory(string directoryPath)
    {
        try
        {
            // Check if the directory exists
            if (Directory.Exists(directoryPath))
            {
                // Delete all files
                foreach (var file in FileHelper.SafeEnumerateFiles(directoryPath))
                    File.Delete(file);

                // Delete all subdirectories and their contents
                foreach (var directory in FileHelper.SafeEnumerateDirectories(directoryPath))
                    if (StationManager.Instance.IsProtectedFolder(directory))
                        ClearDirectory(directory); //recurse down until we no longer have protected folders.
                    else
                        Directory.Delete(directory, true);
            }
            else
            {
                AuLogger.GetCurrentLogger("PathHelper.ClearDirectory")
                    .Warn($"The directory to clear did not exist: {directoryPath}");
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("PathHelper.ClearDirectory")
                .Error(ex, "An error occured while clearing the directory.");
        }
    }

    /// <summary>
    /// Grant's access to the path specified by the filepath. Used mainly for accessing the log folder and reading the log file.
    /// </summary>
    /// <param name="filePath">The path to grant access on.</param>
    public static void GrantAccess(string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        var directoryInfo = fileInfo.Directory;

        if (directoryInfo == null) return;

        var security = directoryInfo.GetAccessControl();
        var user = WindowsIdentity.GetCurrent().Name;
        security.AddAccessRule(new FileSystemAccessRule(
            user,
            FileSystemRights.FullControl,
            AccessControlType.Allow));
        directoryInfo.SetAccessControl(security);
    }

    /// <summary>
    /// Checks if the specified path is a forbidden path and thus invalid for the staging folder (or other purposes).
    /// </summary>
    /// <param name="stagingPath">The proposed staging path.</param>
    /// <returns>A <see cref="ForbiddenPathResult"/> specifying the reason why the path was forbidden or not.</returns>
    public static ForbiddenPathResult IsForbiddenPath(string stagingPath)
    {
        // Check if the staging path is empty or null (not forbidden)
        if (string.IsNullOrEmpty(stagingPath))
            return new ForbiddenPathResult { IsForbidden = false, Reason = ForbiddenPathReason.None };

        // Normalize the staging path for comparison
        var normalizedStagingPath = Path.GetFullPath(stagingPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar))
            .ToLowerInvariant();

        // Check if the staging path is at the root of any drive
        if (IsRootDirectory(normalizedStagingPath))
        {
            return new ForbiddenPathResult { IsForbidden = true, Reason = ForbiddenPathReason.RootDirectory };
        }

        // Check if the staging path is within any forbidden path
        foreach (var forbiddenPath in AlwaysForbiddenPaths)
        {
            if (normalizedStagingPath.StartsWith(forbiddenPath))
            {
                return new ForbiddenPathResult { IsForbidden = true, Reason = ForbiddenPathReason.SystemDirectory };
            }
        }

        // Check if any forbidden keyword is present in the path
        if (GlobalData.ConfigManager.Get("forbiddenKeywords") is List<ForbiddenKeyword> forbiddenKeywords)
        {
            if (forbiddenKeywords.Where(keyword => keyword.IsForbidden).Any(keyword => normalizedStagingPath.Contains(keyword.Keyword, StringComparison.CurrentCultureIgnoreCase)))
            {
                return new ForbiddenPathResult { IsForbidden = true, Reason = ForbiddenPathReason.KeywordMatch };
            }
        }

        // Check if the staging path or any of its parent/child directories contains the __vortex_staging_folder marker
        if (ContainsVortexMarker(normalizedStagingPath))
        {
            return new ForbiddenPathResult { IsForbidden = true, Reason = ForbiddenPathReason.VortexFolder };
        }

        // If none of the forbidden checks triggered, the path is valid
        return new ForbiddenPathResult { IsForbidden = false, Reason = ForbiddenPathReason.None };
    }

    /// <summary>
    /// Checks if the specified path is the root of any drive.
    /// </summary>
    /// <param name="path">The path to check.</param>
    /// <returns>True if the path is a root directory, otherwise false.</returns>
    private static bool IsRootDirectory(string path)
    {
        try
        {
            // Get the root directory of the given path
            var rootPath = Path.GetPathRoot(path)?.ToLowerInvariant();

            // Normalize and compare to ensure the path itself is a root
            return string.Equals(rootPath?.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar), path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar), StringComparison.OrdinalIgnoreCase);
            //return !string.IsNullOrEmpty(rootPath) && string.Equals(path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar), rootPath, StringComparison.OrdinalIgnoreCase);
        }
        catch (Exception ex)
        {
            // Log exception or handle as needed
            AuLogger.GetCurrentLogger("PathHelper.IsRootDirectory").Error(ex, "An error occured while checking path.");
        }

        return false;
    }

    /// <summary>
    /// Recursively checks if the specified path or any of its parent directories contains the Vortex marker file.
    /// </summary>
    /// <param name="path">The path to check.</param>
    /// <returns>True if the Vortex marker file is found, otherwise false.</returns>
    private static bool ContainsVortexMarker(string path)
    {
        const string vortexMarkerFile = "__vortex_staging_folder";
        try
        {
            var directory = new DirectoryInfo(path);
            while (directory != null)
            {
                // Check if the Vortex marker file exists in the current directory
                if (File.Exists(Path.Combine(directory.FullName, vortexMarkerFile)))
                    return true;

                // Move to the parent directory
                directory = directory.Parent;
            }

            // Check for the marker file in all child directories recursively
            return ContainsVortexMarkerInChildren(new DirectoryInfo(path));
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("PathHelper.ContainsVortexMarker").Error(ex, "An error occurred while checking for the Vortex marker file.");
        }

        return false;
    }

    /// <summary>
    /// Recursively checks if the marker file exists in any child directories of the given directory.
    /// </summary>
    /// <param name="directory">The directory to search in.</param>
    /// <returns>True if the marker file is found in any child directory, otherwise false.</returns>
    private static bool ContainsVortexMarkerInChildren(DirectoryInfo directory)
    {
        try
        {
            // Check if the marker file exists in the current directory
            if (File.Exists(Path.Combine(directory.FullName, "__vortex_staging_folder")))
                return true;

            // Recursively search in all subdirectories
            if (directory.GetDirectories().Any(ContainsVortexMarkerInChildren))
                return true;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("PathHelper.ContainsVortexMarkerInChildren").Error(ex, "An error occured while checking for the Vortex marker file.");
        }

        return false;
    }
}