// IEditor.cs : RadioExt-Helper
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

using RadioExt_Helper.models;

namespace RadioExt_Helper.utility;

/// <summary>
/// Interface for all editors in the application.
/// </summary>
public interface IEditor
{
    /// <summary>
    /// The unique identifier for this editor.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The type of editor.
    /// </summary>
    public EditorType Type { get; set; }

    /// <summary>
    ///     The tracked station associated with this control, if applicable.
    /// </summary>
    public TrackableObject<AdditionalStation>? Station { get; }

    /// <summary>
    /// The tracked replacement station associated with this control, if applicable.
    /// </summary>
    public TrackableObject<ReplacementStation>? ReplacedStation { get; }

    /// <summary>
    /// Defines the method to translate the control into the current language.
    /// </summary>
    public void Translate();
}