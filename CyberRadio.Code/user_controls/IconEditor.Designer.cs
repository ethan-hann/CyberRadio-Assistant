using RadioExt_Helper.custom_controls;

namespace RadioExt_Helper.user_controls
{
    partial class IconEditor
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
            lblEditingText = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            picStationIcon = new CustomPictureBox();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picStationIcon).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.5145645F));
            tableLayoutPanel1.Controls.Add(lblEditingText, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 394F));
            tableLayoutPanel1.Size = new Size(618, 440);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblEditingText
            // 
            lblEditingText.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblEditingText.AutoSize = true;
            lblEditingText.Font = new Font("Segoe UI Variable Display", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEditingText.Location = new Point(3, 12);
            lblEditingText.Name = "lblEditingText";
            lblEditingText.Size = new Size(612, 21);
            lblEditingText.TabIndex = 0;
            lblEditingText.Text = "Editing Station Icon: {0}";
            lblEditingText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.5294113F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76.47059F));
            tableLayoutPanel2.Controls.Add(picStationIcon, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 49);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 47.9381447F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 52.0618553F));
            tableLayoutPanel2.Size = new Size(612, 388);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // picStationIcon
            // 
            picStationIcon.AllowDrop = true;
            picStationIcon.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            picStationIcon.Location = new Point(3, 130);
            picStationIcon.Name = "picStationIcon";
            tableLayoutPanel2.SetRowSpan(picStationIcon, 2);
            picStationIcon.Size = new Size(138, 128);
            picStationIcon.TabIndex = 0;
            picStationIcon.TabStop = false;
            picStationIcon.DragDrop += picStationIcon_DragDrop;
            // 
            // IconEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "IconEditor";
            Size = new Size(618, 440);
            Load += IconEditor_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picStationIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label lblEditingText;
        private TableLayoutPanel tableLayoutPanel2;
        private CustomPictureBox picStationIcon;
    }
}
