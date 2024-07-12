// Program.cs : RadioExt-Helper
// Copyright (C) 2024  Ethan Hann
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

using RadioExt_Helper.forms;
using RadioExt_Helper.utility;

namespace RadioExt_Helper;

internal static class Program
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Initialize essential global data like the logger and string resources
        GlobalData.Initialize();

        // Show the splash screen as a modal dialog
        using (var splashScreen = new SplashScreen())
        {
            splashScreen.ShowDialog();
        }

        // After the splash screen is closed, show the main form
        Application.Run(new MainForm());
    }
}