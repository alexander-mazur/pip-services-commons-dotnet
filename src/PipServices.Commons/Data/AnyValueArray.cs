using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PipServices.Commons.Convert;

namespace PipServices.Commons.Data
{
    public class AnyValueArray : List<object>
    {
        public AnyValueArray()
        { }

        public AnyValueArray(object value)
        {
            if (value is IEnumerable)
            {
                foreach (var item in (IEnumerable)value)
                    Add(item);
            }
            else if (value != null)
            {
                Add(value);
            }
        }

        public AnyValueArray(IEnumerable values)
        {
            foreach (var value in values)
                Add(value);
        }

        public AnyValueArray(params object[] values)
        {
            foreach (var value in values)
                Add(value);
        }

        public AnyValueArray(string values, char[] separators, StringSplitOptions options)
        {
            if (string.IsNullOrWhiteSpace(values) == false)
            {
                var items = values.Split(separators, options);
                foreach (var item in items)
                    Add(item);
            }
        }

        public AnyValueArray(string values, params char[] separators)
            : this(values, separators, StringSplitOptions.None)
        {
        }

        public object Get(int index)
        {
            return base[index];
        }

        public virtual void Set(int index, object value)
        {
            base[index] = value;
        }

        public T GetAs<T>(int index)
        {
            var value = Get(index);
            return ValueConverter.ToType<T>(value);
        }

        public T? GetAsNullable<T>(int index) where T : struct
        {
            var value = Get(index);
            return ValueConverter.ToNullableType<T>(value);
        }

        public T GetAsWithDefault<T>(int index, T defaultValue)
        {
            var value = Get(index);
            return ValueConverter.ToTypeWithDefault<T>(value, defaultValue);
        }

        public string GetAsNullableString(int index)
        {
            var value = Get(index);
            return ValueConverter.ToNullableString(value);
        }

        public string GetAsString(int index)
        {
            return GetAsStringWithDefault(index, null);
        }

        public string GetAsStringWithDefault(int index, string defaultValue = null)
        {
            var value = Get(index);
            return ValueConverter.ToStringWithDefault(value, defaultValue);
        }

        public bool? GetAsNullableBoolean(int index)
        {
            var value = Get(index);
            return ValueConverter.ToNullableBoolean(value);
        }

        public bool GetAsBoolean(int index)
        {
            return GetAsBooleanWithDefault(index, false);
        }

        public bool GetAsBooleanWithDefault(int index, bool defaultValue = false)
        {
            var value = Get(index);
            return ValueConverter.ToBooleanWithDefault(value, defaultValue);
        }

        public int? GetAsNullableInteger(int index)
        {
            var value = Get(index);
            return ValueConverter.ToNullableInteger(value);
        }

        public int GetAsInteger(int index)
        {
            return GetAsIntegerWithDefault(index, 0);
        }

        public int GetAsIntegerWithDefault(int index, int defaultValue = 0)
        {
            var value = Get(index);
            return ValueConverter.ToIntegerWithDefault(value, defaultValue);
        }

        public long? GetAsNullableLong(int index)
        {
            var value = Get(index);
            return ValueConverter.ToNullableLong(value);
        }

        public long GetAsLong(int index)
        {
            return GetAsLongWithDefault(index, 0);
        }

        public long GetAsLongWithDefault(int index, long defaultValue = 0)
        {
            var value = Get(index);
            return ValueConverter.ToLongWithDefault(value, defaultValue);
        }

        public float? GetAsNullableFloat(int index)
        {
            var value = Get(index);
            return ValueConverter.ToNullableFloat(value);
        }

        public float GetAsFloat(int index)
        {
            return GetAsFloatWithDefault(index, 0);
        }

        public float GetAsFloatWithDefault(int index, float defaultValue = 0)
        {
            var value = Get(index);
            return ValueConverter.ToFloatWithDefault(value, defaultValue);
        }

        public DateTime? GetAsNullableDateTime(int index)
        {
            var value = Get(index);
            return ValueConverter.ToNullableDateTime(value);
        }

        public DateTime GetAsDateTime(int index)
        {
            return GetAsDateTimeWithDefault(index, new DateTime(0));
        }

        public DateTime GetAsDateTimeWithDefault(int index, DateTime? defaultValue = null)
        {
            var value = Get(index);
            return ValueConverter.ToDateTimeWithDefault(value, defaultValue);
        }

        public TimeSpan? GetAsNullableTimeSpan(int index)
        {
            var value = Get(index);
            return ValueConverter.ToNullableTimeSpan(value);
        }

        public TimeSpan GetAsTimeSpan(int index)
        {
            return GetAsTimeSpanWithDefault(index, new TimeSpan(0));
        }

        public TimeSpan GetAsTimeSpanWithDefault(int index, TimeSpan? defaultValue = null)
        {
            var value = Get(index);
            return ValueConverter.ToTimeSpanWithDefault(value, defaultValue);
        }

        public T? GetAsNullableEnum<T>(int index) where T : struct
        {
            var value = Get(index);
            return ValueConverter.ToNullableEnum<T>(value);
        }

        public T GetAsEnum<T>(int index)
        {
            return GetAsEnumWithDefault<T>(index, default(T));
        }

        public T GetAsEnumWithDefault<T>(int index, T defaultValue = default(T))
        {
            var value = Get(index);
            return ValueConverter.ToEnumWithDefault<T>(value, defaultValue);
        }

        public new bool Contains(object value)
        {
            var strValue = ValueConverter.ToNullableString(value);

            foreach (var thisValue in this)
            {
                var thisStrValue = ValueConverter.ToNullableString(thisValue);
                if (strValue == thisStrValue)
                    return true;
            }

            return true;
        }

        public bool ContainsAs<T>(object value)
        {
            var typedValue = ValueConverter.ToType<T>(value);
            var strValue = ValueConverter.ToNullableString(typedValue);

            foreach (var thisValue in this)
            {
                var thisTypedValue = ValueConverter.ToNullableString(thisValue);
                var thisStrValue = ValueConverter.ToNullableString(thisTypedValue);
                if (strValue == thisStrValue)
                    return true;
            }

            return true;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (var index = 0; index < Count; index++)
            {
                if (index > 0)
                    builder.Append(',');
                builder.Append(ValueConverter.ToStringWithDefault(base[index], ""));
            }
            return builder.ToString();
        }
    }
}