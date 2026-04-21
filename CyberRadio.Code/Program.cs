// Program.cs : RadioExt-Helper
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

using System.Diagnostics;
using RadioExt_Helper.forms;
using RadioExt_Helper.utility;

#endregion

namespace RadioExt_Helper;

internal static class Program
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
        if (args.Contains("--cleanup-temp"))
        {
            RunCleanupMode(args);
            return;
        }

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Initialize essential global data like the logger and string resources
        GlobalData.Initialize();

        // Show the splash screen as a modal dialog
        using (SplashScreen splashScreen = new())
        {
            splashScreen.ShowDialog();
        }

        // After the splash screen is closed, show the main form
        MainForm mainForm = new();

        // If the main form is exiting (hard-closing), do not run the application
        if (mainForm.IsHardClosing)
            return;

        Application.Run(mainForm);
    }

    private static void RunCleanupMode(string[] args)
    {
        var waitPid = GetArgumentIntValue(args, "--wait-pid");

        if (waitPid > 0)
            WaitForProcessExit(waitPid);

        CleanupTemporaryToolData();

        Process.Start(new ProcessStartInfo
        {
            FileName = Application.ExecutablePath,
            WorkingDirectory = AppContext.BaseDirectory,
            UseShellExecute = true
        });
    }

    private static int GetArgumentIntValue(string[] args, string key)
    {
        var index = Array.IndexOf(args, key);
        if (index < 0 || index + 1 >= args.Length) return 0;

        return int.TryParse(args[index + 1], out var value) ? value : 0;
    }

    private static void WaitForProcessExit(int pid)
    {
        try
        {
            using var process = Process.GetProcessById(pid);
            process.WaitForExit(30000);
        }
        catch
        {
            // Process already exited
        }
    }

    private static void CleanupTemporaryToolData()
    {
        var wigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Wolven Icon Generator");
        DeleteDirectoryWithRetry(wigPath);

        var craPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "RadioExt-Helper");
        if (!Directory.Exists(craPath)) return;

        foreach (var item in Directory.EnumerateFileSystemEntries(craPath))
        {
            var fileName = Path.GetFileName(item);
            if (fileName.Equals("logs", StringComparison.OrdinalIgnoreCase) ||
                fileName.Equals("config.yml", StringComparison.OrdinalIgnoreCase))
                continue;

            if (Directory.Exists(item))
                DeleteDirectoryWithRetry(item);
            else if (File.Exists(item))
                DeleteFileWithRetry(item);
        }
    }

    private static void DeleteDirectoryWithRetry(string path, int maxAttempts = 8)
    {
        for (var attempt = 1; attempt <= maxAttempts; attempt++)
            try
            {
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
                return;
            }
            catch when (attempt < maxAttempts)
            {
                Thread.Sleep(300);
            }
    }

    private static void DeleteFileWithRetry(string path, int maxAttempts = 8)
    {
        for (var attempt = 1; attempt <= maxAttempts; attempt++)
            try
            {
                if (File.Exists(path))
                    File.Delete(path);
                return;
            }
            catch when (attempt < maxAttempts)
            {
                Thread.Sleep(300);
            }
    }
}