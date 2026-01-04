using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AudioConverter.Models;

namespace AudioConverter.Core
{
    public enum BatchConversionStatus { Completed, Cancelled }

    public class Converter
    {
        private readonly FFmpegRunner _ffmpegRunner;
        private readonly List<ConversionJob> _jobs;

        // Event to notify UI of progress, including status, message, and percentage
        public event Action<Guid, ConversionStatus, string, double>? JobProgress;

        public Converter()
        {
            _ffmpegRunner = new FFmpegRunner();
            _jobs = new List<ConversionJob>();
        }

        public void AddJob(ConversionJob job)
        {
            _jobs.Add(job);
        }

        public void RemoveJob(Guid jobId)
        {
            var jobToRemove = _jobs.FirstOrDefault(j => j.Id == jobId);
            if (jobToRemove != null)
            {
                _jobs.Remove(jobToRemove);
            }
        }

        public async Task<BatchConversionStatus> StartConversionAsync(string outputFolderPath, CancellationToken cancellationToken)
        {
            var jobsToProcess = _jobs.Where(j => j.Status == ConversionStatus.Pending || j.Status == ConversionStatus.Failed).ToList();
            bool wasCancelled = false;

            foreach (var job in jobsToProcess)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    job.Status = ConversionStatus.Cancelled;
                    job.StatusMessage = "Cancelled by user before starting.";
                    JobProgress?.Invoke(job.Id, job.Status, job.StatusMessage, job.Progress);
                    wasCancelled = true;
                    continue;
                }

                if (!File.Exists(job.InputFilePath))
                {
                    job.Status = ConversionStatus.Failed;
                    job.StatusMessage = "Error: Input file not found.";
                    JobProgress?.Invoke(job.Id, job.Status, job.StatusMessage, 0);
                    continue;
                }

                job.Status = ConversionStatus.InProgress;
                job.StatusMessage = "Initializing...";
                JobProgress?.Invoke(job.Id, job.Status, job.StatusMessage, 0);

                try
                {
                    var totalDuration = await _ffmpegRunner.GetMediaDurationAsync(job.InputFilePath);
                    if (totalDuration == TimeSpan.Zero)
                    {
                        throw new InvalidOperationException("Could not determine media duration. File may be corrupt or unsupported.");
                    }

                    string arguments = BuildArguments(job, outputFolderPath);
                    
                    var progress = new Progress<double>(percentage =>
                    {
                        job.Progress = percentage;
                        // StatusMessage should not show percentage, as Progress column handles that.
                        // It should simply reflect the ongoing state.
                        job.StatusMessage = "In progress...";
                        JobProgress?.Invoke(job.Id, job.Status, job.StatusMessage, job.Progress);
                    });

                    var (success, finalMessage) = await _ffmpegRunner.RunConversionAsync(arguments, totalDuration, progress, job.Cts!.Token);

                    if (finalMessage == "Cancelled")
                    {
                        job.Status = ConversionStatus.Cancelled;
                        job.Progress = 0; // Reset progress on cancellation
                        wasCancelled = true;
                    }
                    else
                    {
                        job.Status = success ? ConversionStatus.Completed : ConversionStatus.Failed;
                        job.Progress = success ? 100 : job.Progress;
                    }
                    job.StatusMessage = finalMessage;
                    JobProgress?.Invoke(job.Id, job.Status, job.StatusMessage, job.Progress);
                }
                catch (OperationCanceledException)
                {
                    // This catch block is for when the main cancellation token is triggered *before*
                    // the RunConversionAsync call returns "Cancelled" (e.g., if it's cancelled directly from the main CTS).
                    job.Status = ConversionStatus.Cancelled;
                    job.StatusMessage = "Cancelled";
                    job.Progress = 0; // Reset progress on cancellation
                    JobProgress?.Invoke(job.Id, job.Status, job.StatusMessage, job.Progress);
                    wasCancelled = true;
                }
                catch (Exception ex)
                {
                    job.Status = ConversionStatus.Failed;
                    job.StatusMessage = $"Error: {ex.Message}";
                    JobProgress?.Invoke(job.Id, job.Status, job.StatusMessage, job.Progress);
                }
            }

            return wasCancelled ? BatchConversionStatus.Cancelled : BatchConversionStatus.Completed;
        }

        private string GetOutputExtension(string format) => format.ToUpperInvariant() switch
        {
            "MP3" => "mp3",
            "FLAC" => "flac",
            "WAV" => "wav",
            _ => "wav" // Default fallback
        };

        private string GetAudioSpecificArgs(ConversionJob job)
        {
            switch (job.Options.OutputFormat.ToUpperInvariant())
            {
                case "WAV":
                    string sampleFormatWAV = job.Options.BitDepth switch { 16 => "s16", 24 => "s24", _ => "s16" };
                    return $"-ac {job.Options.Channels} -ar {job.Options.SampleRate} -sample_fmt {sampleFormatWAV}";
                
                case "MP3":
                    return $"-ac {job.Options.Channels} -ar {job.Options.SampleRate} -codec:a libmp3lame -q:a 2";

                case "FLAC":
                    return $"-ac {job.Options.Channels} -ar {job.Options.SampleRate} -codec:a flac";

                default:
                    string sampleFormatDefault = job.Options.BitDepth switch { 16 => "s16", 24 => "s24", _ => "s16" };
                    return $"-ac {job.Options.Channels} -ar {job.Options.SampleRate} -sample_fmt {sampleFormatDefault}";
            }
        }

        private string BuildArguments(ConversionJob job, string outputFolderPath)
        {
            string outputExtension = GetOutputExtension(job.Options.OutputFormat);
            string outputFileName = Path.ChangeExtension(Path.GetFileName(job.InputFilePath), $".{outputExtension}");

            string finalOutputFolder = string.IsNullOrEmpty(outputFolderPath)
                ? Path.GetDirectoryName(job.InputFilePath)!
                : outputFolderPath;

            job.OutputFilePath = Path.Combine(finalOutputFolder, outputFileName);
            
            string audioSpecificArgs = GetAudioSpecificArgs(job);
            
            string overwrite = "-y";
            string noVideo = "-vn";
            
            return $"{overwrite} -i \"{job.InputFilePath}\" {noVideo} {audioSpecificArgs} \"{job.OutputFilePath}\"" ;
        }

        public void ClearJobs()
        {
            _jobs.Clear();
        }
    }
}