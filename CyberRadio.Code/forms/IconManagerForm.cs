using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;
using RadioExt_Helper.user_controls;
using RadioExt_Helper.utility;
using RadioExt_Helper.utility.event_args;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            lbIcons.Station = _station;
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
            SelectIconEditor(_station.Id, icon.IconId);
        }

        /// <summary>
        /// Updates the UI with the correct icon editor based on the station's ID.
        /// </summary>
        /// <param name="stationId">The ID of the station to get the editor of.</param>
        /// <param name="iconId">The ID of the icon associated with the editor.</param>
        private void SelectIconEditor(Guid? stationId, Guid? iconId)
        {
            if (stationId == null) return;

            var editor = StationManager.Instance.GetStationIconEditor(stationId, iconId);
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
    }
}
