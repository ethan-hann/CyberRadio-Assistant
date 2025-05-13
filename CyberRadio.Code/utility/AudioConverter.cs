using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

namespace RadioExt_Helper.utility
{
    /// <summary>
    /// Provides methods for converting audio files to a valid format.
    /// </summary>
    public sealed class AudioConverter
    {
        private static AudioConverter? _instance;
        private static readonly object Lock = new();

        private AudioConverter() { }

        /// <summary>
        /// Event that is raised when a file conversion starts. Event data is the file path being converted.
        /// </summary>
        public event EventHandler<string>? ConversionStarted;

        /// <summary>
        /// Event that is raised on conversion progress. Event data is a tuple containing the file path and the percentage of completion (0-100).
        /// </summary>
        public event EventHandler<(string file, int percent)>? ConversionProgress;

        /// <summary>
        /// Event that is raised when a conversion fails. Event data is a tuple containing the input file path, a flag indicating success, and the error message (if any)/output path.
        /// If the conversion was successful, the output path will be provided.
        /// If the conversion failed, the error message will be provided.
        /// </summary>
        public event EventHandler<(string file, bool success, string messageOrOutputPath)>? ConversionCompleted;

        /// <summary>
        /// Get the singleton instance of the AudioConverter class.
        /// </summary>
        public static AudioConverter Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ??= new AudioConverter();
                }
            }
        }

        /// <summary>
        /// The working directory for FFmpeg binaries and conversion.
        /// </summary>
        public string? WorkingDirectory { get; private set; }

        /// <summary>
        /// The directory where converted files are saved.
        /// </summary>
        public string? ConvertedDirectory { get; private set; }

        /// <summary>
        /// Indicates whether the AudioConverter has been initialized.
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Initializes the AudioConverter instance and downloads FFmpeg if needed.
        /// </summary>
        public async Task<List<string>> InitializeAsync()
        {
            var messages = new List<string>();
            if (IsInitialized) return [];
            try
            {
                AuLogger.GetCurrentLogger<AudioConverter>("InitializeAsync").Info("Initializing FFmpeg...");

                messages.AddRange(SetupRequiredPaths());

                if (WorkingDirectory == null)
                {
                    messages.Add("Working directory is null.");
                    return messages;
                }

                // Check if FFmpeg is already downloaded
                if (FFmpeg.ExecutablesPath != null && Directory.Exists(FFmpeg.ExecutablesPath))
                {
                    if (File.Exists(Path.Combine(FFmpeg.ExecutablesPath, "ffmpeg")) &&
                        File.Exists(Path.Combine(FFmpeg.ExecutablesPath, "ffprobe")))
                    {
                        messages.Add("FFmpeg binaries already exist.");
                        IsInitialized = true;
                        return messages;
                    }
                }

                await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official, WorkingDirectory);
                IsInitialized = true;
                messages.Add("FFmpeg binaries are ready.");
                return messages;
            }
            catch (Exception ex)
            {
                messages.Add($"Failed to initialize FFmpeg: {ex}");
                return messages;
            }
        }

        private List<string> SetupRequiredPaths()
        {
            var messages = new List<string>();
            try
            {
                WorkingDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "RadioExt-Helper", "ffmpeg");

                ConvertedDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "RadioExt-Helper", "converted-audio");

                if (!Directory.Exists(WorkingDirectory))
                    Directory.CreateDirectory(WorkingDirectory);

                if (!Directory.Exists(ConvertedDirectory))
                    Directory.CreateDirectory(ConvertedDirectory);

                FFmpeg.SetExecutablesPath(WorkingDirectory);
                messages.Add($"FFmpeg executables path set to: {WorkingDirectory}");
                messages.Add($"Converted audio directory set: {ConvertedDirectory}");
                return messages;
            }
            catch (Exception ex)
            {
                messages.Add($"Failed to set up FFmpeg paths: {ex}");
                WorkingDirectory = null;
                ConvertedDirectory = null;
                return messages;
            }
        }

        /// <summary>
        /// Checks if the given file needs conversion based on its extension.
        /// </summary>
        /// <param name="inputPath">The file path to check.</param>
        /// <returns><c>true</c> if conversion is needed; <c>false</c> otherwise.</returns>
        public bool NeedsConversion(string inputPath) => !PathHelper.IsValidAudioFile(inputPath);

        /// <summary>
        /// Converts the given file to <c>.mp3</c> if it is not a supported format.
        /// </summary>
        /// <param name="inputPath">The path to the input audio file.</param>
        /// <param name="outputDirectory">The directory to save the converted file. If null, uses input file's directory.</param>
        /// <returns>The path to the converted file, or null if conversion failed or not needed.</returns>
        public async Task<string?> ConvertToMp3Async(string inputPath, string? outputDirectory)
        {
            var logger = AuLogger.GetCurrentLogger<AudioConverter>("ConvertToMp3IfNeededAsync");
            try
            {
                // Check if the file needs conversion
                var needsConvert = NeedsConversion(inputPath);
                AuLogger.GetCurrentLogger<AudioConverter>("NeedsConversion")
                    .Info(needsConvert
                        ? $"File '{inputPath}' is not a valid audio file. Conversion needed."
                        : $"File '{inputPath}' is a valid audio file. No conversion needed.");
                if (!needsConvert)
                    return null;

                // Prepare output path
                if (ConvertedDirectory == null)
                {
                    logger.Error("Converted directory is not set.");
                    throw new InvalidOperationException("Converted directory is not set.");
                }

                var outputFile = Path.Combine(outputDirectory ?? ConvertedDirectory, 
                    Path.GetFileNameWithoutExtension(inputPath) + ".mp3");

                // Fire event and log
                ConversionStarted?.Invoke(this, inputPath);
                logger.Info($"Starting conversion: '{inputPath}' -> '{outputFile}'");

                // Ensure FFmpeg is ready
                await InitializeAsync();

                var conversion = FFmpeg.Conversions.New()
                    .AddParameter($"-y") // Overwrite output
                    .AddParameter($"-i \"{inputPath}\"")
                    .SetOutput(outputFile)
                    .SetOverwriteOutput(true);

                conversion.OnProgress += (_, args) =>
                {
                    var percent = args.Percent;
                    ConversionProgress?.Invoke(this, (inputPath, percent));
                    logger.Info($"Converting '{inputPath}': {percent}% complete.");
                };

                await conversion.Start();

                if (File.Exists(outputFile))
                {
                    ConversionCompleted?.Invoke(this, (inputPath, true, outputFile));
                    logger.Info($"Conversion complete: '{inputPath}' -> '{outputFile}'");
                    return outputFile;
                }
                else
                {
                    ConversionCompleted?.Invoke(this, (inputPath, false, "Output file not found after conversion."));
                    logger.Error($"Conversion failed: '{inputPath}'. Output file not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                ConversionCompleted?.Invoke(this, (inputPath, false, ex.Message));
                logger.Error(ex, $"Error converting '{inputPath}' to mp3.");
                return null;
            }
        }
    }
}
