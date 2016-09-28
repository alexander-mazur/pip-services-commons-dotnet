using System.Collections.Generic;
using PipServices.Commons.Data.Mapper;
using Newtonsoft.Json;

namespace PipServices.Commons.Convert
{
    /// <summary>
    /// Converts objects to/from Json format.
    /// </summary>
    public static class JsonConverter
    {
        public static string ToJson(object value)
        {
            if (value == null) return null;
            return JsonConvert.SerializeObject(
                value,
                Formatting.None,
                new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    NullValueHandling = NullValueHandling.Ignore
                }
            );
        }

        public static object FromJson(string value)
        {
            if (value == null) return null;
            return JsonConvert.DeserializeObject(value);
        }

        public static T FromJson<T>(string value)
        {
            if (value == null) return default(T);
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static IDictionary<string, object> ToNullableMap(string value)
        {
            try
            {
                return ObjectMapper.MapTo<Dictionary<string, object>>(value);
            }
            catch
            {
                return null;
            }
        }

        public static IDictionary<string, object> ToMap(string value)
        {
            var result = ToNullableMap(value);
            return result ?? new Dictionary<string, object>();
        }

        public static IDictionary<string, object> ToMapWithDefault(string value, IDictionary<string, object> defaultValue)
        {
            var result = ToNullableMap(value);
            return result ?? defaultValue;
        }
    }
}