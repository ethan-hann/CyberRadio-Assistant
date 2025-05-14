using AetherUtils.Core.Extensions;
using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms
{
    /// <summary>
    /// Represents the form used for audio conversion.
    /// </summary>
    public partial class AudioConverterForm : Form
    {
        /// <summary>
        /// Event that is raised when the conversion process is completed. Event data is a list of converted file paths.
        /// </summary>
        public event EventHandler<List<string>>? ConversionCompleted;

        /// <summary>
        /// The list of input file paths to be converted.
        /// </summary>
        private readonly List<string> _inputFiles;

        /// <summary>
        /// Indicates whether a conversion is currently in progress.
        /// </summary>
        private bool _isConverting;

        /// <summary>
        /// The total number of files to convert.
        /// </summary>
        private int _totalToConvert;

        /// <summary>
        /// The number of files that have been converted so far.
        /// </summary>
        private int _conversionCounter;

        /// <summary>
        /// The list of checked items in the ListView representing files selected for conversion.
        /// </summary>
        private List<ListViewItem> _checkedItems = [];

        /// <summary>
        /// The radio station context for the conversion, if any.
        /// </summary>
        private readonly TrackableObject<Station>? _station;

        private CancellationTokenSource? _cts;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioConverterForm"/> class.
        /// </summary>
        /// <param name="inputFiles">A list of input file paths to be converted.</param>
        /// <param name="station">
        /// An optional <see cref="TrackableObject{Station}"/> representing the radio station context for the conversion.
        /// If <c>null</c>, the conversion is not associated with a specific station.
        /// </param>
        public AudioConverterForm(List<string> inputFiles, TrackableObject<Station>? station)
        {
            InitializeComponent();

            _inputFiles = inputFiles;
            _station = station;
            _isConverting = false;
            _totalToConvert = 0;
            _conversionCounter = 0;
        }

        /// <summary>
        /// Handles the form load event. Initializes UI, sets up the ListView, and subscribes to conversion events.
        /// </summary>
        private void AudioConverterForm_Load(object sender, EventArgs e)
        {
            SetupListView();

            _checkedItems = lvFiles.Items.Cast<ListViewItem>()
                .Where(item => item.Checked)
                .ToList();
            _totalToConvert = _checkedItems.Count;
            SetButtonsEnabledState();

            // Set up event listeners
            AudioConverter.Instance.ConversionStarted += OnConversionStarted;
            AudioConverter.Instance.ConversionProgress += OnConversionProgress;
            AudioConverter.Instance.ConversionCompleted += OnConversionCompleted;

            Translate();
        }

        /// <summary>
        /// Translates the UI elements to the current language.
        /// </summary>
        private void Translate()
        {
            Text = _station == null ? Strings.AudioConverterTitle :
                $"{Strings.AudioConverterTitle} - {_station.TrackedObject.MetaData.DisplayName}";
            lblStatus.Text = Strings.Ready;
            fdlgOpenSongs.Title = Strings.AddSongsFileBrowserTitle;
            fdlgOpenSongs.Filter = @"Audio/Video Files|*.mp3;*.wav;*.ogg;*.flac;*.mp2;*.wax;*.wma;*.aac;*.m4a;*.aiff;*.alac;*.opus;*.amr;*.ac3;*.mp4;*.m4v;*.mov;*.avi;*.wmv;*.flv;*.mkv;*.webm;*.mpeg;*.mpg;*.3gp;*.3g2;*.ts;*.mts;*.m2ts";
            btnCheckAll.Text = Strings.CheckAll;
            btnUncheckAll.Text = Strings.UncheckAll;
            btnAddFiles.Text = Strings.AddFiles;
            btnStartConversion.Text = Strings.StartConversion;
            btnCancel.Text = Strings.Cancel;
            grpConversionLog.Text = Strings.ConversionLog;
            lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
            changeOutputToolStripMenuItem.Text = Strings.ChangeOutputDirectory;
            fdlgChangeOutput.Description = Strings.ChangeOutputDirectoryDescription;

            //Listview
            lvFiles.Columns[0].Text = Strings.InputPathsColumn;
            lvFiles.Columns[1].Text = Strings.OutputPathsColumn;
        }

        /// <summary>
        /// Sets up the ListView with columns and populates it with the input files.
        /// </summary>
        private void SetupListView()
        {
            lvFiles.BeginUpdate();
            lvFiles.Columns.Clear();
            lvFiles.Columns.Add(Strings.InputPathsColumn, 120, HorizontalAlignment.Left);
            lvFiles.Columns.Add(Strings.OutputPathsColumn, 120, HorizontalAlignment.Left);
            lvFiles.Items.Clear();

            foreach (var inputFile in _inputFiles)
            {
                AddFileToListView(inputFile);
            }
            lvFiles.ResizeColumns();
            lvFiles.EndUpdate();
        }

        /// <summary>
        /// Adds a file to the ListView with its corresponding output path.
        /// </summary>
        /// <param name="inputFile">The input file path to add.</param>
        private void AddFileToListView(string inputFile)
        {
            try
            {
                var item = new ListViewItem(inputFile)
                {
                    Checked = true,
                    Tag = inputFile
                };

                string outputPath;
                var defaultMusicPath = GlobalData.ConfigManager.Get("defaultSongLocation") as string ?? string.Empty;
                if (_station != null)
                {
                    var stationName = _station.TrackedObject.MetaData.DisplayName;
                    outputPath = Path.Combine(defaultMusicPath, stationName, Path.GetFileNameWithoutExtension(inputFile) + ".mp3");
                }
                else
                {
                    outputPath = Path.Combine(defaultMusicPath, "converted", Path.GetFileNameWithoutExtension(inputFile) + ".mp3");
                }
                item.SubItems.Add(outputPath);
                lvFiles.Items.Add(item);
                _totalToConvert = lvFiles.Items.Count;
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<AudioConverterForm>("AddFileToListView")
                    .Error(ex, $"Failed to add file to list view: {inputFile}");
            }
        }

        /// <summary>
        /// Handles the Check All button click event. Checks all items in the ListView.
        /// </summary>
        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvFiles.Items)
                item.Checked = true;
            SetButtonsEnabledState();
        }

        /// <summary>
        /// Handles the Uncheck All button click event. Unchecks all items in the ListView.
        /// </summary>
        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvFiles.Items)
                item.Checked = false;
            SetButtonsEnabledState();
        }

        /// <summary>
        /// Handles the Add Files button click event. Opens a file dialog to add new files to the ListView.
        /// </summary>
        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            if (fdlgOpenSongs.ShowDialog() != DialogResult.OK) return;
            foreach (var file in fdlgOpenSongs.FileNames)
            {
                if (lvFiles.Items.Cast<ListViewItem>().Any(item => item?.Tag?.ToString() == file))
                    continue;
                AddFileToListView(file);
            }
            lvFiles.ResizeColumns();
            lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
            SetButtonsEnabledState();
        }

        /// <summary>
        /// Handles the Start Conversion button click event. Begins the conversion process for checked files.
        /// </summary>
        private async void btnStartConversion_Click(object sender, EventArgs e)
        {
            try
            {
                _checkedItems = lvFiles.Items.Cast<ListViewItem>()
                    .Where(item => item.Checked)
                    .ToList();

                if (_checkedItems.Count == 0)
                {
                    MessageBox.Show(Strings.NoFilesSelected_Conversion, Strings.Error,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_isConverting)
                {
                    MessageBox.Show(Strings.ConversionOngoing, Strings.Error,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                btnStartConversion.Enabled = false;
                btnCheckAll.Enabled = false;
                btnUncheckAll.Enabled = false;
                btnAddFiles.Enabled = false;
                btnCancel.Enabled = true;
                btnStartConversion.Text = Strings.Converting;

                // reset counters/UI
                _conversionCounter = 0;
                lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);

                _cts?.Dispose();
                _cts = new CancellationTokenSource();

                try
                {
                    foreach (var item in _checkedItems)
                    {
                        // respect the “stop now” request
                        if (_cts.IsCancellationRequested)
                            break;

                        if (item.Tag is not string inputFile)
                            continue;

                        if (!AudioConverter.NeedsConversion(inputFile))
                        {
                            AddLogLine($"{inputFile} => {Strings.NoConversionNeeded}");
                            _totalToConvert--;
                            continue;
                        }

                        var outputPath = Path.GetDirectoryName(item.SubItems[1].Text);
                        await AudioConverter.Instance.ConvertToMp3Async(inputFile, outputPath, _cts.Token);
                    }

                    if (_totalToConvert <= 0)
                    {
                        RunOnUI(() =>
                        {
                            SetButtonsEnabledState();
                            btnStartConversion.Text = Strings.StartConversion;
                            lblStatus.Text = Strings.Ready;
                            lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, 0, 0);
                        });
                    }
                }
                catch (Exception ex)
                {
                    AuLogger.GetCurrentLogger<AudioConverterForm>("btnStartConversion_Click")
                        .Error(ex, "An error occurred during conversion.");
                }
                finally
                {
                    RestoreUI();
                    InvokeConversionComplete();
                }
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<AudioConverterForm>("btnStartConversion_Click")
                    .Error(ex, "An error occurred during conversion.");
            }
        }

        private void RestoreUI()
        {
            btnCancel.Enabled = false;
            SetButtonsEnabledState();
            btnStartConversion.Text = Strings.StartConversion;
            lblStatus.Text = Strings.Ready;
            lblTotalConversions.Text =
                string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
        }

        /// <summary>
        /// Invokes the <see cref="ConversionCompleted"/> event and resets the form state after conversion.
        /// </summary>
        private void InvokeConversionComplete()
        {
            var convertedFiles = lvFiles.Items.Cast<ListViewItem>()
                .Where(item => item.Checked)
                .Select(item => item.SubItems[1].Text)
                .ToList();

            ConversionCompleted?.Invoke(this, convertedFiles);

            _conversionCounter = 0;
            _totalToConvert = 0;
            _isConverting = false;

            if (_station != null)
            {
                Close();
            }

            RunOnUI(() =>
            {
                SetButtonsEnabledState();
                btnStartConversion.Text = Strings.StartConversion;
                lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
                lblStatus.Text = Strings.Ready;
                _inputFiles.Clear();
                SetupListView();
            });
        }

        /// <summary>
        /// Handles the ConversionCompleted event from the AudioConverter. Updates UI and logs the result.
        /// </summary>
        private void OnConversionCompleted(object? sender, (string file, bool success, string messageOrOutputPath) e)
        {
            RunOnUI(ProcessCompletion);
            _isConverting = false;
            return;

            void ProcessCompletion()
            {
                var text = e.success ? $"{e.file} => {e.messageOrOutputPath}" :
                    $"{Strings.Error}: {e.messageOrOutputPath}";
                AddLogLine(text);
                progressBar.Value = 0;
                _conversionCounter++;
                lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
                if (_conversionCounter < _totalToConvert) return;

                SetButtonsEnabledState();
                btnStartConversion.Text = Strings.StartConversion;
                lblStatus.Text = Strings.Ready;
                InvokeConversionComplete();
            }
        }

        /// <summary>
        /// Handles the ConversionProgress event from the AudioConverter. Updates the progress bar and log.
        /// </summary>
        private void OnConversionProgress(object? sender, (string file, int percent) e)
        {
            RunOnUI(() =>
            {
                var text = $"{e.file} => {e.percent}%";
                AddLogLine(text);
                progressBar.Value = e.percent;
                lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
            });
        }

        /// <summary>
        /// Handles the ConversionStarted event from the AudioConverter. Updates the status label and log.
        /// </summary>
        private void OnConversionStarted(object? sender, string e)
        {
            RunOnUI(() =>
            {
                var text = $"{Strings.Converting}: {e}";
                lblStatus.Text = text;
                AddLogLine(text);
                progressBar.Value = 0;
                lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
            });
            _isConverting = true;
        }

        /// <summary>
        /// Handles the ItemChecked event for the ListView. Updates the enabled state of the Start Conversion button and conversion counters.
        /// </summary>
        private void lvFiles_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            _totalToConvert = lvFiles.Items.Cast<ListViewItem>().Count(i => i.Checked);
            lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);

            SetButtonsEnabledState();
        }

        /// <summary>
        /// Adds a line of text to the conversion log RichTextBox.
        /// </summary>
        /// <param name="text">The text to add to the log.</param>
        private void AddLogLine(string text)
        {
            RunOnUI(() =>
            {
                rtbConversionLog.AppendText(text + Environment.NewLine);
                rtbConversionLog.SelectionStart = rtbConversionLog.Text.Length;
                rtbConversionLog.ScrollToCaret();
                rtbConversionLog.Refresh();
            });
        }

        /// <summary>
        /// Handles the FormClosing event. Prevents closing the form if a conversion is in progress.
        /// </summary>
        private void AudioConverterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isConverting) return;

            MessageBox.Show(Strings.ConversionOngoing, Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
        }

        /// <summary>
        /// Handles the Change Output Directory menu item click event. Allows changing the output directory for selected files.
        /// </summary>
        private void changeOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fdlgChangeOutput.ShowDialog() != DialogResult.OK) return;
            foreach (var item in lvFiles.SelectedItems)
            {
                if (item is not ListViewItem listViewItem) continue;
                var inputFile = listViewItem.Tag?.ToString();
                if (string.IsNullOrEmpty(inputFile)) continue;
                var outputPath = Path.Combine(fdlgChangeOutput.SelectedPath, Path.GetFileNameWithoutExtension(inputFile) + ".mp3");
                listViewItem.SubItems[1].Text = outputPath;
            }
        }

        /// <summary>
        /// Handles the MouseDown event for the ListView. Shows the context menu if appropriate.
        /// </summary>
        private void lvFiles_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            if (_station != null) return;
            if (_totalToConvert <= 0) return;
            if (lvFiles.SelectedItems.Count <= 0) return;
            cmsFiles.Show(Cursor.Position);
        }

        /// <summary>
        /// Sets the enabled state of the main action buttons based on state of the current action.
        /// </summary>
        private void SetButtonsEnabledState()
        {
            btnCancel.Enabled = _isConverting;

            btnCheckAll.Enabled = !_isConverting & lvFiles.Items.Count > 0;
            btnUncheckAll.Enabled = btnCheckAll.Enabled;
            btnAddFiles.Enabled = !_isConverting;
            btnStartConversion.Enabled = !_isConverting & lvFiles.Items.Count > 0 & _totalToConvert > 0;
            btnCancel.Enabled = _isConverting;
        }

        /// <summary>
        /// Runs the specified action on the UI thread.
        /// </summary>
        /// <param name="action">The action to run on the UI thread.</param>
        private void RunOnUI(Action action)
        {
            try
            {
                if (InvokeRequired)
                    Invoke(action);
                else
                    action();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
            btnCancel.Enabled = false;
        }
    }
}
