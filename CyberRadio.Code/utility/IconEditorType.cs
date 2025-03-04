// IconEditorType.cs : RadioExt-Helper
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

namespace RadioExt_Helper.utility;

/// <summary>
/// Represents the type of icon editor to use initially. Either editing an icon from a PNG file or from an archive file.
/// </summary>
public enum IconEditorType
{
    /// <summary>
    /// Indicates that the icon editor was initialized from a PNG file.
    /// </summary>
    FromPng,

    /// <summary>
    /// Indicates that the icon editor was initialized from an archive file.
    /// </summary>
    FromArchive
}