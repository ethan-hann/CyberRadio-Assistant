// NoStationSelectedCtl.cs : RadioExt-Helper
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

namespace RadioExt_Helper.user_controls;

/// <summary>
///     Control displayed when no station is selected in either the main station listbox or the vanilla station selector.
/// </summary>
public sealed partial class NoStationSelectedCtl : UserControl, IUserControl
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="NoStationSelectedCtl" /> class.
    /// </summary>
    public NoStationSelectedCtl()
    {
        InitializeComponent();
        Dock = DockStyle.Fill;
    }

    /// <inheritdoc />
    public void Translate()
    {
        lblNoSelection.Text = Strings.VanillaStationSelector_NoStationSelected;
    }
}