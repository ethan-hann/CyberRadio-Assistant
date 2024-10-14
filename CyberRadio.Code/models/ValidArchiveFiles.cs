using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.models
{
    /// <summary>
    /// Enum representing the valid archive files recognized by Cyberpunk 2077.
    /// This enum is mainly used for the description attributes (which are the file extensions) and not the enum values themselves.
    /// </summary>
    public enum ValidArchiveFiles
    {
        [Description(".archive")]
        CdprArchive
    }
}
