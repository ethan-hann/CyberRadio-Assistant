// IconImportEventArgs.cs : RadioExt-Helper
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

using RadioExt_Helper.models;

namespace RadioExt_Helper.utility.event_args;

/// <summary>
/// Event data for when an icon is imported.
/// </summary>
/// <param name="icon">The <see cref="CustomIcon"/> definition for the imported icon.</param>
public class IconImportEventArgs(CustomIcon icon)
{
    /// <summary>
    /// The custom icon that was imported.
    /// </summary>
    public CustomIcon Icon { get; private set; } = icon;
}