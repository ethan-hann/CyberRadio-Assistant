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
            AuLogger.GetCurrentLogger("NexusApi.AuthenticateApiKey").Error("API key is empty or null.");
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
            AuLogger.GetCurrentLogger("NexusApi.AuthenticateApiKey").Error(ex, "Couldn't authenticate API key with Nexus API.");
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
            var image = await PathHelper.DownloadImageAsync(imageUrl.AbsoluteUri);
            return image;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("NexusApi.GetModImage").Error(ex, "Couldn't retrieve mod image. Using fall back image.");
            return Resources.missing_16x16;
        }
    }

    /// <summary>
    /// Get the user's profile image from the Nexus API.
    /// </summary>
    /// <returns>A task, that when completed, contains the user's profile image or the default profile image if the user did not have an image.</returns>
    public static async Task<Bitmap> GetUserImage()
    {
        if (!IsAuthenticated) return PathHelper.ConvertToBitmap(null);
        if (NexusClient == null) return PathHelper.ConvertToBitmap(null);
        if (CurrentApiUser == null) return PathHelper.ConvertToBitmap(null);

        try
        {
            var image = await PathHelper.DownloadImageAsync(CurrentApiUser?.ProfileUrl ?? string.Empty);
            return image;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("NexusApi.GetUserImage").Error(ex, "Couldn't retrieve user profile image. Using fall back image.");
            return PathHelper.ConvertToBitmap(null);
        }
    }
}