namespace RadioExt_Helper.forms
{
    partial class IconManagerForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IconManagerForm));
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            pgProgressBar = new ToolStripProgressBar();
            splitContainer1 = new SplitContainer();
            lbStations = new user_controls.StationListBox();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, toolStripStatusLabel2, pgProgressBar });
            statusStrip1.Location = new Point(0, 569);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(804, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Image = Properties.Resources.status__16x16;
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(55, 17);
            lblStatus.Text = "Ready";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(482, 17);
            toolStripStatusLabel2.Spring = true;
            // 
            // pgProgressBar
            // 
            pgProgressBar.Name = "pgProgressBar";
            pgProgressBar.Size = new Size(250, 16);
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lbStations);
            splitContainer1.Size = new Size(804, 569);
            splitContainer1.SplitterDistance = 268;
            splitContainer1.TabIndex = 1;
            // 
            // lbStations
            // 
            lbStations.AllowDrop = true;
            lbStations.CausesValidation = false;
            lbStations.DisabledIconKey = "disabled";
            lbStations.Dock = DockStyle.Fill;
            lbStations.DrawMode = DrawMode.OwnerDrawFixed;
            lbStations.DuplicateColor = Color.FromArgb(255, 128, 0);
            lbStations.DuplicateFont = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lbStations.EditedStationIconKey = "edited_station";
            lbStations.EnabledIconKey = "enabled";
            lbStations.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbStations.FormattingEnabled = true;
            lbStations.ItemHeight = 17;
            lbStations.Location = new Point(0, 0);
            lbStations.Margin = new Padding(3, 2, 3, 2);
            lbStations.Name = "lbStations";
            lbStations.NewStationColor = Color.DarkGreen;
            lbStations.NewStationFont = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbStations.SavedStationIconKey = "saved_station";
            lbStations.Size = new Size(268, 569);
            lbStations.SongsMissingColor = Color.FromArgb(192, 0, 0);
            lbStations.SongsMissingFont = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbStations.TabIndex = 1;
            lbStations.DataSourceChanged += lbStations_SelectedIndexChanged;
            // 
            // IconManagerForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(804, 591);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "IconManagerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Station Icon Manager";
            Load += IconManagerForm_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripProgressBar pgProgressBar;
        private SplitContainer splitContainer1;
        private user_controls.StationListBox lbStations;
    }
}