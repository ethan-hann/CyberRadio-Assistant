using AetherUtils.Core.Logging;
using NAudio.Wave;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.user_controls;

/// <summary>
///     A simple music player control that allows you to stream audio from a URL in real time.
/// </summary>
public partial class MusicPlayer : UserControl
{
    private MediaFoundationReader? _mediaReader;
    private string _streamUrl = string.Empty;
    private WaveOutEvent? _waveOut;

    public MusicPlayer()
    {
        InitializeComponent();
    }

    public string StreamUrl
    {
        get => _streamUrl;
        set
        {
            _streamUrl = value;
            _waveOut?.Stop();
            _waveOut = null;
            btnPlayPause.ImageIndex = 0;
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
        btnPlayPause.ImageIndex = 0;
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
            btnPlayPause.ImageIndex = 1;
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
            btnPlayPause.ImageIndex = 0;
        }
    }

    private void ResumeStream()
    {
        try
        {
            btnPlayPause.ImageIndex = 1;
            _waveOut?.Play();
        }
        catch (Exception ex)
        {
            var message = GlobalData.Strings.GetString("ErrorStreamingAudio") ?? "Error streaming audio: {0}";
            var error = GlobalData.Strings.GetString("Error") ?? "Error";
            MessageBox.Show(this, string.Format(message, ex.Message), error, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            AuLogger.GetCurrentLogger<MusicPlayer>("ResumeStream").Error(ex, "Error streaming audio");
            btnPlayPause.ImageIndex = 0;
        }
    }

    private void PauseStream()
    {
        try
        {
            btnPlayPause.ImageIndex = 0;
            _waveOut?.Pause();
        }
        catch (Exception ex)
        {
            var message = GlobalData.Strings.GetString("ErrorPausingStream") ?? "Error pausing stream: {0}";
            var error = GlobalData.Strings.GetString("Error") ?? "Error";
            MessageBox.Show(this, string.Format(message, ex.Message), error, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            AuLogger.GetCurrentLogger<MusicPlayer>("PauseStream").Error(ex, "Error pausing stream");
            btnPlayPause.ImageIndex = 1;
        }
    }
}