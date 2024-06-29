using System.Diagnostics;

namespace RadioExt_Helper.utility;

public sealed class AudioStreamChecker
{
    private const string RadioGardenAPIUrl = @"https://radio.garden/api/ara/content/listen/{0}/channel.mp3";
    private readonly HttpClient _httpClient;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AudioStreamChecker" /> class with the specified timeout.
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
    ///     Convert a URL string from radio.garden into a usable audio stream URL.
    /// </summary>
    /// <param name="originalURL">The original web URL.</param>
    /// <returns>The API URL for the station's stream.</returns>
    public static string ConvertRadioGardenURL(string originalURL)
    {
        var lastSlashIndex = originalURL.LastIndexOf('/');
        if (lastSlashIndex == -1) return originalURL;

        var stationId = originalURL[(lastSlashIndex + 1)..];
        return string.Format(RadioGardenAPIUrl, stationId);
    }

    /// <summary>
    ///     Checks if the audio stream at the specified URL is valid.
    /// </summary>
    /// <param name="url">The URL of the audio stream to check.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a boolean indicating whether the
    ///     audio stream is valid.
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
                Debug.WriteLine($"Request error: {response.StatusCode}");
                return false;
            }

            // Check if the content type is an audio type
            var contentType = response.Content.Headers.ContentType?.MediaType;
            if (contentType != null && contentType.StartsWith("audio/")) return true;

            Debug.WriteLine("The content type is not an audio type.");
            return false;
        }
        catch (TaskCanceledException)
        {
            // Handle timeout
            if (!_httpClient.DefaultRequestHeaders.ConnectionClose.HasValue)
            {
                Debug.WriteLine("Request timed out.");
                return false;
            }
        }
        catch (HttpRequestException e)
        {
            Debug.WriteLine($"Request error: {e.Message}");
            return false;
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Unexpected error: {e.Message}");
            return false;
        }

        return false;
    }
}