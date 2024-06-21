using System.Globalization;
using AetherUtils.Core.Reflection;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.user_controls;

public sealed partial class StationEditor : UserControl, IUserControl
{
    private readonly CustomMusicCtl _musicCtl;

    public StationEditor(Station station)
    {
        InitializeComponent();
        Dock = DockStyle.Fill;

        Station = station;
        _musicCtl = new CustomMusicCtl(Station);

        grpSongs.Controls.Add(_musicCtl);
        
        GlobalData.UiIcons.ToList().ForEach(icon => cmbUIIcons.Items.Add(icon));
    }

    public Station Station { get; }

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

        SetDisplayTabValues();
        SetMusicTabValues();
        Translate();

        ResumeLayout();
    }

    #region Display and Icon Tab
    private void SetDisplayTabValues()
    {
        txtDisplayName.Text = Station.MetaData.DisplayName;
        cmbUIIcons.SelectedIndex = cmbUIIcons.Items.IndexOf(Station.MetaData.Icon);

        if (Station.CustomIcon.UseCustom)
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

        txtInkAtlasPath.Text = Station.CustomIcon.InkAtlasPath;
        txtInkAtlasPart.Text = Station.CustomIcon.InkAtlasPart;
        nudFM.Value = (decimal)Station.MetaData.Fm;
        volumeSlider.Value = (int)(Station.MetaData.Volume / 0.1f);
        lblSelectedVolume.Text = $@"{Station.MetaData.Volume:F1}";
    }

    private void txtDisplayName_TextChanged(object sender, EventArgs e)
    {
        Station.MetaData.DisplayName = txtDisplayName.Text;
    }

    private void cmbUIIcons_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbUIIcons.SelectedItem is string iconStr)
            Station.MetaData.Icon = iconStr;
    }

    private void radUseCustomYes_CheckedChanged(object sender, EventArgs e)
    {
        Station.CustomIcon.UseCustom = radUseCustomYes.Checked;
    }

    private void radUseCustomNo_CheckedChanged(object sender, EventArgs e)
    {
        Station.CustomIcon.UseCustom = !radUseCustomNo.Checked;
        txtInkAtlasPart.Visible = !radUseCustomNo.Checked;
        txtInkAtlasPath.Visible = !radUseCustomNo.Checked;
        lblInkPart.Visible = !radUseCustomNo.Checked;
        lblInkPath.Visible = !radUseCustomNo.Checked;
    }

    private void txtInkAtlasPath_TextChanged(object sender, EventArgs e)
    {
        Station.CustomIcon.InkAtlasPath = txtInkAtlasPath.Text;
    }

    private void txtInkAtlasPart_TextChanged(object sender, EventArgs e)
    {
        Station.CustomIcon.InkAtlasPart = txtInkAtlasPart.Text;
    }

    private void nudFM_ValueChanged(object sender, EventArgs e)
    {
        Station.MetaData.Fm = (float)nudFM.Value;
    }

    private void volumeSlider_Scroll(object sender, EventArgs e)
    {
        Station.MetaData.Volume = volumeSlider.Value * 0.1f;
        lblSelectedVolume.Text = $@"{Station.MetaData.Volume:F1}";
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
        radUseStreamYes.Checked = Station.StreamInfo.IsStream;
        radUseStreamNo.Checked = !radUseStreamYes.Checked;

        ToggleStreamControls(Station.StreamInfo.IsStream);

        txtStreamURL.Text = Station.StreamInfo.StreamUrl;
    }

    private void radUseStreamYes_CheckedChanged(object sender, EventArgs e)
    {
        ToggleStreamControls(true);
        Station.StreamInfo.IsStream = radUseStreamYes.Checked;
    }

    private void radUseStreamNo_CheckedChanged(object sender, EventArgs e)
    {
        ToggleStreamControls(false);
        Station.StreamInfo.IsStream = !radUseStreamNo.Checked;
    }

    private void txtStreamURL_TextChanged(object sender, EventArgs e)
    {
        Station.StreamInfo.StreamUrl = txtStreamURL.Text;
        mpStreamPlayer.StreamUrl = Station.StreamInfo.StreamUrl;
    }

    private void ToggleStreamControls(bool onOff)
    {
        lblStreamURL.Visible = onOff;
        txtStreamURL.Visible = onOff;
        mpStreamPlayer.Visible = onOff;

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