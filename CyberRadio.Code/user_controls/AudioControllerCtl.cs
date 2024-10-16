using AetherUtils.Core.Logging;
using NAudio.Gui;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using RadioExt_Helper.models;
using RadioExt_Helper.Properties;

namespace RadioExt_Helper.user_controls;

public sealed partial class AudioControllerCtl : UserControl
{
    private readonly ImageList _stateImages = new();
    private WaveOutEvent? _waveOut;
    private MediaFoundationReader? _mediaReader;
    private SampleChannel? _sampleChannel;
    private List<Song> _playlist = [];
    private int _currentSongIndex = 0;
    private bool _isPlayingStream;
    private string _streamUrl = string.Empty;
    private bool _userStopped;

    // Event to notify the StationPreview form when a song ends.
    public event EventHandler? SongEnded;

    // Event to notify the StationPreview form when a playlist ends.
    public event EventHandler? PlaylistEnded;

    public AudioControllerCtl()
    {
        InitializeComponent();

        _stateImages.Images.Add("play", Resources.play);
        _stateImages.Images.Add("pause", Resources.pause);
        btnPlayPause.ImageList = _stateImages;
        btnPlayPause.ImageKey = "play";
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        //BackColor = Color.Transparent;

        volumeSlider.VolumeChanged += VolumeSlider_VolumeChanged;
    }

    /// <summary>
    /// Gets the currently playing song title, or the stream URL if a stream is playing.
    /// </summary>
    public string CurrentSongTitle { get; private set; }

    public bool IsPlaying => _waveOut is { PlaybackState: PlaybackState.Playing };

    /// <summary>
    /// Get the current state of the station.
    /// </summary>
    public StationState CurrentState { get; private set; } = StationState.Stopped;

    /// <summary>
    /// Sets the playlist for song playback.
    /// </summary>
    public void SetPlaylist(List<Song> playlist)
    {
        _playlist = playlist;
        _currentSongIndex = 0;
        _isPlayingStream = false;
    }

    /// <summary>
    /// Sets the stream URL for streaming playback.
    /// </summary>
    public void SetStreamUrl(string streamUrl)
    {
        _streamUrl = streamUrl;
        _isPlayingStream = true;
    }

