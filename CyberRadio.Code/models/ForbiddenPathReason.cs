// ForbiddenPathReason.cs : RadioExt-Helper
// Copyright (C) 2025  Ethan Hann
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

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