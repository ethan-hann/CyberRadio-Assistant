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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StationEditor));
            label2 = new Label();
            txtDisplayName = new TextBox();
            label1 = new Label();
            label3 = new Label();
            groupBox1 = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            cmbUIIcons = new ComboBox();
            tabControl = new TabControl();
            tabDisplayAndIcon = new TabPage();
            panel1 = new Panel();
            panel3 = new Panel();
            groupBox3 = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            panel2 = new Panel();
            label8 = new Label();
            txtVolumeEdit = new TextBox();
            label7 = new Label();
            lblSelectedVolume = new Label();
            volumeSlider = new FloatTrackBar();
            label5 = new Label();
            numericUpDown1 = new NumericUpDown();
            label4 = new Label();
            groupBox2 = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            txtInkAtlasPart = new TextBox();
            txtInkAtlasPath = new TextBox();
            label6 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            radUseCustomYes = new RadioButton();
            radUseCustomNo = new RadioButton();
            lblInkAtlasPart = new Label();
            lblInkAtlasPath = new Label();
            tabMusic = new TabPage();
            groupBox4 = new GroupBox();
            tableLayoutPanel5 = new TableLayoutPanel();
            lblStreamURL = new Label();
            label9 = new Label();
            flowLayoutPanel2 = new FlowLayoutPanel();
            radUseStreamYes = new RadioButton();
            radUseStreamNo = new RadioButton();
            tableLayoutPanel6 = new TableLayoutPanel();
            flowLayoutPanel3 = new FlowLayoutPanel();
            btnGetRadioGardenURL = new Button();
            txtPastedURL = new TextBox();
            txtStreamURL = new TextBox();
            tabImages = new ImageList(components);
            groupBox1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tabControl.SuspendLayout();
            tabDisplayAndIcon.SuspendLayout();
            panel1.SuspendLayout();
            groupBox3.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)volumeSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            groupBox2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            tabMusic.SuspendLayout();
            groupBox4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("CF Notche Demo", 9F);
            label2.Location = new Point(43, 45);
            label2.Name = "label2";
            label2.Size = new Size(36, 12);
            label2.TabIndex = 2;
            label2.Text = "Icon: ";
            // 
            // txtDisplayName
            // 
            txtDisplayName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDisplayName.Font = new Font("CF Notche Demo", 9F, FontStyle.Bold);
            txtDisplayName.Location = new Point(85, 6);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Size = new Size(648, 21);
            txtDisplayName.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("CF Notche Demo", 9F);
            label1.Location = new Point(35, 11);
            label1.Name = "label1";
            label1.Size = new Size(44, 12);
            label1.TabIndex = 0;
            label1.Text = "Name: ";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(33, -135);
            label3.Name = "label3";
            label3.Size = new Size(111, 16);
            label3.TabIndex = 4;
            label3.Text = "Using Custom Icon?";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel3);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("CF Notche Demo", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(742, 88);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Display";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.141304F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 88.858696F));
            tableLayoutPanel3.Controls.Add(label1, 0, 0);
            tableLayoutPanel3.Controls.Add(label2, 0, 1);
            tableLayoutPanel3.Controls.Add(txtDisplayName, 1, 0);
            tableLayoutPanel3.Controls.Add(cmbUIIcons, 1, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 17);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(736, 68);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // cmbUIIcons
            // 
            cmbUIIcons.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbUIIcons.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbUIIcons.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbUIIcons.Font = new Font("CF Notche Demo", 9F, FontStyle.Bold);
            cmbUIIcons.FormattingEnabled = true;
            cmbUIIcons.Location = new Point(85, 41);
            cmbUIIcons.Name = "cmbUIIcons";
            cmbUIIcons.Size = new Size(648, 20);
            cmbUIIcons.Sorted = true;
            cmbUIIcons.TabIndex = 3;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabDisplayAndIcon);
            tabControl.Controls.Add(tabMusic);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("CF Notche Demo", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabControl.ImageList = tabImages;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(758, 488);
            tabControl.TabIndex = 6;
            // 
            // tabDisplayAndIcon
            // 
            tabDisplayAndIcon.BorderStyle = BorderStyle.FixedSingle;
            tabDisplayAndIcon.Controls.Add(panel1);
            tabDisplayAndIcon.Font = new Font("CF Notche Demo", 9.749999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabDisplayAndIcon.ImageIndex = 0;
            tabDisplayAndIcon.Location = new Point(4, 25);
            tabDisplayAndIcon.Name = "tabDisplayAndIcon";
            tabDisplayAndIcon.Padding = new Padding(3);
            tabDisplayAndIcon.Size = new Size(750, 459);
            tabDisplayAndIcon.TabIndex = 0;
            tabDisplayAndIcon.Text = "Display and Icon";
            tabDisplayAndIcon.ToolTipText = "Change the display name and icon for this radio station.";
            tabDisplayAndIcon.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(groupBox3);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(742, 451);
            panel1.TabIndex = 3;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 351);
            panel3.Name = "panel3";
            panel3.Size = new Size(742, 100);
            panel3.TabIndex = 5;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(tableLayoutPanel1);
            groupBox3.Dock = DockStyle.Top;
            groupBox3.Font = new Font("CF Notche Demo", 9F, FontStyle.Bold);
            groupBox3.Location = new Point(0, 209);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(742, 142);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "Settings";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.440218F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 83.5597839F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 1, 1);
            tableLayoutPanel1.Controls.Add(label5, 0, 1);
            tableLayoutPanel1.Controls.Add(numericUpDown1, 1, 0);
            tableLayoutPanel1.Controls.Add(label4, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 17);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 21.3114758F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 78.68852F));
            tableLayoutPanel1.Size = new Size(736, 122);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(panel2, 0, 1);
            tableLayoutPanel4.Controls.Add(volumeSlider, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(124, 29);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 41.11111F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 58.88889F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Size = new Size(609, 90);
            tableLayoutPanel4.TabIndex = 6;
            // 
            // panel2
            // 
            panel2.Controls.Add(label8);
            panel2.Controls.Add(txtVolumeEdit);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(lblSelectedVolume);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 40);
            panel2.Name = "panel2";
            panel2.Size = new Size(603, 47);
            panel2.TabIndex = 5;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Left;
            label8.AutoSize = true;
            label8.Font = new Font("CF Notche Demo", 9.749999F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label8.Location = new Point(5, 28);
            label8.Name = "label8";
            label8.Size = new Size(104, 13);
            label8.TabIndex = 7;
            label8.Text = "Min: 0.0 - Max: 25.0";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtVolumeEdit
            // 
            txtVolumeEdit.Font = new Font("CF Notche Demo", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtVolumeEdit.Location = new Point(76, 3);
            txtVolumeEdit.Name = "txtVolumeEdit";
            txtVolumeEdit.Size = new Size(67, 22);
            txtVolumeEdit.TabIndex = 5;
            txtVolumeEdit.TabStop = false;
            txtVolumeEdit.Visible = false;
            txtVolumeEdit.KeyDown += txtVolumeEdit_KeyDown;
            txtVolumeEdit.KeyPress += txtVolumeEdit_KeyPress;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Font = new Font("CF Notche Demo", 11.25F, FontStyle.Bold);
            label7.Location = new Point(3, 7);
            label7.Name = "label7";
            label7.Size = new Size(49, 15);
            label7.TabIndex = 6;
            label7.Text = "Value:";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSelectedVolume
            // 
            lblSelectedVolume.Anchor = AnchorStyles.Left;
            lblSelectedVolume.AutoSize = true;
            lblSelectedVolume.Font = new Font("CF Notche Demo", 11.25F, FontStyle.Bold);
            lblSelectedVolume.Location = new Point(76, 7);
            lblSelectedVolume.Name = "lblSelectedVolume";
            lblSelectedVolume.Size = new Size(25, 15);
            lblSelectedVolume.TabIndex = 5;
            lblSelectedVolume.Text = "1.0";
            lblSelectedVolume.TextAlign = ContentAlignment.MiddleCenter;
            lblSelectedVolume.DoubleClick += lblSelectedVolume_DoubleClick;
            // 
            // volumeSlider
            // 
            volumeSlider.BackColor = Color.White;
            volumeSlider.Dock = DockStyle.Fill;
            volumeSlider.LargeChange = 5F;
            volumeSlider.Location = new Point(3, 3);
            volumeSlider.Maximum = 25F;
            volumeSlider.Minimum = 0F;
            volumeSlider.Name = "volumeSlider";
            volumeSlider.Precision = 0.1F;
            volumeSlider.Size = new Size(603, 31);
            volumeSlider.SmallChange = 0.1F;
            volumeSlider.TabIndex = 4;
            volumeSlider.TickFrequency = 5;
            volumeSlider.Value = 1F;
            volumeSlider.Scroll += volumeSlider_Scroll;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("CF Notche Demo", 9F);
            label5.Location = new Point(66, 68);
            label5.Name = "label5";
            label5.Size = new Size(52, 12);
            label5.TabIndex = 3;
            label5.Text = "Volume: ";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Anchor = AnchorStyles.Left;
            numericUpDown1.DecimalPlaces = 1;
            numericUpDown1.Font = new Font("CF Notche Demo", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            numericUpDown1.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown1.Location = new Point(124, 3);
            numericUpDown1.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(91, 22);
            numericUpDown1.TabIndex = 7;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("CF Notche Demo", 9F);
            label4.Location = new Point(91, 7);
            label4.Name = "label4";
            label4.Size = new Size(27, 12);
            label4.TabIndex = 2;
            label4.Text = "FM: ";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(tableLayoutPanel2);
            groupBox2.Dock = DockStyle.Top;
            groupBox2.Font = new Font("CF Notche Demo", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(0, 88);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(742, 121);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Custom Icon";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.440218F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 83.5597839F));
            tableLayoutPanel2.Controls.Add(txtInkAtlasPart, 1, 2);
            tableLayoutPanel2.Controls.Add(txtInkAtlasPath, 1, 1);
            tableLayoutPanel2.Controls.Add(label6, 0, 0);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel1, 1, 0);
            tableLayoutPanel2.Controls.Add(lblInkAtlasPart, 0, 2);
            tableLayoutPanel2.Controls.Add(lblInkAtlasPath, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 17);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 32.9545441F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 30.681818F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 36.363636F));
            tableLayoutPanel2.Size = new Size(736, 101);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // txtInkAtlasPart
            // 
            txtInkAtlasPart.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtInkAtlasPart.Font = new Font("CF Notche Demo", 9F, FontStyle.Bold);
            txtInkAtlasPart.Location = new Point(124, 71);
            txtInkAtlasPart.Name = "txtInkAtlasPart";
            txtInkAtlasPart.Size = new Size(609, 21);
            txtInkAtlasPart.TabIndex = 2;
            // 
            // txtInkAtlasPath
            // 
            txtInkAtlasPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtInkAtlasPath.Font = new Font("CF Notche Demo", 9F, FontStyle.Bold);
            txtInkAtlasPath.Location = new Point(124, 37);
            txtInkAtlasPath.Name = "txtInkAtlasPath";
            txtInkAtlasPath.Size = new Size(609, 21);
            txtInkAtlasPath.TabIndex = 3;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Font = new Font("CF Notche Demo", 9F);
            label6.Location = new Point(74, 10);
            label6.Name = "label6";
            label6.Size = new Size(44, 12);
            label6.TabIndex = 3;
            label6.Text = "Using?";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(radUseCustomYes);
            flowLayoutPanel1.Controls.Add(radUseCustomNo);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(124, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(609, 27);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // radUseCustomYes
            // 
            radUseCustomYes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            radUseCustomYes.AutoSize = true;
            radUseCustomYes.Checked = true;
            radUseCustomYes.Font = new Font("CF Notche Demo", 9F);
            radUseCustomYes.Location = new Point(3, 3);
            radUseCustomYes.Name = "radUseCustomYes";
            radUseCustomYes.Size = new Size(43, 16);
            radUseCustomYes.TabIndex = 5;
            radUseCustomYes.TabStop = true;
            radUseCustomYes.Text = "Yes";
            radUseCustomYes.UseVisualStyleBackColor = true;
            radUseCustomYes.CheckedChanged += radUseCustomYes_CheckedChanged;
            // 
            // radUseCustomNo
            // 
            radUseCustomNo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            radUseCustomNo.AutoSize = true;
            radUseCustomNo.Font = new Font("CF Notche Demo", 9F);
            radUseCustomNo.Location = new Point(52, 3);
            radUseCustomNo.Name = "radUseCustomNo";
            radUseCustomNo.Size = new Size(39, 16);
            radUseCustomNo.TabIndex = 6;
            radUseCustomNo.TabStop = true;
            radUseCustomNo.Text = "No";
            radUseCustomNo.UseVisualStyleBackColor = true;
            radUseCustomNo.CheckedChanged += radUseCustomNo_CheckedChanged;
            // 
            // lblInkAtlasPart
            // 
            lblInkAtlasPart.Anchor = AnchorStyles.Right;
            lblInkAtlasPart.AutoSize = true;
            lblInkAtlasPart.Font = new Font("CF Notche Demo", 9F);
            lblInkAtlasPart.Location = new Point(30, 76);
            lblInkAtlasPart.Name = "lblInkAtlasPart";
            lblInkAtlasPart.Size = new Size(88, 12);
            lblInkAtlasPart.TabIndex = 1;
            lblInkAtlasPart.Text = "Ink Atlas Part: ";
            // 
            // lblInkAtlasPath
            // 
            lblInkAtlasPath.Anchor = AnchorStyles.Right;
            lblInkAtlasPath.AutoSize = true;
            lblInkAtlasPath.Font = new Font("CF Notche Demo", 9F);
            lblInkAtlasPath.Location = new Point(27, 42);
            lblInkAtlasPath.Name = "lblInkAtlasPath";
            lblInkAtlasPath.Size = new Size(91, 12);
            lblInkAtlasPath.TabIndex = 0;
            lblInkAtlasPath.Text = "Ink Atlas Path: ";
            // 
            // tabMusic
            // 
            tabMusic.BorderStyle = BorderStyle.FixedSingle;
            tabMusic.Controls.Add(groupBox4);
            tabMusic.ImageIndex = 1;
            tabMusic.Location = new Point(4, 25);
            tabMusic.Name = "tabMusic";
            tabMusic.Padding = new Padding(3);
            tabMusic.Size = new Size(750, 459);
            tabMusic.TabIndex = 1;
            tabMusic.Text = "Music";
            tabMusic.ToolTipText = "Change the music this radio station will play.";
            tabMusic.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(tableLayoutPanel5);
            groupBox4.Dock = DockStyle.Top;
            groupBox4.Font = new Font("CF Notche Demo", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox4.Location = new Point(3, 3);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(742, 123);
            groupBox4.TabIndex = 0;
            groupBox4.TabStop = false;
            groupBox4.Text = "Stream Settings";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.081522F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 84.91848F));
            tableLayoutPanel5.Controls.Add(lblStreamURL, 0, 1);
            tableLayoutPanel5.Controls.Add(label9, 0, 0);
            tableLayoutPanel5.Controls.Add(flowLayoutPanel2, 1, 0);
            tableLayoutPanel5.Controls.Add(tableLayoutPanel6, 1, 1);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(3, 17);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 29.1262131F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 70.87379F));
            tableLayoutPanel5.Size = new Size(736, 103);
            tableLayoutPanel5.TabIndex = 0;
            // 
            // lblStreamURL
            // 
            lblStreamURL.Anchor = AnchorStyles.Right;
            lblStreamURL.AutoSize = true;
            lblStreamURL.Font = new Font("CF Notche Demo", 9F);
            lblStreamURL.Location = new Point(33, 60);
            lblStreamURL.Name = "lblStreamURL";
            lblStreamURL.Size = new Size(75, 12);
            lblStreamURL.TabIndex = 3;
            lblStreamURL.Text = "Stream URL:";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Font = new Font("CF Notche Demo", 9F);
            label9.Location = new Point(30, 9);
            label9.Name = "label9";
            label9.Size = new Size(78, 12);
            label9.TabIndex = 1;
            label9.Text = "Use Stream?";
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(radUseStreamYes);
            flowLayoutPanel2.Controls.Add(radUseStreamNo);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Location = new Point(114, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(619, 24);
            flowLayoutPanel2.TabIndex = 2;
            // 
            // radUseStreamYes
            // 
            radUseStreamYes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            radUseStreamYes.AutoSize = true;
            radUseStreamYes.Checked = true;
            radUseStreamYes.Font = new Font("CF Notche Demo", 9F);
            radUseStreamYes.Location = new Point(3, 3);
            radUseStreamYes.Name = "radUseStreamYes";
            radUseStreamYes.Size = new Size(43, 16);
            radUseStreamYes.TabIndex = 7;
            radUseStreamYes.TabStop = true;
            radUseStreamYes.Text = "Yes";
            radUseStreamYes.UseVisualStyleBackColor = true;
            radUseStreamYes.CheckedChanged += radUseStreamYes_CheckedChanged;
            // 
            // radUseStreamNo
            // 
            radUseStreamNo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            radUseStreamNo.AutoSize = true;
            radUseStreamNo.Font = new Font("CF Notche Demo", 9F);
            radUseStreamNo.Location = new Point(52, 3);
            radUseStreamNo.Name = "radUseStreamNo";
            radUseStreamNo.Size = new Size(39, 16);
            radUseStreamNo.TabIndex = 8;
            radUseStreamNo.TabStop = true;
            radUseStreamNo.Text = "No";
            radUseStreamNo.UseVisualStyleBackColor = true;
            radUseStreamNo.CheckedChanged += radUseStreamNo_CheckedChanged;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 2;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90.03165F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9.968354F));
            tableLayoutPanel6.Controls.Add(flowLayoutPanel3, 0, 1);
            tableLayoutPanel6.Controls.Add(txtStreamURL, 0, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(114, 33);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new Size(619, 67);
            tableLayoutPanel6.TabIndex = 4;
            // 
            // flowLayoutPanel3
            // 
            tableLayoutPanel6.SetColumnSpan(flowLayoutPanel3, 2);
            flowLayoutPanel3.Controls.Add(btnGetRadioGardenURL);
            flowLayoutPanel3.Controls.Add(txtPastedURL);
            flowLayoutPanel3.Dock = DockStyle.Fill;
            flowLayoutPanel3.Location = new Point(3, 36);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(613, 28);
            flowLayoutPanel3.TabIndex = 1;
            // 
            // btnGetRadioGardenURL
            // 
            btnGetRadioGardenURL.Location = new Point(3, 3);
            btnGetRadioGardenURL.Name = "btnGetRadioGardenURL";
            btnGetRadioGardenURL.Size = new Size(177, 23);
            btnGetRadioGardenURL.TabIndex = 1;
            btnGetRadioGardenURL.Text = "Get URL from Radio Garden";
            btnGetRadioGardenURL.UseVisualStyleBackColor = true;
            btnGetRadioGardenURL.Click += btnGetRadioGardenURL_Click;
            // 
            // txtPastedURL
            // 
            txtPastedURL.Font = new Font("CF Notche Demo", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtPastedURL.Location = new Point(186, 3);
            txtPastedURL.Name = "txtPastedURL";
            txtPastedURL.PlaceholderText = "(Ctrl + V) - Paste Radio Garden URL here";
            txtPastedURL.Size = new Size(244, 21);
            txtPastedURL.TabIndex = 2;
            txtPastedURL.Visible = false;
            txtPastedURL.TextChanged += txtPastedURL_TextChanged;
            txtPastedURL.KeyDown += txtPastedURL_KeyDown;
            // 
            // txtStreamURL
            // 
            txtStreamURL.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel6.SetColumnSpan(txtStreamURL, 2);
            txtStreamURL.Location = new Point(3, 6);
            txtStreamURL.Name = "txtStreamURL";
            txtStreamURL.Size = new Size(613, 21);
            txtStreamURL.TabIndex = 0;
            // 
            // tabImages
            // 
            tabImages.ColorDepth = ColorDepth.Depth32Bit;
            tabImages.ImageStream = (ImageListStreamer)resources.GetObject("tabImages.ImageStream");
            tabImages.TransparentColor = Color.Transparent;
            tabImages.Images.SetKeyName(0, "display-frame.png");
            tabImages.Images.SetKeyName(1, "sound-waves.png");
            // 
            // StationEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tabControl);
            Controls.Add(label3);
            Name = "StationEditor";
            Size = new Size(758, 488);
            Load += StationEditor_Load;
            groupBox1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tabControl.ResumeLayout(false);
            tabDisplayAndIcon.ResumeLayout(false);
            panel1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)volumeSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            groupBox2.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tabMusic.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private TextBox txtDisplayName;
        private Label label2;
        private Label label3;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel3;
        private TabControl tabControl;
        private TabPage tabDisplayAndIcon;
        private TabPage tabMusic;
        private ImageList tabImages;
        private GroupBox groupBox2;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox txtInkAtlasPart;
        private TextBox txtInkAtlasPath;
        private Label label6;
        private FlowLayoutPanel flowLayoutPanel1;
        private RadioButton radUseCustomYes;
        private RadioButton radUseCustomNo;
        private Label lblInkAtlasPart;
        private Label lblInkAtlasPath;
        private ComboBox cmbUIIcons;
        private Panel panel1;
        private GroupBox groupBox3;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label5;
        private Label label4;
        private FloatTrackBar volumeSlider;
        private TableLayoutPanel tableLayoutPanel4;
        private Label lblSelectedVolume;
        private NumericUpDown numericUpDown1;
        private Panel panel2;
        private Label label7;
        private TextBox txtVolumeEdit;
        private Label label8;
        private Panel panel3;
        private GroupBox groupBox4;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label9;
        private FlowLayoutPanel flowLayoutPanel2;
        private RadioButton radUseStreamYes;
        private RadioButton radUseStreamNo;
        private Label lblStreamURL;
        private TableLayoutPanel tableLayoutPanel6;
        private TextBox txtStreamURL;
        private Button btnGetRadioGardenURL;
        private FlowLayoutPanel flowLayoutPanel3;
        private TextBox txtPastedURL;
    }
}
