using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility.event_args
{
    /// <summary>
    /// Represents the event arguments for the various events in the <see cref="IconManager"/> class.
    /// </summary>
    public class IconManagerEventArgs(string? status, int progress, bool isError, string? errorMessage = null)
        : EventArgs
    {
        /// <summary>
        /// The status message of the event.
        /// </summary>
        public string? Status { get; set; } = status;

        /// <summary>
        /// The current progress percentage of the event.
        /// </summary>
        public int Progress { get; set; } = progress;

        /// <summary>
        /// Determines if the event is an error.
        /// </summary>
        public bool IsError { get; set; } = isError;

        /// <summary>
        /// The error message of the event.
        /// </summary>
        public string? ErrorMessage { get; set; } = errorMessage;
    }
}
