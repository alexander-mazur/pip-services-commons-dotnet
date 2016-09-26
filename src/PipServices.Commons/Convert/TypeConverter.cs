using System;
using System.Reflection;

namespace PipServices.Commons.Convert
{
    /// <summary>
    /// Generic type converter.
    /// </summary>
    public static class TypeConverter
    {
        public static T? ToNullableType<T>(object value) where T : struct
        {
            if (value == null) return null;
            if (value is T) return (T)value;

            var typeInfo = typeof(T).GetTypeInfo();

            if (typeInfo.IsEnum)
                value = EnumConverter.ToNullableEnum<T>(value);
            else if (typeInfo.IsAssignableFrom(typeof(string)))
                value = StringConverter.ToNullableString(value);
            else if (typeInfo.IsAssignableFrom(typeof(long)))
                value = LongConverter.ToNullableLong(value);
            else if (typeInfo.IsAssignableFrom(typeof(int)))
                value = IntegerConverter.ToNullableInteger(value);
            else if (typeInfo.IsAssignableFrom(typeof(double)))
                value = DoubleConverter.ToNullableDouble(value);
            else if (typeInfo.IsAssignableFrom(typeof(float)))
                value = FloatConverter.ToNullableFloat(value);
            else if (typeInfo.IsAssignableFrom(typeof(decimal)))
                value = DecimalConverter.ToNullableDecimal(value);
            else if (typeInfo.IsAssignableFrom(typeof(DateTime)))
                value = DateTimeConverter.ToNullableDateTime(value);
            else if (typeInfo.IsAssignableFrom(typeof(TimeSpan)))
                value = TimeSpanConverter.ToNullableTimeSpan(value);

            if (value == null) return null;

            try
            {
                return (T)value;
            }
            catch
            {
                return null;
            }
        }

        public static T ToType<T>(object value)
        {
            return ToTypeWithDefault(value, default(T));
        }

        public static T ToTypeWithDefault<T>(object value, T defaultValue)
        {
            if (value == null) return defaultValue;
            if (value is T) return (T)value;

            var typeInfo = typeof(T).GetTypeInfo();

            if (typeInfo.IsEnum)
                value = EnumConverter.ToEnumWithDefault<T>(value, defaultValue);
            else if (typeInfo.IsAssignableFrom(typeof(string)))
                value = StringConverter.ToNullableString(value);
            else if (typeInfo.IsAssignableFrom(typeof(long)))
                value = LongConverter.ToNullableLong(value);
            else if (typeInfo.IsAssignableFrom(typeof(int)))
                value = IntegerConverter.ToNullableInteger(value);
            else if (typeInfo.IsAssignableFrom(typeof(double)))
                value = DoubleConverter.ToNullableDouble(value);
            else if (typeInfo.IsAssignableFrom(typeof(float)))
                value = FloatConverter.ToNullableFloat(value);
            else if (typeInfo.IsAssignableFrom(typeof(decimal)))
                value = DecimalConverter.ToNullableDecimal(value);
            else if (typeInfo.IsAssignableFrom(typeof(bool)))
                value = BooleanConverter.ToNullableBoolean(value);
            else if (typeInfo.IsAssignableFrom(typeof(DateTime)))
                value = DateTimeConverter.ToNullableDateTime(value);
            else if (typeInfo.IsAssignableFrom(typeof(TimeSpan)))
                value = TimeSpanConverter.ToNullableTimeSpan(value);

            if (value == null) return defaultValue;

            try
            {
                if (typeInfo.IsClass || typeInfo.IsInterface)
                    return value is T ? (T)value : defaultValue;
                else
                    return (T)value;
            }
            catch
            {
                return defaultValue;
            }
        }

    }
}