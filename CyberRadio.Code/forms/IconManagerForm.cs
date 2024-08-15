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

        public IconManagerForm()
        {
            InitializeComponent();
        }

        private void IconManagerForm_Load(object sender, EventArgs e)
        {
            lbStations.DataSource = StationManager.Instance.StationsAsBindingList;
            lbStations.DisplayMember = "TrackedObject.MetaData";

            SetImageList();


        }

        /// <summary>
        ///     Sets up the image list for the station list.
        /// </summary>
        private void SetImageList()
        {
            _stationImageList.Images.Add("disabled", Resources.disabled);
            _stationImageList.Images.Add("enabled", Resources.enabled);
            _stationImageList.Images.Add("edited_station", Resources.save_pending);
            _stationImageList.Images.Add("saved_station", Resources.disk);
            _stationImageList.ImageSize = new Size(16, 16);
            lbStations.ImageList = _stationImageList;
        }

        private void lbStations_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SafeInvoke(() => SelectListBoxItem(lbStations.SelectedIndex, true));
        }

        private void SelectListBoxItem(int index, bool userDriven)
        {
            if (index < 0 || index >= lbStations.Items.Count) return;

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
                lbStations.SelectedIndex = index;
            }

            if (lbStations.SelectedItem is not TrackableObject<Station> station) return;
            SelectStationEditor(station.Id);
            UpdateTitleBar(station.Id);
        }

        /// <summary>
        /// Updates the UI with the correct icon editor based on the station's ID.
        /// </summary>
        /// <param name="stationId">The ID of the station to get the editor of.</param>
        private void SelectStationEditor(Guid? stationId)
        {
            if (stationId == null) return;

            UpdateIconEditor(StationManager.Instance.GetStation(stationId)?.Value);
        }

        /// <summary>
        ///     Updates the currently displayed station editor.
        ///     <param name="editor">The editor to display in the split panel.</param>
        /// </summary>
        private void UpdateIconEditor(StationEditor? editor)
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
                AuLogger.GetCurrentLogger<MainForm>("UpdateStationEditor")
                    .Error(ex, "An error occurred while updating the station editor.");
            }
        }
    }
}
