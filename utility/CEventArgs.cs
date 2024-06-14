using RadioExt_Helper.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility
{
    public abstract class CEventArgs
    {
        /// <summary>
        /// Passed when a station is modified.
        /// </summary>
        /// <param name="metaData">The metadata describing the station.</param>
        /// <param name="previousStationName">The name of the station, before it was modified.</param>
        public class StationUpdatedEventArgs(MetaData metaData, SongList songList, string previousStationName) : EventArgs
        {
            /// <summary>
            /// The metadata describing the station.
            /// </summary>
            public MetaData MetaData { get; private set; } = metaData;

            /// <summary>
            /// The list of songs for the station. Empty if using stream instead.
            /// </summary>
            public SongList Songs { get; private set; } = songList;

            /// <summary>
            /// The name of the station, before it was modified.
            /// </summary>
            public string PreviousStationName { get; private set; } = previousStationName;
        }

        /// <summary>
        /// Passed when a song list is modified.
        /// </summary>
        /// <param name="songList">The modified song list.</param>
        public class SongListUpdatedEventArgs(SongList songList) : EventArgs
        {
            /// <summary>
            /// The list of songs for the station.
            /// </summary>
            public SongList Songs { get; private set; } = songList;
        }
    }
}
