using RadioExt_Helper.models;
using RadioExt_Helper.utility;
using System.Globalization;
using static RadioExt_Helper.utility.CEventArgs;

namespace RadioExt_Helper.user_controls
{
    public sealed partial class StationEditor : UserControl, IUserControl
    {
        public EventHandler? StationUpdated;

        private MetaData _metaData = new();
        private CustomIcon _icon = new();
        private StreamInfo _streamInfo = new();
        private SongList _songList = new();

        private CustomMusicCtl _musicCtl = new();

        private bool _isPasteOperation;
        private string _initialStationName = string.Empty;

        public string UniqueName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Station Station { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public StationEditor()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public void SetMetaData(MetaData metaData, SongList songList)
        {
            _metaData = metaData;
            _icon = _metaData.CustomIcon;
            _streamInfo = _metaData.StreamInfo;
            _songList = songList;

            _initialStationName = _metaData.DisplayName;

            if (grpSongs.Controls["CustomMusicCtl"] is CustomMusicCtl musicCtl)
                musicCtl.SetSongList(_songList);

            SetDisplayTabValues();
            SetMusicTabValues();
            Translate();
        }

        private void StationEditor_Load(object sender, EventArgs e)
        {
            //Populate combobox of UIIcons
            GlobalData.UiIcons.ToList().ForEach(icon => cmbUIIcons.Items.Add(icon));

            grpSongs.Controls.Add(_musicCtl);
            _musicCtl.SongListUpdated += UpdateSongList;

            SetDisplayTabValues();
            SetMusicTabValues();
            Translate();
        }

        private void UpdateSongList(object? sender, EventArgs e)
        {
            if (e is SongListUpdatedEventArgs args)
            {
                _songList = args.Songs;
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
            btnGetRadioGardenURL.Text = GlobalData.Strings.GetString("GetURLFromRadioGarden");
            txtPastedURL.PlaceholderText = GlobalData.Strings.GetString("URLHint");
            radUseStreamYes.Text = GlobalData.Strings.GetString("Yes");
            radUseStreamNo.Text = GlobalData.Strings.GetString("No");

            lblStatus.Text = GlobalData.Strings.GetString("Ready");

            _musicCtl.Translate();
        }

        #region Display and Icon Tab
        private void SetDisplayTabValues()
        {
            txtDisplayName.Text = _metaData.DisplayName;
            cmbUIIcons.Text = _metaData.Icon;

            if (_icon.UseCustom)
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

            txtInkAtlasPath.Text = _icon.InkAtlasPath;
            txtInkAtlasPart.Text = _icon.InkAtlasPart;
            nudFM.Value = (decimal)_metaData.Fm;
            volumeSlider.Value = (int)(_metaData.Volume / 0.1f);
            lblSelectedVolume.Text = $@"{_metaData.Volume:F1}";
        }

        private void txtDisplayName_TextChanged(object sender, EventArgs e)
        {
            _metaData.DisplayName = txtDisplayName.Text;
            UpdateEvent();
        }

        private void cmbUIIcons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUIIcons.SelectedItem is string iconStr)
            {
                _metaData.Icon = iconStr;
                UpdateEvent();
            }
        }

        private void radUseCustomYes_CheckedChanged(object sender, EventArgs e)
        {
            _icon.UseCustom = radUseCustomYes.Checked;
            UpdateEvent();
        }

        private void radUseCustomNo_CheckedChanged(object sender, EventArgs e)
        {
            _icon.UseCustom = !radUseCustomNo.Checked;
            txtInkAtlasPart.Visible = !radUseCustomNo.Checked;
            txtInkAtlasPath.Visible = !radUseCustomNo.Checked;
            lblInkPart.Visible = !radUseCustomNo.Checked;
            lblInkPath.Visible = !radUseCustomNo.Checked;

            UpdateEvent();
        }

        private void txtInkAtlasPath_TextChanged(object sender, EventArgs e)
        {
            _icon.InkAtlasPath = txtInkAtlasPath.Text;
            UpdateEvent();
        }

        private void txtInkAtlasPart_TextChanged(object sender, EventArgs e)
        {
            _icon.InkAtlasPart = txtInkAtlasPart.Text;
            UpdateEvent();
        }

