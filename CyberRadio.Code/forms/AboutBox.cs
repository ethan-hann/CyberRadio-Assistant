// AboutBox.cs : RadioExt-Helper
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

        PopulateRichText();
    }

    private void PopulateRichText()
    {
        rtbCredits.DetectUrls = true;
        rtbCredits.LinkClicked += (s, e) =>
        {
            if (e.LinkText is { } url)
                url.OpenUrl();
        };

        // Set the text for the credits
        rtbCredits.Text = Strings.AboutCredits;
    }

    /// <summary>
    ///     Translates the text of the form and its controls based on the language settings.
    /// </summary>
    private void Translate()
    {
        Text = string.Concat(Strings.About, " - ", string.Format(Strings.AboutVersion, GlobalData.AppVersion));
        lblAppName.Text = Strings.MainTitle;
        lblAboutInfo.Text = Strings.AboutInfo;
        lnkGithubRepo.Text = Strings.AboutGithubRepo;
        lnkLicense.Text = Strings.AboutLicense;
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