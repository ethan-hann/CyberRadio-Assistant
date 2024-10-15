using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility;

/// <summary>
/// Provides extension methods for collections.
/// </summary>
public static class CollectionExtensions
{
    private static readonly Random Random = new();

    /// <summary>
    /// Get a random item from the collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static T GetRandomItem<T>(this ICollection<T> collection)
    {
        if (collection == null || collection.Count == 0)
        {
            throw new ArgumentException("The collection cannot be null or empty.");
        }

        var randomIndex = Random.Next(0, collection.Count); // Generate a random index
        foreach (var item in collection)
        {
            if (randomIndex == 0)
            {
                return item; // Return the item at the random index
            }
            randomIndex--;
        }

        // Should never get here
        throw new InvalidOperationException("Random item selection failed.");
    }

    /// <summary>
    /// Shuffle the elements of the list using the Fisher-Yates shuffle algorithm (in-place and O(n) time complexity).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <exception cref="ArgumentException"></exception>
    public static void Shuffle<T>(this IList<T> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new ArgumentException("The list cannot be null or empty.");
        }

        // Fisher-Yates shuffle algorithm
        for (var i = list.Count - 1; i > 0; i--)
        {
            var j = Random.Next(0, i + 1); // Get a random index between 0 and i (inclusive)
            
            // Swap list[i] with list[j]
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    /// <summary>
    /// Shuffle the elements of the list while preserving the order of "ordered" items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="isOrdered"></param>
    /// <returns></returns>
    public static List<T> OrderedShuffle<T>(this List<T> list, Func<T, bool> isOrdered)
    {
        // Separate ordered and non-ordered items
        var orderedItems = list.Where(isOrdered).ToList();
        var nonOrderedItems = list.Where(item => !isOrdered(item)).ToList();

        // Shuffle the non-ordered items
        var rng = new Random();
        nonOrderedItems = nonOrderedItems.OrderBy(_ => rng.Next()).ToList();

        // Now merge the ordered items back into the list at random positions
        var resultList = new List<T>();
        int orderedIndex = 0, nonOrderedIndex = 0;

        while (orderedIndex < orderedItems.Count || nonOrderedIndex < nonOrderedItems.Count)
        {
            // Randomly decide whether to place an ordered item or non-ordered item
            if (orderedIndex < orderedItems.Count && rng.NextDouble() < 0.5)
            {
                resultList.Add(orderedItems[orderedIndex]);
                orderedIndex++;
            }
            else if (nonOrderedIndex < nonOrderedItems.Count)
            {
                resultList.Add(nonOrderedItems[nonOrderedIndex]);
                nonOrderedIndex++;
            }
        }

        return resultList;
    }
}