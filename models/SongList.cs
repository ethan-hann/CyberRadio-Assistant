using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.models
{
    /// <summary>
    /// Represents a list of songs associated with a radio station.
    /// </summary>
    public class SongList : IEnumerable<Song>
    {
        private List<Song> _songs = [];

        [JsonProperty("songs")]
        public List<Song> Songs { get; set; } = [];

        public void Add(Song song)
        {
            ArgumentNullException.ThrowIfNull(song);

            if (_songs.Any(s => s.Name.Equals(song.Name))) return;

            _songs.Add(song);
        }

        public void Remove(Song song)
        {
            ArgumentNullException.ThrowIfNull(song);
            _songs.Remove(song);
        }

        public void Remove(string songName)
        {
            ArgumentException.ThrowIfNullOrEmpty(songName);

            _songs.Remove(_songs.First(s => s.Name.Equals(songName)));
        }

        public IEnumerator<Song> GetEnumerator()
        {
            return _songs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
