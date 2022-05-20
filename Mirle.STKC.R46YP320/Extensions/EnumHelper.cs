using System;

namespace Mirle.STKC.R46YP320.Extensions
{
    public static class EnumHelper
    {
        public static T ToEnum<T>(this string enumString)
        {
            return (T)Enum.Parse(typeof(T), enumString);
        }

        public static T ToEnum<T>(this int enumValue)
        {
            return (T)Enum.ToObject(typeof(T), enumValue);
        }
    }
}