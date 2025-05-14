using System.ComponentModel;
using AetherUtils.Core.Extensions;
using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
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
        /// The list of conversion candidates. Each candidate represents a file to be converted and its target format.
        /// </summary>
        private readonly BindingList<ConvertCandidate> _candidates = [];

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
        /// The list of checked items in the listbox that are to be converted.
        /// </summary>
        private List<ConvertCandidate> _checkedItems = [];

        /// <summary>
        /// The radio station context for the conversion, if any.
        /// </summary>
        private readonly TrackableObject<Station>? _station;

        private readonly string _defaultMusicPath = GlobalData.ConfigManager.Get("defaultSongLocation") as string ?? string.Empty;

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
            SetupListBox();
            SetupPropertyGrid();

            _checkedItems = lbCandidates.CheckedItems.Cast<ConvertCandidate>().ToList();
            _totalToConvert = _checkedItems.Count;

            // Set up event listeners
            AudioConverter.Instance.ConversionStarted += OnConversionStarted;
            AudioConverter.Instance.ConversionProgress += OnConversionProgress;
            AudioConverter.Instance.ConversionCompleted += OnConversionCompleted;

            Translate();

            SetUiEnabledStates();
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
            //changeOutputToolStripMenuItem.Text = Strings.ChangeOutputDirectory;
            fdlgChangeOutput.Description = Strings.ChangeOutputDirectoryDescription;

            ////Listview
            //lvFiles.Columns[0].Text = Strings.InputPathsColumn;
            //lvFiles.Columns[1].Text = Strings.OutputPathsColumn;
        }

        /// <summary>
        /// Sets up the ListView with columns and populates it with the input files.
        /// </summary>
        private void SetupListBox()
        {
            try
            {
                _candidates.Clear();

                foreach (var inputFile in _inputFiles)
                {
                    AddFileToListBox(inputFile);
                }

                lbCandidates.BeginUpdate();
                // bind the listbox to the candidates
                lbCandidates.DataSource = null;
                lbCandidates.DataSource = _candidates;
                lbCandidates.DisplayMember = "ToString";
                _totalToConvert = _candidates.Count;
                lbCandidates.EndUpdate();

                lbCandidates.SelectedIndex = _totalToConvert > 0 ? 0 : -1;

                CheckAll();
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<AudioConverterForm>("SetupListBox")
                    .Error(ex, "Failed to set up the ListBox.");

            }
        }

        private void SetupPropertyGrid()
        {
            var changeOutputPathBtn = new ToolStripButton(Strings.ChangeOutputDirectory, Resources.folder__16x16);
            pgConvertCandidate.AddButton("change_path", changeOutputPathBtn);
            pgConvertCandidate.DrawButtons();

            changeOutputPathBtn.Click += ChangeOutputPathBtn_Click;
        }

        private void ChangeOutputPathBtn_Click(object? sender, EventArgs e)
        {
            if (fdlgChangeOutput.ShowDialog() != DialogResult.OK) return;
            var outputPath = fdlgChangeOutput.SelectedPath;
            if (string.IsNullOrEmpty(outputPath)) return;

            // Update the path of the selected item
            if (lbCandidates.SelectedItem is not ConvertCandidate selectedItem) return;

            selectedItem.OutputPath = Path.Combine(outputPath, $"{Path.GetFileNameWithoutExtension(selectedItem.InputPath)}{selectedItem.TargetFormat.ToDescriptionString()}");

            //    foreach (var item in lvFiles.SelectedItems)
            //    {
            //        if (item is not ListViewItem listViewItem) continue;
            //        var inputFile = listViewItem.Tag?.ToString();
            //        if (string.IsNullOrEmpty(inputFile)) continue;
            //        var outputPath = Path.Combine(fdlgChangeOutput.SelectedPath, Path.GetFileNameWithoutExtension(inputFile) + ".mp3");
            //        listViewItem.SubItems[1].Text = outputPath;
            //    }
        }

        private void AddFileToListBox(string fileName)
        {
            string outputPath;
            if (_station != null)
            {
                var stationName = _station.TrackedObject.MetaData.DisplayName;
                outputPath = Path.Combine(_defaultMusicPath, stationName);
            }
            else
            {
                outputPath = Path.Combine(_defaultMusicPath, "converted");
            }

            var convertCandidate = new ConvertCandidate(fileName, ValidAudioFiles.Mp3, outputPath);
            _candidates.Add(convertCandidate);

            // Set the last item as checked
            var lastIndex = lbCandidates.Items.Count - 1;
            if (lastIndex >= 0)
                lbCandidates.SetItemChecked(lastIndex, true);

            _totalToConvert = lbCandidates.CheckedItems.Count;
        }

        /// <summary>
        /// Handles the Check All button click event. Checks all items in the ListView.
        /// </summary>
        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            CheckAll();
        }

        private void CheckAll()
        {
            for (var i = 0; i < lbCandidates.Items.Count; i++)
                lbCandidates.SetItemChecked(i, true);
            SetUiEnabledStates();
        }

        /// <summary>
        /// Handles the Uncheck All button click event. Unchecks all items in the ListView.
        /// </summary>
        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            UncheckAll();
        }

        private void UncheckAll()
        {
            for (var i = 0; i < lbCandidates.Items.Count; i++)
                lbCandidates.SetItemChecked(i, false);
            SetUiEnabledStates();
        }

        /// <summary>
        /// Handles the Add Files button click event. Opens a file dialog to add new files to the ListView.
        /// </summary>
        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            if (fdlgOpenSongs.ShowDialog() != DialogResult.OK) return;

            foreach (var file in fdlgOpenSongs.FileNames)
            {
                if (string.IsNullOrEmpty(file))
                    continue;

                // Check if the file is already in the list
                if (lbCandidates.Items.Cast<ConvertCandidate>().Any(item => item.InputPath.Equals(file)))
                    continue;
                AddFileToListBox(file);
            }

            
            SetUiEnabledStates();
        }

        /// <summary>
        /// Handles the Start Conversion button click event. Begins the conversion process for checked files.
        /// </summary>
        private async void btnStartConversion_Click(object sender, EventArgs e)
        {
            try
            {
                _checkedItems = lbCandidates.CheckedItems.Cast<ConvertCandidate>().ToList();

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
                pgConvertCandidate.Enabled = false;
                lbCandidates.Enabled = false;
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

                        if (!AudioConverter.NeedsConversion(item.InputPath))
                        {
                            AddLogLine($"{item.InputPath} => {Strings.NoConversionNeeded}");
                            _totalToConvert--;
                            continue;
                        }

                        RunOnUI(() =>
                        {
                            lbCandidates.SelectedItem = item;
                        });

                        await AudioConverter.Instance.ConvertAsync(item, _cts.Token);
                    }

                    if (_totalToConvert <= 0)
                    {
                        RunOnUI(() =>
                        {
                            SetUiEnabledStates();
                            pgConvertCandidate.Enabled = true;
                            lbCandidates.Enabled = true;
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
            SetUiEnabledStates();
            btnStartConversion.Text = Strings.StartConversion;
            lblStatus.Text = Strings.Ready;
            lblTotalConversions.Text =
                string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
            pgConvertCandidate.Enabled = true;
            lbCandidates.Enabled = true;
        }

        /// <summary>
        /// Invokes the <see cref="ConversionCompleted"/> event and resets the form state after conversion.
        /// </summary>
        private void InvokeConversionComplete()
        {
            var convertedFiles = lbCandidates.CheckedItems.Cast<ConvertCandidate>()
                .Select(item => item.OutputPath)
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
                SetUiEnabledStates();
                pgConvertCandidate.Enabled = true;
                lbCandidates.Enabled = true;
                btnStartConversion.Text = Strings.StartConversion;
                lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
                lblStatus.Text = Strings.Ready;
                _inputFiles.Clear();
                SetupListBox();
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

                SetUiEnabledStates();
                btnStartConversion.Text = Strings.StartConversion;
                pgConvertCandidate.Enabled = true;
                lbCandidates.Enabled = true;
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

        private void lbCandidates_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            _totalToConvert = lbCandidates.CheckedItems.Count + (e.NewValue == CheckState.Checked ? 1 : -1);
            SetUiEnabledStates();
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
        /// Sets the enabled state of the main action buttons based on state of the current action.
        /// </summary>
        private void SetUiEnabledStates()
        {
            btnCancel.Enabled = _isConverting;

            btnCheckAll.Enabled = !_isConverting & lbCandidates.Items.Count > 0;
            btnUncheckAll.Enabled = btnCheckAll.Enabled;
            btnAddFiles.Enabled = !_isConverting;
            btnStartConversion.Enabled = !_isConverting & lbCandidates.Items.Count > 0 & _totalToConvert > 0;
            pgConvertCandidate.Enabled = !_isConverting & lbCandidates.Items.Count > 0 & _totalToConvert > 0;
            lbCandidates.Enabled = !_isConverting;
            btnCancel.Enabled = _isConverting;

            lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
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

        private void lbCandidates_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Show the selected candidate in the property grid
            pgConvertCandidate.SelectedObject = lbCandidates.SelectedItem;
        }
    }
}
