using AetherUtils.Core.WinForms.Controls;
using RadioExt_Helper.custom_controls;

namespace RadioExt_Helper.forms
{
    partial class IconManagerForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IconManagerForm));
            splitContainer1 = new SplitContainer();
            grpIcons = new GroupBox();
            lbIcons = new IconListBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnDeleteIcon = new SplitButton();
            deleteAllIconsContextStrip = new ContextMenuStrip(components);
            btnDeleteAllIcons = new ToolStripMenuItem();
            btnAddIcon = new SplitButton();
            newIconContextStrip = new ContextMenuStrip(components);
            fromArchiveFileToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnDisableIcon = new Button();
            btnEnableIcon = new Button();
            fdlgOpenArchive = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            grpIcons.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            deleteAllIconsContextStrip.SuspendLayout();
            newIconContextStrip.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(grpIcons);
            splitContainer1.Size = new Size(1284, 815);
            splitContainer1.SplitterDistance = 336;
            splitContainer1.TabIndex = 1;
            // 
            // grpIcons
            // 
            grpIcons.Controls.Add(lbIcons);
            grpIcons.Controls.Add(tableLayoutPanel1);
            grpIcons.Controls.Add(tableLayoutPanel2);
            grpIcons.Dock = DockStyle.Fill;
            grpIcons.Location = new Point(0, 0);
            grpIcons.Name = "grpIcons";
            grpIcons.Size = new Size(336, 815);
            grpIcons.TabIndex = 0;
            grpIcons.TabStop = false;
            grpIcons.Text = "Icons";
            // 
            // lbIcons
            // 
            lbIcons.AllowDrop = true;
            lbIcons.DisabledIconKey = "disabled";
            lbIcons.Dock = DockStyle.Fill;
            lbIcons.DrawMode = DrawMode.OwnerDrawFixed;
            lbIcons.EnabledIconKey = "enabled";
            lbIcons.FormattingEnabled = true;
            lbIcons.FromArchiveKey = "fromArchive";
            lbIcons.FromPngKey = "fromPng";
            lbIcons.ItemHeight = 15;
            lbIcons.Location = new Point(3, 53);
            lbIcons.Name = "lbIcons";
            lbIcons.Size = new Size(330, 722);
            lbIcons.TabIndex = 6;
            lbIcons.SelectedIndexChanged += lbIcons_SelectedIndexChanged;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btnDeleteIcon, 1, 0);
            tableLayoutPanel1.Controls.Add(btnAddIcon, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(3, 775);
            tableLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(330, 37);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // btnDeleteIcon
            // 
            btnDeleteIcon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnDeleteIcon.BackColor = Color.Yellow;
            btnDeleteIcon.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnDeleteIcon.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnDeleteIcon.FlatStyle = FlatStyle.Flat;
            btnDeleteIcon.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnDeleteIcon.Image = Properties.Resources.delete__16x16;
            btnDeleteIcon.Location = new Point(168, 2);
            btnDeleteIcon.Margin = new Padding(3, 2, 3, 2);
            btnDeleteIcon.Menu = deleteAllIconsContextStrip;
            btnDeleteIcon.Name = "btnDeleteIcon";
            btnDeleteIcon.Size = new Size(159, 33);
            btnDeleteIcon.SplitWidth = 25;
            btnDeleteIcon.TabIndex = 1;
            btnDeleteIcon.Text = "Delete Icon";
            btnDeleteIcon.TextAlign = ContentAlignment.MiddleRight;
            btnDeleteIcon.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDeleteIcon.UseVisualStyleBackColor = false;
            btnDeleteIcon.Click += btnDeleteIcon_Click;
            // 
            // deleteAllIconsContextStrip
            // 
            deleteAllIconsContextStrip.Items.AddRange(new ToolStripItem[] { btnDeleteAllIcons });
            deleteAllIconsContextStrip.Name = "newIconContextStrip";
            deleteAllIconsContextStrip.Size = new Size(156, 26);
            // 
            // btnDeleteAllIcons
            // 
            btnDeleteAllIcons.Image = Properties.Resources.warning__16x16;
            btnDeleteAllIcons.Name = "btnDeleteAllIcons";
            btnDeleteAllIcons.Size = new Size(155, 22);
            btnDeleteAllIcons.Text = "Delete All Icons";
            btnDeleteAllIcons.Click += btnDeleteAllIcons_Click;
            // 
            // btnAddIcon
            // 
            btnAddIcon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnAddIcon.BackColor = Color.Yellow;
            btnAddIcon.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnAddIcon.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnAddIcon.FlatStyle = FlatStyle.Flat;
            btnAddIcon.Font = new Font("Segoe UI Variable Display Semib", 9.75F, FontStyle.Bold);
            btnAddIcon.Image = Properties.Resources.add__16x16;
            btnAddIcon.Location = new Point(3, 2);
            btnAddIcon.Margin = new Padding(3, 2, 3, 2);
            btnAddIcon.Menu = newIconContextStrip;
            btnAddIcon.Name = "btnAddIcon";
            btnAddIcon.Size = new Size(159, 33);
            btnAddIcon.SplitWidth = 25;
            btnAddIcon.TabIndex = 0;
            btnAddIcon.Text = "New Icon";
            btnAddIcon.TextAlign = ContentAlignment.MiddleRight;
            btnAddIcon.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddIcon.UseVisualStyleBackColor = false;
            btnAddIcon.Click += btnAddIcon_Click;
            // 
            // newIconContextStrip
            // 
            newIconContextStrip.Items.AddRange(new ToolStripItem[] { fromArchiveFileToolStripMenuItem });
            newIconContextStrip.Name = "newIconContextStrip";
            newIconContextStrip.Size = new Size(175, 26);
            // 
            // fromArchiveFileToolStripMenuItem
            // 
            fromArchiveFileToolStripMenuItem.Image = Properties.Resources.box_16x16;
            fromArchiveFileToolStripMenuItem.Name = "fromArchiveFileToolStripMenuItem";
            fromArchiveFileToolStripMenuItem.Size = new Size(174, 22);
            fromArchiveFileToolStripMenuItem.Text = "From .archive file...";
            fromArchiveFileToolStripMenuItem.Click += fromArchiveFileToolStripMenuItem_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(btnDisableIcon, 0, 0);
            tableLayoutPanel2.Controls.Add(btnEnableIcon, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(330, 34);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // btnDisableIcon
            // 
            btnDisableIcon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnDisableIcon.BackColor = Color.Yellow;
            btnDisableIcon.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnDisableIcon.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnDisableIcon.FlatStyle = FlatStyle.Flat;
            btnDisableIcon.Image = Properties.Resources.disabled__16x16;
            btnDisableIcon.Location = new Point(168, 2);
            btnDisableIcon.Margin = new Padding(3, 2, 3, 2);
            btnDisableIcon.Name = "btnDisableIcon";
            btnDisableIcon.Size = new Size(159, 30);
            btnDisableIcon.TabIndex = 2;
            btnDisableIcon.Text = "Disable Selected";
            btnDisableIcon.TextAlign = ContentAlignment.MiddleRight;
            btnDisableIcon.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDisableIcon.UseVisualStyleBackColor = false;
            btnDisableIcon.Click += btnDisableIcon_Click;
            // 
            // btnEnableIcon
            // 
            btnEnableIcon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnEnableIcon.BackColor = Color.Yellow;
            btnEnableIcon.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnEnableIcon.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnEnableIcon.FlatStyle = FlatStyle.Flat;
            btnEnableIcon.Image = Properties.Resources.enabled__16x16;
            btnEnableIcon.Location = new Point(3, 2);
            btnEnableIcon.Margin = new Padding(3, 2, 3, 2);
            btnEnableIcon.Name = "btnEnableIcon";
            btnEnableIcon.Size = new Size(159, 30);
            btnEnableIcon.TabIndex = 1;
            btnEnableIcon.Text = "Enable Selected";
            btnEnableIcon.TextAlign = ContentAlignment.MiddleRight;
            btnEnableIcon.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEnableIcon.UseVisualStyleBackColor = false;
            btnEnableIcon.Click += btnEnableIcon_Click;
            // 
            // fdlgOpenArchive
            // 
            fdlgOpenArchive.Filter = "Mod Archive (*.archive) | *.archive";
            // 
            // IconManagerForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(1284, 815);
            Controls.Add(splitContainer1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "IconManagerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Icon Manager - {0}";
            FormClosing += IconManagerForm_FormClosing;
            Load += IconManagerForm_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            grpIcons.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            deleteAllIconsContextStrip.ResumeLayout(false);
            newIconContextStrip.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private SplitContainer splitContainer1;
        private GroupBox grpIcons;
        private TableLayoutPanel tableLayoutPanel1;
        private SplitButton btnDeleteIcon;
        private SplitButton btnAddIcon;
        private TableLayoutPanel tableLayoutPanel2;
        private Button btnDisableIcon;
        private Button btnEnableIcon;
        private IconListBox lbIcons;
        private ContextMenuStrip newIconContextStrip;
        private ToolStripMenuItem fromArchiveFileToolStripMenuItem;
        private OpenFileDialog fdlgOpenArchive;
        private ContextMenuStrip deleteAllIconsContextStrip;
        private ToolStripMenuItem btnDeleteAllIcons;
    }
}