// AudioConverterForm.cs : RadioExt-Helper
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

using System.ComponentModel;
using AetherUtils.Core.Extensions;
using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

/// <summary>
/// Represents the form used for audio conversion.
/// </summary>
public partial class AudioConverterForm : Form
{
    /// <summary>
    /// The list of conversion candidates. Each candidate represents a file to be converted and its target format.
    /// </summary>
    private readonly BindingList<ConvertCandidate> _candidates = [];

    private readonly string _defaultMusicPath = GlobalData.ConfigManager.Get("defaultSongLocation") as string ??
                                                Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

    /// <summary>
    /// The list of input file paths to be converted.
    /// </summary>
    private readonly List<string> _inputFiles;

    /// <summary>
    /// The radio station context for the conversion, if any.
    /// </summary>
    private readonly TrackableObject<AdditionalStation>? _station;

    /// <summary>
    /// The list of checked items in the listbox that are to be converted.
    /// </summary>
    private List<ConvertCandidate> _checkedItems = [];

    /// <summary>
    /// The number of files that have been converted so far.
    /// </summary>
    private int _conversionCounter;

    private CancellationTokenSource? _cts;

    /// <summary>
    /// Indicates the user has cancelled the running conversion.
    /// </summary>
    private bool _isCancelling;

    /// <summary>
    /// Indicates whether a conversion is currently in progress.
    /// </summary>
    private bool _isConverting;

    /// <summary>
    /// The total number of files to convert.
    /// </summary>
    private int _totalToConvert;

    /// <summary>
    /// Initializes a new instance of the <see cref="AudioConverterForm"/> class.
    /// </summary>
    /// <param name="inputFiles">A list of input file paths to be converted.</param>
    /// <param name="station">
    /// An optional <see cref="TrackableObject{Station}"/> representing the radio station context for the conversion.
    /// If <c>null</c>, the conversion is not associated with a specific station.
    /// </param>
    public AudioConverterForm(List<string> inputFiles, TrackableObject<AdditionalStation>? station)
    {
        InitializeComponent();

        _inputFiles = inputFiles;
        _station = station;
        _isConverting = false;
        _totalToConvert = 0;
        _conversionCounter = 0;
    }

    /// <summary>
    /// Event that is raised when the conversion process is completed. Event data is a list of converted file paths.
    /// </summary>
    public event EventHandler<List<string>>? ConversionCompleted;

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
        Text = _station == null
            ? Strings.AudioConverterTitle
            : $"{Strings.AudioConverterTitle} - {_station.TrackedObject.MetaData.DisplayName}";
        lblStatus.Text = Strings.Ready;
        fdlgOpenSongs.Title = Strings.AddSongsFileBrowserTitle;
        fdlgOpenSongs.Filter =
            @"Audio/Video Files|*.mp3;*.wav;*.ogg;*.flac;*.mp2;*.wax;*.wma;*.aac;*.m4a;*.aiff;*.alac;*.opus;*.amr;*.ac3;*.mp4;*.m4v;*.mov;*.avi;*.wmv;*.flv;*.mkv;*.webm;*.mpeg;*.mpg;*.3gp;*.3g2;*.ts;*.mts;*.m2ts";
        btnCheckAll.Text = Strings.CheckAll;
        btnUncheckAll.Text = Strings.UncheckAll;
        btnAddFiles.Text = Strings.AddFiles;
        btnRemoveFiles.Text = Strings.AudioConvert_RemoveSelectedFiles;
        btnStartConversion.Text = Strings.StartConversion;
        btnCancel.Text = Strings.Cancel;
        grpConversionLog.Text = Strings.ConversionLog;
        lblTotalConversions.Text = string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
        fdlgChangeOutput.Description = Strings.ChangeOutputDirectoryDescription;

