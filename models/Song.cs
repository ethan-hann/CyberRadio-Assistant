using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.models
{
    /// <summary>
    /// Represents a single song entry. Song information is read from the file on disk.
    /// </summary>
    public class Song
    {
        /// <summary>
        /// The name of the song.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The artist of the song or audio file.
        /// </summary>
        [JsonProperty("artist")]
        public string Artist { get; set; } = string.Empty;

        /// <summary>
        /// The duration of the song or audio file.
        /// </summary>
        [JsonProperty("duration")]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// The file size (in bytes) of the song or audio file.
        /// </summary>
        [JsonProperty("size")]
        public ulong Size { get; set; }

        /// <summary>
        /// The original file path of the song or audio file on disk.
        /// </summary>
        [JsonProperty("original_path")]
        public string OriginalFilePath { get; set; } = string.Empty;

        public override string ToString() => Name;
    }
}
