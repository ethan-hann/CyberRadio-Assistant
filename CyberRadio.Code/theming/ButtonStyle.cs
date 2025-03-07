using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.theming
{
    /// <summary>
    /// Represents a style for a button.
    /// </summary>
    public sealed class ButtonStyle
    {
        /// <summary>
        /// The background color of the button.
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.Yellow;

        /// <summary>
        /// The text color of the button.
        /// </summary>
        public Color TextColor { get; set; } = Color.Black;

        /// <summary>
        /// The border color of the button.
        /// </summary>
        public Color BorderColor { get; set; } = Color.Black;

        /// <summary>
        /// The background color of the button when the mouse is pressed while over it.
        /// </summary>
        public Color MouseDownBackColor { get; set; } = Color.FromArgb(0, 122, 255);

        /// <summary>
        /// The background color of the button when the mouse is over it.
        /// </summary>
        public Color MouseOverBackColor { get; set; } = Color.FromArgb(2, 215, 242);

        /// <summary>
        /// The thickness of the border of the button.
        /// </summary>
        public int BorderThickness { get; set; } = 1;

        /// <summary>
        /// The radius of the border of the button.
        /// </summary>
        public int BorderRadius { get; set; } = 0;

        /// <summary>
        /// Whether the button should be flat instead of raised.
        /// </summary>
        public bool IsFlat { get; set; } = true;

        /// <summary>
        /// Whether the button should use a shadow. Does not apply to flat buttons.
        /// </summary>
        public bool UseShadow { get; set; } = false;

        /// <summary>
        /// The font family to use for the button.
        /// </summary>
        public string FontFamily { get; set; } = "Segoe UI";

        /// <summary>
        /// The font size to use for the button.
        /// </summary>
        public float FontSize { get; set; } = 9.0f;

        /// <summary>
        /// The font style to use for the button.
        /// </summary>
        public string FontStyle { get; set; } = "Regular";
    }
}
