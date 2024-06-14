using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.models
{
    /// <summary>
    /// Wrapper class for a language string and the associated country flag. Used for the custom Image Combo Box.
    /// </summary>
    public class LanguageItem(string language, Image flag)
    {
        /// <summary>
        /// The language string.
        /// </summary>
        public string Language { get; set; } = language;

        /// <summary>
        /// An image of the country flag associated with this language.
        /// </summary>
        public Image Flag { get; set; } = flag;

        public override string ToString()
        {
            return Language;
        }
    }
}
