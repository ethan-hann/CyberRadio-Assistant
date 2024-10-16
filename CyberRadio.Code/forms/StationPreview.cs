using RadioExt_Helper.models;
using System.Drawing.Drawing2D;
using AetherUtils.Core.Logging;
using WMPLib;
using System.ComponentModel;

namespace RadioExt_Helper.forms
{
    public partial class StationPreview : Form
    {
        public TrackableObject<Station> Station
        {
            get => _station;
            set => UpdateStation(value);
        }

        private TrackableObject<Station> _station;
        private readonly List<Song> _trackList = []; // The list of songs for the station (ordered and unordered)
        private List<Song> _playlist = []; // The current playlist

        private bool _userStoppedStation;
        private bool _isClosing;

        public StationPreview()
        {
            InitializeComponent();
        }

        public StationPreview(TrackableObject<Station> station)
        {
            InitializeComponent();
            _station = station;
        }

        private void StationPreview_Load(object sender, EventArgs e)
        {
            Translate();
            lblStationName.Text = _station?.TrackedObject.MetaData.DisplayName;
            SetPicBoxIcon();
            InitializePreview();
        }

        private void InitializePreview()
        {
            //If the station is currently playing, stop it and reset it.
            if (audioController.CurrentState is StationState.Playing or StationState.Paused)
            {
                _userStoppedStation = true;
                audioController.StopPlayback();
            }

            // Check if the station is a streaming station or song-based
            RefreshPlaylistOrStream();
            StartPlaylistOrStream();
            //if (_station.TrackedObject.MetaData.StreamInfo.IsStream)
            //{
            //    _playlist.Clear();
            //    audioController.SetStreamUrl(_station.TrackedObject.MetaData.StreamInfo.StreamUrl);
            //    audioController.StartPlayback();
            //    ToggleButtons();
            //}
            //else
            //{
            //    bgLoader.RunWorkerAsync();  // Load song playlist asynchronously
            //}
        }

        private void ToggleButtons()
        {
            Invoke(() =>
            {
                switch (audioController.CurrentState)
                {
                    case StationState.Playing:
                        btnPlayStation.Enabled = false;
                        btnResetStationPreview.Enabled = !_station.TrackedObject.MetaData.StreamInfo.IsStream;
                        btnStopStation.Enabled = true;
                        break;
                    case StationState.Paused:
                        btnPlayStation.Enabled = true;
                        btnResetStationPreview.Enabled = !_station.TrackedObject.MetaData.StreamInfo.IsStream;
                        btnStopStation.Enabled = true;
                        break;
                    case StationState.Stopped:
                        btnPlayStation.Enabled = true;
                        btnResetStationPreview.Enabled = !_station.TrackedObject.MetaData.StreamInfo.IsStream;
                        btnStopStation.Enabled = false;
                        break;
                }

                btnNormalize.Enabled =
                    !_station.TrackedObject.MetaData.StreamInfo
                        .IsStream; // Disable normalize button for streaming stations
            });
        }

        public void UpdateStation(TrackableObject<Station> station)
        {
            _station = station;

            // Update the station name and icon
            lblStationName.Text = _station?.TrackedObject.MetaData.DisplayName;
            Text = string.Format(Strings.StationPreviewTitle, _station.TrackedObject.MetaData.DisplayName);
            SetPicBoxIcon();
            SetPlayerVolume(_station.TrackedObject.MetaData.Volume);
            ToggleButtons();
        }

        private void SetPicBoxIcon()
        {
            picStationIcon.Image = null;
            if (_station!.TrackedObject.CheckActiveIconValid())
            {
                var icon = _station.TrackedObject.GetActiveIcon()?.TrackedObject;
                if (icon?.ImagePath != null)
                {
                    picStationIcon.Load(icon.ImagePath);
                }
            }
        }

        private void SetPlayerVolume(float newVolume)
        {
            try
            {
                audioController.SetVolume(newVolume);
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<StationPreview>("SetPlayerVolume").Error(ex, "Error while trying to set the new player volume.");
            }
        }

        private void Translate()
        {
            Text = string.Format(Strings.StationPreviewTitle, _station.TrackedObject.MetaData.DisplayName);
        }

        /// <summary>
        /// Adds the station's songs to the track listbox.
        /// </summary>
        private void SetupTrackList()
        {

        }

