namespace RadioExt_Helper.forms
{
    partial class IconGeneratorForm
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
            button1 = new Button();
            richTextBox1 = new RichTextBox();
            fldrBrowserInput = new FolderBrowserDialog();
            textBox1 = new TextBox();
            pictureBox1 = new PictureBox();
            bgWorker = new System.ComponentModel.BackgroundWorker();
            pbProgress = new ProgressBar();
            button2 = new Button();
            bgUnpacker = new System.ComponentModel.BackgroundWorker();
            ofdArchiveDialog = new OpenFileDialog();
            lblPercent = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(231, 11);
            button1.Name = "button1";
            button1.Size = new Size(75, 25);
            button1.TabIndex = 0;
            button1.Text = "Generate";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(146, 42);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(642, 382);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // fldrBrowserInput
            // 
            fldrBrowserInput.Description = "Select the input folder containing the PNG file.";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(213, 23);
            textBox1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 41);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(128, 128);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // bgWorker
            // 
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.ProgressChanged += bgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            // 
            // pbProgress
            // 
            pbProgress.Location = new Point(146, 445);
            pbProgress.Name = "pbProgress";
            pbProgress.Size = new Size(642, 23);
            pbProgress.TabIndex = 5;
            // 
            // button2
            // 
            button2.Location = new Point(312, 12);
            button2.Name = "button2";
            button2.Size = new Size(75, 25);
            button2.TabIndex = 6;
            button2.Text = "Unpack";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // bgUnpacker
            // 
            bgUnpacker.WorkerReportsProgress = true;
            bgUnpacker.WorkerSupportsCancellation = true;
            bgUnpacker.DoWork += bgUnpacker_DoWork;
            bgUnpacker.ProgressChanged += bgUnpacker_ProgressChanged;
            bgUnpacker.RunWorkerCompleted += bgUnpacker_RunWorkerCompleted;
            // 
            // ofdArchiveDialog
            // 
            ofdArchiveDialog.Filter = "Archive Files (*.archive)|*.archive";
            // 
            // lblPercent
            // 
            lblPercent.AutoSize = true;
            lblPercent.Location = new Point(12, 452);
            lblPercent.Name = "lblPercent";
            lblPercent.Size = new Size(98, 16);
            lblPercent.TabIndex = 7;
            lblPercent.Text = "Percent Done: {0}";
            // 
            // IconGeneratorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 480);
            Controls.Add(lblPercent);
            Controls.Add(button2);
            Controls.Add(pbProgress);
            Controls.Add(pictureBox1);
            Controls.Add(textBox1);
            Controls.Add(richTextBox1);
            Controls.Add(button1);
            Name = "IconGeneratorForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "IconGenerator";
            Load += IconGeneratorForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private RichTextBox richTextBox1;
        private FolderBrowserDialog fldrBrowserInput;
        private TextBox textBox1;
        private PictureBox pictureBox1;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private ProgressBar pbProgress;
        private Button button2;
        private System.ComponentModel.BackgroundWorker bgUnpacker;
        private OpenFileDialog ofdArchiveDialog;
        private Label lblPercent;
    }
}