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
            grpLogSearchHeader = new GroupBox();
            dgvStatus = new DataGridView();
            colDateTime = new DataGridViewTextBoxColumn();
            colOutput = new DataGridViewTextBoxColumn();
            btnShowMore = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            txtSearch = new TextBox();
            tableLayoutPanel1.SuspendLayout();
            grpLogSearchHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStatus).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.0405121F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.9594879F));
            tableLayoutPanel1.Controls.Add(btnShowMore, 0, 2);
            tableLayoutPanel1.Controls.Add(dgvStatus, 0, 1);
            tableLayoutPanel1.Controls.Add(grpLogSearchHeader, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 13.1274128F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 86.87259F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 41F));
            tableLayoutPanel1.Size = new Size(938, 560);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // grpLogSearchHeader
            // 
            tableLayoutPanel1.SetColumnSpan(grpLogSearchHeader, 2);
            grpLogSearchHeader.Controls.Add(tableLayoutPanel2);
            grpLogSearchHeader.Dock = DockStyle.Fill;
            grpLogSearchHeader.Font = new Font("Segoe UI Variable Small Semibol", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grpLogSearchHeader.Location = new Point(3, 3);
            grpLogSearchHeader.Name = "grpLogSearchHeader";
            grpLogSearchHeader.Size = new Size(932, 62);
            grpLogSearchHeader.TabIndex = 0;
            grpLogSearchHeader.TabStop = false;
            grpLogSearchHeader.Text = "Log File";
            // 
            // dgvStatus
            // 
            dgvStatus.AllowUserToAddRows = false;
            dgvStatus.AllowUserToDeleteRows = false;
            dgvStatus.BackgroundColor = Color.White;
            dgvStatus.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStatus.Columns.AddRange(new DataGridViewColumn[] { colDateTime, colOutput });
            tableLayoutPanel1.SetColumnSpan(dgvStatus, 2);
            dgvStatus.Dock = DockStyle.Fill;
            dgvStatus.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvStatus.Location = new Point(3, 71);
            dgvStatus.MultiSelect = false;
            dgvStatus.Name = "dgvStatus";
            dgvStatus.ReadOnly = true;
            dgvStatus.Size = new Size(932, 444);
            dgvStatus.TabIndex = 4;
            // 
            // colDateTime
            // 
            colDateTime.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            colDateTime.HeaderText = "Timestamp";
            colDateTime.Name = "colDateTime";
            colDateTime.ReadOnly = true;
            colDateTime.Width = 91;
            // 
            // colOutput
            // 
            colOutput.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colOutput.HeaderText = "Output";
            colOutput.Name = "colOutput";
            colOutput.ReadOnly = true;
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
            btnShowMore.Location = new Point(3, 521);
            btnShowMore.Name = "btnShowMore";
            btnShowMore.Size = new Size(932, 36);
            btnShowMore.TabIndex = 5;
            btnShowMore.Text = "Show More...";
            btnShowMore.TextAlign = ContentAlignment.MiddleRight;
            btnShowMore.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnShowMore.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.9546432F));
            tableLayoutPanel2.Controls.Add(txtSearch, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 25);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(926, 34);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.Dock = DockStyle.Fill;
            txtSearch.Location = new Point(3, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Enter search filter...";
            txtSearch.Size = new Size(920, 29);
            txtSearch.TabIndex = 0;
            // 
            // LogViewerControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tableLayoutPanel1);
            Name = "LogViewerControl";
            Size = new Size(938, 560);
            tableLayoutPanel1.ResumeLayout(false);
            grpLogSearchHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvStatus).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox grpLogSearchHeader;
        private DataGridView dgvStatus;
        private DataGridViewTextBoxColumn colDateTime;
        private DataGridViewTextBoxColumn colOutput;
        private Button btnShowMore;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox txtSearch;
    }
}
