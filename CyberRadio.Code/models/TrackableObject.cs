// TrackableObject.cs : RadioExt-Helper
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

using System.Collections;
using System.ComponentModel;
using System.Reflection;
using AetherUtils.Core.Logging;
using RadioExt_Helper.utility;

namespace RadioExt_Helper.models;

/// <summary>
///     Represents a trackable object that implements the <see cref="ITrackable" /> interface.
///     This class can track changes made to the object and determines if there are pending changes to its properties.
///     <para>The object must have public properties in order to be tracked.</para>
///     <para>
///         If the tracked object contains non-primitive objects that also need to be tracked independently of the parent,
///         the tracked object, <see cref="TrackedObject" /> and all children need to implement
///         <see cref="INotifyPropertyChanged" /> as well as
///         <see cref="ICloneable" /> and <see cref="IEquatable{T}" />.
///     </para>
/// </summary>
/// <typeparam name="T">The type of the object being tracked.</typeparam>
public sealed class TrackableObject<T> : INotifyPropertyChanged, ITrackable
    where T : class, INotifyPropertyChanged, new()
{
    private readonly Dictionary<string, object?> _originalValues = [];
    private bool _isPendingSave;
    private bool _originalValuesInitialized;
    private T _trackedObject;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TrackableObject{T}" /> class.
    /// </summary>
    /// <param name="obj">The object to be tracked.</param>
    public TrackableObject(T obj)
    {
        _trackedObject = obj;
        InitializeOriginalValues();
        TrackedObject.PropertyChanged += OnTrackedObjectPropertyChanged;
    }

    /// <summary>
    ///     Get a unique identifier for the tracked object.
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();

    /// <summary>
    ///     Gets the tracked object.
    /// </summary>
    public T TrackedObject
    {
        get => _trackedObject;
        private set
        {
            if (_trackedObject == value) return;

            _trackedObject = value;
            OnPropertyChanged(nameof(TrackedObject));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    ///     Gets a value indicating whether the object has pending changes.
    /// </summary>
    public bool IsPendingSave
    {
        get => _isPendingSave;
        private set
        {
            if (_isPendingSave == value) return;

            _isPendingSave = value;
            OnPropertyChanged(nameof(IsPendingSave));
        }
    }

    /// <summary>
    ///     Accepts the changes made to the tracked object and marks it as not pending save.
    ///     Accepting the changes will update the original values with the changed ones.
    /// </summary>
    public void AcceptChanges()
    {
        foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!prop.CanRead) continue;

            var value = prop.GetValue(TrackedObject);

            // Handle only TrackableObject lists of type ITrackable
            if (value is IEnumerable enumerable)
            {
                var items = enumerable.Cast<object>().ToList();
                var isTrackable = items.Any(item => item is ITrackable);
                if (isTrackable)
                {
                    foreach (var item in items)
                        if (item is ITrackable trackableItem)
                            trackableItem.AcceptChanges(); // Accept changes for each TrackableObject
                }
                else
                {
                    // Deep clone the enumerable object, including all TrackableObjects inside it
                    _originalValues[prop.Name] = DeepClone(value);
                }
            }
            else
            {
                // Handle non-collection properties
                _originalValues[prop.Name] = DeepClone(value);
            }
        }

        IsPendingSave = false;
    }

    /// <summary>
    ///     Reverts the changes made to the tracked object and marks it as not pending save.
    /// </summary>
    public void DeclineChanges()
    {
        foreach (var (key, originalValue) in _originalValues)
        {
            var prop = typeof(T).GetProperty(key);
            if (prop == null || !prop.CanWrite) continue;

            if (originalValue is IEnumerable enumerable)
            {
                var items = enumerable.Cast<object>().ToList();
                var isTrackable = items.Any(item => item is ITrackable);
                if (isTrackable)
                    foreach (var item in items)
                        if (item is ITrackable trackableItem)
                            trackableItem.DeclineChanges(); // Revert changes for each TrackableObject

                // Restore the deep-cloned enumerable object (the original list of trackable objects)
                prop.SetValue(TrackedObject, DeepClone(originalValue));
            }
            else
            {
                prop.SetValue(TrackedObject, DeepClone(originalValue));
            }
        }

        IsPendingSave = false;
        CheckPendingSaveStatus();
    }

    /// <summary>
    ///     Checks the pending save status of the tracked object.
    /// </summary>
    /// <returns>True if there are pending changes, otherwise false.</returns>
    public bool CheckPendingSaveStatus()
    {
        var isPendingSave = false;

        foreach (var kvp in _originalValues)
        {
            var currentValue = typeof(T).GetProperty(kvp.Key)?.GetValue(TrackedObject);

            // If the property is a collection, check if any items are trackable and have pending changes
            if (currentValue is IEnumerable enumerable)
            {
                var items = enumerable.Cast<object>().ToList();
                var isTrackable = items.Any(item => item is ITrackable);
                if (isTrackable)
                {
                    foreach (var item in items)
                    {
                        if (item is not ITrackable trackableItem) continue;

                        //Recursive call to check pending save status on each TrackableObject in the list
                        if (!trackableItem.CheckPendingSaveStatus()) continue;

                        isPendingSave = true;
                        break;
                    }
                }
                else
                {
                    // If not a trackable object, compare the property value to the original value
                    if (DeepEquals(currentValue, kvp.Value)) continue;

                    isPendingSave = true;
                    break;
                }
            }
            else
            {
                // If not an enumerable, compare the property value to the original value
                if (DeepEquals(currentValue, kvp.Value)) continue;

                isPendingSave = true;
                break;
            }
        }

        IsPendingSave = isPendingSave;
        return isPendingSave;
    }

    /// <summary>
    ///     Raises the <see cref="PropertyChanged" /> event.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    ///     Initializes the original values dictionary with the current property values of the tracked object.
    /// </summary>
    private void InitializeOriginalValues()
    {
        if (_originalValuesInitialized) return; // Prevent multiple initializations of original values

        foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!prop.CanRead) continue;

            try
            {
                var value = prop.GetValue(_trackedObject);

                // Handle collections of trackable objects (similar to DeepClone and DeepEquals)
                if (value is IEnumerable enumerable)
                {
                    var items = enumerable.Cast<object>().ToList();
                    var isTrackable = items.Any(item => item is ITrackable);
                    if (isTrackable)
                        // Ensure each trackable object in the collection is initialized
                        foreach (var item in items)
                            if (item is ITrackable trackableItem)
                                trackableItem.AcceptChanges(); // Initialize original values for trackable items
                }

                // Deep clone the current value and store it as the original value
                _originalValues[prop.Name] = DeepClone(value);
            }
            catch (Exception ex)
            {
                AuLogger.GetCurrentLogger<TrackableObject<T>>()
                    .Error(ex, $"Error initializing property {prop.Name}: {ex.Message}");
            }
        }

        IsPendingSave = false;
        _originalValuesInitialized = true;
    }

    /// <summary>
    /// Deep clones an object, handling collections and other types appropriately.
    /// </summary>
    /// <param name="obj">The object to clone.</param>
    /// <returns>A deep clone of the object.</returns>
    private static object? DeepClone(object? obj)
    {
        if (obj == null) return null;

        switch (obj)
        {
            case ICloneable cloneable:
                return cloneable.Clone();

            case IEnumerable enumerable when obj.GetType().IsGenericType:
            {
                var listType = typeof(List<>).MakeGenericType(obj.GetType().GetGenericArguments().First());
                var list = Activator.CreateInstance(listType) as IList;
                foreach (var item in enumerable)
                    // Deep clone each item, ensuring TrackableObjects are cloned correctly
                    list?.Add(DeepClone(item));
                return list;
            }

            default:
                return obj; // Return the object as-is if it's not cloneable or enumerable
        }
    }

    /// <summary>
    /// Deeply compares two objects, handling collections appropriately.
    /// </summary>
    /// <param name="obj1">First object.</param>
    /// <param name="obj2">Second object.</param>
    /// <returns>True if objects are equal, false otherwise.</returns>
    private static bool DeepEquals(object? obj1, object? obj2)
    {
        if (ReferenceEquals(obj1, obj2)) return true;
        if (obj1 == null || obj2 == null) return false;

        if (obj1 is IEnumerable enumerable1 && obj2 is IEnumerable enumerable2)
            return enumerable1.Cast<object>().SequenceEqual(enumerable2.Cast<object>());

        return obj1.Equals(obj2);
    }

    private void OnTrackedObjectPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        CheckPendingSaveStatus();
    }

    /// <summary>
    ///     Returns a string that represents the tracked object.
    /// </summary>
    /// <returns>A string that represents the tracked object.</returns>
    public override string ToString()
    {
        return TrackedObject.ToString() ?? string.Empty;
    }
}