// ForbiddenKeyword.cs : RadioExt-Helper
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

using AetherUtils.Core.Attributes;

namespace RadioExt_Helper.config;

/// <summary>
/// Represents a forbidden keyword that should not be present in a path.
/// </summary>
public class ForbiddenKeyword(string group, string keyword, bool isForbidden)
{
    /// <summary>
    /// Empty constructor for serialization.
    /// </summary>
    public ForbiddenKeyword() : this(string.Empty, string.Empty, false)
    {
    }

    /// <summary>
    /// The group that the keyword belongs to.
    /// </summary>
    [Config("group")]
    public string Group { get; set; } = group;

    /// <summary>
    /// The keyword that is forbidden.
    /// </summary>
    [Config("keyword")]
    public string Keyword { get; set; } = keyword;

    /// <summary>
    /// Indicates if the keyword is forbidden.
    /// </summary>
    [Config("isForbidden")]
    public bool IsForbidden { get; set; } = isForbidden;
}