        // No need to translate the property grid buttons as it's done in SetupPropertyGrid()
    }

    /// <summary>
    /// Sets up the ListView with columns and populates it with the input files.
    /// </summary>
    private void SetupListBox()
    {
        try
        {
            _candidates.Clear();

            foreach (var inputFile in _inputFiles) AddFileToListBox(inputFile);

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
        //Remove the built-in buttons via reflection
        var toolStrip = pgConvertCandidate.Controls.OfType<ToolStrip>().FirstOrDefault();

        if (toolStrip != null)
        {
            toolStrip.Items.Clear();

            //Add custom button
            var changeOutputPathBtn = new ToolStripButton(Strings.ChangeOutputDirectory, Resources.folder__16x16);
            pgConvertCandidate.AddButton("change_path", changeOutputPathBtn);
            changeOutputPathBtn.Click += ChangeOutputPathBtn_Click;

            //If the station is not null, disable button as we don't want to change the output path
            changeOutputPathBtn.Enabled = _station == null;
        }

        pgConvertCandidate.DrawButtons();
    }

    private void ChangeOutputPathBtn_Click(object? sender, EventArgs e)
    {
        if (fdlgChangeOutput.ShowDialog() != DialogResult.OK) return;
        var outputPath = fdlgChangeOutput.SelectedPath;
        if (string.IsNullOrEmpty(outputPath)) return;

        // Update the path of the selected item
        if (lbCandidates.SelectedItem is not ConvertCandidate selectedItem) return;

        selectedItem.OutputPath = Path.Combine(outputPath,
            $"{Path.GetFileNameWithoutExtension(selectedItem.InputPath)}{selectedItem.TargetFormat.ToDescriptionString()}");
        pgConvertCandidate.Invalidate();
        pgConvertCandidate.Update();
    }

    private void AddFileToListBox(string fileName)
    {
        string outputPath;
        if (_station != null)
        {
            // If a station is provided, we use its display name (which should be the folder name as well) in the staging folder.
            var stationName = _station.TrackedObject.MetaData.DisplayName;
            var stagingFolder = GlobalData.ConfigManager.Get("stagingPath") as string ?? string.Empty;
            if (string.IsNullOrEmpty(stagingFolder))
                stagingFolder = _defaultMusicPath;

            outputPath = Path.Combine(stagingFolder, stationName);
        }
        else
        {
            // If no station is provided, we use the default music path.
            outputPath = Path.Combine(_defaultMusicPath, "converted");
        }

        var convertCandidate = new ConvertCandidate(fileName, ValidAudioFiles.Mp3, outputPath);
        _candidates.Add(convertCandidate);

        // Set the last item as checked
        var lastIndex = lbCandidates.Items.Count - 1;
        if (lastIndex >= 0)
            lbCandidates.SetItemChecked(lastIndex, true);

        // Select it in the property grid
        pgConvertCandidate.SelectedObject = _candidates[lastIndex < 0 ? 0 : lastIndex];

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
    /// Handles the Add Files button click event. Opens a file dialog to add new files to the ListBox.
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
    /// Handles the Remove Files button click event. Removes selected files from the ListBox.
    /// </summary>
    private void btnRemoveFiles_Click(object sender, EventArgs e)
    {
        if (lbCandidates.CheckedItems.Count <= 0) return;

        //Make sure we aren't converting
        if (_isConverting)
        {
            MessageBox.Show(Strings.ConversionOngoing, Strings.Error,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Confirm with user
        var result = MessageBox.Show(
            string.Format(Strings.AudioConvert_ConfirmRemoveFiles, lbCandidates.CheckedItems.Count),
            Strings.Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result != DialogResult.Yes) return;

        var selectedItems = lbCandidates.CheckedItems.Cast<ConvertCandidate>().ToList();
        foreach (var item in selectedItems)
            _candidates.Remove(item);

        _totalToConvert = lbCandidates.CheckedItems.Count;
        _checkedItems = lbCandidates.CheckedItems.Cast<ConvertCandidate>().ToList();
        _conversionCounter = 0; // Reset conversion counter when items are removed
        _isConverting = false;

        SetUiEnabledStates();

        if (lbCandidates.Items.Count > 0)
        {
            lbCandidates.SelectedIndex = 0; // Select the first item after removal
            pgConvertCandidate.SelectedObject = lbCandidates.SelectedItem; // Show the first item in the property grid
        }
        else
        {
            pgConvertCandidate.SelectedObject = null; // Clear the property grid if no items left
        }
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

            // Confirm with user that conversion can take a while
            var confirmResult = MessageBox.Show(Strings.AudioConvert_ConfirmStartConversion,
                Strings.Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult != DialogResult.Yes) return;

            btnStartConversion.Enabled = false;
            btnCheckAll.Enabled = false;
            btnUncheckAll.Enabled = false;
            btnRemoveFiles.Enabled = false;
            btnAddFiles.Enabled = false;
            btnCancel.Enabled = true;
            pgConvertCandidate.Enabled = false;
            lbCandidates.Enabled = false;
            btnStartConversion.Text = Strings.Converting;

            // reset counters/UI
            _conversionCounter = 0;
            lblTotalConversions.Text =
                string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);

            _cts?.Dispose();
            _cts = new CancellationTokenSource();

            try
            {
                foreach (var item in _checkedItems)
                {
                    if (_cts.IsCancellationRequested)
                        break;

                    if (!AudioConverter.NeedsConversion(item.InputPath))
                    {
                        AddLogLine($"{item.InputPath} => {Strings.NoConversionNeeded}");
                        _totalToConvert--;
                        continue;
                    }

                    RunOnUI(() => { lbCandidates.SelectedItem = item; });

                    await AudioConverter.Instance.ConvertAsync(item, false, _cts.Token);
                }

                if (_totalToConvert <= 0)
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
            catch (Exception ex)
            {
                MessageBox.Show(Strings.AudioConvert_Error, Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

                AuLogger.GetCurrentLogger<AudioConverterForm>("btnStartConversion_Click")
                    .Error(ex, "An error occurred during conversion.");
            }
            finally
            {
                AuLogger.GetCurrentLogger<AudioConverterForm>("btnStartConversion_Click")
                    .Info("Conversion Log:\n" + rtbConversionLog.Text);

                AuLogger.GetCurrentLogger<AudioConverterForm>("btnStartConversion_Click")
                    .Info($"Conversion process completed. Converted {_conversionCounter} of {_totalToConvert} files.");

                RestoreUi();
                InvokeConversionComplete();
            }
        }
        catch (Exception ex)
        {
            //Show message box
            MessageBox.Show(Strings.AudioConvert_Error, Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

            AuLogger.GetCurrentLogger<AudioConverterForm>("btnStartConversion_Click")
                .Error(ex, "An error occurred during conversion.");
        }
    }

    private void RestoreUi()
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

        // We only want to notify if the conversion was successful and not cancelled.
        if (!_isCancelling)
            ConversionCompleted?.Invoke(this, convertedFiles);

        _conversionCounter = 0;
        _totalToConvert = 0;
        _isConverting = false;
        _isCancelling = false;

        RunOnUI(() =>
        {
            SetUiEnabledStates();
            pgConvertCandidate.Enabled = true;
            lbCandidates.Enabled = true;
            btnStartConversion.Text = Strings.StartConversion;
            lblTotalConversions.Text =
                string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
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
            var text = e.success ? $"{e.file} => {e.messageOrOutputPath}" : $"{Strings.Error}: {e.messageOrOutputPath}";
            AddLogLine(text);
            progressBar.Value = 0;
            _conversionCounter++;
            lblTotalConversions.Text =
                string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
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
            lblTotalConversions.Text =
                string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
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
            lblTotalConversions.Text =
                string.Format(Strings.TotalConversionsLabel, _conversionCounter, _totalToConvert);
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

        MessageBox.Show(Strings.ConversionOngoing, Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        e.Cancel = true;
    }

    /// <summary>
    /// Sets the enabled state of the main action buttons based on state of the current action.
    /// </summary>
    private void SetUiEnabledStates()
    {
        btnCancel.Enabled = _isConverting;

        btnCheckAll.Enabled = !_isConverting & (lbCandidates.Items.Count > 0);
        btnUncheckAll.Enabled = btnCheckAll.Enabled;
        btnAddFiles.Enabled = !_isConverting;
        btnRemoveFiles.Enabled = !_isConverting & (_totalToConvert > 0);
        btnStartConversion.Enabled = !_isConverting & (lbCandidates.Items.Count > 0) & (_totalToConvert > 0);
        pgConvertCandidate.Enabled = !_isConverting & (lbCandidates.Items.Count > 0) & (_totalToConvert > 0);
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
            // ignored to prevent log spam
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        _cts?.Cancel();
        btnCancel.Enabled = false;
        _isCancelling = true;
    }

    private void lbCandidates_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Show the selected candidate in the property grid
        pgConvertCandidate.SelectedObject = lbCandidates.SelectedItem;
    }

    private void pgConvertCandidate_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
        //Ensure the property grid is updated with the selected item when a value is changed
        pgConvertCandidate.SelectedObject = lbCandidates.SelectedItem;
    }

    private void ControlMouseEnter(object sender, EventArgs e)
    {
        if (_isConverting) return;

        this.SafeInvoke(() =>
        {
            if (sender is not Control hoveredControl) return;

            var helpKey = hoveredControl.Tag as string ?? string.Empty;
            if (!string.IsNullOrEmpty(helpKey))
                lblStatus.Text = GlobalData.Strings.GetString(helpKey);
        });
    }

    private void ControlMouseLeave(object sender, EventArgs e)
    {
        if (!_isConverting)
            this.SafeInvoke(() => lblStatus.Text = Strings.Ready);
    }
}