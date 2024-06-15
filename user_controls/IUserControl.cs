using RadioExt_Helper.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.user_controls
{
    public interface IUserControl
    {
        /// <summary>
        /// Unique name to identify and associate this control.
        /// </summary>
        public string UniqueName { get; set; }

        /// <summary>
        /// The station associated with this control.
        /// </summary>
        public Station Station { get; set; }

        /// <summary>
        /// Specify how embedded fonts are to be applied to children of this control.
        /// </summary>
        public void ApplyFonts();
    }
}
