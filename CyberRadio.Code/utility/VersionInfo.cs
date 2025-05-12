// VersionInfo.cs : RadioExt-Helper
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

namespace RadioExt_Helper.utility;

/// <summary>
///     Simple class to hold the latest version of the application and the download URL.
/// </summary>
/// <param name="latestVersion">The latest version of the application retrieved from GitHub.</param>
/// <param name="downloadUrl">The direct download URL for the latest version.</param>
public sealed class VersionInfo(Version latestVersion, string downloadUrl)
{
    /// <summary>
    ///     The latest version of the application retrieved from GitHub.
    /// </summary>
    public Version LatestVersion { get; set; } = latestVersion;

    /// <summary>
    ///     The direct download URL for the latest version.
    /// </summary>
    public string DownloadUrl { get; set; } = downloadUrl;
}