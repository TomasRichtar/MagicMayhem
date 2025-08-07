using System;

namespace TastyCore.Extensions
{
    public static class EnumExtensions
    {
        private static Random _random = new Random();
	
        public static TEnum RandomOf<TEnum>()
        {
            if (!typeof(TEnum).IsEnum)
                throw new InvalidOperationException("Must use Enum type");

            Array enumValues = Enum.GetValues(typeof(TEnum));
            return (TEnum)enumValues.GetValue(_random.Next(enumValues.Length));
        }
	
        public static TEnum Next<TEnum>(this TEnum src) where TEnum : Enum
        {
            TEnum[] array = (TEnum[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf(array, src) + 1;
            return (array.Length == j) ? array[0] : array[j];
        }

        public static TEnum Prev<TEnum>(this TEnum src) where TEnum : Enum
        {
            TEnum[] array = (TEnum[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf(array, src) - 1;
            return (-1 == j) ? array[array.Length - 1] : array[j];
        }
	
        // Extension for string
        public static TEnum ToEnumOrDefault<TEnum>(this string strEnumValue, TEnum defaultValue)
        {
            if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
                return defaultValue;

            return (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
        }
    }
}