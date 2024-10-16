using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.models;

/// <summary>
/// Defines the state of a station while it is being previewed.
/// </summary>
public enum StationState
{
    /// <summary>
    /// The station is currently playing audio.
    /// </summary>
    Playing,

    /// <summary>
    /// The station audio is currently paused.
    /// </summary>
    Paused,

    /// <summary>
    /// The station audio is currently stopped. Stopped is different from paused in that it will reset the audio to the beginning of the playlist.
    /// </summary>
    Stopped
}