// ControlExtensions.cs : RadioExt-Helper
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

using AetherUtils.Core.Logging;

namespace RadioExt_Helper.utility;

/// <summary>
/// Contains extension methods for the <see cref="Control"/> class.
/// </summary>
public static class ControlExtensions
{
    /// <summary>
    ///     Safely invokes the specified action on the control's thread.
    /// </summary>
    /// <param name="control">The control on which to invoke the action.</param>
    /// <param name="action">The action to invoke.</param>
    public static void SafeInvoke(this Control control, Action action)
    {
        if (control.InvokeRequired)
            control.Invoke(action);
        else
            action();
    }

    /// <summary>
    ///     Safely begins invoking the specified action on the control's thread.
    /// </summary>
    /// <param name="control">The control on which to begin invoking the action.</param>
    /// <param name="action">The action to begin invoking.</param>
    public static void SafeBeginInvoke(this Control control, Action action)
    {
        if (control.InvokeRequired)
            control.BeginInvoke(action);
        else
            action();
    }

    /// <summary>
    ///     Safely ends invoking the specified asynchronous result on the control's thread.
    /// </summary>
    /// <param name="control">The control on which to end invoking the asynchronous result.</param>
    /// <param name="result">The asynchronous result to end invoking.</param>
    public static void SafeEndInvoke(this Control control, IAsyncResult result)
    {
        if (control.InvokeRequired) control.EndInvoke(result);
    }

    /// <summary>
    ///  Safely sets a metadata key-value pair on the control.
    /// </summary>
    /// <param name="control">The control on which to set the metadata.</param>
    /// <param name="key">The key of the metadata.</param>
    /// <param name="value">The value of the metadata.</param>
    public static void SetMetadata(this Control control, string key, string value)
    {
        try
        {
            if (control.Tag is not Dictionary<string, string> metadata)
            {
                metadata = new Dictionary<string, string>();
                control.Tag = metadata;
            }

            metadata[key] = value;
        }
        catch (Exception ex)
        {
            AuLogger.GetCurrentLogger("ControlExtensions.SetMetadata").Error(ex);
        }
    }

    /// <summary>
    /// Retrieves the metadata key-value pair from the control's Tag property.
    /// </summary>
    /// <param name="control">The control on which to retrieve the metadata for.</param>
    /// <returns>A dictionary containing the key-value metadata.</returns>
    public static Dictionary<string, string> GetMetadata(this Control control)
    {
        return control.Tag as Dictionary<string, string> ?? new Dictionary<string, string>();
    }
}