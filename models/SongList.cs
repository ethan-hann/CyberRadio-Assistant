using System.Collections;
using Newtonsoft.Json;

namespace RadioExt_Helper.models
{
    /// <summary>
    /// Represents a list of songs associated with a radio station.
    /// </summary>
    public class SongList : ICollection<Song>
    {
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

        public void Remove(Song song)
        {
            ArgumentNullException.ThrowIfNull(song);
            Songs.Remove(song);
        }

        public void Remove(string songName)
        {
            ArgumentException.ThrowIfNullOrEmpty(songName);

            Songs.Remove(Songs.First(s => s.Name.Equals(songName)));
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
            Remove(item);
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
