// ListViewItemComparer.cs : RadioExt-Helper
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

using System.Collections;
using RadioExt_Helper.models;

namespace RadioExt_Helper.utility;

/// <summary>
///     Custom comparer that compares between two list view items based on the column and sort order.
/// </summary>
/// <param name="column">The column index items should be compared in.</param>
/// <param name="order">The <see cref="SortOrder" /> of the comparison.</param>
public class ListViewItemComparer(int column, SortOrder order) : IComparer
{
    /// <summary>
    ///     The <see cref="ListView"/> column index that items reside in.
    /// </summary>
    public int Column { get; } = column;

    /// <summary>
    ///     The <see cref="SortOrder" /> used in the comparison.
    /// </summary>
    public SortOrder Order { get; set; } = order;

    public int Compare(object? x, object? y)
    {
        var returnVal = -1;

        if (x is ListViewItem item1 && y is ListViewItem item2)
        {
            if (item1.Tag is Song song)
            {
                if (Column == 3) //compare file size
                    returnVal = item2.Tag != null
                        ? song.FileSize.CompareTo(((Song)item2.Tag).FileSize)
                        : DefaultStringCompare(item1, item2);
                else
                    returnVal = DefaultStringCompare(item1, item2);
            }
            else
            {
                returnVal = DefaultStringCompare(item1, item2);
            }
        }

        if (Order == SortOrder.Descending)
            returnVal *= -1;
        return returnVal;
    }

    private int DefaultStringCompare(ListViewItem item1, ListViewItem item2)
    {
        return string.Compare(item1.SubItems[Column].Text, item2.SubItems[Column].Text,
            StringComparison.OrdinalIgnoreCase);
    }
}