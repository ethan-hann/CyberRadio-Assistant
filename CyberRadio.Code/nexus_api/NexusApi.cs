// NexusApi.cs : RadioExt-Helper
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

using AetherUtils.Core.Logging;
using Pathoschild.FluentNexus;
using Pathoschild.FluentNexus.Models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Image = SixLabors.ImageSharp.Image;

namespace RadioExt_Helper.nexus_api;

/// <summary>
/// Provides a static class for interacting with the Nexus API; a high-level wrapper around <see cref="Pathoschild.FluentNexus.NexusClient"/>.
/// <para>This class maintains a single API user, <see cref="CurrentApiUser"/> and a reference to the underlying <see cref="Pathoschild.FluentNexus.NexusClient"/>.</para>
/// </summary>
public static class NexusApi
{
    /// <summary>
    /// Get a value indicating whether the API key is authenticated with the Nexus API.
    /// </summary>
    public static bool IsAuthenticated { get; private set; }

    /// <summary>
    /// The current user from the Nexus API. This is <c>null</c> if the API key is not authenticated.
    /// </summary>
    public static User? CurrentApiUser { get; private set; }

    /// <summary>
    /// Get the Nexus client instance for making requests to the Nexus API.
    /// </summary>
    public static NexusClient? NexusClient { get; private set; }

    /// <summary>
    /// Clear the current authentication state.
    /// </summary>
    public static void ClearAuthentication()
    {
        IsAuthenticated = false;
        CurrentApiUser = null;
        NexusClient = null;
    }

    /// <summary>
    /// Authenticates the API key with the Nexus API.
    /// </summary>
    /// <param name="apiKey">The API key to validate.</param>
    /// <returns><c>true</c> if the API key is valid for requests; <c>false</c> otherwise.</returns>
    public static async Task<bool> AuthenticateApiKey(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            AuLogger.GetCurrentLogger("NexusApi.AuthenticateApiKey")
                .Error("API key is empty or null.");
            IsAuthenticated = false;
            CurrentApiUser = null;
            return IsAuthenticated;
        }

        NexusClient = new NexusClient(apiKey, "CyberRadioAssistant", GlobalData.AppVersion.ToString());
        try
        {
            var user = await NexusClient.Users.ValidateAsync();
            IsAuthenticated = user != null;
            CurrentApiUser = user;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("NexusApi.AuthenticateApiKey")
                .Error(ex, "Couldn't authenticate API key with Nexus API.");
            IsAuthenticated = false;
            CurrentApiUser = null;
        }

        return IsAuthenticated;
    }

    /// <summary>
    /// Get the main image for the specified mod from the Nexus API.
    /// </summary>
    /// <param name="mod">The <see cref="Mod"/> to get the main image of.</param>
    /// <returns>A task, that when completed, contains the mod's main image or the default missing image if the mod did not have an image.</returns>
    public static async Task<Bitmap> GetModImage(Mod mod)
    {
        if (NexusClient == null) return Resources.missing_16x16;

        try
        {
            var imageUrl = mod.PictureUrl;
            var image = await DownloadImageAsync(imageUrl.AbsoluteUri);
            return image;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("NexusApi.GetModImage")
                .Error(ex, "Couldn't retrieve mod image. Using fall back image.");
            return Resources.missing_16x16;
        }
    }

    /// <summary>
    /// Get the user's profile image from the Nexus API.
    /// </summary>
    /// <returns>A task, that when completed, contains the user's profile image or the default profile image if the user did not have an image.</returns>
    public static async Task<Bitmap> GetUserImage()
    {
        if (!IsAuthenticated) return ConvertToBitmap(null);
        if (NexusClient == null) return ConvertToBitmap(null);
        if (CurrentApiUser == null) return ConvertToBitmap(null);

        try
        {
            var image = await DownloadImageAsync(CurrentApiUser?.ProfileUrl ?? string.Empty);
            return image;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("NexusApi.GetUserImage")
                .Error(ex, "Couldn't retrieve user profile image. Using fall back image.");
            return ConvertToBitmap(null);
        }
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
    private static async Task<Bitmap> DownloadImageAsync(string imageUrl)
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
    private static Bitmap ConvertToBitmap(Image<Rgba32>? image)
    {
        if (image == null)
            return Resources.missing_16x16;

        using var memoryStream = new MemoryStream();

        image.SaveAsBmp(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new Bitmap(memoryStream);
    }

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
                    var destinationPath = Path.Combine(destinationDirectory, entry.Key);
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                    entry.WriteToFile(destinationPath, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                }
            }
        });
    }
}