using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.models
{
    /// <summary>
    /// Wrapper that represents a single radio station. Contains metadata and a song list for the station.
    /// </summary>
    public class Station
    {
        /// <summary>
        /// The metadata associated with this station.
        /// </summary>
        public MetaData MetaData { get; set; } = new();

        /// <summary>
        /// The song list for this station; empty if <see cref="MetaData.StreamInfo.IsStream"/> is <c>true</c>.
        /// </summary>
        public SongList SongList { get; set; } = [];
    }
}
