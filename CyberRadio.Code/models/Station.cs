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

using Newtonsoft.Json;
using System.ComponentModel;

namespace RadioExt_Helper.models;

/// <summary>
///     Represents a single radio station. Contains all information related to the station.
/// </summary>
public sealed class Station : INotifyPropertyChanged, ICloneable, IEquatable<Station>
{
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

    public object Clone()
    {
        return new Station
        {
            MetaData = (MetaData)MetaData.Clone(),
            Songs = [..Songs]
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