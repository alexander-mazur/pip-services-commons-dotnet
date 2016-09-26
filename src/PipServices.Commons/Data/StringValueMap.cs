using System;
using System.Collections;
using System.Collections.Generic;
using PipServices.Commons.Convert;
using System.Text;

namespace PipServices.Commons.Data
{
    public class StringValueMap : Dictionary<string, string>
    {
        public StringValueMap() { }

        public StringValueMap(IDictionary map)
            : base()
        {
            SetAsMap(map);
        }

        public string Get(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            foreach (var key in Keys)
            {
                if (string.Compare(key, name, true) == 0)
                {
                    return this[key];
                }
            }
            return null;
        }

        public void SetAsMap(IDictionary map)
        {
            foreach (var key in map.Keys)
            {
                this[StringConverter.ToString(key)] = StringConverter.ToNullableString(map[key]);
            }
        }

        public object GetAsObject()
        {
            var result = new Dictionary<string, object>();
            foreach (var key in Keys)
            {
                result[key] = this[key];
            }
            return result;
        }

        public void SetAsObject(object value)
        {
            Clear();
            SetAsMap((IDictionary)MapConverter.ToMap(value));
        }

        public object GetAsObject(string key)
        {
            return Get(key);
        }

        public string GetAsNullableString(string key)
        {
            var value = TryGet(key);
            return StringConverter.ToNullableString(value);
        }

        public string GetAsString(string key)
        {
            return GetAsStringWithDefault(key, null);
        }

        public string GetAsStringWithDefault(string key, string defaultValue = null)
        {
            var value = TryGet(key);
            return StringConverter.ToStringWithDefault(value, defaultValue);
        }

        public bool? GetAsNullableBoolean(string key)
        {
            var value = TryGet(key);
            return BooleanConverter.ToNullableBoolean(value);
        }

        public bool GetAsBoolean(string key)
        {
            return GetAsBooleanWithDefault(key, false);
        }

        public bool GetAsBooleanWithDefault(string key, bool defaultValue = false)
        {
            var value = TryGet(key);
            return BooleanConverter.ToBooleanWithDefault(value, defaultValue);
        }

        public int? GetAsNullableInteger(string key)
        {
            var value = TryGet(key);
            return IntegerConverter.ToNullableInteger(value);
        }

        public int GetAsInteger(string key)
        {
            return GetAsIntegerWithDefault(key, 0);
        }

        public int GetAsIntegerWithDefault(string key, int defaultValue = 0)
        {
            var value = TryGet(key);
            return IntegerConverter.ToIntegerWithDefault(value, defaultValue);
        }

        public long? GetAsNullableLong(string key)
        {
            var value = TryGet(key);
            return LongConverter.ToNullableLong(value);
        }

        public long GetAsLong(string key)
        {
            return GetAsLongWithDefault(key, 0);
        }

        public long GetAsLongWithDefault(string key, long defaultValue = 0)
        {
            var value = TryGet(key);
            return LongConverter.ToLongWithDefault(value, defaultValue);
        }

        public float? GetAsNullableFloat(string key)
        {
            var value = TryGet(key);
            return FloatConverter.ToNullableFloat(value);
        }

        public float GetAsFloat(string key)
        {
            return GetAsFloatWithDefault(key, 0);
        }

        public float GetAsFloatWithDefault(string key, float defaultValue = 0)
        {
            var value = TryGet(key);
            return FloatConverter.ToFloatWithDefault(value, defaultValue);
        }

        public double? GetAsNullableDouble(string key)
        {
            var value = TryGet(key);
            return DoubleConverter.ToNullableDouble(value);
        }

        public double GetAsDouble(string key)
        {
            return GetAsDoubleWithDefault(key, 0);
        }

        public double GetAsDoubleWithDefault(string key, double defaultValue = 0)
        {
            var value = TryGet(key);
            return DoubleConverter.ToDoubleWithDefault(value, defaultValue);
        }

        public DateTime? GetAsNullableDateTime(string key)
        {
            var value = TryGet(key);
            return DateTimeConverter.ToNullableDateTime(value);
        }

        public DateTime GetAsDateTime(string key)
        {
            return GetAsDateTimeWithDefault(key, new DateTime(0));
        }

        public DateTime GetAsDateTimeWithDefault(string key, DateTime? defaultValue = null)
        {
            var value = TryGet(key);
            return DateTimeConverter.ToDateTimeWithDefault(value, defaultValue);
        }

