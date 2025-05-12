// LogViewerControl.cs : RadioExt-Helper
// Copyright (C) 2025  Ethan Hann
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using AetherUtils.Core.Logging;
using RadioExt_Helper.user_controls;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.custom_controls;

public partial class LogViewerControl : UserControl, IUserControl
{
    private readonly string _logFilePath = GlobalData.GetLogFilePath();
    private int _currentDisplayIndex;

    private List<string> _filteredLogLines;

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

    public string? Identifier { get; set; } =
        "gobbledygook"; //something totally random that should never show up in the log file :)

    public void Translate()
    {
        grpLogSearchHeader.Text = Strings.LogViewerControl_grpLogSearchHeader_Text;
        txtSearch.PlaceholderText = Strings.LogViewerControl_txtSearch_PlaceholderText;
        btnShowMore.Text = Strings.LogViewerControl_btnShowMore_Text;

        dgvLogs.Columns[0].HeaderText = Strings.LogViewerControl_dgvLogs_Columns_0_HeaderText;
        dgvLogs.Columns[1].HeaderText = Strings.LogViewerControl_dgvLogs_Columns_1_HeaderText;
    }

    public void AddLogEntry(DateTime timestamp, string message)
    {
        this.SafeInvoke(() => { dgvLogs.Rows.Insert(0, timestamp.ToString("yyyy-MM-dd HH:mm:ss.ffff"), message); });
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
            MessageBox.Show(
                string.Format(Strings.LogViewerControl_LoadRelevantLogEntries_Failed_to_load_log_data___0_, ex.Message),
                Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            AuLogger.GetCurrentLogger<LogViewerControl>("LoadRelevantLogEntries")
                .Error(ex, "Couldn't open log file for reading.");
        }
    }

    public void DisplayLastLines(int lineCount)
    {
        try
        {
            // Clear existing rows in DataGridView
            dgvLogs.Rows.Clear();

            // Determine how many lines to display, limiting to the available number of filtered lines
            var linesToDisplay = Math.Min(lineCount, _filteredLogLines.Count);

            // Calculate the start index
            var startIndex = Math.Max(0, _filteredLogLines.Count - linesToDisplay);

            // Iterate over the range of lines to add them to the DataGridView
            for (var i = startIndex; i < _filteredLogLines.Count; i++)
            {
                var logParts = _filteredLogLines[i].Split('|', 4);
                if (logParts.Length != 4) continue;

                var timestamp = logParts[0]; // Extract timestamp
                var message = logParts[3]; // Extract message

                // Add the entry to the DataGridView
                dgvLogs.Rows.Insert(0, timestamp, message);
            }

            // Update the current display index to keep track of how many lines have been displayed
            _currentDisplayIndex += linesToDisplay;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                string.Format(Strings.LogViewerControl_DisplayLastLines_Failed_to_display_log_lines___0_, ex.Message),
                Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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