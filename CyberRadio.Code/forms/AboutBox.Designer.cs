namespace RadioExt_Helper.forms
{
    partial class AboutBox
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
            pictureBox1 = new PictureBox();
            lblAppName = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            lnkLicense = new LinkLabel();
            lblVersion = new Label();
            lblSpecialThanks2 = new Label();
            lnkRadioExtDev = new LinkLabel();
            lblSpecialThanks1 = new Label();
            lblAboutInfo = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblCopyright = new Label();
            lnkGithubRepo = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Left;
            pictureBox1.Image = Properties.Resources.cyber_radio_assistant;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(124, 181);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // lblAppName
            // 
            lblAppName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblAppName.AutoSize = true;
            lblAppName.Font = new Font("Segoe UI Variable Display", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAppName.Location = new Point(3, 3);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(366, 21);
            lblAppName.TabIndex = 1;
            lblAppName.Text = "Cyber Radio Assistant";
            lblAppName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.7676773F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 2);
            tableLayoutPanel1.Controls.Add(lblAboutInfo, 0, 1);
            tableLayoutPanel1.Controls.Add(lblAppName, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(124, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 39.5833321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 60.4166679F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 79F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tableLayoutPanel1.Size = new Size(372, 181);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Controls.Add(lnkLicense);
            panel1.Controls.Add(lblVersion);
            panel1.Controls.Add(lblSpecialThanks2);
            panel1.Controls.Add(lnkRadioExtDev);
            panel1.Controls.Add(lblSpecialThanks1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 75);
            panel1.Name = "panel1";
            panel1.Size = new Size(366, 73);
            panel1.TabIndex = 3;
            // 
            // lnkLicense
            // 
            lnkLicense.AutoSize = true;
            lnkLicense.Location = new Point(319, 27);
            lnkLicense.Name = "lnkLicense";
            lnkLicense.Size = new Size(46, 15);
            lnkLicense.TabIndex = 7;
            lnkLicense.TabStop = true;
            lnkLicense.Text = "License";
            lnkLicense.LinkClicked += lnkLicense_LinkClicked;
            // 
            // lblVersion
            // 
            lblVersion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblVersion.AutoSize = true;
            lblVersion.Location = new Point(281, 48);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(63, 15);
            lblVersion.TabIndex = 6;
            lblVersion.Text = "Version 1.0";
            // 
            // lblSpecialThanks2
            // 
            lblSpecialThanks2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblSpecialThanks2.AutoSize = true;
            lblSpecialThanks2.Location = new Point(3, 48);
            lblSpecialThanks2.Name = "lblSpecialThanks2";
            lblSpecialThanks2.Size = new Size(172, 15);
            lblSpecialThanks2.TabIndex = 5;
            lblSpecialThanks2.Text = "for the awesome radioExt mod!";
            lblSpecialThanks2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lnkRadioExtDev
            // 
            lnkRadioExtDev.AutoSize = true;
            lnkRadioExtDev.Location = new Point(3, 27);
            lnkRadioExtDev.Name = "lnkRadioExtDev";
            lnkRadioExtDev.Size = new Size(166, 15);
            lnkRadioExtDev.TabIndex = 4;
            lnkRadioExtDev.TabStop = true;
            lnkRadioExtDev.Text = "justarandomguyontheinternet";
            lnkRadioExtDev.LinkClicked += lnkRadioExtDev_LinkClicked;
            // 
            // lblSpecialThanks1
            // 
            lblSpecialThanks1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblSpecialThanks1.AutoSize = true;
            lblSpecialThanks1.Location = new Point(3, 12);
            lblSpecialThanks1.Name = "lblSpecialThanks1";
            lblSpecialThanks1.Size = new Size(96, 15);
            lblSpecialThanks1.TabIndex = 3;
            lblSpecialThanks1.Text = "Special thanks to";
            lblSpecialThanks1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblAboutInfo
            // 
            lblAboutInfo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblAboutInfo.AutoSize = true;
            lblAboutInfo.Location = new Point(3, 35);
            lblAboutInfo.Name = "lblAboutInfo";
            lblAboutInfo.Size = new Size(366, 30);
            lblAboutInfo.TabIndex = 3;
            lblAboutInfo.Text = "A tool to help create and manage custom radio stations in Cyberpunk 2077 when using the radioExt Mod.";
            lblAboutInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(lblCopyright, 0, 0);
            tableLayoutPanel2.Controls.Add(lnkGithubRepo, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 154);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(366, 24);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // lblCopyright
            // 
            lblCopyright.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblCopyright.AutoSize = true;
            lblCopyright.Location = new Point(3, 4);
            lblCopyright.Name = "lblCopyright";
            lblCopyright.Size = new Size(177, 15);
            lblCopyright.TabIndex = 2;
            lblCopyright.Text = "(c) 2024 Ethan Hann";
            lblCopyright.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lnkGithubRepo
            // 
            lnkGithubRepo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lnkGithubRepo.AutoSize = true;
            lnkGithubRepo.Location = new Point(186, 4);
            lnkGithubRepo.Name = "lnkGithubRepo";
            lnkGithubRepo.Size = new Size(177, 15);
            lnkGithubRepo.TabIndex = 3;
            lnkGithubRepo.TabStop = true;
            lnkGithubRepo.Text = "Github Repo";
            lnkGithubRepo.TextAlign = ContentAlignment.MiddleCenter;
            lnkGithubRepo.LinkClicked += lnkGithubRepo_LinkClicked;
            // 
            // AboutBox
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(496, 181);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutBox";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "About";
            TopMost = true;
            Load += AboutBox_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Label lblAppName;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblAboutInfo;
        private Label lblCopyright;
        private Panel panel1;
        private Label lblVersion;
        private Label lblSpecialThanks2;
        private LinkLabel lnkRadioExtDev;
        private Label lblSpecialThanks1;
        private TableLayoutPanel tableLayoutPanel2;
        private LinkLabel lnkGithubRepo;
        private LinkLabel lnkLicense;
    }
}