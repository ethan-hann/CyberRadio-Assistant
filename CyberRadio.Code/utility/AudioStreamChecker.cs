// AudioStreamChecker.cs : RadioExt-Helper
// Copyright (C) 2025  Ethan Hann
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

namespace RadioExt_Helper.utility;

/// <summary>
/// Checks the validity of an audio stream URL. The stream is valid if its content type is an audio type.
/// <para>Originally designed to check radio.garden streams but can also be used to check any URL that might be an audio stream.</para>
/// </summary>
public sealed class AudioStreamChecker
{
    private const string RadioGardenApiUrl = @"https://radio.garden/api/ara/content/listen/{0}/channel.mp3";
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="AudioStreamChecker" /> class with the specified timeout.
    /// </summary>
    /// <param name="timeout">The timespan to wait before the request times out.</param>
    public AudioStreamChecker(TimeSpan timeout)
    {
        _httpClient = new HttpClient
        {
            Timeout = timeout
        };
    }

    /// <summary>
    /// Disposes the resources used by the <see cref="AudioStreamChecker" />.
    /// </summary>
    ~AudioStreamChecker()
    {
        _httpClient.Dispose();
    }

    /// <summary>
    /// Convert a URL string from radio.garden into a usable audio stream URL.
    /// </summary>
    /// <param name="originalUrl">The original web URL.</param>
    /// <returns>The API URL for the station's stream.</returns>
    public static string ConvertRadioGardenUrl(string originalUrl)
    {
        var lastSlashIndex = originalUrl.LastIndexOf('/');
        if (lastSlashIndex == -1) return originalUrl;

        var stationId = originalUrl[(lastSlashIndex + 1)..];
        return string.Format(RadioGardenApiUrl, stationId);
    }

    /// <summary>
    /// Checks if the audio stream at the specified URL is valid. The stream is valid if its content type is an audio type.
    /// </summary>
    /// <param name="url">The URL of the audio stream to check.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a boolean indicating whether the
    /// audio stream is valid.
    /// </returns>
    public async Task<bool> IsAudioStreamValidAsync(string url)
    {
        try
        {
            // Send a HEAD request to check the stream without downloading the content
            var request = new HttpRequestMessage(HttpMethod.Head, url);
            var response = await _httpClient.SendAsync(request);

            // Check if the response is successful
            if (!response.IsSuccessStatusCode)
            {
                AuLogger.GetCurrentLogger<AudioStreamChecker>("IsAudioStreamValidAsync")
                    .Error($"Request error: {response.StatusCode}");
                return false;
            }

            // Check if the content type is an audio type
            var contentType = response.Content.Headers.ContentType?.MediaType;
            if (contentType != null && contentType.StartsWith("audio/")) return true;

            AuLogger.GetCurrentLogger<AudioStreamChecker>("IsAudioStreamValidAsync")
                .Warn("The content type is not an audio type.");
            return false;
        }
        catch (TaskCanceledException)
        {
            // Handle timeout
            if (!_httpClient.DefaultRequestHeaders.ConnectionClose.HasValue)
            {
                AuLogger.GetCurrentLogger<AudioStreamChecker>("IsAudioStreamValidAsync").Warn("Request timed out.");
                return false;
            }
        }
        catch (HttpRequestException ex)
        {
            AuLogger.GetCurrentLogger<AudioStreamChecker>("IsAudioStreamValidAsync").Error(ex, "Request exception");
            return false;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<AudioStreamChecker>("IsAudioStreamValidAsync").Error(ex, "Unexpected error");
            return false;
        }

        return false;
    }
}