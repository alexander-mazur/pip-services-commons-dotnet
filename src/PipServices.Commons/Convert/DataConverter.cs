using Newtonsoft.Json;

namespace PipServices.Commons.Convert
{
    public static class DataConverter
    {
        public static string Serialize(object value)
        {
            if (value == null) return null;
            //return JsonConvert.SerializeObject(value);
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

        public static object Deserialize(string value)
        {
            if (value == null) return null;
            return JsonConvert.DeserializeObject(value);
        }

        public static T DeserializeAs<T>(string value)
        {
            if (value == null) return default(T);
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
