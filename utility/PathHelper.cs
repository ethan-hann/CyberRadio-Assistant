using System.Diagnostics;

namespace RadioExt_Helper.utility
{
    public static class PathHelper
    {
        /// <summary>
        /// Retrieves the base game path (the folder containing <c>bin</c>) from the Cyberpunk 2077 executable.
        /// </summary>
        /// <param name="fileDialog">The <see cref="OpenFileDialog"/> to show to the user.</param>
        /// <param name="shouldLoop">Indicates whether the file dialog should continue showing until a valid file is selected.</param>
        /// <returns>The base path of the game or <see cref="string.Empty"/> if path couldn't be determined.</returns>
        public static string? GetGamePath(OpenFileDialog fileDialog, bool shouldLoop = false)
        {
            try
            {
                var gamePath = string.Empty;
                do
                {
                    if (fileDialog.ShowDialog() != DialogResult.OK) continue;
                    
                    if (fileDialog.FileName.Contains("Cyberpunk2077"))
                        gamePath = fileDialog.FileName;
                    else
                        MessageBox.Show(GlobalData.Strings.GetString("NonCyberpunkExe"));
                }
                while (gamePath.Equals(string.Empty) && shouldLoop);

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
            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        /// Checks for and retrieves (if it exists) the path to the <c>radioExt</c> folder where custom radios are placed.
        /// </summary>
        /// <returns>The path to the radioExt folder, or <see cref="string.Empty"/> if the path couldn't be determined.</returns>
        public static string GetRadioExtPath(string gameBasePath)
        {
            var path = Path.Combine(gameBasePath, "bin", "x64", "plugins", "cyber_engine_tweaks", "mods", "radioExt");
            try
            {
                return Path.Exists(path) ? path : string.Empty;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return string.Empty;
            }
        }
    }
}
