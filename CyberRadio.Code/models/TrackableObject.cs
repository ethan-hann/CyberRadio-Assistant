using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.models
{
    /// <summary>
    /// Represents a trackable object that implements the <see cref="INotifyPropertyChanged"/> interface.
    /// This class can track changes made to the object and determine if there are pending changes.
    /// <para>The object must have public properties in order to be tracked.</para>
    /// </summary>
    /// <typeparam name="T">The type of the object being tracked.</typeparam>
    public class TrackableObject<T> : INotifyPropertyChanged where T : class, INotifyPropertyChanged, new()
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly Dictionary<string, object?> _originalValues = [];
        private bool _isPendingSave;
        private T _trackedObject;

        /// <summary>
        /// Gets a value indicating whether the object has pending changes.
        /// </summary>
        public bool IsPendingSave
        {
            get => _isPendingSave;
            private set
            {
                if (_isPendingSave != value)
                {
                    _isPendingSave = value;
                    OnPropertyChanged(nameof(IsPendingSave));
                }
            }
        }

        /// <summary>
        /// Gets the tracked object.
        /// </summary>
        public T TrackedObject
        {
            get => _trackedObject;
            private set
            {
                if (_trackedObject != value)
                {
                    _trackedObject = value;
                    OnPropertyChanged(nameof(TrackedObject));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackableObject{T}"/> class.
        /// </summary>
        /// <param name="obj">The object to be tracked.</param>
        public TrackableObject(T obj)
        {
            _trackedObject = obj;
            InitializeOriginalValues();
            TrackedObject.PropertyChanged += OnTrackedObjectPropertyChanged;
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Initializes the original values dictionary with the current property values of the tracked object.
        /// </summary>
        private void InitializeOriginalValues()
        {
            foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.CanRead)
                {
                    var value = prop.GetValue(_trackedObject);
                    if (value is ICloneable cloneable)
                    {
                        _originalValues[prop.Name] = cloneable.Clone();
                    }
                    else
                    {
                        _originalValues[prop.Name] = value;
                    }
                }
            }
            IsPendingSave = false;
        }

        /// <summary>
        /// Accepts the changes made to the tracked object and marks it as not pending save.
        /// </summary>
        public void AcceptChanges()
        {
            foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.CanRead)
                {
                    var value = prop.GetValue(TrackedObject);
                    if (value is ICloneable cloneable)
                    {
                        _originalValues[prop.Name] = cloneable.Clone();
                    }
                    else
                    {
                        _originalValues[prop.Name] = value;
                    }
                }
            }
            IsPendingSave = false;
        }

        /// <summary>
        /// Reverts the changes made to the tracked object and marks it as not pending save.
        /// </summary>
        public void DeclineChanges()
        {
            foreach (var kvp in _originalValues)
            {
                var prop = typeof(T).GetProperty(kvp.Key);
                if (prop != null && prop.CanWrite)
                {
                    if (kvp.Value is ICloneable cloneable)
                    {
                        prop.SetValue(TrackedObject, cloneable.Clone());
                    }
                    else
                    {
                        prop.SetValue(TrackedObject, kvp.Value);
                    }
                }
            }
            IsPendingSave = false;
        }

        private void OnTrackedObjectPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            CheckPendingSaveStatus();
        }

        /// <summary>
        /// Checks the pending save status of the tracked object.
        /// </summary>
        /// <returns>True if there are pending changes, otherwise false.</returns>
        public bool CheckPendingSaveStatus()
        {
            bool isPendingSave = false;

            foreach (var kvp in _originalValues)
            {
                var currentValue = typeof(T)?.GetProperty(kvp.Key)?.GetValue(TrackedObject);
                if (kvp.Value is SongList originalSongList && currentValue is SongList currentSongList)
                {
                    if (!originalSongList.Equals(currentSongList))
                    {
                        isPendingSave = true;
                        break;
                    }
                }
                else if (!Equals(currentValue, kvp.Value))
                {
                    isPendingSave = true;
                    break;
                }
            }

            IsPendingSave = isPendingSave;
            return isPendingSave;
        }

        /// <summary>
        /// Returns a string that represents the tracked object.
        /// </summary>
        /// <returns>A string that represents the tracked object.</returns>
        public override string ToString()
        {
            return TrackedObject.ToString() ?? string.Empty;
        }
    }
}
