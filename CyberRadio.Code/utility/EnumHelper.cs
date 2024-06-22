using System.ComponentModel;
using System.Reflection;

namespace RadioExt_Helper.utility;

public class EnumHelper
{
    public static IEnumerable<string> GetEnumDescriptions<T>() where T : Enum
    {
        return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => f.GetCustomAttribute<DescriptionAttribute>()?.Description)
            .Where(d => d != null);
    }
}