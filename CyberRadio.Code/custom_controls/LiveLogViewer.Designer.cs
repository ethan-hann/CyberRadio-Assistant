namespace RadioExt_Helper.custom_controls
{
    partial class LiveLogViewer
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            tableLayoutPanel1 = new TableLayoutPanel();
            txtSearch = new TextBox();
            dgvLogs = new DataGridView();
            colTimestamp = new DataGridViewTextBoxColumn();
            colLevel = new DataGridViewTextBoxColumn();
            colMessage = new DataGridViewTextBoxColumn();
            cmsLineRightClick = new ContextMenuStrip(components);
            copyLineToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            cmsLineRightClick.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(txtSearch, 0, 0);
            tableLayoutPanel1.Controls.Add(dgvLogs, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(946, 643);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.Dock = DockStyle.Fill;
            txtSearch.Location = new Point(3, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search log entries...";
            txtSearch.Size = new Size(940, 23);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // dgvLogs
            // 
            dgvLogs.AllowUserToAddRows = false;
            dgvLogs.AllowUserToDeleteRows = false;
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLogs.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvLogs.BackgroundColor = Color.White;
            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogs.Columns.AddRange(new DataGridViewColumn[] { colTimestamp, colLevel, colMessage });
            dgvLogs.Dock = DockStyle.Fill;
            dgvLogs.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvLogs.Location = new Point(3, 32);
            dgvLogs.MultiSelect = false;
            dgvLogs.Name = "dgvLogs";
            dgvLogs.ReadOnly = true;
            dgvLogs.RowHeadersVisible = false;
            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogs.Size = new Size(940, 608);
            dgvLogs.TabIndex = 1;
            dgvLogs.MouseDown += dgvLogs_MouseDown;
            // 
            // colTimestamp
            // 
            colTimestamp.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            colTimestamp.FillWeight = 130F;
            colTimestamp.HeaderText = "Timestamp";
            colTimestamp.Name = "colTimestamp";
            colTimestamp.ReadOnly = true;
            colTimestamp.Width = 92;
            // 
            // colLevel
            // 
            colLevel.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            colLevel.FillWeight = 65F;
            colLevel.HeaderText = "Level";
            colLevel.Name = "colLevel";
            colLevel.ReadOnly = true;
            colLevel.Width = 59;
            // 
            // colMessage
            // 
            colMessage.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            colMessage.DefaultCellStyle = dataGridViewCellStyle1;
            colMessage.FillWeight = 505F;
            colMessage.HeaderText = "Message";
            colMessage.Name = "colMessage";
            colMessage.ReadOnly = true;
            // 
            // cmsLineRightClick
            // 
            cmsLineRightClick.Items.AddRange(new ToolStripItem[] { copyLineToolStripMenuItem });
            cmsLineRightClick.Name = "cmsLineRightClick";
            cmsLineRightClick.Size = new Size(181, 48);
            // 
            // copyLineToolStripMenuItem
            // 
            copyLineToolStripMenuItem.Image = Properties.Resources.copy_alt;
            copyLineToolStripMenuItem.Name = "copyLineToolStripMenuItem";
            copyLineToolStripMenuItem.Size = new Size(180, 22);
            copyLineToolStripMenuItem.Text = "Copy Line";
            copyLineToolStripMenuItem.Click += copyLineToolStripMenuItem_Click;
            // 
            // LiveLogViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "LiveLogViewer";
            Size = new Size(946, 643);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            cmsLineRightClick.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TextBox txtSearch;
        private DataGridView dgvLogs;
        private DataGridViewTextBoxColumn colTimestamp;
        private DataGridViewTextBoxColumn colLevel;
        private DataGridViewTextBoxColumn colMessage;
        private ContextMenuStrip cmsLineRightClick;
        private ToolStripMenuItem copyLineToolStripMenuItem;
    }
}
