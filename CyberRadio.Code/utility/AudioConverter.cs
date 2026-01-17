// AudioConverter.cs : RadioExt-Helper
// Copyright (C) 2026  Ethan Hann
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

#region

using AetherUtils.Core.Extensions;
using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using System;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

#endregion

namespace RadioExt_Helper.utility;

/// <summary>
///     Provides methods for converting audio/video files to supported formats
///     using FFmpeg (via Xabe.FFmpeg) with cancellation support.
/// </summary>
public sealed class AudioConverter
{
    private static AudioConverter? _instance;
    private static readonly object Lock = new();

    private AudioConverter()
    {
    }

    /// <summary>
    ///     Singleton instance of AudioConverter.
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
    ///     The directory where FFmpeg binaries are stored.
    /// </summary>
    public string? WorkingDirectory { get; private set; }

    /// <summary>
    ///     The directory where converted files are saved.
    /// </summary>
    public string? ConvertedDirectory { get; private set; }

    /// <summary>
    ///     True if FFmpeg binaries are downloaded and paths are set.
    /// </summary>
    public bool IsInitialized { get; private set; }

    /// <summary>Fired when a conversion starts (arg = input path).</summary>
    public event EventHandler<string>? ConversionStarted;

    /// <summary>Fired on progress (arg = (input path, percent)).</summary>
    public event EventHandler<(string file, int percent)>? ConversionProgress;

    /// <summary>
    ///     Fired when done or failed (arg = (input path, success, output path or error)).
    /// </summary>
    public event EventHandler<(string file, bool success, string messageOrOutputPath)>? ConversionCompleted;

    /// <summary>
    ///     Ensures FFmpeg binaries are downloaded & paths are set.
    /// </summary>
    public async Task<List<string>> InitializeAsync()
    {
        List<string> messages = [];
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
                await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official, WorkingDirectory);

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
        List<string> messages = new();
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
    ///     Returns true if the file is not already a supported audio format.
    /// </summary>
    public static bool NeedsConversion(string inputPath)
    {
        return !PathHelper.IsValidAudioFile(inputPath);
    }

