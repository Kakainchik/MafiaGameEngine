using System;
using System.Windows.Markup;

namespace WPFApplication.Extensions
{
    public class EnumBindingExtension : MarkupExtension
    {
        public Type EnumType { get; private set; }

        public EnumBindingExtension(Type enumType)
        {
            if(enumType is null || !enumType.IsEnum)
                throw new NullReferenceException(
                    $"{nameof(enumType)} must be of type Enum and must not be null.");

            EnumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(EnumType);
        }
    }
}