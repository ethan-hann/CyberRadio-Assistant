﻿// Station.cs : RadioExt-Helper
// Copyright (C) 2025  Ethan Hann
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.ComponentModel;
using AetherUtils.Core.Logging;
using Newtonsoft.Json;
using WIG.Lib.Models;

namespace RadioExt_Helper.models;

/// <summary>
///     Represents a single radio station. Contains all information related to the station.
/// </summary>
public sealed class Station : INotifyPropertyChanged, ICloneable, IEquatable<Station>
{
    private List<TrackableObject<WolvenIcon>> _icons = [];
    private MetaData _metaData = new();
    private List<Song> _songs = [];

    /// <summary>
    ///     The metadata associated with this station.
    /// </summary>
    [JsonIgnore]
    public MetaData MetaData
    {
        get => _metaData;
        set
        {
            _metaData = value;
            OnPropertyChanged(nameof(MetaData));
        }
    }

    /// <summary>
    ///     The stream info associated with this station.
    /// </summary>
    [JsonIgnore]
    public StreamInfo StreamInfo
    {
        get => MetaData.StreamInfo;
        set
        {
            MetaData.StreamInfo = value;
            OnPropertyChanged(nameof(StreamInfo));
        }
    }

    /// <summary>
    ///     The custom icon associated with this station.
    /// </summary>
    [JsonIgnore]
    public CustomIcon CustomIcon
    {
        get => MetaData.CustomIcon;
        set
        {
            MetaData.CustomIcon = value;
            OnPropertyChanged(nameof(CustomIcon));
        }
    }

    /// <summary>
    ///     Represents a list of songs associated with a radio station.
    /// </summary>
    [JsonProperty("songs")]
    public List<Song> Songs
    {
        get => _songs;
        set
        {
            _songs = value;
            OnPropertyChanged(nameof(Songs));
        }
    }

    /// <summary>
    ///     Represents a list of icons associated with a radio station.
    ///     A station can only have one active icon at a time.
    /// </summary>
    [JsonProperty("icons")]
    public List<TrackableObject<WolvenIcon>> Icons
    {
        get => _icons;
        set
        {
            _icons = value;
            OnPropertyChanged(nameof(Icons));
        }
    }

    public object Clone()
    {
        return new Station
        {
            MetaData = (MetaData)MetaData.Clone(),
            Songs = [.. Songs],
            Icons = [.. Icons.Select(icon => new TrackableObject<WolvenIcon>(icon.TrackedObject))]
        };
    }

