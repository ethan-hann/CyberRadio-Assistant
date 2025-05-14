using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AetherUtils.Core.Extensions;
using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

namespace RadioExt_Helper.utility
{
    /// <summary>
    /// Provides methods for converting audio/video files to .mp3
    /// using FFmpeg (via Xabe.FFmpeg) with cancellation support.
    /// </summary>
    public sealed class AudioConverter
    {
        private static AudioConverter? _instance;
        private static readonly object Lock = new();

        private AudioConverter() { }

        /// <summary>
        /// Singleton instance of AudioConverter.
        /// </summary>
        public static AudioConverter Instance
        {
            get
            {
                lock (Lock)
                    return _instance ??= new AudioConverter();
            }
        }

        /// <summary>Fired when a conversion starts (arg = input path).</summary>
        public event EventHandler<string>? ConversionStarted;

        /// <summary>Fired on progress (arg = (input path, percent)).</summary>
        public event EventHandler<(string file, int percent)>? ConversionProgress;

        /// <summary>
        /// Fired when done or failed (arg = (input path, success, output path or error)).
        /// </summary>
        public event EventHandler<(string file, bool success, string messageOrOutputPath)>? ConversionCompleted;

        /// <summary>
        /// The directory where FFmpeg binaries are stored.
        /// </summary>
        public string? WorkingDirectory { get; private set; }

        /// <summary>
        /// The directory where converted files are saved.
        /// </summary>
        public string? ConvertedDirectory { get; private set; }

        /// <summary>
        /// True if FFmpeg binaries are downloaded and paths are set.
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Ensures FFmpeg binaries are downloaded & paths are set.
        /// </summary>
        public async Task<List<string>> InitializeAsync()
        {
            var messages = new List<string>();
            if (IsInitialized) return messages;

            var logger = AuLogger.GetCurrentLogger<AudioConverter>("InitializeAsync");
            try
            {
                logger.Info("Initializing FFmpeg...");
                messages.AddRange(SetupRequiredPaths());

                if (WorkingDirectory is null)
                {
                    messages.Add("WorkingDirectory is null.");
                    return messages;
                }

                // Download if missing
                if (!File.Exists(Path.Combine(WorkingDirectory, "ffmpeg")) ||
                    !File.Exists(Path.Combine(WorkingDirectory, "ffprobe")))
                {
                    await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official, WorkingDirectory);
                }

                IsInitialized = true;
                messages.Add("FFmpeg binaries are ready.");
            }
            catch (Exception ex)
            {
                messages.Add($"Failed to initialize FFmpeg: {ex.Message}");
                logger.Error(ex, "InitializeAsync");
            }

            return messages;
        }

        private List<string> SetupRequiredPaths()
        {
            var messages = new List<string>();
            try
            {
                WorkingDirectory = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "RadioExt-Helper", "ffmpeg");
                ConvertedDirectory = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "RadioExt-Helper", "converted-audio");

                Directory.CreateDirectory(WorkingDirectory!);
                Directory.CreateDirectory(ConvertedDirectory!);

