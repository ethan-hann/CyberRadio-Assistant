using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility
{
    /// <summary>
    /// Represents the type of icon editor to use initially. Either editing an icon from a PNG file or from an archive file.
    /// </summary>
    public enum IconEditorType
    {
        /// <summary>
        /// Indicates that the icon editor was initialized from a PNG file.
        /// </summary>
        FromPng,
        /// <summary>
        /// Indicates that the icon editor was initialized from an archive file.
        /// </summary>
        FromArchive
    }
}
