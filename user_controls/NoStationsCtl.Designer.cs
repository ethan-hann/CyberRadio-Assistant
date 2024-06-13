namespace RadioExt_Helper.user_controls
{
    partial class NoStationsCtl
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
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(885, 647);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Bottom;
            pictureBox1.Image = Properties.Resources.cyber_radio_assistant;
            pictureBox1.Location = new Point(314, 64);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(256, 256);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // lblNoStations
            // 
            lblNoStations.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            lblNoStations.AutoSize = true;
            lblNoStations.Font = new Font("CF Notche Demo", 27.7499962F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNoStations.Location = new Point(293, 348);
            lblNoStations.Margin = new Padding(3, 25, 3, 0);
            lblNoStations.Name = "lblNoStations";
            lblNoStations.Size = new Size(299, 299);
            lblNoStations.TabIndex = 1;
            lblNoStations.Text = "No Stations Yet!";
            lblNoStations.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // NoStationsCtl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tableLayoutPanel1);
            Name = "NoStationsCtl";
            Size = new Size(885, 647);
            Load += NoStationsCtl_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBox1;
        private Label lblNoStations;
    }
}
