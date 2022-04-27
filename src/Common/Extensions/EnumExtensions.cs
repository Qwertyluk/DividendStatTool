using System.ComponentModel;
using System.Reflection;

namespace Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum e)
        {
            Type enumType = e.GetType();
            string? enumName = Enum.GetName(enumType, e);
            if (enumName != null)
            {
                FieldInfo? fieldInfo = enumType.GetField(enumName);
                if (fieldInfo != null)
                {
                    if (Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) is DescriptionAttribute attr)
                    {
                        return attr.Description;
                    }
                }
            }

            return string.Empty;
        }
    }
}
