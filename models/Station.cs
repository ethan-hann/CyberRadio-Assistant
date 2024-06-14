using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.models
{
    public class Station
    {
        public MetaData MetaData { get; set; } = new();
        public SongList SongList { get; set; } = [];
    }
}
