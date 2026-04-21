using AetherUtils.Core.Logging;
using RadioExt_Helper.user_controls;
using RadioExt_Helper.utility;
using System.Text.RegularExpressions;

namespace RadioExt_Helper.custom_controls
{
    /// <summary>
    /// Provides a user control for viewing and monitoring log files in real time, displaying log entries as they are
    /// written to disk.
    /// </summary>
    /// <remarks>LiveLogViewer enables users to observe log file updates instantly, filter log entries by
    /// search terms, and interact with log data through a graphical interface. The control automatically parses log
    /// entries based on the configured log layout and updates the display as the log file changes. It is designed for
    /// integration into applications that require live log monitoring and supports thread-safe operations for
    /// concurrent log updates.</remarks>
    public partial class LiveLogViewer : UserControl, IUserControl
    {
        private readonly List<LogEntry> _entries = [];
        private readonly Lock _syncRoot = new();

        private FileSystemWatcher? _fileWatcher;
        private string _logLayout = "${longdate}|${level:uppercase=true}|${logger}|${message:withexception=true}";
        private string? _logFilePath;
        private int _levelTokenIndex = -1;
        private int _messageTokenIndex = -1;
        private long _lastReadPosition;
        private string[] _layoutSeparators = [];
        private int _timestampTokenIndex = -1;
        private string[] _layoutTokens = [];

        /// <summary>
        /// Initializes a new instance of the LiveLogViewer class.
        /// </summary>
        /// <remarks>This constructor sets up the component and subscribes to the Disposed event. Use this
        /// constructor to create and display a live log viewer in the application.</remarks>
        public LiveLogViewer()
        {
            InitializeComponent();

            Disposed += LiveLogViewer_Disposed;
        }

        /// <inheritdoc />
        public void Translate()
        {
            txtSearch.PlaceholderText = Strings.LiveLog_SearchPrompt;
            colTimestamp.HeaderText = Strings.LiveLog_DGV_Timestamp;
            colLevel.HeaderText = Strings.LiveLog_DGV_Level;
            colMessage.HeaderText = Strings.LiveLog_DGV_Message;
            copyLineToolStripMenuItem.Text = Strings.LiveLog_CopyLine;
        }

        /// <summary>
        /// Initializes the log monitoring process and begins watching the specified log file for changes.
        /// </summary>
        /// <remarks>If the specified log file does not exist or the path is invalid, the monitoring
        /// process will not start. This method loads existing log entries and applies any active filters before
        /// monitoring begins.</remarks>
        /// <param name="logFilePath">The path to the log file to monitor. If null or whitespace, the default log file path is used.</param>
        public void Start(string? logFilePath = null)
        {
            _logFilePath = string.IsNullOrWhiteSpace(logFilePath) ? GlobalData.GetLogFilePath() : logFilePath;
            InitializeLayoutParser();

            if (string.IsNullOrWhiteSpace(_logFilePath) || !File.Exists(_logFilePath))
            {
                Stop();
                return;
            }

            LoadExistingEntries();
            StartWatcher();
            ApplyFilter();
        }

        /// <summary>
        /// Stops monitoring the file system for changes and releases all resources used by the file watcher.
        /// </summary>
        /// <remarks>After calling this method, the file watcher will no longer raise events for file
        /// system changes. This method is safe to call multiple times; subsequent calls have no effect if monitoring
        /// has already been stopped.</remarks>
        public void Stop()
        {
            if (_fileWatcher == null) return;

            _fileWatcher.EnableRaisingEvents = false;
            _fileWatcher.Changed -= FileWatcher_Changed;
            _fileWatcher.Created -= FileWatcher_Created;
            _fileWatcher.Deleted -= FileWatcher_Deleted;
            _fileWatcher.Renamed -= FileWatcher_Renamed;
            _fileWatcher.Dispose();
            _fileWatcher = null;
        }

        private void StartWatcher()
        {
            if (string.IsNullOrWhiteSpace(_logFilePath)) return;

            Stop();

            var directoryPath = Path.GetDirectoryName(_logFilePath);
            var fileName = Path.GetFileName(_logFilePath);

            if (string.IsNullOrWhiteSpace(directoryPath) || string.IsNullOrWhiteSpace(fileName)) return;

            _fileWatcher = new FileSystemWatcher(directoryPath, fileName)
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName,
                IncludeSubdirectories = false,
                EnableRaisingEvents = true
            };

            _fileWatcher.Changed += FileWatcher_Changed;
            _fileWatcher.Created += FileWatcher_Created;
            _fileWatcher.Deleted += FileWatcher_Deleted;
            _fileWatcher.Renamed += FileWatcher_Renamed;
        }

