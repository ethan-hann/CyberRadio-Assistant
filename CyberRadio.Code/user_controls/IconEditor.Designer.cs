﻿using RadioExt_Helper.custom_controls;

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
            models.ImageProperties imageProperties1 = new models.ImageProperties();
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
            tblWarning = new TableLayoutPanel();
            lblImageStatus = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblImageColorMode = new Label();
            lblImageFormat = new Label();
            lblImageWidth = new Label();
            lblImageHeight = new Label();
            grpIconProperties = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            grpActions = new GroupBox();
            tableLayoutPanel11 = new TableLayoutPanel();
            btnStartExtract = new Button();
            panelLogControl = new Panel();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel10.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();
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
            tblWarning.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            grpIconProperties.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            grpActions.SuspendLayout();
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
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(635, 225);
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
            tableLayoutPanel10.Location = new Point(140, 183);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.RowCount = 1;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel10.Size = new Size(492, 39);
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
            btnCopyIconPart.Location = new Point(435, 3);
            btnCopyIconPart.Name = "btnCopyIconPart";
            btnCopyIconPart.Size = new Size(54, 33);
            btnCopyIconPart.TabIndex = 3;
            btnCopyIconPart.UseVisualStyleBackColor = false;
            btnCopyIconPart.Click += btnCopyIconPart_Click;
            btnCopyIconPart.MouseEnter += CopyBtnMouseEnter;
            btnCopyIconPart.MouseLeave += LblMouseLeave;
            // 
            // txtIconPart
            // 
            txtIconPart.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtIconPart.Location = new Point(3, 7);
            txtIconPart.Name = "txtIconPart";
            txtIconPart.ReadOnly = true;
            txtIconPart.Size = new Size(426, 25);
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
            tableLayoutPanel6.Location = new Point(140, 93);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new Size(492, 39);
            tableLayoutPanel6.TabIndex = 6;
            // 
            // txtSha256Hash
            // 
            txtSha256Hash.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtSha256Hash.Location = new Point(3, 7);
            txtSha256Hash.Name = "txtSha256Hash";
            txtSha256Hash.ReadOnly = true;
            txtSha256Hash.Size = new Size(428, 25);
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
            btnCopySha256Hash.Location = new Point(437, 3);
            btnCopySha256Hash.Name = "btnCopySha256Hash";
            btnCopySha256Hash.Size = new Size(52, 33);
            btnCopySha256Hash.TabIndex = 2;
            btnCopySha256Hash.UseVisualStyleBackColor = false;
            btnCopySha256Hash.Click += btnCopySha256Hash_Click;
            btnCopySha256Hash.MouseEnter += CopyBtnMouseEnter;
            btnCopySha256Hash.MouseLeave += LblMouseLeave;
            // 
            // lblSha256Hash
            // 
            lblSha256Hash.Anchor = AnchorStyles.Right;
            lblSha256Hash.AutoSize = true;
            lblSha256Hash.Font = new Font("Segoe UI Variable Text", 9F);
            lblSha256Hash.Location = new Point(53, 104);
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
            tableLayoutPanel5.Location = new Point(140, 48);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(492, 39);
            tableLayoutPanel5.TabIndex = 4;
            // 
            // txtArchivePath
            // 
            txtArchivePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtArchivePath.Location = new Point(3, 7);
            txtArchivePath.Name = "txtArchivePath";
            txtArchivePath.ReadOnly = true;
            txtArchivePath.Size = new Size(428, 25);
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
            btnCopyArchivePath.Location = new Point(437, 3);
            btnCopyArchivePath.Name = "btnCopyArchivePath";
            btnCopyArchivePath.Size = new Size(52, 33);
            btnCopyArchivePath.TabIndex = 2;
            btnCopyArchivePath.UseVisualStyleBackColor = false;
            btnCopyArchivePath.Click += btnCopyArchivePath_Click;
            btnCopyArchivePath.MouseEnter += CopyBtnMouseEnter;
            btnCopyArchivePath.MouseLeave += LblMouseLeave;
            // 
            // lblArchivePath
            // 
            lblArchivePath.Anchor = AnchorStyles.Right;
            lblArchivePath.AutoSize = true;
            lblArchivePath.Font = new Font("Segoe UI Variable Text", 9F);
            lblArchivePath.Location = new Point(57, 59);
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
            lblImagePath.Location = new Point(65, 14);
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
            tableLayoutPanel4.Location = new Point(140, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(492, 39);
            tableLayoutPanel4.TabIndex = 2;
            // 
            // txtImagePath
            // 
            txtImagePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtImagePath.Location = new Point(3, 7);
            txtImagePath.Name = "txtImagePath";
            txtImagePath.ReadOnly = true;
            txtImagePath.Size = new Size(428, 25);
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
            btnCopyImagePath.Location = new Point(437, 3);
            btnCopyImagePath.Name = "btnCopyImagePath";
            btnCopyImagePath.Size = new Size(52, 33);
            btnCopyImagePath.TabIndex = 2;
            btnCopyImagePath.UseVisualStyleBackColor = false;
            btnCopyImagePath.Click += btnCopyImagePath_Click;
            btnCopyImagePath.MouseEnter += CopyBtnMouseEnter;
            btnCopyImagePath.MouseLeave += LblMouseLeave;
            // 
            // lblIconPath
            // 
            lblIconPath.Anchor = AnchorStyles.Right;
            lblIconPath.AutoSize = true;
            lblIconPath.Font = new Font("Segoe UI Variable Text", 9F);
            lblIconPath.Location = new Point(74, 149);
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
            tableLayoutPanel9.Location = new Point(140, 138);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 1;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel9.Size = new Size(492, 39);
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
            btnCopyIconPath.Location = new Point(435, 3);
            btnCopyIconPath.Name = "btnCopyIconPath";
            btnCopyIconPath.Size = new Size(54, 33);
            btnCopyIconPath.TabIndex = 3;
            btnCopyIconPath.UseVisualStyleBackColor = false;
            btnCopyIconPath.Click += btnCopyIconPath_Click;
            btnCopyIconPath.MouseEnter += CopyBtnMouseEnter;
            btnCopyIconPath.MouseLeave += LblMouseLeave;
            // 
            // txtIconPath
            // 
            txtIconPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtIconPath.Location = new Point(3, 7);
            txtIconPath.Name = "txtIconPath";
            txtIconPath.ReadOnly = true;
            txtIconPath.Size = new Size(426, 25);
            txtIconPath.TabIndex = 2;
            // 
            // lblIconPart
            // 
            lblIconPart.Anchor = AnchorStyles.Right;
            lblIconPart.AutoSize = true;
            lblIconPart.Font = new Font("Segoe UI Variable Text", 9F);
            lblIconPart.Location = new Point(77, 194);
            lblIconPart.Name = "lblIconPart";
            lblIconPart.Size = new Size(57, 16);
            lblIconPart.TabIndex = 8;
            lblIconPart.Text = "Icon Part:";
            lblIconPart.MouseEnter += lblIconPart_MouseEnter;
            lblIconPart.MouseLeave += LblMouseLeave;
            // 
            // btnCancelImport
            // 
            btnCancelImport.BackColor = Color.Yellow;
            btnCancelImport.Dock = DockStyle.Fill;
            btnCancelImport.Enabled = false;
            btnCancelImport.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCancelImport.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCancelImport.FlatStyle = FlatStyle.Flat;
            btnCancelImport.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnCancelImport.Image = (Image)resources.GetObject("btnCancelImport.Image");
            btnCancelImport.Location = new Point(3, 85);
            btnCancelImport.Name = "btnCancelImport";
            btnCancelImport.Size = new Size(193, 35);
            btnCancelImport.TabIndex = 4;
            btnCancelImport.Text = "Cancel Action";
            btnCancelImport.TextAlign = ContentAlignment.MiddleRight;
            btnCancelImport.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelImport.UseVisualStyleBackColor = false;
            btnCancelImport.Click += btnCancelImport_Click;
            btnCancelImport.MouseEnter += btnCancelImport_MouseEnter;
            btnCancelImport.MouseLeave += LblMouseLeave;
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
            btnImportIcon.Size = new Size(193, 35);
            btnImportIcon.TabIndex = 3;
            btnImportIcon.Text = "Start Import";
            btnImportIcon.TextAlign = ContentAlignment.MiddleRight;
            btnImportIcon.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnImportIcon.UseVisualStyleBackColor = false;
            btnImportIcon.Click += btnImportIcon_Click;
            btnImportIcon.MouseEnter += btnImportIcon_MouseEnter;
            btnImportIcon.MouseLeave += LblMouseLeave;
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
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel7.Size = new Size(1024, 68);
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
            lblIconName.MouseEnter += lblIconName_MouseEnter;
            lblIconName.MouseLeave += LblMouseLeave;
            // 
            // lblAtlasName
            // 
            lblAtlasName.Anchor = AnchorStyles.Right;
            lblAtlasName.AutoSize = true;
            lblAtlasName.Font = new Font("Segoe UI Variable Text", 9F);
            lblAtlasName.Location = new Point(74, 42);
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
            txtAtlasName.Location = new Point(150, 37);
            txtAtlasName.Name = "txtAtlasName";
            txtAtlasName.Size = new Size(871, 27);
            txtAtlasName.TabIndex = 7;
            txtAtlasName.TextChanged += txtAtlasName_TextChanged;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.Transparent;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, pgProgress });
            statusStrip1.Location = new Point(0, 690);
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
            pgProgress.Size = new Size(400, 16);
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
            editorTabs.Size = new Size(1038, 690);
            editorTabs.TabIndex = 3;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.White;
            tabPage1.Controls.Add(editorSplitContainer);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1030, 657);
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
            editorSplitContainer.Size = new Size(1024, 651);
            editorSplitContainer.SplitterDistance = 329;
            editorSplitContainer.TabIndex = 2;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 68);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(grpIconPreview);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(grpIconProperties);
            splitContainer1.Size = new Size(1024, 261);
            splitContainer1.SplitterDistance = 379;
            splitContainer1.TabIndex = 2;
            // 
            // grpIconPreview
            // 
            grpIconPreview.Controls.Add(picStationIcon);
            grpIconPreview.Controls.Add(tblWarning);
            grpIconPreview.Controls.Add(tableLayoutPanel1);
            grpIconPreview.Dock = DockStyle.Fill;
            grpIconPreview.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpIconPreview.Location = new Point(0, 0);
            grpIconPreview.Name = "grpIconPreview";
            grpIconPreview.Size = new Size(379, 261);
            grpIconPreview.TabIndex = 0;
            grpIconPreview.TabStop = false;
            grpIconPreview.Text = "Preview";
            // 
            // picStationIcon
            // 
            picStationIcon.AllowDrop = true;
            picStationIcon.Dock = DockStyle.Fill;
            imageProperties1.Height = 0;
            imageProperties1.ImageFormat = null;
            imageProperties1.PixelFormat = System.Drawing.Imaging.PixelFormat.DontCare;
            imageProperties1.Width = 0;
            picStationIcon.ImageProperties = imageProperties1;
            picStationIcon.Location = new Point(3, 41);
            picStationIcon.Name = "picStationIcon";
            picStationIcon.Size = new Size(373, 171);
            picStationIcon.TabIndex = 4;
            picStationIcon.TabStop = false;
            picStationIcon.DragDrop += picStationIcon_DragDrop;
            picStationIcon.MouseEnter += picStationIcon_MouseEnter;
            picStationIcon.MouseLeave += LblMouseLeave;
            // 
            // tblWarning
            // 
            tblWarning.ColumnCount = 1;
            tblWarning.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblWarning.Controls.Add(lblImageStatus, 0, 0);
            tblWarning.Dock = DockStyle.Top;
            tblWarning.Location = new Point(3, 21);
            tblWarning.Name = "tblWarning";
            tblWarning.RowCount = 1;
            tblWarning.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblWarning.Size = new Size(373, 20);
            tblWarning.TabIndex = 6;
            tblWarning.Visible = false;
            // 
            // lblImageStatus
            // 
            lblImageStatus.Anchor = AnchorStyles.Left;
            lblImageStatus.AutoSize = true;
            tblWarning.SetColumnSpan(lblImageStatus, 2);
            lblImageStatus.Font = new Font("Segoe UI Variable Display", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblImageStatus.ForeColor = Color.OrangeRed;
            lblImageStatus.Location = new Point(3, 2);
            lblImageStatus.Name = "lblImageStatus";
            lblImageStatus.Size = new Size(30, 16);
            lblImageStatus.TabIndex = 4;
            lblImageStatus.Text = "test";
            lblImageStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43.4659081F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56.5340919F));
            tableLayoutPanel1.Controls.Add(lblImageColorMode, 1, 1);
            tableLayoutPanel1.Controls.Add(lblImageFormat, 0, 1);
            tableLayoutPanel1.Controls.Add(lblImageWidth, 0, 0);
            tableLayoutPanel1.Controls.Add(lblImageHeight, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(3, 212);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(373, 46);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // lblImageColorMode
            // 
            lblImageColorMode.Anchor = AnchorStyles.Right;
            lblImageColorMode.AutoSize = true;
            lblImageColorMode.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblImageColorMode.Location = new Point(275, 23);
            lblImageColorMode.Name = "lblImageColorMode";
            lblImageColorMode.Size = new Size(95, 17);
            lblImageColorMode.TabIndex = 3;
            lblImageColorMode.Text = "Color Mode: {0}";
            lblImageColorMode.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblImageFormat
            // 
            lblImageFormat.Anchor = AnchorStyles.Left;
            lblImageFormat.AutoSize = true;
            lblImageFormat.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblImageFormat.Location = new Point(3, 23);
            lblImageFormat.Name = "lblImageFormat";
            lblImageFormat.Size = new Size(70, 17);
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
            lblImageWidth.Size = new Size(42, 17);
            lblImageWidth.TabIndex = 0;
            lblImageWidth.Text = "W: {0}";
            lblImageWidth.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblImageHeight
            // 
            lblImageHeight.Anchor = AnchorStyles.Right;
            lblImageHeight.AutoSize = true;
            lblImageHeight.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblImageHeight.Location = new Point(331, 0);
            lblImageHeight.Name = "lblImageHeight";
            lblImageHeight.Size = new Size(39, 17);
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
            grpIconProperties.Size = new Size(641, 261);
            grpIconProperties.TabIndex = 1;
            grpIconProperties.TabStop = false;
            grpIconProperties.Text = "Properties";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 79.44163F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.5583763F));
            tableLayoutPanel2.Controls.Add(grpActions, 1, 0);
            tableLayoutPanel2.Controls.Add(panelLogControl, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(1024, 318);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // grpActions
            // 
            grpActions.Controls.Add(tableLayoutPanel11);
            grpActions.Dock = DockStyle.Fill;
            grpActions.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            grpActions.Location = new Point(816, 3);
            grpActions.Name = "grpActions";
            grpActions.Size = new Size(205, 312);
            grpActions.TabIndex = 1;
            grpActions.TabStop = false;
            grpActions.Text = "Actions";
            // 
            // tableLayoutPanel11
            // 
            tableLayoutPanel11.AutoSize = true;
            tableLayoutPanel11.ColumnCount = 1;
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel11.Controls.Add(btnImportIcon, 0, 0);
            tableLayoutPanel11.Controls.Add(btnStartExtract, 0, 1);
            tableLayoutPanel11.Controls.Add(btnCancelImport, 0, 2);
            tableLayoutPanel11.Dock = DockStyle.Top;
            tableLayoutPanel11.Location = new Point(3, 21);
            tableLayoutPanel11.Name = "tableLayoutPanel11";
            tableLayoutPanel11.RowCount = 3;
            tableLayoutPanel11.RowStyles.Add(new RowStyle());
            tableLayoutPanel11.RowStyles.Add(new RowStyle());
            tableLayoutPanel11.RowStyles.Add(new RowStyle());
            tableLayoutPanel11.Size = new Size(199, 123);
            tableLayoutPanel11.TabIndex = 0;
            // 
            // btnStartExtract
            // 
            btnStartExtract.BackColor = Color.Yellow;
            btnStartExtract.Dock = DockStyle.Fill;
            btnStartExtract.Enabled = false;
            btnStartExtract.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnStartExtract.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnStartExtract.FlatStyle = FlatStyle.Flat;
            btnStartExtract.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnStartExtract.Image = Properties.Resources.export__16x16;
            btnStartExtract.Location = new Point(3, 44);
            btnStartExtract.Name = "btnStartExtract";
            btnStartExtract.Size = new Size(193, 35);
            btnStartExtract.TabIndex = 5;
            btnStartExtract.Text = "Start Extraction";
            btnStartExtract.TextAlign = ContentAlignment.MiddleRight;
            btnStartExtract.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnStartExtract.UseVisualStyleBackColor = false;
            btnStartExtract.Click += btnStartExtract_Click;
            btnStartExtract.MouseEnter += btnStartExtract_MouseEnter;
            btnStartExtract.MouseLeave += LblMouseLeave;
            // 
            // panelLogControl
            // 
            panelLogControl.Dock = DockStyle.Fill;
            panelLogControl.Location = new Point(3, 3);
            panelLogControl.Name = "panelLogControl";
            panelLogControl.Size = new Size(807, 312);
            panelLogControl.TabIndex = 2;
            // 
            // IconEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(editorTabs);
            Controls.Add(statusStrip1);
            Name = "IconEditor";
            Size = new Size(1038, 712);
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
            tblWarning.ResumeLayout(false);
            tblWarning.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            grpIconProperties.ResumeLayout(false);
            grpIconProperties.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            grpActions.ResumeLayout(false);
            grpActions.PerformLayout();
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
        private TableLayoutPanel tblWarning;
        private Button btnCancelImport;
        private Button btnImportIcon;
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
        private GroupBox grpActions;
        private TableLayoutPanel tableLayoutPanel11;
        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblImageStatus;
        private Label lblImageColorMode;
        private Label lblImageFormat;
        private Label lblImageWidth;
        private Label lblImageHeight;
        private Button btnStartExtract;
        private Panel panelLogControl;
    }
}
