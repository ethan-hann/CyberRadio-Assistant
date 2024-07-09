using Newtonsoft.Json;
using System.ComponentModel;

namespace RadioExt_Helper.models;

/// <summary>
/// Represents the stream that a station uses to play audio from a web source.
/// </summary>
public class StreamInfo : INotifyPropertyChanged, ICloneable, IEquatable<StreamInfo>
{
    private string _streamUrl = "https://radio.garden/api/ara/content/listen/TP8NDBv7/channel.mp3";

    /// <summary>
    /// The web URL of an internet stream for the station.
    /// </summary>
    [JsonProperty("streamURL")]
    public string StreamUrl
    {
        get => _streamUrl;
        set
        {
            _streamUrl = value;
            OnPropertyChanged(nameof(StreamUrl));
        }
    }

    private bool _isStream;

    /// <summary>
    /// Indicates if the station is a streaming station or not.
    /// </summary>
    [JsonProperty("isStream")]
    public bool IsStream
    {
        get => _isStream;
        set
        {
            _isStream = value;
            OnPropertyChanged(nameof(IsStream));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public object Clone()
    {
        return new StreamInfo
        {
            StreamUrl = StreamUrl,
            IsStream = IsStream
        };
    }

    public bool Equals(StreamInfo? other)
    {
        if (other == null) return false;
        return StreamUrl == other.StreamUrl && IsStream == other.IsStream;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as StreamInfo);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(StreamUrl, IsStream);
    }
}
