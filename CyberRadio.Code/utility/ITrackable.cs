// ITrackable.cs : RadioExt-Helper
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

namespace RadioExt_Helper.utility;

/// <summary>
/// Interface for trackable objects.
/// </summary>
public interface ITrackable
{
    /// <summary>
    /// Get a value indicating whether the object has pending changes.
    /// </summary>
    bool IsPendingSave { get; }

    /// <summary>
    /// Defines the method to accept changes to the object.
    /// </summary>
    void AcceptChanges();

    /// <summary>
    /// Defines the method to decline changes to the object.
    /// </summary>
    void DeclineChanges();

    /// <summary>
    /// Defines the method to check if the object has pending changes.
    /// </summary>
    /// <returns></returns>
    bool CheckPendingSaveStatus();
}