using System.ComponentModel;

namespace RadioExt_Helper.models;

/// <summary>
/// Defines the various reasons a path may be considered forbidden.
/// </summary>
public enum ForbiddenPathReason
{
    /// <summary>
    /// No reason. The path was valid.
    /// </summary>
    None,
    /// <summary>
    /// The path was a root directory.
    /// </summary>
    [Description("ForbiddenPath_RootDirectory")]
    RootDirectory,

    /// <summary>
    /// The path was within a system directory.
    /// </summary>
    [Description("ForbiddenPath_SystemDirectory")]
    SystemDirectory,

    /// <summary>
    /// The path was within the program files directory (either x86 or x64).
    /// </summary>
    [Description("ForbiddenPath_ProgramFiles")]
    ProgramFiles,

    /// <summary>
    /// The path was within the user's appdata folder (either local or roaming).
    /// </summary>
    [Description("ForbiddenPath_UserAppData")]
    UserAppData,

    /// <summary>
    /// The path was within Vortex Mod Manager managed folder.
    /// </summary>
    [Description("ForbiddenPath_VortexFolder")]
    VortexFolder,

    /// <summary>
    /// The path contained forbidden keywords that would make it an invalid path (e.g., steam, epic games, gog galaxy, etc...).
    /// </summary>
    [Description("ForbiddenPath_KeywordMatch")]
    KeywordMatch
}