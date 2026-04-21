using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms
{
    /// <summary>
    /// Represents a window that displays live log output to the user.
    /// </summary>
    /// <remarks>The log window can be initialized with a specific log file path or will use the application's
    /// default log file if none is provided. The window manages the lifecycle of the log viewer, starting and stopping
    /// log monitoring as the window is shown or closed.</remarks>
    public partial class LogWindow : Form
    {
        private readonly string? _logFilePath;

        /// <summary>
        /// Initializes a new instance of the LogWindow class.
        /// </summary>
        /// <remarks>This constructor sets up the log window and prepares its components for
        /// use.</remarks>
        public LogWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the LogWindow class using the specified log file path.
        /// </summary>
        /// <param name="logFilePath">The full path to the log file to be displayed in the window. Cannot be null or empty.</param>
        public LogWindow(string logFilePath) : this()
        {
            _logFilePath = logFilePath;
        }

        /// <summary>
        /// Updates the user interface text to reflect the current language settings.
        /// </summary>
        /// <remarks>This method should be called after a language change to ensure that all UI elements
        /// display the correct localized text.</remarks>
        public void Translate()
        {
            Text = Strings.LiveLog_Title;
            liveLogViewer1.Translate();
        }

        private void LogWindow_Load(object? sender, EventArgs e)
        {
            Translate();

            liveLogViewer1.Start(_logFilePath ?? GlobalData.GetLogFilePath());
        }

        private void LogWindow_FormClosing(object? sender, FormClosingEventArgs e)
        {
            liveLogViewer1.Stop();
        }
    }
}
