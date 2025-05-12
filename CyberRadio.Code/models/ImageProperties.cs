// ImageProperties.cs : RadioExt-Helper
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

using System.Drawing.Imaging;

namespace RadioExt_Helper.models;

/// <summary>
/// Represents the properties of an image. Used when displaying images in the Icon Manager.
/// </summary>
public class ImageProperties
{
    public int Width { get; set; }
    public int Height { get; set; }
    public ImageFormat ImageFormat { get; set; } = ImageFormat.Png;
    public PixelFormat PixelFormat { get; set; } = PixelFormat.DontCare;
}