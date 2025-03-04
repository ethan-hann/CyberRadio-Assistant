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
    public Color BackgroundColor { get; set; } = Color.White;
    public Color ForegroundColor { get; set; } = Color.Black;

    public ButtonStyle PrimaryButton { get; set; } = new();
    public ButtonStyle SecondaryButton { get; set; } = new();
    public ButtonStyle DangerButton { get; set; } = new();

    public Color TextBoxBackground { get; set; } = Color.White;
    public Color TextBoxTextColor { get; set; } = Color.Black;
    public Color LabelTextColor { get; set; } = Color.Black;

    public Color ListViewBackground { get; set; } = Color.White;
    public Color ListViewTextColor { get; set; } = Color.Black;

    public Color MenuStripBackground { get; set; } = Color.White;
    public Color MenuStripTextColor { get; set; } = Color.Black;

    public string FontFamily { get; set; } = "Segoe UI";
    public float FontSize { get; set; } = 10.0f;

    public string IconSet { get; set; } = "light";

    public bool HighContrast { get; set; } = false;
}