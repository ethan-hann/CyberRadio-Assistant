using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AetherUtils.Core.Files;
using AetherUtils.Core.Logging;
using Pathoschild.FluentNexus;
using Pathoschild.FluentNexus.Models;
using Pathoschild.Http.Client;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using Image = SixLabors.ImageSharp.Image;

namespace RadioExt_Helper.nexus_api
{
    /// <summary>
    /// Provides a static class for interacting with the Nexus API; a high-level wrapper around <see cref="Pathoschild.FluentNexus.NexusClient"/>.
    /// </summary>
    public static class NexusApi
    {
        /// <summary>
        /// Get a value indicating whether the API key is authenticated with the Nexus API.
        /// </summary>
        public static bool IsAuthenticated { get; private set; }

        public static User? CurrentApiUser { get; private set; }

        /// <summary>
        /// Get the Nexus client instance for making requests to the Nexus API.
        /// </summary>
        public static NexusClient? NexusClient { get; private set; }

        /// <summary>
        /// Clears the current authentication state.
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
                //var response = await NexusClient.HttpClient.GetAsync("v1/users/validate.json").AsString();
                //var nexusUser = DynamicSerializer.FromJson(response);
                //IsAuthenticated = (bool)(nexusUser?.is_premium ?? false);
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

        private static Bitmap ConvertToBitmap(Image<Rgba32>? image)
        {
            if (image == null)
                return Resources.missing_16x16;

            using var memoryStream = new MemoryStream();

            image.SaveAsBmp(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new Bitmap(memoryStream);
        }

        public static async Task DownloadFileAsync(string fileUrl, string destinationFilePath)
        {
            using var client = new HttpClient();
            using var response = await client.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            await using var contentStream = await response.Content.ReadAsStreamAsync();
            var fileStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);
            await using var stream = fileStream.ConfigureAwait(false);

            await contentStream.CopyToAsync(fileStream);
        }
    }
}
