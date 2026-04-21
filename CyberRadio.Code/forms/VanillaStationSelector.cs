// VanillaStationSelector.cs : RadioExt-Helper
// Copyright (C) 2026  Ethan Hann
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

#region

using AetherUtils.Core.Extensions;
using AetherUtils.Core.Logging;
using RadioExt_Helper.utility;
using WIG.Lib.Models.Audio;
using WIG.Lib.Utility;

#endregion

namespace RadioExt_Helper.forms;

public partial class VanillaStationSelector : Form
{
    /// <summary>
    ///     Occurs when a vanilla station is selected and the user confirms their selection.
    /// </summary>
    public EventHandler<VanillaStation>? OnStationSelected;

    /// <summary>
    ///     Creates a new instance of the <see cref="VanillaStationSelector" /> form.
    /// </summary>
    public VanillaStationSelector()
    {
        InitializeComponent();
    }

    private void VanillaStationSelector_Load(object sender, EventArgs e)
    {
        Translate();
        // Populate the list box with vanilla stations
        PopulateListBox();
    }

    private void Translate()
    {
        Text = Strings.VanillaStationSelector_Title;
        lblHelp.Text = Strings.VanillaStationSelector_Instructions;
        btnSelectStation.Text = Strings.VanillaStationSelector_SelectStationButton;
        colTrackName.Text = Strings.VanillaStationSelector_Column_TrackName;
        colTrackArtist.Text = Strings.VanillaStationSelector_Column_TrackArtist;
        colTrackDuration.Text = Strings.VanillaStationSelector_Column_TrackDuration;
    }

    private void PopulateListBox()
    {
        List<VanillaStation> stationsAlreadyReplaced = new();
        StationManager.Instance.ReplacementStationsAsList.ForEach(s =>
            stationsAlreadyReplaced.Add(s.TrackedObject.VanillaStation));

        lbVanillaStations.DataSource = null;
        lbVanillaStations.DataSource = AudioManager.Instance.VanillaStations.Except(stationsAlreadyReplaced).ToList();
        lbVanillaStations.DisplayMember = "ToString";
    }

    private void PopulateListView(VanillaStation station)
    {
        lvTracks.SuspendLayout();
        lvTracks.Items.Clear();
        foreach (var song in station.Tracks)
        {
            var durations = song.TrackDuration.Select(d => TimeSpan.FromSeconds(d).ToString("g")).ToList();
            string durationString;
            if (durations.Count() > 1)
                durationString = string.Join(", ", durations);
            else
                durationString = durations.FirstOrDefault() ?? "Unknown";

            ListViewItem lvItem = new([
                song.TrackName,
                song.TrackArtist,
                durationString
            ]) { Tag = song };

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
                Strings.VanillaStationSelector_NoStationSelected, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}