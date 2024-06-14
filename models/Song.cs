using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.models
{
    /// <summary>
    /// Represents a single song entry.
    /// </summary>
    public class Song
    {
        /// <summary>
        /// The name of the song; retrieved from the file path.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The length (in seconds) of the song.
        /// </summary>
        [JsonProperty("length")]
        public float Length { get; set; }

        /// <summary>
        /// The file size (in bytes) of the song.
        /// </summary>
        [JsonProperty("size")]
        public ulong Size { get; set; }

        /// <summary>
        /// The original file path of the song on disk.
        /// </summary>
        [JsonProperty("original_path")]
        public string OriginalFilePath { get; set; } = string.Empty;
    }
}
