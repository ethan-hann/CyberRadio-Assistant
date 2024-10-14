// IconManagerEventArgs.cs : RadioExt-Helper
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

namespace RadioExt_Helper.utility.event_args;

/// <summary>
/// Represents the event arguments for the various events in the <see cref="IconManager"/> class.
/// </summary>
public class IconManagerEventArgs(string? status, int progress, bool isError, string? errorMessage = null)
    : EventArgs
{
    /// <summary>
    /// The status message of the event.
    /// </summary>
    public string? Status { get; set; } = status;

    /// <summary>
    /// The current progress percentage of the event.
    /// </summary>
    public int Progress { get; set; } = progress;

    /// <summary>
    /// Determines if the event is an error.
    /// </summary>
    public bool IsError { get; set; } = isError;

    /// <summary>
    /// The error message of the event.
    /// </summary>
    public string? ErrorMessage { get; set; } = errorMessage;
}