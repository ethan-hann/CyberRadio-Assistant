// AssemblyExtensions.cs : RadioExt-Helper
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
}