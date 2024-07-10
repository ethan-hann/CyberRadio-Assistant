// MusicPlayer.cs : RadioExt-Helper
// Copyright (C) 2024  Ethan Hann
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

using AetherUtils.Core.Logging;
using NAudio.Wave;
using RadioExt_Helper.Properties;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.custom_controls;

/// <summary>
///     A simple music player control that allows streaming audio from a URL in real time.
/// </summary>
public sealed partial class MusicPlayer : UserControl
{
    private readonly ImageList _stateImages = new();

    private MediaFoundationReader? _mediaReader;
    private string _streamUrl = string.Empty;
    private WaveOutEvent? _waveOut;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MusicPlayer" /> class.
    /// </summary>
    public MusicPlayer()
    {
        InitializeComponent();

        _stateImages.Images.Add("play", Resources.play);
        _stateImages.Images.Add("play_down", Resources.play_down);
        _stateImages.Images.Add("play_over", Resources.play_over);
        _stateImages.Images.Add("pause", Resources.pause);
        _stateImages.Images.Add("pause_down", Resources.pause_down);
        _stateImages.Images.Add("pause_over", Resources.pause_over);
        _stateImages.ImageSize = new Size(32, 32);

        btnPlayPause.ImageList = _stateImages;

        btnPlayPause.ImageKey = "play";

        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
    }

    /// <summary>
    ///     Gets or sets the stream URL. When setting, if the stream is currently playing, it will stop the stream.
    /// </summary>
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

    /// <summary>
    ///     Stops the audio stream.
    /// </summary>
    public void StopStream()
    {
        _waveOut?.Stop();
        _waveOut = null;
        btnPlayPause.ImageKey = "play";
    }

    /// <summary>
    ///     Handles the click event of the play/pause button.
    /// </summary>
    /// <param name="sender">The sender object.</param>
    /// <param name="e">The event arguments.</param>
    private void BtnPlayPause_Click(object sender, EventArgs e)
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
                AuLogger.GetCurrentLogger<MusicPlayer>("Btn_PlayPause")
                    .Error("Playback state was not valid for the audio player.");
                break;
        }
    }

    /// <summary>
    ///     Plays the audio stream.
    /// </summary>
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

    /// <summary>
    ///     Resumes the audio stream.
    /// </summary>
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

    /// <summary>
    ///     Pauses the audio stream.
    /// </summary>
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

    /// <summary>
    ///     Handles the mouse down event of the play/pause button.
    /// </summary>
    /// <param name="sender">The sender object.</param>
    /// <param name="e">The mouse event arguments.</param>
    private void BtnPlayPause_MouseDown(object sender, MouseEventArgs e)
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

    /// <summary>
    ///     Handles the mouse hover event of the play/pause button.
    /// </summary>
    /// <param name="sender">The sender object.</param>
    /// <param name="e">The event arguments.</param>
    private void BtnPlayPause_MouseHover(object sender, EventArgs e)
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

    /// <summary>
    ///     Handles the mouse leave event of the play/pause button.
    /// </summary>
    /// <param name="sender">The sender object.</param>
    /// <param name="e">The event arguments.</param>
    private void BtnPlayPause_MouseLeave(object sender, EventArgs e)
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
    ///     Overrides the OnPaintBackground method to prevent painting the background.
    /// </summary>
    /// <param name="e">The paint event arguments.</param>
    protected override void OnPaintBackground(PaintEventArgs e)
    {
        // Do not paint the background to keep it transparent
    }

    /// <summary>
    ///     Overrides the OnParentBackColorChanged method to update the parent background.
    /// </summary>
    /// <param name="e">The event arguments.</param>
    protected override void OnParentBackColorChanged(EventArgs e)
    {
        base.OnParentBackColorChanged(e);
        Invalidate(); // Invalidate to repaint with the updated parent background
    }
}