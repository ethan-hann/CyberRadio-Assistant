using AetherUtils.Core.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility
{
    /// <summary>
    /// Encapsulates a <see cref="FileSystemWatcher"/> to watch a directory for changes.
    /// Subscribe to events to be notified when files are changed, created, deleted, or renamed.
    /// </summary>
    public class DirectoryWatcher
    {
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

        private readonly FileSystemWatcher _watcher;

        /// <summary>
        /// Create a new instance of <see cref="DirectoryWatcher"/> to watch the specified directory for changes.
        /// </summary>
        /// <param name="watchPath">The path to watch for changes.</param>
        /// <exception cref="ArgumentNullException">Occurs when the <paramref name="watchPath"/> is <c>null</c> or empty.</exception>
        /// <exception cref="ArgumentException">Occurs if the directory specified by <paramref name="watchPath"/> does not exist.</exception>
        public DirectoryWatcher(string watchPath)
        {
            if (string.IsNullOrEmpty(watchPath))
                throw new ArgumentNullException(nameof(watchPath));

            if (!FileHelper.DoesFolderExist(watchPath))
                throw new ArgumentException($"Directory '{watchPath}' does not exist", nameof(watchPath));

            _watcher = new(watchPath)
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName
            };

            _watcher.Changed += OnChanged;
            _watcher.Created += OnCreated;
            _watcher.Deleted += OnDeleted;
            _watcher.Renamed += OnRenamed;
            _watcher.Error += OnError;
        }

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

        private void OnChanged(object sender, FileSystemEventArgs e) => FileChanged?.Invoke(this, e.FullPath);

        private void OnCreated(object sender, FileSystemEventArgs e) => FileCreated?.Invoke(this, e.FullPath);

        private void OnDeleted(object sender, FileSystemEventArgs e) => FileDeleted?.Invoke(this, e.FullPath);

        private void OnRenamed(object sender, RenamedEventArgs e) => FileRenamed?.Invoke(this, (e.OldFullPath, e.FullPath));

        private void OnError(object sender, ErrorEventArgs e) => Error?.Invoke(this, e.GetException());
    }
}
