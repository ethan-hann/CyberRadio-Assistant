using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility
{
    /// <summary>
    /// Represents the various levels of compression that can be used for the backup operation.
    /// </summary>
    public enum CompressionLevel
    {
        None = 0,          // No compression (ratio: 1.0)
        Fastest = 1,     // Very low compression (ratio: 0.9)
        Fast = 2,       // Low compression (ratio: 0.8)
        SuperFast = 3,          // Medium-low compression (ratio: 0.7)
        Normal = 4,        // Medium compression (ratio: 0.6)
        High = 5,          // Medium-high compression (ratio: 0.5)
        Maximum = 6,       // High compression (ratio: 0.4)
        Ultra = 7,         // Very high compression (ratio: 0.3)
        Extreme = 8,       // Maximum compression (ratio: 0.25)
        Ultimate = 9       // Ultra compression (ratio: 0.2)
    }
}
