// StreamInfo.cs : RadioExt-Helper
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
///     Represents the stream that a station uses to play audio from a web source.
/// </summary>
public sealed class StreamInfo : INotifyPropertyChanged, ICloneable, IEquatable<StreamInfo>
{
    private bool _isStream;
    private string _streamUrl = "https://radio.garden/api/ara/content/listen/TP8NDBv7/channel.mp3";

    /// <summary>
    ///     The web URL of an internet stream for the station.
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

    /// <summary>
    ///     Indicates if the station is a streaming station or not.
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

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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