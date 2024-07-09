using System.Globalization;
using AetherUtils.Core.WinForms.Controls;
using AetherUtils.Core.WinForms.CustomArgs;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.user_controls;

public sealed partial class StationEditor : UserControl, IUserControl
{
    public event EventHandler? StationUpdated;

    private readonly ImageList _tabImages = new();

    private readonly ComboBox _cmbUiIcons;
    private readonly CustomMusicCtl _musicCtl;

    public StationEditor(TrackableObject<Station> station)
    {
        InitializeComponent();
        Dock = DockStyle.Fill;

        SetTabImages();

        Station = station;
        _musicCtl = new CustomMusicCtl(Station);
        _musicCtl.StationUpdated += (sender, args) => StationUpdated?.Invoke(this, EventArgs.Empty);

        _cmbUiIcons = GlobalData.CloneTemplateComboBox();
        _cmbUiIcons.SelectedIndexChanged += cmbUIIcons_SelectedIndexChanged;
    }

    public TrackableObject<Station> Station { get; }

    public void Translate()
    {
        tabDisplayAndIcon.Text = GlobalData.Strings.GetString("DisplayAndIcon");
        lblName.Text = GlobalData.Strings.GetString("DisplayName");
        lblIcon.Text = GlobalData.Strings.GetString("Icon");
        lblUsingCustomIcon.Text = GlobalData.Strings.GetString("Using?");
        lblInkPath.Text = GlobalData.Strings.GetString("InkAtlasPath");
        lblInkPart.Text = GlobalData.Strings.GetString("InkAtlasPart");
        lblFM.Text = GlobalData.Strings.GetString("FM");
        lblVolume.Text = GlobalData.Strings.GetString("Volume");
        lblVolumeVal.Text = GlobalData.Strings.GetString("Value");
        radUseCustomYes.Text = GlobalData.Strings.GetString("Yes");
        radUseCustomNo.Text = GlobalData.Strings.GetString("No");
        grpDisplay.Text = GlobalData.Strings.GetString("Display");
        grpCustomIcon.Text = GlobalData.Strings.GetString("CustomIcon");
        grpSettings.Text = GlobalData.Strings.GetString("Settings");

        tabMusic.Text = GlobalData.Strings.GetString("Music");
        lblUseStream.Text = GlobalData.Strings.GetString("UseStream");
        lblStreamURL.Text = GlobalData.Strings.GetString("StreamURL");
        btnGetFromRadioGarden.Text = GlobalData.Strings.GetString("ParseFromRadioGarden");
        grpStreamSettings.Text = GlobalData.Strings.GetString("StreamSettings");
        grpSongs.Text = GlobalData.Strings.GetString("Songs");
        radUseStreamYes.Text = GlobalData.Strings.GetString("Yes");
        radUseStreamNo.Text = GlobalData.Strings.GetString("No");

        lblStatus.Text = GlobalData.Strings.GetString("Ready");

        _musicCtl.Translate();
    }

    public MusicPlayer GetMusicPlayer()
    {
        return mpStreamPlayer;
    }

    private void StationEditor_Load(object sender, EventArgs e)
    {
        SuspendLayout();
        tlpDisplayTable.Controls.Add(_cmbUiIcons, 1, 1);
        grpSongs.Controls.Add(_musicCtl);

        SetDisplayTabValues();
        SetMusicTabValues();
        Translate();

        ResumeLayout();
    }

    private void SetTabImages()
    {
        _tabImages.Images.Add("display", Properties.Resources.display_frame);
        _tabImages.Images.Add("music", Properties.Resources.sound_waves);
        tabControl.ImageList = _tabImages;
        tabDisplayAndIcon.ImageKey = "display";
        tabMusic.ImageKey = "music";
    }

    #region Display and Icon Tab

