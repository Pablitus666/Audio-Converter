namespace AudioConverter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxTitle = new System.Windows.Forms.PictureBox();
            this.panelDivider = new System.Windows.Forms.Panel();
            this.panelDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(99)))));
            this.panelDivider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDivider.Margin = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.panelDivider.Name = "panelDivider";
            this.panelDivider.TabIndex = 5;
            this.buttonsLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAddFiles = new AudioConverter.NoFocusButton();
            this.buttonConvert = new AudioConverter.NoFocusButton();
            this.buttonInfo = new AudioConverter.NoFocusButton();
            this.buttonExit = new AudioConverter.NoFocusButton();
            this.dgvJobs = new System.Windows.Forms.DataGridView();
            this.optionsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.labelSampleRate = new System.Windows.Forms.Label();
            this.comboBoxSampleRate = new System.Windows.Forms.ComboBox();
            this.labelChannels = new System.Windows.Forms.Label();
            this.comboBoxChannels = new System.Windows.Forms.ComboBox();
            this.labelBitDepth = new System.Windows.Forms.Label();
            this.comboBoxBitDepth = new System.Windows.Forms.ComboBox();
            this.labelOutputFormat = new System.Windows.Forms.Label();
            this.comboBoxOutputFormat = new System.Windows.Forms.ComboBox();
            this.outputFolderLayout = new System.Windows.Forms.TableLayoutPanel();
            this.labelOutputFolder = new System.Windows.Forms.Label();
            this.textOutputFolder = new System.Windows.Forms.TextBox();
            this.buttonSelectFolder = new AudioConverter.NoFocusButton();
            this.mainLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTitle)).BeginInit();
            this.buttonsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobs)).BeginInit();
            this.optionsLayout.SuspendLayout();
            this.outputFolderLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Controls.Add(this.pictureBoxTitle, 0, 0);
            this.mainLayout.Controls.Add(this.panelDivider, 0, 1);
            this.mainLayout.Controls.Add(this.optionsLayout, 0, 2);
            this.mainLayout.Controls.Add(this.dgvJobs, 0, 3);
            this.mainLayout.Controls.Add(this.outputFolderLayout, 0, 4);
            this.mainLayout.Controls.Add(this.buttonsLayout, 0, 5);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.Padding = new System.Windows.Forms.Padding(10);
            this.mainLayout.RowCount = 6;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.mainLayout.Size = new System.Drawing.Size(784, 561);
            this.mainLayout.TabIndex = 0;
            // 
            // pictureBoxTitle
            // 
            this.pictureBoxTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxTitle.Location = new System.Drawing.Point(13, 13);
            this.pictureBoxTitle.Margin = new System.Windows.Forms.Padding(3, 8, 3, 15);
            this.pictureBoxTitle.Name = "pictureBoxTitle";
            this.pictureBoxTitle.Size = new System.Drawing.Size(758, 94);
            this.pictureBoxTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxTitle.TabIndex = 0;
            this.pictureBoxTitle.TabStop = false;
            // 
            // buttonsLayout
            // 
            this.buttonsLayout.Controls.Add(this.buttonExit);
            this.buttonsLayout.Controls.Add(this.buttonInfo);
            this.buttonsLayout.Controls.Add(this.buttonConvert);
            this.buttonsLayout.Controls.Add(this.buttonAddFiles);
            this.buttonsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsLayout.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonsLayout.Location = new System.Drawing.Point(13, 504);
            this.buttonsLayout.Name = "buttonsLayout";
            this.buttonsLayout.Size = new System.Drawing.Size(758, 54);
            this.buttonsLayout.TabIndex = 1;
            // 
            // buttonAddFiles
            // 
            this.buttonAddFiles.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Bold);
            this.buttonAddFiles.Location = new System.Drawing.Point(600, 3);
            this.buttonAddFiles.Name = "buttonAddFiles";
            this.buttonAddFiles.Size = new System.Drawing.Size(155, 45);
            this.buttonAddFiles.TabIndex = 0;
            this.buttonAddFiles.Tag = "buttonAddFiles_Text";
            this.buttonAddFiles.Text = "Add Files";
            this.buttonAddFiles.UseVisualStyleBackColor = true;
            // 
            // buttonConvert
            // 
            this.buttonConvert.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Bold);
            this.buttonConvert.Location = new System.Drawing.Point(439, 3);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(155, 45);
            this.buttonConvert.TabIndex = 1;
            this.buttonConvert.Tag = "buttonConvert_Text";
            this.buttonConvert.Text = "Convert";
            this.buttonConvert.UseVisualStyleBackColor = true;
            // 
            // buttonInfo
            // 
            this.buttonInfo.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Bold);
            this.buttonInfo.Location = new System.Drawing.Point(278, 3);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(155, 45);
            this.buttonInfo.TabIndex = 2;
            this.buttonInfo.Tag = "buttonInfo_Text";
            this.buttonInfo.Text = "Info";
            this.buttonInfo.UseVisualStyleBackColor = true;
            // 
            // buttonExit
            // 
            this.buttonExit.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Bold);
            this.buttonExit.Location = new System.Drawing.Point(117, 3);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(155, 45);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Tag = "buttonExit_Text";
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            // 
            // dgvJobs
            // 
            this.dgvJobs.AllowUserToAddRows = false;
            this.dgvJobs.AllowUserToDeleteRows = false;
            this.dgvJobs.AllowDrop = true;
            this.dgvJobs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvJobs.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            this.dgvJobs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvJobs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(48)))), ((int)(((byte)(71)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvJobs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvJobs.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvJobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvJobs.EnableHeadersVisualStyles = false;
            this.dgvJobs.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(48)))), ((int)(((byte)(71)))));
            this.dgvJobs.Location = new System.Drawing.Point(13, 155);
            this.dgvJobs.Name = "dgvJobs";
            this.dgvJobs.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvJobs.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvJobs.RowHeadersVisible = false;
            this.dgvJobs.RowTemplate.Height = 25;
            this.dgvJobs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvJobs.Size = new System.Drawing.Size(758, 305);
            this.dgvJobs.TabIndex = 2;
            // 
            // optionsLayout
            // 
            this.optionsLayout.ColumnCount = 8;
            this.optionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.optionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.optionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.optionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.optionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.optionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.optionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.optionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.optionsLayout.Controls.Add(this.labelSampleRate, 0, 0);
            this.optionsLayout.Controls.Add(this.comboBoxSampleRate, 1, 0);
            this.optionsLayout.Controls.Add(this.labelChannels, 2, 0);
            this.optionsLayout.Controls.Add(this.comboBoxChannels, 3, 0);
            this.optionsLayout.Controls.Add(this.labelBitDepth, 4, 0);
            this.optionsLayout.Controls.Add(this.comboBoxBitDepth, 5, 0);
            this.optionsLayout.Controls.Add(this.labelOutputFormat, 6, 0);
            this.optionsLayout.Controls.Add(this.comboBoxOutputFormat, 7, 0);
            this.optionsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsLayout.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.optionsLayout.Location = new System.Drawing.Point(13, 115);
            this.optionsLayout.Name = "optionsLayout";
            this.optionsLayout.RowCount = 1;
            this.optionsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.optionsLayout.Size = new System.Drawing.Size(758, 34);
            this.optionsLayout.TabIndex = 3;
            // 
            // labelSampleRate
            // 
            this.labelSampleRate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelSampleRate.AutoSize = true;
            this.labelSampleRate.Location = new System.Drawing.Point(3, 9);
            this.labelSampleRate.Name = "labelSampleRate";
            this.labelSampleRate.Size = new System.Drawing.Size(76, 15);
            this.labelSampleRate.TabIndex = 0;
            this.labelSampleRate.Tag = "Label_SampleRate";
            this.labelSampleRate.Text = "Sample Rate:";
            // 
            // comboBoxSampleRate
            // 
            this.comboBoxSampleRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSampleRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSampleRate.FormattingEnabled = true;
            this.comboBoxSampleRate.Location = new System.Drawing.Point(85, 3);
            this.comboBoxSampleRate.Name = "comboBoxSampleRate";
            this.comboBoxSampleRate.Size = new System.Drawing.Size(126, 23);
            this.comboBoxSampleRate.TabIndex = 1;
            // 
            // labelChannels
            // 
            this.labelChannels.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelChannels.AutoSize = true;
            this.labelChannels.Location = new System.Drawing.Point(217, 9);
            this.labelChannels.Name = "labelChannels";
            this.labelChannels.Size = new System.Drawing.Size(60, 15);
            this.labelChannels.TabIndex = 2;
            this.labelChannels.Tag = "Label_Channels";
            this.labelChannels.Text = "Channels:";
            // 
            // comboBoxChannels
            // 
            this.comboBoxChannels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChannels.FormattingEnabled = true;
            this.comboBoxChannels.Location = new System.Drawing.Point(283, 3);
            this.comboBoxChannels.Name = "comboBoxChannels";
            this.comboBoxChannels.Size = new System.Drawing.Size(126, 23);
            this.comboBoxChannels.TabIndex = 3;
            // 
            // labelBitDepth
            // 
            this.labelBitDepth.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelBitDepth.AutoSize = true;
            this.labelBitDepth.Location = new System.Drawing.Point(415, 9);
            this.labelBitDepth.Name = "labelBitDepth";
            this.labelBitDepth.Size = new System.Drawing.Size(58, 15);
            this.labelBitDepth.TabIndex = 4;
            this.labelBitDepth.Tag = "Label_BitDepth";
            this.labelBitDepth.Text = "Bit Depth:";
            // 
            // comboBoxBitDepth
            // 
            this.comboBoxBitDepth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxBitDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBitDepth.FormattingEnabled = true;
            this.comboBoxBitDepth.Location = new System.Drawing.Point(479, 3);
            this.comboBoxBitDepth.Name = "comboBoxBitDepth";
            this.comboBoxBitDepth.Size = new System.Drawing.Size(126, 23);
            this.comboBoxBitDepth.TabIndex = 5;
            // 
            // labelOutputFormat
            // 
            this.labelOutputFormat.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelOutputFormat.AutoSize = true;
            this.labelOutputFormat.Location = new System.Drawing.Point(611, 9);
            this.labelOutputFormat.Name = "labelOutputFormat";
            this.labelOutputFormat.Size = new System.Drawing.Size(86, 15);
            this.labelOutputFormat.TabIndex = 6;
            this.labelOutputFormat.Tag = "Label_OutputFormat";
            this.labelOutputFormat.Text = "Output Format:";
            // 
            // comboBoxOutputFormat
            // 
            this.comboBoxOutputFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxOutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOutputFormat.FormattingEnabled = true;
            this.comboBoxOutputFormat.Location = new System.Drawing.Point(703, 3);
            this.comboBoxOutputFormat.Name = "comboBoxOutputFormat";
            this.comboBoxOutputFormat.Size = new System.Drawing.Size(52, 23);
            this.comboBoxOutputFormat.TabIndex = 7;
            // 
            // outputFolderLayout
            // 
            this.outputFolderLayout.ColumnCount = 3;
            this.outputFolderLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.outputFolderLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.outputFolderLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.outputFolderLayout.Controls.Add(this.labelOutputFolder, 0, 0);
            this.outputFolderLayout.Controls.Add(this.textOutputFolder, 1, 0);
            this.outputFolderLayout.Controls.Add(this.buttonSelectFolder, 2, 0);
            this.outputFolderLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputFolderLayout.Location = new System.Drawing.Point(13, 474);
            this.outputFolderLayout.Name = "outputFolderLayout";
            this.outputFolderLayout.RowCount = 1;
            this.outputFolderLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.outputFolderLayout.Size = new System.Drawing.Size(758, 34);
            this.outputFolderLayout.TabIndex = 4;
            // 
            // labelOutputFolder
            // 
            this.labelOutputFolder.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelOutputFolder.AutoSize = true;
            this.labelOutputFolder.Location = new System.Drawing.Point(3, 9);
            this.labelOutputFolder.Name = "labelOutputFolder";
            this.labelOutputFolder.Size = new System.Drawing.Size(84, 15);
            this.labelOutputFolder.TabIndex = 0;
            this.labelOutputFolder.Tag = "Label_OutputFolder";
            this.labelOutputFolder.Text = "Output Folder:";
            // 
            // textOutputFolder
            // 
            this.textOutputFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textOutputFolder.Location = new System.Drawing.Point(93, 3);
            this.textOutputFolder.Name = "textOutputFolder";
            this.textOutputFolder.Size = new System.Drawing.Size(542, 23);
            this.textOutputFolder.TabIndex = 1;
            // 
            // buttonSelectFolder
            // 
            this.buttonSelectFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSelectFolder.Font = new System.Drawing.Font("Comic Sans MS", 8F, System.Drawing.FontStyle.Bold);
            this.buttonSelectFolder.Location = new System.Drawing.Point(641, 3);
            this.buttonSelectFolder.Name = "buttonSelectFolder";
            this.buttonSelectFolder.Size = new System.Drawing.Size(114, 28);
            this.buttonSelectFolder.TabIndex = 2;
            this.buttonSelectFolder.Tag = "buttonSelectFolder_Text";
            this.buttonSelectFolder.Text = "Select...";
            this.buttonSelectFolder.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.mainLayout);
            this.Name = "Form1";
            this.Tag = "Form1_Text";
            this.Text = "Audio Converter";
            this.mainLayout.ResumeLayout(false);
            this.mainLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTitle)).EndInit();
            this.buttonsLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobs)).EndInit();
            this.optionsLayout.ResumeLayout(false);
            this.optionsLayout.PerformLayout();
            this.outputFolderLayout.ResumeLayout(false);
            this.outputFolderLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.PictureBox pictureBoxTitle;
        private System.Windows.Forms.FlowLayoutPanel buttonsLayout;
        private NoFocusButton buttonAddFiles;
        private NoFocusButton buttonConvert;
        private NoFocusButton buttonInfo;
        private NoFocusButton buttonExit;
        private System.Windows.Forms.DataGridView dgvJobs;
        private System.Windows.Forms.TableLayoutPanel optionsLayout;
        private System.Windows.Forms.Label labelSampleRate;
        private System.Windows.Forms.ComboBox comboBoxSampleRate;
        private System.Windows.Forms.Label labelChannels;
        private System.Windows.Forms.ComboBox comboBoxChannels;
        private System.Windows.Forms.Label labelBitDepth;
        private System.Windows.Forms.ComboBox comboBoxBitDepth;
        private System.Windows.Forms.TableLayoutPanel outputFolderLayout;
        private System.Windows.Forms.Label labelOutputFolder;
        private System.Windows.Forms.TextBox textOutputFolder;
        private NoFocusButton buttonSelectFolder;
        private System.Windows.Forms.Label labelOutputFormat;
        private System.Windows.Forms.ComboBox comboBoxOutputFormat;
        private System.Windows.Forms.Panel panelDivider;
    }
}