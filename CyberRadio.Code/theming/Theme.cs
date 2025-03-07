using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.theming;

/// <summary>
/// Represents a theme for the application.
/// </summary>
public class Theme
{
    /// <summary>
    /// The name of the theme.
    /// </summary>
    public string ThemeName { get; set; } = "Default";

    /// <summary>
    /// The main background color of the application.
    /// </summary>
    public Color BackgroundColor { get; set; } = Color.White;

    /// <summary>
    /// The main foreground color of the application.
    /// </summary>
    public Color ForegroundColor { get; set; } = Color.Black;

    /// <summary>
    /// The <see cref="ButtonStyle"/> to use for primary buttons.
    /// </summary>
    public ButtonStyle PrimaryButton { get; set; } = new();

    /// <summary>
    /// The <see cref="ButtonStyle"/> to use for secondary buttons.
    /// </summary>
    public ButtonStyle SecondaryButton { get; set; } = new();

    /// <summary>
    /// The <see cref="ButtonStyle"/> to use for danger buttons.
    /// </summary>
    public ButtonStyle DangerButton { get; set; } = new();

    /// <summary>
    /// The background color of text boxes.
    /// </summary>
    public Color TextBoxBackground { get; set; } = Color.White;

    /// <summary>
    /// The text color of text boxes.
    /// </summary>
    public Color TextBoxTextColor { get; set; } = Color.Black;

    /// <summary>
    /// The color to use for labels.
    /// </summary>
    public Color LabelTextColor { get; set; } = Color.Black;

    /// <summary>
    /// The background color of list views.
    /// </summary>
    public Color ListViewBackground { get; set; } = Color.White;

    /// <summary>
    /// The color to use for list view text.
    /// </summary>
    public Color ListViewTextColor { get; set; } = Color.Black;

    /// <summary>
    /// The background color of menu strips. Also applies to tool strips and status strips.
    /// </summary>
    public Color MenuStripBackground { get; set; } = Color.White;

    /// <summary>
    /// The color to use for menu strip text.
    /// </summary>
    public Color MenuStripTextColor { get; set; } = Color.Black;

    /// <summary>
    /// The font family to use for menu strips.
    /// </summary>
    public string MenuStripFontFamily { get; set; } = "Segoe UI";

    /// <summary>
    /// The font size to use for menu strips.
    /// </summary>
    public float MenuStripFontSize { get; set; } = 9.0f;

    /// <summary>
    /// The font style to use for menu strips.
    /// </summary>
    public string MenuStripFontStyle { get; set; } = "SemiBold";

    /// <summary>
    /// The font family to use for the application.
    /// </summary>
    public string FontFamily { get; set; } = "Segoe UI";

    /// <summary>
    /// The font size to use for the application.
    /// </summary>
    public float FontSize { get; set; } = 9.0f;

    /// <summary>
    /// The font style to use for the application.
    /// </summary>
    public string FontStyle { get; set; } = "Regular";

    /// <summary>
    /// The icon set to use for the application.
    /// </summary>
    public string IconSet { get; set; } = "Default";

    /// <summary>
    /// Whether to use high contrast mode.
    /// </summary>
    public bool HighContrast { get; set; } = false;
}