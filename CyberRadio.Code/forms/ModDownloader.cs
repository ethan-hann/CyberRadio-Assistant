// ModDownloader.cs : RadioExt-Helper
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

using RadioExt_Helper.theming;

namespace RadioExt_Helper.forms;

//Eventual implementation of a mod downloader form
public partial class ModDownloader : Form
{
    //private readonly string _gameId = "cyberpunk2077";
    //private readonly string _nexusBaseUrl;

    /// <summary>
    /// Contains all mods that have been downloaded from Nexus, indexed by their ModID.
    /// </summary>
    //private readonly Dictionary<int, Mod> _modsDictionary = [];

    //private readonly BindingList<Mod> _modsDownloadQueue = [];
    public ModDownloader()
    {
        InitializeComponent();

        ThemeManager.Instance.ApplyThemeToControl(this);
        //_nexusBaseUrl = $"https://www.nexusmods.com/{_gameId}";
    }
}