using System.ComponentModel;
using Newtonsoft.Json;
using RadioExt_Helper.utility;
using WIG.Lib.Models.Audio;

namespace RadioExt_Helper.models;

/// <summary>
/// Represents a replacement station that can replace a vanilla station in the game.
/// </summary>
public sealed class ReplacementStation : AdditionalStation, INotifyPropertyChanged, ICloneable, IEquatable<ReplacementStation>
{
    private VanillaStation? _vanillaStation;
    private List<ReplacementTrack> _tracks = [];

    /// <inheritdoc />
    public override string StationName { get; set; } = string.Empty;

    /// <inheritdoc />
    public override List<string> Tags { get; set; } = [];

    /// <inheritdoc />
    public override StationType StationType { get; set; } = StationType.Replacement;

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
            StationName = _vanillaStation?.StationName ?? string.Empty;
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
        return HashCode.Combine(_vanillaStation, _tracks);
    }
}