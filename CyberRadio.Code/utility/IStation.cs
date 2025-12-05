// // IStation.cs : RadioExt-Helper
// // Copyright (C) 2025  Ethan Hann
// //
// // This program is free software: you can redistribute it and/or modify
// // it under the terms of the GNU General Public License as published by
// // the Free Software Foundation, either version 3 of the License, or
// // (at your option) any later version.
// //
// // This program is distributed in the hope that it will be useful,
// // but WITHOUT ANY WARRANTY; without even the implied warranty of
// // MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// // GNU General Public License for more details.
// //
// // You should have received a copy of the GNU General Public License
// // along with this program.  If not, see <https://www.gnu.org/licenses/>.

namespace RadioExt_Helper.utility;

/// <summary>
///     Interface representing a radio station with a name and associated tags.
///     Can be implemented by different types of stations (i.e., vanilla replacement or completely new stations).
/// </summary>
public interface IStation
{
    /// <summary>
    ///     A list of tags associated with the station.
    /// </summary>
    List<string> Tags { get; set; }

    /// <summary>
    ///     The type of station (e.g., vanilla replacement or completely new station).
    /// </summary>
    StationType StationType { get; }
}