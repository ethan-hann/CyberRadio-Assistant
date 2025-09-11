// AudioConverter.cs : RadioExt-Helper
// Copyright (C) 2025  Ethan Hann
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

using AetherUtils.Core.Extensions;
using AetherUtils.Core.Logging;
using RadioExt_Helper.models;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

namespace RadioExt_Helper.utility;

/// <summary>
/// Provides methods for converting audio/video files to supported formats
/// using FFmpeg (via Xabe.FFmpeg) with cancellation support.
/// </summary>
public sealed class AudioConverter
{
    private static AudioConverter? _instance;
    private static readonly object Lock = new();

    private AudioConverter()
    {
    }

    /// <summary>
    /// Singleton instance of AudioConverter.
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

    /// <summary>Fired when a conversion starts (arg = input path).</summary>
    public event EventHandler<string>? ConversionStarted;

    /// <summary>Fired on progress (arg = (input path, percent)).</summary>
    public event EventHandler<(string file, int percent)>? ConversionProgress;

    /// <summary>
    /// Fired when done or failed (arg = (input path, success, output path or error)).
    /// </summary>
    public event EventHandler<(string file, bool success, string messageOrOutputPath)>? ConversionCompleted;

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
    public static bool NeedsConversion(string inputPath)
    {
        return !PathHelper.IsValidAudioFile(inputPath);
    }

    /// <summary>
    /// Returns true if the file is not already a supported audio format.
    /// </summary>
    /// <param name="inputPath">The input file to check.</param>
    /// <param name="targetExtension">The target extension that the file should be.</param>
    /// <returns></returns>
    public static bool NeedsConversion(string inputPath, string targetExtension)
    {
        return !inputPath.EndsWith(targetExtension, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Converts a file to the specified target format based on the conversion candidate.
    /// </summary>
    /// <param name="candidate">The <see cref="ConvertCandidate"/> to use for this conversion.</param>
    /// <param name="cancellationToken">An optional token to support cancellation.</param>
    /// <param name="byPassNeedsConversionCheck">If true, will bypass the <c>NeedsConversion</c> check and convert the file anyway.</param>
    /// <returns></returns>
    public async Task<string?> ConvertAsync(ConvertCandidate candidate, bool byPassNeedsConversionCheck, CancellationToken cancellationToken = default)
    {
        if (candidate is null)
            throw new ArgumentNullException(nameof(candidate));

        if (!byPassNeedsConversionCheck)
        {
            // skip if already correct extension
            if (!NeedsConversion(candidate.InputPath, candidate.TargetFormat.ToDescriptionString()))
                return candidate.InputPath;
        }

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

            const string msg = "Output file not found.";
            ConversionCompleted?.Invoke(this, (candidate.InputPath, false, msg));
            return null;
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