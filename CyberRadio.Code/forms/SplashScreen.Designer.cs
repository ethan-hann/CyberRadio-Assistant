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
            lblVersion = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblSplashStatus = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // lblVersion
            // 
            lblVersion.Anchor = AnchorStyles.Right;
            lblVersion.AutoSize = true;
            lblVersion.BackColor = Color.Transparent;
            lblVersion.Font = new Font("Segoe UI Variable Display", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVersion.ForeColor = Color.White;
            lblVersion.ImageAlign = ContentAlignment.MiddleLeft;
            lblVersion.Location = new Point(489, 1);
            lblVersion.Name = "lblVersion";
            lblVersion.Padding = new Padding(40, 0, 0, 0);
            lblVersion.Size = new Size(92, 26);
            lblVersion.TabIndex = 2;
            lblVersion.Text = "1.0.0";
            lblVersion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(lblSplashStatus, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 315);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(584, 46);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // lblSplashStatus
            // 
            lblSplashStatus.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblSplashStatus.AutoSize = true;
            lblSplashStatus.Font = new Font("Segoe UI Variable Display", 11.25F, FontStyle.Bold);
            lblSplashStatus.ForeColor = Color.White;
            lblSplashStatus.Location = new Point(3, 13);
            lblSplashStatus.Name = "lblSplashStatus";
            lblSplashStatus.Size = new Size(578, 20);
            lblSplashStatus.TabIndex = 0;
            lblSplashStatus.Text = "Status";
            lblSplashStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.Transparent;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(lblVersion, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(584, 29);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // SplashScreen
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Black;
            BackgroundImage = Properties.Resources.Splash_screen_fixed;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(584, 361);
            ControlBox = false;
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "SplashScreen";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Splash";
            Load += SplashScreen_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label lblVersion;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblSplashStatus;
        private TableLayoutPanel tableLayoutPanel2;
    }
}