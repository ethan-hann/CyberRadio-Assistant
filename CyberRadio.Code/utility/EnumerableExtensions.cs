// EnumerableExtensions.cs : RadioExt-Helper
// Copyright (C) 2026  Ethan Hann
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

#region

using System.ComponentModel;
using System.Text;

#endregion

namespace RadioExt_Helper.utility;

/// <summary>
///     Extension methods for <see cref="IEnumerable{T}" />.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    ///     Generates a <see cref="BindingList{T}" /> from the specified <see cref="IEnumerable{T}" />.
    /// </summary>
    /// <typeparam name="T">The type of object contained in the <see cref="IEnumerable{T}" />.</typeparam>
    /// <param name="source">The <see cref="IEnumerable{T}" /> to get a <see cref="BindingList{T}" /> for.</param>
    /// <returns>A <see cref="BindingList{T}" /> representing the <see cref="IEnumerable{T}" />.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="source" /> was <c>null</c>.</exception>
    public static BindingList<T> ToBindingList<T>(this IEnumerable<T> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        return new BindingList<T>(new List<T>(source));
    }

    /// <summary>
    /// Converts the elements of the sequence to a single string, with each element separated by a line break.
    /// </summary>
    /// <remarks>Each element is converted to its string representation using its ToString method. No
    /// delimiter is added between elements other than the line break.</remarks>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <param name="source">The sequence of elements to convert to a string. Cannot be null.</param>
    /// <returns>A string that contains the string representation of each element in the sequence, each followed by a line break.
    /// Returns an empty string if the sequence contains no elements.</returns>
    /// <exception cref="ArgumentNullException">Thrown if source is null.</exception>
    public static string ToFriendlyString<T>(this IEnumerable<T> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        var finalString = new StringBuilder();
        finalString = source.Aggregate(finalString, (current, item) => current.Append(item).Append(Environment.NewLine));

        return finalString.ToString();
    }
}