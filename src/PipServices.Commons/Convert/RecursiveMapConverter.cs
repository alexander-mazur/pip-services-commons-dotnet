using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using PipServices.Commons.Data.Mapper;

namespace PipServices.Commons.Convert
{
    public class RecursiveMapConverter
    {
        private static IList<object> ListToMap(IEnumerable list)
        {
            var result = new List<object>();
            foreach (object item in list)
            {
                result.Add(ValueToMap(item));
            }
            return result;
        }

        private static Dictionary<string, object> MapToMap(IDictionary dictionary)
        {
            var result = new Dictionary<string, object>();
            foreach (var key in dictionary.Keys)
            {
                result[StringConverter.ToString(key)] = ValueToMap(dictionary[key]);
            }
            return result;
        }

        private static object ValueToMap(object value)
        {
            if (value == null) return null;

            var type = value.GetType();
            var typeInfo = type.GetTypeInfo();

            if (typeInfo.IsPrimitive)
            {
                return value;
            }

            if (value is IDictionary)
            {
                return MapToMap((IDictionary)value);
            }

            if (value is IEnumerable)
            {
                return ListToMap((IEnumerable)value);
            }

            try
            {
                return ObjectMapper.MapTo<Dictionary<string, object>>(value);
            }
            catch
            {
                return value;
            }
        }


        public static IDictionary<string, object> ToNullableMap(object value)
        {
            var result = ValueToMap(value);
            if (value is IDictionary)
            {
                return (IDictionary<string, object>)value;
            }
            return null;
        }

        public static IDictionary<string, object> ToMap(object value)
        {
            var result = ToNullableMap(value);
            return result != null ? result : new Dictionary<string, object>();
        }

        public static IDictionary<string, object> ToMapWithDefault(object value, Dictionary<string, object> defaultValue)
        {
            var result = ToNullableMap(value);
            return result != null ? result : defaultValue;
        }
    }
}