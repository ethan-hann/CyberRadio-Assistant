using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.user_controls;
using RadioExt_Helper.utility;
using Icon = RadioExt_Helper.models.Icon;

namespace RadioExt_Helper.forms
{
    public partial class IconManagerForm : Form
    {
        public event EventHandler<Guid>? IconAdded;
        public event EventHandler<Guid>? IconUpdated;
        public event EventHandler<Guid>? IconDeleted;

        private readonly ImageList _stationImageList = new();
        private bool _ignoreSelectedIndexChanged;
        private IconEditor? _currentEditor;
        private readonly TrackableObject<Station> _station;

        public IconManagerForm(TrackableObject<Station> station)
        {
            InitializeComponent();

            _station = station;
        }

        private void IconManagerForm_Load(object sender, EventArgs e)
        {
            Text = $"Icon Manager - {_station.TrackedObject.MetaData.DisplayName}";

            lbIcons.DataSource = _station.TrackedObject.Icons;
            lbIcons.DisplayMember = "Name";

            SetImageList();
        }

        /// <summary>
        ///     Sets up the image list for the station list.
        /// </summary>
        private void SetImageList()
        {
            _stationImageList.Images.Add("disabled", Resources.disabled);
            _stationImageList.Images.Add("enabled", Resources.enabled);
            _stationImageList.ImageSize = new Size(16, 16);
            lbIcons.ImageList = _stationImageList;
        }

        private void lbIcons_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SafeInvoke(() => SelectListBoxItem(lbIcons.SelectedIndex, true));
        }

        private void SelectListBoxItem(int index, bool userDriven)
        {
            if (index < 0 || index >= lbIcons.Items.Count) return;

            if (userDriven)
            {
                if (_ignoreSelectedIndexChanged)
                {
                    _ignoreSelectedIndexChanged = false;
                    return;
                }
            }
            else
            {
                lbIcons.SelectedIndex = index;
            }

            if (lbIcons.SelectedItem is not Icon icon) return;
            SelectIconEditor(icon.IconId);
        }

        /// <summary>
        /// Updates the UI with the correct icon editor based on the station's ID.
        /// </summary>
        /// <param name="iconId">The ID of the icon associated with the editor.</param>
        private void SelectIconEditor(Guid? iconId)
        {
            var editor = StationManager.Instance.GetStationIconEditor(_station.Id, iconId);
            UpdateIconEditor(editor);
        }

        /// <summary>
        ///     Updates the currently displayed icon editor.
        ///     <param name="editor">The editor to display in the split panel.</param>
        /// </summary>
        private void UpdateIconEditor(IconEditor? editor)
        {
            try
            {
                if (_currentEditor == editor) return;
                if (editor == null) return;

                splitContainer1.Panel2.SuspendLayout();
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(editor);
                splitContainer1.Panel2.ResumeLayout();

                _currentEditor = editor;
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<IconManagerForm>("UpdateIconEditor")
                    .Error(ex, "An error occurred while updating the icon editor.");
            }
        }

        private void btnAddIcon_Click(object sender, EventArgs e)
        {
            var icon = new Icon();

            var added = StationManager.Instance.AddStationIcon(_station.Id, ref icon);
            if (!added) return;

            lbIcons.SelectedItem = icon;
            SelectIconEditor(icon.IconId);

            IconAdded?.Invoke(this, icon.IconId);
        }

        private void btnDeleteIcon_Click(object sender, EventArgs e)
        {
            if (lbIcons.SelectedItem is not Icon icon) return;
            var text = GlobalData.Strings.GetString("ConfirmIconDelete") ?? "Do you want to delete associated icon files from disk?";
            var caption = GlobalData.Strings.GetString("Confirm") ?? "Confirm Delete";
            var result = MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                StationManager.Instance.RemoveStationIcon(_station.Id, ref icon, true);
            else
                StationManager.Instance.RemoveStationIcon(_station.Id, ref icon);

            IconDeleted?.Invoke(this, icon.IconId);
        }

        private void btnEnableIcon_Click(object sender, EventArgs e)
        {
            if (lbIcons.SelectedItem is not Icon icon) return;
            lbIcons.BeginUpdate();
            foreach (var i in _station.TrackedObject.Icons)
            {
                i.IsActive = false;
            }

            icon.IsActive = true;
            lbIcons.Invalidate();
            lbIcons.EndUpdate();

            IconUpdated?.Invoke(this, icon.IconId);
        }

        private void btnDisableIcon_Click(object sender, EventArgs e)
        {
            if (lbIcons.SelectedItem is not Icon icon) return;

            lbIcons.BeginUpdate();
            icon.IsActive = false;
            lbIcons.Invalidate();
            lbIcons.EndUpdate();

            IconUpdated?.Invoke(this, icon.IconId);
        }
    }
}
