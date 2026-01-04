using System;
using System.Threading;
using System.Windows.Forms;

namespace AudioConverter.Models
{
    public enum ConversionStatus
    {
        Pending,
        InProgress,
        Completed,
        Failed,
        Cancelled
    }

    public class ConversionJob
    {
        public Guid Id { get; }
        public string InputFilePath { get; }
        public string OutputFilePath { get; set; }
        public ConversionOptions Options { get; }
        public ConversionStatus Status { get; set; }
        public string StatusMessage { get; set; }
        public double Progress { get; set; }
        public CancellationTokenSource? Cts { get; set; }


        public ConversionJob(string inputFilePath, ConversionOptions options)
        {
            Id = Guid.NewGuid();
            InputFilePath = inputFilePath;
            Options = options;
            Status = ConversionStatus.Pending;
            StatusMessage = "Pending";
            OutputFilePath = string.Empty; // Will be set later
            Progress = 0;
        }
    }

    public class ConversionOptions
    {
        public int SampleRate { get; set; }
        public int Channels { get; set; }
        public int BitDepth { get; set; }
        public string OutputFormat { get; set; } = "WAV"; // Default to WAV
    }
}
