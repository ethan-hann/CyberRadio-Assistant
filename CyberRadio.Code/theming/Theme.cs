using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.theming
{
    /// <summary>
    /// Represents a theme for the application.
    /// </summary>
    public class Theme
    {
        public Color BackgroundColor { get; set; } = Color.White;
        public Color ForegroundColor { get; set; } = Color.Black;

        // Buttons
        public ButtonStyle PrimaryButton { get; set; } = new ButtonStyle();
        public ButtonStyle SecondaryButton { get; set; } = new ButtonStyle();
        public ButtonStyle DangerButton { get; set; } = new ButtonStyle();

        // TextBoxes
        public Color TextBoxBackground { get; set; } = Color.White;
        public Color TextBoxTextColor { get; set; } = Color.Black;

        // Labels
        public Color LabelTextColor { get; set; } = Color.Black;

        // ListView
        public Color ListViewBackground { get; set; } = Color.White;
        public Color ListViewTextColor { get; set; } = Color.Black;

        // MenuStrip
        public Color MenuStripBackground { get; set; } = Color.Gray;
        public Color MenuStripTextColor { get; set; } = Color.White;

        // Font Customization
        public string FontFamily { get; set; } = "Segoe UI";
        public float FontSize { get; set; } = 9.0f;

        // Icon Settings
        public string IconSet { get; set; } = "light"; // User can specify "light" or "dark"
        public Dictionary<string, string> IconMappings { get; set; } = new Dictionary<string, string>();

        // Accessibility Options
        public bool HighContrast { get; set; } = false;
    }
}
