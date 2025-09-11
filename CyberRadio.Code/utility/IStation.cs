using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility
{
    /// <summary>
    /// Interface representing a radio station with a name and associated tags.
    /// Can be implemented by different types of stations (i.e., vanilla replacement or completely new stations).
    /// </summary>
    public interface IStation
    {
        /// <summary>
        /// A list of tags associated with the station.
        /// </summary>
        List<string> Tags { get; set; }

        /// <summary>
        /// The type of station (e.g., vanilla replacement or completely new station).
        /// </summary>
        StationType StationType { get; }
    }
}