    private void SetDisplayTabValues()
    {
        txtDisplayName.Text = Station.TrackedObject.MetaData.DisplayName;
        _cmbUiIcons.SelectedIndex = _cmbUiIcons.Items.IndexOf(Station.TrackedObject.MetaData.Icon);

        if (Station.TrackedObject.CustomIcon.UseCustom)
        {
            radUseCustomYes.Checked = true;
            radUseCustomNo.Checked = false;
        }
        else
        {
            radUseCustomNo.Checked = true;
            radUseCustomYes.Checked = false;
            txtInkAtlasPart.Visible = !radUseCustomNo.Checked;
            txtInkAtlasPath.Visible = !radUseCustomNo.Checked;
            lblInkPart.Visible = !radUseCustomNo.Checked;
            lblInkPath.Visible = !radUseCustomNo.Checked;
        }

        txtInkAtlasPath.Text = Station.TrackedObject.CustomIcon.InkAtlasPath;
        txtInkAtlasPart.Text = Station.TrackedObject.CustomIcon.InkAtlasPart;
        nudFM.Value = (decimal)Station.TrackedObject.MetaData.Fm;
        volumeSlider.Value = (int)(Station.TrackedObject.MetaData.Volume / 0.1f);
        lblSelectedVolume.Text = $@"{Station.TrackedObject.MetaData.Volume:F1}";
    }

