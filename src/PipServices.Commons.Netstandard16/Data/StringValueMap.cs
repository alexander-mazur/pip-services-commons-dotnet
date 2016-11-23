using PipServices.Commons.Convert;
using System;
using System.Collections.Generic;
using System.Text;

namespace PipServices.Commons.Data
{
    public class StringValueMap : Dictionary<string, string>
    {
        public StringValueMap()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public StringValueMap(IDictionary<string, string> map)
            : base(StringComparer.OrdinalIgnoreCase)
        {
            SetAsMap(map);
        }

        public new string this[string key]
        {
            get { return Get(key); }
            set { Set(key, value); }
        }

        public virtual string Get(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            foreach (var thisKey in Keys)
            {
                if (string.Compare(thisKey, key, true) == 0)
                    return base[thisKey];
            }

            return null;
        }

        public virtual void Set(string key, string value)
        {
            base[key] = value;
        }

        public new void Add(string key, string value)
        {
            Set(key, value);
        }

        public void SetAsMap(IDictionary<string, object> map)
        {
            Clear();

            if (map == null || map.Count == 0) return;

            foreach (var key in map.Keys)
                SetAsObject(key, StringConverter.ToNullableString(map[key]));
        }

        public void SetAsMap(IDictionary<string, string> map)
        {
            Clear();

            if (map == null || map.Count == 0) return;

            foreach (var key in map.Keys)
                SetAsObject(key, map[key]);
        }

        public object GetAsObject()
        {
            var result = new Dictionary<string, object>();
            foreach (var key in Keys)
                result[key] = this[key];
            return result;
        }

        public void SetAsObject(object value)
        {
            Clear();
            SetAsMap(MapConverter.ToMap(value));
        }

        public void SetAsObject(string key, object value)
        {
            Set(key, StringConverter.ToNullableString(value));
        }

        public object GetAsObject(string key)
        {
            return Get(key);
        }

        public string GetAsNullableString(string key)
        {
            var value = Get(key);
            return StringConverter.ToNullableString(value);
        }

        public string GetAsString(string key)
        {
            return GetAsStringWithDefault(key, null);
        }

        public string GetAsStringWithDefault(string key, string defaultValue = null)
        {
            var value = Get(key);
            return StringConverter.ToStringWithDefault(value, defaultValue);
        }

        public bool? GetAsNullableBoolean(string key)
        {
            var value = Get(key);
            return BooleanConverter.ToNullableBoolean(value);
        }

        public bool GetAsBoolean(string key)
        {
            return GetAsBooleanWithDefault(key, false);
        }

        public bool GetAsBooleanWithDefault(string key, bool defaultValue = false)
        {
            var value = Get(key);
            return BooleanConverter.ToBooleanWithDefault(value, defaultValue);
        }

        public int? GetAsNullableInteger(string key)
        {
            var value = Get(key);
            return IntegerConverter.ToNullableInteger(value);
        }

        public int GetAsInteger(string key)
        {
            return GetAsIntegerWithDefault(key, 0);
        }

        public int GetAsIntegerWithDefault(string key, int defaultValue = 0)
        {
            var value = Get(key);
            return IntegerConverter.ToIntegerWithDefault(value, defaultValue);
        }

        public long? GetAsNullableLong(string key)
        {
            var value = Get(key);
            return LongConverter.ToNullableLong(value);
        }

        public long GetAsLong(string key)
        {
            return GetAsLongWithDefault(key, 0);
        }

        public long GetAsLongWithDefault(string key, long defaultValue = 0)
        {
            var value = Get(key);
            return LongConverter.ToLongWithDefault(value, defaultValue);
        }

        public float? GetAsNullableFloat(string key)
        {
            var value = Get(key);
            return FloatConverter.ToNullableFloat(value);
        }

        public float GetAsFloat(string key)
        {
            return GetAsFloatWithDefault(key, 0);
        }

        public float GetAsFloatWithDefault(string key, float defaultValue = 0)
        {
            var value = Get(key);
            return FloatConverter.ToFloatWithDefault(value, defaultValue);
        }

