using Newtonsoft.Json;

namespace RadioExt_Helper.models;

public class StreamInfo
{
    [JsonProperty("streamURL")]
    public string StreamUrl { get; set; } = "https://radio.garden/api/ara/content/listen/TP8NDBv7/channel.mp3";

    [JsonProperty("isStream")] public bool IsStream { get; set; }
}