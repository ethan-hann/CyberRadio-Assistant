using Newtonsoft.Json;

namespace RadioExt_Helper.models;

/// <summary>
///     Represents a custom icon definition for a radio station.
/// </summary>
public class CustomIcon
{
    /// <summary>
    ///     Points to the <c>.inkatlas</c> that holds the icon texture.
    /// </summary>
    [JsonProperty("inkAtlasPath")]
    public string InkAtlasPath { get; set; } = "path\\to\\custom\\atlas.inkatlas";

    /// <summary>
    ///     Specifies which part of the <c>.inkatlas</c> should be used for the icon.
    /// </summary>
    [JsonProperty("inkAtlasPart")]
    public string InkAtlasPart { get; set; } = "custom_texture_part";

    /// <summary>
    ///     If <c>false</c>, the icon specified inside <see cref="MetaData.Icon" /> will be used. If <c>true</c>, the
    ///     custom icon defined here will be used.
    /// </summary>
    [JsonProperty("useCustom")]
    public bool UseCustom { get; set; }
}