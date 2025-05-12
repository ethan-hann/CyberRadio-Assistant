// EnumHelper.cs : RadioExt-Helper
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
using System.Reflection;

namespace RadioExt_Helper.utility;

/// <summary>
/// Contains helper methods for working with Enum types.
/// </summary>
/// <typeparam name="T">The Enum type to get descriptions of.</typeparam>
public static class EnumHelper<T> where T : Enum
{
    /// <summary>
    /// Gets all the field descriptions of the specified Enum class.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}" /> that contains the descriptions.</returns>
    public static IEnumerable<string?> GetEnumDescriptions()
    {
        return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => f.GetCustomAttribute<DescriptionAttribute>()?.Description)
            .Where(d => d != null);
    }
}