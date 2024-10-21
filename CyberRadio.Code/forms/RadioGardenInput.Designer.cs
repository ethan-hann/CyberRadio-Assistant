namespace RadioExt_Helper.forms
{
    partial class RadioGardenInput
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
            tableLayoutPanel1 = new TableLayoutPanel();
            lblRadioGardenDesc = new Label();
            txtRadioGardenInput = new TextBox();
            btnParseUrl = new Button();
            btnCancel = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30.708662F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 69.2913361F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 113F));
            tableLayoutPanel1.Controls.Add(lblRadioGardenDesc, 0, 0);
            tableLayoutPanel1.Controls.Add(btnCancel, 2, 1);
            tableLayoutPanel1.Controls.Add(txtRadioGardenInput, 1, 0);
            tableLayoutPanel1.Controls.Add(btnParseUrl, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 61.05263F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 38.94737F));
            tableLayoutPanel1.Size = new Size(656, 95);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblRadioGardenDesc
            // 
            lblRadioGardenDesc.Anchor = AnchorStyles.Right;
            lblRadioGardenDesc.AutoSize = true;
            lblRadioGardenDesc.Location = new Point(9, 21);
            lblRadioGardenDesc.Name = "lblRadioGardenDesc";
            lblRadioGardenDesc.Size = new Size(154, 15);
            lblRadioGardenDesc.TabIndex = 0;
            lblRadioGardenDesc.Text = "Enter the radio.garden URL: ";
            // 
            // txtRadioGardenInput
            // 
            txtRadioGardenInput.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(txtRadioGardenInput, 2);
            txtRadioGardenInput.Location = new Point(169, 17);
            txtRadioGardenInput.Name = "txtRadioGardenInput";
            txtRadioGardenInput.Size = new Size(484, 23);
            txtRadioGardenInput.TabIndex = 1;
            // 
            // btnParseUrl
            // 
            btnParseUrl.BackColor = Color.Yellow;
            tableLayoutPanel1.SetColumnSpan(btnParseUrl, 2);
            btnParseUrl.Dock = DockStyle.Fill;
            btnParseUrl.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnParseUrl.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnParseUrl.FlatStyle = FlatStyle.Flat;
            btnParseUrl.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            btnParseUrl.Image = Properties.Resources.parse_16x16;
            btnParseUrl.Location = new Point(3, 61);
            btnParseUrl.Name = "btnParseUrl";
            btnParseUrl.Size = new Size(536, 31);
            btnParseUrl.TabIndex = 2;
            btnParseUrl.Text = "Parse URL";
            btnParseUrl.TextAlign = ContentAlignment.MiddleRight;
            btnParseUrl.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnParseUrl.UseVisualStyleBackColor = false;
            btnParseUrl.Click += btnParseUrl_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Yellow;
            btnCancel.Dock = DockStyle.Fill;
            btnCancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 122, 255);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 215, 242);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Variable Display", 9.75F, FontStyle.Bold);
            btnCancel.Image = Properties.Resources.cancel_16x16;
            btnCancel.Location = new Point(545, 61);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(108, 31);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.TextAlign = ContentAlignment.MiddleRight;
            btnCancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // RadioGardenInput
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(656, 95);
            ControlBox = false;
            Controls.Add(tableLayoutPanel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RadioGardenInput";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Radio Garden Input Box";
            TopMost = true;
            Load += RadioGardenInput_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label lblRadioGardenDesc;
        private TextBox txtRadioGardenInput;
        private Button btnParseUrl;
        private Button btnCancel;
    }
}