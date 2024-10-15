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
        private readonly List<Song> _trackList = new List<Song>(); // The list of songs for the station (ordered and unordered)
        private List<Song> _playlist = new List<Song>(); // The current playlist
        private bool _manualMode = false;  // Indicates whether the user is in manual mode (selected a song manually)
        private int _currentSongIndex = 0;  // Tracks the current song index

        public StationPreview()
        {
            InitializeComponent();
        }

        public StationPreview(TrackableObject<Station> station)
        {
            InitializeComponent();
            _station = station;
        }

        public void UpdateStation(TrackableObject<Station> station)
        {
            _station = station;

            // Update the station name and icon
            lblStationName.Text = _station?.TrackedObject.MetaData.DisplayName;
            Text = string.Format(Strings.StationPreviewTitle, station.TrackedObject.MetaData.DisplayName);
            SetPicBoxIcon();
            SetPlayerVolume(station.TrackedObject.MetaData.Volume);
        }

        private void StationPreview_Load(object sender, EventArgs e)
        {
            Translate();
            lblStationName.Text = _station?.TrackedObject.MetaData.DisplayName;
            SetPicBoxIcon();

            mediaPlayer.PlayStateChange += MediaPlayer_PlayStateChange;

            // Check if the station is a streaming station or song-based
            if (_station.TrackedObject.MetaData.StreamInfo.IsStream)
            {
                SetupTrackList();
                btnNormalize.Enabled = false;
                PlayStream(_station.TrackedObject.MetaData.StreamInfo.StreamUrl);
            }
            else
            {
                bgLoader.RunWorkerAsync();  // Load song playlist asynchronously
            }
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
                mediaPlayer.settings.volume = ScaleVolume(newVolume);
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<StationPreview>("SetPlayerVolume").Error(ex, "Error while trying to set the new player volume.");
            }
        }

        /// <summary>
        /// Scales the station volume (0 - 25) to the media player's volume range (0 - 100),
        /// while ensuring that a station volume of 1 maps to media player volume 16.
        /// </summary>
        private int ScaleVolume(float stationVolume)
        {
            stationVolume = Math.Max(0, Math.Min(25.0f, stationVolume));

            var baseScale = 100.0f / 25.0f;
            var bias = 16.0f - (baseScale * 1.0f);

            var mediaPlayerVolume = (stationVolume * baseScale) + bias;
            return (int)Math.Max(0, Math.Min(100.0f, mediaPlayerVolume));
        }

        private void MediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            try
            {
                var newState = (WMPPlayState)e.newState;

                if (newState == WMPPlayState.wmppsStopped || newState == WMPPlayState.wmppsMediaEnded)
                {
                    if (!_manualMode && _currentSongIndex < _playlist.Count - 1)
                    {
                        _currentSongIndex++;
                        PlayCurrentSong();  // Play the next song in the playlist
                    }
                    else if (!_manualMode)
                    {
                        // Restart the playlist when all songs are finished
                        _currentSongIndex = 0;
                        PlayCurrentSong();
                    }
                }
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<StationPreview>("MediaPlayer_PlayStateChange").Error(ex, "Error handling media player state change.");
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
            lbTracks.BeginUpdate();
            _trackList.Clear();

            if (_station.TrackedObject.MetaData.StreamInfo.IsStream)
            {
                lbTracks.Items.Add(Strings.StationPreviewListBoxStreaming);
                lbTracks.EndUpdate();
                return;
            }

            foreach (var song in _station.TrackedObject.Songs)
            {
                _trackList.Add((Song)song.Clone());
            }

            lbTracks.DataSource = _trackList;
            lbTracks.DisplayMember = "Title";
            lbTracks.EndUpdate();
        }

        private void bgLoader_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Invoke(SetupTrackList);
            _playlist = GetShuffledPlaylist(_station.TrackedObject.Songs, _station.TrackedObject.MetaData.SongOrder);
        }

        private void bgLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //not used
        }

        private void bgLoader_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            SetPlayerVolume(ScaleVolume(_station.TrackedObject.MetaData.Volume));
            PlayPlaylist();
        }

        private void PlayPlaylist()
        {
            _manualMode = false;  // Reset manual mode
            mediaPlayer.currentPlaylist.clear();  // Clear any previous playlist

            foreach (var song in _playlist)
            {
                var mediaItem = mediaPlayer.newMedia(song.FilePath);
                mediaPlayer.currentPlaylist.appendItem(mediaItem);
            }

            _currentSongIndex = 0;  // Start from the first song
            PlayCurrentSong();
        }

        private void PlayCurrentSong()
        {
            var currentSong = _playlist[_currentSongIndex];
            mediaPlayer.Ctlcontrols.stop();  // Stop before playing new song
            mediaPlayer.URL = currentSong.FilePath;

            Task.Delay(200).Wait();  // Wait to ensure the media player loads the file

            mediaPlayer.Ctlcontrols.play();  // Start playing the current song
            lblNowPlaying.Text = mediaPlayer.currentMedia?.name ?? currentSong.Title;
        }

        private void PlaySong(Song song)
        {
            _manualMode = true;  // Enable manual mode
            mediaPlayer.currentPlaylist.clear();  // Clear the playlist for manual song play
            var mediaItem = mediaPlayer.newMedia(song.FilePath);
            mediaPlayer.currentPlaylist.appendItem(mediaItem);

            mediaPlayer.Ctlcontrols.play();  // Play the selected song
            lblNowPlaying.Text = mediaPlayer.currentMedia?.name ?? song.Title;
        }

        private void PlayStream(string streamUrl)
        {
            if (!string.IsNullOrEmpty(streamUrl))
            {
                mediaPlayer.URL = streamUrl;  // Set the media player to play the stream
                lblNowPlaying.Text = streamUrl;
                mediaPlayer.Ctlcontrols.play();  // Start streaming
            }
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

        private void lbTracks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_manualMode) return;  // Prevent triggering manual play during playlist mode

            if (lbTracks.SelectedItem is Song selectedSong)
                PlaySong(selectedSong);  // Manual play when a song is selected from the listbox
        }

        private void btnPlayStation_Click(object sender, EventArgs e)
        {
            _playlist = GetShuffledPlaylist(_station.TrackedObject.Songs, _station.TrackedObject.MetaData.SongOrder);
            PlayPlaylist();  // Reset the playlist when Play Station Preview is clicked
        }

        private void picStationIcon_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingMode = CompositingMode.SourceOver;
        }

        private void StationPreview_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                mediaPlayer.Ctlcontrols.stop();
                mediaPlayer.Dispose();
                mediaPlayer = null;
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<StationPreview>("StationPreview_FormClosing").Error(ex, "Error closing the media player.");
            }
        }

        private void btnResetStationPreview_Click(object sender, EventArgs e)
        {
            _manualMode = false;
            _playlist = GetShuffledPlaylist(_station.TrackedObject.Songs, _station.TrackedObject.MetaData.SongOrder);
            PlayPlaylist();  // Reset and restart the playlist
        }

        private void btnNormalize_Click(object sender, EventArgs e)
        {
            // Normalize functionality (if applicable)
        }
    }
}