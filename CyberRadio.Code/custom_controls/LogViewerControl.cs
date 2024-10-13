using AetherUtils.Core.Logging;
using RadioExt_Helper.user_controls;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.custom_controls
{
    public partial class LogViewerControl : UserControl, IUserControl
    {
        private readonly string _logFilePath = GlobalData.GetLogFilePath();

        private List<string> _filteredLogLines;
        private int _currentDisplayIndex;

        public string? Identifier { get; set; } = "gobbledygook"; //something totally random that should never show up in the log file :)

        public LogViewerControl()
        {
            InitializeComponent();
            _filteredLogLines = [];
        }

        public LogViewerControl(string identifier)
        {
            InitializeComponent();
            _filteredLogLines = [];
            Identifier = identifier;

            LoadRelevantLogEntries();
            DisplayLastLines(50);
        }

        public void Translate()
        {
            //TODO: translate the control
        }

        public void AddLogEntry(DateTime timestamp, string message)
        {
            this.SafeInvoke(() =>
            {
                dgvLogs.Rows.Insert(0, timestamp.ToString("yyyy-MM-dd HH:mm:ss.ffff"), message);
            });
        }

        public void LoadRelevantLogEntries()
        {
            try
            {
                var fileInfo = new FileInfo(_logFilePath)
                {
                    IsReadOnly = false
                };

                //Remove the read-only flags
                File.SetAttributes(fileInfo.FullName, FileAttributes.Normal);

                using var fs = new FileStream(_logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var sr = new StreamReader(fs);

                // Read the entire log file once to get the relevant entries
                var logLines = sr.ReadToEnd().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

                // Filter relevant log lines based on the identifier (atlas name or archive path or nothing if the Identifier is null)
                if (Identifier == null)
                    _filteredLogLines = [];
                else
                    _filteredLogLines = logLines
                    .Where(line => line.Contains(Identifier, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Strings.LogViewerControl_LoadRelevantLogEntries_Failed_to_load_log_data___0_, ex.Message), 
                    Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuLogger.GetCurrentLogger<LogViewerControl>("LoadRelevantLogEntries").Error(ex, "Couldn't open log file for reading.");
            }
        }

        public void DisplayLastLines(int lineCount)
        {
            try
            {
                // Clear existing rows in DataGridView
                dgvLogs.Rows.Clear();

                // Determine how many lines to display, limiting to the available number of filtered lines
                int linesToDisplay = Math.Min(lineCount, _filteredLogLines.Count);

                // Calculate the start index
                int startIndex = Math.Max(0, _filteredLogLines.Count - linesToDisplay);

                // Iterate over the range of lines to add them to the DataGridView
                for (int i = startIndex; i < _filteredLogLines.Count; i++)
                {
                    var logParts = _filteredLogLines[i].Split('|', 4);
                    if (logParts.Length != 4) continue;

                    var timestamp = logParts[0];  // Extract timestamp
                    var message = logParts[3];    // Extract message

                    // Add the entry to the DataGridView
                    dgvLogs.Rows.Insert(0, timestamp, message);
                }

                // Update the current display index to keep track of how many lines have been displayed
                _currentDisplayIndex += linesToDisplay;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Strings.LogViewerControl_DisplayLastLines_Failed_to_display_log_lines___0_, ex.Message), Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var searchQuery = txtSearch.Text.ToLower();
            foreach (DataGridViewRow row in dgvLogs.Rows)
            {
                var isVisible = row.Cells[1]?.Value?.ToString()?.ToLower().Contains(searchQuery);
                row.Visible = isVisible ?? false;
            }
        }

        private void btnShowMore_Click(object sender, EventArgs e)
        {
            DisplayLastLines(50);
        }
    }
}
