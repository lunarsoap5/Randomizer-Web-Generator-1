namespace TPRandomizer.Util
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    public static class EnumUtils
    {
        /// <summary>
        /// Extracts the [Description] attribute value from any Enum or returns the enum ToString if
        /// the attribute is not defined.
        /// </summary>
        public static string GetDescription<TEnum>(TEnum value) where TEnum : struct, Enum
        {
            string enumString = value.ToString();

            // Use reflection to locate the field matching the enum value string
            FieldInfo field = typeof(TEnum).GetField(enumString);
            if (field == null)
                return enumString;

            DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();

            return attribute != null ? attribute.Description : enumString;
        }
    }
}
