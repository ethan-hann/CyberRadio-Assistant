using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility.localization
{
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

}