    public bool Equals(Station? other)
    {
        if (other == null) return false;
        return MetaData.Equals(other.MetaData) &&
               Songs.SequenceEqual(other.Songs) &&
               Icons.SequenceEqual(other.Icons);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    ///     Get a value indicating if this station is active in game or not.
    /// </summary>
    /// <returns></returns>
    public bool GetStatus()
    {
        return MetaData.IsActive;
    }

    /// <summary>
    /// Add an icon to this station if it is not already associated. A station can have any number of icons but only one can be active at a time.
    /// </summary>
    /// <param name="icon">The <see cref="Icon"/> to add to the station.</param>
    /// <returns><c>true</c> if the icon was added successfully; <c>false</c> otherwise.</returns>
    public bool AddIcon(TrackableObject<WolvenIcon> icon)
    {
        return AddIcon(icon, false);
    }

    /// <summary>
    /// Add an icon to this station if it is not already associated. A station can have any number of icons but only one can be active at a time.
    /// Optionally, make the icon active.
    /// </summary>
    /// <param name="icon">The <see cref="Icon"/> to add to the station.</param>
    /// <param name="makeActive">Indicates whether to make the newly added icon active for the station.</param>
    /// <returns><c>true</c> if the icon was added successfully; <c>false</c> otherwise.</returns>
    public bool AddIcon(TrackableObject<WolvenIcon> icon, bool makeActive)
    {
        if (Icons.Contains(icon)) return false;

        Icons.Add(icon);
        if (makeActive)
            icon.TrackedObject.IsActive = true;

        OnPropertyChanged(nameof(Icons));
        return true;
    }

    /// <summary>
    /// Remove an icon from the station, if it exists.
    /// </summary>
    /// <param name="icon">The <see cref="Icon"/> to remove.</param>
    /// <returns><c>true</c> if the icon was removed successfully; <c>false</c> otherwise.</returns>
    public bool RemoveIcon(TrackableObject<WolvenIcon> icon)
    {
        if (!Icons.Contains(icon)) return false;

        Icons.Remove(icon);
        OnPropertyChanged(nameof(Icons));
        return true;
    }

    /// <summary>
    /// Remove all icons from the station.
    /// </summary>
    /// <returns></returns>
    public void RemoveAllIcons()
    {
        Icons.Clear();
        OnPropertyChanged(nameof(Icons));
    }

    /// <summary>
    /// Get the active <see cref="Icon"/> for the station.
    /// </summary>
    /// <returns>The active <see cref="Icon"/> or <c>null</c> if no active icons or there was more than 1 active icon.</returns>
    /// <exception cref="InvalidOperationException">Occurs when there is more than 1 active icon in the station.</exception>
    public TrackableObject<WolvenIcon>? GetActiveIcon()
    {
        try
        {
            if (Icons.Count == 0) return null;
            if (Icons.Count(i => i.TrackedObject.IsActive) > 1)
                throw new InvalidOperationException("A station can only have one active icon.");
            return Icons.FirstOrDefault(i => i.TrackedObject.IsActive);
        }
        catch (Exception e)
        {
            AuLogger.GetCurrentLogger<Station>().Error(e);
        }

        return null;
    }

    /// <summary>
    /// Ensures the active icon (if any) is valid for the station. A valid active icon has been imported and has its archive file present.
    /// This should be the case when either the icon is generated from a PNG or imported from a .zip file containing a station.
    /// </summary>
    /// <returns><c>true</c> if the active icon is valid. <c>false</c> otherwise or there were no active icons.</returns>
    /// <exception cref="InvalidOperationException">Occurs when there is more than 1 active icon in the station.</exception>
    public bool CheckActiveIconValid()
    {
        try
        {
            if (Icons.Count == 0) return false;
            if (Icons.Count(i => i.TrackedObject.IsActive) > 1)
                throw new InvalidOperationException("A station can only have one active icon.");
            var activeIcon = Icons.FirstOrDefault(i => i.TrackedObject.IsActive);
            return activeIcon != null && activeIcon.TrackedObject.CheckIconValid();
        }
        catch (Exception e)
        {
            AuLogger.GetCurrentLogger<Station>().Error(e);
        }

        return false;
    }

    /// <summary>
    /// Add custom data to the station. If the key already exists, the value will be updated.
    /// </summary>
    /// <param name="key">The key of the data to add.</param>
    /// <param name="value">The data contents to add with the specified key.</param>
    public void AddCustomData(string? key, object value)
    {
        try
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            MetaData.CustomData[key] = value;
            OnPropertyChanged(nameof(MetaData.CustomData));
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<Station>("AddCustomData").Error(ex,
                $"An error occurred while adding custom data for the station: {MetaData.DisplayName}");
        }
    }

    /// <summary>
    /// Remove the custom data associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the data to remove.</param>
    public void RemoveCustomData(string? key)
    {
        try
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            MetaData.CustomData.Remove(key);
            OnPropertyChanged(nameof(MetaData.CustomData));
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<Station>("RemoveCustomData").Error(ex,
                $"An error occurred while removing custom data for the station: {MetaData.DisplayName}");
        }
    }

    /// <summary>
    /// Get a value indicating if the station's custom data contains the specified key.
    /// </summary>
    /// <param name="key">The key of the data to check existence of.</param>
    /// <returns><c>true</c> if the key was present in the custom data; <c>false</c> otherwise.</returns>
    public bool ContainsCustomData(string key)
    {
        try
        {
            return MetaData.CustomData.ContainsKey(key);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<Station>("ContainsCustomData").Error(ex,
                $"An error occurred while checking the custom data with key ({key}) for the station: {MetaData.DisplayName}");
        }

        return false;
    }

    /// <summary>
    /// Get the custom data specified by the key in the station.
    /// </summary>
    /// <param name="key">The key of the data to get.</param>
    /// <returns>A data object referenced with the specified key or a blank, new <see cref="object"/> if an error occurred.</returns>
    public object GetCustomData(string key)
    {
        try
        {
            return MetaData.CustomData[key];
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<Station>("ContainsCustomData").Error(ex,
                $"An error occurred while getting the custom data with key ({key}) for the station: {MetaData.DisplayName}");
        }

        return new object();
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public override string ToString()
    {
        return MetaData.DisplayName;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Station);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(MetaData, Songs, Icons);
    }
}