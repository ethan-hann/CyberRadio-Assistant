using RadioExt_Helper.custom_controls;

namespace RadioExt_Helper.user_controls
{
    sealed partial class IconEditor
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
            lblEditingText = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel10 = new TableLayoutPanel();
            btnCopyIconPart = new Button();
            txtIconPart = new TextBox();
            lblIconPart = new Label();
            tableLayoutPanel6 = new TableLayoutPanel();
            txtSha256Hash = new TextBox();
            btnCopySha256Hash = new Button();
            lblSha256Hash = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            txtArchivePath = new TextBox();
            btnCopyArchivePath = new Button();
            lblArchivePath = new Label();
            lblImagePath = new Label();
            tableLayoutPanel4 = new TableLayoutPanel();
            txtImagePath = new TextBox();
            btnCopyImagePath = new Button();
            lblIconPath = new Label();
            tableLayoutPanel9 = new TableLayoutPanel();
            btnCopyIconPath = new Button();
            txtIconPath = new TextBox();
            picStationIcon = new CustomPictureBox();
            panel1 = new Panel();
            dgvStatus = new DataGridView();
            colDateTime = new DataGridViewTextBoxColumn();
            colOutput = new DataGridViewTextBoxColumn();
            tableLayoutPanel8 = new TableLayoutPanel();
            btnCancelImport = new Button();
            btnImportIcon = new Button();
            tableLayoutPanel7 = new TableLayoutPanel();
            lblAtlasName = new Label();
            txtAtlasName = new TextBox();
            centeredTabControl1 = new CenteredTabControl();
            tabPage2 = new TabPage();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            pgProgress = new ToolStripProgressBar();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel10.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picStationIcon).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStatus).BeginInit();
            tableLayoutPanel8.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            centeredTabControl1.SuspendLayout();
            tabPage2.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lblEditingText
            // 
            lblEditingText.AutoSize = true;
            lblEditingText.Dock = DockStyle.Top;
            lblEditingText.Font = new Font("Segoe UI Variable Display", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEditingText.Location = new Point(3, 3);
            lblEditingText.Name = "lblEditingText";
            lblEditingText.Size = new Size(130, 21);
            lblEditingText.TabIndex = 0;
            lblEditingText.Text = "Editing Icon: {0}";
            lblEditingText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.5294113F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76.47059F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 1, 0);
            tableLayoutPanel2.Controls.Add(picStationIcon, 0, 0);
            tableLayoutPanel2.Controls.Add(panel1, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 24);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 36.9963379F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 63.0036621F));
            tableLayoutPanel2.Size = new Size(694, 532);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.65725F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78.34275F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel10, 1, 4);
            tableLayoutPanel3.Controls.Add(lblIconPart, 0, 4);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel6, 1, 2);
            tableLayoutPanel3.Controls.Add(lblSha256Hash, 0, 2);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel5, 1, 1);
            tableLayoutPanel3.Controls.Add(lblArchivePath, 0, 1);
            tableLayoutPanel3.Controls.Add(lblImagePath, 0, 0);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 1, 0);
            tableLayoutPanel3.Controls.Add(lblIconPath, 0, 3);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel9, 1, 3);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(166, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 5;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 14F));
            tableLayoutPanel3.Size = new Size(525, 190);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel10
            // 
            tableLayoutPanel10.ColumnCount = 2;
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 87.83455F));
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.16545F));
            tableLayoutPanel10.Controls.Add(btnCopyIconPart, 0, 0);
            tableLayoutPanel10.Controls.Add(txtIconPart, 0, 0);
            tableLayoutPanel10.Dock = DockStyle.Fill;
            tableLayoutPanel10.Location = new Point(116, 162);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.RowCount = 1;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel10.Size = new Size(406, 25);
            tableLayoutPanel10.TabIndex = 10;
            // 
            // btnCopyIconPart
            // 
            btnCopyIconPart.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnCopyIconPart.BackColor = Color.Yellow;
            btnCopyIconPart.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCopyIconPart.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCopyIconPart.FlatStyle = FlatStyle.Flat;
            btnCopyIconPart.Image = Properties.Resources.copy_alt_16x16;
            btnCopyIconPart.Location = new Point(359, 3);
            btnCopyIconPart.Name = "btnCopyIconPart";
            btnCopyIconPart.Size = new Size(44, 19);
            btnCopyIconPart.TabIndex = 3;
            btnCopyIconPart.UseVisualStyleBackColor = false;
            btnCopyIconPart.Click += btnCopyIconPart_Click;
            // 
            // txtIconPart
            // 
            txtIconPart.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtIconPart.Location = new Point(3, 3);
            txtIconPart.Name = "txtIconPart";
            txtIconPart.ReadOnly = true;
            txtIconPart.Size = new Size(350, 23);
            txtIconPart.TabIndex = 2;
            // 
            // lblIconPart
            // 
            lblIconPart.Anchor = AnchorStyles.Right;
            lblIconPart.AutoSize = true;
            lblIconPart.Location = new Point(53, 167);
            lblIconPart.Name = "lblIconPart";
            lblIconPart.Size = new Size(57, 15);
            lblIconPart.TabIndex = 8;
            lblIconPart.Text = "Icon Part:";
            lblIconPart.MouseEnter += lblIconPart_MouseEnter;
            lblIconPart.MouseLeave += LblMouseLeave;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 2;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 88.295166F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.7048349F));
            tableLayoutPanel6.Controls.Add(txtSha256Hash, 0, 0);
            tableLayoutPanel6.Controls.Add(btnCopySha256Hash, 1, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(116, 86);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new Size(406, 35);
            tableLayoutPanel6.TabIndex = 6;
            // 
            // txtSha256Hash
            // 
            txtSha256Hash.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtSha256Hash.Location = new Point(3, 6);
            txtSha256Hash.Name = "txtSha256Hash";
            txtSha256Hash.ReadOnly = true;
            txtSha256Hash.Size = new Size(352, 23);
            txtSha256Hash.TabIndex = 1;
            // 
            // btnCopySha256Hash
            // 
            btnCopySha256Hash.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnCopySha256Hash.BackColor = Color.Yellow;
            btnCopySha256Hash.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCopySha256Hash.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCopySha256Hash.FlatStyle = FlatStyle.Flat;
            btnCopySha256Hash.Image = Properties.Resources.copy_alt_16x16;
            btnCopySha256Hash.Location = new Point(361, 6);
            btnCopySha256Hash.Name = "btnCopySha256Hash";
            btnCopySha256Hash.Size = new Size(42, 22);
            btnCopySha256Hash.TabIndex = 2;
            btnCopySha256Hash.UseVisualStyleBackColor = false;
            btnCopySha256Hash.Click += btnCopySha256Hash_Click;
            // 
            // lblSha256Hash
            // 
            lblSha256Hash.Anchor = AnchorStyles.Right;
            lblSha256Hash.AutoSize = true;
            lblSha256Hash.Location = new Point(29, 96);
            lblSha256Hash.Name = "lblSha256Hash";
            lblSha256Hash.Size = new Size(81, 15);
            lblSha256Hash.TabIndex = 5;
            lblSha256Hash.Text = "SHA256 Hash:";
            lblSha256Hash.MouseEnter += lblSha256Hash_MouseEnter;
            lblSha256Hash.MouseLeave += LblMouseLeave;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 88.295166F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.7048349F));
            tableLayoutPanel5.Controls.Add(txtArchivePath, 0, 0);
            tableLayoutPanel5.Controls.Add(btnCopyArchivePath, 1, 0);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(116, 49);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(406, 31);
            tableLayoutPanel5.TabIndex = 4;
            // 
            // txtArchivePath
            // 
            txtArchivePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtArchivePath.Location = new Point(3, 4);
            txtArchivePath.Name = "txtArchivePath";
            txtArchivePath.ReadOnly = true;
            txtArchivePath.Size = new Size(352, 23);
            txtArchivePath.TabIndex = 1;
            // 
            // btnCopyArchivePath
            // 
            btnCopyArchivePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnCopyArchivePath.BackColor = Color.Yellow;
            btnCopyArchivePath.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCopyArchivePath.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCopyArchivePath.FlatStyle = FlatStyle.Flat;
            btnCopyArchivePath.Image = Properties.Resources.copy_alt_16x16;
            btnCopyArchivePath.Location = new Point(361, 3);
            btnCopyArchivePath.Name = "btnCopyArchivePath";
            btnCopyArchivePath.Size = new Size(42, 25);
            btnCopyArchivePath.TabIndex = 2;
            btnCopyArchivePath.UseVisualStyleBackColor = false;
            btnCopyArchivePath.Click += btnCopyArchivePath_Click;
            // 
            // lblArchivePath
            // 
            lblArchivePath.Anchor = AnchorStyles.Right;
            lblArchivePath.AutoSize = true;
            lblArchivePath.Location = new Point(33, 57);
            lblArchivePath.Name = "lblArchivePath";
            lblArchivePath.Size = new Size(77, 15);
            lblArchivePath.TabIndex = 3;
            lblArchivePath.Text = "Archive Path:";
            lblArchivePath.MouseEnter += lblArchivePath_MouseEnter;
            lblArchivePath.MouseLeave += LblMouseLeave;
            // 
            // lblImagePath
            // 
            lblImagePath.Anchor = AnchorStyles.Right;
            lblImagePath.AutoSize = true;
            lblImagePath.Location = new Point(40, 15);
            lblImagePath.Name = "lblImagePath";
            lblImagePath.Size = new Size(70, 15);
            lblImagePath.TabIndex = 0;
            lblImagePath.Text = "Image Path:";
            lblImagePath.MouseEnter += lblImagePath_MouseEnter;
            lblImagePath.MouseLeave += LblMouseLeave;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 88.295166F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.7048349F));
            tableLayoutPanel4.Controls.Add(txtImagePath, 0, 0);
            tableLayoutPanel4.Controls.Add(btnCopyImagePath, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(116, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(406, 40);
            tableLayoutPanel4.TabIndex = 2;
            // 
            // txtImagePath
            // 
            txtImagePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtImagePath.Location = new Point(3, 8);
            txtImagePath.Name = "txtImagePath";
            txtImagePath.ReadOnly = true;
            txtImagePath.Size = new Size(352, 23);
            txtImagePath.TabIndex = 1;
            // 
            // btnCopyImagePath
            // 
            btnCopyImagePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnCopyImagePath.BackColor = Color.Yellow;
            btnCopyImagePath.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCopyImagePath.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCopyImagePath.FlatStyle = FlatStyle.Flat;
            btnCopyImagePath.Image = Properties.Resources.copy_alt_16x16;
            btnCopyImagePath.Location = new Point(361, 7);
            btnCopyImagePath.Name = "btnCopyImagePath";
            btnCopyImagePath.Size = new Size(42, 25);
            btnCopyImagePath.TabIndex = 2;
            btnCopyImagePath.UseVisualStyleBackColor = false;
            btnCopyImagePath.Click += btnCopyImagePath_Click;
            // 
            // lblIconPath
            // 
            lblIconPath.Anchor = AnchorStyles.Right;
            lblIconPath.AutoSize = true;
            lblIconPath.Location = new Point(50, 134);
            lblIconPath.Name = "lblIconPath";
            lblIconPath.Size = new Size(60, 15);
            lblIconPath.TabIndex = 7;
            lblIconPath.Text = "Icon Path:";
            lblIconPath.MouseEnter += lblIconPath_MouseEnter;
            lblIconPath.MouseLeave += LblMouseLeave;
            // 
            // tableLayoutPanel9
            // 
            tableLayoutPanel9.ColumnCount = 2;
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 87.83455F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.16545F));
            tableLayoutPanel9.Controls.Add(btnCopyIconPath, 0, 0);
            tableLayoutPanel9.Controls.Add(txtIconPath, 0, 0);
            tableLayoutPanel9.Dock = DockStyle.Fill;
            tableLayoutPanel9.Location = new Point(116, 127);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 1;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel9.Size = new Size(406, 29);
            tableLayoutPanel9.TabIndex = 9;
            // 
            // btnCopyIconPath
            // 
            btnCopyIconPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnCopyIconPath.BackColor = Color.Yellow;
            btnCopyIconPath.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCopyIconPath.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCopyIconPath.FlatStyle = FlatStyle.Flat;
            btnCopyIconPath.Image = Properties.Resources.copy_alt_16x16;
            btnCopyIconPath.Location = new Point(359, 3);
            btnCopyIconPath.Name = "btnCopyIconPath";
            btnCopyIconPath.Size = new Size(44, 22);
            btnCopyIconPath.TabIndex = 3;
            btnCopyIconPath.UseVisualStyleBackColor = false;
            btnCopyIconPath.Click += btnCopyIconPath_Click;
            // 
            // txtIconPath
            // 
            txtIconPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtIconPath.Location = new Point(3, 3);
            txtIconPath.Name = "txtIconPath";
            txtIconPath.ReadOnly = true;
            txtIconPath.Size = new Size(350, 23);
            txtIconPath.TabIndex = 2;
            // 
            // picStationIcon
            // 
            picStationIcon.AllowDrop = true;
            picStationIcon.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            picStationIcon.Location = new Point(3, 37);
            picStationIcon.Name = "picStationIcon";
            picStationIcon.Size = new Size(157, 122);
            picStationIcon.SizeMode = PictureBoxSizeMode.Zoom;
            picStationIcon.TabIndex = 0;
            picStationIcon.TabStop = false;
            picStationIcon.DragDrop += picStationIcon_DragDrop;
            // 
            // panel1
            // 
            tableLayoutPanel2.SetColumnSpan(panel1, 2);
            panel1.Controls.Add(dgvStatus);
            panel1.Controls.Add(tableLayoutPanel8);
            panel1.Controls.Add(tableLayoutPanel7);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 199);
            panel1.Name = "panel1";
            panel1.Size = new Size(688, 330);
            panel1.TabIndex = 3;
            // 
            // dgvStatus
            // 
            dgvStatus.AllowUserToAddRows = false;
            dgvStatus.AllowUserToDeleteRows = false;
            dgvStatus.BackgroundColor = Color.White;
            dgvStatus.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStatus.Columns.AddRange(new DataGridViewColumn[] { colDateTime, colOutput });
            dgvStatus.Dock = DockStyle.Fill;
            dgvStatus.Location = new Point(0, 35);
            dgvStatus.MultiSelect = false;
            dgvStatus.Name = "dgvStatus";
            dgvStatus.ReadOnly = true;
            dgvStatus.Size = new Size(688, 259);
            dgvStatus.TabIndex = 3;
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
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.ColumnCount = 2;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 77.1261F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.8739F));
            tableLayoutPanel8.Controls.Add(btnCancelImport, 0, 0);
            tableLayoutPanel8.Controls.Add(btnImportIcon, 0, 0);
            tableLayoutPanel8.Dock = DockStyle.Bottom;
            tableLayoutPanel8.Location = new Point(0, 294);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 1;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.Size = new Size(688, 36);
            tableLayoutPanel8.TabIndex = 2;
            // 
            // btnCancelImport
            // 
            btnCancelImport.BackColor = Color.Yellow;
            btnCancelImport.Dock = DockStyle.Fill;
            btnCancelImport.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCancelImport.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCancelImport.FlatStyle = FlatStyle.Flat;
            btnCancelImport.Image = Properties.Resources.cancel_16x16;
            btnCancelImport.Location = new Point(533, 3);
            btnCancelImport.Name = "btnCancelImport";
            btnCancelImport.Size = new Size(152, 30);
            btnCancelImport.TabIndex = 4;
            btnCancelImport.Text = "Cancel Import";
            btnCancelImport.TextAlign = ContentAlignment.MiddleRight;
            btnCancelImport.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelImport.UseVisualStyleBackColor = false;
            btnCancelImport.Click += btnCancelImport_Click;
            // 
            // btnImportIcon
            // 
            btnImportIcon.BackColor = Color.Yellow;
            btnImportIcon.Dock = DockStyle.Fill;
            btnImportIcon.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnImportIcon.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnImportIcon.FlatStyle = FlatStyle.Flat;
            btnImportIcon.Image = Properties.Resources.magic_wand_16x16;
            btnImportIcon.Location = new Point(3, 3);
            btnImportIcon.Name = "btnImportIcon";
            btnImportIcon.Size = new Size(524, 30);
            btnImportIcon.TabIndex = 3;
            btnImportIcon.Text = "Start Import";
            btnImportIcon.TextAlign = ContentAlignment.MiddleRight;
            btnImportIcon.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnImportIcon.UseVisualStyleBackColor = false;
            btnImportIcon.Click += btnImportIcon_Click;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.ColumnCount = 2;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.8885632F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 82.1114349F));
            tableLayoutPanel7.Controls.Add(lblAtlasName, 0, 0);
            tableLayoutPanel7.Controls.Add(txtAtlasName, 1, 0);
            tableLayoutPanel7.Dock = DockStyle.Top;
            tableLayoutPanel7.Location = new Point(0, 0);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 1;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.Size = new Size(688, 35);
            tableLayoutPanel7.TabIndex = 0;
            // 
            // lblAtlasName
            // 
            lblAtlasName.Anchor = AnchorStyles.Right;
            lblAtlasName.AutoSize = true;
            lblAtlasName.Location = new Point(49, 10);
            lblAtlasName.Name = "lblAtlasName";
            lblAtlasName.Size = new Size(71, 15);
            lblAtlasName.TabIndex = 6;
            lblAtlasName.Text = "Atlas Name:";
            lblAtlasName.MouseEnter += lblAtlasName_MouseEnter;
            lblAtlasName.MouseLeave += LblMouseLeave;
            // 
            // txtAtlasName
            // 
            txtAtlasName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtAtlasName.Location = new Point(126, 6);
            txtAtlasName.Name = "txtAtlasName";
            txtAtlasName.Size = new Size(559, 23);
            txtAtlasName.TabIndex = 7;
            txtAtlasName.TextChanged += txtAtlasName_TextChanged;
            // 
            // centeredTabControl1
            // 
            centeredTabControl1.Appearance = TabAppearance.FlatButtons;
            centeredTabControl1.Controls.Add(tabPage2);
            centeredTabControl1.Dock = DockStyle.Fill;
            centeredTabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            centeredTabControl1.Location = new Point(0, 0);
            centeredTabControl1.Name = "centeredTabControl1";
            centeredTabControl1.SelectedIndex = 0;
            centeredTabControl1.Size = new Size(708, 612);
            centeredTabControl1.TabIndex = 1;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.White;
            tabPage2.Controls.Add(tableLayoutPanel2);
            tabPage2.Controls.Add(statusStrip1);
            tabPage2.Controls.Add(lblEditingText);
            tabPage2.Location = new Point(4, 27);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(700, 581);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Editing Icon: {0}";
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, pgProgress });
            statusStrip1.Location = new Point(3, 556);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(694, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Image = Properties.Resources.status__16x16;
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(55, 17);
            lblStatus.Text = "Ready";
            lblStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pgProgress
            // 
            pgProgress.Name = "pgProgress";
            pgProgress.Size = new Size(300, 16);
            // 
            // IconEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(centeredTabControl1);
            Name = "IconEditor";
            Size = new Size(708, 612);
            Load += IconEditor_Load;
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel10.ResumeLayout(false);
            tableLayoutPanel10.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel9.ResumeLayout(false);
            tableLayoutPanel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picStationIcon).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvStatus).EndInit();
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            centeredTabControl1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label lblEditingText;
        private TableLayoutPanel tableLayoutPanel2;
        private CustomPictureBox picStationIcon;
        private TableLayoutPanel tableLayoutPanel3;
        private Label lblImagePath;
        private TableLayoutPanel tableLayoutPanel4;
        private TextBox txtImagePath;
        private Button btnCopyImagePath;
        private TableLayoutPanel tableLayoutPanel6;
        private TextBox txtSha256Hash;
        private Button btnCopySha256Hash;
        private Label lblSha256Hash;
        private TableLayoutPanel tableLayoutPanel5;
        private TextBox txtArchivePath;
        private Button btnCopyArchivePath;
        private Label lblArchivePath;
        private TableLayoutPanel tableLayoutPanel7;
        private Label lblAtlasName;
        private TextBox txtAtlasName;
        private TableLayoutPanel tableLayoutPanel8;
        private Button btnCancelImport;
        private Button btnImportIcon;
        private DataGridView dgvStatus;
        private DataGridViewTextBoxColumn colDateTime;
        private DataGridViewTextBoxColumn colOutput;
        private Label lblIconPart;
        private Label lblIconPath;
        private TableLayoutPanel tableLayoutPanel9;
        private TableLayoutPanel tableLayoutPanel10;
        private Button btnCopyIconPart;
        private TextBox txtIconPart;
        private Button btnCopyIconPath;
        private TextBox txtIconPath;
        private Panel panel1;
        private CenteredTabControl centeredTabControl1;
        private TabPage tabPage2;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private ToolStripProgressBar pgProgress;
    }
}
