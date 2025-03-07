using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.theming
{
    /// <summary>
    /// Represents the theme to use for icons.
    /// </summary>
    public enum IconTheme
    {
        /// <summary>
        /// Black icons on a light background.
        /// </summary>
        Light,
        /// <summary>
        /// White icons on a dark background.
        /// </summary>
        Dark,
        /// <summary>
        /// The default icon theme. This is the same as <see cref="Light"/>.
        /// </summary>
        Default = Light
    }
}
