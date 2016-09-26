using System;
using System.Collections.Generic;
using PipServices.Commons.Convert;

namespace PipServices.Commons.Data
{
    public class StringValueMap : Dictionary<string, string>
    {
        public StringValueMap() { }

        public StringValueMap(IDictionary<string, string> content)
            : base()
        {
            if (content != null)
            {
                foreach (var entry in content)
                    Add(entry.Key, entry.Value);
            }
        }

        public StringValueMap(params object[] values)
        {
            SetTuples(values);
        }

        public virtual string Get(string key)
        {
            string value = TryGet(key);
            if (value == null)
                throw new NullReferenceException("Value with key " + key + " is not defined");
            return value;
        }

        public string TryGet(string key)
        {
            string value = null;
            this.TryGetValue(key, out value);
            return value;
        }

        public void SetTuples(params object[] values)
        {
            for (var i = 0; i < values.Length; i += 2)
            {
                if (i + 1 >= values.Length) break;

                var name = values[i].ToString();
                var value = values[i + 1];

                Set(name, value);
            }
        }

        public virtual void Set(string key, object value)
        {
            base[key] = StringConverter.ToNullableString(value);
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

        public void SetAsJson(string key, object value)
        {
            var strValue = JsonConverter.ToJson(value);
            Set(key, strValue);
        }

        public T GetAsJson<T>(string key)
        {
            string value = TryGet(key);
            return JsonConverter.FromJson<T>(value);
        }

    }
}