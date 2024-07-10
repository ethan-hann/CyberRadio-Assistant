using System.Reflection;
using AetherUtils.Core.Extensions;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.forms;

public partial class AboutBox : Form
{
    private readonly Version _currentVersion;

    public AboutBox()
    {
        InitializeComponent();

        var v = Assembly.GetExecutingAssembly().GetName().Version;
        _currentVersion = v != null
            ? new Version(v.Major, v.Minor, v.Build)
            : new Version(0, 0, 0); //This should never happen, but just in case!
    }

    private void AboutBox_Load(object sender, EventArgs e)
    {
        Translate();
    }

    private void Translate()
    {
        lblAppName.Text = GlobalData.Strings.GetString("MainTitle") ?? "Cyber Radio Assistant";
        lblAboutInfo.Text = GlobalData.Strings.GetString("AboutInfo") ??
                            "A tool to help create and manage custom radio stations " +
                            "in Cyberpunk 2077 when using the radioExt Mod.";
        lblSpecialThanks1.Text = GlobalData.Strings.GetString("AboutSpecialThanks1") ?? "Special thanks to";
        lblSpecialThanks2.Text = GlobalData.Strings.GetString("AboutSpecialThanks2") ?? "for the awesome radioExt mod!";
        lblVersion.Text = string.Format(GlobalData.Strings.GetString("AboutVersion") ?? "Version {0}", _currentVersion);
        lnkGithubRepo.Text = GlobalData.Strings.GetString("AboutGithubRepo") ?? "Github Repo";
    }

    private void lnkRadioExtDev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        "https://github.com/justarandomguyintheinternet/CP77_radioExt".OpenUrl();
    }

    private void lnkGithubRepo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        "https://github.com/ethan-hann/CyberRadio-Assistant".OpenUrl();
    }

    private void lnkLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        "https://github.com/ethan-hann/CyberRadio-Assistant/blob/main/LICENSE".OpenUrl();
    }
}