using AetherUtils.Core.Attributes;

namespace RadioExt_Helper.config;

/// <summary>
///     Represents the window size of the application.
/// </summary>
/// <param name="width">The width of the window.</param>
/// <param name="height">The height of the window.</param>
public class WindowSize(int width, int height)
{
    /// <summary>
    ///     Create a new window size with the default values.
    /// </summary>
    public WindowSize() : this(1240, 600)
    {
    }

    /// <summary>
    ///     The width of the window.
    /// </summary>
    [Config("width")]
    public int Width { get; set; } = width;

    /// <summary>
    ///     The height of the window.
    /// </summary>
    [Config("height")]
    public int Height { get; set; } = height;

    /// <summary>
    /// Get a value indicating whether the window size is empty.
    /// </summary>
    /// <returns>true if both the width and height are equal to 0; false, otherwise.</returns>
    public bool IsEmpty()
    {
        return Width == 0 && Height == 0;
    }
}