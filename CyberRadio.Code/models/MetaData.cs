// MetaData.cs : RadioExt-Helper
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
using AetherUtils.Core.Utility;
using Newtonsoft.Json;

namespace RadioExt_Helper.models;

/// <summary>
///     Represents the metadata that describes a radio stations properties.
/// </summary>
public sealed class MetaData : INotifyPropertyChanged, ICloneable, IEquatable<MetaData>
{
    private SerializableDictionary<string, object> _customData = [];
    private CustomIcon _customIcon = new();
    private string _displayName = "69.9 Your Station Name";
    private float _fm = 69.9f;
    private string _icon = "UIIcon.alcohol_absynth";
    private bool _isActive = true;
    private List<string> _songOrder = [];
    private StreamInfo _streamInfo = new();
    private float _volume = 1.0f;

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
    public float Fm
    {
        get => _fm;
        set
        {
            _fm = value;
            OnPropertyChanged(nameof(Fm));
        }
    }

    /// <summary>
    ///     The overall volume multiplier for the station.
    /// </summary>
    [JsonProperty("volume")]
    public float Volume
    {
        get => _volume;
        set
        {
            _volume = value;
            OnPropertyChanged(nameof(Volume));
        }
    }

    /// <summary>
    ///     The icon for the station, if not using a custom one defined in <see cref="CustomIcon" />.
    /// </summary>
    [JsonProperty("icon")]
    public string Icon
    {
        get => _icon;
        set
        {
            _icon = value;
            OnPropertyChanged(nameof(Icon));
        }
    }

    /// <summary>
    ///     Defines the custom icon to use for the station, if applicable. If not using a custom icon,
    ///     this property is not used.
    /// </summary>
    [JsonProperty("customIcon")]
    public CustomIcon CustomIcon
    {
        get => _customIcon;
        set
        {
            _customIcon = value;
            OnPropertyChanged(nameof(CustomIcon));
        }
    }

    /// <summary>
    ///     Defines the streaming properties for the station, if applicable. If using audio files instead of a web stream,
    ///     this property is not used.
    /// </summary>
    [JsonProperty("streamInfo")]
    public StreamInfo StreamInfo
    {
        get => _streamInfo;
        set
        {
            _streamInfo = value;
            OnPropertyChanged(nameof(StreamInfo));
        }
    }

    /// <summary>
    ///     Specifies the order in which songs should be played. The order does not have to contain all the songs in the
    ///     station. Any songs not specified in the order will be played randomly before/after the ordered section.
    /// </summary>
    [JsonProperty("order")]
    public List<string> SongOrder
    {
        get => _songOrder;
        set
        {
            _songOrder = value;
            OnPropertyChanged(nameof(SongOrder));
        }
    }

    /// <summary>
    ///     Indicates whether this station is enabled in game or not.
    /// </summary>
    [JsonProperty("enabled")]
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            OnPropertyChanged(nameof(IsActive));
        }
    }

    /// <summary>
    /// A dictionary of custom data that can be used to store additional information about the station.
    /// </summary>
    [JsonProperty("custom_data")]
    public SerializableDictionary<string, object> CustomData
    {
        get => _customData;
        set
        {
            _customData = value;
            OnPropertyChanged(nameof(CustomData));
        }
    }

    public object Clone()
    {
        var m = new MetaData
        {
            DisplayName = DisplayName,
            Fm = Fm,
            Volume = Volume,
            Icon = Icon,
            CustomIcon = (CustomIcon)CustomIcon.Clone(),
            StreamInfo = (StreamInfo)StreamInfo.Clone(),
            SongOrder = [.. SongOrder],
            IsActive = IsActive,
            CustomData = (SerializableDictionary<string, object>)CustomData.Clone()
        };

        return m;
    }

    public bool Equals(MetaData? other)
    {
        if (other == null) return false;
        return DisplayName == other.DisplayName &&
               Math.Abs(Fm - other.Fm) < float.Epsilon &&
               Math.Abs(Volume - other.Volume) < float.Epsilon &&
               Icon == other.Icon &&
               CustomIcon.Equals(other.CustomIcon) &&
               StreamInfo.Equals(other.StreamInfo) &&
               SongOrder.SequenceEqual(other.SongOrder) &&
               IsActive == other.IsActive &&
               CustomData.Equals(other.CustomData);
    }

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

    public override bool Equals(object? obj)
    {
        return Equals(obj as MetaData);
    }

    public override int GetHashCode()
    {
        var hashCode = HashCode.Combine(DisplayName, Fm, Volume, Icon, CustomIcon, StreamInfo, SongOrder, IsActive);
        return HashCode.Combine(hashCode, CustomData.GetHashCode());
    }
}