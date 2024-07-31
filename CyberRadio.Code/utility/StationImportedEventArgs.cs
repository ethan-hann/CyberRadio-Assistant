using RadioExt_Helper.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility
{
    public class StationImportedEventArgs(TrackableObject<Station> station, 
        string? iconFilePath, string? iconFileName) : EventArgs
    {
        public TrackableObject<Station> Station { get; private set; } = station;
        public string? IconFilePath { get; private set; } = iconFilePath;
        public string? IconFileName { get; private set; } = iconFileName;
    }
}
