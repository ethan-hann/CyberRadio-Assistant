// Song.cs : RadioExt-Helper
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

using System.Diagnostics.CodeAnalysis;
using AetherUtils.Core.Logging;
using Newtonsoft.Json;
using File = TagLib.File;

namespace RadioExt_Helper.models;

/// <summary>
///     Represents a single song entry. Song information is read from the file on disk.
/// </summary>
public sealed class Song : IEquatable<Song>, ICloneable
{
    /// <summary>
    ///     The name of the song.
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    ///     The artist of the song or audio file.
    /// </summary>
    [JsonProperty("artist")]
    public string Artist { get; set; } = string.Empty;

    /// <summary>
    ///     The duration of the song or audio file.
    /// </summary>
    [JsonProperty("duration")]
    public TimeSpan Duration { get; set; }

    /// <summary>
    ///     The file size (in bytes) of the song or audio file.
    /// </summary>
    [JsonProperty("file_size")]
    public ulong FileSize { get; set; }

    /// <summary>
    ///     The original file path of the song or audio file on disk.
    /// </summary>
    [JsonProperty("file_path")]
    public string FilePath { get; set; } = string.Empty;

    public object Clone()
    {
        return new Song
        {
            Title = Title,
            Artist = Artist,
            Duration = Duration,
            FileSize = FileSize,
            FilePath = FilePath
        };
    }

    public bool Equals(Song? other)
    {
        if (other == null) return false;
        return Title.Equals(other.Title) &&
               Artist.Equals(other.Artist) &&
               Duration.Equals(other.Duration) &&
               FileSize.Equals(other.FileSize) &&
               FilePath.Equals(other.FilePath);
    }

    /// <summary>
    ///     Get metadata about an audio file and return a new <see cref="Song" /> object.
    /// </summary>
    /// <param name="filePath">The path to the audio file.</param>
    /// <returns>A new <see cref="Song" /> with the metadata or <c>null</c> if an exception occurred.</returns>
    public static Song? FromFile(string filePath)
    {
        try
        {
            return CreateSongFromFile(filePath);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<Song>("FromFile")
                .Error(ex, $"Couldn't read song file: {filePath}");
            return null;
        }
    }

    private static Song CreateSongFromFile(string filePath)
    {
        var file = File.Create(filePath);
        return new Song
        {
            FilePath = filePath,
            Title = file.Tag.Title ?? Path.GetFileName(filePath),
            Artist = file.Tag.FirstPerformer ?? string.Empty,
            FileSize = (ulong)new FileInfo(filePath).Length,
            Duration = file.Properties.Duration
        };
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Song);
    }

    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode()
    {
        return HashCode.Combine(Title, Artist, Duration, FileSize, FilePath);
    }

    /// <summary>
    ///     Get a display friendly value representing this song.
    /// </summary>
    /// <returns>The <see cref="Title" /> of the song.</returns>
    public override string ToString()
    {
        return Title;
    }
}