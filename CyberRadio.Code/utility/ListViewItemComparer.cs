using System.Collections;

namespace RadioExt_Helper.utility;

public class ListViewItemComparer(int column, SortOrder order) : IComparer
{
    public int Column { get; } = column;

    public SortOrder Order { get; set; } = order;

    public int Compare(object? x, object? y)
    {
        var returnVal = -1;

        if (x is ListViewItem item1 && y is ListViewItem item2)
            returnVal = string.Compare(item1.SubItems[Column].Text, item2.SubItems[Column].Text,
                StringComparison.OrdinalIgnoreCase);

        //Determine whether the sort order is descending
        if (Order == SortOrder.Descending)
            returnVal *= -1;
        return returnVal;
    }
}