        public TimeSpan? GetAsNullableTimeSpan(string key)
        {
            var value = TryGet(key);
            return TimeSpanConverter.ToNullableTimeSpan(value);
        }

        public TimeSpan GetAsTimeSpan(string key)
        {
            return GetAsTimeSpanWithDefault(key, new TimeSpan(0));
        }

        public TimeSpan GetAsTimeSpanWithDefault(string key, TimeSpan? defaultValue = null)
        {
            var value = TryGet(key);
            return TimeSpanConverter.ToTimeSpanWithDefault(value, defaultValue);
        }

        public T? GetAsNullableEnum<T>(string key) where T : struct
        {
            var value = TryGet(key);
            return EnumConverter.ToNullableEnum<T>(value);
        }

        public T GetAsEnum<T>(string key)
        {
            return GetAsEnumWithDefault<T>(key, default(T));
        }

        public T GetAsEnumWithDefault<T>(string key, T defaultValue = default(T))
        {
            var value = TryGet(key);
            return EnumConverter.ToEnumWithDefault<T>(value, defaultValue);
        }

        public T? GetAsNullableType<T>(string key) where T : struct
        {
            return TypeConverter.ToNullableType<T>(key);
        }

        public T GetAsType<T>(string key) where T : struct
        {
            return TypeConverter.ToType<T>(key);
        }

        public T GetAsTypeWithDefault<T>(string key, T defaultValue) where T : struct
        {
            return TypeConverter.ToTypeWithDefault<T>(key, defaultValue);
        }

        public AnyValue GetAsValue(string key)
        {
            return new AnyValue(GetAsObject(key));
        }

        public AnyValueArray GetAsNullableArray(string key)
        {
            var value = GetAsObject(key);
            return value != null ? new AnyValueArray(value) : null;
        }

        public AnyValueArray GetAsArray(string key)
        {
            return new AnyValueArray(GetAsObject(key));
        }

        public AnyValueArray GetAsArrayWithDefault(string key, AnyValueArray defaultValue)
        {
            var value = GetAsObject(key);
            return value != null ? new AnyValueArray(value) : defaultValue;
        }

        public AnyValueMap GetAsNullableMap(string key)
        {
            var result = GetAsObject(key);
            return result != null ? new AnyValueMap(result) : null;
        }

        public AnyValueMap GetASMap(string key)
        {
            return new AnyValueMap(GetAsObject(key));
        }

        public AnyValueMap GetAsMapWithDefault(string key, AnyValueMap defaultValue)
        {
            var result = GetAsNullableMap(key);
            return result != null ? result : defaultValue;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var key in Keys)
            {
                if (builder.Length > 0)
                {
                    builder.Append(";");
                }
                var value = this[key];
                if (value != null)
                {
                    builder.Append(key + "=" + value);
                }
                else
                {
                    builder.Append(key);
                }
            }
            return builder.ToString();
        }

        public object Clone()
        {
            return new StringValueMap(this);
        }

        public static StringValueMap FromTuples(params object[] tuples)
        {
            var result = new StringValueMap();
            for (var i = 0; i < tuples.Length; i += 2)
            {
                if (i + 1 >= tuples.Length) break;

                var name = StringConverter.ToString(tuples[i]);
                var value = StringConverter.ToString(tuples[i + 1]);

                result.Put(name, value);
            }
            return result;
        }

        public static StringValueMap FromString(string line)
        {
            var result = new StringValueMap();
            if (line == null || line.Length == 0) return result;

            var tokens = line.Split(';');
            foreach (var token in tokens)
            {
                if (token.Length == 0) continue;
                var index = token.IndexOf("=");
                var key = index > 0 ? token.Substring(0, index).Trim() : token.Trim();
                var value = index > 0 ? token.Substring(index + 1).Trim() : null;
                result.Put(key, value);
            }
            return result;
        }

        public static StringValueMap FromMaps(params IDictionary[] maps)
        {
            var result = new StringValueMap();
            if (maps != null && maps.Length > 0)
            {
                foreach (var map in maps)
                {
                    result.SetAsMap(map);
                }
            }
            return result;
        }

        private string TryGet(string key)
        {
            string value = null;
            this.TryGetValue(key, out value);
            return value;
        }

        public virtual void Put(string key, object value)
        {
            base[key] = StringConverter.ToNullableString(value);
        }
    }
}