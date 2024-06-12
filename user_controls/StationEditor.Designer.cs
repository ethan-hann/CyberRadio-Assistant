namespace RadioExt_Helper.user_controls
{
    partial class StationEditor
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
            panel1 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            picStationIcon = new PictureBox();
            textBox2 = new TextBox();
            label2 = new Label();
            txtDisplayName = new TextBox();
            label1 = new Label();
            label3 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            panel2 = new Panel();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            label4 = new Label();
            label5 = new Label();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picStationIcon).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(tableLayoutPanel2);
            panel1.Font = new Font("CF Notche Demo", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(880, 630);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54.8275871F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.1724129F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 628F));
            tableLayoutPanel2.Controls.Add(textBox2, 2, 1);
            tableLayoutPanel2.Controls.Add(label2, 1, 1);
            tableLayoutPanel2.Controls.Add(picStationIcon, 0, 0);
            tableLayoutPanel2.Controls.Add(txtDisplayName, 2, 0);
            tableLayoutPanel2.Controls.Add(label1, 1, 0);
            tableLayoutPanel2.Controls.Add(label3, 0, 2);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel1, 1, 2);
            tableLayoutPanel2.Controls.Add(panel2, 2, 2);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 7;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 46.31579F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 53.68421F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 83F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 362F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(880, 630);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // picStationIcon
            // 
            picStationIcon.Image = Properties.Resources.cyber_radio_assistant;
            picStationIcon.Location = new Point(3, 3);
            picStationIcon.Name = "picStationIcon";
            tableLayoutPanel2.SetRowSpan(picStationIcon, 2);
            picStationIcon.Size = new Size(128, 73);
            picStationIcon.SizeMode = PictureBoxSizeMode.Zoom;
            picStationIcon.TabIndex = 0;
            picStationIcon.TabStop = false;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(254, 46);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(623, 24);
            textBox2.TabIndex = 3;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(203, 50);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 2;
            label2.Text = "Icon: ";
            // 
            // txtDisplayName
            // 
            txtDisplayName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDisplayName.Location = new Point(254, 6);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Size = new Size(623, 24);
            txtDisplayName.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(142, 11);
            label1.Name = "label1";
            label1.Size = new Size(106, 15);
            label1.TabIndex = 0;
            label1.Text = "Display Name: ";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(27, 96);
            label3.Name = "label3";
            label3.Size = new Size(108, 30);
            label3.TabIndex = 4;
            label3.Text = "Using Custom Icon?";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(radioButton1);
            flowLayoutPanel1.Controls.Add(radioButton2);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(141, 82);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(107, 59);
            flowLayoutPanel1.TabIndex = 6;
            // 
            // radioButton1
            // 
            radioButton1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(3, 3);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(50, 19);
            radioButton1.TabIndex = 5;
            radioButton1.TabStop = true;
            radioButton1.Text = "Yes";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(5, 28);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(45, 19);
            radioButton2.TabIndex = 6;
            radioButton2.TabStop = true;
            radioButton2.Text = "No";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(textBox4);
            panel2.Controls.Add(textBox3);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label5);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(254, 82);
            panel2.Name = "panel2";
            panel2.Size = new Size(623, 59);
            panel2.TabIndex = 7;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(120, 30);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(500, 24);
            textBox4.TabIndex = 3;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(120, 2);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(500, 24);
            textBox3.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1, 11);
            label4.Name = "label4";
            label4.Size = new Size(113, 15);
            label4.TabIndex = 0;
            label4.Text = "Ink Atlas Path: ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 39);
            label5.Name = "label5";
            label5.Size = new Size(111, 15);
            label5.TabIndex = 1;
            label5.Text = "Ink Atlas Part: ";
            // 
            // StationEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "StationEditor";
            Size = new Size(1503, 1083);
            Load += StationEditor_Load;
            panel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picStationIcon).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox picStationIcon;
        private Label label1;
        private TextBox txtDisplayName;
        private Label label2;
        private TextBox textBox2;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label3;
        private FlowLayoutPanel flowLayoutPanel1;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private Panel panel2;
        private TextBox textBox4;
        private TextBox textBox3;
        private Label label4;
        private Label label5;
    }
}
