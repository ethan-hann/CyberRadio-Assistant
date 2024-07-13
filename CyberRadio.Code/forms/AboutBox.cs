// AboutBox.cs : RadioExt-Helper
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

using System.Reflection;
using AetherUtils.Core.Extensions;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

/// <summary>
///     Represents the About Box form.
/// </summary>
public partial class AboutBox : Form
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AboutBox" /> class.
    /// </summary>
    public AboutBox()
    {
        InitializeComponent();
    }

    /// <summary>
    ///     Handles the Load event of the AboutBox control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void AboutBox_Load(object sender, EventArgs e)
    {
        Translate();
    }

    /// <summary>
    ///     Translates the text of the form and its controls based on the language settings.
    /// </summary>
    private void Translate()
    {
        lblAppName.Text = GlobalData.Strings.GetString("MainTitle") ?? "Cyber Radio Assistant";
        lblAboutInfo.Text = GlobalData.Strings.GetString("AboutInfo") ??
                            "A tool to help create and manage custom radio stations " +
                            "in Cyberpunk 2077 when using the radioExt Mod.";
        lblSpecialThanks1.Text = GlobalData.Strings.GetString("AboutSpecialThanks1") ?? "Special thanks to";
        lblSpecialThanks2.Text = GlobalData.Strings.GetString("AboutSpecialThanks2") ?? "for the awesome radioExt mod!";
        lblVersion.Text = string.Format(GlobalData.Strings.GetString("AboutVersion") ?? "Version {0}", GlobalData.AppVersion);
        lnkGithubRepo.Text = GlobalData.Strings.GetString("AboutGithubRepo") ?? "Github Repo";
    }

    private void LnkRadioExtDev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        "https://github.com/justarandomguyintheinternet/CP77_radioExt".OpenUrl();
    }

    private void LnkGithubRepo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        "https://github.com/ethan-hann/CyberRadio-Assistant".OpenUrl();
    }

    private void LnkLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        "https://github.com/ethan-hann/CyberRadio-Assistant/blob/main/LICENSE".OpenUrl();
    }

    private void LnkNexusMods_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        "https://www.nexusmods.com/cyberpunk2077/mods/15338".OpenUrl();
    }
}