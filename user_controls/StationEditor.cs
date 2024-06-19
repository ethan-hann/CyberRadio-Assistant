using AetherUtils.Core.Reflection;
using RadioExt_Helper.models;
using RadioExt_Helper.utility;
using System.ComponentModel;
using System.Globalization;
using static RadioExt_Helper.utility.CEventArgs;

namespace RadioExt_Helper.user_controls
{
    public sealed partial class StationEditor : UserControl, IUserControl
    {
        public EventHandler? StationUpdated;

        private readonly Station _station;
        private readonly CustomMusicCtl _musicCtl;

        private string _initialStationName = string.Empty;

        public Station Station { get => _station; }

        public StationEditor(Station station)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            _station = station;

            _initialStationName = _station.MetaData.DisplayName;
            _musicCtl = new(_station);
            _musicCtl.SongListUpdated += UpdateSongList;

            grpSongs.Controls.Add(_musicCtl);

            //Populate combobox of UIIcons
            //cmbUIIcons.DataSource = GlobalData.UiIcons;
            //cmbUIIcons.DisplayMember = "Name";
            GlobalData.UiIcons.ToList().ForEach(icon => cmbUIIcons.Items.Add(icon));
            ApplyFonts();
        }

        public void ApplyFonts()
        {
            ApplyFontsToControls(this);
        }

        private void ApplyFontsToControls(Control control)
        {
            switch (control)
            {
                case MenuStrip:
                case GroupBox:
                case Button:
                    FontHandler.Instance.ApplyFont(control, "CyberPunk_Regular", 9, FontStyle.Bold);
                    break;
                case TabControl:
                    FontHandler.Instance.ApplyFont(control, "CyberPunk_Regular", 12, FontStyle.Bold);
                    break;
                case Label:
                    if (control.Name.Equals("lblSelectedVolume"))
                        break;
                    FontHandler.Instance.ApplyFont(control, "CyberPunk_Regular", 9, FontStyle.Regular);
                    break;
            }

            foreach (Control child in control.Controls)
                ApplyFontsToControls(child);
        }

        private void StationEditor_Load(object sender, EventArgs e)
        {
            SuspendLayout();

            SetDisplayTabValues();
            SetMusicTabValues();
            Translate();

            ResumeLayout();
        }

        private void UpdateSongList(object? sender, EventArgs e)
        {
            if (e is SongListUpdatedEventArgs args)
            {
                _station.SongsAsList = args.Songs;
                UpdateEvent();
            }
        }

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

        #region Display and Icon Tab
        private void SetDisplayTabValues()
        {
            txtDisplayName.Text = _station.MetaData.DisplayName;
            cmbUIIcons.SelectedIndex = cmbUIIcons.Items.IndexOf(_station.MetaData.Icon);

            if (_station.CustomIcon.UseCustom)
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

            txtInkAtlasPath.Text = _station.CustomIcon.InkAtlasPath;
            txtInkAtlasPart.Text = _station.CustomIcon.InkAtlasPart;
            nudFM.Value = (decimal)_station.MetaData.Fm;
            volumeSlider.Value = (int)(_station.MetaData.Volume / 0.1f);
            lblSelectedVolume.Text = $@"{_station.MetaData.Volume:F1}";
        }

        private void txtDisplayName_TextChanged(object sender, EventArgs e)
        {
            _station.MetaData.DisplayName = txtDisplayName.Text;
            UpdateEvent();
            _initialStationName = txtDisplayName.Text;
        }

        private void cmbUIIcons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUIIcons.SelectedItem is string iconStr)
            {
                _station.MetaData.Icon = iconStr;
                UpdateEvent();
            }
        }

        private void radUseCustomYes_CheckedChanged(object sender, EventArgs e)
        {
            _station.CustomIcon.UseCustom = radUseCustomYes.Checked;
            UpdateEvent();
        }

        private void radUseCustomNo_CheckedChanged(object sender, EventArgs e)
        {
            _station.CustomIcon.UseCustom = !radUseCustomNo.Checked;
            txtInkAtlasPart.Visible = !radUseCustomNo.Checked;
            txtInkAtlasPath.Visible = !radUseCustomNo.Checked;
            lblInkPart.Visible = !radUseCustomNo.Checked;
            lblInkPath.Visible = !radUseCustomNo.Checked;

            UpdateEvent();
        }

        private void txtInkAtlasPath_TextChanged(object sender, EventArgs e)
        {
            _station.CustomIcon.InkAtlasPath = txtInkAtlasPath.Text;
            UpdateEvent();
        }

        private void txtInkAtlasPart_TextChanged(object sender, EventArgs e)
        {
            _station.CustomIcon.InkAtlasPart = txtInkAtlasPart.Text;
            UpdateEvent();
        }

        private void nudFM_ValueChanged(object sender, EventArgs e)
        {
            _station.MetaData.Fm = (float)nudFM.Value;
            UpdateEvent();
        }

        private void volumeSlider_Scroll(object sender, EventArgs e)
        {
            _station.MetaData.Volume = volumeSlider.Value * 0.1f;
            lblSelectedVolume.Text = $@"{_station.MetaData.Volume:F1}";
            UpdateEvent();
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
                e.Handled = true;
            else
            {
                lblSelectedVolume.Text = txtVolumeEdit.Text;
                txtVolumeEdit.Visible = false;
                volumeSlider.Enabled = true;
                volumeSlider.Value = (int)(newVolume / 0.1f);
                e.Handled = true;
                UpdateEvent();
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
            radUseStreamYes.Checked = _station.StreamInfo.IsStream;
            radUseStreamNo.Checked = !radUseStreamYes.Checked;

            ToggleStreamControls(_station.StreamInfo.IsStream);

            txtStreamURL.Text = _station.StreamInfo.StreamUrl;
        }

        private void radUseStreamYes_CheckedChanged(object sender, EventArgs e)
        {
            ToggleStreamControls(true);
            _station.StreamInfo.IsStream = radUseStreamYes.Checked;
            UpdateEvent();
        }

        private void radUseStreamNo_CheckedChanged(object sender, EventArgs e)
        {
            ToggleStreamControls(false);
            _station.StreamInfo.IsStream = !radUseStreamNo.Checked;
            UpdateEvent();
        }

        private void txtStreamURL_TextChanged(object sender, EventArgs e)
        {
            _station.StreamInfo.StreamUrl = txtStreamURL.Text;
            mpStreamPlayer.StreamURL = _station.StreamInfo.StreamUrl;
            UpdateEvent();
        }

        private void ToggleStreamControls(bool onOff)
        {
            lblStreamURL.Visible = onOff;
            txtStreamURL.Visible = onOff;
            mpStreamPlayer.Visible = onOff;

            if (!txtStreamURL.Visible)
                mpStreamPlayer.StopStream();

            //if (!txtStreamURL.Visible)
            //    txtStreamURL.Text = string.Empty;

            grpSongs.Visible = !onOff;
        }

        #endregion

        /// <summary>
        /// Calls the event handler <see cref="StationUpdated"/> with the updated station information.
        /// </summary>
        private void UpdateEvent() => StationUpdated?.Invoke(this, new StationUpdatedEventArgs(_station, _initialStationName));

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
}
