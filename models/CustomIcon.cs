using Newtonsoft.Json;

namespace RadioExt_Helper.models;

public class CustomIcon
{
    [JsonProperty("inkAtlasPath")] public string InkAtlasPath { get; set; } = "path\\to\\custom\\atlas.inkatlas";

    [JsonProperty("inkAtlasPart")] public string InkAtlasPart { get; set; } = "custom_texture_part";

    [JsonProperty("useCustom")] public bool UseCustom { get; set; }
}