using System;
using System.Globalization;

namespace PipServices.Commons.Convert
{
    // Todo: Change DateTime to DataTimeOffset

    /// <summary>
    /// Converts objects to DateTime.
    /// </summary>
    public class DateTimeConverter
    {
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
    }
}