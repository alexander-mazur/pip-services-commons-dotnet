using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using PipServices.Commons.Data.Mapper;

namespace PipServices.Commons.Convert
{
    public class MapConverter
    {
        private static IDictionary<string, object> MapToMap(IDictionary dictionary)
        {
            var result = new Dictionary<string, object>();
            foreach(var key in dictionary.Keys)
            {
                result[StringConverter.ToString(key)] = dictionary[key];
            }
            return result;
        }

        private static IDictionary<string, object> EnumerableToMap(IEnumerable enumerable)
        {
            var result = new Dictionary<string, object>();
            var index = 0;
            foreach (var obj in enumerable)
            {
                result[StringConverter.ToString(index++)] = obj;
            }
            return result;
        }
        public static IDictionary<string, object> ToNullableMap(object value)
        {
            if (value == null) return null;

            var type = value.GetType();
            var typeInfo = type.GetTypeInfo();

            if (typeInfo.IsPrimitive) return null;

            if(value is IDictionary)
            {
                return MapToMap(((IDictionary)value));
            }

            if (value is IEnumerable)
            {
                return EnumerableToMap(((IEnumerable)value));
            }

            try
            {
                return ObjectMapper.MapTo<Dictionary<string, object>>(value);
            }
            catch
            {
                return null;
            }
        }

        public static IDictionary<string, object> ToMap(object value)
        {
            var result = ToNullableMap(value);
            return result ?? new Dictionary<string, object>();
        }

        public static IDictionary<string, object> ToMapWithDefault(object value, Dictionary<string, object> defaultValue)
        {
            var result = ToNullableMap(value);
            return result ?? defaultValue;
        }
    }
}