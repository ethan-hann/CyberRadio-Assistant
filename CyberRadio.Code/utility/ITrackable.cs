using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility
{
    public interface ITrackable
    {
        void AcceptChanges();
        void DeclineChanges();
        bool IsPendingSave { get; }
    }
}
