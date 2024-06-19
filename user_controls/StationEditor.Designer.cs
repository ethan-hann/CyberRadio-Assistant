namespace RadioExt_Helper.user_controls
{
    sealed partial class StationEditor
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
            lblIcon = new Label();
            txtDisplayName = new TextBox();
            lblName = new Label();
            label3 = new Label();
            grpDisplay = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            cmbUIIcons = new ComboBox();
            tabControl = new TabControl();
            tabDisplayAndIcon = new TabPage();
            panel1 = new Panel();
            panel3 = new Panel();
            grpSettings = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblVolume = new Label();
            nudFM = new NumericUpDown();
            lblFM = new Label();
            flowLayoutPanel4 = new FlowLayoutPanel();
            volumeSlider = new TrackBar();
            lblVolumeVal = new Label();
            panel2 = new Panel();
            txtVolumeEdit = new TextBox();
            lblSelectedVolume = new Label();
            lblVolumeMinMax = new Label();
            grpCustomIcon = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            txtInkAtlasPart = new TextBox();
            txtInkAtlasPath = new TextBox();
            lblUsingCustomIcon = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            radUseCustomYes = new RadioButton();
            radUseCustomNo = new RadioButton();
            lblInkPart = new Label();
            lblInkPath = new Label();
            tabMusic = new TabPage();
            grpSongs = new GroupBox();
            grpStreamSettings = new GroupBox();
            tableLayoutPanel5 = new TableLayoutPanel();
            lblStreamURL = new Label();
            lblUseStream = new Label();
            flowLayoutPanel2 = new FlowLayoutPanel();
            radUseStreamYes = new RadioButton();
            radUseStreamNo = new RadioButton();
            tableLayoutPanel6 = new TableLayoutPanel();
            flowLayoutPanel3 = new FlowLayoutPanel();
            mpStreamPlayer = new MusicPlayer();
            txtStreamURL = new TextBox();
            tabImages = new ImageList(components);
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            grpDisplay.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tabControl.SuspendLayout();
            tabDisplayAndIcon.SuspendLayout();
            panel1.SuspendLayout();
            grpSettings.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudFM).BeginInit();
            flowLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)volumeSlider).BeginInit();
            panel2.SuspendLayout();
            grpCustomIcon.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            tabMusic.SuspendLayout();
            grpStreamSettings.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lblIcon
            // 
            lblIcon.Anchor = AnchorStyles.Right;
            lblIcon.AutoSize = true;
            lblIcon.Font = new Font("CF Notche Demo", 9F);
            lblIcon.Location = new Point(68, 63);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(36, 12);
            lblIcon.TabIndex = 2;
            lblIcon.Text = "Icon: ";
            lblIcon.MouseEnter += lblIcon_MouseEnter;
            lblIcon.MouseLeave += lbl_MouseLeave;
            // 
            // txtDisplayName
            // 
            txtDisplayName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDisplayName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtDisplayName.Location = new Point(110, 9);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Size = new Size(849, 23);
            txtDisplayName.TabIndex = 1;
            txtDisplayName.TextChanged += txtDisplayName_TextChanged;
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Right;
            lblName.AutoSize = true;
            lblName.Font = new Font("CF Notche Demo", 9F);
            lblName.Location = new Point(60, 15);
            lblName.Name = "lblName";
            lblName.Size = new Size(44, 12);
            lblName.TabIndex = 0;
            lblName.Text = "Name: ";
            lblName.MouseEnter += lblName_MouseEnter;
            lblName.MouseLeave += lbl_MouseLeave;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(259, 2);
            label3.Name = "label3";
            label3.Size = new Size(113, 15);
            label3.TabIndex = 4;
            label3.Text = "Using Custom Icon?";
            // 
            // grpDisplay
            // 
            grpDisplay.BackColor = Color.White;
            grpDisplay.Controls.Add(tableLayoutPanel3);
            grpDisplay.Dock = DockStyle.Top;
            grpDisplay.Font = new Font("CF Notche Demo", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grpDisplay.Location = new Point(0, 0);
            grpDisplay.Name = "grpDisplay";
            grpDisplay.Size = new Size(968, 116);
            grpDisplay.TabIndex = 2;
            grpDisplay.TabStop = false;
            grpDisplay.Text = "Display";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.141304F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 88.858696F));
            tableLayoutPanel3.Controls.Add(lblName, 0, 0);
            tableLayoutPanel3.Controls.Add(lblIcon, 0, 1);
            tableLayoutPanel3.Controls.Add(txtDisplayName, 1, 0);
            tableLayoutPanel3.Controls.Add(cmbUIIcons, 1, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 17);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 44.3038F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 55.6962F));
            tableLayoutPanel3.Size = new Size(962, 96);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // cmbUIIcons
            // 
            cmbUIIcons.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbUIIcons.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbUIIcons.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbUIIcons.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cmbUIIcons.FormattingEnabled = true;
            cmbUIIcons.Location = new Point(110, 57);
            cmbUIIcons.Name = "cmbUIIcons";
            cmbUIIcons.Size = new Size(849, 23);
            cmbUIIcons.Sorted = true;
            cmbUIIcons.TabIndex = 3;
            cmbUIIcons.SelectedIndexChanged += cmbUIIcons_SelectedIndexChanged;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabDisplayAndIcon);
            tabControl.Controls.Add(tabMusic);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabControl.ImageList = tabImages;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(984, 691);
            tabControl.TabIndex = 6;
            // 
            // tabDisplayAndIcon
            // 
            tabDisplayAndIcon.BorderStyle = BorderStyle.FixedSingle;
            tabDisplayAndIcon.Controls.Add(panel1);
            tabDisplayAndIcon.Font = new Font("CF Notche Demo", 9.749999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabDisplayAndIcon.ImageIndex = 0;
            tabDisplayAndIcon.Location = new Point(4, 29);
            tabDisplayAndIcon.Name = "tabDisplayAndIcon";
            tabDisplayAndIcon.Padding = new Padding(3);
            tabDisplayAndIcon.Size = new Size(976, 658);
            tabDisplayAndIcon.TabIndex = 0;
            tabDisplayAndIcon.Text = "Display and Icon";
            tabDisplayAndIcon.ToolTipText = "Change the display name and icon for this radio station.";
            tabDisplayAndIcon.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(grpSettings);
            panel1.Controls.Add(grpCustomIcon);
            panel1.Controls.Add(grpDisplay);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(968, 650);
            panel1.TabIndex = 3;
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 377);
            panel3.Name = "panel3";
            panel3.Size = new Size(968, 273);
            panel3.TabIndex = 5;
            // 
            // grpSettings
            // 
            grpSettings.BackColor = Color.White;
            grpSettings.Controls.Add(tableLayoutPanel1);
            grpSettings.Dock = DockStyle.Top;
            grpSettings.Font = new Font("CF Notche Demo", 9F, FontStyle.Bold);
            grpSettings.Location = new Point(0, 250);
            grpSettings.Name = "grpSettings";
            grpSettings.Size = new Size(968, 127);
            grpSettings.TabIndex = 4;
            grpSettings.TabStop = false;
            grpSettings.Text = "Settings";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.440218F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 83.5597839F));
            tableLayoutPanel1.Controls.Add(lblVolume, 0, 1);
            tableLayoutPanel1.Controls.Add(nudFM, 1, 0);
            tableLayoutPanel1.Controls.Add(lblFM, 0, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel4, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 17);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 41.09589F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 58.90411F));
            tableLayoutPanel1.Size = new Size(962, 107);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblVolume
            // 
            lblVolume.Anchor = AnchorStyles.Right;
            lblVolume.AutoSize = true;
            lblVolume.Font = new Font("CF Notche Demo", 9F);
            lblVolume.Location = new Point(103, 69);
            lblVolume.Name = "lblVolume";
            lblVolume.Size = new Size(52, 12);
            lblVolume.TabIndex = 3;
            lblVolume.Text = "Volume: ";
            lblVolume.MouseEnter += lblVolume_MouseEnter;
            lblVolume.MouseLeave += lbl_MouseLeave;
            // 
            // nudFM
            // 
            nudFM.Anchor = AnchorStyles.Left;
            nudFM.DecimalPlaces = 2;
            nudFM.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nudFM.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudFM.Location = new Point(161, 10);
            nudFM.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            nudFM.Name = "nudFM";
            nudFM.Size = new Size(91, 23);
            nudFM.TabIndex = 7;
            nudFM.ValueChanged += nudFM_ValueChanged;
            // 
            // lblFM
            // 
            lblFM.Anchor = AnchorStyles.Right;
            lblFM.AutoSize = true;
            lblFM.Font = new Font("CF Notche Demo", 9F);
            lblFM.Location = new Point(128, 15);
            lblFM.Name = "lblFM";
            lblFM.Size = new Size(27, 12);
            lblFM.TabIndex = 2;
            lblFM.Text = "FM: ";
            lblFM.MouseEnter += lblFM_MouseEnter;
            lblFM.MouseLeave += lbl_MouseLeave;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.BackColor = Color.White;
            flowLayoutPanel4.Controls.Add(volumeSlider);
            flowLayoutPanel4.Controls.Add(lblVolumeVal);
            flowLayoutPanel4.Controls.Add(panel2);
            flowLayoutPanel4.Controls.Add(lblVolumeMinMax);
            flowLayoutPanel4.Dock = DockStyle.Fill;
            flowLayoutPanel4.Location = new Point(161, 46);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(798, 58);
            flowLayoutPanel4.TabIndex = 8;
            // 
            // volumeSlider
            // 
            volumeSlider.BackColor = Color.White;
            volumeSlider.LargeChange = 50;
            volumeSlider.Location = new Point(3, 3);
            volumeSlider.Maximum = 250;
            volumeSlider.Name = "volumeSlider";
            volumeSlider.Size = new Size(263, 45);
            volumeSlider.SmallChange = 10;
            volumeSlider.TabIndex = 6;
            volumeSlider.TickStyle = TickStyle.None;
            volumeSlider.Scroll += volumeSlider_Scroll;
            // 
            // lblVolumeVal
            // 
            lblVolumeVal.Anchor = AnchorStyles.Left;
            lblVolumeVal.AutoSize = true;
            lblVolumeVal.Font = new Font("CF Notche Demo", 9.749999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblVolumeVal.Location = new Point(269, 19);
            lblVolumeVal.Margin = new Padding(0);
            lblVolumeVal.Name = "lblVolumeVal";
            lblVolumeVal.Size = new Size(43, 13);
            lblVolumeVal.TabIndex = 6;
            lblVolumeVal.Text = "Value:";
            lblVolumeVal.TextAlign = ContentAlignment.MiddleCenter;
            lblVolumeVal.MouseEnter += lblVolumeVal_MouseEnter;
            lblVolumeVal.MouseLeave += lbl_MouseLeave;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Left;
            panel2.Controls.Add(txtVolumeEdit);
            panel2.Controls.Add(lblSelectedVolume);
            panel2.Location = new Point(312, 10);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(71, 30);
            panel2.TabIndex = 8;
            // 
            // txtVolumeEdit
            // 
            txtVolumeEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtVolumeEdit.Location = new Point(1, 4);
            txtVolumeEdit.Name = "txtVolumeEdit";
            txtVolumeEdit.Size = new Size(67, 23);
            txtVolumeEdit.TabIndex = 5;
            txtVolumeEdit.TabStop = false;
            txtVolumeEdit.Visible = false;
            txtVolumeEdit.KeyDown += txtVolumeEdit_KeyDown;
            txtVolumeEdit.KeyPress += txtVolumeEdit_KeyPress;
            // 
            // lblSelectedVolume
            // 
            lblSelectedVolume.Anchor = AnchorStyles.Left;
            lblSelectedVolume.AutoSize = true;
            lblSelectedVolume.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSelectedVolume.Location = new Point(2, 5);
            lblSelectedVolume.Name = "lblSelectedVolume";
            lblSelectedVolume.Size = new Size(31, 20);
            lblSelectedVolume.TabIndex = 5;
            lblSelectedVolume.Text = "1.0";
            lblSelectedVolume.TextAlign = ContentAlignment.MiddleCenter;
            lblSelectedVolume.DoubleClick += lblSelectedVolume_DoubleClick;
            // 
            // lblVolumeMinMax
            // 
            lblVolumeMinMax.Anchor = AnchorStyles.Left;
            lblVolumeMinMax.AutoSize = true;
            lblVolumeMinMax.Font = new Font("CF Notche Demo", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblVolumeMinMax.Location = new Point(386, 19);
            lblVolumeMinMax.Name = "lblVolumeMinMax";
            lblVolumeMinMax.Size = new Size(98, 12);
            lblVolumeMinMax.TabIndex = 7;
            lblVolumeMinMax.Text = "Min: 0.0 - Max: 25.0";
            lblVolumeMinMax.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // grpCustomIcon
            // 
            grpCustomIcon.BackColor = Color.White;
            grpCustomIcon.Controls.Add(tableLayoutPanel2);
            grpCustomIcon.Dock = DockStyle.Top;
            grpCustomIcon.Font = new Font("CF Notche Demo", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grpCustomIcon.Location = new Point(0, 116);
            grpCustomIcon.Name = "grpCustomIcon";
            grpCustomIcon.Size = new Size(968, 134);
            grpCustomIcon.TabIndex = 3;
            grpCustomIcon.TabStop = false;
            grpCustomIcon.Text = "Custom Icon";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.440218F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 83.5597839F));
            tableLayoutPanel2.Controls.Add(txtInkAtlasPart, 1, 2);
            tableLayoutPanel2.Controls.Add(txtInkAtlasPath, 1, 1);
            tableLayoutPanel2.Controls.Add(lblUsingCustomIcon, 0, 0);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel1, 1, 0);
            tableLayoutPanel2.Controls.Add(lblInkPart, 0, 2);
            tableLayoutPanel2.Controls.Add(lblInkPath, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 17);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 32.9545441F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 30.681818F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 36.363636F));
            tableLayoutPanel2.Size = new Size(962, 114);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // txtInkAtlasPart
            // 
            txtInkAtlasPart.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtInkAtlasPart.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtInkAtlasPart.Location = new Point(161, 81);
            txtInkAtlasPart.Name = "txtInkAtlasPart";
            txtInkAtlasPart.Size = new Size(798, 23);
            txtInkAtlasPart.TabIndex = 2;
            txtInkAtlasPart.TextChanged += txtInkAtlasPart_TextChanged;
            // 
            // txtInkAtlasPath
            // 
            txtInkAtlasPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtInkAtlasPath.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtInkAtlasPath.Location = new Point(161, 42);
            txtInkAtlasPath.Name = "txtInkAtlasPath";
            txtInkAtlasPath.Size = new Size(798, 23);
            txtInkAtlasPath.TabIndex = 3;
            txtInkAtlasPath.TextChanged += txtInkAtlasPath_TextChanged;
            // 
            // lblUsingCustomIcon
            // 
            lblUsingCustomIcon.Anchor = AnchorStyles.Right;
            lblUsingCustomIcon.AutoSize = true;
            lblUsingCustomIcon.Font = new Font("CF Notche Demo", 9F);
            lblUsingCustomIcon.Location = new Point(111, 12);
            lblUsingCustomIcon.Name = "lblUsingCustomIcon";
            lblUsingCustomIcon.Size = new Size(44, 12);
            lblUsingCustomIcon.TabIndex = 3;
            lblUsingCustomIcon.Text = "Using?";
            lblUsingCustomIcon.MouseEnter += lblUsingCustomIcon_MouseEnter;
            lblUsingCustomIcon.MouseLeave += lbl_MouseLeave;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(radUseCustomYes);
            flowLayoutPanel1.Controls.Add(radUseCustomNo);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(161, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(798, 31);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // radUseCustomYes
            // 
            radUseCustomYes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            radUseCustomYes.AutoSize = true;
            radUseCustomYes.Checked = true;
            radUseCustomYes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radUseCustomYes.Location = new Point(3, 3);
            radUseCustomYes.Name = "radUseCustomYes";
            radUseCustomYes.Size = new Size(43, 19);
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
            radUseCustomNo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radUseCustomNo.Location = new Point(52, 3);
            radUseCustomNo.Name = "radUseCustomNo";
            radUseCustomNo.Size = new Size(41, 19);
            radUseCustomNo.TabIndex = 6;
            radUseCustomNo.TabStop = true;
            radUseCustomNo.Text = "No";
            radUseCustomNo.UseVisualStyleBackColor = true;
            radUseCustomNo.CheckedChanged += radUseCustomNo_CheckedChanged;
            // 
            // lblInkPart
            // 
            lblInkPart.Anchor = AnchorStyles.Right;
            lblInkPart.AutoSize = true;
            lblInkPart.Font = new Font("CF Notche Demo", 9F);
            lblInkPart.Location = new Point(67, 86);
            lblInkPart.Name = "lblInkPart";
            lblInkPart.Size = new Size(88, 12);
            lblInkPart.TabIndex = 1;
            lblInkPart.Text = "Ink Atlas Part: ";
            lblInkPart.MouseEnter += lblInkPart_MouseEnter;
            lblInkPart.MouseLeave += lbl_MouseLeave;
            // 
            // lblInkPath
            // 
            lblInkPath.Anchor = AnchorStyles.Right;
            lblInkPath.AutoSize = true;
            lblInkPath.Font = new Font("CF Notche Demo", 9F);
            lblInkPath.Location = new Point(64, 48);
            lblInkPath.Name = "lblInkPath";
            lblInkPath.Size = new Size(91, 12);
            lblInkPath.TabIndex = 0;
            lblInkPath.Text = "Ink Atlas Path: ";
            lblInkPath.MouseEnter += lblInkPath_MouseEnter;
            lblInkPath.MouseLeave += lbl_MouseLeave;
            // 
            // tabMusic
            // 
            tabMusic.BackColor = Color.White;
            tabMusic.BorderStyle = BorderStyle.FixedSingle;
            tabMusic.Controls.Add(grpSongs);
            tabMusic.Controls.Add(grpStreamSettings);
            tabMusic.ImageIndex = 1;
            tabMusic.Location = new Point(4, 29);
            tabMusic.Name = "tabMusic";
            tabMusic.Padding = new Padding(3);
            tabMusic.Size = new Size(976, 658);
            tabMusic.TabIndex = 1;
            tabMusic.Text = "Music";
            tabMusic.ToolTipText = "Change the music this radio station will play.";
            // 
            // grpSongs
            // 
            grpSongs.Dock = DockStyle.Fill;
            grpSongs.Font = new Font("CF Notche Demo", 9F, FontStyle.Bold);
            grpSongs.Location = new Point(3, 164);
            grpSongs.Name = "grpSongs";
            grpSongs.Size = new Size(968, 489);
            grpSongs.TabIndex = 1;
            grpSongs.TabStop = false;
            grpSongs.Text = "Songs";
            // 
            // grpStreamSettings
            // 
            grpStreamSettings.Controls.Add(tableLayoutPanel5);
            grpStreamSettings.Dock = DockStyle.Top;
            grpStreamSettings.Font = new Font("CF Notche Demo", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grpStreamSettings.Location = new Point(3, 3);
            grpStreamSettings.Name = "grpStreamSettings";
            grpStreamSettings.Size = new Size(968, 161);
            grpStreamSettings.TabIndex = 0;
            grpStreamSettings.TabStop = false;
            grpStreamSettings.Text = "Stream Settings";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.081522F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 84.91848F));
            tableLayoutPanel5.Controls.Add(lblStreamURL, 0, 1);
            tableLayoutPanel5.Controls.Add(lblUseStream, 0, 0);
            tableLayoutPanel5.Controls.Add(flowLayoutPanel2, 1, 0);
            tableLayoutPanel5.Controls.Add(tableLayoutPanel6, 1, 1);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(3, 17);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 29.1262131F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 70.87379F));
            tableLayoutPanel5.Size = new Size(962, 141);
            tableLayoutPanel5.TabIndex = 0;
            // 
            // lblStreamURL
            // 
            lblStreamURL.Anchor = AnchorStyles.Right;
            lblStreamURL.AutoSize = true;
            lblStreamURL.Font = new Font("CF Notche Demo", 9F);
            lblStreamURL.Location = new Point(67, 85);
            lblStreamURL.Name = "lblStreamURL";
            lblStreamURL.Size = new Size(75, 12);
            lblStreamURL.TabIndex = 3;
            lblStreamURL.Text = "Stream URL:";
            lblStreamURL.MouseEnter += lblStreamURL_MouseEnter;
            lblStreamURL.MouseLeave += lbl_MouseLeave;
            // 
            // lblUseStream
            // 
            lblUseStream.Anchor = AnchorStyles.Right;
            lblUseStream.AutoSize = true;
            lblUseStream.Font = new Font("CF Notche Demo", 9F);
            lblUseStream.Location = new Point(64, 14);
            lblUseStream.Name = "lblUseStream";
            lblUseStream.Size = new Size(78, 12);
            lblUseStream.TabIndex = 1;
            lblUseStream.Text = "Use Stream?";
            lblUseStream.MouseEnter += lblUseStream_MouseEnter;
            lblUseStream.MouseLeave += lbl_MouseLeave;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(radUseStreamYes);
            flowLayoutPanel2.Controls.Add(radUseStreamNo);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Location = new Point(148, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(811, 35);
            flowLayoutPanel2.TabIndex = 2;
            // 
            // radUseStreamYes
            // 
            radUseStreamYes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            radUseStreamYes.AutoSize = true;
            radUseStreamYes.Checked = true;
            radUseStreamYes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radUseStreamYes.Location = new Point(3, 3);
            radUseStreamYes.Name = "radUseStreamYes";
            radUseStreamYes.Size = new Size(43, 19);
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
            radUseStreamNo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radUseStreamNo.Location = new Point(52, 3);
            radUseStreamNo.Name = "radUseStreamNo";
            radUseStreamNo.Size = new Size(41, 19);
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
            tableLayoutPanel6.Location = new Point(148, 44);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new Size(811, 94);
            tableLayoutPanel6.TabIndex = 4;
            // 
            // flowLayoutPanel3
            // 
            tableLayoutPanel6.SetColumnSpan(flowLayoutPanel3, 2);
            flowLayoutPanel3.Controls.Add(mpStreamPlayer);
            flowLayoutPanel3.Dock = DockStyle.Fill;
            flowLayoutPanel3.Location = new Point(3, 50);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(805, 41);
            flowLayoutPanel3.TabIndex = 1;
            // 
            // mpStreamPlayer
            // 
            mpStreamPlayer.BackColor = Color.Transparent;
            mpStreamPlayer.Location = new Point(3, 2);
            mpStreamPlayer.Margin = new Padding(3, 2, 3, 2);
            mpStreamPlayer.Name = "mpStreamPlayer";
            mpStreamPlayer.Size = new Size(32, 32);
            mpStreamPlayer.StreamURL = "";
            mpStreamPlayer.TabIndex = 3;
            // 
            // txtStreamURL
            // 
            txtStreamURL.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel6.SetColumnSpan(txtStreamURL, 2);
            txtStreamURL.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtStreamURL.Location = new Point(3, 12);
            txtStreamURL.Name = "txtStreamURL";
            txtStreamURL.Size = new Size(805, 23);
            txtStreamURL.TabIndex = 0;
            txtStreamURL.TextChanged += txtStreamURL_TextChanged;
            // 
            // tabImages
            // 
            tabImages.ColorDepth = ColorDepth.Depth32Bit;
            tabImages.ImageStream = (ImageListStreamer)resources.GetObject("tabImages.ImageStream");
            tabImages.TransparentColor = Color.Transparent;
            tabImages.Images.SetKeyName(0, "display-frame.png");
            tabImages.Images.SetKeyName(1, "sound-waves.png");
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip1.Location = new Point(0, 691);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(984, 25);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 7;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Image = Properties.Resources.info;
            lblStatus.Margin = new Padding(5, 3, 0, 2);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(2);
            lblStatus.Size = new Size(59, 20);
            lblStatus.Text = "Ready";
            // 
            // StationEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tabControl);
            Controls.Add(statusStrip1);
            Controls.Add(label3);
            Name = "StationEditor";
            Size = new Size(984, 716);
            Load += StationEditor_Load;
            grpDisplay.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tabControl.ResumeLayout(false);
            tabDisplayAndIcon.ResumeLayout(false);
            panel1.ResumeLayout(false);
            grpSettings.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudFM).EndInit();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)volumeSlider).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            grpCustomIcon.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tabMusic.ResumeLayout(false);
            grpStreamSettings.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblName;
        private TextBox txtDisplayName;
        private Label lblIcon;
        private Label label3;
        private GroupBox grpDisplay;
        private TableLayoutPanel tableLayoutPanel3;
        private TabControl tabControl;
        private TabPage tabDisplayAndIcon;
        private TabPage tabMusic;
        private ImageList tabImages;
        private GroupBox grpCustomIcon;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox txtInkAtlasPart;
        private TextBox txtInkAtlasPath;
        private Label lblUsingCustomIcon;
        private FlowLayoutPanel flowLayoutPanel1;
        private RadioButton radUseCustomYes;
        private RadioButton radUseCustomNo;
        private Label lblInkPart;
        private Label lblInkPath;
        private ComboBox cmbUIIcons;
        private Panel panel1;
        private GroupBox grpSettings;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblVolume;
        private Label lblFM;
        private Label lblSelectedVolume;
        private NumericUpDown nudFM;
        private Label lblVolumeVal;
        private TextBox txtVolumeEdit;
        private Label lblVolumeMinMax;
        private Panel panel3;
        private GroupBox grpStreamSettings;
        private TableLayoutPanel tableLayoutPanel5;
        private Label lblUseStream;
        private FlowLayoutPanel flowLayoutPanel2;
        private RadioButton radUseStreamYes;
        private RadioButton radUseStreamNo;
        private Label lblStreamURL;
        private TableLayoutPanel tableLayoutPanel6;
        private TextBox txtStreamURL;
        private FlowLayoutPanel flowLayoutPanel3;
        private TrackBar volumeSlider;
        private FlowLayoutPanel flowLayoutPanel4;
        private Panel panel2;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private GroupBox grpSongs;
        private MusicPlayer mpStreamPlayer;
    }
}
