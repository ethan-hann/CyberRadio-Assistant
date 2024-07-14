using RadioExt_Helper.custom_controls;

namespace RadioExt_Helper.user_controls
{
    sealed partial class ModDetails
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
            pbModImage = new PictureBox();
            lblName = new AutoSizeLabel();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            rtbSummary = new RichTextBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblAuthor = new AutoSizeLabel();
            ((System.ComponentModel.ISupportInitialize)pbModImage).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // pbModImage
            // 
            pbModImage.Dock = DockStyle.Fill;
            pbModImage.Location = new Point(3, 3);
            pbModImage.Name = "pbModImage";
            pbModImage.Size = new Size(169, 149);
            pbModImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbModImage.TabIndex = 0;
            pbModImage.TabStop = false;
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI Variable Display", 10F, FontStyle.Bold);
            lblName.Location = new Point(3, 7);
            lblName.Name = "lblName";
            lblName.Size = new Size(343, 19);
            lblName.TabIndex = 1;
            lblName.Text = "<name>";
            lblName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.98106F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80.0189362F));
            tableLayoutPanel1.Controls.Add(pbModImage, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(880, 153);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Controls.Add(rtbSummary);
            panel1.Controls.Add(tableLayoutPanel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(178, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(699, 149);
            panel1.TabIndex = 2;
            // 
            // rtbSummary
            // 
            rtbSummary.BackColor = Color.White;
            rtbSummary.BorderStyle = BorderStyle.None;
            rtbSummary.Dock = DockStyle.Fill;
            rtbSummary.Location = new Point(0, 34);
            rtbSummary.Name = "rtbSummary";
            rtbSummary.ReadOnly = true;
            rtbSummary.Size = new Size(699, 115);
            rtbSummary.TabIndex = 3;
            rtbSummary.Text = "";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(lblName, 0, 0);
            tableLayoutPanel2.Controls.Add(lblAuthor, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(699, 34);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // lblAuthor
            // 
            lblAuthor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblAuthor.AutoSize = true;
            lblAuthor.Font = new Font("Segoe UI Variable Display Semib", 8F, FontStyle.Bold);
            lblAuthor.Location = new Point(352, 9);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Size = new Size(344, 15);
            lblAuthor.TabIndex = 2;
            lblAuthor.Text = "By: <author>";
            lblAuthor.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ModDetails
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tableLayoutPanel1);
            Name = "ModDetails";
            Size = new Size(880, 153);
            Load += ModDetails_Load;
            ((System.ComponentModel.ISupportInitialize)pbModImage).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbModImage;
        private AutoSizeLabel lblName;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private AutoSizeLabel lblAuthor;
        private RichTextBox rtbSummary;
        private TableLayoutPanel tableLayoutPanel2;
    }
}
