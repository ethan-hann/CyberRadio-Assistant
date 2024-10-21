namespace RadioExt_Helper.forms
{
    partial class SplashScreen
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            lblSplashStatus = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            lblVersion = new Label();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, lblSplashStatus, toolStripStatusLabel2 });
            statusStrip1.Location = new Point(0, 336);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(584, 25);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(259, 20);
            toolStripStatusLabel1.Spring = true;
            // 
            // lblSplashStatus
            // 
            lblSplashStatus.Font = new Font("Segoe UI Variable Text Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSplashStatus.ForeColor = Color.FromArgb(224, 224, 224);
            lblSplashStatus.Name = "lblSplashStatus";
            lblSplashStatus.Size = new Size(50, 20);
            lblSplashStatus.Text = "Status";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(259, 20);
            toolStripStatusLabel2.Spring = true;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.BackColor = Color.Transparent;
            lblVersion.Font = new Font("Segoe UI Variable Display", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVersion.ForeColor = Color.Silver;
            lblVersion.ImageAlign = ContentAlignment.MiddleLeft;
            lblVersion.Location = new Point(23, 4);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(52, 26);
            lblVersion.TabIndex = 2;
            lblVersion.Text = "1.0.0";
            lblVersion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SplashScreen
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            BackgroundImage = Properties.Resources.CRA_splash;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(584, 361);
            ControlBox = false;
            Controls.Add(lblVersion);
            Controls.Add(statusStrip1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SplashScreen";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Splash";
            Load += SplashScreen_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblSplashStatus;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private Label lblVersion;
    }
}