    /// <summary>
    ///     Returns true if the file is not already a supported audio format.
    /// </summary>
    /// <param name="inputPath">The input file to check.</param>
    /// <param name="targetExtension">The target extension that the file should be.</param>
    /// <returns></returns>
    public static bool NeedsConversion(string inputPath, string targetExtension)
    {
        return !inputPath.EndsWith(targetExtension, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Converts a file to the specified target format based on the conversion candidate.
    /// </summary>
    /// <param name="candidate">The <see cref="ConvertCandidate" /> to use for this conversion.</param>
    /// <param name="cancellationToken">An optional token to support cancellation.</param>
    /// <param name="byPassNeedsConversionCheck">
    ///     If true, will bypass the <c>NeedsConversion</c> check and convert the file
    ///     anyway.
    /// </param>
    /// <returns></returns>
        public async Task<string?> ConvertAsync(ConvertCandidate candidate, bool byPassNeedsConversionCheck,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(candidate);

        if (!byPassNeedsConversionCheck)
            if (!NeedsConversion(candidate.InputPath, candidate.TargetFormat.ToDescriptionString()))
                return candidate.InputPath;

        await InitializeAsync().ConfigureAwait(false);

        var logger = AuLogger.GetCurrentLogger<AudioConverter>("ConvertAsync");

        ConversionStarted?.Invoke(this, candidate.InputPath);

        string? tempInputPath = null;

        var normalizedOutputPath = PathHelper.SanitizeFilePath(candidate.OutputPath);

        try
        {
            if (!File.Exists(candidate.InputPath))
            {
                ConversionCompleted?.Invoke(this, (candidate.InputPath, false, "Input file not found."));
                return null;
            }

            if (ConvertedDirectory is null)
            {
                ConversionCompleted?.Invoke(this, (candidate.InputPath, false, "ConvertedDirectory is null."));
                return null;
            }

            if (!TryGetTargetExtension(candidate, out var targetExtension, out var extensionError))
            {
                ConversionCompleted?.Invoke(this, (candidate.InputPath, false, extensionError));
                return null;
            }

            var outputDir = Path.GetDirectoryName(normalizedOutputPath);
            if (!string.IsNullOrEmpty(outputDir))
                Directory.CreateDirectory(outputDir);

            var tempInputDir = Path.Combine(ConvertedDirectory, "input-temp");
            Directory.CreateDirectory(tempInputDir);

            var originalFileName = Path.GetFileName(candidate.InputPath);
            var uniqueSuffix = Guid.NewGuid().ToString("N");
            var extension = Path.GetExtension(originalFileName);
            var baseName = Path.GetFileNameWithoutExtension(originalFileName);

            var proposedTempFileName = $"{baseName}_{uniqueSuffix}{extension}";
            var proposedTempFullPath = Path.Combine(tempInputDir, proposedTempFileName);

            tempInputPath = PathHelper.SanitizeFilePath(proposedTempFullPath);

            File.Copy(candidate.InputPath, tempInputPath, true);
            var inputPathForFfmpeg = tempInputPath;

            // Ensure output path matches the requested extension
            if (!normalizedOutputPath.EndsWith(targetExtension, StringComparison.OrdinalIgnoreCase))
            {
                var outDir = Path.GetDirectoryName(normalizedOutputPath) ?? string.Empty;
                var outName = Path.GetFileNameWithoutExtension(normalizedOutputPath);
                normalizedOutputPath = Path.Combine(outDir, outName + targetExtension);
                normalizedOutputPath = PathHelper.SanitizeFilePath(normalizedOutputPath);
            }

            var conversion = FFmpeg.Conversions.New();

            // Robust defaults:
            // - disable video (important for mkv/mp4)
            // - explicitly select audio codec per target
            conversion.AddParameter($"-vn -i \"{inputPathForFfmpeg}\"", ParameterPosition.PreInput);
            ApplyAudioEncodingParameters(ref conversion, candidate.TargetFormat);

            conversion.SetOutput(normalizedOutputPath);
            conversion.SetOverwriteOutput(true);

            conversion.OnProgress += (_, prog) =>
                ConversionProgress?.Invoke(this, (candidate.InputPath, prog.Percent));

            await conversion.Start(cancellationToken).ConfigureAwait(false);

            if (File.Exists(normalizedOutputPath))
            {
                ConversionCompleted?.Invoke(this, (candidate.InputPath, true, normalizedOutputPath));
                return normalizedOutputPath;
            }

            ConversionCompleted?.Invoke(this, (candidate.InputPath, false, "Output file not found."));
            return null;
        }
        catch (OperationCanceledException)
        {
            ConversionCompleted?.Invoke(this, (candidate.InputPath, false, "Cancelled"));
            return null;
        }
        catch (Exception ex)
        {
            logger.Error(ex, "ConvertAsync");
            ConversionCompleted?.Invoke(this, (candidate.InputPath, false, ex.Message));
            return null;
        }
        finally
        {
            try
            {
                if (!string.IsNullOrEmpty(tempInputPath) && File.Exists(tempInputPath))
                    File.Delete(tempInputPath);
            }
            catch (Exception ex)
            {
                logger.Warn($"Failed to delete temp input file: {tempInputPath}. {ex.Message}");
            }
        }
    }

    private static bool TryGetTargetExtension(ConvertCandidate candidate, out string extension, out string error)
    {
        extension = candidate.TargetFormat.ToDescriptionString();
        error = string.Empty;

        if (string.IsNullOrWhiteSpace(extension) || !extension.StartsWith('.'))
        {
            error = "Target extension is invalid.";
            return false;
        }

        if (string.Equals(extension, ".wem", StringComparison.OrdinalIgnoreCase))
        {
            error = "WEM conversion is not supported.";
            return false;
        }

        return true;
    }

    private static void ApplyAudioEncodingParameters(ref IConversion conversion, ValidAudioFiles targetFormat)
    {
        // Note: If you need "exact" radioExt expectations, you can tighten (sample rate/channels/bitrate) per format.
        switch (targetFormat)
        {
            case ValidAudioFiles.Wav:
                // Force PCM encoder explicitly (otherwise FFmpeg can end up with codec none)
                conversion.AddParameter("-c:a pcm_s16le -ar 44100 -ac 2");
                break;

            case ValidAudioFiles.Mp3:
                conversion.AddParameter("-c:a libmp3lame -q:a 2");
                break;

            case ValidAudioFiles.Ogg:
                // Use Opus inside OGG for best availability; if Vorbis is needed specifically, swap to libvorbis.
                conversion.AddParameter("-c:a libvorbis -b:a 192k");
                break;

            case ValidAudioFiles.Flac:
                conversion.AddParameter("-c:a flac");
                break;

            case ValidAudioFiles.Mp2:
                conversion.AddParameter("-c:a mp2 -b:a 192k -ar 44100 -ac 2");
                break;

            case ValidAudioFiles.Wma:
            case ValidAudioFiles.Wax:
                // Use WMAV2 in ASF container (wma/wax)
                conversion.AddParameter("-f asf -c:a wmav2 -b:a 192k -ar 44100 -ac 2 -af aresample=async=1");
                break;

            case ValidAudioFiles.Wem:
                // excluded by TryGetTargetExtension
                break;

            default:
                conversion.AddParameter("-c:a pcm_s16le -ar 44100 -ac 2");
                break;
        }
    }
}