        public double? GetAsNullableDouble(string key)
        {
            var value = Get(key);
            return DoubleConverter.ToNullableDouble(value);
        }

        public double GetAsDouble(string key)
        {
            return GetAsDoubleWithDefault(key, 0);
        }

        public double GetAsDoubleWithDefault(string key, double defaultValue = 0)
        {
            var value = Get(key);
            return DoubleConverter.ToDoubleWithDefault(value, defaultValue);
        }

        public DateTime? GetAsNullableDateTime(string key)
        {
            var value = Get(key);
            return DateTimeConverter.ToNullableDateTime(value);
        }

        public DateTime GetAsDateTime(string key)
        {
            return GetAsDateTimeWithDefault(key, new DateTime());
        }

        public DateTime GetAsDateTimeWithDefault(string key, DateTime? defaultValue = null)
        {
            var value = Get(key);
            return DateTimeConverter.ToDateTimeWithDefault(value, defaultValue);
        }

        public TimeSpan? GetAsNullableTimeSpan(string key)
        {
            var value = Get(key);
            return TimeSpanConverter.ToNullableTimeSpan(value);
        }

        public TimeSpan GetAsTimeSpan(string key)
        {
            return GetAsTimeSpanWithDefault(key, new TimeSpan(0));
        }

        public TimeSpan GetAsTimeSpanWithDefault(string key, TimeSpan? defaultValue = null)
        {
            var value = Get(key);
            return TimeSpanConverter.ToTimeSpanWithDefault(value, defaultValue);
        }

        public T? GetAsNullableEnum<T>(string key) where T : struct
        {
            var value = Get(key);
            return EnumConverter.ToNullableEnum<T>(value);
        }

        public T GetAsEnum<T>(string key)
        {
            return GetAsEnumWithDefault<T>(key, default(T));
        }

        public T GetAsEnumWithDefault<T>(string key, T defaultValue = default(T))
        {
            var value = Get(key);
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
            return value != null ? AnyValueArray.FromValue(value) : null;
        }

        public AnyValueArray GetAsArray(string key)
        {
            return AnyValueArray.FromValue(GetAsObject(key));
        }

        public AnyValueArray GetAsArrayWithDefault(string key, AnyValueArray defaultValue)
        {
            var value = GetAsNullableArray(key);
            return value != null ? value : defaultValue;
        }

        public AnyValueMap GetAsNullableMap(string key)
        {
            var result = GetAsObject(key);
            return result != null ? AnyValueMap.FromValue(result) : null;
        }

        public AnyValueMap GetAsMap(string key)
        {
            return AnyValueMap.FromValue(GetAsObject(key));
        }

        public AnyValueMap GetAsMapWithDefault(string key, AnyValueMap defaultValue)
        {
            var result = GetAsNullableMap(key);
            return result ?? defaultValue;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var key in Keys)
            {
                if (builder.Length > 0)
                    builder.Append(";");

                var value = this[key];
                if (value != null)
                    builder.Append(key + "=" + value);
                else
                    builder.Append(key);
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

            if (tuples == null || tuples.Length == 0)
                return result;

            for (var i = 0; i < tuples.Length; i += 2)
            {
                if (i + 1 >= tuples.Length) break;

                var name = StringConverter.ToString(tuples[i]);
                var value = StringConverter.ToString(tuples[i + 1]);

                result[name] = value;
            }

            return result;
        }

        public static StringValueMap FromString(string line)
        {
            var result = new StringValueMap();
            if (string.IsNullOrWhiteSpace(line))
                return result;

            var tokens = line.Split(';');
            foreach (var token in tokens)
            {
                if (token.Length == 0) continue;
                var index = token.IndexOf("=");
                var key = index > 0 ? token.Substring(0, index).Trim() : token.Trim();
                var value = index > 0 ? token.Substring(index + 1).Trim() : null;
                result[key] = value;
            }
            return result;
        }

        public static StringValueMap FromMaps(params IDictionary<string, string>[] maps)
        {
            var result = new StringValueMap();
            if (maps != null && maps.Length > 0)
            {
                foreach (var map in maps)
                    result.SetAsMap(map);
            }
            return result;
        }
    }
}