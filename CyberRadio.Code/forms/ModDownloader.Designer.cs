namespace RadioExt_Helper.forms
{
    partial class ModDownloader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModDownloader));
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            splitContainer1 = new SplitContainer();
            dataGridView1 = new DataGridView();
            colModId = new DataGridViewTextBoxColumn();
            panel1 = new Panel();
            btnPasteModIds = new Button();
            lblModIdHelp = new RadioExt_Helper.custom_controls.WrapLabel();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnCancel = new Button();
            btnGetMods = new Button();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip1.Location = new Point(0, 600);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1053, 22);
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
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            splitContainer1.Panel1.Controls.Add(panel1);
            splitContainer1.Size = new Size(1053, 557);
            splitContainer1.SplitterDistance = 298;
            splitContainer1.TabIndex = 7;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { colModId });
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 100);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(298, 457);
            dataGridView1.TabIndex = 1;
            // 
            // colModId
            // 
            colModId.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colModId.HeaderText = "Mod ID";
            colModId.MaxInputLength = 15;
            colModId.Name = "colModId";
            colModId.Resizable = DataGridViewTriState.False;
            colModId.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnPasteModIds);
            panel1.Controls.Add(lblModIdHelp);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(298, 100);
            panel1.TabIndex = 0;
            // 
            // btnPasteModIds
            // 
            btnPasteModIds.BackColor = Color.Yellow;
            btnPasteModIds.Dock = DockStyle.Bottom;
            btnPasteModIds.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnPasteModIds.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnPasteModIds.FlatStyle = FlatStyle.Flat;
            btnPasteModIds.Image = Properties.Resources.paste_16x16;
            btnPasteModIds.Location = new Point(0, 67);
            btnPasteModIds.Margin = new Padding(3, 2, 3, 2);
            btnPasteModIds.Name = "btnPasteModIds";
            btnPasteModIds.Size = new Size(298, 33);
            btnPasteModIds.TabIndex = 4;
            btnPasteModIds.Text = "Paste from Clipboard";
            btnPasteModIds.TextAlign = ContentAlignment.MiddleRight;
            btnPasteModIds.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnPasteModIds.UseVisualStyleBackColor = false;
            // 
            // lblModIdHelp
            // 
            lblModIdHelp.Font = new Font("Segoe UI Variable Display Semib", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblModIdHelp.InnerTextAlignment = custom_controls.WrapLabel.InnerTextAlign.Center;
            lblModIdHelp.Location = new Point(3, 9);
            lblModIdHelp.Name = "lblModIdHelp";
            lblModIdHelp.Size = new Size(292, 34);
            lblModIdHelp.TabIndex = 2;
            lblModIdHelp.Text = "Enter the Mod Ids for the mods you want to download. Press <Enter> after each entry.";
            lblModIdHelp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 82.86814F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.1318569F));
            tableLayoutPanel1.Controls.Add(btnCancel, 1, 0);
            tableLayoutPanel1.Controls.Add(btnGetMods, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 557);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1053, 43);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnCancel.BackColor = Color.Yellow;
            btnCancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Image = Properties.Resources.cancel_16x16;
            btnCancel.Location = new Point(875, 5);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(175, 33);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.TextAlign = ContentAlignment.MiddleRight;
            btnCancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnGetMods
            // 
            btnGetMods.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnGetMods.BackColor = Color.Yellow;
            btnGetMods.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnGetMods.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnGetMods.FlatStyle = FlatStyle.Flat;
            btnGetMods.Image = Properties.Resources.download_16x16;
            btnGetMods.Location = new Point(3, 5);
            btnGetMods.Margin = new Padding(3, 2, 3, 2);
            btnGetMods.Name = "btnGetMods";
            btnGetMods.Size = new Size(866, 33);
            btnGetMods.TabIndex = 3;
            btnGetMods.Text = "Get Mods";
            btnGetMods.TextAlign = ContentAlignment.MiddleRight;
            btnGetMods.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGetMods.UseVisualStyleBackColor = false;
            // 
            // ModDownloader
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(1053, 622);
            Controls.Add(splitContainer1);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(statusStrip1);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ModDownloader";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Download Mods";
            TopMost = true;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private user_controls.ModDetails modDetails;
        private SplitContainer splitContainer1;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnCancel;
        private Button btnGetMods;
        private Button btnPasteModIds;
        private custom_controls.WrapLabel lblModIdHelp;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn colModId;
    }
}