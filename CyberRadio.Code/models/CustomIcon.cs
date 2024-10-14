// CustomIcon.cs : RadioExt-Helper
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
using Newtonsoft.Json;

namespace RadioExt_Helper.models;

/// <summary>
///     Represents a custom icon definition for a radio station.
/// </summary>
public sealed class CustomIcon : INotifyPropertyChanged, ICloneable, IEquatable<CustomIcon>
{
    private string _inkAtlasPart = "custom_texture_part";
    private string _inkAtlasPath = "path\\to\\custom\\atlas.inkatlas";
    private bool _useCustom;

    /// <summary>
    ///     Points to the <c>.inkatlas</c> that holds the icon texture.
    /// </summary>
    [JsonProperty("inkAtlasPath")]
    public string InkAtlasPath
    {
        get => _inkAtlasPath;
        set
        {
            _inkAtlasPath = value;
            OnPropertyChanged(nameof(InkAtlasPath));
        }
    }

    /// <summary>
    ///     Specifies which part of the <c>.inkatlas</c> should be used for the icon.
    /// </summary>
    [JsonProperty("inkAtlasPart")]
    public string InkAtlasPart
    {
        get => _inkAtlasPart;
        set
        {
            _inkAtlasPart = value;
            OnPropertyChanged(nameof(InkAtlasPart));
        }
    }

    /// <summary>
    ///     If <c>false</c>, the icon specified inside <see cref="MetaData.Icon" /> will be used. If <c>true</c>, the
    ///     custom icon defined here will be used.
    /// </summary>
    [JsonProperty("useCustom")]
    public bool UseCustom
    {
        get => _useCustom;
        set
        {
            _useCustom = value;
            OnPropertyChanged(nameof(UseCustom));
        }
    }

    public object Clone()
    {
        return new CustomIcon
        {
            InkAtlasPath = InkAtlasPath,
            InkAtlasPart = InkAtlasPart,
            UseCustom = UseCustom
        };
    }

    public bool Equals(CustomIcon? other)
    {
        if (other == null) return false;
        return InkAtlasPath == other.InkAtlasPath &&
               InkAtlasPart == other.InkAtlasPart &&
               UseCustom == other.UseCustom;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as CustomIcon);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(InkAtlasPath, InkAtlasPart, UseCustom);
    }
}