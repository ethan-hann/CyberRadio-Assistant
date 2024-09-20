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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IconEditor));
            models.ImageProperties imageProperties2 = new models.ImageProperties();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel10 = new TableLayoutPanel();
            btnCopyIconPart = new Button();
            txtIconPart = new TextBox();
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
            lblIconPart = new Label();
            dgvStatus = new DataGridView();
            colDateTime = new DataGridViewTextBoxColumn();
            colOutput = new DataGridViewTextBoxColumn();
            btnCancelImport = new Button();
            btnImportIcon = new Button();
            tableLayoutPanel7 = new TableLayoutPanel();
            lblIconName = new Label();
            lblAtlasName = new Label();
            txtIconName = new TextBox();
            txtAtlasName = new TextBox();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            pgProgress = new ToolStripProgressBar();
            editorTabs = new TabControl();
            tabPage1 = new TabPage();
            editorSplitContainer = new SplitContainer();
            splitContainer1 = new SplitContainer();
            grpIconPreview = new GroupBox();
            picStationIcon = new CustomPictureBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblImageStatus = new Label();
            lblImageColorMode = new Label();
            lblImageFormat = new Label();
            lblImageWidth = new Label();
            lblImageHeight = new Label();
            grpIconProperties = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            grpOutput = new GroupBox();
            groupBox1 = new GroupBox();
            tableLayoutPanel11 = new TableLayoutPanel();
            btnResetPicView = new Button();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel10.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStatus).BeginInit();
            tableLayoutPanel7.SuspendLayout();
            statusStrip1.SuspendLayout();
            editorTabs.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)editorSplitContainer).BeginInit();
            editorSplitContainer.Panel1.SuspendLayout();
            editorSplitContainer.Panel2.SuspendLayout();
            editorSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            grpIconPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picStationIcon).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            grpIconProperties.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            grpOutput.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel11.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.6916771F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78.30832F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel10, 1, 4);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel6, 1, 2);
            tableLayoutPanel3.Controls.Add(lblSha256Hash, 0, 2);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel5, 1, 1);
            tableLayoutPanel3.Controls.Add(lblArchivePath, 0, 1);
            tableLayoutPanel3.Controls.Add(lblImagePath, 0, 0);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 1, 0);
            tableLayoutPanel3.Controls.Add(lblIconPath, 0, 3);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel9, 1, 3);
            tableLayoutPanel3.Controls.Add(lblIconPart, 0, 4);
            tableLayoutPanel3.Dock = DockStyle.Top;
            tableLayoutPanel3.Location = new Point(3, 21);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 5;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
            tableLayoutPanel3.Size = new Size(656, 240);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel10
            // 
            tableLayoutPanel10.ColumnCount = 2;
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 87.83455F));
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.16545F));
            tableLayoutPanel10.Controls.Add(btnCopyIconPart, 1, 0);
            tableLayoutPanel10.Controls.Add(txtIconPart, 0, 0);
            tableLayoutPanel10.Dock = DockStyle.Fill;
            tableLayoutPanel10.Location = new Point(145, 195);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.RowCount = 1;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel10.Size = new Size(508, 42);
            tableLayoutPanel10.TabIndex = 10;
            // 
            // btnCopyIconPart
            // 
            btnCopyIconPart.BackColor = Color.Yellow;
            btnCopyIconPart.Dock = DockStyle.Fill;
            btnCopyIconPart.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCopyIconPart.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCopyIconPart.FlatStyle = FlatStyle.Flat;
            btnCopyIconPart.Image = (Image)resources.GetObject("btnCopyIconPart.Image");
            btnCopyIconPart.Location = new Point(449, 3);
            btnCopyIconPart.Name = "btnCopyIconPart";
            btnCopyIconPart.Size = new Size(56, 36);
            btnCopyIconPart.TabIndex = 3;
            btnCopyIconPart.UseVisualStyleBackColor = false;
            btnCopyIconPart.Click += btnCopyIconPart_Click;
            // 
            // txtIconPart
            // 
            txtIconPart.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtIconPart.Location = new Point(3, 8);
            txtIconPart.Name = "txtIconPart";
            txtIconPart.ReadOnly = true;
            txtIconPart.Size = new Size(440, 25);
            txtIconPart.TabIndex = 2;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 2;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 88.295166F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.7048349F));
            tableLayoutPanel6.Controls.Add(txtSha256Hash, 0, 0);
            tableLayoutPanel6.Controls.Add(btnCopySha256Hash, 1, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(145, 99);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new Size(508, 42);
            tableLayoutPanel6.TabIndex = 6;
            // 
            // txtSha256Hash
            // 
            txtSha256Hash.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtSha256Hash.Location = new Point(3, 8);
            txtSha256Hash.Name = "txtSha256Hash";
            txtSha256Hash.ReadOnly = true;
            txtSha256Hash.Size = new Size(442, 25);
            txtSha256Hash.TabIndex = 1;
            // 
            // btnCopySha256Hash
            // 
            btnCopySha256Hash.BackColor = Color.Yellow;
            btnCopySha256Hash.Dock = DockStyle.Fill;
            btnCopySha256Hash.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCopySha256Hash.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCopySha256Hash.FlatStyle = FlatStyle.Flat;
            btnCopySha256Hash.Image = (Image)resources.GetObject("btnCopySha256Hash.Image");
            btnCopySha256Hash.Location = new Point(451, 3);
            btnCopySha256Hash.Name = "btnCopySha256Hash";
            btnCopySha256Hash.Size = new Size(54, 36);
            btnCopySha256Hash.TabIndex = 2;
            btnCopySha256Hash.UseVisualStyleBackColor = false;
            btnCopySha256Hash.Click += btnCopySha256Hash_Click;
            // 
            // lblSha256Hash
            // 
            lblSha256Hash.Anchor = AnchorStyles.Right;
            lblSha256Hash.AutoSize = true;
            lblSha256Hash.Font = new Font("Segoe UI Variable Text", 9F);
            lblSha256Hash.Location = new Point(58, 112);
            lblSha256Hash.Name = "lblSha256Hash";
            lblSha256Hash.Size = new Size(81, 16);
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
            tableLayoutPanel5.Location = new Point(145, 51);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(508, 42);
            tableLayoutPanel5.TabIndex = 4;
            // 
            // txtArchivePath
            // 
            txtArchivePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtArchivePath.Location = new Point(3, 8);
            txtArchivePath.Name = "txtArchivePath";
            txtArchivePath.ReadOnly = true;
            txtArchivePath.Size = new Size(442, 25);
            txtArchivePath.TabIndex = 1;
            // 
            // btnCopyArchivePath
            // 
            btnCopyArchivePath.BackColor = Color.Yellow;
            btnCopyArchivePath.Dock = DockStyle.Fill;
            btnCopyArchivePath.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCopyArchivePath.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCopyArchivePath.FlatStyle = FlatStyle.Flat;
            btnCopyArchivePath.Image = (Image)resources.GetObject("btnCopyArchivePath.Image");
            btnCopyArchivePath.Location = new Point(451, 3);
            btnCopyArchivePath.Name = "btnCopyArchivePath";
            btnCopyArchivePath.Size = new Size(54, 36);
            btnCopyArchivePath.TabIndex = 2;
            btnCopyArchivePath.UseVisualStyleBackColor = false;
            btnCopyArchivePath.Click += btnCopyArchivePath_Click;
            // 
            // lblArchivePath
            // 
            lblArchivePath.Anchor = AnchorStyles.Right;
            lblArchivePath.AutoSize = true;
            lblArchivePath.Font = new Font("Segoe UI Variable Text", 9F);
            lblArchivePath.Location = new Point(62, 64);
            lblArchivePath.Name = "lblArchivePath";
            lblArchivePath.Size = new Size(77, 16);
            lblArchivePath.TabIndex = 3;
            lblArchivePath.Text = "Archive Path:";
            lblArchivePath.MouseEnter += lblArchivePath_MouseEnter;
            lblArchivePath.MouseLeave += LblMouseLeave;
            // 
            // lblImagePath
            // 
            lblImagePath.Anchor = AnchorStyles.Right;
            lblImagePath.AutoSize = true;
            lblImagePath.Font = new Font("Segoe UI Variable Text", 9F);
            lblImagePath.Location = new Point(70, 16);
            lblImagePath.Name = "lblImagePath";
            lblImagePath.Size = new Size(69, 16);
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
            tableLayoutPanel4.Location = new Point(145, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(508, 42);
            tableLayoutPanel4.TabIndex = 2;
            // 
            // txtImagePath
            // 
            txtImagePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtImagePath.Location = new Point(3, 8);
            txtImagePath.Name = "txtImagePath";
            txtImagePath.ReadOnly = true;
            txtImagePath.Size = new Size(442, 25);
            txtImagePath.TabIndex = 1;
            // 
            // btnCopyImagePath
            // 
            btnCopyImagePath.BackColor = Color.Yellow;
            btnCopyImagePath.Dock = DockStyle.Fill;
            btnCopyImagePath.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCopyImagePath.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCopyImagePath.FlatStyle = FlatStyle.Flat;
            btnCopyImagePath.Image = (Image)resources.GetObject("btnCopyImagePath.Image");
            btnCopyImagePath.Location = new Point(451, 3);
            btnCopyImagePath.Name = "btnCopyImagePath";
            btnCopyImagePath.Size = new Size(54, 36);
            btnCopyImagePath.TabIndex = 2;
            btnCopyImagePath.UseVisualStyleBackColor = false;
            btnCopyImagePath.Click += btnCopyImagePath_Click;
            // 
            // lblIconPath
            // 
            lblIconPath.Anchor = AnchorStyles.Right;
            lblIconPath.AutoSize = true;
            lblIconPath.Font = new Font("Segoe UI Variable Text", 9F);
            lblIconPath.Location = new Point(79, 160);
            lblIconPath.Name = "lblIconPath";
            lblIconPath.Size = new Size(60, 16);
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
            tableLayoutPanel9.Location = new Point(145, 147);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 1;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel9.Size = new Size(508, 42);
            tableLayoutPanel9.TabIndex = 9;
            // 
            // btnCopyIconPath
            // 
            btnCopyIconPath.BackColor = Color.Yellow;
            btnCopyIconPath.Dock = DockStyle.Fill;
            btnCopyIconPath.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCopyIconPath.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCopyIconPath.FlatStyle = FlatStyle.Flat;
            btnCopyIconPath.Image = (Image)resources.GetObject("btnCopyIconPath.Image");
            btnCopyIconPath.Location = new Point(449, 3);
            btnCopyIconPath.Name = "btnCopyIconPath";
            btnCopyIconPath.Size = new Size(56, 36);
            btnCopyIconPath.TabIndex = 3;
            btnCopyIconPath.UseVisualStyleBackColor = false;
            btnCopyIconPath.Click += btnCopyIconPath_Click;
            // 
            // txtIconPath
            // 
            txtIconPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtIconPath.Location = new Point(3, 8);
            txtIconPath.Name = "txtIconPath";
            txtIconPath.ReadOnly = true;
            txtIconPath.Size = new Size(440, 25);
            txtIconPath.TabIndex = 2;
            // 
            // lblIconPart
            // 
            lblIconPart.Anchor = AnchorStyles.Right;
            lblIconPart.AutoSize = true;
            lblIconPart.Font = new Font("Segoe UI Variable Text", 9F);
            lblIconPart.Location = new Point(82, 208);
            lblIconPart.Name = "lblIconPart";
            lblIconPart.Size = new Size(57, 16);
            lblIconPart.TabIndex = 8;
            lblIconPart.Text = "Icon Part:";
            lblIconPart.MouseEnter += lblIconPart_MouseEnter;
            lblIconPart.MouseLeave += LblMouseLeave;
            // 
            // dgvStatus
            // 
            dgvStatus.AllowUserToAddRows = false;
            dgvStatus.AllowUserToDeleteRows = false;
            dgvStatus.BackgroundColor = Color.White;
            dgvStatus.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStatus.Columns.AddRange(new DataGridViewColumn[] { colDateTime, colOutput });
            dgvStatus.Dock = DockStyle.Fill;
            dgvStatus.Location = new Point(3, 21);
            dgvStatus.MultiSelect = false;
            dgvStatus.Name = "dgvStatus";
            dgvStatus.ReadOnly = true;
            dgvStatus.Size = new Size(801, 307);
            dgvStatus.TabIndex = 3;
            // 
            // colDateTime
            // 
            colDateTime.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            colDateTime.HeaderText = "Timestamp";
            colDateTime.Name = "colDateTime";
            colDateTime.ReadOnly = true;
            colDateTime.Width = 101;
            // 
            // colOutput
            // 
            colOutput.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colOutput.HeaderText = "Output";
            colOutput.Name = "colOutput";
            colOutput.ReadOnly = true;
            // 
            // btnCancelImport
            // 
            btnCancelImport.BackColor = Color.Yellow;
            btnCancelImport.Dock = DockStyle.Fill;
            btnCancelImport.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCancelImport.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCancelImport.FlatStyle = FlatStyle.Flat;
            btnCancelImport.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnCancelImport.Image = (Image)resources.GetObject("btnCancelImport.Image");
            btnCancelImport.Location = new Point(3, 46);
            btnCancelImport.Name = "btnCancelImport";
            btnCancelImport.Size = new Size(193, 37);
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
            btnImportIcon.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnImportIcon.Image = (Image)resources.GetObject("btnImportIcon.Image");
            btnImportIcon.Location = new Point(3, 3);
            btnImportIcon.Name = "btnImportIcon";
            btnImportIcon.Size = new Size(193, 37);
            btnImportIcon.TabIndex = 3;
            btnImportIcon.Text = "Start Import";
            btnImportIcon.TextAlign = ContentAlignment.MiddleRight;
            btnImportIcon.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnImportIcon.UseVisualStyleBackColor = false;
            btnImportIcon.Click += btnImportIcon_Click;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.AutoSize = true;
            tableLayoutPanel7.ColumnCount = 2;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.3824024F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85.6176F));
            tableLayoutPanel7.Controls.Add(lblIconName, 0, 0);
            tableLayoutPanel7.Controls.Add(lblAtlasName, 0, 1);
            tableLayoutPanel7.Controls.Add(txtIconName, 1, 0);
            tableLayoutPanel7.Controls.Add(txtAtlasName, 1, 1);
            tableLayoutPanel7.Dock = DockStyle.Top;
            tableLayoutPanel7.Location = new Point(0, 0);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 2;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 37F));
            tableLayoutPanel7.Size = new Size(1024, 70);
            tableLayoutPanel7.TabIndex = 0;
            // 
            // lblIconName
            // 
            lblIconName.Anchor = AnchorStyles.Right;
            lblIconName.AutoSize = true;
            lblIconName.Font = new Font("Segoe UI Variable Text", 9F);
            lblIconName.Location = new Point(77, 8);
            lblIconName.Name = "lblIconName";
            lblIconName.Size = new Size(67, 16);
            lblIconName.TabIndex = 8;
            lblIconName.Text = "Icon Name:";
            // 
            // lblAtlasName
            // 
            lblAtlasName.Anchor = AnchorStyles.Right;
            lblAtlasName.AutoSize = true;
            lblAtlasName.Font = new Font("Segoe UI Variable Text", 9F);
            lblAtlasName.Location = new Point(74, 43);
            lblAtlasName.Name = "lblAtlasName";
            lblAtlasName.Size = new Size(70, 16);
            lblAtlasName.TabIndex = 6;
            lblAtlasName.Text = "Atlas Name:";
            lblAtlasName.MouseEnter += lblAtlasName_MouseEnter;
            lblAtlasName.MouseLeave += LblMouseLeave;
            // 
            // txtIconName
            // 
            txtIconName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtIconName.Location = new Point(150, 3);
            txtIconName.Name = "txtIconName";
            txtIconName.Size = new Size(871, 27);
            txtIconName.TabIndex = 9;
            txtIconName.TextChanged += txtIconName_TextChanged;
            // 
            // txtAtlasName
            // 
            txtAtlasName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtAtlasName.Location = new Point(150, 38);
            txtAtlasName.Name = "txtAtlasName";
            txtAtlasName.Size = new Size(871, 27);
            txtAtlasName.TabIndex = 7;
            txtAtlasName.TextChanged += txtAtlasName_TextChanged;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, pgProgress });
            statusStrip1.Location = new Point(0, 737);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1038, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Image = (Image)resources.GetObject("lblStatus.Image");
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(55, 17);
            lblStatus.Text = "Ready";
            lblStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pgProgress
            // 
            pgProgress.Name = "pgProgress";
            pgProgress.Size = new Size(400, 17);
            pgProgress.Visible = false;
            // 
            // editorTabs
            // 
            editorTabs.Controls.Add(tabPage1);
            editorTabs.Dock = DockStyle.Fill;
            editorTabs.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            editorTabs.Location = new Point(0, 0);
            editorTabs.Name = "editorTabs";
            editorTabs.SelectedIndex = 0;
            editorTabs.Size = new Size(1038, 737);
            editorTabs.TabIndex = 3;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.White;
            tabPage1.Controls.Add(editorSplitContainer);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1030, 704);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Editing Icon: {0}";
            // 
            // editorSplitContainer
            // 
            editorSplitContainer.Dock = DockStyle.Fill;
            editorSplitContainer.Location = new Point(3, 3);
            editorSplitContainer.Name = "editorSplitContainer";
            editorSplitContainer.Orientation = Orientation.Horizontal;
            // 
            // editorSplitContainer.Panel1
            // 
            editorSplitContainer.Panel1.Controls.Add(splitContainer1);
            editorSplitContainer.Panel1.Controls.Add(tableLayoutPanel7);
            // 
            // editorSplitContainer.Panel2
            // 
            editorSplitContainer.Panel2.Controls.Add(tableLayoutPanel2);
            editorSplitContainer.Size = new Size(1024, 698);
            editorSplitContainer.SplitterDistance = 357;
            editorSplitContainer.TabIndex = 2;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 70);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(grpIconPreview);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(grpIconProperties);
            splitContainer1.Size = new Size(1024, 287);
            splitContainer1.SplitterDistance = 358;
            splitContainer1.TabIndex = 2;
            // 
            // grpIconPreview
            // 
            grpIconPreview.Controls.Add(picStationIcon);
            grpIconPreview.Controls.Add(tableLayoutPanel1);
            grpIconPreview.Dock = DockStyle.Fill;
            grpIconPreview.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpIconPreview.Location = new Point(0, 0);
            grpIconPreview.Name = "grpIconPreview";
            grpIconPreview.Size = new Size(358, 287);
            grpIconPreview.TabIndex = 0;
            grpIconPreview.TabStop = false;
            grpIconPreview.Text = "Preview";
            // 
            // picStationIcon
            // 
            picStationIcon.AllowDrop = true;
            picStationIcon.Dock = DockStyle.Fill;
            imageProperties2.Height = 0;
            imageProperties2.ImageFormat = null;
            imageProperties2.PixelFormat = System.Drawing.Imaging.PixelFormat.DontCare;
            imageProperties2.Width = 0;
            picStationIcon.ImageProperties = imageProperties2;
            picStationIcon.Location = new Point(3, 21);
            picStationIcon.Name = "picStationIcon";
            picStationIcon.Size = new Size(352, 178);
            picStationIcon.TabIndex = 4;
            picStationIcon.TabStop = false;
            picStationIcon.DragDrop += picStationIcon_DragDrop;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(lblImageStatus, 0, 2);
            tableLayoutPanel1.Controls.Add(lblImageColorMode, 1, 1);
            tableLayoutPanel1.Controls.Add(lblImageFormat, 0, 1);
            tableLayoutPanel1.Controls.Add(lblImageWidth, 0, 0);
            tableLayoutPanel1.Controls.Add(lblImageHeight, 1, 0);
            tableLayoutPanel1.Controls.Add(btnResetPicView, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(3, 199);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(352, 85);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // lblImageStatus
            // 
            lblImageStatus.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblImageStatus.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(lblImageStatus, 2);
            lblImageStatus.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblImageStatus.ForeColor = Color.OrangeRed;
            lblImageStatus.Location = new Point(3, 36);
            lblImageStatus.Name = "lblImageStatus";
            lblImageStatus.Size = new Size(346, 17);
            lblImageStatus.TabIndex = 4;
            lblImageStatus.Text = "test";
            lblImageStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblImageColorMode
            // 
            lblImageColorMode.Anchor = AnchorStyles.Right;
            lblImageColorMode.AutoSize = true;
            lblImageColorMode.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblImageColorMode.Location = new Point(254, 18);
            lblImageColorMode.Name = "lblImageColorMode";
            lblImageColorMode.Size = new Size(95, 18);
            lblImageColorMode.TabIndex = 3;
            lblImageColorMode.Text = "Color Mode: {0}";
            lblImageColorMode.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblImageFormat
            // 
            lblImageFormat.Anchor = AnchorStyles.Left;
            lblImageFormat.AutoSize = true;
            lblImageFormat.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblImageFormat.Location = new Point(3, 18);
            lblImageFormat.Name = "lblImageFormat";
            lblImageFormat.Size = new Size(71, 18);
            lblImageFormat.TabIndex = 2;
            lblImageFormat.Text = "Format: {0}";
            lblImageFormat.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblImageWidth
            // 
            lblImageWidth.Anchor = AnchorStyles.Left;
            lblImageWidth.AutoSize = true;
            lblImageWidth.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblImageWidth.Location = new Point(3, 0);
            lblImageWidth.Name = "lblImageWidth";
            lblImageWidth.Size = new Size(41, 18);
            lblImageWidth.TabIndex = 0;
            lblImageWidth.Text = "W: {0}";
            lblImageWidth.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblImageHeight
            // 
            lblImageHeight.Anchor = AnchorStyles.Right;
            lblImageHeight.AutoSize = true;
            lblImageHeight.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblImageHeight.Location = new Point(311, 0);
            lblImageHeight.Name = "lblImageHeight";
            lblImageHeight.Size = new Size(38, 18);
            lblImageHeight.TabIndex = 1;
            lblImageHeight.Text = "H: {0}";
            lblImageHeight.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpIconProperties
            // 
            grpIconProperties.Controls.Add(tableLayoutPanel3);
            grpIconProperties.Dock = DockStyle.Fill;
            grpIconProperties.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpIconProperties.Location = new Point(0, 0);
            grpIconProperties.Name = "grpIconProperties";
            grpIconProperties.Size = new Size(662, 287);
            grpIconProperties.TabIndex = 1;
            grpIconProperties.TabStop = false;
            grpIconProperties.Text = "Properties";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 79.44163F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.5583763F));
            tableLayoutPanel2.Controls.Add(grpOutput, 0, 0);
            tableLayoutPanel2.Controls.Add(groupBox1, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(1024, 337);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // grpOutput
            // 
            grpOutput.Controls.Add(dgvStatus);
            grpOutput.Dock = DockStyle.Fill;
            grpOutput.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpOutput.Location = new Point(3, 3);
            grpOutput.Name = "grpOutput";
            grpOutput.Size = new Size(807, 331);
            grpOutput.TabIndex = 0;
            grpOutput.TabStop = false;
            grpOutput.Text = "Command Output";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel11);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            groupBox1.Location = new Point(816, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(205, 331);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Actions";
            // 
            // tableLayoutPanel11
            // 
            tableLayoutPanel11.AutoSize = true;
            tableLayoutPanel11.ColumnCount = 1;
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel11.Controls.Add(btnCancelImport, 0, 1);
            tableLayoutPanel11.Controls.Add(btnImportIcon, 0, 0);
            tableLayoutPanel11.Dock = DockStyle.Top;
            tableLayoutPanel11.Location = new Point(3, 21);
            tableLayoutPanel11.Name = "tableLayoutPanel11";
            tableLayoutPanel11.RowCount = 2;
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
            tableLayoutPanel11.Size = new Size(199, 86);
            tableLayoutPanel11.TabIndex = 0;
            // 
            // btnResetPicView
            // 
            btnResetPicView.BackColor = Color.Yellow;
            tableLayoutPanel1.SetColumnSpan(btnResetPicView, 2);
            btnResetPicView.Dock = DockStyle.Fill;
            btnResetPicView.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnResetPicView.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnResetPicView.FlatStyle = FlatStyle.Flat;
            btnResetPicView.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnResetPicView.Image = Properties.Resources.refresh__16x16;
            btnResetPicView.Location = new Point(3, 56);
            btnResetPicView.Name = "btnResetPicView";
            btnResetPicView.Size = new Size(346, 26);
            btnResetPicView.TabIndex = 5;
            btnResetPicView.Text = "Reset View";
            btnResetPicView.TextAlign = ContentAlignment.MiddleRight;
            btnResetPicView.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnResetPicView.UseVisualStyleBackColor = false;
            btnResetPicView.Click += btnResetPicView_Click;
            // 
            // IconEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(editorTabs);
            Controls.Add(statusStrip1);
            Name = "IconEditor";
            Size = new Size(1038, 759);
            Load += IconEditor_Load;
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
            ((System.ComponentModel.ISupportInitialize)dgvStatus).EndInit();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            editorTabs.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            editorSplitContainer.Panel1.ResumeLayout(false);
            editorSplitContainer.Panel1.PerformLayout();
            editorSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)editorSplitContainer).EndInit();
            editorSplitContainer.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            grpIconPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picStationIcon).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            grpIconProperties.ResumeLayout(false);
            grpIconProperties.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            grpOutput.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tableLayoutPanel11.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
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
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private ToolStripProgressBar pgProgress;
        private TabControl editorTabs;
        private TabPage tabPage1;
        private TextBox txtIconName;
        private Label lblIconName;
        private SplitContainer editorSplitContainer;
        private CustomPictureBox picStationIcon;
        private GroupBox grpIconPreview;
        private GroupBox grpIconProperties;
        private TableLayoutPanel tableLayoutPanel2;
        private GroupBox grpOutput;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel11;
        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblImageStatus;
        private Label lblImageColorMode;
        private Label lblImageFormat;
        private Label lblImageWidth;
        private Label lblImageHeight;
        private Button btnResetPicView;
    }
}
