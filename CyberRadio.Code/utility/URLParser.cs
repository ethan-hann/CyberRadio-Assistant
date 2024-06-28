using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility
{
    public static class URLParser
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string RadioGardenAPIUrl = @"https://radio.garden/api/ara/content/listen/{0}/channel.mp3";

        /// <summary>
        /// Convert a URL string from radio.garden into a usable audio stream URL.
        /// </summary>
        /// <param name="originalURL">The original web URL.</param>
        /// <returns>The API URL for the station's stream.</returns>
        public static string ConvertRadioGardenURL(string originalURL)
        {
            int lastSlashIndex = originalURL.LastIndexOf('/');
            if (lastSlashIndex == -1) { return  originalURL; }

            string stationId = originalURL[(lastSlashIndex + 1)..];
            return string.Format(RadioGardenAPIUrl, stationId);
        }

        /// <summary>
        /// Checks the URL for the response <c>{"error":"Not found"}</c> indicating the stream URL is not going to work in game.
        /// </summary>
        /// <param name="streamUrl">The URL to check.</param>
        /// <returns><c>true</c> if the stream URL is valid; <c>false</c> otherwise.</returns>
        public static async Task<bool> TestStreamURL(string streamUrl)
        {
            try
            {
                var response = await _httpClient.GetAsync(streamUrl);
                response.EnsureSuccessStatusCode(); // Throw if not a success code.

                var contentType = response.Content.Headers.ContentType?.MediaType;
                var responseContent = await response.Content.ReadAsStringAsync();

                // If response content type is JSON, return false indicating invalid stream.
                return contentType != "application/json" && contentType != "text/json";
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"Request error: {e.Message}");
                return false; // Assuming a request error means we should not return true
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unexpected error: {e.Message}");
                return false; // For any other unexpected errors
            }
        }
    }
}
