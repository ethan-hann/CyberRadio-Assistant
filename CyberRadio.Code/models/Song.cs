using System.Diagnostics;
using Newtonsoft.Json;
using File = TagLib.File;

namespace RadioExt_Helper.models;

/// <summary>
///     Represents a single song entry. Song information is read from the file on disk.
/// </summary>
public class Song
{
    /// <summary>
    ///     The name of the song.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

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
    [JsonProperty("size")]
    public ulong Size { get; set; }

    /// <summary>
    ///     The original file path of the song or audio file on disk.
    /// </summary>
    [JsonProperty("original_path")]
    public string OriginalFilePath { get; set; } = string.Empty;

    /// <summary>
    /// Get metadata about an audio file and return a new <see cref="Song"/> object.
    /// </summary>
    /// <param name="filePath">The path to the audio file.</param>
    /// <returns>A new <see cref="Song"/> with the metadata or <c>null</c> if an exception occurred.</returns>
    public static Song? ParseFromFile(string filePath)
    {
        Song? song = new();
        try
        {
            var file = File.Create(filePath);
            song.OriginalFilePath = filePath;
            song.Name = file.Tag.Title ?? Path.GetFileName(filePath);
            song.Artist = file.Tag.FirstPerformer ?? string.Empty;
            song.Size = (ulong)new FileInfo(filePath).Length;
            song.Duration = file.Properties.Duration;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            song = null;
        }

        return song;
    }
    
    public override string ToString()
    {
        return Name;
    }
}