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

public enum ToastType
{
    Info,
    Success,
    Warning,
    Error
}

public static class ToastNotification
{
    private const int DefaultDurationMs = 3500;
    private const int MaxNotificationTextLength = 255;
    private const string AppDisplayName = "Cyber Radio Assistant";

    private static readonly Lock Sync = new();
    private static NotifyIcon? _notifyIcon;
    private static bool _initialized;

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

    private static string Truncate(string text, int maxLength)
    {
        if (text.Length <= maxLength) return text;
        return $"{text[..(maxLength - 3)]}...";
    }

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
