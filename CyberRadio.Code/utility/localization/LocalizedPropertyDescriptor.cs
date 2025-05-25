using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility.localization
{
    /// <summary>
    /// A PropertyDescriptor that localizes the Description attribute of a property.
    /// </summary>
    /// <param name="basePropertyDescriptor"></param>
    public class LocalizedPropertyDescriptor(PropertyDescriptor basePropertyDescriptor) : PropertyDescriptor(basePropertyDescriptor)
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

        // Forward all other members to the base descriptor

        /// <inheritdoc />
        public override bool CanResetValue(object component) => basePropertyDescriptor.CanResetValue(component);

        /// <inheritdoc />
        public override Type ComponentType => basePropertyDescriptor.ComponentType;

        /// <inheritdoc />
        public override object? GetValue(object? component) => basePropertyDescriptor.GetValue(component);

        /// <inheritdoc />
        public override bool IsReadOnly => basePropertyDescriptor.IsReadOnly;

        /// <inheritdoc />
        public override Type PropertyType => basePropertyDescriptor.PropertyType;

        /// <inheritdoc />
        public override void ResetValue(object component) => basePropertyDescriptor.ResetValue(component);

        /// <inheritdoc />
        public override void SetValue(object component, object value) => basePropertyDescriptor.SetValue(component, value);

        /// <inheritdoc />
        public override bool ShouldSerializeValue(object component) => basePropertyDescriptor.ShouldSerializeValue(component);
    }

}
