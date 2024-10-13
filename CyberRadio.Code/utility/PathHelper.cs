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

using System.Security.AccessControl;
using AetherUtils.Core.Files;
using AetherUtils.Core.Logging;
using RadioExt_Helper.Properties;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Security.Cryptography;
using Image = SixLabors.ImageSharp.Image;

namespace RadioExt_Helper.utility;

/// <summary>
///     Helper class to get the various paths associated with the game.
/// </summary>
public static class PathHelper
{
    /// <summary>
    ///     Retrieves the base game path (the folder containing <c>bin</c>) from the Cyberpunk 2077 executable.
    /// </summary>
    /// <param name="shouldLoop">Indicates whether the file dialog should continue showing until a valid file is selected.</param>
    /// <returns>The base path of the game or <see cref="string.Empty" /> if path couldn't be determined.</returns>
    public static string? GetGamePath(bool shouldLoop = false)
    {
        OpenFileDialog dialog = new()
        {
            Filter = @"Game Executable|Cyberpunk2077.exe",
            Title = GlobalData.Strings.GetString("Open") ?? "Open Game Executable"
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
                    MessageBox.Show(GlobalData.Strings.GetString("NonCyberpunkExe"));
            } while (gamePath.Equals(string.Empty) && shouldLoop);

            var name = Directory.GetParent(gamePath)?.FullName;
            if (name != null)
            {
                var fullName = Directory.GetParent(name)?.FullName;
                if (fullName != null)
                {
                    var basePath = Directory.GetParent(fullName)?.FullName;
                    return basePath;
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
    ///     Retrieves the staging path (the folder containing radio stations before copied to the game).
    /// </summary>
    /// <param name="shouldLoop">Indicates whether the file dialog should continue showing until a valid file is selected.</param>
    /// <returns>The base path of the game or <see cref="string.Empty" /> if path couldn't be determined.</returns>
    public static string GetStagingPath(bool shouldLoop = false)
    {
        FolderBrowserDialog dialog = new()
        {
            Description = GlobalData.Strings.GetString("StagingPathHelp") ?? "Select the radio station staging path.",
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
            var fullBasePath = Path.GetFullPath(basePath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;
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

            if (string.IsNullOrEmpty(relativePath))
                return fullPath;

            // Replace forward slashes with backslashes for Windows paths
            return relativePath.Replace('/', '\\');
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
    /// Replaces invalid path characters and specific characters (like the apostrophe (') which can causing issues with pathing.
    /// </summary>
    /// <param name="path">The path to sanitize.</param>
    /// <returns>The sanitized path.</returns>
    public static string SanitizePath(string path)
    {
        try
        {
            foreach (var c in Path.GetInvalidPathChars()) path = path.Replace(c, '_');
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
    /// Calculates the SHA-256 hash of the file at the specified path.
    /// </summary>
    /// <param name="file">The file to calculate the hash of.</param>
    /// <param name="useLowerInvariant">Indicates whether the hash should be converted to lower-invariant case.</param>
    /// <returns>The SHA-256 hash of the file</returns>
    public static string ComputeSha256Hash(string file, bool useLowerInvariant)
    {
        // Calculate the hash of the icon file
        using var sha256 = SHA256.Create();
        using var fileStream = File.OpenRead(file);

        var hashBytes = sha256.ComputeHash(fileStream);
        var fileHash = BitConverter.ToString(hashBytes).Replace("-", "");
        fileHash = useLowerInvariant ? fileHash.ToLowerInvariant() : fileHash.ToUpperInvariant();
        return fileHash;
    }

    /// <summary>
    /// Compares two SHA256 hashes for equality.
    /// </summary>
    /// <param name="hash1">The first hash.</param>
    /// <param name="hash2">The second hash.</param>
    /// <returns><c>true</c> if the two hashes are equal; <c>false</c> otherwise.</returns>
    public static bool CompareSha256Hash(string hash1, string hash2)
    {
        return string.Equals(hash1, hash2, StringComparison.OrdinalIgnoreCase);
    }

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
            using (var archive = ZipArchive.Open(zipFilePath))
            {
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    var entryKey = entry.Key;
                    if (string.IsNullOrEmpty(entryKey)) continue;

                    var destinationPath = Path.Combine(destinationDirectory, entryKey);
                    var destinationDir = Path.GetDirectoryName(destinationPath);

                    if (string.IsNullOrEmpty(destinationDir)) continue;

                    Directory.CreateDirectory(destinationDir);
                    entry.WriteToFile(destinationPath, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                }
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
                {
                    if (StationManager.Instance.IsProtectedFolder(directory))
                        ClearDirectory(directory); //recurse down until we no longer have protected folders.
                    else
                        Directory.Delete(directory, true);
                }
            }
            else
            {
                AuLogger.GetCurrentLogger("PathHelper.ClearDirectory").Warn($"The directory to clear did not exist: {directoryPath}");
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("PathHelper.ClearDirectory").Error(ex, "An error occured while clearing the directory.");
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
        security.AddAccessRule(new FileSystemAccessRule(
            Environment.UserName,
            FileSystemRights.FullControl,
            AccessControlType.Allow));
        directoryInfo.SetAccessControl(security);
    }
}