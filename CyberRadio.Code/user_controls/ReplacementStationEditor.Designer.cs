namespace RadioExt_Helper.user_controls
{
    sealed partial class ReplacementStationEditor
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
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            tabControl = new TabControl();
            tabDisplay = new TabPage();
            groupBox1 = new GroupBox();
            tinyMce1 = new RadioExt_Helper.custom_controls.TinyMce();
            grpDisplay = new GroupBox();
            tlpDisplayTable = new TableLayoutPanel();
            lblIcon = new Label();
            txtDisplayName = new TextBox();
            lblDisplayName = new Label();
            lblVanillaName = new Label();
            txtVanillaStationName = new TextBox();
            pbStationIcon = new PictureBox();
            tabMusic = new TabPage();
            statusStrip1.SuspendLayout();
            tabControl.SuspendLayout();
            tabDisplay.SuspendLayout();
            groupBox1.SuspendLayout();
            grpDisplay.SuspendLayout();
            tlpDisplayTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbStationIcon).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip1.Location = new Point(0, 614);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(987, 25);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 8;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Image = Properties.Resources.info__16x16;
            lblStatus.Margin = new Padding(5, 3, 0, 2);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(2);
            lblStatus.Size = new Size(59, 20);
            lblStatus.Text = "Ready";
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabDisplay);
            tabControl.Controls.Add(tabMusic);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(987, 614);
            tabControl.TabIndex = 10;
            // 
            // tabDisplay
            // 
            tabDisplay.BackColor = Color.White;
            tabDisplay.BorderStyle = BorderStyle.FixedSingle;
            tabDisplay.Controls.Add(groupBox1);
            tabDisplay.Controls.Add(grpDisplay);
            tabDisplay.Font = new Font("Microsoft Sans Serif", 9.749999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabDisplay.ImageIndex = 0;
            tabDisplay.Location = new Point(4, 29);
            tabDisplay.Name = "tabDisplay";
            tabDisplay.Padding = new Padding(3);
            tabDisplay.Size = new Size(979, 581);
            tabDisplay.TabIndex = 0;
            tabDisplay.Text = "Main Info";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(tinyMce1);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            groupBox1.Location = new Point(3, 208);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(971, 368);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Notes";
            // 
            // tinyMce1
            // 
            tinyMce1.Dock = DockStyle.Fill;
            tinyMce1.Location = new Point(3, 21);
            tinyMce1.Name = "tinyMce1";
            tinyMce1.Size = new Size(965, 344);
            tinyMce1.TabIndex = 0;
            // 
            // grpDisplay
            // 
            grpDisplay.BackColor = Color.White;
            grpDisplay.Controls.Add(tlpDisplayTable);
            grpDisplay.Dock = DockStyle.Top;
            grpDisplay.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpDisplay.Location = new Point(3, 3);
            grpDisplay.Name = "grpDisplay";
            grpDisplay.Size = new Size(971, 205);
            grpDisplay.TabIndex = 3;
            grpDisplay.TabStop = false;
            grpDisplay.Text = "Display";
            // 
            // tlpDisplayTable
            // 
            tlpDisplayTable.ColumnCount = 2;
            tlpDisplayTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.9896374F));
            tlpDisplayTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 86.01036F));
            tlpDisplayTable.Controls.Add(lblIcon, 0, 2);
            tlpDisplayTable.Controls.Add(txtDisplayName, 1, 1);
            tlpDisplayTable.Controls.Add(lblDisplayName, 0, 1);
            tlpDisplayTable.Controls.Add(lblVanillaName, 0, 0);
            tlpDisplayTable.Controls.Add(txtVanillaStationName, 1, 0);
            tlpDisplayTable.Controls.Add(pbStationIcon, 1, 2);
            tlpDisplayTable.Dock = DockStyle.Fill;
            tlpDisplayTable.Location = new Point(3, 21);
            tlpDisplayTable.Name = "tlpDisplayTable";
            tlpDisplayTable.RowCount = 3;
            tlpDisplayTable.RowStyles.Add(new RowStyle(SizeType.Percent, 44.3038F));
            tlpDisplayTable.RowStyles.Add(new RowStyle(SizeType.Percent, 55.6962F));
            tlpDisplayTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 111F));
            tlpDisplayTable.Size = new Size(965, 181);
            tlpDisplayTable.TabIndex = 0;
            // 
            // lblIcon
            // 
            lblIcon.Anchor = AnchorStyles.Right;
            lblIcon.AutoSize = true;
            lblIcon.Font = new Font("Segoe UI Variable Text", 9F);
            lblIcon.Location = new Point(59, 117);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(73, 16);
            lblIcon.TabIndex = 4;
            lblIcon.Text = "Station Icon:";
            // 
            // txtDisplayName
            // 
            txtDisplayName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDisplayName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtDisplayName.Location = new Point(138, 38);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Size = new Size(824, 23);
            txtDisplayName.TabIndex = 3;
            txtDisplayName.TextChanged += txtDisplayName_TextChanged;
            // 
            // lblDisplayName
            // 
            lblDisplayName.Anchor = AnchorStyles.Right;
            lblDisplayName.AutoSize = true;
            lblDisplayName.Font = new Font("Segoe UI Variable Text", 9F);
            lblDisplayName.Location = new Point(50, 42);
            lblDisplayName.Name = "lblDisplayName";
            lblDisplayName.Size = new Size(82, 16);
            lblDisplayName.TabIndex = 2;
            lblDisplayName.Text = "Display Name:";
            lblDisplayName.MouseEnter += lblDisplayName_MouseEnter;
            lblDisplayName.MouseLeave += Lbl_MouseLeave;
            // 
            // lblVanillaName
            // 
            lblVanillaName.Anchor = AnchorStyles.Right;
            lblVanillaName.AutoSize = true;
            lblVanillaName.Font = new Font("Segoe UI Variable Text", 9F);
            lblVanillaName.Location = new Point(14, 7);
            lblVanillaName.Name = "lblVanillaName";
            lblVanillaName.Size = new Size(118, 16);
            lblVanillaName.TabIndex = 0;
            lblVanillaName.Text = "Vanilla Station Name:";
            lblVanillaName.MouseEnter += lblVanillaName_MouseEnter;
            lblVanillaName.MouseLeave += Lbl_MouseLeave;
            // 
            // txtVanillaStationName
            // 
            txtVanillaStationName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtVanillaStationName.Enabled = false;
            txtVanillaStationName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtVanillaStationName.Location = new Point(138, 4);
            txtVanillaStationName.Name = "txtVanillaStationName";
            txtVanillaStationName.ReadOnly = true;
            txtVanillaStationName.Size = new Size(824, 23);
            txtVanillaStationName.TabIndex = 1;
            // 
            // pbStationIcon
            // 
            pbStationIcon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pbStationIcon.Location = new Point(138, 72);
            pbStationIcon.Name = "pbStationIcon";
            pbStationIcon.Size = new Size(143, 106);
            pbStationIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            pbStationIcon.TabIndex = 5;
            pbStationIcon.TabStop = false;
            // 
            // tabMusic
            // 
            tabMusic.BackColor = Color.White;
            tabMusic.BorderStyle = BorderStyle.FixedSingle;
            tabMusic.ImageIndex = 1;
            tabMusic.Location = new Point(4, 29);
            tabMusic.Name = "tabMusic";
            tabMusic.Padding = new Padding(3);
            tabMusic.Size = new Size(979, 581);
            tabMusic.TabIndex = 1;
            tabMusic.Text = "Tracks";
            tabMusic.ToolTipText = "Change the music this radio station will play.";
            // 
            // ReplacementStationEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tabControl);
            Controls.Add(statusStrip1);
            Name = "ReplacementStationEditor";
            Size = new Size(987, 639);
            Load += ReplacementStationEditor_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tabControl.ResumeLayout(false);
            tabDisplay.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            grpDisplay.ResumeLayout(false);
            tlpDisplayTable.ResumeLayout(false);
            tlpDisplayTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbStationIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private TabControl tabControl;
        private TabPage tabDisplay;
        private TabPage tabMusic;
        private GroupBox grpDisplay;
        private TableLayoutPanel tlpDisplayTable;
        private Label lblDisplayName;
        private Label lblVanillaName;
        private TextBox txtVanillaStationName;
        private TextBox txtDisplayName;
        private Label lblIcon;
        private PictureBox pbStationIcon;
        private GroupBox groupBox1;
        private custom_controls.TinyMce tinyMce1;
    }
}
