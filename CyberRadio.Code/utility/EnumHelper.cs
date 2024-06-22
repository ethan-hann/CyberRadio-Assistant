using System.ComponentModel;
using System.Reflection;

namespace RadioExt_Helper.utility;

public abstract class EnumHelper
{
    /// <summary>
    ///     Get all the field descriptions of the specified Enum class.
    /// </summary>
    /// <typeparam name="T">The Enum type to get descriptions of.</typeparam>
    /// <returns>An <see cref="IEnumerable{T}" /> that contains the descriptions.</returns>
    public static IEnumerable<string?> GetEnumDescriptions<T>() where T : Enum
    {
        return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => f.GetCustomAttribute<DescriptionAttribute>()?.Description)
            .Where(d => d != null);
    }
}