        private void nudFM_ValueChanged(object sender, EventArgs e)
        {
            _metaData.Fm = (float)nudFM.Value;
            UpdateEvent();
        }

        private void volumeSlider_Scroll(object sender, EventArgs e)
        {
            _metaData.Volume = volumeSlider.Value * 0.1f;
            lblSelectedVolume.Text = $@"{_metaData.Volume:F1}";
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
            radUseStreamYes.Checked = _streamInfo.IsStream;
            radUseStreamNo.Checked = !radUseStreamYes.Checked;

            ToggleStreamControls(_streamInfo.IsStream);

            txtStreamURL.Text = _streamInfo.StreamUrl;
        }

        private void radUseStreamYes_CheckedChanged(object sender, EventArgs e)
        {
            ToggleStreamControls(true);
            _streamInfo.IsStream = radUseStreamYes.Checked;
            UpdateEvent();
        }

        private void radUseStreamNo_CheckedChanged(object sender, EventArgs e)
        {
            ToggleStreamControls(false);
            _streamInfo.IsStream = !radUseStreamNo.Checked;
            UpdateEvent();
        }

        private void txtStreamURL_TextChanged(object sender, EventArgs e)
        {
            _streamInfo.StreamUrl = txtStreamURL.Text;
            UpdateEvent();
        }

        private void ToggleStreamControls(bool onOff)
        {
            lblStreamURL.Visible = onOff;
            txtStreamURL.Visible = onOff;
            btnGetRadioGardenURL.Visible = onOff;

            if (!txtStreamURL.Visible)
                txtStreamURL.Text = string.Empty;

            if (txtPastedURL.Visible && radUseStreamNo.Checked)
                HideRadioGardenTextInput();

            grpSongs.Visible = !onOff;
        }

        private void HideRadioGardenTextInput()
        {
            txtPastedURL.Visible = false;
            txtStreamURL.Enabled = true;
            btnGetRadioGardenURL.Text = GlobalData.Strings.GetString("GetURLFromRadioGarden");
            txtPastedURL.Text = string.Empty;
        }

        private void ShowRadioGardenTextInput()
        {
            txtPastedURL.Visible = true;
            txtStreamURL.Enabled = false;
            btnGetRadioGardenURL.Text = GlobalData.Strings.GetString("Cancel");
        }

        private void btnGetRadioGardenURL_Click(object sender, EventArgs e)
        {
            if (txtPastedURL.Visible)
                HideRadioGardenTextInput();
            else
                ShowRadioGardenTextInput();
        }

        private void txtPastedURL_TextChanged(object sender, EventArgs e)
        {
            if (_isPasteOperation)
            {
                HandlePasteOperation();
                _isPasteOperation = false;
            }
            else
            {
                txtPastedURL.Text = string.Empty;
            }
        }

        private void txtPastedURL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e is { Control: true, KeyCode: Keys.V })
                _isPasteOperation = true;
        }

        private void HandlePasteOperation()
        {
            var pastedText = txtPastedURL.Text;
            if (pastedText.StartsWith("https://") && pastedText.Contains("radio.garden"))
            {
                txtStreamURL.Text = ParseRadioGardenURL(pastedText);
                _streamInfo.StreamUrl = txtStreamURL.Text;
                HideRadioGardenTextInput();
            }
            else
            {
                txtPastedURL.Text = string.Empty;
            }
        }

        private string ParseRadioGardenURL(string rawURL)
        {
            //TODO: Get actual stream url from pasted text
            return string.Empty;
        }

        #endregion

        /// <summary>
        /// Calls the event handler <see cref="StationUpdated"/> with the updated station information.
        /// </summary>
        private void UpdateEvent() => StationUpdated?.Invoke(this, new StationUpdatedEventArgs(_metaData, _songList, _initialStationName));

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

        private void btnGetRadioGardenURL_MouseEnter(object sender, EventArgs e)
        {
            if (!btnGetRadioGardenURL.Text.Equals(GlobalData.Strings.GetString("Cancel")))
                lblStatus.Text = GlobalData.Strings.GetString("RadioGardenBtnHelp");
        }

        private void lbl_MouseLeave(object sender, EventArgs e)
        {
            lblStatus.Text = GlobalData.Strings.GetString("Ready");
        }

        public void ApplyFonts()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
