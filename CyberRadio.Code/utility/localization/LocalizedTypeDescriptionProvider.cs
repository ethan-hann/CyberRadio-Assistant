using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility.localization
{
    /// <summary>
    /// A type description provider that localizes the properties of a component.
    /// </summary>
    /// <param name="type"></param>
    public class LocalizedTypeDescriptionProvider(Type type) : TypeDescriptionProvider(TypeDescriptor.GetProvider(type))
    {
        private readonly TypeDescriptionProvider _baseProvider = TypeDescriptor.GetProvider(type);

        /// <inheritdoc />
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object? instance)
        {
            var baseDescriptor = _baseProvider.GetTypeDescriptor(objectType, instance);
            return new LocalizedTypeDescriptor(baseDescriptor);
        }
    }

}
