// FilePreview.cs : RadioExt-Helper
// Copyright (C) 2024  Ethan Hann
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
/// Simple class to hold information about a file for previewing purposes. Holds the file name and size.
/// </summary>
public sealed class FilePreview
{
    /// <summary>
    /// The filename of the file.
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// The size, in bytes, of the file.
    /// </summary>
    public long Size { get; set; }
}