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

using System.Runtime.InteropServices;
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;

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
    private const int MaxNotificationTextLength = 220;
    private const string AppDisplayName = "Cyber Radio Assistant";
    private const string AppUserModelId = "CyberRadioAssistant.Desktop";

    private static readonly Lock Sync = new();
    private static bool _notificationsAvailable = true;
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

        EnsureInitialized();
        if (!_notificationsAvailable) return;

        var title = GetStyledTitle(type);
        var body = Truncate(message, MaxNotificationTextLength);
        var appNotification = new AppNotificationBuilder()
            .AddText(AppDisplayName)
            .AddText(title)
            .AddText(body)
            .BuildNotification();

        AppNotificationManager.Default.Show(appNotification);
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

    private static void EnsureInitialized()
    {
        if (_initialized) return;

        lock (Sync)
        {
            if (_initialized) return;

            _ = SetCurrentProcessExplicitAppUserModelID(AppUserModelId);

            try
            {
                _ = AppNotificationManager.Default;
            }
            catch (COMException)
            {
                _notificationsAvailable = false;
            }

            _initialized = true;
        }
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

    [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
    private static extern int SetCurrentProcessExplicitAppUserModelID(string appId);
}
