using AetherUtils.Core.Logging;

namespace RadioExt_Helper.utility;

/// <summary>
///     Helper class to get the various paths associated with the game.
/// </summary>
public static class PathHelper
{
    /// <summary>
    ///     Retrieves the base game path (the folder containing <c>bin</c>) from the Cyberpunk 2077 executable.
    /// </summary>
    /// <param name="shouldLoop">Indicates whether the file dialog should continue showing until a valid file is selected.</param>
    /// <returns>The base path of the game or <see cref="string.Empty" /> if path couldn't be determined.</returns>
    public static string? GetGamePath(bool shouldLoop = false)
    {
        OpenFileDialog dialog = new()
        {
            Filter = @"Game Executable|Cyberpunk2077.exe",
            Title = GlobalData.Strings.GetString("Open") ?? "Open Game Executable"
        };

        try
        {
            var gamePath = string.Empty;
            do
            {
                if (dialog.ShowDialog() != DialogResult.OK) continue;

                if (dialog.FileName.Contains("Cyberpunk2077"))
                    gamePath = dialog.FileName;
                else
                    MessageBox.Show(GlobalData.Strings.GetString("NonCyberpunkExe"));
            } while (gamePath.Equals(string.Empty) && shouldLoop);

            var name = Directory.GetParent(gamePath)?.FullName;
            if (name != null)
            {
                var fullName = Directory.GetParent(name)?.FullName;
                if (fullName != null)
                {
                    var basePath = Directory.GetParent(fullName)?.FullName;
                    return basePath;
                }
            }
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("PathHelper.GetGamePath")
                .Error(ex, "Error retrieving base game path.");
            return string.Empty;
        }

        return string.Empty;
    }

    /// <summary>
    ///     Retrieves the staging path (the folder containing radio stations before copied to the game).
    /// </summary>
    /// <param name="shouldLoop">Indicates whether the file dialog should continue showing until a valid file is selected.</param>
    /// <returns>The base path of the game or <see cref="string.Empty" /> if path couldn't be determined.</returns>
    public static string GetStagingPath(bool shouldLoop = false)
    {
        FolderBrowserDialog dialog = new()
        {
            Description = GlobalData.Strings.GetString("StagingPathHelp") ?? "Select the radio station staging path.",
            UseDescriptionForTitle = true
        };

        try
        {
            var stagingPath = string.Empty;
            do
            {
                if (dialog.ShowDialog() != DialogResult.OK) continue;

                stagingPath = dialog.SelectedPath;
            } while (stagingPath.Equals(string.Empty) && shouldLoop);

            return stagingPath;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("PathHelper.GetStagingPath")
                .Error(ex, "Error retrieving staging path.");
            return string.Empty;
        }
    }

    /// <summary>
    ///     Checks for and retrieves (if it exists) the path to the <c>radioExt</c> folder where custom radios are placed.
    /// </summary>
    /// <returns>The path to the radioExt folder, or <see cref="string.Empty" /> if the path couldn't be determined.</returns>
    public static string GetRadioExtPath(string gameBasePath)
    {
        var path = Path.Combine(gameBasePath, "bin", "x64", "plugins", "cyber_engine_tweaks", "mods", "radioExt");
        try
        {
            return Path.Exists(path) ? path : string.Empty;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("PathHelper.GetRadioExtPath")
                .Error(ex, "Error retrieving path to radioExt mod's folder.");
            return string.Empty;
        }
    }

    /// <summary>
    ///     Get the path to the <c>radios</c> folder under the radioExt mod folder. This method does not check if the path
    ///     exists.
    /// </summary>
    /// <param name="gameBasePath">The base path of the game.</param>
    /// <returns>The path to the <c>radios</c> folder, based on the base path of the game.</returns>
    public static string GetRadiosPath(string gameBasePath)
    {
        return Path.Combine(GetRadioExtPath(gameBasePath), "radios");
    }
}