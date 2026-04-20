// ToastNotification.cs : RadioExt-Helper
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

namespace RadioExt_Helper.utility;

/// <summary>
/// Enum representing the type of toast notification to display, which determines the icon and styling used in the notification.
/// </summary>
public enum ToastType
{
    /// <summary>
    /// Represents informational content or a message, typically used to convey non-critical details to the user.
    /// </summary>
    Info,
    /// <summary>
    /// Indicates that the operation completed successfully.
    /// </summary>
    Success,
    /// <summary>
    /// Represents a warning message or state.
    /// </summary>
    Warning,
    /// <summary>
    /// Represents an error condition or error information.
    /// </summary>
    Error
}

/// <summary>
/// Provides static methods for displaying balloon-style toast notifications using the Windows notification area (system
/// tray).
/// </summary>
/// <remarks>This class displays notifications as balloon tips associated with the application's NotifyIcon.
/// Notifications support different styles (info, warning, error, success) and are styled according to the specified
/// type. Only balloon-style notifications are used, and the notification title appears as 'RadioExt-Helper' due to
/// system constraints. The class is thread-safe and ensures that notifications are shown on the correct UI thread. Use
/// this class to provide user feedback or alerts in a Windows Forms application without relying on deprecated or
/// incompatible notification APIs.</remarks>
public static class ToastNotification
{
    private const int DefaultDurationMs = 3500;
    private const int MaxNotificationTextLength = 255;
    private const string AppDisplayName = "Cyber Radio Assistant";

    private static readonly Lock Sync = new();
    private static NotifyIcon? _notifyIcon;
    private static bool _initialized;

    /// <summary>
    /// Displays a balloon-style toast notification with the specified message and style for a limited duration.
    /// </summary>
    /// <remarks>This method uses a NotifyIcon balloon tip to display the notification. The notification's
    /// appearance reflects the specified type, and the balloon tip title will show as 'RadioExt-Helper'. If called from
    /// a non-UI thread, the notification is marshaled to the UI thread. If the message exceeds the maximum allowed
    /// length, it is truncated.</remarks>
    /// <param name="owner">The parent control that owns the notification window, or null to use the default application window.</param>
    /// <param name="message">The message text to display in the notification. Cannot be null, empty, or whitespace.</param>
    /// <param name="type">The style of the notification, indicating its purpose or severity. The default is ToastType.Info.</param>
    /// <param name="durationMs">The duration, in milliseconds, for which the notification is displayed. Must be at least 1,000 milliseconds.</param>
    public static void Show(Control? owner, string message, ToastType type = ToastType.Info,
        int durationMs = DefaultDurationMs)
    {
        if (string.IsNullOrWhiteSpace(message)) return;

        var resolvedOwner = ResolveOwner(owner);
        if (resolvedOwner is { IsDisposed: false, InvokeRequired: true })
        {
            resolvedOwner.BeginInvoke(() => Show(resolvedOwner, message, type, durationMs));
            return;
        }

        var title = GetStyledTitle(type);
        var body = Truncate(message, MaxNotificationTextLength);

        EnsureInitialized();

        lock (Sync)
        {
            if (_notifyIcon is null) return;

            _notifyIcon.BalloonTipIcon = GetBalloonIcon(type);
            _notifyIcon.BalloonTipTitle = title;
            _notifyIcon.BalloonTipText = body;
            _notifyIcon.ShowBalloonTip(Math.Max(1000, durationMs));
        }
    }

    /// <summary>
    /// Displays a toast notification with the specified message and style for a given duration.
    /// </summary>
    /// <remarks>This method displays the toast notification on the application's main form. Use the 'type'
    /// parameter to distinguish between informational, warning, error, or success notifications. The notification will
    /// automatically disappear after the specified duration.</remarks>
    /// <param name="message">The text to display in the toast notification. Cannot be null or empty.</param>
    /// <param name="type">The style of the toast notification, indicating its purpose or severity. The default is ToastType.Info.</param>
    /// <param name="durationMs">The duration, in milliseconds, for which the toast notification is displayed. The default is DefaultDurationMs.</param>
    public static void Show(string message, ToastType type = ToastType.Info, int durationMs = DefaultDurationMs)
    {
        Show(ApplicationContext.MainFormInstance, message, type, durationMs);
    }

    private static Control? ResolveOwner(Control? owner)
    {
        if (owner is { IsDisposed: false }) return owner;
        if (ApplicationContext.MainFormInstance is { IsDisposed: false } mainForm) return mainForm;
        return Form.ActiveForm;
    }

    private static string GetStyledTitle(ToastType type)
    {
        return type switch
        {
            ToastType.Success => "✅ Success",
            ToastType.Warning => "⚠ Warning",
            ToastType.Error => "❌ Error",
            _ => "ℹ Notification"
        };
    }

    private static string Truncate(string text, int maxLength) => text.Length <= maxLength ? text : $"{text[..(maxLength - 3)]}...";

    private static void EnsureInitialized()
    {
        if (_initialized) return;

        lock (Sync)
        {
            if (_initialized) return;

            var icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath) ?? SystemIcons.Application;

            _notifyIcon = new NotifyIcon
            {
                Icon = icon,
                Text = AppDisplayName,
                Visible = true
            };

            Application.ApplicationExit += (_, _) =>
            {
                lock (Sync)
                {
                    if (_notifyIcon is null) return;
                    _notifyIcon.Visible = false;
                    _notifyIcon.Dispose();
                    _notifyIcon = null;
                }
            };

            _initialized = true;
        }
    }

    private static ToolTipIcon GetBalloonIcon(ToastType type)
    {
        return type switch
        {
            ToastType.Error => ToolTipIcon.Error,
            ToastType.Warning => ToolTipIcon.Warning,
            _ => ToolTipIcon.Info
        };
    }
}