                FFmpeg.SetExecutablesPath(WorkingDirectory);
                messages.Add($"FFmpeg path: {WorkingDirectory}");
                messages.Add($"Converted directory: {ConvertedDirectory}");
            }
            catch (Exception ex)
            {
                messages.Add($"Path setup failed: {ex.Message}");
                WorkingDirectory = null;
                ConvertedDirectory = null;
            }

            return messages;
        }

        /// <summary>
        /// Returns true if the file is not already a supported audio format.
        /// </summary>
        public static bool NeedsConversion(string inputPath) =>
            !PathHelper.IsValidAudioFile(inputPath);

        /// <summary>
        /// Returns true if the file is not already a supported audio format.
        /// </summary>
        /// <param name="inputPath">The input file to check.</param>
        /// <param name="targetExtension">The target extension that the file should be.</param>
        /// <returns></returns>
        public static bool NeedsConversion(string inputPath, string targetExtension)
            => !inputPath.EndsWith(targetExtension, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Converts <paramref name="inputPath"/> to .mp3 in <paramref name="outputDirectory"/> (or default).
        /// Honors <paramref name="cancellationToken"/> and raises all three events.
        /// </summary>
        public async Task<string?> ConvertToMp3Async(
            string inputPath,
            string? outputDirectory = null,
            CancellationToken cancellationToken = default)
        {
            var logger = AuLogger.GetCurrentLogger<AudioConverter>("ConvertToMp3Async");

            if (!NeedsConversion(inputPath))
                return null;

            if (ConvertedDirectory is null)
            {
                const string err = "ConvertedDirectory is not set.";
                logger.Error(err);
                throw new InvalidOperationException(err);
            }

            var outDir = outputDirectory ?? ConvertedDirectory;
            var outputFile = Path.Combine(
                outDir,
                Path.GetFileNameWithoutExtension(inputPath) + ".mp3");

            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                ConversionStarted?.Invoke(this, inputPath);
                logger.Info($"Starting conversion: {inputPath} → {outputFile}");

                await InitializeAsync();
                cancellationToken.ThrowIfCancellationRequested();

                // Use the built-in snippet for audio → mp3
                var conversion = await FFmpeg.Conversions
                    .FromSnippet.Convert(inputPath, outputFile);

                conversion.SetOverwriteOutput(true);

                conversion.OnProgress += (_, progress) =>
                {
                    ConversionProgress?.Invoke(this, (inputPath, progress.Percent));
                };

                await conversion.Start(cancellationToken);

                if (File.Exists(outputFile))
                {
                    ConversionCompleted?.Invoke(this, (inputPath, true, outputFile));
                    logger.Info($"Conversion succeeded: {outputFile}");
                    return outputFile;
                }
                else
                {
                    var msg = "Output file not found after conversion.";
                    ConversionCompleted?.Invoke(this, (inputPath, false, msg));
                    logger.Error(msg);
                    return null;
                }
            }
            catch (OperationCanceledException)
            {
                var msg = "Conversion cancelled by user.";
                ConversionCompleted?.Invoke(this, (inputPath, false, msg));
                logger.Warn(msg);
                return null;
            }
            catch (Exception ex)
            {
                ConversionCompleted?.Invoke(this, (inputPath, false, ex.Message));
                logger.Error(ex, $"Error converting '{inputPath}'");
                return null;
            }
        }

        /// <summary>
        /// Converts a file to the specified target format based on the conversion candidate.
        /// </summary>
        /// <param name="candidate">The <see cref="ConvertCandidate"/> to use for this conversion.</param>
        /// <param name="cancellationToken">An optional token to support cancellation.</param>
        /// <returns></returns>
        public async Task<string?> ConvertAsync(ConvertCandidate candidate, CancellationToken cancellationToken = default)
        {
            // skip if already correct extension
            if (!NeedsConversion(candidate.InputPath, candidate.TargetFormat.ToDescriptionString()))
                return candidate.InputPath;

            // ensure we have ffmpeg/ffprobe
            await InitializeAsync().ConfigureAwait(false);

            ConversionStarted?.Invoke(this, candidate.InputPath);

            var conversion = await FFmpeg.Conversions
                .FromSnippet.Convert(candidate.InputPath, candidate.OutputPath);

            conversion.SetOverwriteOutput(true);

            conversion.OnProgress += (_, prog) =>
                ConversionProgress?.Invoke(this, (candidate.InputPath, prog.Percent));

            try
            {
                await conversion.Start(cancellationToken).ConfigureAwait(false);

                if (File.Exists(candidate.OutputPath))
                {
                    ConversionCompleted?.Invoke(this, (candidate.InputPath, true, candidate.OutputPath));
                    return candidate.OutputPath;
                }
                else
                {
                    const string msg = "Output file not found.";
                    ConversionCompleted?.Invoke(this, (candidate.InputPath, false, msg));
                    return null;
                }
            }
            catch (OperationCanceledException)
            {
                ConversionCompleted?.Invoke(this, (candidate.InputPath, false, "Cancelled"));
                return null;
            }
            catch (Exception ex)
            {
                ConversionCompleted?.Invoke(this, (candidate.InputPath, false, ex.Message));
                return null;
            }
        }
    }
}
