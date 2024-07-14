using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AetherUtils.Core.Extensions;
using CodeKicker.BBCode.Core;
using Pathoschild.FluentNexus.Models;
using RadioExt_Helper.nexus_api;

namespace RadioExt_Helper.user_controls
{
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
