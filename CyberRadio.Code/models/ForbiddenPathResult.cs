// ForbiddenPathResult.cs : RadioExt-Helper
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

namespace RadioExt_Helper.models;

/// <summary>
/// Represents the result of a forbidden path check.
/// </summary>
public class ForbiddenPathResult
{
    /// <summary>
    /// Indicates if the path is forbidden.
    /// </summary>
    public bool IsForbidden { get; set; }

    /// <summary>
    /// The reason the path is considered forbidden. The description of this enum can be used for displaying in message boxes.
    /// </summary>
    public ForbiddenPathReason Reason { get; set; }
}