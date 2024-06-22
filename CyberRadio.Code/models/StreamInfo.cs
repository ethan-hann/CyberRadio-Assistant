using Newtonsoft.Json;

namespace RadioExt_Helper.models;

/// <summary>
///     Represents the stream that a station uses to play audio from a web source.
/// </summary>
public class StreamInfo
{
    /// <summary>
    ///     The web URL of an internet stream for the station.
    /// </summary>
    [JsonProperty("streamURL")]
    public string StreamUrl { get; set; } = "https://radio.garden/api/ara/content/listen/TP8NDBv7/channel.mp3";

    /// <summary>
    ///     Indicates if the station is a streaming station or not.
    /// </summary>
    [JsonProperty("isStream")]
    public bool IsStream { get; set; }
}