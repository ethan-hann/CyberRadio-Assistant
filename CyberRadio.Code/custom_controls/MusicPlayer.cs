using AetherUtils.Core.Logging;
using NAudio.Wave;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.user_controls;

/// <summary>
///     A simple music player control that allows you to stream audio from a URL in real time.
/// </summary>
public partial class MusicPlayer : UserControl
{
    private readonly ImageList _stateImages = new();

    private MediaFoundationReader? _mediaReader;
    private string _streamUrl = string.Empty;
    private WaveOutEvent? _waveOut;

    public MusicPlayer()
    {
        InitializeComponent();

        _stateImages.Images.Add("play", Properties.Resources.play);
        _stateImages.Images.Add("play_down", Properties.Resources.play_down);
        _stateImages.Images.Add("play_over", Properties.Resources.play_over);
        _stateImages.Images.Add("pause", Properties.Resources.pause);
        _stateImages.Images.Add("pause_down", Properties.Resources.pause_down);
        _stateImages.Images.Add("pause_over", Properties.Resources.pause_over);
        _stateImages.ImageSize = new Size(32, 32);

        btnPlayPause.ImageList = _stateImages;

        btnPlayPause.ImageKey = "play";

        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
    }

    public string StreamUrl
    {
        get => _streamUrl;
        set
        {
            _streamUrl = value;
            _waveOut?.Stop();
            _waveOut = null;
            btnPlayPause.ImageKey = "play";
        }
    }

    ~MusicPlayer()
    {
        _waveOut?.Dispose();
        _mediaReader?.Dispose();
    }

    public void StopStream()
    {
        _waveOut?.Stop();
        _waveOut = null;
        btnPlayPause.ImageKey = "play";
    }

    private void btnPlayPause_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(StreamUrl))
        {
            var message = GlobalData.Strings.GetString("StreamURLError") ?? "Please enter a valid stream URL.";
            var error = GlobalData.Strings.GetString("Error") ?? "Error";
            MessageBox.Show(this, message, error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            AuLogger.GetCurrentLogger<MusicPlayer>("Btn_PlayPause").Error("Stream URL was invalid or empty.");
            return;
        }

        _waveOut ??= new WaveOutEvent();

        switch (_waveOut.PlaybackState)
        {
            case PlaybackState.Playing:
                PauseStream();
                break;
            case PlaybackState.Paused:
                ResumeStream();
                break;
            case PlaybackState.Stopped:
                PlayStream();
                break;
            default:
                throw new Exception("Playback state was not valid for the audio player.");
        }
    }

    private void PlayStream()
    {
        try
        {
            btnPlayPause.ImageKey = "pause";
            _mediaReader = new MediaFoundationReader(StreamUrl);
            _waveOut?.Init(_mediaReader);
            _waveOut?.Play();
        }
        catch (Exception ex)
        {
            var message = GlobalData.Strings.GetString("ErrorStreamingAudio") ?? "Error streaming audio: {0}";
            var error = GlobalData.Strings.GetString("Error") ?? "Error";
            MessageBox.Show(this, string.Format(message, ex.Message), error, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            AuLogger.GetCurrentLogger<MusicPlayer>("PlayStream").Error(ex, "Error streaming audio");
            btnPlayPause.ImageKey = "play";
        }
    }

    private void ResumeStream()
    {
        try
        {
            btnPlayPause.ImageKey = "pause";
            _waveOut?.Play();
        }
        catch (Exception ex)
        {
            var message = GlobalData.Strings.GetString("ErrorStreamingAudio") ?? "Error streaming audio: {0}";
            var error = GlobalData.Strings.GetString("Error") ?? "Error";
            MessageBox.Show(this, string.Format(message, ex.Message), error, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            AuLogger.GetCurrentLogger<MusicPlayer>("ResumeStream").Error(ex, "Error streaming audio");
            btnPlayPause.ImageKey = "play";
        }
    }

    private void PauseStream()
    {
        try
        {
            btnPlayPause.ImageKey = "play";
            _waveOut?.Pause();
        }
        catch (Exception ex)
        {
            var message = GlobalData.Strings.GetString("ErrorPausingStream") ?? "Error pausing stream: {0}";
            var error = GlobalData.Strings.GetString("Error") ?? "Error";
            MessageBox.Show(this, string.Format(message, ex.Message), error, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            AuLogger.GetCurrentLogger<MusicPlayer>("PauseStream").Error(ex, "Error pausing stream");
            btnPlayPause.ImageKey = "pause";
        }
    }

    private void btnPlayPause_MouseDown(object sender, MouseEventArgs e)
    {
        if (_waveOut == null)
        {
            btnPlayPause.ImageKey = "play_down";
            return;
        }

        if (_waveOut?.PlaybackState == PlaybackState.Playing)
        {
            btnPlayPause.ImageKey = "pause_down";
        }

        if (_waveOut?.PlaybackState == PlaybackState.Paused || _waveOut?.PlaybackState == PlaybackState.Stopped)
        {
            btnPlayPause.ImageKey = "play_down";
        }
    }

    private void btnPlayPause_MouseHover(object sender, EventArgs e)
    {
        if (_waveOut == null)
        {
            btnPlayPause.ImageKey = "play_over";
            return;
        }

        if (_waveOut?.PlaybackState == PlaybackState.Playing)
        {
            btnPlayPause.ImageKey = "pause_over";
        }

        if (_waveOut?.PlaybackState == PlaybackState.Paused || _waveOut?.PlaybackState == PlaybackState.Stopped)
        {
            btnPlayPause.ImageKey = "play_over";
        }
    }

    private void btnPlayPause_MouseLeave(object sender, EventArgs e)
    {
        if (_waveOut == null)
        {
            btnPlayPause.ImageKey = "play";
            return;
        }

        if (_waveOut?.PlaybackState == PlaybackState.Playing)
        {
            btnPlayPause.ImageKey = "pause";
        }

        if (_waveOut?.PlaybackState == PlaybackState.Paused || _waveOut?.PlaybackState == PlaybackState.Stopped)
        {
            btnPlayPause.ImageKey = "play";
        }
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        // Do not paint the background to keep it transparent
    }

    protected override void OnParentBackColorChanged(EventArgs e)
    {
        base.OnParentBackColorChanged(e);
        Invalidate(); // Invalidate to repaint with the updated parent background
    }
}