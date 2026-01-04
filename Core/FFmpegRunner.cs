using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AudioConverter.Helpers;

namespace AudioConverter.Core
{
    public class FFmpegRunner
    {
        private readonly string _ffmpegPath;

        public FFmpegRunner()
        {
            _ffmpegPath = Path.Combine(Path.GetTempPath(), "AudioConverter_ffmpeg.exe");
            ExtractFFmpeg();
        }

        private void ExtractFFmpeg()
        {
            if (File.Exists(_ffmpegPath))
            {
                return;
            }

            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = "AudioConverter.FFmpeg.ffmpeg.exe";

            using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException("Embedded ffmpeg.exe resource not found! This is a critical application error.", resourceName);
                }

                using (FileStream fs = new FileStream(_ffmpegPath, FileMode.Create))
                {
                    stream.CopyTo(fs);
                }
            }
        }

        public async Task<TimeSpan> GetMediaDurationAsync(string inputPath)
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = _ffmpegPath,
                    Arguments = $"-i \"{inputPath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            string output = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            var match = Regex.Match(output, @"Duration: (\d{2}):(\d{2}):(\d{2})\.(\d{2})");
            if (match.Success)
            {
                return new TimeSpan(0, 
                    int.Parse(match.Groups[1].Value), 
                    int.Parse(match.Groups[2].Value), 
                    int.Parse(match.Groups[3].Value), 
                    int.Parse(match.Groups[4].Value) * 10);
            }

            return TimeSpan.Zero;
        }

        public async Task<(bool success, string finalMessage)> RunConversionAsync(string arguments, TimeSpan totalDuration, IProgress<double> progress, CancellationToken cancellationToken)
        {
            // Add progress reporting argument to ffmpeg command
            string finalArguments = $"-nostdin -progress pipe:1 {arguments}";

            using var process = new Process
            {
                StartInfo =
                {
                    FileName = _ffmpegPath,
                    Arguments = finalArguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                },
                EnableRaisingEvents = true
            };

            var stdErrReader = new StringWriter();
            var lastMessage = string.Empty;

            process.ErrorDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    Logger.Log($"[FFMPEG_ERR]: {e.Data}");
                    stdErrReader.WriteLine(e.Data); // Capture all stderr for final message on failure
                }
            };
            
            process.Start();
            process.BeginErrorReadLine();

            using (cancellationToken.Register(() =>
            {
                Logger.Log($"[FFMPEGRUNNER] Cancellation requested for process {process.Id}. Attempting to kill FFmpeg process.");
                try
                {
                    if (!process.HasExited)
                    {
                        process.Kill(true); // Kill process and its entire tree
                        Logger.Log($"[FFMPEGRUNNER] FFmpeg process {process.Id} killed successfully.");
                    }
                    else
                    {
                        Logger.Log($"[FFMPEGRUNNER] FFmpeg process {process.Id} already exited.");
                    }
                }
                catch (Exception ex)
                {
                     Logger.Log($"Error killing process {process.Id}: {ex.Message}");
                }
            }))
            {
                try
                {
                    string? line;
                    TimeSpan processedTime = TimeSpan.Zero;
                    while ((line = await process.StandardOutput.ReadLineAsync()) != null) // Removed cancellationToken from ReadLineAsync
                    {
                        if (cancellationToken.IsCancellationRequested) // Explicit check for cancellation
                        {
                            Logger.Log($"[FFMPEGRUNNER] Cancellation detected during stream read, breaking loop for process {process.Id}.");
                            break; // Exit the loop early
                        }
                        
                        Logger.Log($"[FFMPEG_OUT]: {line}");
                        if (line.StartsWith("out_time=") && totalDuration > TimeSpan.Zero)
                        {
                            var timeMatch = Regex.Match(line, @"out_time=(\d{2}):(\d{2}):(\d{2})\.(\d{6})");
                            if (timeMatch.Success)
                            {
                                processedTime = TimeSpan.Parse(timeMatch.Groups[0].Value.Replace("out_time=", ""), CultureInfo.InvariantCulture);
                                double percentage = Math.Min(100.0, (processedTime.TotalMilliseconds / totalDuration.TotalMilliseconds) * 100.0);
                                progress.Report(percentage);
                            }
                        }
                    }

                    await process.WaitForExitAsync(cancellationToken);

                    // After the process has exited, we MUST check if cancellation was the reason.
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Logger.Log($"[FFMPEGRUNNER] Process {process.Id} exited, and cancellation token was triggered. Returning 'Cancelled'.");
                        return (false, "Cancelled");
                    }

                    if (process.ExitCode == 0)
                    {
                        return (true, "Completed");
                    }
                    else
                    {
                        string finalError = stdErrReader.ToString();
                        return (false, string.IsNullOrWhiteSpace(finalError) ? $"FFmpeg failed with exit code {process.ExitCode}." : finalError);
                    }
                }
                catch (OperationCanceledException)
                {
                    Logger.Log($"[FFMPEGRUNNER] Conversion for process {process.Id} was cancelled due to OperationCanceledException.");
                    return (false, "Cancelled");
                }
            }
        }
    }
}
