using RadioExt_Helper.custom_controls;

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
            models.ImageProperties imageProperties3 = new models.ImageProperties();
            txtDisplayName = new TextBox();
            lblName = new Label();
            label3 = new Label();
            grpDisplay = new GroupBox();
            tlpDisplayTable = new TableLayoutPanel();
            tabControl = new TabControl();
            tabDisplayAndIcon = new TabPage();
            panel1 = new Panel();
            grpNotes = new GroupBox();
            dgvMetadata = new DataGridView();
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
            tlpCustomIcon = new TableLayoutPanel();
            txtInkAtlasPart = new TextBox();
            txtInkAtlasPath = new TextBox();
            lblUsingCustomIcon = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            radUseCustomYes = new RadioButton();
            radUseCustomNo = new RadioButton();
            btnOpenIconManager = new Button();
            lblInkPart = new Label();
            lblInkPath = new Label();
            picStationIcon = new CustomPictureBox();
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
            flpStreamUrlTesting = new FlowLayoutPanel();
            mpStreamPlayer = new MusicPlayer();
            btnGetFromRadioGarden = new Button();
            txtStreamURL = new TextBox();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblIcon = new Label();
            picGameIcon = new PictureBox();
            grpDisplay.SuspendLayout();
            tlpDisplayTable.SuspendLayout();
            tabControl.SuspendLayout();
            tabDisplayAndIcon.SuspendLayout();
            panel1.SuspendLayout();
            grpNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMetadata).BeginInit();
            grpSettings.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudFM).BeginInit();
            flowLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)volumeSlider).BeginInit();
            panel2.SuspendLayout();
            grpCustomIcon.SuspendLayout();
            tlpCustomIcon.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picStationIcon).BeginInit();
            tabMusic.SuspendLayout();
            grpStreamSettings.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            flpStreamUrlTesting.SuspendLayout();
            statusStrip1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picGameIcon).BeginInit();
            SuspendLayout();
            // 
            // txtDisplayName
            // 
            txtDisplayName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDisplayName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtDisplayName.Location = new Point(110, 10);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Size = new Size(849, 23);
            txtDisplayName.TabIndex = 1;
            txtDisplayName.TextChanged += TxtDisplayName_TextChanged;
            txtDisplayName.Leave += TxtDisplayName_Leave;
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Right;
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI Variable Text", 9F);
            lblName.Location = new Point(60, 14);
            lblName.Name = "lblName";
            lblName.Size = new Size(44, 16);
            lblName.TabIndex = 0;
            lblName.Text = "Name: ";
            lblName.MouseEnter += LblName_MouseEnter;
            lblName.MouseLeave += Lbl_MouseLeave;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(259, 2);
            label3.Name = "label3";
            label3.Size = new Size(111, 16);
            label3.TabIndex = 4;
            label3.Text = "Using Custom Icon?";
            // 
            // grpDisplay
            // 
            grpDisplay.BackColor = Color.White;
            grpDisplay.Controls.Add(tlpDisplayTable);
            grpDisplay.Dock = DockStyle.Top;
            grpDisplay.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpDisplay.Location = new Point(0, 0);
            grpDisplay.Name = "grpDisplay";
            grpDisplay.Size = new Size(968, 124);
            grpDisplay.TabIndex = 2;
            grpDisplay.TabStop = false;
            grpDisplay.Text = "Display";
            // 
            // tlpDisplayTable
            // 
            tlpDisplayTable.ColumnCount = 2;
            tlpDisplayTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.141304F));
            tlpDisplayTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 88.858696F));
            tlpDisplayTable.Controls.Add(lblName, 0, 0);
            tlpDisplayTable.Controls.Add(txtDisplayName, 1, 0);
            tlpDisplayTable.Controls.Add(tableLayoutPanel2, 0, 1);
            tlpDisplayTable.Dock = DockStyle.Fill;
            tlpDisplayTable.Location = new Point(3, 21);
            tlpDisplayTable.Name = "tlpDisplayTable";
            tlpDisplayTable.RowCount = 2;
            tlpDisplayTable.RowStyles.Add(new RowStyle(SizeType.Percent, 44.3038F));
            tlpDisplayTable.RowStyles.Add(new RowStyle(SizeType.Percent, 55.6962F));
            tlpDisplayTable.Size = new Size(962, 100);
            tlpDisplayTable.TabIndex = 0;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabDisplayAndIcon);
            tabControl.Controls.Add(tabMusic);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(984, 739);
            tabControl.TabIndex = 6;
            // 
            // tabDisplayAndIcon
            // 
            tabDisplayAndIcon.BorderStyle = BorderStyle.FixedSingle;
            tabDisplayAndIcon.Controls.Add(panel1);
            tabDisplayAndIcon.Font = new Font("Microsoft Sans Serif", 9.749999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabDisplayAndIcon.ImageIndex = 0;
            tabDisplayAndIcon.Location = new Point(4, 29);
            tabDisplayAndIcon.Name = "tabDisplayAndIcon";
            tabDisplayAndIcon.Padding = new Padding(3);
            tabDisplayAndIcon.Size = new Size(976, 706);
            tabDisplayAndIcon.TabIndex = 0;
            tabDisplayAndIcon.Text = "Display and Icon";
            tabDisplayAndIcon.ToolTipText = "Change the display name and icon for this radio station.";
            tabDisplayAndIcon.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(grpNotes);
            panel1.Controls.Add(grpSettings);
            panel1.Controls.Add(grpCustomIcon);
            panel1.Controls.Add(grpDisplay);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(968, 698);
            panel1.TabIndex = 3;
            // 
            // grpNotes
            // 
            grpNotes.BackColor = Color.White;
            grpNotes.Controls.Add(dgvMetadata);
            grpNotes.Dock = DockStyle.Fill;
            grpNotes.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpNotes.Location = new Point(0, 402);
            grpNotes.Name = "grpNotes";
            grpNotes.Size = new Size(968, 296);
            grpNotes.TabIndex = 6;
            grpNotes.TabStop = false;
            grpNotes.Text = "Custom Data";
            // 
            // dgvMetadata
            // 
            dgvMetadata.BackgroundColor = Color.WhiteSmoke;
            dgvMetadata.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMetadata.Dock = DockStyle.Fill;
            dgvMetadata.Location = new Point(3, 21);
            dgvMetadata.Name = "dgvMetadata";
            dgvMetadata.Size = new Size(962, 272);
            dgvMetadata.TabIndex = 0;
            dgvMetadata.CellValueChanged += DgvMetadata_CellValueChanged;
            dgvMetadata.UserAddedRow += DgvMetadata_UserAddedRow;
            dgvMetadata.UserDeletedRow += DgvMetadata_UserDeletedRow;
            // 
            // grpSettings
            // 
            grpSettings.BackColor = Color.White;
            grpSettings.Controls.Add(tableLayoutPanel1);
            grpSettings.Dock = DockStyle.Top;
            grpSettings.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpSettings.Location = new Point(0, 267);
            grpSettings.Name = "grpSettings";
            grpSettings.Size = new Size(968, 135);
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
            tableLayoutPanel1.Location = new Point(3, 21);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 41.09589F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 58.90411F));
            tableLayoutPanel1.Size = new Size(962, 111);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblVolume
            // 
            lblVolume.Anchor = AnchorStyles.Right;
            lblVolume.AutoSize = true;
            lblVolume.Font = new Font("Segoe UI Variable Text", 9F);
            lblVolume.Location = new Point(103, 70);
            lblVolume.Name = "lblVolume";
            lblVolume.Size = new Size(52, 16);
            lblVolume.TabIndex = 3;
            lblVolume.Text = "Volume: ";
            lblVolume.MouseEnter += LblVolume_MouseEnter;
            lblVolume.MouseLeave += Lbl_MouseLeave;
            // 
            // nudFM
            // 
            nudFM.Anchor = AnchorStyles.Left;
            nudFM.DecimalPlaces = 2;
            nudFM.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nudFM.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudFM.Location = new Point(161, 11);
            nudFM.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            nudFM.Name = "nudFM";
            nudFM.Size = new Size(91, 23);
            nudFM.TabIndex = 7;
            nudFM.ValueChanged += NudFM_ValueChanged;
            // 
            // lblFM
            // 
            lblFM.Anchor = AnchorStyles.Right;
            lblFM.AutoSize = true;
            lblFM.Font = new Font("Segoe UI Variable Text", 9F);
            lblFM.Location = new Point(125, 14);
            lblFM.Name = "lblFM";
            lblFM.Size = new Size(30, 16);
            lblFM.TabIndex = 2;
            lblFM.Text = "FM: ";
            lblFM.MouseEnter += LblFM_MouseEnter;
            lblFM.MouseLeave += Lbl_MouseLeave;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.BackColor = Color.White;
            flowLayoutPanel4.Controls.Add(volumeSlider);
            flowLayoutPanel4.Controls.Add(lblVolumeVal);
            flowLayoutPanel4.Controls.Add(panel2);
            flowLayoutPanel4.Controls.Add(lblVolumeMinMax);
            flowLayoutPanel4.Dock = DockStyle.Fill;
            flowLayoutPanel4.Location = new Point(161, 48);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(798, 60);
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
            volumeSlider.Scroll += VolumeSlider_Scroll;
            // 
            // lblVolumeVal
            // 
            lblVolumeVal.Anchor = AnchorStyles.Left;
            lblVolumeVal.AutoSize = true;
            lblVolumeVal.Font = new Font("Segoe UI Variable Text", 9F);
            lblVolumeVal.Location = new Point(269, 17);
            lblVolumeVal.Margin = new Padding(0);
            lblVolumeVal.Name = "lblVolumeVal";
            lblVolumeVal.Size = new Size(38, 16);
            lblVolumeVal.TabIndex = 6;
            lblVolumeVal.Text = "Value:";
            lblVolumeVal.TextAlign = ContentAlignment.MiddleCenter;
            lblVolumeVal.DoubleClick += LblVolumeVal_DoubleClick;
            lblVolumeVal.MouseEnter += LblVolumeVal_MouseEnter;
            lblVolumeVal.MouseLeave += Lbl_MouseLeave;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Left;
            panel2.Controls.Add(txtVolumeEdit);
            panel2.Controls.Add(lblSelectedVolume);
            panel2.Location = new Point(307, 9);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(71, 32);
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
            txtVolumeEdit.KeyDown += TxtVolumeEdit_KeyDown;
            txtVolumeEdit.KeyPress += TxtVolumeEdit_KeyPress;
            // 
            // lblSelectedVolume
            // 
            lblSelectedVolume.Anchor = AnchorStyles.Left;
            lblSelectedVolume.AutoSize = true;
            lblSelectedVolume.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSelectedVolume.Location = new Point(2, 5);
            lblSelectedVolume.Name = "lblSelectedVolume";
            lblSelectedVolume.Size = new Size(32, 20);
            lblSelectedVolume.TabIndex = 5;
            lblSelectedVolume.Text = "1.0";
            lblSelectedVolume.TextAlign = ContentAlignment.MiddleCenter;
            lblSelectedVolume.DoubleClick += LblSelectedVolume_DoubleClick;
            // 
            // lblVolumeMinMax
            // 
            lblVolumeMinMax.Anchor = AnchorStyles.Left;
            lblVolumeMinMax.AutoSize = true;
            lblVolumeMinMax.Font = new Font("Segoe UI Variable Text", 9F);
            lblVolumeMinMax.Location = new Point(381, 17);
            lblVolumeMinMax.Name = "lblVolumeMinMax";
            lblVolumeMinMax.Size = new Size(110, 16);
            lblVolumeMinMax.TabIndex = 7;
            lblVolumeMinMax.Text = "Min: 0.0 - Max: 25.0";
            lblVolumeMinMax.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // grpCustomIcon
            // 
            grpCustomIcon.BackColor = Color.White;
            grpCustomIcon.Controls.Add(tlpCustomIcon);
            grpCustomIcon.Dock = DockStyle.Top;
            grpCustomIcon.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpCustomIcon.Location = new Point(0, 124);
            grpCustomIcon.Name = "grpCustomIcon";
            grpCustomIcon.Size = new Size(968, 143);
            grpCustomIcon.TabIndex = 3;
            grpCustomIcon.TabStop = false;
            grpCustomIcon.Text = "Custom Icon";
            // 
            // tlpCustomIcon
            // 
            tlpCustomIcon.ColumnCount = 3;
            tlpCustomIcon.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 153F));
            tlpCustomIcon.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.8220024F));
            tlpCustomIcon.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 84.17799F));
            tlpCustomIcon.Controls.Add(txtInkAtlasPart, 2, 2);
            tlpCustomIcon.Controls.Add(txtInkAtlasPath, 2, 1);
            tlpCustomIcon.Controls.Add(lblUsingCustomIcon, 1, 0);
            tlpCustomIcon.Controls.Add(flowLayoutPanel1, 2, 0);
            tlpCustomIcon.Controls.Add(lblInkPart, 1, 2);
            tlpCustomIcon.Controls.Add(lblInkPath, 1, 1);
            tlpCustomIcon.Controls.Add(picStationIcon, 0, 0);
            tlpCustomIcon.Dock = DockStyle.Fill;
            tlpCustomIcon.Location = new Point(3, 21);
            tlpCustomIcon.Name = "tlpCustomIcon";
            tlpCustomIcon.RowCount = 3;
            tlpCustomIcon.RowStyles.Add(new RowStyle(SizeType.Percent, 32.9545441F));
            tlpCustomIcon.RowStyles.Add(new RowStyle(SizeType.Percent, 30.681818F));
            tlpCustomIcon.RowStyles.Add(new RowStyle(SizeType.Percent, 36.363636F));
            tlpCustomIcon.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpCustomIcon.Size = new Size(962, 119);
            tlpCustomIcon.TabIndex = 0;
            // 
            // txtInkAtlasPart
            // 
            txtInkAtlasPart.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtInkAtlasPart.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtInkAtlasPart.Location = new Point(284, 85);
            txtInkAtlasPart.Name = "txtInkAtlasPart";
            txtInkAtlasPart.ReadOnly = true;
            txtInkAtlasPart.Size = new Size(675, 23);
            txtInkAtlasPart.TabIndex = 2;
            txtInkAtlasPart.TextChanged += TxtInkAtlasPart_TextChanged;
            // 
            // txtInkAtlasPath
            // 
            txtInkAtlasPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtInkAtlasPath.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtInkAtlasPath.Location = new Point(284, 45);
            txtInkAtlasPath.Name = "txtInkAtlasPath";
            txtInkAtlasPath.ReadOnly = true;
            txtInkAtlasPath.Size = new Size(675, 23);
            txtInkAtlasPath.TabIndex = 3;
            txtInkAtlasPath.TextChanged += TxtInkAtlasPath_TextChanged;
            txtInkAtlasPath.Leave += txtInkAtlasPath_Leave;
            // 
            // lblUsingCustomIcon
            // 
            lblUsingCustomIcon.Anchor = AnchorStyles.Right;
            lblUsingCustomIcon.AutoSize = true;
            lblUsingCustomIcon.Font = new Font("Segoe UI Variable Text", 9F);
            lblUsingCustomIcon.Location = new Point(236, 11);
            lblUsingCustomIcon.Name = "lblUsingCustomIcon";
            lblUsingCustomIcon.Size = new Size(42, 16);
            lblUsingCustomIcon.TabIndex = 3;
            lblUsingCustomIcon.Text = "Using?";
            lblUsingCustomIcon.MouseEnter += LblUsingCustomIcon_MouseEnter;
            lblUsingCustomIcon.MouseLeave += Lbl_MouseLeave;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(radUseCustomYes);
            flowLayoutPanel1.Controls.Add(radUseCustomNo);
            flowLayoutPanel1.Controls.Add(btnOpenIconManager);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(284, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(675, 33);
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
            radUseCustomYes.Size = new Size(45, 29);
            radUseCustomYes.TabIndex = 5;
            radUseCustomYes.TabStop = true;
            radUseCustomYes.Text = "Yes";
            radUseCustomYes.UseVisualStyleBackColor = true;
            radUseCustomYes.CheckedChanged += RadUseCustomYes_CheckedChanged;
            // 
            // radUseCustomNo
            // 
            radUseCustomNo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            radUseCustomNo.AutoSize = true;
            radUseCustomNo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radUseCustomNo.Location = new Point(54, 3);
            radUseCustomNo.Name = "radUseCustomNo";
            radUseCustomNo.Size = new Size(41, 29);
            radUseCustomNo.TabIndex = 6;
            radUseCustomNo.TabStop = true;
            radUseCustomNo.Text = "No";
            radUseCustomNo.UseVisualStyleBackColor = true;
            radUseCustomNo.CheckedChanged += RadUseCustomNo_CheckedChanged;
            // 
            // btnOpenIconManager
            // 
            btnOpenIconManager.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnOpenIconManager.BackColor = Color.Yellow;
            btnOpenIconManager.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnOpenIconManager.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnOpenIconManager.FlatStyle = FlatStyle.Flat;
            btnOpenIconManager.Image = Properties.Resources.magic_wand_16x16;
            btnOpenIconManager.Location = new Point(101, 2);
            btnOpenIconManager.Margin = new Padding(3, 2, 3, 2);
            btnOpenIconManager.Name = "btnOpenIconManager";
            btnOpenIconManager.Size = new Size(189, 31);
            btnOpenIconManager.TabIndex = 7;
            btnOpenIconManager.Text = "Icon Manager";
            btnOpenIconManager.TextAlign = ContentAlignment.MiddleRight;
            btnOpenIconManager.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnOpenIconManager.UseVisualStyleBackColor = false;
            btnOpenIconManager.Click += btnOpenIconManager_Click;
            // 
            // lblInkPart
            // 
            lblInkPart.Anchor = AnchorStyles.Right;
            lblInkPart.AutoSize = true;
            lblInkPart.Font = new Font("Segoe UI Variable Text", 9F);
            lblInkPart.Location = new Point(196, 89);
            lblInkPart.Name = "lblInkPart";
            lblInkPart.Size = new Size(82, 16);
            lblInkPart.TabIndex = 1;
            lblInkPart.Text = "Ink Atlas Part: ";
            lblInkPart.MouseEnter += LblInkPart_MouseEnter;
            lblInkPart.MouseLeave += Lbl_MouseLeave;
            // 
            // lblInkPath
            // 
            lblInkPath.Anchor = AnchorStyles.Right;
            lblInkPath.AutoSize = true;
            lblInkPath.Font = new Font("Segoe UI Variable Text", 9F);
            lblInkPath.Location = new Point(193, 49);
            lblInkPath.Name = "lblInkPath";
            lblInkPath.Size = new Size(85, 16);
            lblInkPath.TabIndex = 0;
            lblInkPath.Text = "Ink Atlas Path: ";
            lblInkPath.MouseEnter += LblInkPath_MouseEnter;
            lblInkPath.MouseLeave += Lbl_MouseLeave;
            // 
            // picStationIcon
            // 
            picStationIcon.AllowDrop = true;
            picStationIcon.Dock = DockStyle.Fill;
            picStationIcon.Image = Properties.Resources.drag_and_drop;
            imageProperties3.Height = 0;
            imageProperties3.ImageFormat = System.Drawing.Imaging.ImageFormat.Png;
            imageProperties3.PixelFormat = System.Drawing.Imaging.PixelFormat.DontCare;
            imageProperties3.Width = 0;
            picStationIcon.ImageProperties = imageProperties3;
            picStationIcon.Location = new Point(3, 3);
            picStationIcon.Name = "picStationIcon";
            tlpCustomIcon.SetRowSpan(picStationIcon, 3);
            picStationIcon.Size = new Size(147, 113);
            picStationIcon.SizeMode = PictureBoxSizeMode.Zoom;
            picStationIcon.TabIndex = 4;
            picStationIcon.TabStop = false;
            picStationIcon.Tag = "dropTarget";
            picStationIcon.DragDrop += PicStationIcon_DragDrop;
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
            tabMusic.Size = new Size(976, 706);
            tabMusic.TabIndex = 1;
            tabMusic.Text = "Music";
            tabMusic.ToolTipText = "Change the music this radio station will play.";
            // 
            // grpSongs
            // 
            grpSongs.Dock = DockStyle.Fill;
            grpSongs.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpSongs.Location = new Point(3, 175);
            grpSongs.Name = "grpSongs";
            grpSongs.Size = new Size(968, 526);
            grpSongs.TabIndex = 1;
            grpSongs.TabStop = false;
            grpSongs.Text = "Songs";
            // 
            // grpStreamSettings
            // 
            grpStreamSettings.Controls.Add(tableLayoutPanel5);
            grpStreamSettings.Dock = DockStyle.Top;
            grpStreamSettings.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpStreamSettings.Location = new Point(3, 3);
            grpStreamSettings.Name = "grpStreamSettings";
            grpStreamSettings.Size = new Size(968, 172);
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
            tableLayoutPanel5.Location = new Point(3, 21);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 29.1262131F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 70.87379F));
            tableLayoutPanel5.Size = new Size(962, 148);
            tableLayoutPanel5.TabIndex = 0;
            // 
            // lblStreamURL
            // 
            lblStreamURL.Anchor = AnchorStyles.Right;
            lblStreamURL.AutoSize = true;
            lblStreamURL.Font = new Font("Segoe UI Variable Text", 9F);
            lblStreamURL.Location = new Point(72, 87);
            lblStreamURL.Name = "lblStreamURL";
            lblStreamURL.Size = new Size(70, 16);
            lblStreamURL.TabIndex = 3;
            lblStreamURL.Text = "Stream URL:";
            lblStreamURL.MouseEnter += LblStreamURL_MouseEnter;
            lblStreamURL.MouseLeave += Lbl_MouseLeave;
            // 
            // lblUseStream
            // 
            lblUseStream.Anchor = AnchorStyles.Right;
            lblUseStream.AutoSize = true;
            lblUseStream.Font = new Font("Segoe UI Variable Text", 9F);
            lblUseStream.Location = new Point(72, 13);
            lblUseStream.Name = "lblUseStream";
            lblUseStream.Size = new Size(70, 16);
            lblUseStream.TabIndex = 1;
            lblUseStream.Text = "Use Stream?";
            lblUseStream.MouseEnter += LblUseStream_MouseEnter;
            lblUseStream.MouseLeave += Lbl_MouseLeave;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(radUseStreamYes);
            flowLayoutPanel2.Controls.Add(radUseStreamNo);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Location = new Point(148, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(811, 37);
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
            radUseStreamYes.Size = new Size(45, 20);
            radUseStreamYes.TabIndex = 7;
            radUseStreamYes.TabStop = true;
            radUseStreamYes.Text = "Yes";
            radUseStreamYes.UseVisualStyleBackColor = true;
            radUseStreamYes.CheckedChanged += RadUseStreamYes_CheckedChanged;
            // 
            // radUseStreamNo
            // 
            radUseStreamNo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            radUseStreamNo.AutoSize = true;
            radUseStreamNo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            radUseStreamNo.Location = new Point(54, 3);
            radUseStreamNo.Name = "radUseStreamNo";
            radUseStreamNo.Size = new Size(41, 20);
            radUseStreamNo.TabIndex = 8;
            radUseStreamNo.TabStop = true;
            radUseStreamNo.Text = "No";
            radUseStreamNo.UseVisualStyleBackColor = true;
            radUseStreamNo.CheckedChanged += RadUseStreamNo_CheckedChanged;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 2;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 71.39334F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28.6066589F));
            tableLayoutPanel6.Controls.Add(flpStreamUrlTesting, 0, 1);
            tableLayoutPanel6.Controls.Add(btnGetFromRadioGarden, 1, 0);
            tableLayoutPanel6.Controls.Add(txtStreamURL, 0, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(148, 46);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new Size(811, 99);
            tableLayoutPanel6.TabIndex = 4;
            // 
            // flpStreamUrlTesting
            // 
            flpStreamUrlTesting.BackColor = Color.Transparent;
            tableLayoutPanel6.SetColumnSpan(flpStreamUrlTesting, 2);
            flpStreamUrlTesting.Controls.Add(mpStreamPlayer);
            flpStreamUrlTesting.Dock = DockStyle.Fill;
            flpStreamUrlTesting.Location = new Point(3, 52);
            flpStreamUrlTesting.Name = "flpStreamUrlTesting";
            flpStreamUrlTesting.Size = new Size(805, 44);
            flpStreamUrlTesting.TabIndex = 1;
            // 
            // mpStreamPlayer
            // 
            mpStreamPlayer.BackColor = Color.Transparent;
            mpStreamPlayer.ForeColor = Color.Transparent;
            mpStreamPlayer.Location = new Point(3, 3);
            mpStreamPlayer.Name = "mpStreamPlayer";
            mpStreamPlayer.Size = new Size(32, 34);
            mpStreamPlayer.StreamUrl = "";
            mpStreamPlayer.TabIndex = 4;
            // 
            // btnGetFromRadioGarden
            // 
            btnGetFromRadioGarden.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnGetFromRadioGarden.BackColor = Color.Yellow;
            btnGetFromRadioGarden.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnGetFromRadioGarden.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnGetFromRadioGarden.FlatStyle = FlatStyle.Flat;
            btnGetFromRadioGarden.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGetFromRadioGarden.Image = Properties.Resources.parse_16x16;
            btnGetFromRadioGarden.Location = new Point(582, 7);
            btnGetFromRadioGarden.Name = "btnGetFromRadioGarden";
            btnGetFromRadioGarden.Size = new Size(226, 34);
            btnGetFromRadioGarden.TabIndex = 4;
            btnGetFromRadioGarden.Text = "Parse From Radio.Garden";
            btnGetFromRadioGarden.TextAlign = ContentAlignment.MiddleRight;
            btnGetFromRadioGarden.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGetFromRadioGarden.UseVisualStyleBackColor = false;
            btnGetFromRadioGarden.Click += BtnGetFromRadioGarden_Click;
            // 
            // txtStreamURL
            // 
            txtStreamURL.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtStreamURL.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtStreamURL.Location = new Point(3, 13);
            txtStreamURL.Name = "txtStreamURL";
            txtStreamURL.Size = new Size(573, 23);
            txtStreamURL.TabIndex = 0;
            txtStreamURL.TextChanged += TxtStreamURL_TextChanged;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip1.Location = new Point(0, 739);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(984, 25);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 7;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Image = Properties.Resources.status__16x16;
            lblStatus.Margin = new Padding(5, 3, 0, 2);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(2);
            lblStatus.Size = new Size(59, 20);
            lblStatus.Text = "Ready";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(lblIcon, 1, 0);
            tableLayoutPanel2.Controls.Add(picGameIcon, 0, 0);
            tableLayoutPanel2.Location = new Point(3, 47);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(101, 50);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // lblIcon
            // 
            lblIcon.Anchor = AnchorStyles.Right;
            lblIcon.AutoSize = true;
            lblIcon.Font = new Font("Segoe UI Variable Text", 9F);
            lblIcon.Location = new Point(62, 17);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(36, 16);
            lblIcon.TabIndex = 3;
            lblIcon.Text = "Icon: ";
            // 
            // picGameIcon
            // 
            picGameIcon.Dock = DockStyle.Fill;
            picGameIcon.Image = Properties.Resources.add;
            picGameIcon.Location = new Point(3, 3);
            picGameIcon.Name = "picGameIcon";
            picGameIcon.Size = new Size(44, 44);
            picGameIcon.SizeMode = PictureBoxSizeMode.Zoom;
            picGameIcon.TabIndex = 4;
            picGameIcon.TabStop = false;
            // 
            // StationEditor
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tabControl);
            Controls.Add(statusStrip1);
            Controls.Add(label3);
            Name = "StationEditor";
            Size = new Size(984, 764);
            Load += StationEditor_Load;
            grpDisplay.ResumeLayout(false);
            tlpDisplayTable.ResumeLayout(false);
            tlpDisplayTable.PerformLayout();
            tabControl.ResumeLayout(false);
            tabDisplayAndIcon.ResumeLayout(false);
            panel1.ResumeLayout(false);
            grpNotes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMetadata).EndInit();
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
            tlpCustomIcon.ResumeLayout(false);
            tlpCustomIcon.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picStationIcon).EndInit();
            tabMusic.ResumeLayout(false);
            grpStreamSettings.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            flpStreamUrlTesting.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picGameIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblName;
        private TextBox txtDisplayName;
        private Label label3;
        private GroupBox grpDisplay;
        private TableLayoutPanel tlpDisplayTable;
        private TabControl tabControl;
        private TabPage tabDisplayAndIcon;
        private TabPage tabMusic;
        private GroupBox grpCustomIcon;
        private TableLayoutPanel tlpCustomIcon;
        private TextBox txtInkAtlasPart;
        private TextBox txtInkAtlasPath;
        private Label lblUsingCustomIcon;
        private FlowLayoutPanel flowLayoutPanel1;
        private RadioButton radUseCustomYes;
        private RadioButton radUseCustomNo;
        private Label lblInkPart;
        private Label lblInkPath;
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
        private GroupBox grpStreamSettings;
        private TableLayoutPanel tableLayoutPanel5;
        private Label lblUseStream;
        private FlowLayoutPanel flowLayoutPanel2;
        private RadioButton radUseStreamYes;
        private RadioButton radUseStreamNo;
        private Label lblStreamURL;
        private TableLayoutPanel tableLayoutPanel6;
        private TextBox txtStreamURL;
        private FlowLayoutPanel flpStreamUrlTesting;
        private TrackBar volumeSlider;
        private FlowLayoutPanel flowLayoutPanel4;
        private Panel panel2;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private GroupBox grpSongs;
        private Button btnGetFromRadioGarden;
        private MusicPlayer mpStreamPlayer;
        private GroupBox grpNotes;
        private DataGridView dgvMetadata;
        private CustomPictureBox picStationIcon;
        private Button btnOpenIconManager;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lblIcon;
        private PictureBox picGameIcon;
    }
}