    private void txtDisplayName_TextChanged(object sender, EventArgs e)
    {
        Station.TrackedObject.MetaData.DisplayName = txtDisplayName.Text;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void txtDisplayName_Leave(object sender, EventArgs e)
    {
        EnsureDisplayNameFormat();
    }

    private void cmbUIIcons_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_cmbUiIcons.SelectedItem is string iconStr)
            Station.TrackedObject.MetaData.Icon = iconStr;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void radUseCustomYes_CheckedChanged(object sender, EventArgs e)
    {
        Station.TrackedObject.CustomIcon.UseCustom = radUseCustomYes.Checked;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void radUseCustomNo_CheckedChanged(object sender, EventArgs e)
    {
        Station.TrackedObject.CustomIcon.UseCustom = !radUseCustomNo.Checked;
        txtInkAtlasPart.Visible = !radUseCustomNo.Checked;
        txtInkAtlasPath.Visible = !radUseCustomNo.Checked;
        lblInkPart.Visible = !radUseCustomNo.Checked;
        lblInkPath.Visible = !radUseCustomNo.Checked;
    }

    private void txtInkAtlasPath_TextChanged(object sender, EventArgs e)
    {
        Station.TrackedObject.CustomIcon.InkAtlasPath = txtInkAtlasPath.Text;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void txtInkAtlasPart_TextChanged(object sender, EventArgs e)
    {
        Station.TrackedObject.CustomIcon.InkAtlasPart = txtInkAtlasPart.Text;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void nudFM_ValueChanged(object sender, EventArgs e)
    {
        Station.TrackedObject.MetaData.Fm = (float)nudFM.Value;
        EnsureDisplayNameFormat();
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void EnsureDisplayNameFormat()
    {
        string fmValue = nudFM.Value.ToString("00.00", CultureInfo.InvariantCulture); // Format to two decimal places
        string currentText = txtDisplayName.Text;

        // Use a regular expression to detect and remove any existing FM value at the start
        var regex = new System.Text.RegularExpressions.Regex(@"^\d+(\.\d+)?\s*");
        var match = regex.Match(currentText);

        if (match.Success)
        {
            // Remove the existing FM value from the start
            currentText = currentText.Substring(match.Length).TrimStart();
        }

        // Combine FM value and station name with the correct format
        txtDisplayName.Text = $"{fmValue} {currentText}";
    }

    private void volumeSlider_Scroll(object sender, EventArgs e)
    {
        Station.TrackedObject.MetaData.Volume = volumeSlider.Value * 0.1f;
        lblSelectedVolume.Text = $@"{Station.TrackedObject.MetaData.Volume:F1}";
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void lblVolumeVal_DoubleClick(object sender, EventArgs e)
    {
        lblSelectedVolume_DoubleClick(sender, e);
    }

    private void lblSelectedVolume_DoubleClick(object sender, EventArgs e)
    {
        txtVolumeEdit.Text = lblSelectedVolume.Text;
        txtVolumeEdit.Size = lblSelectedVolume.Size;
        txtVolumeEdit.Visible = true;
        volumeSlider.Enabled = false;
        txtVolumeEdit.Focus();
        txtVolumeEdit.SelectAll();
    }

    private void txtVolumeEdit_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter) return;

        var maxVol = volumeSlider.Maximum * 0.1f;
        var minVol = volumeSlider.Minimum * 0.1f;

        if (!float.TryParse(txtVolumeEdit.Text, NumberStyles.Float, CultureInfo.InvariantCulture,
                out var newVolume)) return;

        if (newVolume > maxVol || newVolume < minVol)
        {
            e.Handled = true;
        }
        else
        {
            lblSelectedVolume.Text = txtVolumeEdit.Text;
            txtVolumeEdit.Visible = false;
            volumeSlider.Enabled = true;
            volumeSlider.Value = (int)(newVolume / 0.1f);
            volumeSlider_Scroll(sender, e);
            e.Handled = true;
        }
    }

    private void txtVolumeEdit_KeyPress(object sender, KeyPressEventArgs e)
    {
        // Allow control characters, digits, decimal point, and minus sign
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            e.Handled = true;

        // Allow only one decimal point
        if (e.KeyChar == '.' && ((TextBox)sender).Text.IndexOf('.') > -1)
            e.Handled = true;
    }

    #endregion

    #region Music Tab

    private void SetMusicTabValues()
    {
        radUseStreamYes.Checked = Station.TrackedObject.StreamInfo.IsStream;
        radUseStreamNo.Checked = !radUseStreamYes.Checked;

        ToggleStreamControls(Station.TrackedObject.StreamInfo.IsStream);

        txtStreamURL.Text = Station.TrackedObject.StreamInfo.StreamUrl;
    }

    private void radUseStreamYes_CheckedChanged(object sender, EventArgs e)
    {
        ToggleStreamControls(true);
        Station.TrackedObject.StreamInfo.IsStream = radUseStreamYes.Checked;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void radUseStreamNo_CheckedChanged(object sender, EventArgs e)
    {
        ToggleStreamControls(false);
        Station.TrackedObject.StreamInfo.IsStream = !radUseStreamNo.Checked;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void txtStreamURL_TextChanged(object sender, EventArgs e)
    {
        Station.TrackedObject.StreamInfo.StreamUrl = txtStreamURL.Text;
        mpStreamPlayer.StreamUrl = Station.TrackedObject.StreamInfo.StreamUrl;
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void btnGetFromRadioGarden_Click(object sender, EventArgs e)
    {
        var text = GlobalData.Strings.GetString("RadioGardenInput") ??
                   "Enter the radio.garden URL from the web: ";
        var caption = GlobalData.Strings.GetString("RadioGardenURLCaption") ?? "Radio Garden URL";

        InputBox input = new(caption, text, RetrieveUrlFromInputBox);
        input.ShowDialog();
    }

    private async void RetrieveUrlFromInputBox(object? sender, EventArgs e)
    {
        var streamChecker = new AudioStreamChecker(TimeSpan.FromSeconds(5));
        if (e is not InputBoxEventArgs args) return;

        var streamUrl = AudioStreamChecker.ConvertRadioGardenURL(args.InputText);
        var isValid = await streamChecker.IsAudioStreamValidAsync(streamUrl);
        if (isValid)
            txtStreamURL.Text = streamUrl;
        else
            txtStreamURL.Text = GlobalData.Strings.GetString("InvalidStream") ??
                                "Invalid stream - Will not work in game";
        StationUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void ToggleStreamControls(bool onOff)
    {
        lblStreamURL.Visible = onOff;
        txtStreamURL.Visible = onOff;
        mpStreamPlayer.Visible = onOff;
        btnGetFromRadioGarden.Visible = onOff;

        if (!txtStreamURL.Visible)
            mpStreamPlayer.StopStream();

        grpSongs.Visible = !onOff;
    }

    #endregion

    #region Hover Help

    private void lblName_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("StationNameHelp");
    }

    private void lblIcon_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("IconHelp");
    }

    private void lblUsingCustomIcon_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("CustomIconHelp");
    }

    private void lblInkPath_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("InkPathHelp");
    }

    private void lblInkPart_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("InkPartHelp");
    }

    private void lblFM_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("FMHelp");
    }

    private void lblVolume_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("VolumeHelp");
    }

    private void lblVolumeVal_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("VolumeValHelp");
    }

    private void lblUseStream_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("UseStreamHelp");
    }

    private void lblStreamURL_MouseEnter(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("StreamURLHelp");
    }

    private void lbl_MouseLeave(object sender, EventArgs e)
    {
        lblStatus.Text = GlobalData.Strings.GetString("Ready");
    }

    #endregion

    
}