        private void RefreshPlaylistOrStream()
        {
            lblNowPlaying.Text = "Loading preview...";
            SetupTrackList();

            audioController.SetVolume(_station.TrackedObject.MetaData.Volume);

            if (_station.TrackedObject.MetaData.StreamInfo.IsStream)
            {
                audioController.SetPlaylist([]);
                audioController.SetStreamUrl(_station.TrackedObject.MetaData.StreamInfo.StreamUrl);
            }
            else
            {
                _playlist = GetShuffledPlaylist(_station.TrackedObject.Songs, _station.TrackedObject.MetaData.SongOrder);
                audioController.SetPlaylist(_playlist);
            }
        }

        private void StartPlaylistOrStream()
        {
            audioController.StartPlayback();
            lblNowPlaying.Text = audioController.CurrentSongTitle;
            ToggleButtons();
            audioController.PlaylistEnded += audioController_PlaylistEnded; //resubscribe to playlist ended event.
        }

        private static List<Song> GetShuffledPlaylist(List<Song> allSongs, List<string> orderedTitles)
        {
            var orderedSongs = new List<Song>();
            var unorderedSongs = new List<Song>();

            foreach (var song in allSongs)
            {
                if (orderedTitles.Contains(song.Title))
                {
                    orderedSongs.Add(song); // Ordered songs
                }
                else
                {
                    unorderedSongs.Add(song); // Unordered songs
                }
            }

            var rng = new Random();
            unorderedSongs = unorderedSongs.OrderBy(_ => rng.Next()).ToList();

            var finalPlaylist = new List<Song>();
            finalPlaylist.AddRange(orderedSongs);  // Add ordered songs first
            finalPlaylist.AddRange(unorderedSongs);  // Then add shuffled unordered songs

            return finalPlaylist;
        }

        private void picStationIcon_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingMode = CompositingMode.SourceOver;
        }

        private void StationPreview_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _userStoppedStation = true;
                audioController.StopPlayback();
                audioController.Dispose();
                audioController.PlaylistEnded -= audioController_PlaylistEnded;
                _isClosing = true;
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<StationPreview>("StationPreview_FormClosing").Error(ex, "Error closing the media player.");
            }
        }

        private void btnPlayStation_Click(object sender, EventArgs e)
        {
            PlayStation();
        }

        private void PlayStation()
        {
            if (audioController.IsPlaying)
            {
                audioController.PlaylistEnded -= audioController_PlaylistEnded;
                _userStoppedStation = true;
                audioController.StopPlayback();
            }

            audioController.ResetPlaybackToBeginning();
            StartPlaylistOrStream(); //Start playback from beginning of playlist; do not re-shuffle the playlist
            ToggleButtons();
        }

        private void btnResetStationPreview_Click(object sender, EventArgs e)
        {
            ReshuffleStation();
        }

        private void ReshuffleStation()
        {
            if (audioController.IsPlaying)
            {
                audioController.PlaylistEnded -= audioController_PlaylistEnded;
                _userStoppedStation = true;
                audioController.StopPlayback();
            }

            audioController.ResetPlaybackToBeginning();
            RefreshPlaylistOrStream(); //Clear the playlist and get a new shuffled playlist
            StartPlaylistOrStream();
            ToggleButtons();
        }

        private void btnNormalize_Click(object sender, EventArgs e)
        {
            // Normalize functionality (if applicable)
        }

        private void btnStopStation_Click(object sender, EventArgs e)
        {
            StopStation();
        }

        private void StopStation()
        {
            if (audioController.IsPlaying)
            {
                audioController.PlaylistEnded -= audioController_PlaylistEnded;
                _userStoppedStation = true;
                audioController.StopPlayback();
            }

            audioController.ResetPlaybackToBeginning();
            ToggleButtons();
        }

        private void audioController_PlaylistEnded(object? sender, EventArgs e)
        {
            if (_isClosing)
                return;

            if (_userStoppedStation)
            {
                _userStoppedStation = false;
                ToggleButtons();
                return;
            }

            RefreshPlaylistOrStream();
            StartPlaylistOrStream();
        }

        private void audioController_SongEnded(object sender, EventArgs e)
        {
            lblNowPlaying.Text = audioController.CurrentSongTitle;
        }
    }
}