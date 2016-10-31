using System;
using System.Globalization;

namespace PipServices.Commons.Convert
{
    /// <summary>
    /// Converts objects to DateTime.
    /// </summary>
    public class DateTimeConverter
    {
        public static DateTimeOffset? ToNullableDateTime(object value)
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

        public static DateTimeOffset ToDateTime(object value)
        {
            return ToDateTimeWithDefault(value, new DateTimeOffset());
        }

        public static DateTimeOffset ToDateTimeWithDefault(object value, DateTimeOffset? defaultValue = null)
        {
            var realDefault = defaultValue ?? new DateTimeOffset();
            var result = ToNullableDateTime(value);
            return result ?? realDefault;
        }
    }
}