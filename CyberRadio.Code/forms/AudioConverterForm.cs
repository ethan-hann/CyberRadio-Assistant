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

        private readonly List<string> _inputFiles;

        private bool _isConverting;

        private int _totalToConvert;
        private int _conversionCounter = 0;
        private List<ListViewItem> _checkedItems = [];

        private readonly TrackableObject<Station>? _station;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioConverterForm"/> class.
        /// </summary>
        /// <param name="inputFiles">The list of initial files for conversion.</param>
        /// <param name="station">The station to associate with converted files, if any.</param>
        public AudioConverterForm(List<string> inputFiles, TrackableObject<Station>? station)
        {
            InitializeComponent();

            _inputFiles = inputFiles;
            _station = station;
        }

        private void AudioConverterForm_Load(object sender, EventArgs e)
        {
            Translate();
            SetupListView();

            _checkedItems = lvFiles.Items.Cast<ListViewItem>()
                .Where(item => item.Checked)
                .ToList();

            _totalToConvert = _checkedItems.Count;

            lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, 0, _totalToConvert);

            if (_totalToConvert == 0)
            {
                btnStartConversion.Enabled = false;
                btnCheckAll.Enabled = false;
                btnUncheckAll.Enabled = false;
            }
            else
            {
                btnStartConversion.Enabled = true;
                btnCheckAll.Enabled = true;
                btnUncheckAll.Enabled = true;
            }

            //Set up event listeners
            AudioConverter.Instance.ConversionStarted += OnConversionStarted;
            AudioConverter.Instance.ConversionProgress += OnConversionProgress;
            AudioConverter.Instance.ConversionCompleted += OnConversionCompleted;
        }

        ~AudioConverterForm()
        {
            // Unsubscribe from events to prevent memory leaks
            AudioConverter.Instance.ConversionStarted -= OnConversionStarted;
            AudioConverter.Instance.ConversionProgress -= OnConversionProgress;
            AudioConverter.Instance.ConversionCompleted -= OnConversionCompleted;
            Dispose();
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
            grpConversionLog.Text = Strings.ConversionLog;
            lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, 0, 0);

            changeOutputToolStripMenuItem.Text = Strings.ChangeOutputDirectory;
            fdlgChangeOutput.Description = Strings.ChangeOutputDirectoryDescription;
        }

        private void SetupListView()
        {
            lvFiles.BeginUpdate();
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
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<AudioConverterForm>("AddFileToListView")
                    .Error(ex, $"Failed to add file to list view: {inputFile}");
            }
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvFiles.Items)
                item.Checked = true;

            btnStartConversion.Enabled = true;
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvFiles.Items)
                item.Checked = false;

            btnStartConversion.Enabled = false;
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            if (fdlgOpenSongs.ShowDialog() != DialogResult.OK) return;

            foreach (var file in fdlgOpenSongs.FileNames)
            {
                //If the file is already in the list, skip it
                if (lvFiles.Items.Cast<ListViewItem>().Any(item => item?.Tag?.ToString() == file)) continue;

                AddFileToListView(file);
                _totalToConvert++;
            }

            lvFiles.ResizeColumns();

            lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
            btnStartConversion.Enabled = _totalToConvert > 0;
            btnCheckAll.Enabled = _totalToConvert > 0;
            btnUncheckAll.Enabled = _totalToConvert > 0;
        }

        private async void btnStartConversion_Click(object sender, EventArgs e)
        {
            // Get the updated list of checked items
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

            // Disable the button to prevent multiple clicks
            btnStartConversion.Enabled = false;
            btnCheckAll.Enabled = false;
            btnUncheckAll.Enabled = false;
            btnAddFiles.Enabled = false;
            btnStartConversion.Text = Strings.Converting;

            try
            {
                //Start conversion
                foreach (var item in _checkedItems)
                {
                    if (item.Tag is not string inputFile) continue;
                    if (!AudioConverter.Instance.NeedsConversion(inputFile))
                    {
                        AddLogLine($"{inputFile} => {Strings.NoConversionNeeded}");
                        _totalToConvert--;
                        continue;
                    }

                    var outputPath = Path.GetDirectoryName(item.SubItems[1].Text);
                    await AudioConverter.Instance.ConvertToMp3Async(inputFile, outputPath);
                }

                // If nothing to convert, update UI
                if (_totalToConvert <= 0)
                {
                    btnStartConversion.Enabled = true;
                    btnCheckAll.Enabled = true;
                    btnUncheckAll.Enabled = true;
                    btnAddFiles.Enabled = true;
                    btnStartConversion.Text = Strings.StartConversion;
                    lblStatus.Text = Strings.Ready;
                    lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, 0, 0);
                }
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<AudioConverterForm>("btnStartConversion_Click")
                    .Error(ex, "An error occurred during conversion.");
            }
        }

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
                Close();
            else
            {
                // Reset the UI for the next conversion
                btnStartConversion.Enabled = true;
                btnCheckAll.Enabled = true;
                btnUncheckAll.Enabled = true;
                btnAddFiles.Enabled = true;
                btnStartConversion.Text = Strings.StartConversion;
                lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, 0, 0);
                lblStatus.Text = Strings.Ready;
                _inputFiles.Clear();
                SetupListView();
            }
        }

        private void OnConversionCompleted(object? sender, (string file, bool success, string messageOrOutputPath) e)
        {
            var text = e.success ? $"{e.file} => {e.messageOrOutputPath}" :
                $"{Strings.Error}: {e.messageOrOutputPath}";
            if (InvokeRequired)
            {
                Invoke(() =>
                {
                    AddLogLine(text);
                    progressBar.Value = 0;
                    _conversionCounter++;
                    lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);

                    if (_conversionCounter < _totalToConvert) return;

                    btnStartConversion.Enabled = true;
                    btnCheckAll.Enabled = true;
                    btnUncheckAll.Enabled = true;
                    btnAddFiles.Enabled = true;
                    btnStartConversion.Text = Strings.StartConversion;
                    lblStatus.Text = Strings.Ready;
                    InvokeConversionComplete();
                });
            }
            else
            {
                AddLogLine(text);
                progressBar.Value = 0;
                _conversionCounter++;
                lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);

                if (_conversionCounter < _totalToConvert) return;

                btnStartConversion.Enabled = true;
                btnCheckAll.Enabled = true;
                btnUncheckAll.Enabled = true;
                btnAddFiles.Enabled = true;
                btnStartConversion.Text = Strings.StartConversion;
                lblStatus.Text = Strings.Ready;
                InvokeConversionComplete();
            }
            _isConverting = false;
        }

        private void OnConversionProgress(object? sender, (string file, int percent) e)
        {
            if (InvokeRequired)
            {
                Invoke(() =>
                {
                    var text = $"{e.file} => {e.percent}%";
                    AddLogLine(text);
                    progressBar.Value = e.percent;
                    lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
                });
            }
            else
            {
                var text = $"{e.file} => {e.percent}%";
                AddLogLine(text);
                progressBar.Value = e.percent;
                lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
            }
        }

        private void OnConversionStarted(object? sender, string e)
        {
            var text = $"{Strings.Converting}: {e}";
            if (InvokeRequired)
            {
                Invoke(() =>
                {
                    lblStatus.Text = text;
                    AddLogLine(text);
                    progressBar.Value = 0;
                    lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
                });
            }
            else
            {
                lblStatus.Text = text;
                AddLogLine(text);
                progressBar.Value = 0;
                lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
            }

            _isConverting = true;
        }

        private void lvFiles_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                btnStartConversion.Enabled = true;
                _totalToConvert++;
            }
            else
            {
                var allUnchecked = lvFiles.Items.Cast<ListViewItem>().All(item => !item.Checked);
                btnStartConversion.Enabled = !allUnchecked;

                _totalToConvert = allUnchecked ? 0 : _totalToConvert - 1;
            }

            lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
        }

        private void AddLogLine(string text)
        {
            if (InvokeRequired)
                Invoke(new Action<string>(AddLogLine), text);
            else
            {
                rtbConversionLog.AppendText(text);
                rtbConversionLog.AppendText(Environment.NewLine);
                rtbConversionLog.SelectionStart = rtbConversionLog.Text.Length;
                rtbConversionLog.ScrollToCaret();
                rtbConversionLog.Refresh();
            }
        }

        private void AudioConverterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isConverting) return;

            MessageBox.Show(Strings.ConversionOngoing, Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
        }

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

        private void lvFiles_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            if (_station != null) return; // Disable context menu for station-specific conversions.

            if (_totalToConvert <= 0) return; // Disable if no items in the list view.

            if (lvFiles.SelectedItems.Count <= 0) return; // Disable if nothing is selected.

            cmsFiles.Show(Cursor.Position);
        }
    }
}
