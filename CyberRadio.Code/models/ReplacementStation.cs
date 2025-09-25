using System.ComponentModel;
using Newtonsoft.Json;
using RadioExt_Helper.utility;
using WIG.Lib.Models.Audio;

namespace RadioExt_Helper.models;

/// <summary>
/// Represents a replacement station that can replace a vanilla station in the game.
/// </summary>
public sealed class ReplacementStation : IStation, INotifyPropertyChanged, ICloneable, IEquatable<ReplacementStation>
{
    private string _displayName = string.Empty;
    private VanillaStation? _vanillaStation;
    private List<ReplacementTrack> _tracks = [];
    private bool _isActive;
    private string _notes = string.Empty;

    /// <inheritdoc />
    public List<string> Tags { get; set; } = [];

    /// <inheritdoc />
    public StationType StationType => StationType.Replacement;

    /// <summary>
    /// The display name for this replacement station. Does not affect in-game name.
    /// </summary>
    [JsonProperty("displayName")]
    public string DisplayName
    {
        get => _displayName;
        set
        {
            if (_displayName == value) return;
            _displayName = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayName)));
        }
    }


    /// <summary>
    /// The vanilla station that this replacement station corresponds to.
    /// </summary>
    [JsonProperty("vanillaStation")]
    public VanillaStation? VanillaStation
    {
        get => _vanillaStation;
        set
        {
            if (_vanillaStation == value) return;

            _vanillaStation = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VanillaStation)));
        }
    }

    /// <summary>
    /// The list of replacement tracks for this station.
    /// </summary>
    [JsonProperty("tracks")]
    public List<ReplacementTrack> Tracks
    {
        get => _tracks;
        set
        {
            if (_tracks == value) return;

            _tracks = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tracks)));
        }
    }

    /// <summary>
    /// Indicates whether this replacement station is active (i.e., should replace the vanilla station when exporting).
    /// </summary>
    [JsonProperty("enabled")]
    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (_isActive == value) return;
            _isActive = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsActive)));
        }
    }

    /// <summary>
    /// Additional notes about the replacement station. Can be used for user reference. Stores HTML content from TinyMCE editor.
    /// </summary>
    [JsonProperty("notes")]
    public string Notes
    {
        get => _notes;
        set
        {
            if (_notes == value) return;
            _notes = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Notes)));
        }
    }

    /// <inheritdoc />
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <inheritdoc />
    public object Clone()
    {
        return new ReplacementStation()
        {
            Tracks = _tracks,
            VanillaStation = _vanillaStation
        };
    }

    /// <inheritdoc />
    public bool Equals(ReplacementStation? other)
    {
        if (other is null) return false;

        return other.VanillaStation?.StationName == VanillaStation?.StationName &&
               other.Tracks.SequenceEqual(Tracks);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as ReplacementStation);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return VanillaStation?.StationName ?? "No Station Selected";
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(_vanillaStation, _tracks, _isActive);
    }
}