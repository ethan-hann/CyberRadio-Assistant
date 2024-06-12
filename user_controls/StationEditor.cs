using RadioExt_Helper.models;
using RadioExt_Helper.utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadioExt_Helper.user_controls
{
    public partial class StationEditor : UserControl
    {
        public EventHandler? StationUpdated;

        private MetaData _metaData;
        private CustomIcon _icon;
        private StreamInfo _streamInfo;

        private bool isPasteOperation = false;

        public StationEditor(MetaData metaData)
        {
            InitializeComponent();

            _metaData = metaData;
            _icon = _metaData.CustomIcon;
            _streamInfo = _metaData.StreamInfo;

            //Populate combobox of UIIcons
            GlobalData.UIIcons.ToList().ForEach(icon => cmbUIIcons.Items.Add(icon));

            Dock = DockStyle.Fill;
        }

        private void StationEditor_Load(object sender, EventArgs e)
        {
            SetDisplayTabValues();

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
                lblInkAtlasPart.Visible = !radUseCustomNo.Checked;
                lblInkAtlasPath.Visible = !radUseCustomNo.Checked;
            }

            txtInkAtlasPath.Text = _icon.InkAtlasPath;
            txtInkAtlasPart.Text = _icon.InkAtlasPart;
        }

        private void radUseCustomYes_CheckedChanged(object sender, EventArgs e)
        {
            _icon.UseCustom = radUseCustomYes.Checked;
        }

        private void radUseCustomNo_CheckedChanged(object sender, EventArgs e)
        {
            _icon.UseCustom = !radUseCustomNo.Checked;
            txtInkAtlasPart.Visible = !radUseCustomNo.Checked;
            txtInkAtlasPath.Visible = !radUseCustomNo.Checked;
            lblInkAtlasPart.Visible = !radUseCustomNo.Checked;
            lblInkAtlasPath.Visible = !radUseCustomNo.Checked;
        }

        private void volumeSlider_Scroll(object sender, EventArgs e)
        {
            lblSelectedVolume.Text = $"{volumeSlider.Value:F1}";
            _metaData.Volume = volumeSlider.Value;
        }

        private void lblSelectedVolume_DoubleClick(object sender, EventArgs e)
        {
            txtVolumeEdit.Text = lblSelectedVolume.Text;
            txtVolumeEdit.Size = lblSelectedVolume.Size;
            txtVolumeEdit.Visible = true;
            txtVolumeEdit.Focus();
            txtVolumeEdit.SelectAll();
        }

        private void txtVolumeEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (float.TryParse(txtVolumeEdit.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out float newVolume))
                {
                    if (newVolume > volumeSlider.Maximum || newVolume < volumeSlider.Minimum)
                        e.Handled = true;
                    else
                    {
                        lblSelectedVolume.Text = txtVolumeEdit.Text;
                        txtVolumeEdit.Visible = false;
                        volumeSlider.Value = newVolume;
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtVolumeEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control characters, digits, decimal point, and minus sign
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Allow only one decimal point
            if (e.KeyChar == '.' && ((TextBox)sender).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        #endregion

        #region Music Tab
        private void radUseStreamYes_CheckedChanged(object sender, EventArgs e)
        {
            ToggleStreamControls(true);
            _streamInfo.IsStream = radUseStreamYes.Checked;
        }

        private void radUseStreamNo_CheckedChanged(object sender, EventArgs e)
        {
            ToggleStreamControls(false);
            _streamInfo.IsStream = !radUseStreamNo.Checked;
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
        }

        private void HideRadioGardenTextInput()
        {
            txtPastedURL.Visible = false;
            txtStreamURL.Enabled = true;
            btnGetRadioGardenURL.Text = "Get URL from Radio Garden";
            txtPastedURL.Text = string.Empty;
        }

        private void ShowRadioGardenTextInput()
        {
            txtPastedURL.Visible = true;
            txtStreamURL.Enabled = false;
            btnGetRadioGardenURL.Text = "Cancel";
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
            if (isPasteOperation)
            {
                HandlePasteOperation();
                isPasteOperation = false;
            }
            else
            {
                txtPastedURL.Text = string.Empty;
            }
        }

        private void txtPastedURL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
                isPasteOperation = true;
        }

        private void HandlePasteOperation()
        {
            string pastedText = txtPastedURL.Text;
            if (pastedText.StartsWith("https://") && pastedText.Contains("radio.garden"))
            {
                txtStreamURL.Text = pastedText;
                //TODO: Get actual stream url from pasted text
                _streamInfo.StreamURL = txtStreamURL.Text;
                HideRadioGardenTextInput();
            }
            else
            {
                txtPastedURL.Text = string.Empty;
            }
        }

        #endregion


        
    }
}
