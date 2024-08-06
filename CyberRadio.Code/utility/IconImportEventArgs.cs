using RadioExt_Helper.models;

namespace RadioExt_Helper.utility;

/// <summary>
/// Event data for when an icon is imported.
/// </summary>
/// <param name="icon">The <see cref="CustomIcon"/> definition for the imported icon.</param>
public class IconImportEventArgs(CustomIcon icon)
{
    /// <summary>
    /// The custom icon that was imported.
    /// </summary>
    public CustomIcon Icon { get; private set; } = icon;
}