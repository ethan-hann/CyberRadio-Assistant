// TrackableObject.cs : RadioExt-Helper
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

using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace RadioExt_Helper.models;

/// <summary>
///     Represents a trackable object that implements the <see cref="INotifyPropertyChanged" /> interface.
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
public sealed class TrackableObject<T> : INotifyPropertyChanged where T : class, INotifyPropertyChanged, new()
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
        if (_originalValuesInitialized) return; // Prevent multiple initializations

        foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!prop.CanRead) continue;

            var value = prop.GetValue(_trackedObject);
            _originalValues[prop.Name] = DeepClone(value);
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
        switch (obj)
        {
            case null:
                return null;
            case ICloneable cloneable:
                return cloneable.Clone();
            case IEnumerable enumerable when obj.GetType().IsGenericType:
                {
                    var listType = typeof(List<>).MakeGenericType(obj.GetType().GetGenericArguments().First());
                    var list = Activator.CreateInstance(listType) as IList;
                    foreach (var item in enumerable) list?.Add(DeepClone(item));
                    return list;
                }
            default:
                return obj;
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
            _originalValues[prop.Name] = DeepClone(value);
        }

        IsPendingSave = false;
    }

    /// <summary>
    ///     Reverts the changes made to the tracked object and marks it as not pending save.
    /// </summary>
    public void DeclineChanges()
    {
        foreach (var kvp in _originalValues)
        {
            var prop = typeof(T).GetProperty(kvp.Key);
            if (prop == null || !prop.CanWrite) continue;

            prop.SetValue(TrackedObject, DeepClone(kvp.Value));
        }

        IsPendingSave = false;
        CheckPendingSaveStatus();
    }

    private void OnTrackedObjectPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
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
            if (!DeepEquals(currentValue, kvp.Value))
            {
                isPendingSave = true;
                break;
            }
        }

        IsPendingSave = isPendingSave;
        return isPendingSave;
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