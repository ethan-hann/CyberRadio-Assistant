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
        public Color BackgroundColor { get; set; } = Color.LightGray;
        public Color TextColor { get; set; } = Color.Black;
        public Color BorderColor { get; set; } = Color.Black;
        public int BorderThickness { get; set; } = 1;
        public int BorderRadius { get; set; } = 0;
        public bool UseShadow { get; set; } = false;
    }
}
