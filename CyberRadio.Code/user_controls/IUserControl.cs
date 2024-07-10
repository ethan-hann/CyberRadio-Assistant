// IUserControl.cs : RadioExt-Helper
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

namespace RadioExt_Helper.user_controls;

/// <summary>
/// Interface for a custom user control.
/// Implementors should define the station they are associated with and how to translate the strings of the control.
/// </summary>
public interface IUserControl
{
    /// <summary>
    ///     The tracked station associated with this control.
    /// </summary>
    public TrackableObject<Station> Station { get; }

    /// <summary>
    ///     Specify how to translate the strings of this control.
    /// </summary>
    public void Translate();
}