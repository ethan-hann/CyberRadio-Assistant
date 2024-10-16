namespace RadioExt_Helper.user_controls
{
    sealed partial class AudioControllerCtl
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
            tableLayoutPanel1 = new TableLayoutPanel();
            btnPlayPause = new custom_controls.RoundedPictureBox();
            volumeSlider = new NAudio.Gui.VolumeSlider();
            lblVolume = new Label();
            waveformPainter = new NAudio.Gui.WaveformPainter();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnPlayPause).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 48F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 496F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
            tableLayoutPanel1.Controls.Add(btnPlayPause, 0, 0);
            tableLayoutPanel1.Controls.Add(volumeSlider, 1, 0);
            tableLayoutPanel1.Controls.Add(lblVolume, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 143);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(652, 48);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnPlayPause
            // 
            btnPlayPause.BackColor = Color.White;
            btnPlayPause.BorderColor = Color.Transparent;
            btnPlayPause.BorderRadius = 20;
            btnPlayPause.BorderWidth = 2;
            btnPlayPause.Dock = DockStyle.Fill;
            btnPlayPause.Image = Properties.Resources.play;
            btnPlayPause.ImageKey = "";
            btnPlayPause.ImageList = null;
            btnPlayPause.IncludeBorder = true;
            btnPlayPause.Location = new Point(3, 3);
            btnPlayPause.Name = "btnPlayPause";
            btnPlayPause.Size = new Size(42, 42);
            btnPlayPause.TabIndex = 0;
            btnPlayPause.TabStop = false;
            btnPlayPause.Click += btnPlayPause_Click;
            btnPlayPause.MouseDown += btnPlayPause_MouseDown;
            btnPlayPause.MouseLeave += btnPlayPause_MouseLeave;
            btnPlayPause.MouseHover += btnPlayPause_MouseHover;
            // 
            // volumeSlider
            // 
            volumeSlider.Dock = DockStyle.Fill;
            volumeSlider.Location = new Point(51, 3);
            volumeSlider.Name = "volumeSlider";
            volumeSlider.Size = new Size(490, 42);
            volumeSlider.TabIndex = 2;
            // 
            // lblVolume
            // 
            lblVolume.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblVolume.AutoSize = true;
            lblVolume.Font = new Font("Segoe UI Variable Display", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVolume.Location = new Point(547, 13);
            lblVolume.Name = "lblVolume";
            lblVolume.Size = new Size(102, 21);
            lblVolume.TabIndex = 3;
            lblVolume.Text = "Volume: {0}";
            // 
            // waveformPainter
            // 
            waveformPainter.BackColor = Color.Black;
            waveformPainter.Dock = DockStyle.Fill;
            waveformPainter.ForeColor = Color.Green;
            waveformPainter.Location = new Point(0, 0);
            waveformPainter.Name = "waveformPainter";
            waveformPainter.Size = new Size(652, 143);
            waveformPainter.TabIndex = 1;
            waveformPainter.Text = "waveformPainter1";
            // 
            // AudioControllerCtl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(waveformPainter);
            Controls.Add(tableLayoutPanel1);
            Name = "AudioControllerCtl";
            Size = new Size(652, 191);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)btnPlayPause).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private custom_controls.RoundedPictureBox btnPlayPause;
        private NAudio.Gui.VolumeSlider volumeSlider;
        private Label lblVolume;
        private NAudio.Gui.WaveformPainter waveformPainter;
    }
}
