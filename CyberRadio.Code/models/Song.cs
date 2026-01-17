// Song.cs : RadioExt-Helper
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

using AetherUtils.Core.Logging;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;
using RadioExt_Helper.utility;
using Xabe.FFmpeg;

#endregion

namespace RadioExt_Helper.models;

/// <summary>
///     Represents a single song entry. Song information is read from the file on disk.
/// </summary>
public sealed partial class Song : IEquatable<Song>, ICloneable
{
    /// <summary>
    ///     The name of the song.
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    ///     The artist of the song or audio file.
    /// </summary>
    [JsonProperty("artist")]
    public string Artist { get; set; } = string.Empty;

    /// <summary>
    ///     The duration of the song or audio file.
    /// </summary>
    [JsonProperty("duration")]
    public TimeSpan Duration { get; set; }

    /// <summary>
    ///     The file size (in bytes) of the song or audio file.
    /// </summary>
    [JsonProperty("file_size")]
    public ulong FileSize { get; set; }

    /// <summary>
    ///     The original file path of the song or audio file on disk.
    /// </summary>
    [JsonProperty("file_path")]
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// Creates a new Song object that is a copy of the current instance.
    /// </summary>
    /// <remarks>The cloned Song is a shallow copy; reference-type properties, if any, are not deeply cloned.
    /// Use this method to create a duplicate Song that can be modified independently of the original.</remarks>
    /// <returns>A new Song object with the same property values as the current instance.</returns>
    public object Clone()
    {
        return new Song
        {
            Title = Title,
            Artist = Artist,
            Duration = Duration,
            FileSize = FileSize,
            FilePath = FilePath
        };
    }

    /// <summary>
    /// Determines whether the current Song instance is equal to another Song instance.
    /// </summary>
    /// <remarks>Two Song instances are considered equal if their Title, Artist, Duration, FileSize, and
    /// FilePath properties are all equal.</remarks>
    /// <param name="other">The Song instance to compare with the current instance. Can be null.</param>
    /// <returns>true if the specified Song is equal to the current Song; otherwise, false.</returns>
    public bool Equals(Song? other)
    {
        if (other == null) return false;
        return Title.Equals(other.Title) &&
               Artist.Equals(other.Artist) &&
               Duration.Equals(other.Duration) &&
               FileSize.Equals(other.FileSize) &&
               FilePath.Equals(other.FilePath);
    }

    private static readonly HashSet<string> RejectedExactTitles = new(StringComparer.OrdinalIgnoreCase)
    {
        "unknown",
        "untitled",
        "n/a",
        "na",
        "-"
    };

    private static readonly string[] RejectedPrefixes =
    [
        "simple_"
    ];

    private static readonly Regex[] RejectedTitlePatterns =
    [
        RejectedRecording(),
        RejectedTitleTrack(),
        RejectedNumbersOnly()
    ];

    /// <summary>
    /// Creates a new Song instance by reading metadata from the specified audio file.
    /// </summary>
    /// <remarks>If the file format is not supported by the underlying metadata library, a fallback Song
    /// instance may be created with limited information. If an unexpected error occurs while reading the file, the
    /// method returns null and logs the error.</remarks>
    /// <param name="filePath">The full path to the audio file to read. Cannot be null or empty.</param>
    /// <returns>A Song instance containing metadata from the specified file, or null if the file could not be read or is in an
    /// unsupported format.</returns>
    public static async Task<Song?> FromFileAsync(string filePath)
    {
        try
        {
            return CreateSongFromFile(filePath);
        }
        catch (TagLib.UnsupportedFormatException)
        {
            // .wax is commonly treated as a Windows Media redirector/playlist by libraries like taglib#.
            // radioExt may still support it, but CRA should not fail hard here.
            return await CreateFallbackSongAsync(filePath);
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger<Song>("FromFile").Error(ex, $"Couldn't read song file: {filePath}");
            return null;
        }
    }

    /// <summary>
    /// Creates a fallback Song instance using the specified file path when full metadata is unavailable.
    /// </summary>
    /// <remarks>Use this method when only minimal information about a song file is available, such as when
    /// metadata extraction fails. The returned Song will have empty or default values for properties that cannot be
    /// determined from the file.</remarks>
    /// <param name="filePath">The path to the audio file for which to create the fallback Song. Cannot be null or empty.</param>
    /// <returns>A Song object with basic information derived from the file path. The Title is set to the file name without
    /// extension, Artist is empty, FileSize is set if the file exists, and Duration is set if it can be determined;
    /// otherwise, defaults are used.</returns>
    private static async Task<Song> CreateFallbackSongAsync(string filePath)
    {
        return new Song
        {
            FilePath = filePath,
            Title = Path.GetFileNameWithoutExtension(filePath),
            Artist = string.Empty,
            FileSize = File.Exists(filePath) ? (ulong)new FileInfo(filePath).Length : 0UL,
            Duration = await TryGetDurationWithFfprobe(filePath) ?? TimeSpan.Zero
        };
    }

