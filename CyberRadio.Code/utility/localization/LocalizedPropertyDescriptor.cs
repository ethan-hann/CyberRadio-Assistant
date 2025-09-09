// LocalizedPropertyDescriptor.cs : RadioExt-Helper
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

using System.ComponentModel;

namespace RadioExt_Helper.utility.localization;

/// <summary>
/// A PropertyDescriptor that localizes the Description attribute of a property.
/// </summary>
/// <param name="basePropertyDescriptor"></param>
public class LocalizedPropertyDescriptor(PropertyDescriptor basePropertyDescriptor)
    : PropertyDescriptor(basePropertyDescriptor)
{
    /// <inheritdoc />
    public override string Description
    {
        get
        {
            // Get the original DescriptionAttribute
            if (basePropertyDescriptor.Attributes[typeof(DescriptionAttribute)] is not DescriptionAttribute
                descAttr) return basePropertyDescriptor.Description;

            // Use resource manager to translate
            var translated = Strings.ResourceManager.GetString(descAttr.Description);
            return translated ?? descAttr.Description;
        }
    }

    /// <inheritdoc />
    public override string DisplayName
    {
        get
        {
            if (basePropertyDescriptor.Attributes[typeof(DisplayNameAttribute)] is not DisplayNameAttribute
                displayAttr) return basePropertyDescriptor.DisplayName;

            var localized = Strings.ResourceManager.GetString(displayAttr.DisplayName);
            return localized ?? displayAttr.DisplayName;
        }
    }

    /// <inheritdoc />
    public override string Category
    {
        get
        {
            if (basePropertyDescriptor.Attributes[typeof(CategoryAttribute)] is not CategoryAttribute categoryAttr)
                return basePropertyDescriptor.Category;

            var localized = Strings.ResourceManager.GetString(categoryAttr.Category);
            return localized ?? categoryAttr.Category;
        }
    }

    /// <inheritdoc />
    public override Type ComponentType => basePropertyDescriptor.ComponentType;

    /// <inheritdoc />
    public override bool IsReadOnly => basePropertyDescriptor.IsReadOnly;

    /// <inheritdoc />
    public override Type PropertyType => basePropertyDescriptor.PropertyType;

    // Forward all other members to the base descriptor

    /// <inheritdoc />
    public override bool CanResetValue(object component)
    {
        return basePropertyDescriptor.CanResetValue(component);
    }

    /// <inheritdoc />
    public override object? GetValue(object? component)
    {
        return basePropertyDescriptor.GetValue(component);
    }

    /// <inheritdoc />
    public override void ResetValue(object component)
    {
        basePropertyDescriptor.ResetValue(component);
    }

    /// <inheritdoc />
    public override void SetValue(object component, object value)
    {
        basePropertyDescriptor.SetValue(component, value);
    }

    /// <inheritdoc />
    public override bool ShouldSerializeValue(object component)
    {
        return basePropertyDescriptor.ShouldSerializeValue(component);
    }
}