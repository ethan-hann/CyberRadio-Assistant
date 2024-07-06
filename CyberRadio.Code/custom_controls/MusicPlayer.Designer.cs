using RadioExt_Helper.custom_controls;

namespace RadioExt_Helper.user_controls
{
    partial class MusicPlayer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MusicPlayer));
            btnPlayPause = new RoundedPictureBox();
            ((System.ComponentModel.ISupportInitialize)btnPlayPause).BeginInit();
            SuspendLayout();
            // 
            // btnPlayPause
            // 
            btnPlayPause.BackColor = Color.Transparent;
            btnPlayPause.BackgroundImageLayout = ImageLayout.None;
            btnPlayPause.BorderColor = Color.Transparent;
            btnPlayPause.BorderRadius = 0;
            btnPlayPause.BorderWidth = 0;
            btnPlayPause.Dock = DockStyle.Fill;
            btnPlayPause.Image = (Image)resources.GetObject("btnPlayPause.Image");
            btnPlayPause.ImageKey = "play";
            btnPlayPause.IncludeBorder = false;
            btnPlayPause.Location = new Point(0, 0);
            btnPlayPause.Name = "btnPlayPause";
            btnPlayPause.Size = new Size(32, 32);
            btnPlayPause.TabIndex = 5;
            btnPlayPause.TabStop = false;
            btnPlayPause.Click += btnPlayPause_Click;
            btnPlayPause.MouseDown += btnPlayPause_MouseDown;
            btnPlayPause.MouseLeave += btnPlayPause_MouseLeave;
            btnPlayPause.MouseHover += btnPlayPause_MouseHover;
            // 
            // MusicPlayer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(btnPlayPause);
            ForeColor = Color.Transparent;
            Name = "MusicPlayer";
            Size = new Size(32, 32);
            ((System.ComponentModel.ISupportInitialize)btnPlayPause).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private RoundedPictureBox btnPlayPause;
    }
}