    /// <summary>
    /// Attempts to retrieve the duration of a media file using ffprobe.
    /// </summary>
    /// <remarks>This method waits for the FFmpeg executables to be initialized before attempting to retrieve
    /// media information. If the file does not exist, FFmpeg is not properly initialized, or an error occurs, the
    /// method returns <see langword="null"/>. The operation is subject to a short timeout to prevent hanging on
    /// problematic files.</remarks>
    /// <param name="filePath">The full path to the media file for which to obtain the duration. Cannot be null or empty.</param>
    /// <returns>A <see cref="TimeSpan"/> representing the duration of the media file if successful; otherwise, <see
    /// langword="null"/>.</returns>
    private static async Task<TimeSpan?> TryGetDurationWithFfprobe(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                return null;

            var maxWaitTime = 10000; // 10 seconds
            const int pauseTime = 1000; // 1 second
            while (!AudioConverter.Instance.IsInitialized)
            {
                if (maxWaitTime <= 0)
                    break;

                // Wait for AudioConverter to initialize (FFmpeg executables to be ready).
                // This is needed because AudioConverter.InitializeAsync() may be running concurrently.
                await Task.Delay(pauseTime);

                maxWaitTime -= pauseTime;
            }

            // If FFmpeg executables path is not set or doesn't exist, we can't proceed. This should not happen at this point because of the wait above.
            if (string.IsNullOrWhiteSpace(FFmpeg.ExecutablesPath) || !Directory.Exists(FFmpeg.ExecutablesPath))
                return null;

            // Avoid hanging the UI on weird files by using a small timeout.
            using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(3));

            return await FFmpeg.GetMediaInfo(filePath, timeoutCts.Token)?.Duration;
        }
        catch
        {
            AuLogger.GetCurrentLogger<Song>("TryGetDurationWithFfprobe")
                .Warn($"Failed to get duration for file '{filePath}' using ffprobe.");
            return null;
        }
    }

    private static Song CreateSongFromFile(string filePath)
    {
        var file = TagLib.File.Create(filePath);

        var title = GetUserFacingTitle(filePath, file.Tag.Title);

        //Parse duration
        if (TimeSpan.TryParse(file.Tag.Length, CultureInfo.InvariantCulture,
                out var duration))
            return new Song
            {
                FilePath = filePath,
                Title = title,
                Artist = file.Tag.FirstPerformer ?? string.Empty,
                FileSize = (ulong)new FileInfo(filePath).Length,
                Duration = duration
            };

        return new Song
        {
            FilePath = filePath,
            Title = title,
            Artist = file.Tag.FirstPerformer ?? string.Empty,
            FileSize = (ulong)new FileInfo(filePath).Length,
            Duration = file.Properties.Duration
        };
    }

    /// <summary>
    /// Determines the most appropriate user-facing title for a file based on the provided tag title and file path.
    /// </summary>
    /// <remarks>This method prefers a provided tag title, but will fall back to using the file name if the
    /// tag title is missing, empty, or deemed unsuitable. The returned title is intended for display to end
    /// users.</remarks>
    /// <param name="filePath">The full path to the file. Used as a fallback to derive the title if a valid tag title is not provided.</param>
    /// <param name="tagTitle">An optional title from file metadata or tags. If null, empty, or invalid, the file name is used instead.</param>
    /// <returns>A string containing the user-facing title. Returns the normalized tag title if valid; otherwise, returns the
    /// file name without its extension.</returns>
    private static string GetUserFacingTitle(string filePath, string? tagTitle)
    {
        var fallback = Path.GetFileNameWithoutExtension(filePath);

        if (string.IsNullOrWhiteSpace(tagTitle))
            return fallback;

        var candidate = NormalizeTitle(tagTitle);

        if (candidate.Length == 0 || IsRejectedTitle(candidate))
            return fallback;

        return candidate;
    }

    /// <summary>
    /// Normalizes a title string by trimming leading and trailing whitespace and collapsing consecutive internal
    /// whitespace into a single space.
    /// </summary>
    /// <remarks>This method is useful for ensuring consistent title formatting, especially when matching or
    /// comparing titles that may contain irregular spacing.</remarks>
    /// <param name="title">The title string to normalize. Cannot be null.</param>
    /// <returns>A normalized string with trimmed edges and single spaces between words.</returns>
    private static string NormalizeTitle(string title)
    {
        // Trim + collapse internal whitespace so "  Track   01 " matches patterns consistently.
        var trimmed = title.Trim();
        return TrimRegex().Replace(trimmed, " ");
    }

    /// <summary>
    /// Determines whether the specified title matches any rejected titles, prefixes, or patterns.
    /// </summary>
    /// <param name="title">The title to evaluate against the list of rejected titles, prefixes, and patterns. Cannot be null.</param>
    /// <returns>true if the title is considered rejected; otherwise, false.</returns>
    private static bool IsRejectedTitle(string title)
    {
        if (RejectedExactTitles.Contains(title))
            return true;

        return RejectedPrefixes.Any(prefix => title.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) 
               || RejectedTitlePatterns.Any(pattern => pattern.IsMatch(title));
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as Song);
    }

    /// <inheritdoc />
    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode()
    {
        return HashCode.Combine(Title, Artist, Duration, FileSize, FilePath);
    }

    /// <summary>
    ///     Get a display friendly value representing this song.
    /// </summary>
    /// <returns>The <see cref="Title" /> of the song.</returns>
    public override string ToString()
    {
        return Title;
    }

    [GeneratedRegex(@"\brecording\b", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex RejectedRecording();
    [GeneratedRegex(@"^(track|title)\s*\d{1,3}$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex RejectedTitleTrack();
    [GeneratedRegex(@"^\d{1,4}$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex RejectedNumbersOnly();
    [GeneratedRegex(@"\s+")]
    private static partial Regex TrimRegex();
}