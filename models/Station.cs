using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.models
{
    /// <summary>
    /// Wrapper that represents a single radio station. Contains all information related to the station.
    /// </summary>
    public class Station
    {
        /// <summary>
        /// The metadata associated with this station.
        /// </summary>
        public MetaData MetaData { get; set; } = new();

        ///// <summary>
        ///// The metadata associated with this station.
        ///// </summary>
        //public MetaData MetaData { get; set; } = new();

        /// <summary>
        /// The stream info associated with this station.
        /// </summary>
        public StreamInfo StreamInfo
        {
            get
            {
                return MetaData.StreamInfo;
            }
            set
            {
                MetaData.StreamInfo = value;
            }
        }

        /// <summary>
        /// The custom icon associated with this station.
        /// </summary>
        public CustomIcon CustomIcon
        {
            get
            {
                return MetaData.CustomIcon;
            }
            set
            {
                MetaData.CustomIcon = value;
            }
        }

        /// <summary>
        /// The song list for this station as a <see cref="List{T}"/>. Empty if <see cref="MetaData.StreamInfo.IsStream"/> is <c>true</c>.
        /// </summary>
        public List<Song> SongsAsList { get; set; } = [];

        /// <summary>
        /// Get a <see cref="SongList"/> representing the current songs in the <see cref="SongsAsList"/> list.
        /// </summary>
        public SongList Songs { get => [.. SongsAsList]; }
    }
}
