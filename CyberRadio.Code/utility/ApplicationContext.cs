using RadioExt_Helper.forms;

namespace RadioExt_Helper.utility
{
    /// <summary>
    /// Keeps a reference to the main form instance.
    /// </summary>
    internal class ApplicationContext
    {
        internal static MainForm? MainFormInstance { get; set; }
    }
}
