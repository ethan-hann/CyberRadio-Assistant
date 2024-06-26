﻿namespace RadioExt_Helper.user_controls
{
    sealed partial class NoStationsCtl
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
            pictureBox1 = new PictureBox();
            lblNoStations = new Label();
            tlpNoStagingPath = new TableLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            pictureBox3 = new PictureBox();
            lblNoStagingPath = new Label();
            btnPaths = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            pictureBox2 = new PictureBox();
            lblNoGamePath = new Label();
            tlpNoGamePath = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tlpNoStagingPath.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            tlpNoGamePath.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(lblNoStations, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 62.70872F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 37.29128F));
            tableLayoutPanel1.Size = new Size(885, 539);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            pictureBox1.Image = Properties.Resources.cyber_radio_assistant;
            pictureBox1.Location = new Point(314, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(256, 332);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // lblNoStations
            // 
            lblNoStations.Dock = DockStyle.Fill;
            lblNoStations.Font = new Font("Segoe UI Variable Display", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNoStations.Location = new Point(3, 363);
            lblNoStations.Margin = new Padding(3, 25, 3, 0);
            lblNoStations.Name = "lblNoStations";
            lblNoStations.Size = new Size(879, 176);
            lblNoStations.TabIndex = 1;
            lblNoStations.Text = "No Stations Yet!";
            lblNoStations.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tlpNoStagingPath
            // 
            tlpNoStagingPath.ColumnCount = 2;
            tlpNoStagingPath.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85F));
            tlpNoStagingPath.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tlpNoStagingPath.Controls.Add(flowLayoutPanel2, 0, 0);
            tlpNoStagingPath.Controls.Add(btnPaths, 1, 0);
            tlpNoStagingPath.Dock = DockStyle.Bottom;
            tlpNoStagingPath.Location = new Point(0, 590);
            tlpNoStagingPath.Name = "tlpNoStagingPath";
            tlpNoStagingPath.RowCount = 1;
            tlpNoStagingPath.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpNoStagingPath.Size = new Size(885, 57);
            tlpNoStagingPath.TabIndex = 1;
            tlpNoStagingPath.Visible = false;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(pictureBox3);
            flowLayoutPanel2.Controls.Add(lblNoStagingPath);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Location = new Point(3, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(746, 51);
            flowLayoutPanel2.TabIndex = 1;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.warning;
            pictureBox3.Location = new Point(3, 8);
            pictureBox3.Margin = new Padding(3, 8, 3, 3);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(32, 32);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            // 
            // lblNoStagingPath
            // 
            lblNoStagingPath.AutoSize = true;
            lblNoStagingPath.Font = new Font("Segoe UI Variable Text", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNoStagingPath.Location = new Point(41, 8);
            lblNoStagingPath.Margin = new Padding(3, 8, 3, 0);
            lblNoStagingPath.Name = "lblNoStagingPath";
            lblNoStagingPath.Size = new Size(124, 17);
            lblNoStagingPath.TabIndex = 3;
            lblNoStagingPath.Text = "<no staging path>";
            // 
            // btnPaths
            // 
            btnPaths.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnPaths.BackColor = Color.Yellow;
            btnPaths.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnPaths.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnPaths.FlatStyle = FlatStyle.Flat;
            btnPaths.Image = Properties.Resources.folder_16x16;
            btnPaths.Location = new Point(755, 11);
            btnPaths.Name = "btnPaths";
            btnPaths.Size = new Size(127, 34);
            btnPaths.TabIndex = 2;
            btnPaths.Text = "Paths...";
            btnPaths.TextAlign = ContentAlignment.MiddleRight;
            btnPaths.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnPaths.UseVisualStyleBackColor = false;
            btnPaths.Click += btnPaths_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(pictureBox2);
            flowLayoutPanel1.Controls.Add(lblNoGamePath);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(746, 45);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.warning;
            pictureBox2.Location = new Point(3, 6);
            pictureBox2.Margin = new Padding(3, 6, 3, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(32, 32);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // lblNoGamePath
            // 
            lblNoGamePath.AutoSize = true;
            lblNoGamePath.Font = new Font("Segoe UI Variable Text", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNoGamePath.Location = new Point(41, 8);
            lblNoGamePath.Margin = new Padding(3, 8, 3, 0);
            lblNoGamePath.Name = "lblNoGamePath";
            lblNoGamePath.Size = new Size(112, 17);
            lblNoGamePath.TabIndex = 1;
            lblNoGamePath.Text = "<no game path>";
            // 
            // tlpNoGamePath
            // 
            tlpNoGamePath.ColumnCount = 2;
            tlpNoGamePath.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85F));
            tlpNoGamePath.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tlpNoGamePath.Controls.Add(flowLayoutPanel1, 0, 0);
            tlpNoGamePath.Dock = DockStyle.Bottom;
            tlpNoGamePath.Location = new Point(0, 539);
            tlpNoGamePath.Name = "tlpNoGamePath";
            tlpNoGamePath.RowCount = 1;
            tlpNoGamePath.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpNoGamePath.Size = new Size(885, 51);
            tlpNoGamePath.TabIndex = 2;
            tlpNoGamePath.Visible = false;
            // 
            // NoStationsCtl
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(tlpNoGamePath);
            Controls.Add(tlpNoStagingPath);
            Name = "NoStationsCtl";
            Size = new Size(885, 647);
            Load += NoStationsCtl_Load;
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tlpNoStagingPath.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            tlpNoGamePath.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBox1;
        private Label lblNoStations;
        private TableLayoutPanel tlpNoStagingPath;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private PictureBox pictureBox2;
        private Label lblNoGamePath;
        private PictureBox pictureBox3;
        private Label lblNoStagingPath;
        private TableLayoutPanel tlpNoGamePath;
        private Button btnPaths;
    }
}
