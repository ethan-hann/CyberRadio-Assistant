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

    /// <summary>
    /// Extracts an embedded resource from the assembly to a temporary file.
    /// </summary>
    /// <param name="assembly">The assembly to extract from.</param>
    /// <param name="resourceName">The fully qualified name of the embedded resource.</param>
    /// <returns>A string representing the resource or <see cref="string.Empty"/> if the resource could not be found.</returns>
    public static string ExtractEmbeddedResource(this Assembly assembly, string resourceName)
    {
        var tempPath = Path.Combine(Path.GetTempPath(), resourceName);
        using var resource = assembly.GetManifestResourceStream(resourceName);
        if (resource == null) return string.Empty;

        using var file = new FileStream(tempPath, FileMode.Create, FileAccess.Write);
        resource?.CopyTo(file);

        return tempPath;
    }
}