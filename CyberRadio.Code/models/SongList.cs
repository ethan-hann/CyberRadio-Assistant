// SongList.cs : RadioExt-Helper
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

using System.Collections;
using Newtonsoft.Json;

namespace RadioExt_Helper.models;

/// <summary>
///     Represents a list of songs associated with a radio station.
/// </summary>
public class SongList : ICollection<Song>, IEquatable<SongList>, ICloneable
{
    /// <summary>
    ///     Represents a list of songs associated with a radio station.
    /// </summary>
    [JsonProperty("songs")]
    public List<Song> Songs { get; set; } = [];

    /// <summary>
    ///     Performs a deep copy of the current SongList object and returns a new instance.
    /// </summary>
    /// <returns>A new SongList object that is a deep copy of the current instance.</returns>
    public object Clone()
    {
        var clone = new SongList();
        foreach (var song in Songs)
            clone.Add((Song)song.Clone());
        return clone;
    }

    /// <summary>
    ///     Gets the number of elements in the collection.
    /// </summary>
    public int Count => Songs.Count;

    public bool IsReadOnly => false;

    /// <summary>
    ///     Adds a song to the song list associated with a radio station.
    /// </summary>
    /// <param name="song">The song to be added to the list.</param>
    /// <remarks>
    ///     If the song with the same title already exists in the list, it will not be added again.
    /// </remarks>
    public void Add(Song song)
    {
        ArgumentNullException.ThrowIfNull(song);

        if (Songs.Any(s => s.Title.Equals(song.Title))) return;

        Songs.Add(song);
    }

    /// <summary>
    ///     Returns an enumerator that iterates through the SongList.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the SongList.</returns>
    public IEnumerator<Song> GetEnumerator()
    {
        return Songs.GetEnumerator();
    }

    /// <summary>
    ///     Removes all songs from this song list.
    /// </summary>
    public void Clear()
    {
        Songs.Clear();
    }

    /// <summary>
    ///     Checks whether the song list contains a specific song.
    /// </summary>
    /// <param name="item">The song to search for.</param>
    /// <returns>True if the song list contains the specified song; otherwise, false.</returns>
    public bool Contains(Song item)
    {
        return Songs.Contains(item);
    }

    /// <summary>
    ///     Copies the elements of the SongList to an array, starting at the specified array index.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied from the SongList.</param>
    /// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
    /// <exception cref="ArgumentNullException">Thrown when the song parameter is null.</exception>
    /// <exception cref="IndexOutOfRangeException">
    ///     Thrown when the arrayIndex is less than 0 or greater than or equal to the
    ///     length of the array.
    /// </exception>
    public void CopyTo(Song[] array, int arrayIndex)
    {
        Songs.ForEach(s => array[arrayIndex++] = s);
    }

    /// <summary>
    ///     Removes a song from the song list.
    /// </summary>
    /// <param name="song">The song to remove.</param>
    /// <returns>True if the song is successfully removed; otherwise, false.</returns>
    bool ICollection<Song>.Remove(Song song)
    {
        return Songs.Remove(song);
    }

    /// <summary>
    ///     Returns an enumerator that iterates through the list of songs.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the list of songs.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    ///     Determines whether the current SongList object is equal to another SongList object.
    /// </summary>
    /// <param name="other">The SongList to compare with the current object.</param>
    /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
    public bool Equals(SongList? other)
    {
        return other != null && Songs.SequenceEqual(other.Songs);
    }

    /// <summary>
    ///     Represents a list of songs associated with a radio station.
    /// </summary>
    /// <remarks>
    ///     This class implements the ICollection&lt;Song&gt;, IEquatable&lt;SongList&gt;, and ICloneable interfaces.
    /// </remarks>
    public override bool Equals(object? obj)
    {
        return Equals(obj as SongList);
    }

    public override int GetHashCode()
    {
        //Create a hash code that depends on the contents of the list
        unchecked
        {
            return this.Aggregate(19, (current, song) => current * 31 + song.GetHashCode());
        }
    }
}