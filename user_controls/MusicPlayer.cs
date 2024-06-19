using NAudio.Wave;
using RadioExt_Helper.utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadioExt_Helper.user_controls
{
    public partial class MusicPlayer : UserControl, IDisposable
    {
        private IWavePlayer? waveOut;
        private MediaFoundationReader mediaReader;

        private string _streamURL = string.Empty;
        public string StreamURL { 
            get
            {
                return _streamURL;
            }
            set
            {
                _streamURL = value;
                waveOut?.Stop();
                waveOut = null;
                btnPlayPause.ImageIndex = 0;
            }
        }

        public MusicPlayer()
        {
            InitializeComponent();
        }

        ~MusicPlayer()
        {
            waveOut?.Dispose();
            mediaReader?.Dispose();
        }

        public void StopStream()
        {
            waveOut?.Stop();
            waveOut = null;
            btnPlayPause.ImageIndex = 0;
        }

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(StreamURL))
            {
                var message = GlobalData.Strings.GetString("StreamURLError") ?? "Please enter a valid stream URL.";
                var error = GlobalData.Strings.GetString("Error") ?? "Error";
                MessageBox.Show(this, message, error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (waveOut == null)
                waveOut = new WaveOutEvent();

            switch (waveOut.PlaybackState)
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
            }
        }

        private void PlayStream()
        {
            try
            {
                btnPlayPause.ImageIndex = 1;
                mediaReader = new MediaFoundationReader(StreamURL);
                waveOut?.Init(mediaReader);
                waveOut?.Play();
            } catch (Exception ex)
            {
                var message = GlobalData.Strings.GetString("ErrorStreamingAudio") ?? "Error streaming audio: {0}";
                var error = GlobalData.Strings.GetString("Error") ?? "Error";
                MessageBox.Show(this, string.Format(message, ex.Message), error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnPlayPause.ImageIndex = 0;
            }
        }

        private void ResumeStream()
        {
            try
            {
                btnPlayPause.ImageIndex = 1;
                //mediaReader = new MediaFoundationReader(StreamURL);
                waveOut?.Play();
            }
            catch (Exception ex)
            {
                var message = GlobalData.Strings.GetString("ErrorStreamingAudio") ?? "Error streaming audio: {0}";
                var error = GlobalData.Strings.GetString("Error") ?? "Error";
                MessageBox.Show(this, string.Format(message, ex.Message), error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnPlayPause.ImageIndex = 0;
            }
        }

        private void PauseStream()
        {
            try
            {
                btnPlayPause.ImageIndex = 0;
                waveOut?.Pause();
            }
            catch (Exception ex)
            {
                var message = GlobalData.Strings.GetString("ErrorPausingStream") ?? "Error pausing stream: {0}";
                var error = GlobalData.Strings.GetString("Error") ?? "Error";
                MessageBox.Show(this, string.Format(message, ex.Message), error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnPlayPause.ImageIndex = 1;
            }
        }
    }
}
