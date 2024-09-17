using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.user_controls;
using RadioExt_Helper.utility;
using WIG.Lib.Models;

namespace RadioExt_Helper.forms
{
    public partial class IconManagerForm : Form
    {
        public event EventHandler<TrackableObject<WolvenIcon>>? IconAdded;
        public event EventHandler<TrackableObject<WolvenIcon>>? IconUpdated;
        public event EventHandler<TrackableObject<WolvenIcon>>? IconDeleted;

        private readonly ImageList _stationImageList = new();
        private bool _ignoreSelectedIndexChanged;
        private IconEditor? _currentEditor;
        private readonly TrackableObject<Station> _station;

        private bool _addedFromImagePath = false;
        private TrackableObject<WolvenIcon>? _iconFromImagePath = null;

        public IconManagerForm(TrackableObject<Station> station)
        {
            InitializeComponent();

            _station = station;
        }

        public IconManagerForm(TrackableObject<Station> station, string imagePath)
        {
            InitializeComponent();

            _station = station;
            _iconFromImagePath = new TrackableObject<WolvenIcon>(WolvenIcon.FromPath(imagePath));
            _addedFromImagePath = true;
        }

        private void IconManagerForm_Load(object sender, EventArgs e)
        {
            Text = $"Icon Manager - {_station.TrackedObject.MetaData.DisplayName}";

            lbIcons.DataSource = _station.TrackedObject.Icons.ToBindingList();
            lbIcons.DisplayMember = "TrackedObject.AtlasName";

            SetImageList();

            if (_addedFromImagePath && _iconFromImagePath != null)
            {
                AddNewIcon(_iconFromImagePath);
            }
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

            if (lbIcons.SelectedItem is not TrackableObject<WolvenIcon> icon) return;
            SelectIconEditor(icon.Id);
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

                //Remove the subscribed event if the current editor is not null.
                if (_currentEditor != null)
                    _currentEditor.IconUpdated -= _currentEditor_IconUpdated;

                splitContainer1.Panel2.SuspendLayout();
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(editor);
                splitContainer1.Panel2.ResumeLayout();

                _currentEditor = editor;

                //Resubscribe to the event for the icon updating
                _currentEditor.IconUpdated += _currentEditor_IconUpdated;
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<IconManagerForm>("UpdateIconEditor")
                    .Error(ex, "An error occurred while updating the icon editor.");
            }
        }

        private void _currentEditor_IconUpdated(object? sender, TrackableObject<WolvenIcon> icon)
        {
            IconUpdated?.Invoke(this, icon);
        }

        private void AddNewIcon(TrackableObject<WolvenIcon> icon)
        {
            var added = StationManager.Instance.AddStationIcon(_station.Id, icon);
            if (!added) return;

            ResetListBox();

            lbIcons.SelectedItem = icon;
            SelectIconEditor(icon.Id);

            IconAdded?.Invoke(this, icon);
        }

        private void RemoveIcon(TrackableObject<WolvenIcon> icon)
        {
            var text = GlobalData.Strings.GetString("ConfirmIconDelete") ?? "Do you want to delete associated icon files from disk?";
            var caption = GlobalData.Strings.GetString("Confirm") ?? "Confirm Delete";
            var result = MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                StationManager.Instance.RemoveStationIcon(_station.Id, icon, true);
            else
                StationManager.Instance.RemoveStationIcon(_station.Id, icon);

            ResetListBox();

            IconDeleted?.Invoke(this, icon);
        }

        private void ResetListBox()
        {
            lbIcons.DataSource = null;
            lbIcons.DataSource = _station.TrackedObject.Icons.ToBindingList();
            lbIcons.DisplayMember = "TrackedObject.AtlasName";
        }

        private void btnAddIcon_Click(object sender, EventArgs e)
        {
            var icon = new TrackableObject<WolvenIcon>(new WolvenIcon());
            AddNewIcon(icon);
        }

        private void btnDeleteIcon_Click(object sender, EventArgs e)
        {
            if (lbIcons.SelectedItem is not TrackableObject<WolvenIcon> icon) return;
            RemoveIcon(icon);
        }

        private void btnEnableIcon_Click(object sender, EventArgs e)
        {
            if (lbIcons.SelectedItem is not TrackableObject<WolvenIcon> icon) return;
            EnableIcon(icon);
        }

        private void EnableIcon(TrackableObject<WolvenIcon> icon)
        {
            lbIcons.BeginUpdate();
            foreach (var i in _station.TrackedObject.Icons)
            {
                i.TrackedObject.IsActive = false;
            }

            icon.TrackedObject.IsActive = true;
            lbIcons.Invalidate();
            lbIcons.EndUpdate();

            IconUpdated?.Invoke(this, icon);
        }

        private void DisableIcon(TrackableObject<WolvenIcon> icon)
        {
            lbIcons.BeginUpdate();
            icon.TrackedObject.IsActive = false;
            lbIcons.Invalidate();
            lbIcons.EndUpdate();

            IconUpdated?.Invoke(this, icon);
        }

        private void btnDisableIcon_Click(object sender, EventArgs e)
        {
            if (lbIcons.SelectedItem is not TrackableObject<WolvenIcon> icon) return;
            DisableIcon(icon);
        }
    }
}
