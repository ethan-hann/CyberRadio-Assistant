using RadioExt_Helper.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility
{
    public class CEventArgs
    {
        public class StationUpdatedEventArgs(MetaData metaData) : EventArgs
        {
            public MetaData MetaData { get; private set; } = metaData;
        }
    }
}
