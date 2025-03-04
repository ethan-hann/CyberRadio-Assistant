// AssemblyExtensions.cs : RadioExt-Helper
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

using System.Reflection;

namespace RadioExt_Helper.utility;

/// <summary>
/// Contains extension methods for the <see cref="Assembly"/> class.
/// </summary>
internal static class AssemblyExtensions
{
    /// <summary>
    ///     Reads an embedded string resource from the assembly.
    /// </summary>
    /// <param name="assembly">The assembly to read from.</param>
    /// <param name="resourceName">The fully qualified name of the embedded resource.</param>
    /// <returns>A string representing the resource or <see cref="string.Empty"/> if the resource could not be found.</returns>
    public static string ReadResource(this Assembly assembly, string resourceName)
    {
        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null) return string.Empty;

        using StreamReader reader = new(stream);
        return reader.ReadToEnd();
    }

    /// <summary>
    /// Reads an embedded image resource from the assembly.
    /// </summary>
    /// <param name="assembly">The assembly to read from.</param>
    /// <param name="resourceName">The fully qualified name of the embedded image.</param>
    /// <returns>An image representing the resource or <c>null</c> if the resource could not be found.</returns>
    public static Image? GetEmbeddedIcon(this Assembly assembly, string resourceName)
    {
        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null) return null;

        using StreamReader reader = new(stream);
        return Image.FromStream(stream);
    }

    /// <summary>
    /// Extracts an embedded resource from the assembly to a temporary file.
    /// </summary>
    /// <param name="assembly">The assembly to extract from.</param>
    /// <param name="resourceName">The fully qualified name of the embedded resource.</param>
    /// <returns>A string representing the resource or <see cref="string.Empty"/> if the resource could not be found.</returns>
    public static string ExtractEmbeddedResource(this Assembly assembly, string resourceName)
    {
        var tempPath = Path.Combine(Path.GetTempPath(), resourceName);
        using var resource = assembly.GetManifestResourceStream(resourceName);
        if (resource == null) return string.Empty;

        using var file = new FileStream(tempPath, FileMode.Create, FileAccess.Write);
        resource?.CopyTo(file);

        return tempPath;
    }
}