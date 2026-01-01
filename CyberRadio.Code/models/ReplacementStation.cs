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
using AetherUtils.Core.Logging;
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

    /// <summary>
    /// Replaces an existing track in the replacement station with a new track using the specified parameters.
    /// </summary>
    /// <remarks>If the specified track does not exist in the replacement station, or if the original track
    /// cannot be removed, the method returns false and no changes are made.</remarks>
    /// <param name="trackName">The name of the track to be replaced. This value is compared against the vanilla track names in the station.
    /// Cannot be null.</param>
    /// <param name="wemId">The WEM identifier to associate with the replacement track. Cannot be null.</param>
    /// <param name="replacementFile">The file path of the replacement audio file to use for the new track. Cannot be null.</param>
    /// <returns>true if the track was successfully replaced; otherwise, false.</returns>
    public bool ReplaceTrack(string trackName, string wemId, string replacementFile)
    {
        var originalTrack = Tracks.FirstOrDefault(t => t.VanillaTrackName.Equals(trackName) && t.WemId.Equals(wemId));
        var newTrack = new ReplacementTrack(trackName, wemId, replacementFile);
        if (originalTrack == null)
        {
            AuLogger.GetCurrentLogger<ReplacementStation>("ReplaceTrack")
                .Info(
                    $"No replacement track found for track '{trackName}' in replacement station '{DisplayName}' for WEM ID '{wemId}'. Creating a new one...");
            Tracks.Add(newTrack);
            return Tracks.Contains(newTrack);
        }

        //Track was already replaced; we are updating the file path so we remove and add the new one. The ReplacementTrack class is immutable.
        AuLogger.GetCurrentLogger<ReplacementStation>("ReplaceTrack").Info($"Replacement track found for track/WEMId combo: '{trackName}/{wemId}'. Updating with new file...");
        if (Tracks.Remove(originalTrack))
            Tracks.Add(newTrack);
        else
        {
            AuLogger.GetCurrentLogger<ReplacementStation>("ReplaceTrack")
                .Warn(
                    $"Failed to remove original track '{trackName}' from replacement station '{DisplayName}'.");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Removes a replaced track from the collection that matches the specified track name and WEM ID.
    /// </summary>
    /// <remarks>If no replacement track matching both the specified track name and WEM ID is found, no action
    /// is taken and the method returns false. This method does not throw an exception if the track is not
    /// found.</remarks>
    /// <param name="trackName">The name of the original (vanilla) track to identify the replacement to remove. Cannot be null.</param>
    /// <param name="wemId">The WEM ID associated with the replacement track to remove. Cannot be null.</param>
    /// <returns>true if a matching replacement track was found and removed; otherwise, false.</returns>
    public bool RemoveReplacedTrack(string trackName, string wemId)
    {
        var trackToRemove = Tracks.FirstOrDefault(t => t.VanillaTrackName.Equals(trackName) && t.WemId.Equals(wemId));
        if (trackToRemove == null)
        {
            AuLogger.GetCurrentLogger<ReplacementStation>("RemoveReplacedTrack")
                .Info(
                    $"No replacement track found for track '{trackName}' in replacement station '{DisplayName}' for WEM ID '{wemId}'. Nothing to remove.");
            return false;
        }
        AuLogger.GetCurrentLogger<ReplacementStation>("RemoveReplacedTrack")
            .Info(
                $"Removing replacement track for track '{trackName}' in replacement station '{DisplayName}' for WEM ID '{wemId}'.");
        return Tracks.Remove(trackToRemove);
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