    /// <summary>
    /// Starts playback of the current playlist or stream.
    /// </summary>
    public void StartPlayback()
    {
        if (_isPlayingStream)
        {
            PlayStream();
        }
        else
        {
            if (_playlist.Count > 0)
            {
                PlayCurrentSong();
            }
            else
            {
                MessageBox.Show("No songs in the playlist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// Stops playback.
    /// </summary>
    public void StopPlayback()
    {
        _waveOut?.Stop();
        _mediaReader?.Dispose();
        _mediaReader = null;
        _waveOut = null;
        waveformPainter.AddMax(0);
        btnPlayPause.ImageKey = "play";
        CurrentState = StationState.Stopped;
        _userStopped = true;
    }

    /// <summary>
    /// Pauses playback.
    /// </summary>
    public void PausePlayback()
    {
        _waveOut?.Pause();
        btnPlayPause.ImageKey = "play";
        CurrentState = StationState.Paused;
    }

    public void ResetPlaybackToBeginning()
    {
        _currentSongIndex = 0;
        StopPlayback();
    }

    /// <summary>
    /// Initializes the volume slider based on the station's volume setting (scaled 0-25 to 0.0f-1.0f).
    /// </summary>
    public void SetVolume(float stationVolume)
    {
        volumeSlider.Volume = ScaleVolumeToSlider(stationVolume);
        lblVolume.Text = string.Format(Strings.AudioControllerCtl_VolumeSlider_VolumeChanged_Volume___0_F1_, stationVolume);
    }

    private void VolumeSlider_VolumeChanged(object? sender, EventArgs e)
    {
        if (_waveOut == null) return;

        _waveOut.Volume = volumeSlider.Volume;  // Update WaveOut volume when slider changes
        lblVolume.Text = string.Format(Strings.AudioControllerCtl_VolumeSlider_VolumeChanged_Volume___0_F1_, GetNonScaledVolume(volumeSlider.Volume));
    }

    private void btnPlayPause_Click(object sender, EventArgs e)
    {
        if (_waveOut == null)
        {
            StartPlayback();
        }
        else
        {
            if (_waveOut.PlaybackState == PlaybackState.Playing)
            {
                PausePlayback();
            }
            else
            {
                _waveOut.Play();
                btnPlayPause.ImageKey = "pause";
            }
        }
    }

    private void PlayStream()
    {
        try
        {
            btnPlayPause.ImageKey = "pause";
            _mediaReader = new MediaFoundationReader(_streamUrl);
            _waveOut = new WaveOutEvent();
            _waveOut.Init(_mediaReader);
            _waveOut.Play();
            CurrentState = StationState.Playing;
            CurrentSongTitle = _streamUrl;
            _userStopped = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error streaming audio: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            StopPlayback();
        }
    }

    private void PlayCurrentSong()
    {
        if (_currentSongIndex < 0 || _currentSongIndex >= _playlist.Count) return;

        var currentSong = _playlist[_currentSongIndex];
        CurrentSongTitle = currentSong.Title;
        _userStopped = false;

        try
        {
            btnPlayPause.ImageKey = "pause";
            _mediaReader?.Dispose();
            _mediaReader = new MediaFoundationReader(currentSong.FilePath);
            _waveOut?.Dispose();
            _waveOut = new WaveOutEvent();

            // Set up sample channel for waveform visualization
            _sampleChannel = new SampleChannel(_mediaReader, true);
            var postVolumeMeter = new MeteringSampleProvider(_sampleChannel);
            postVolumeMeter.StreamVolume += OnPostVolumeMeter;

            _waveOut.Init(new SampleToWaveProvider(postVolumeMeter));
            _waveOut.Play();
            CurrentState = StationState.Playing;

            // Handle playback state change.
            _waveOut.PlaybackStopped += (s, e) =>
            {
                if (e.Exception != null)
                    AuLogger.GetCurrentLogger<AudioControllerCtl>("PlayCurrentSong").Error(e.Exception, "Error playing song.");

                if (_currentSongIndex < _playlist.Count & !_userStopped)
                {
                    // Move to the next song.
                    _currentSongIndex++;
                    SongEnded?.Invoke(this, EventArgs.Empty); // Notify that the song has ended.
                    PlayCurrentSong(); // Play the next song.
                }
                else
                {
                    // Notify that the playlist has ended.
                    CurrentState = StationState.Stopped;
                    PlaylistEnded?.Invoke(this, EventArgs.Empty);
                }
            };
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error playing song: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            StopPlayback();
        }
    }

    private void OnPostVolumeMeter(object? sender, StreamVolumeEventArgs e)
    {
        // Update the waveform painter with audio samples
        waveformPainter.AddMax(e.MaxSampleValues.Max());
    }

    private void btnPlayPause_MouseDown(object sender, MouseEventArgs e)
    {
        if (_waveOut == null)
        {
            btnPlayPause.ImageKey = "play_down";
            return;
        }

        btnPlayPause.ImageKey = _waveOut?.PlaybackState switch
        {
            PlaybackState.Playing => "pause_down",
            PlaybackState.Paused or PlaybackState.Stopped => "play_down",
            _ => btnPlayPause.ImageKey
        };
    }

    private void btnPlayPause_MouseHover(object sender, EventArgs e)
    {
        if (_waveOut == null)
        {
            btnPlayPause.ImageKey = "play_over";
            return;
        }

        btnPlayPause.ImageKey = _waveOut?.PlaybackState switch
        {
            PlaybackState.Playing => "pause_over",
            PlaybackState.Paused or PlaybackState.Stopped => "play_over",
            _ => btnPlayPause.ImageKey
        };
    }

    private void btnPlayPause_MouseLeave(object sender, EventArgs e)
    {
        if (_waveOut == null)
        {
            btnPlayPause.ImageKey = "play";
            return;
        }

        btnPlayPause.ImageKey = _waveOut?.PlaybackState switch
        {
            PlaybackState.Playing => "pause",
            PlaybackState.Paused or PlaybackState.Stopped => "play",
            _ => btnPlayPause.ImageKey
        };
    }

    /// <summary>
    /// Scales the station volume (0 - 25) to the VolumeSlider's range (0.0f - 1.0f).
    /// </summary>
    private float ScaleVolumeToSlider(float stationVolume)
    {
        const float maxStationVolume = 25.0f;
        return Math.Max(0.0f, Math.Min(1.0f, stationVolume / maxStationVolume));
    }

    private float GetNonScaledVolume(float scaledVolume)
    {
        const float maxStationVolume = 25.0f;
        return scaledVolume * maxStationVolume;
    }
}