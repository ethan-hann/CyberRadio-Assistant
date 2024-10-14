// DirectoryWatcher.cs : RadioExt-Helper
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

using System.Collections.Concurrent;
using AetherUtils.Core.Files;

namespace RadioExt_Helper.utility;

/// <summary>
/// Encapsulates a <see cref="FileSystemWatcher"/> to watch a directory for changes.
/// Subscribe to events to be notified when files are changed, created, deleted, or renamed.
/// </summary>
public class DirectoryWatcher
{
    private readonly ConcurrentDictionary<string, DateTime> _processedDirectories;
    private readonly TimeSpan _timeWindow;

    private readonly FileSystemWatcher _watcher;

    /// <summary>
    /// Create a new instance of <see cref="DirectoryWatcher"/> to watch the specified directory for changes.
    /// </summary>
    /// <param name="watchPath">The path to watch for changes.</param>
    /// <param name="timeWindow"> The time window to wait before processing another event in the same directory.</param>
    /// <exception cref="ArgumentNullException">Occurs when the <paramref name="watchPath"/> is <c>null</c> or empty.</exception>
    /// <exception cref="ArgumentException">Occurs if the directory specified by <paramref name="watchPath"/> does not exist.</exception>
    public DirectoryWatcher(string watchPath, TimeSpan timeWindow)
    {
        if (string.IsNullOrEmpty(watchPath))
            throw new ArgumentNullException(nameof(watchPath));

        if (!FileHelper.DoesFolderExist(watchPath))
            throw new ArgumentException(string.Format(Strings.DirectoryWatcher_DirectoryNotExists, watchPath),
                nameof(watchPath));

        _watcher = new FileSystemWatcher(watchPath)
        {
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName
        };

        _watcher.Changed += OnChanged;
        _watcher.Created += OnCreated;
        _watcher.Deleted += OnDeleted;
        _watcher.Renamed += OnRenamed;
        _watcher.Error += OnError;

        _processedDirectories = new ConcurrentDictionary<string, DateTime>();
        _timeWindow = timeWindow;
    }

    /// <summary>
    /// Occurs when a file in the watched directory is changed.
    /// </summary>
    public event EventHandler<string>? FileChanged;

    /// <summary>
    /// Occurs when a file is created in the watched directory.
    /// </summary>
    public event EventHandler<string>? FileCreated;

    /// <summary>
    /// Occurs when a file is deleted from the watched directory.
    /// </summary>
    public event EventHandler<string>? FileDeleted;

    /// <summary>
    /// Occurs when a file is renamed in the watched directory. Event data includes the old and new file paths.
    /// </summary>
    public event EventHandler<(string OldPath, string NewPath)>? FileRenamed;

    /// <summary>
    /// Occurs when the file system watcher encounters an error.
    /// </summary>
    public event EventHandler<Exception>? Error;

    /// <summary>
    /// Start watching the directory for changes.
    /// </summary>
    public void Start()
    {
        _watcher.EnableRaisingEvents = true;
    }

    /// <summary>
    /// Stop watching the directory for changes.
    /// </summary>
    public void Stop()
    {
        _watcher.EnableRaisingEvents = false;
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        HandleEvent(FileChanged, e.FullPath);
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        HandleEvent(FileCreated, e.FullPath);
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        HandleEvent(FileDeleted, e.FullPath);
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        FileRenamed?.Invoke(this, (e.OldFullPath, e.FullPath));
    }

    private void OnError(object sender, ErrorEventArgs e)
    {
        Error?.Invoke(this, e.GetException());
    }

    private void HandleEvent(EventHandler<string>? eventHandler, string path)
    {
        var directory = Path.GetDirectoryName(path);
        if (directory == null)
            return;

        if (_processedDirectories.TryGetValue(directory, out var lastProcessed))
            if (DateTime.Now - lastProcessed < _timeWindow)
                return;

        _processedDirectories[directory] = DateTime.Now;
        eventHandler?.Invoke(this, path);
    }
}