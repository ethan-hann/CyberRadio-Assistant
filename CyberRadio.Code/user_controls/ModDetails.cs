// ModDetails.cs : RadioExt-Helper
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

using Pathoschild.FluentNexus.Models;
using RadioExt_Helper.nexus_api;

namespace RadioExt_Helper.user_controls;

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