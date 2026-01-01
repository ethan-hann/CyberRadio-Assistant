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
using RadioExt_Helper.models;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.user_controls
{
    /// <summary>
    /// A UserControl for displaying and editing properties of a replaced track associated with a replacement station.
    /// </summary>
    public partial class ReplacedTrackPropertiesCtl : UserControl, IEditor
    {
        /// <summary>
        /// Event triggered when the track's properties are changed. Event data contains the vanilla track name.
        /// </summary>
        public EventHandler<string>? TrackChanged;

        private readonly string _trackName;

        /// <summary>
        /// Initializes a new instance of the ReplacedTrackPropertiesCtl class with the specified replacement station.
        /// </summary>
        /// <param name="station">A TrackableObject containing the replacement station to associate with this control. Cannot be null.</param>
        /// <param name="trackName">The name of the track this property window is editing.</param>
        public ReplacedTrackPropertiesCtl(TrackableObject<ReplacementStation> station, string trackName)
        {
            InitializeComponent();

            ReplacedStation = station;
            _trackName = trackName;
        }

        private void ReplacedTrackPropertiesCtl_Load(object sender, EventArgs e)
        {
            fdlgSelectFile.Title = Strings.AddReplacementTrackTitle;
            fdlgSelectFile.Filter = 
                @"Audio Files|*.mp3;*.wav;*.ogg;*.flac;*.mp2;*.wax;*.wma;*.wem";

            PopulateListView();
        }

        /// <inheritdoc />
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <inheritdoc />
        public EditorType Type { get; set; }

        /// <inheritdoc />
        public TrackableObject<AdditionalStation>? Station => null; // Not applicable for this control

        /// <inheritdoc />
        public TrackableObject<ReplacementStation>? ReplacedStation { get; } // The replacement station being edited

        /// <inheritdoc />
        public void Translate()
        {
            lblWEMIdHelp.Text = Strings.WEMIdHelp;
            lblWemIdDisclaimer.Text = Strings.WEMIdDisclaimer;
            grpReplacedTrack.Text = Strings.ReplacedWemIdsGroupBox;
            colWemId.Text = Strings.WemId;
            colReplacedFile.Text = Strings.ReplacedWithFile;

            btnReplace.Text = Strings.ReplaceWemId;
            btnRemove.Text = Strings.RemoveReplacedWemId;
            btnReplaceAll.Text = Strings.ReplaceAllWemIds;
            btnRemoveAll.Text = Strings.RemoveAllWemIds;

            fdlgSelectFile.Title = Strings.AddReplacementTrackTitle;
            fdlgSelectFile.Filter = 
                @"Audio Files|*.mp3;*.wav;*.ogg;*.flac;*.mp2;*.wax;*.wma;*.wem";
        }

        private void PopulateListView()
        {
            if (ReplacedStation is null) return;

            lvTracks.SuspendLayout();
            lvTracks.Items.Clear();

            //Get list of wem ids from replaced station and vanilla station name
            var wemIds = ReplacedStation.TrackedObject.VanillaStation?.Tracks
                .FirstOrDefault(t => t.TrackName.Equals(_trackName))?.WemIds;

            if (wemIds is null)
            {
                AuLogger.GetCurrentLogger<ReplacedTrackPropertiesCtl>()
                    .Warn(
                        $"No WEM IDs found for track '{_trackName}' in vanilla station associated with replacement station '{ReplacedStation.TrackedObject.DisplayName}'.");
                return;
            }

            //Each WEM ID should be treated as its own entry in the list view and display the replaced file for the WEM ID and track combo

            foreach (var lvItem in from wemId in wemIds let replacedFile = ReplacedStation.TrackedObject.Tracks
                         .FirstOrDefault(rt => rt.VanillaTrackName.Equals(_trackName) && rt.WemId.Equals(wemId))?
                         .ReplacementFilePath ?? Strings.TrackNotReplacedYet select new ListViewItem([
                             wemId,
                             replacedFile
                         ])
                         { Tag = wemId })
            {
                lvTracks.Items.Add(lvItem);
            }

            lvTracks.ResizeColumns();
            lvTracks.ResumeLayout();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (ReplacedStation is null) return;

            if (lvTracks.SelectedItems.Count <= 0) return;
            if (lvTracks.SelectedItems[0].Tag is not string selectedWemId) return;
            if (fdlgSelectFile.ShowDialog() != DialogResult.OK) return;

            if (!ReplacedStation.TrackedObject.ReplaceTrack(
                    _trackName,
                    selectedWemId,
                    fdlgSelectFile.FileName)) return;

            PopulateListView();
            TrackChanged?.Invoke(this, _trackName);
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            if (ReplacedStation is null) return;
            if (fdlgSelectFile.ShowDialog() != DialogResult.OK) return;
            var wemIds = ReplacedStation.TrackedObject.VanillaStation?.Tracks
                .FirstOrDefault(t => t.TrackName.Equals(_trackName))?.WemIds;
            if (wemIds is null)
            {
                AuLogger.GetCurrentLogger<ReplacedTrackPropertiesCtl>("btnReplaceAll_Click")
                    .Error(
                        $"No WEM IDs found for track '{_trackName}' in vanilla station associated with replacement station '{ReplacedStation.TrackedObject.DisplayName}'. This indicates that the Google sheet was not parsed correctly!");
                return;
            }

            foreach (var wemId in wemIds)
            {
                ReplacedStation.TrackedObject.ReplaceTrack(
                    _trackName,
                    wemId,
                    fdlgSelectFile.FileName);
            }

            PopulateListView();
            TrackChanged?.Invoke(this, _trackName);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (ReplacedStation is null) return;

            if (lvTracks.SelectedItems.Count <= 0) return;
            if (lvTracks.SelectedItems[0].Tag is not string selectedWemId) return;
            if (!ReplacedStation.TrackedObject.RemoveReplacedTrack(
                    _trackName,
                    selectedWemId)) return;

            PopulateListView();
            TrackChanged?.Invoke(this, _trackName);
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            if (ReplacedStation is null) return;
            var wemIds = ReplacedStation.TrackedObject.VanillaStation?.Tracks
                .FirstOrDefault(t => t.TrackName.Equals(_trackName))?.WemIds;
            if (wemIds is null)
            {
                AuLogger.GetCurrentLogger<ReplacedTrackPropertiesCtl>("btnRemoveAll_Click")
                    .Error(
                        $"No WEM IDs found for track '{_trackName}' in vanilla station associated with replacement station '{ReplacedStation.TrackedObject.DisplayName}'. This indicates that the Google sheet was not parsed correctly!");
                return;
            }

            foreach (var wemId in wemIds)
            {
                ReplacedStation.TrackedObject.RemoveReplacedTrack(
                    _trackName,
                    wemId);
            }

            PopulateListView();
            TrackChanged?.Invoke(this, _trackName);
        }
    }
}
