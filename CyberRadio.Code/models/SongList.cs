using System.Collections;
using Newtonsoft.Json;

namespace RadioExt_Helper.models;

/// <summary>
///     Represents a list of songs associated with a radio station.
/// </summary>
public class SongList : ICollection<Song>, IEquatable<SongList>, ICloneable
{
    /// <summary>
    ///     The songs for the radio station, as a serializable list.
    /// </summary>
    [JsonProperty("songs")]
    public List<Song> Songs { get; set; } = [];

    public int Count => Songs.Count;

    public bool IsReadOnly => false;

    public void Add(Song song)
    {
        ArgumentNullException.ThrowIfNull(song);

        if (Songs.Any(s => s.Name.Equals(song.Name))) return;

        Songs.Add(song);
    }

    public IEnumerator<Song> GetEnumerator()
    {
        return Songs.GetEnumerator();
    }

    public void Clear()
    {
        Songs.Clear();
    }

    public bool Contains(Song item)
    {
        return Songs.Contains(item);
    }

    public void CopyTo(Song[] array, int arrayIndex)
    {
        Songs.ForEach(s => array[arrayIndex++] = s);
    }

    bool ICollection<Song>.Remove(Song item)
    {
        return Songs.Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool Equals(SongList? other)
    {
        if (other == null) return false;
        return Songs.SequenceEqual(other.Songs);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as SongList);
    }

    public override int GetHashCode()
    {
        //Create a hash code that depends on the contents of the list
        unchecked
        {
            int hash = 19;
            foreach (var song in this)
            {
                hash = hash * 31 + (song?.GetHashCode() ?? 0);
            }
            return hash;
        }
    }

    public object Clone()
    {
        var clone = new SongList();
        foreach (var song in Songs)
        {
            clone.Add((Song)song.Clone());
        }
        return clone;
    }
}