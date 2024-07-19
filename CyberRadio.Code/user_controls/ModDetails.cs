using Pathoschild.FluentNexus.Models;
using RadioExt_Helper.nexus_api;

namespace RadioExt_Helper.user_controls
{
    //Eventual goal is to have this user control display the details of a mod
    public sealed partial class ModDetails : UserControl
    {
        private Mod? _mod;
        private bool _modDetailsInitialized;

        public ModDetails()
        {
            InitializeComponent();
        }

        public ModDetails(Mod mod)
        {
            _mod = mod;

            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        private void ModDetails_Load(object sender, EventArgs e)
        {
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            if (_mod == null || _modDetailsInitialized)
                return;

            pbModImage.Image = await NexusApi.GetModImage(_mod);
            lblName.Text = _mod.Name;
            lblAuthor.Text = _mod.Author;
            rtbSummary.Text = _mod.Summary;
            _modDetailsInitialized = true;
        }

        public void SetMod(Mod mod)
        {
            _mod = mod;
            _modDetailsInitialized = false;
            _ = InitializeAsync();
        }
    }
}