        private void LoadExistingEntries()
        {
            if (string.IsNullOrWhiteSpace(_logFilePath) || !File.Exists(_logFilePath)) return;

            lock (_syncRoot)
            {
                _entries.Clear();
            }

            using var stream = new FileStream(_logFilePath, FileMode.Open, FileAccess.Read,
                FileShare.ReadWrite | FileShare.Delete);
            using var reader = new StreamReader(stream);

            var i = 0;
            while (reader.ReadLine() is { } line)
            {
                i++;
                if (i < 17) //skip initial log lines that print system info
                    continue;

                TryAddEntry(line);
            }

            lock (_syncRoot)
            {
                _lastReadPosition = stream.Position;
            }
        }

        private void ReadNewEntries()
        {
            if (string.IsNullOrWhiteSpace(_logFilePath) || !File.Exists(_logFilePath)) return;

            using var stream = new FileStream(_logFilePath, FileMode.Open, FileAccess.Read,
                FileShare.ReadWrite | FileShare.Delete);

            lock (_syncRoot)
            {
                if (stream.Length < _lastReadPosition)
                {
                    _entries.Clear();
                    _lastReadPosition = 0;
                }

                stream.Seek(_lastReadPosition, SeekOrigin.Begin);
            }

            using var reader = new StreamReader(stream);

            while (reader.ReadLine() is { } line)
            {
                TryAddEntry(line);
            }

            lock (_syncRoot)
            {
                _lastReadPosition = stream.Position;
            }

            this.SafeBeginInvoke(ApplyFilter);
        }

        private void TryAddEntry(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return;

            var entry = ParseLogEntry(line);

            lock (_syncRoot)
            {
                _entries.Add(entry);
            }
        }

