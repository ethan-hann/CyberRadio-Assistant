// // TinyMceInstaller.cs : RadioExt-Helper
// // Copyright (C) 2025  Ethan Hann
// //
// // This program is free software: you can redistribute it and/or modify
// // it under the terms of the GNU General Public License as published by
// // the Free Software Foundation, either version 3 of the License, or
// // (at your option) any later version.
// //
// // This program is distributed in the hope that it will be useful,
// // but WITHOUT ANY WARRANTY; without even the implied warranty of
// // MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// // GNU General Public License for more details.
// //
// // You should have received a copy of the GNU General Public License
// // along with this program.  If not, see <https://www.gnu.org/licenses/>.

#region

using System.IO.Compression;

#endregion

namespace RadioExt_Helper.utility;

/// <summary>
///     Ensures TinyMCE is downloaded and extracted into the expected local folder:
///     %LOCALAPPDATA%\RadioExt-Helper\tinymce
///     The TinyMCE control expects:
///     TinyMceRootFolder\tinymce\js\tinymce\tinymce.min.js
/// </summary>
public static class TinyMceInstaller
{
    /// <summary>
    ///     Root folder where the TinyMCE zip will be extracted.
    ///     Example: C:\Users\you\AppData\Local\RadioExt-Helper\tinymce
    /// </summary>
    public static string TinyMceRootFolder { get; } =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "RadioExt-Helper",
            "tinymce");

    /// <summary>
    ///     Full path to the TinyMCE core script that the control will look for.
    /// </summary>
    public static string TinyMceScriptPath { get; } =
        Path.Combine(
            TinyMceRootFolder,
            "tinymce",
            "js",
            "tinymce",
            "tinymce.min.js");

    /// <summary>
    ///     Full path to the TinyMCE languages folder.
    /// </summary>
    public static string TinyMceLangsPath { get; } =
        Path.Combine(
            TinyMceRootFolder,
            "tinymce",
            "js",
            "tinymce",
            "langs");

    /// <summary>
    ///     Returns true if TinyMCE appears to already be installed locally.
    /// </summary>
    public static bool IsInstalled()
    {
        return File.Exists(TinyMceScriptPath);
    }

    /// <summary>
    ///     Returns true if TinyMCE appears to have the languages ready to go.
    /// </summary>
    public static bool IsLanguagesInstalled()
    {
        return File.Exists(Path.Combine(TinyMceLangsPath, "ar.js"));
    }

    /// <summary>
    ///     Ensures TinyMCE is available locally.
    ///     If it's already installed, this is a no-op.
    ///     Otherwise, downloads the provided zip URL and extracts it to <see cref="TinyMceRootFolder" />.
    /// </summary>
    /// <param name="downloadUrl">Direct URL to the TinyMCE self-hosted zip file.</param>
    /// <param name="progress">Optional progress reporter for status messages.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public static async Task EnsureInstalledAsync(
        string downloadUrl,
        IProgress<string>? progress = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(downloadUrl))
            throw new ArgumentException("downloadUrl is required.", nameof(downloadUrl));

        if (IsInstalled())
        {
            progress?.Report("TinyMCE already installed.");
            return;
        }

        progress?.Report("TinyMCE not found. Preparing to download...");

        Directory.CreateDirectory(TinyMceRootFolder);

        var tempZipPath = Path.Combine(
            Path.GetTempPath(),
            $"tinymce_{Guid.NewGuid():N}.zip");

        try
        {
            await DownloadZipAsync(downloadUrl, tempZipPath, progress, cancellationToken)
                .ConfigureAwait(false);

            await ExtractZipAsync(tempZipPath, TinyMceRootFolder, true, progress, cancellationToken)
                .ConfigureAwait(false);

            if (!IsInstalled())
                throw new InvalidOperationException(
                    $"TinyMCE zip was extracted, but '{TinyMceScriptPath}' was not found. " +
                    "Verify the zip contents and expected folder structure.");

            progress?.Report("TinyMCE installation complete.");
        }
        finally
        {
            try
            {
                if (File.Exists(tempZipPath))
                    File.Delete(tempZipPath);
            }
            catch
            {
                // Non-fatal; if temp cleanup fails, we just leave the file.
            }
        }
    }

    /// <summary>
    ///     Ensures TinyMCE languages are available locally.
    ///     If they're already installed, this is a no-op.
    ///     Otherwise, downloads the provided zip URL and extracts it to <see cref="TinyMceLangsPath" />.
    /// </summary>
    /// <param name="downloadUrl">Direct URL to the TinyMCE languages zip file.</param>
    /// <param name="progress">Optional progress reporter for status messages.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task EnsureLanguagesInstalled(string downloadUrl,
        IProgress<string>? progress = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(downloadUrl))
            throw new ArgumentException("downloadUrl is required.", nameof(downloadUrl));
        if (Directory.Exists(TinyMceLangsPath) && Directory.GetFiles(TinyMceLangsPath).Length > 1)
        {
            progress?.Report("TinyMCE languages already installed.");
            return;
        }

        progress?.Report("TinyMCE languages not found. Preparing to download...");
        Directory.CreateDirectory(TinyMceRootFolder);
        var tempZipPath = Path.Combine(
            Path.GetTempPath(),
            $"tinymce_langs_{Guid.NewGuid():N}.zip");
        try
        {
            await DownloadZipAsync(downloadUrl, tempZipPath, progress, cancellationToken)
                .ConfigureAwait(false);
            await ExtractZipAsync(tempZipPath, Directory.GetParent(TinyMceLangsPath).FullName, false, progress,
                    cancellationToken)
                .ConfigureAwait(false);
            if (!Directory.Exists(TinyMceLangsPath) || Directory.GetFiles(TinyMceLangsPath).Length == 0)
                throw new InvalidOperationException(
                    $"TinyMCE languages zip was extracted, but '{TinyMceLangsPath}' was not found or is empty. " +
                    "Verify the zip contents and expected folder structure.");
            progress?.Report("TinyMCE languages installation complete.");
        }
        finally
        {
            try
            {
                if (File.Exists(tempZipPath))
                    File.Delete(tempZipPath);
            }
            catch
            {
                // Non-fatal; if temp cleanup fails, we just leave the file.
            }
        }
    }

    private static async Task DownloadZipAsync(
        string downloadUrl,
        string destinationPath,
        IProgress<string>? progress,
        CancellationToken cancellationToken)
    {
        progress?.Report("Downloading TinyMCE package...");

        using HttpClient httpClient = new()
        {
            Timeout = TimeSpan.FromMinutes(5)
        };

        using var response = await httpClient.GetAsync(
                downloadUrl,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        await using var httpStream = await response.Content
            .ReadAsStreamAsync(cancellationToken)
            .ConfigureAwait(false);

        await using FileStream fileStream = new(
            destinationPath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None);

        await httpStream.CopyToAsync(fileStream, cancellationToken)
            .ConfigureAwait(false);

        progress?.Report("TinyMCE package downloaded.");
    }

    private static Task ExtractZipAsync(
        string zipPath,
        string targetFolder,
        bool replaceExisting,
        IProgress<string>? progress,
        CancellationToken cancellationToken)
    {
        progress?.Report("Extracting zip file...");

        // Replace existing folder to avoid stale files
        if (replaceExisting)
            if (Directory.Exists(targetFolder))
                Directory.Delete(targetFolder, true);

        Directory.CreateDirectory(targetFolder);

        // ZipFile.ExtractToDirectory is synchronous; wrap in Task.Run to keep caller async.
        return Task.Run(() =>
        {
            ZipFile.ExtractToDirectory(zipPath, targetFolder);
            progress?.Report("Zip file extracted.");
        }, cancellationToken);
    }
}