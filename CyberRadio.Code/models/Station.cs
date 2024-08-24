// Station.cs : RadioExt-Helper
// Copyright (C) 2024  Ethan Hann
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
using RadioExt_Helper.utility;

namespace RadioExt_Helper.models;

/// <summary>
///     Represents a single radio station. Contains all information related to the station.
/// </summary>
public sealed class Station : INotifyPropertyChanged, ICloneable, IEquatable<Station>
{
    private MetaData _metaData = new();
    private List<Song> _songs = [];
    private BindingList<Icon> _icons = [];

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
    public BindingList<Icon> Icons
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
            Songs = [.. Songs]
        };
    }

    public bool Equals(Station? other)
    {
        if (other == null) return false;
        return MetaData.Equals(other.MetaData) &&
               Songs.SequenceEqual(other.Songs);
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
    /// <param name="makeActive">Indicates whether to make the newly added icon active for the station.</param>
    /// <returns><c>true</c> if the icon was added successfully; <c>false</c> otherwise.</returns>
    public bool AddIcon(Icon icon, bool makeActive = false)
    {
        if (Icons.Contains(icon)) return false;

        Icons.Add(icon);
        if (makeActive) icon.IsActive = true;

        OnPropertyChanged(nameof(Icons));
        return true;
    }

    /// <summary>
    /// Remove an icon from the station, if it exists.
    /// </summary>
    /// <param name="icon">The <see cref="Icon"/> to remove.</param>
    /// <returns><c>true</c> if the icon was removed successfully; <c>false</c> otherwise.</returns>
    public bool RemoveIcon(Icon icon)
    {
        if (!Icons.Contains(icon)) return false;

        Icons.Remove(icon);
        OnPropertyChanged(nameof(Icons));
        return true;
    }

    /// <summary>
    /// Get the active <see cref="Icon"/> for the station.
    /// </summary>
    /// <returns>The active <see cref="Icon"/> or <c>null</c> if no active icons or there was more than 1 active icon.</returns>
    /// <exception cref="InvalidOperationException">Occurs when there are more than 1 active icon in the station.</exception>
    public Icon? GetActiveIcon()
    {
        try
        {
            if (Icons.Count == 0) return null;
            if (Icons.Count(i => i.IsActive) > 1) throw new InvalidOperationException("A station can only have one active icon.");
            return Icons.FirstOrDefault(i => i.IsActive);
        }
        catch (Exception e)
        {
            AuLogger.GetCurrentLogger<Station>().Error(e);
        }

        return null;
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
        return HashCode.Combine(MetaData, Songs);
    }
}