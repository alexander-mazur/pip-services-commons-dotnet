using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Reflection;

namespace PipServices.Commons.Convert
{
    public static class ValueConverter
    {
        public static T? ToNullableType<T>(object value) where T : struct
        {
            if (value == null) return null;
            if (value is T) return (T)value;

            var typeInfo = typeof(T).GetTypeInfo();

            if (typeInfo.IsEnum)
                value = ToNullableEnum<T>(value);
            else if (typeInfo.IsAssignableFrom(typeof(string)))
                value = ToNullableString(value);
            else if (typeInfo.IsAssignableFrom(typeof(long)))
                value = ToNullableLong(value);
            else if (typeInfo.IsAssignableFrom(typeof(int)))
                value = ToNullableInteger(value);
            else if (typeInfo.IsAssignableFrom(typeof(float)))
                value = ToNullableFloat(value);
            else if (typeInfo.IsAssignableFrom(typeof(DateTime)))
                value = ToNullableDateTime(value);
            else if (typeInfo.IsAssignableFrom(typeof(TimeSpan)))
                value = ToNullableTimeSpan(value);

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
                value = ToEnumWithDefault<T>(value, defaultValue);
            else if (typeInfo.IsAssignableFrom(typeof(string)))
                value = ToNullableString(value);
            else if (typeInfo.IsAssignableFrom(typeof(long)))
                value = ToNullableLong(value);
            else if (typeInfo.IsAssignableFrom(typeof(int)))
                value = ToNullableInteger(value);
            else if (typeInfo.IsAssignableFrom(typeof(float)))
                value = ToNullableFloat(value);
            else if (typeInfo.IsAssignableFrom(typeof(bool)))
                value = ToNullableBoolean(value);
            else if (typeInfo.IsAssignableFrom(typeof(DateTime)))
                value = ToNullableDateTime(value);
            else if (typeInfo.IsAssignableFrom(typeof(TimeSpan)))
                value = ToNullableTimeSpan(value);

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

        public static string ToNullableString(object value)
        {
            return ToStringWithDefault(value, null);
        }

        public static string ToString(object value)
        {
            return ToStringWithDefault(value, null);
        }

        public static string ToStringWithDefault(object value, string defaultValue = null)
        {
            if (value == null) return defaultValue;
            if (value is string) return (string)value;

            if (value is TimeSpan || value is TimeSpan?)
                value = ((TimeSpan)value).TotalMilliseconds;

            if (value is DateTime || value is DateTime?)
                return ((DateTime)value).ToString("o", CultureInfo.InvariantCulture);

            if (value is IEnumerable)
            {
                var builder = new StringBuilder();
                foreach (var item in (IEnumerable)value)
                {
                    if (builder.Length > 0)
                        builder.Append(',');
                    builder.Append(ToStringWithDefault(item, ""));
                }
                return builder.ToString();
            }

            try
            {
                return System.Convert.ToString(value, CultureInfo.InvariantCulture);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool? ToNullableBoolean(object value)
        {
            if (value == null) return null;
            if (value is bool || value is bool?) return (bool)value;

            var strValue = System.Convert.ToString(value, CultureInfo.InvariantCulture).ToLowerInvariant();
            if (strValue == "1" || strValue == "true" || strValue == "t"
                || strValue == "yes" || strValue == "y")
                return true;

            if (strValue == "0" || strValue == "false" || strValue == "f"
                || strValue == "no" || strValue == "n")
                return false;

            return null;
        }

        public static bool ToBoolean(object value)
        {
            return ToBooleanWithDefault(value, false);
        }

        public static bool ToBooleanWithDefault(object value, bool defaultValue = false)
        {
            var result = ToNullableBoolean(value);
            return result.HasValue ? result.Value : defaultValue;
        }

        public static int? ToNullableInteger(object value)
        {
            if (value == null) return null;
            if (value is DateTime || value is DateTime?)
                return (int)((DateTime)value).Ticks;
            if (value is TimeSpan || value is TimeSpan?)
                return (int)((TimeSpan)value).TotalMilliseconds;
            if (value is bool || value is bool?)
                return (bool)value ? 1 : 0;

            try
            {
                return System.Convert.ToInt32(value, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        public static int ToInteger(object value)
        {
            return ToIntegerWithDefault(value, 0);
        }

        public static int ToIntegerWithDefault(object value, int defaultValue = 0)
        {
            var result = ToNullableInteger(value);
            return result.HasValue ? result.Value : defaultValue;
        }

        public static long? ToNullableLong(object value)
        {
            if (value == null) return null;
            if (value is TimeSpan || value is TimeSpan?)
                return (long)((TimeSpan)value).TotalMilliseconds;
            if (value is DateTime || value is DateTime?)
                return ((DateTime)value).Ticks;
            if (value is bool || value is bool?)
                return (bool)value ? 1 : 0;

            try
            {
                return System.Convert.ToInt64(value, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        public static long ToLong(object value)
        {
            return ToLongWithDefault(value, 0);
        }

        public static long ToLongWithDefault(object value, long defaultValue = 0)
        {
            var result = ToNullableLong(value);
            return result.HasValue ? result.Value : defaultValue;
        }

        public static float? ToNullableFloat(object value)
        {
            if (value == null) return null;
            if (value is TimeSpan || value is TimeSpan?)
                return (float)((TimeSpan)value).TotalMilliseconds;
            if (value is DateTime || value is DateTime?)
                return ((DateTime)value).Ticks;
            if (value is bool || value is bool?)
                return (bool)value ? 1 : 0;

            try
            {
                return System.Convert.ToSingle(value, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        public static float ToFloat(object value)
        {
            return ToFloatWithDefault(value, 0);
        }

        public static float ToFloatWithDefault(object value, float defaultValue = 0)
        {
            var result = ToNullableFloat(value);
            return result.HasValue ? result.Value : defaultValue;
        }

        public static DateTime? ToNullableDateTime(object value)
        {
            if (value == null) return null;
            if (value is DateTime || value is DateTime?)
                return (DateTime)value;
            if (value is TimeSpan || value is TimeSpan?)
                value = (long)((TimeSpan)value).TotalMilliseconds;
            if (value is int) return new DateTime((int)value);
            if (value is long) return new DateTime((long)value);

            try
            {
                if (value is string)
                    return DateTime.Parse((string)value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);

                return System.Convert.ToDateTime(value, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        public static DateTime ToDateTime(object value)
        {
            return ToDateTimeWithDefault(value, new DateTime(0));
        }

        public static DateTime ToDateTimeWithDefault(object value, DateTime? defaultValue = null)
        {
            var realDefault = defaultValue.HasValue ? defaultValue.Value : new DateTime(0);
            var result = ToNullableDateTime(value);
            return result.HasValue ? result.Value : realDefault;
        }

        public static TimeSpan? ToNullableTimeSpan(object value)
        {
            if (value == null) return null;
            if (value is DateTime || value is DateTime?) return null;
            if (value is TimeSpan || value is TimeSpan?) return (TimeSpan)value;
            if (value is int) return TimeSpan.FromMilliseconds((int)value);
            if (value is long) return TimeSpan.FromMilliseconds((long)value);
            if (value is float) return TimeSpan.FromMilliseconds((float)value);
            if (value is double) return TimeSpan.FromMilliseconds((double)value);

            try
            {
                float? millis = ToNullableFloat(value);
                if (millis.HasValue) return TimeSpan.FromMilliseconds(millis.Value);
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static TimeSpan ToTimeSpan(object value)
        {
            return ToTimeSpanWithDefault(value, new TimeSpan(0));
        }

        public static TimeSpan ToTimeSpanWithDefault(object value, TimeSpan? defaultValue = null)
        {
            var realDefault = defaultValue.HasValue ? defaultValue.Value : new TimeSpan(0);
            var result = ToNullableTimeSpan(value);
            return result.HasValue ? result.Value : realDefault;
        }

        public static IList<object> ToNullableArray(object value)
        {
            // Return null when nothing found
            if (value == null)
            {
                return null;
            }

            // Convert enumerable
            if (value is IEnumerable)
            {
                var array = new List<object>();
                foreach (var item in (IEnumerable)value)
                    array.Add(item);
                return array;
            }
            else
            {
                // Convert single values
                IList<object> array = new List<object>();
                array.Add(value);
                return array;
            }
        }

        public static IList<object> ToArray(object value)
        {
            var result = ToNullableArray(value);
            return result != null ? result : new List<object>();
        }

        public static IList<object> ToArrayWithDefault(object value, IList<object> defaultValue)
        {
            var result = ToNullableArray(value);
            return result != null ? result : defaultValue;
        }

        public static T? ToNullableEnum<T>(object value) where T : struct
        {
            if (value == null) return null;

            try
            {
                return (T)value;
            }
            catch
            {
                try
                {
                    return (T)Enum.Parse(typeof(T), value.ToString());
                }
                catch
                {
                    return null;
                }
            }
        }

        public static T ToEnum<T>(object value)
        {
            return ToEnumWithDefault<T>(value, default(T));
        }

        public static T ToEnumWithDefault<T>(object value, T defaultValue)
        {
            if (value == null) return defaultValue;

            try
            {
                return (T)value;
            }
            catch
            {
                try
                {
                    return (T)Enum.Parse(typeof(T), value.ToString());
                }
                catch
                {
                    return defaultValue;
                }
            }
        }

    }
}