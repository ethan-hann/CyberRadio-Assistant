using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility;

/// <summary>
/// Represents the type of station, indicating whether it replaces an existing station or is an additional
/// station.
/// </summary>
/// <remarks>This enumeration is used to distinguish between stations that serve as replacements for
/// existing vanilla stations  and those that are added as new stations without replacing any existing
/// ones.</remarks>
public enum StationType
{
    /// <summary>
    /// Specifies that the station is a replacement for an existing vanilla station.
    /// </summary>
    Replacement,

    /// <summary>
    /// Specifies that the station is an additional station that does not replace any existing vanilla station.
    /// </summary>
    Additional
}