using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.models
{
    public class LanguageItem
    {
        public string Language { get; set; }
        public Image Flag { get; set; }

        public LanguageItem(string language, Image flag)
        {
            Language = language;
            Flag = flag;
        }

        public override string ToString()
        {
            return Language;
        }
    }
}
