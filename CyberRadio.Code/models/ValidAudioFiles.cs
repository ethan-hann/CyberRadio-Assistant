using System.ComponentModel;

namespace RadioExt_Helper.models;

/// <summary>
/// Enum representing the valid audio files recognized by the radioExt mod.
/// </summary>
public enum ValidAudioFiles
{
    [Description(".mp3")]
    Mp3,
    [Description(".wav")]
    Wav,
    [Description(".ogg")]
    Ogg,
    [Description(".flac")]
    Flac,
    [Description(".mp2")]
    Mp2,
    [Description(".wax")]
    Wax,
    [Description(".wma")]
    Wma
}