        private void ApplyFilter()
        {
            var searchTerm = txtSearch.Text;

            List<LogEntry> snapshot;
            lock (_syncRoot)
            {
                snapshot = [.. _entries];
            }

            var filteredEntries = string.IsNullOrWhiteSpace(searchTerm)
                ? snapshot
                : snapshot.Where(entry =>
                    entry.Raw.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    entry.Message.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

            dgvLogs.SuspendLayout();
            dgvLogs.Rows.Clear();

            foreach (var entry in filteredEntries)
            {
                var rowIndex = dgvLogs.Rows.Add(
                    entry.Timestamp?.ToString("yyyy-MM-dd HH:mm:ss.ffff") ?? string.Empty,
                    entry.Level,
                    entry.Message);

                ApplyLevelStyle(dgvLogs.Rows[rowIndex], entry.Level);
            }

            ScrollToBottom();

            dgvLogs.ResumeLayout();
        }

        private void ApplyLevelStyle(DataGridViewRow row, string level)
        {
            var normalizedLevel = level.Trim().ToUpperInvariant();

            switch (normalizedLevel)
            {
                case "WARN":
                case "WARNING":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 249, 196);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(120, 77, 0);
                    break;
                case "ERROR":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 238);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(183, 28, 28);
                    break;
                case "FATAL":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(183, 28, 28);
                    row.DefaultCellStyle.ForeColor = Color.White;
                    break;
            }
        }

        private void ScrollToBottom()
        {
            if (dgvLogs.Rows.Count == 0) return;

            var lastIndex = dgvLogs.Rows.Count - 1;
            dgvLogs.ClearSelection();
            dgvLogs.Rows[lastIndex].Selected = true;
            dgvLogs.CurrentCell = dgvLogs.Rows[lastIndex].Cells[colMessage.Index];
            dgvLogs.FirstDisplayedScrollingRowIndex = lastIndex;
        }

        private void InitializeLayoutParser()
        {
            _logLayout = ResolveLogLayout();

            var matches = Regex.Matches(_logLayout, @"\$\{(?<name>[a-zA-Z]+)(?::[^}]*)?\}");
            if (matches.Count == 0)
            {
                _layoutTokens = [];
                _layoutSeparators = [];
                _timestampTokenIndex = -1;
                _levelTokenIndex = -1;
                _messageTokenIndex = -1;
                return;
            }

            _layoutTokens = new string[matches.Count];
            _layoutSeparators = new string[matches.Count + 1];

            var cursor = 0;
            for (var i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                _layoutSeparators[i] = _logLayout.Substring(cursor, match.Index - cursor);
                _layoutTokens[i] = match.Groups["name"].Value.ToLowerInvariant();
                cursor = match.Index + match.Length;
            }

            _layoutSeparators[^1] = _logLayout[cursor..];

            _timestampTokenIndex = Array.FindIndex(_layoutTokens,
                token => token.Contains("date", StringComparison.OrdinalIgnoreCase) ||
                         token.Contains("time", StringComparison.OrdinalIgnoreCase));
            _levelTokenIndex = Array.FindIndex(_layoutTokens, token => token.Equals("level", StringComparison.Ordinal));
            _messageTokenIndex = Array.FindIndex(_layoutTokens,
                token => token.Equals("message", StringComparison.Ordinal));
        }

        private string ResolveLogLayout()
        {
            var configuredLayout = GlobalData.ConfigManager.Get("logLayout") as string;
            return string.IsNullOrWhiteSpace(configuredLayout)
                ? "${longdate}|${level:uppercase=true}|${logger}|${message:withexception=true}"
                : configuredLayout;
        }

        private LogEntry ParseLogEntry(string line)
        {
            if (TryParseWithConfiguredLayout(line, out var tokenValues))
            {
                var timestampText = _timestampTokenIndex >= 0 ? tokenValues[_timestampTokenIndex] : string.Empty;
                var level = _levelTokenIndex >= 0 ? tokenValues[_levelTokenIndex] : string.Empty;
                var message = _messageTokenIndex >= 0 ? tokenValues[_messageTokenIndex] : line;
                DateTime.TryParse(timestampText, out var timestamp);

                return new LogEntry(_timestampTokenIndex >= 0 ? timestamp : null, level, message, line);
            }

            var fallbackParts = line.Split('|', 4);
            if (fallbackParts.Length == 4)
            {
                DateTime.TryParse(fallbackParts[0], out var fallbackTimestamp);
                return new LogEntry(fallbackTimestamp, fallbackParts[1], fallbackParts[3], line);
            }

            return new LogEntry(null, string.Empty, line, line);
        }

        private bool TryParseWithConfiguredLayout(string line, out string[] tokenValues)
        {
            tokenValues = [];

            if (_layoutTokens.Length == 0 || _layoutSeparators.Length != _layoutTokens.Length + 1) return false;

            var position = 0;
            var prefix = _layoutSeparators[0];
            if (!string.IsNullOrEmpty(prefix))
            {
                if (!line.StartsWith(prefix, StringComparison.Ordinal)) return false;
                position += prefix.Length;
            }

            tokenValues = new string[_layoutTokens.Length];

            for (var i = 0; i < _layoutTokens.Length; i++)
            {
                var nextSeparator = _layoutSeparators[i + 1];

                if (i == _layoutTokens.Length - 1)
                {
                    if (string.IsNullOrEmpty(nextSeparator))
                    {
                        tokenValues[i] = line[position..];
                        position = line.Length;
                        continue;
                    }

                    var separatorIndex = line.LastIndexOf(nextSeparator, StringComparison.Ordinal);
                    if (separatorIndex < position) return false;

                    tokenValues[i] = line.Substring(position, separatorIndex - position);
                    position = separatorIndex + nextSeparator.Length;
                    continue;
                }

                if (string.IsNullOrEmpty(nextSeparator)) return false;

                var nextIndex = line.IndexOf(nextSeparator, position, StringComparison.Ordinal);
                if (nextIndex < 0) return false;

                tokenValues[i] = line.Substring(position, nextIndex - position);
                position = nextIndex + nextSeparator.Length;
            }

            return position == line.Length;
        }

        private void FileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            ReadNewEntries();
        }

        private void FileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            lock (_syncRoot)
            {
                _lastReadPosition = 0;
            }
            ReadNewEntries();
        }

        private void FileWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            lock (_syncRoot)
            {
                _entries.Clear();
                _lastReadPosition = 0;
            }
            this.SafeBeginInvoke(ApplyFilter);
        }

        private void FileWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            lock (_syncRoot)
            {
                _lastReadPosition = 0;
            }
            ReadNewEntries();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void LiveLogViewer_Disposed(object? sender, EventArgs e)
        {
            Stop();
        }

        private void dgvLogs_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var hitTest = dgvLogs.HitTest(e.X, e.Y);
            if (hitTest.RowIndex < 0) return;

            dgvLogs.ClearSelection();
            dgvLogs.Rows[hitTest.RowIndex].Selected = true;
            cmsLineRightClick.Show(dgvLogs, e.Location);
        }

        private void copyLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedLine = dgvLogs.SelectedRows[0];
                var timestamp = selectedLine.Cells[colTimestamp.Index].Value?.ToString() ?? string.Empty;
                var level = selectedLine.Cells[colLevel.Index].Value?.ToString() ?? string.Empty;
                var message = selectedLine.Cells[colMessage.Index].Value?.ToString() ?? string.Empty;

                var fullText = $"{timestamp}|{level}|{message}";
                Clipboard.SetText(fullText);

                ToastNotification.Show(this, string.Format(Strings.Toast_LogLineCopied, fullText), ToastType.Success, 2000);
            } catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<LiveLogViewer>().Error(ex, "Failed to copy log line to clipboard");
                ToastNotification.Show(this, string.Format(Strings.Toast_FailedToCopyLogLine, ex.Message), ToastType.Error, 3000);
            }
        }

        private sealed record LogEntry(DateTime? Timestamp, string Level, string Message, string Raw);
    }
}
