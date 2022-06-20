using System.ComponentModel;

namespace Panda;

public static class EnumExtensions
{
    /// <summary>
    ///     获取枚举的 DescriptionAttribute
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string Description(this Enum value)
    {
        var enumType = value.GetType();
        // 获取枚举常数名称。
        var name = Enum.GetName(enumType, value);
        if (name == null) return "";
        // 获取枚举字段。
        var fieldInfo = enumType.GetField(name);
        if (fieldInfo == null) return "";
        // 获取描述的属性。
        if (Attribute.GetCustomAttribute(fieldInfo,
                typeof(DescriptionAttribute), false) is DescriptionAttribute attr)
            return attr.Description;

        return "";
    }
}