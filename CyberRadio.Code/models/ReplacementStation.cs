// // ReplacementStation.cs : RadioExt-Helper
// // Copyright (C) 2025  Ethan Hann
// //
// // This program is free software: you can redistribute it and/or modify
// // it under the terms of the GNU General Public License as published by
// // the Free Software Foundation, either version 3 of the License, or
// // (at your option) any later version.
// //
// // This program is distributed in the hope that it will be useful,
// // but WITHOUT ANY WARRANTY; without even the implied warranty of
// // MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// // GNU General Public License for more details.
// //
// // You should have received a copy of the GNU General Public License
// // along with this program.  If not, see <https://www.gnu.org/licenses/>.

#region

using System.ComponentModel;
using Newtonsoft.Json;
using RadioExt_Helper.utility;
using WIG.Lib.Models.Audio;

#endregion

namespace RadioExt_Helper.models;

/// <summary>
///     Represents a replacement station that can replace a vanilla station in the game.
/// </summary>
public sealed class ReplacementStation : IStation, INotifyPropertyChanged, ICloneable, IEquatable<ReplacementStation>
{
    private string _displayName = string.Empty;
    private bool _isActive;
    private string _notes = string.Empty;
    private List<ReplacementTrack> _tracks = [];
    private VanillaStation? _vanillaStation;

    /// <summary>
    ///     The display name for this replacement station. Does not affect in-game name.
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
    ///     The vanilla station that this replacement station corresponds to.
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
    ///     The list of replacement tracks for this station.
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
    ///     Indicates whether this replacement station is active (i.e., should replace the vanilla station when exporting).
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
    ///     Additional notes about the replacement station. Can be used for user reference. Stores HTML content from TinyMCE
    ///     editor.
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
    public object Clone()
    {
        return new ReplacementStation
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
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <inheritdoc />
    public List<string> Tags { get; set; } = [];

    /// <inheritdoc />
    public StationType StationType => StationType.Replacement;

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