using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility
{
    public static class ControlExtensions
    {
        /// <summary>
        /// Safely invokes the specified action on the control's thread.
        /// </summary>
        /// <param name="control">The control on which to invoke the action.</param>
        /// <param name="action">The action to invoke.</param>
        public static void SafeInvoke(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// Safely begins invoking the specified action on the control's thread.
        /// </summary>
        /// <param name="control">The control on which to begin invoking the action.</param>
        /// <param name="action">The action to begin invoking.</param>
        public static void SafeBeginInvoke(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// Safely ends invoking the specified asynchronous result on the control's thread.
        /// </summary>
        /// <param name="control">The control on which to end invoking the asynchronous result.</param>
        /// <param name="result">The asynchronous result to end invoking.</param>
        public static void SafeEndInvoke(this Control control, IAsyncResult result)
        {
            if (control.InvokeRequired)
            {
                control.EndInvoke(result);
            }
        }
    }
}
