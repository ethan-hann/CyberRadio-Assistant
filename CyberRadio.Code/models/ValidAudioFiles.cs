// ValidAudioFiles.cs : RadioExt-Helper
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

using System.ComponentModel;

namespace RadioExt_Helper.models;

/// <summary>
/// Enum representing the valid audio files recognized by the radioExt mod.
/// This enum is mainly used for the description attributes (which are the file extensions) and not the enum values themselves.
/// </summary>
public enum ValidAudioFiles
{
    [Description(".mp3")] Mp3,
    [Description(".wav")] Wav,
    [Description(".ogg")] Ogg,
    [Description(".flac")] Flac,
    [Description(".mp2")] Mp2,
    [Description(".wax")] Wax,
    [Description(".wma")] Wma
}