// WindowSize.cs : RadioExt-Helper
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
///     Represents the window size of the application.
/// </summary>
/// <param name="width">The width of the window.</param>
/// <param name="height">The height of the window.</param>
public class WindowSize(int width, int height)
{
    /// <summary>
    ///     Create a new window size with the default values.
    /// </summary>
    public WindowSize() : this(1240, 600)
    {
    }

    /// <summary>
    ///     The width of the window.
    /// </summary>
    [Config("width")]
    public int Width { get; set; } = width;

    /// <summary>
    ///     The height of the window.
    /// </summary>
    [Config("height")]
    public int Height { get; set; } = height;

    /// <summary>
    ///     Get a value indicating whether the window size is empty.
    /// </summary>
    /// <returns>true if both the width and height are equal to 0; false, otherwise.</returns>
    public bool IsEmpty()
    {
        return Width == 0 && Height == 0;
    }
}