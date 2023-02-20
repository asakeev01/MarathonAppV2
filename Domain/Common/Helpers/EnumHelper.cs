using Domain.Common.Constants;
using Domain.Common.Exceptions;
using System.ComponentModel;
using System.Reflection;

namespace Domain.Common.Helpers;

public static class ExceptionHelpers
{
    public static void ThrowIfNotEnum<TEnum>()
        where TEnum : struct
    {
        if (!typeof(TEnum).IsEnum)
        {
            throw new InnerException(InnerExTexts.NotEnum.Value, InnerExTexts.NotEnum.Key);
        }
    }

    public static string GetEnumDescription(Enum value)
    {
        // Get the Description attribute value for the enum value
        FieldInfo fi = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes.Length > 0)
            return attributes[0].Description;
        else
            return value.ToString();
    }

}