using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AetherUtils.Core.Extensions;
using AetherUtils.Core.Logging;
using WIG.Lib.Models.Audio;
using WIG.Lib.Utility;

namespace RadioExt_Helper.forms
{
    public partial class VanillaStationSelector : Form
    {
        /// <summary>
        /// Occurs when a vanilla station is selected and the user confirms their selection.
        /// </summary>
        public EventHandler<VanillaStation>? OnStationSelected;

        /// <summary>
        /// Creates a new instance of the <see cref="VanillaStationSelector"/> form.
        /// </summary>
        public VanillaStationSelector()
        {
            InitializeComponent();
        }

        private void VanillaStationSelector_Load(object sender, EventArgs e)
        {
            // Populate the list box with vanilla stations
            PopulateListBox();
        }

        private void PopulateListBox()
        {
            lbVanillaStations.DataSource = null;
            lbVanillaStations.DataSource = AudioManager.Instance.VanillaStations;
            lbVanillaStations.DisplayMember = "StationName";
        }

        private void PopulateListView(VanillaStation station)
        {
            lvTracks.SuspendLayout();
            lvTracks.Items.Clear();
            foreach (var song in station.Tracks)
            {
                var durations = song.TrackDuration
                    .Select(d => TimeSpan.FromSeconds(d).ToString("g")).ToList();
                string durationString;
                if (durations.Count() > 1)
                    durationString = string.Join(", ", durations);
                else
                    durationString = durations.FirstOrDefault() ?? "Unknown";

                var lvItem = new ListViewItem([
                    song.TrackName,
                    song.TrackArtist,
                    durationString
                ])
                { Tag = song };

                lvTracks.Items.Add(lvItem);
            }

            lvTracks.ResizeColumns();
            lvTracks.ResumeLayout();
        }

        private void lbVanillaStations_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update the list view with songs from the selected station
            if (lbVanillaStations.SelectedItem is VanillaStation selectedStation)
            {
                PopulateListView(selectedStation);
            }
            else
            {
                lvTracks.Items.Clear();
                AuLogger.GetCurrentLogger<VanillaStationSelector>().Warn("Selected item is not a VanillaStation.");
            }
        }

        private void btnSelectStation_Click(object sender, EventArgs e)
        {
            if (lbVanillaStations.SelectedItem is VanillaStation station)
            {
                OnStationSelected?.Invoke(this, station);
                Close();
            }
            else
            {
                MessageBox.Show(this, Strings.VanillaStationSelector_NoStationSelectedMessage,
                    Strings.VanillaStationSelector_NoStationSelected, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            }
        }
    }
}
