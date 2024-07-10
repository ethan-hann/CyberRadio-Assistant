using System.ComponentModel;
using Newtonsoft.Json;

namespace RadioExt_Helper.models;

/// <summary>
///     Represents the metadata that describes a radio stations properties.
/// </summary>
public sealed class MetaData : INotifyPropertyChanged
{
    private string _displayName = "69.9 Your Station Name";

    /// <summary>
    ///     The name of the station that will be displayed in game. Also used for the station folder.
    /// </summary>
    [JsonProperty("displayName")]
    public string DisplayName
    {
        get => _displayName;
        set
        {
            _displayName = value;
            OnPropertyChanged(nameof(DisplayName));
        }
    }

    /// <summary>
    ///     Used to place the station at the right place in the stations list. If <see cref="DisplayName" /> has a number,
    ///     it should be the same here.
    /// </summary>
    [JsonProperty("fm")]
    public float Fm { get; set; } = 69.9f;

    /// <summary>
    ///     The overall volume multiplier for the station.
    /// </summary>
    [JsonProperty("volume")]
    public float Volume { get; set; } = 1.0f;

    /// <summary>
    ///     The icon for the station, if not using a custom one defined in <see cref="CustomIcon" />.
    /// </summary>
    [JsonProperty("icon")]
    public string Icon { get; set; } = "UIIcon.alcohol_absynth";

    /// <summary>
    ///     Defines the custom icon to use for the station, if applicable. If not using a custom icon,
    ///     this property is not used.
    /// </summary>
    [JsonProperty("customIcon")]
    public CustomIcon CustomIcon { get; set; } = new();

    /// <summary>
    ///     Defines the streaming properties for the station, if applicable. If using audio files instead of a web stream,
    ///     this property is not used.
    /// </summary>
    [JsonProperty("streamInfo")]
    public StreamInfo StreamInfo { get; set; } = new();

    /// <summary>
    ///     Specifies the order in which songs should be played. The order does not have to contain all the songs in the
    ///     station. Any songs not specified in the order will be played randomly before/after the ordered section.
    /// </summary>
    [JsonProperty("order")]
    public List<string> SongOrder { get; set; } = [];

    /// <summary>
    ///     Indicates whether this station is enabled in game or not.
    /// </summary>
    [JsonProperty("enabled")]
    public bool IsActive { get; set; } = true;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    ///     Get a display friendly value representing this metadata.
    /// </summary>
    /// <returns>The <see cref="DisplayName" /> of the station.</returns>
    public override string ToString()
    {
        return DisplayName;
    }
}