using Newtonsoft.Json;
using System.ComponentModel;

namespace RadioExt_Helper.models;

/// <summary>
///     Represents a custom icon definition for a radio station.
/// </summary>
public class CustomIcon : INotifyPropertyChanged, ICloneable, IEquatable<CustomIcon>
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string _inkAtlasPath = "path\\to\\custom\\atlas.inkatlas";
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

    private string _inkAtlasPart = "custom_texture_part";
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

    private bool _useCustom;
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

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

    public override bool Equals(object? obj)
    {
        return Equals(obj as CustomIcon);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(InkAtlasPath, InkAtlasPart, UseCustom);
    }
}