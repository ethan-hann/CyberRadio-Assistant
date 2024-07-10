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