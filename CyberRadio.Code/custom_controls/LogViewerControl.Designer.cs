namespace RadioExt_Helper.custom_controls
{
    partial class LogViewerControl
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
            btnShowMore = new Button();
            txtSearch = new TextBox();
            dgvLogs = new DataGridView();
            colDateTime = new DataGridViewTextBoxColumn();
            colOutput = new DataGridViewTextBoxColumn();
            grpLogSearchHeader = new GroupBox();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            grpLogSearchHeader.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.0405121F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.9594879F));
            tableLayoutPanel1.Controls.Add(btnShowMore, 0, 2);
            tableLayoutPanel1.Controls.Add(txtSearch, 0, 0);
            tableLayoutPanel1.Controls.Add(dgvLogs, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 25);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.367347F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 91.63265F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 41F));
            tableLayoutPanel1.Size = new Size(932, 532);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnShowMore
            // 
            btnShowMore.BackColor = Color.Yellow;
            tableLayoutPanel1.SetColumnSpan(btnShowMore, 2);
            btnShowMore.Dock = DockStyle.Fill;
            btnShowMore.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnShowMore.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnShowMore.FlatStyle = FlatStyle.Flat;
            btnShowMore.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnShowMore.Image = Properties.Resources.down__16x16;
            btnShowMore.Location = new Point(3, 493);
            btnShowMore.Name = "btnShowMore";
            btnShowMore.Size = new Size(926, 36);
            btnShowMore.TabIndex = 5;
            btnShowMore.Text = "Show More...";
            btnShowMore.TextAlign = ContentAlignment.MiddleRight;
            btnShowMore.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnShowMore.UseVisualStyleBackColor = false;
            btnShowMore.Click += btnShowMore_Click;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(txtSearch, 2);
            txtSearch.Location = new Point(3, 6);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Enter search filter...";
            txtSearch.Size = new Size(926, 29);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // dgvLogs
            // 
            dgvLogs.AllowUserToAddRows = false;
            dgvLogs.AllowUserToDeleteRows = false;
            dgvLogs.BackgroundColor = Color.White;
            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogs.Columns.AddRange(new DataGridViewColumn[] { colDateTime, colOutput });
            tableLayoutPanel1.SetColumnSpan(dgvLogs, 2);
            dgvLogs.Dock = DockStyle.Fill;
            dgvLogs.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvLogs.Location = new Point(3, 44);
            dgvLogs.MultiSelect = false;
            dgvLogs.Name = "dgvLogs";
            dgvLogs.ReadOnly = true;
            dgvLogs.Size = new Size(926, 443);
            dgvLogs.TabIndex = 4;
            // 
            // colDateTime
            // 
            colDateTime.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            colDateTime.HeaderText = "Timestamp";
            colDateTime.Name = "colDateTime";
            colDateTime.ReadOnly = true;
            colDateTime.Width = 119;
            // 
            // colOutput
            // 
            colOutput.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colOutput.HeaderText = "Output";
            colOutput.Name = "colOutput";
            colOutput.ReadOnly = true;
            // 
            // grpLogSearchHeader
            // 
            grpLogSearchHeader.Controls.Add(tableLayoutPanel1);
            grpLogSearchHeader.Dock = DockStyle.Fill;
            grpLogSearchHeader.Font = new Font("Segoe UI Variable Small Semibol", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grpLogSearchHeader.Location = new Point(0, 0);
            grpLogSearchHeader.Name = "grpLogSearchHeader";
            grpLogSearchHeader.Size = new Size(938, 560);
            grpLogSearchHeader.TabIndex = 0;
            grpLogSearchHeader.TabStop = false;
            grpLogSearchHeader.Text = "Log File";
            // 
            // LogViewerControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(grpLogSearchHeader);
            Name = "LogViewerControl";
            Size = new Size(938, 560);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            grpLogSearchHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox grpLogSearchHeader;
        private DataGridView dgvLogs;
        private DataGridViewTextBoxColumn colDateTime;
        private DataGridViewTextBoxColumn colOutput;
        private Button btnShowMore;
        private TextBox txtSearch;
    }
}
