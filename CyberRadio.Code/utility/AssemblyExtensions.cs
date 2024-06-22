using System.Reflection;

namespace RadioExt_Helper.utility;

internal static class AssemblyExtensions
{
    /// <summary>
    ///     Reads an embedded string resource from the assembly.
    /// </summary>
    /// <param name="assembly">The assembly to read from.</param>
    /// <param name="resourceName">The fully qualified name of the embedded resource.</param>
    /// <returns>A string representing the resource.</returns>
    public static string ReadResource(this Assembly assembly, string resourceName)
    {
        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null) return string.Empty;

        using StreamReader reader = new(stream);
        return reader.ReadToEnd();
    }
}