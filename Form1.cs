using System.Diagnostics;
using System.Globalization;
using System.Threading;
using AudioConverter.Core;
using AudioConverter.Helpers;
using AudioConverter.Models;

namespace AudioConverter
{
    public partial class Form1 : Form
    {
        private readonly Core.Converter _converter;
        private CancellationTokenSource _mainCts = new();
        private bool _isConverting = false;

        public Form1()
        {
            InitializeComponent();
            _converter = new Core.Converter();
            _converter.JobProgress += OnJobProgress;

            dgvJobs.DragEnter += dgvJobs_DragEnter;
            dgvJobs.DragDrop += dgvJobs_DragDrop;
            dgvJobs.CellClick += dgvJobs_CellClick;


            SetupAppearance();
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
        }

        private void OnJobProgress(Guid jobId, ConversionStatus status, string message, double progress)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(() => OnJobProgress(jobId, status, message, progress));
                return;
            }

            foreach (DataGridViewRow row in dgvJobs.Rows)
            {
                if (row.Tag is ConversionJob job && job.Id == jobId)
                {
                    row.Cells["Progress"].Value = progress;
                    row.Cells["Status"].Value = message;

                    bool inProgress = status == ConversionStatus.InProgress;
                    if (inProgress)
                    {
                        row.Cells["Cancel"].Value = "Cancel";
                        row.Cells["Cancel"].Style.BackColor = Color.DarkRed;
                        row.Cells["Cancel"].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        row.Cells["Cancel"].Value = string.Empty;
                        row.Cells["Cancel"].Style.BackColor = row.DefaultCellStyle.BackColor;
                        row.Cells["Cancel"].Style.ForeColor = row.DefaultCellStyle.ForeColor;
                    }

                    if (status == ConversionStatus.Completed || status == ConversionStatus.Failed || status == ConversionStatus.Cancelled)
                    {
                        job.Cts?.Dispose();
                        job.Cts = null;
                    }

                    break;
                }
            }
        }

        private void SetupAppearance()
        {
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#023047");
            this.ForeColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            
            var labels = new[] { labelSampleRate, labelChannels, labelBitDepth, labelOutputFolder, labelOutputFormat };
            foreach (var label in labels)
            {
                label.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                label.ForeColor = Color.White;
            }

            var comboBoxes = new[] { comboBoxSampleRate, comboBoxChannels, comboBoxBitDepth, comboBoxOutputFormat };
            foreach (var cb in comboBoxes)
            {
                cb.BackColor = System.Drawing.ColorTranslator.FromHtml("#0B2027");
                cb.ForeColor = Color.White;
                cb.FlatStyle = FlatStyle.Flat;
            }
            
            textOutputFolder.BackColor = System.Drawing.ColorTranslator.FromHtml("#0B2027");
            textOutputFolder.ForeColor = Color.White;
            textOutputFolder.BorderStyle = BorderStyle.FixedSingle;
        }

        private void SetupJobsGrid()
        {
            dgvJobs.AutoGenerateColumns = false;
            dgvJobs.Columns.Clear();
            dgvJobs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Let columns fill space intelligently

            var fileNameCol = new DataGridViewTextBoxColumn
            {
                Name = "FileName",
                HeaderText = LocalizationManager.GetString("ListView_File") ?? "File",
                DataPropertyName = "FileName",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, // Fill remaining space
                Resizable = DataGridViewTriState.False
            };
            fileNameCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            var progressCol = new DataGridViewProgressColumn
            {
                Name = "Progress",
                HeaderText = LocalizationManager.GetString("ListView_Progress") ?? "Progress",
                DataPropertyName = "Progress",
                ReadOnly = true,
                Width = 100, // User requested 100px
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None, // Ensure fixed width
                Resizable = DataGridViewTriState.False
            };
            progressCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            var statusCol = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = LocalizationManager.GetString("ListView_Status") ?? "Status",
                DataPropertyName = "StatusMessage",
                ReadOnly = true,
                Width = 80, // User requested 80px
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None, // Ensure fixed width
                Resizable = DataGridViewTriState.False
            };
            statusCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            var cancelCol = new DataGridViewButtonColumn
            {
                Name = "Cancel",
                HeaderText = LocalizationManager.GetString("ListView_Cancel") ?? "Cancel",
                UseColumnTextForButtonValue = true,
                Width = 80, // User requested 80px
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None, // Ensure fixed width
                FlatStyle = FlatStyle.Flat,
                Resizable = DataGridViewTriState.False
            };
            cancelCol.DefaultCellStyle.ForeColor = Color.Black;
            cancelCol.DefaultCellStyle.BackColor = dgvJobs.DefaultCellStyle.BackColor;
            cancelCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dgvJobs.Columns.AddRange(fileNameCol, progressCol, statusCol, cancelCol);
        }


        private void Form1_Load(object? sender, EventArgs e)
        {
            LocalizationManager.CurrentLanguage = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            LocalizationManager.ApplyLocalization(this);

            SetupJobsGrid();

            LoadImages();
            PopulateOptions();

            buttonAddFiles.Click += ButtonAddFiles_Click;
            buttonConvert.Click += ButtonConvert_Click;
            buttonInfo.Click += ButtonInfo_Click;
            buttonExit.Click += ButtonExit_Click;
            buttonSelectFolder.Click += ButtonSelectFolder_Click;
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_mainCts != null && !_mainCts.IsCancellationRequested)
            {
                _mainCts.Cancel();
            }
            
            _converter.JobProgress -= OnJobProgress;

            foreach (DataGridViewRow row in dgvJobs.Rows)
            {
                if (row.Tag is ConversionJob job)
                {
                    job.Cts?.Dispose();
                }
            }
            _mainCts?.Dispose();
        }

        private void LoadImages()
        {
            try
            {
                Image? titleImage = ImageUtils.CargarImagenDesdeRecurso("titulo.png");
                if (titleImage != null) this.pictureBoxTitle.Image = titleImage;

                Image? buttonImage = ImageUtils.CargarImagenDesdeRecurso("boton.png");
                if (buttonImage != null)
                {
                    var buttons = new[] { buttonAddFiles, buttonConvert, buttonInfo, buttonExit, buttonSelectFolder };
                    foreach (var btn in buttons)
                    {
                        btn.BackgroundImage = buttonImage;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
                
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                using (Stream? iconStream = assembly.GetManifestResourceStream("AudioConverter.images.icon.ico"))
                {
                    if (iconStream != null) this.Icon = new Icon(iconStream);
                }
            }
            catch (Exception ex)
            {
                CustomMessageBoxForm.Show(this, $"Error loading images: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void PopulateOptions()
        {
            comboBoxSampleRate.Items.AddRange(new object[] { "44100 Hz", "48000 Hz", "88200 Hz", "96000 Hz" });
            comboBoxSampleRate.SelectedIndex = 1;

            comboBoxChannels.Items.AddRange(new object[] { "Stereo", "Mono" });
            comboBoxChannels.SelectedIndex = 0;

            comboBoxBitDepth.Items.AddRange(new object[] { "16-bit", "24-bit" });
            comboBoxBitDepth.SelectedIndex = 0;

            comboBoxOutputFormat.Items.AddRange(new object[] { "WAV", "MP3", "FLAC" });
            comboBoxOutputFormat.SelectedIndex = 0;
        }
        
        private void AddFileToQueue(string filePath)
        {
            var options = new ConversionOptions
            {
                SampleRate = int.Parse(comboBoxSampleRate.Text.Split(' ')[0]),
                Channels = comboBoxChannels.Text == "Stereo" ? 2 : 1,
                BitDepth = int.Parse(comboBoxBitDepth.Text.Split('-')[0]),
                OutputFormat = comboBoxOutputFormat.Text
            };

            var job = new ConversionJob(filePath, options);
            _converter.AddJob(job);

            var rowIdx = dgvJobs.Rows.Add(Path.GetFileName(filePath), 0.0, "Pending", "");
            dgvJobs.Rows[rowIdx].Tag = job;
        }

        private void ButtonAddFiles_Click(object? sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Audio/Video Files|*.mp3;*.mp4;*.m4a;*.aac;*.flac;*.ogg;*.wma;*.wav;*.mov;*.mkv|All Files|*.*";
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in ofd.FileNames)
                    {
                        AddFileToQueue(file);
                    }
                }
            }
        }

        private async void ButtonConvert_Click(object? sender, EventArgs e)
        {
            if (dgvJobs.Rows.Count == 0)
            {
                CustomMessageBoxForm.Show(this, LocalizationManager.GetString("Conversion_NoFiles") ?? "No files to convert. Please add files first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _isConverting = true;
            ToggleControls(false);

            try
            {
                // Dispose the old CTS if it exists and create a new one for the new batch.
                _mainCts?.Dispose();
                _mainCts = new CancellationTokenSource();

                // Create linked CTS for each job so we can cancel them individually
                foreach (DataGridViewRow row in dgvJobs.Rows)
                {
                    if (row.Tag is ConversionJob job && (job.Status == ConversionStatus.Pending || job.Status == ConversionStatus.Failed))
                    {
                        job.Cts = CancellationTokenSource.CreateLinkedTokenSource(_mainCts.Token);
                    }
                }

                var batchResult = await _converter.StartConversionAsync(textOutputFolder.Text, _mainCts.Token);

                if (batchResult == Core.BatchConversionStatus.Cancelled)
                {
                    CustomMessageBoxForm.Show(this,
                        LocalizationManager.GetString("Conversion_Cancelled") ?? "Conversion process cancelled.",
                        LocalizationManager.GetString("Conversion_Title_Cancelled") ?? "Conversion Cancelled",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    CustomMessageBoxForm.Show(this,
                        LocalizationManager.GetString("Conversion_Finished") ?? "Conversion process finished.",
                        LocalizationManager.GetString("Conversion_Title") ?? "Conversion Completed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            finally
            {
                _isConverting = false;
                ToggleControls(true);
            }
        }

        private void ButtonInfo_Click(object? sender, EventArgs e)
        {
            using (var aboutBox = new AboutBoxForm())
            {
                aboutBox.ShowDialog(this);
            }
        }



        private void ButtonExit_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonSelectFolder_Click(object? sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    textOutputFolder.Text = fbd.SelectedPath;
                }
            }
        }

        private void dgvJobs_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void dgvJobs_DragDrop(object? sender, DragEventArgs e)
        {
            if (_isConverting) return;

            if (e.Data != null)
            {
                string[]? files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files != null)
                {
                    foreach (string file in files)
                    {
                        AddFileToQueue(file);
                    }
                }
            }
        }
        
        private void dgvJobs_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvJobs.Columns["Cancel"].Index)
                return;

            if (dgvJobs.Rows[e.RowIndex].Tag is ConversionJob job)
            {
                if (job.Status == ConversionStatus.InProgress)
                {
                    // Show confirmation dialog before canceling and removing.
                    var confirmResult = CustomMessageBoxForm.Show(
                        this,
                        LocalizationManager.GetString("Confirm_CancelJob_Message") ?? "Are you sure you want to cancel this job and remove it from the list?",
                        LocalizationManager.GetString("Confirm_CancelJob_Title") ?? "Confirm Cancellation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (confirmResult == DialogResult.Yes)
                    {
                        Logger.Log($"[UI] User confirmed cancellation for job {job.Id} (File: {Path.GetFileName(job.InputFilePath)}).");
                        job.Cts?.Cancel(); // Trigger cancellation in the worker.

                        // Important: Remove the job from the internal list in Converter immediately
                        _converter.RemoveJob(job.Id); 
                        dgvJobs.Rows.RemoveAt(e.RowIndex); // Remove from the UI grid
                    }
                    else
                    {
                        Logger.Log($"[UI] User cancelled cancellation for job {job.Id}.");
                    }
                }
                else
                {
                    Logger.Log($"[UI] Job {job.Id} (File: {Path.GetFileName(job.InputFilePath)}) is not in progress. Current status: {job.Status}. Cannot cancel.");
                }
            }
        }

        private void ToggleControls(bool enabled)
        {
            buttonAddFiles.Enabled = enabled;
            buttonConvert.Enabled = enabled;
            buttonSelectFolder.Enabled = enabled;
            comboBoxSampleRate.Enabled = enabled;
            comboBoxChannels.Enabled = enabled;
            comboBoxBitDepth.Enabled = enabled;
            comboBoxOutputFormat.Enabled = enabled;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Delete)
            {
                if (_isConverting) return true; // Block delete operations during conversion

                if (dgvJobs.SelectedRows.Count > 0)
                {
                    var rowsToRemove = dgvJobs.SelectedRows.Cast<DataGridViewRow>().ToList();
                    foreach (DataGridViewRow row in rowsToRemove)
                    {
                        if (row.Tag is ConversionJob job)
                        {
                            if (job.Status == ConversionStatus.InProgress)
                            {
                                job.Cts?.Cancel(); // Cancel if it's running
                            }
                            _converter.RemoveJob(job.Id);
                            dgvJobs.Rows.Remove(row);
                        }
                    }
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}