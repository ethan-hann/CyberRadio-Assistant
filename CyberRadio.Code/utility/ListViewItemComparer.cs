using System.Collections;

namespace RadioExt_Helper.utility;

/// <summary>
///     Custom comparer that compares between two list view items based on the column and sort order.
/// </summary>
/// <param name="column">The column index items should be compared in.</param>
/// <param name="order">The <see cref="SortOrder" /> of the comparison.</param>
public class ListViewItemComparer(int column, SortOrder order) : IComparer
{
    /// <summary>
    ///     The ListView column index that items reside in.
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
            returnVal = string.Compare(item1.SubItems[Column].Text, item2.SubItems[Column].Text,
                StringComparison.OrdinalIgnoreCase);

        if (Order == SortOrder.Descending)
            returnVal *= -1;
        return returnVal;
    }
}