// LocalizedTypeDescriptor.cs : RadioExt-Helper
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
/// A type descriptor that localizes the properties of a component.
/// </summary>
/// <param name="parent"></param>
public class LocalizedTypeDescriptor(ICustomTypeDescriptor? parent) : CustomTypeDescriptor(parent)
{
    /// <inheritdoc />
    public override PropertyDescriptorCollection GetProperties()
    {
        var baseProps = base.GetProperties();
        var props = baseProps.Cast<PropertyDescriptor>()
            .Select(p => new LocalizedPropertyDescriptor(p))
            .ToArray<PropertyDescriptor>();
        return new PropertyDescriptorCollection(props);
    }

    /// <inheritdoc />
    public override PropertyDescriptorCollection GetProperties(Attribute[]? attributes)
    {
        var baseProps = base.GetProperties(attributes);
        var props = baseProps.Cast<PropertyDescriptor>()
            .Select(p => new LocalizedPropertyDescriptor(p))
            .ToArray<PropertyDescriptor>();
        return new PropertyDescriptorCollection(props);
    }
}