using RadioExt_Helper.models;
using System.Drawing.Drawing2D;
using AetherUtils.Core.Logging;
using WMPLib;

namespace RadioExt_Helper.forms;

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
    private int _currentSongIndex;  // The index of the currently playing song
    private bool isManualPlayMode = false;  // Tracks if we're in manual play mode (user-selected song)
    private bool isProgrammaticSelection = false;  // Tracks if selection is changed programmatically


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

        //Update the station name and icon
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

        // Check if station is a streaming station or song-based
        if (_station.TrackedObject.MetaData.StreamInfo.IsStream)
        {
            SetupTrackList();
            btnNormalize.Enabled = false;

            PlayStream(_station.TrackedObject.MetaData.StreamInfo.StreamUrl);
        }
        else
        {
            bgLoader.RunWorkerAsync();  // Loads song playlist asynchronously
        }
    }

    private void SetPicBoxIcon()
    {
        picStationIcon.Image = null;
        if (_station!.TrackedObject.CheckActiveIconValid())
        {
            var icon = _station.TrackedObject.GetActiveIcon()?.TrackedObject;

            if (icon?.ImagePath == null)
                return;

            picStationIcon.Load(icon.ImagePath);
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
    /// while ensuring that a station volume of 1 maps to media player volume 16. This value seems to be a good volume.
    /// </summary>
    /// <param name="stationVolume">The volume level of the station (between 0 and 25).</param>
    /// <returns>The corresponding volume for the media player (between 0 and 100).</returns>
    private int ScaleVolume(float stationVolume)
    {
        // Clamp the station volume to be between 0 and 25
        stationVolume = Math.Max(0, Math.Min(25.0f, stationVolume));

        // Use a linear scaling with a bias so that station volume of 1 maps to media player volume 16
        var baseScale = 100.0f / 25.0f;  // Scaling from 0-25 to 0-100
        var bias = 16.0f - (baseScale * 1.0f);  // Adjust to ensure 1 maps to 16

        // Apply the scaling and bias
        var mediaPlayerVolume = (stationVolume * baseScale) + bias;

        // Clamp the final result to the valid media player volume range (0 to 100)
        return (int)Math.Max(0, Math.Min(100.0f, mediaPlayerVolume));
    }

    private void MediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
    {
        try
        {
            var newState = (WMPPlayState)e.newState;

            switch (newState)
            {
                case WMPPlayState.wmppsStopped or WMPPlayState.wmppsMediaEnded:
                    {
                        if (isManualPlayMode)
                        {
                            // Do nothing in manual mode; user must manually select the next song
                            return;
                        }

                        _currentSongIndex++;

                        if (_currentSongIndex < _playlist.Count)
                        {
                            PlayCurrentSong();
                        }
                        else
                        {
                            if (!_station.TrackedObject.MetaData.StreamInfo.IsStream)
                            {
                                // Reshuffle the playlist and restart if not a stream
                                _playlist = GetShuffledPlaylist(_station.TrackedObject.Songs, _station.TrackedObject.MetaData.SongOrder);
                                PlayCurrentSong();
                            }
                        }
                        break;
                    }
                // In case it doesn't play, retry
                case WMPPlayState.wmppsReady:
                    mediaPlayer.Ctlcontrols.play();
                    break;
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationPreview>("MediaPlayer_PlayStateChange").Error(ex, "Error handling media player state change.");
        }
    }

    private void Translate()
    {
        //TODO: add translations
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
            lbTracks.DataSource = null;
            lbTracks.Items.Add(Strings.StationPreviewListBoxStreaming);
            return; // No track list for streams
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

    private void bgLoader_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
    {

    }

    private void bgLoader_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
    {
        SetPlayerVolume(ScaleVolume(_station.TrackedObject.MetaData.Volume));
        PlayPlaylist();
    }

    private void PlayPlaylist()
    {
        _currentSongIndex = 0;  // Start at the first song
        PlayCurrentSong();
    }

    private void PlayCurrentSong()
    {
        if (_currentSongIndex < 0 || _currentSongIndex >= _playlist.Count) return;

        var currentSong = _playlist[_currentSongIndex];

        mediaPlayer.Ctlcontrols.stop();  // Stop the player before setting a new URL
        mediaPlayer.URL = currentSong.FilePath;

        // Delay to ensure the media player has time to load the file
        Task.Delay(200).Wait();

        if (!string.IsNullOrEmpty(mediaPlayer.URL))
        {
            mediaPlayer.Ctlcontrols.play();  // Play the song
            lblNowPlaying.Text = mediaPlayer.currentMedia.name;

            // Select the current song in the ListBox
            isProgrammaticSelection = true;  // Temporarily disable event handling
            lbTracks.SelectedItem = currentSong;  // Set the selected item to the current song
            isProgrammaticSelection = false;  // Re-enable event handling
        }
        else
        {
            AuLogger.GetCurrentLogger<StationPreview>("PlayCurrentSong").Error("Error playing song: URL is empty.");
        }
    }

    private void PlayStream(string streamUrl)
    {
        if (!string.IsNullOrEmpty(streamUrl))
        {
            mediaPlayer.URL = streamUrl;  // Set the media player to play the stream
            lblNowPlaying.Text = streamUrl;
            mediaPlayer.Ctlcontrols.play();  // Start streaming
        }
        else
        {
            AuLogger.GetCurrentLogger<StationPreview>("PlayStream").Error("Error playing stream: URL is empty.");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="allSongs"></param>
    /// <param name="orderedTitles"></param>
    /// <returns></returns>
    private static List<Song> GetShuffledPlaylist(List<Song> allSongs, List<string> orderedTitles)
    {
        // Separate the songs into ordered and unordered lists
        var orderedSongs = new List<Song>();
        var unorderedSongs = new List<Song>();

        // Iterate through all songs and group them into ordered or unordered
        foreach (var song in allSongs)
        {
            if (orderedTitles.Contains(song.Title))
            {
                orderedSongs.Add(song); // Song is part of the order
            }
            else
            {
                unorderedSongs.Add(song); // Song is not part of the order
            }
        }

        // Shuffle the unordered songs
        var rng = new Random();
        unorderedSongs = unorderedSongs.OrderBy(_ => rng.Next()).ToList();

        // Final playlist will have ordered songs first and then the shuffled unordered songs
        var finalPlaylist = new List<Song>();

        // Add ordered songs to the final playlist in their defined order
        foreach (var title in orderedTitles)
        {
            var orderedSong = orderedSongs.FirstOrDefault(song => song.Title == title);
            if (orderedSong != null)
            {
                finalPlaylist.Add(orderedSong);
            }
        }

        // Add shuffled unordered songs after the ordered songs
        finalPlaylist.AddRange(unorderedSongs);

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
            //Cleanup the media player
            mediaPlayer.Ctlcontrols.stop();
            mediaPlayer.Dispose();
            mediaPlayer = null;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<StationPreview>("StationPreview_FormClosing").Error(ex, "Error closing the media player.");
        }
    }

    private void lbTracks_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Only trigger manual play mode if the selection is user-initiated
        if (isProgrammaticSelection) return;

        if (lbTracks.SelectedItem is Song selectedSong)
        {
            PlaySong(selectedSong);
        }
    }

    private void PlaySong(Song song)
    {
        if (song == null || string.IsNullOrEmpty(song.FilePath))
        {
            MessageBox.Show(this, "Song file path is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Switch to manual play mode when a song is selected from the list
        isManualPlayMode = true;

        mediaPlayer.Ctlcontrols.stop();  // Stop the player before setting a new URL
        mediaPlayer.URL = song.FilePath;

        // Delay to ensure the media player has time to load the file
        Task.Delay(200).Wait();

        mediaPlayer.Ctlcontrols.play();  // Play the song
    }

    private void btnPlayStation_Click(object sender, EventArgs e)
    {
        // Reset the play mode to playlist mode
        isManualPlayMode = false;

        // Reshuffle and restart the playlist
        _playlist.Clear();
        _playlist = GetShuffledPlaylist(_station.TrackedObject.Songs, _station.TrackedObject.MetaData.SongOrder);
        PlayPlaylist();
    }

    private void btnResetStationPreview_Click(object sender, EventArgs e)
    {
        // Reset the play mode to playlist mode
        isManualPlayMode = false;

        // Reshuffle and restart the playlist
        _playlist.Clear();
        _playlist = GetShuffledPlaylist(_station.TrackedObject.Songs, _station.TrackedObject.MetaData.SongOrder);
        PlayPlaylist();
    }

    private void btnNormalize_Click(object sender, EventArgs e)
    {

    }
}