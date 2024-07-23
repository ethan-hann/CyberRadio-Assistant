// EnumerableExtensions.cs : RadioExt-Helper
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

using System.ComponentModel;

namespace RadioExt_Helper.utility;

/// <summary>
/// Extension methods for <see cref="IEnumerable{T}"/>.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Generates a <see cref="BindingList{T}"/> from the specified <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of object contained in the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <param name="source">The <see cref="IEnumerable{T}"/> to get a <see cref="BindingList{T}"/> for.</param>
    /// <returns>A <see cref="BindingList{T}"/> representing the <see cref="IEnumerable{T}"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="source"/> was <c>null</c>.</exception>
    public static BindingList<T> ToBindingList<T>(this IEnumerable<T> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        return new BindingList<T>(new List<T>(source));
    }
}