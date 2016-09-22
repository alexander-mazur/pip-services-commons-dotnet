using System;
using PipServices.Commons.Convert;

namespace PipServices.Commons.Data
{
    public class AnyValue
    {
        private object _value;

        public AnyValue(object value = null)
        {
            _value = value;
        }

        public AnyValue(AnyValue value)
        {
            _value = value.Get();
        }

        public object Value
        {
            get { return Get(); }
            set { Set(value); }
        }

        public virtual object Get()
        {
            return _value;
        }

        public virtual void Set(object value)
        {
            _value = value;
        }

        public string GetAsNullableString()
        {
            return ValueConverter.ToNullableString(_value);
        }

        public string GetAsString()
        {
            return GetAsStringWithDefault(null);
        }

        public string GetAsStringWithDefault(string defaultValue = null)
        {
            return ValueConverter.ToStringWithDefault(_value, defaultValue);
        }

        public bool? GetAsNullableBoolean()
        {
            return ValueConverter.ToNullableBoolean(_value);
        }

        public bool GetAsBoolean()
        {
            return GetAsBooleanWithDefault(false);
        }

        public bool GetAsBooleanWithDefault(bool defaultValue = false)
        {
            return ValueConverter.ToBooleanWithDefault(_value, defaultValue);
        }

        public int? GetAsNullableInteger()
        {
            return ValueConverter.ToNullableInteger(_value);
        }

        public int GetAsInteger()
        {
            return GetAsIntegerWithDefault(0);
        }

        public int GetAsIntegerWithDefault(int defaultValue = 0)
        {
            return ValueConverter.ToIntegerWithDefault(_value, defaultValue);
        }

        public long? GetAsNullableLong()
        {
            return ValueConverter.ToNullableLong(_value);
        }

        public long GetAsLong()
        {
            return GetAsLongWithDefault(0);
        }

        public long GetAsLongWithDefault(long defaultValue = 0)
        {
            return ValueConverter.ToLongWithDefault(_value, defaultValue);
        }

        public float? GetAsNullableFloat()
        {
            return ValueConverter.ToNullableFloat(_value);
        }

        public float GetAsFloat()
        {
            return GetAsFloatWithDefault(0);
        }

        public float GetAsFloatWithDefault(float defaultValue = 0)
        {
            return ValueConverter.ToFloatWithDefault(_value, defaultValue);
        }

        public DateTime? GetAsNullableDateTime()
        {
            return ValueConverter.ToNullableDateTime(_value);
        }

        public DateTime GetAsDateTime()
        {
            return GetAsDateTimeWithDefault(new DateTime(0));
        }

        public DateTime GetAsDateTimeWithDefault(DateTime? defaultValue = null)
        {
            return ValueConverter.ToDateTimeWithDefault(_value, defaultValue);
        }

        public TimeSpan? GetAsNullableTimeSpan()
        {
            return ValueConverter.ToNullableTimeSpan(_value);
        }

        public TimeSpan GetAsTimeSpan()
        {
            return GetAsTimeSpanWithDefault(new TimeSpan(0));
        }

        public TimeSpan GetAsTimeSpanWithDefault(TimeSpan? defaultValue = null)
        {
            return ValueConverter.ToTimeSpanWithDefault(_value, defaultValue);
        }

        public T? GetAsNullableEnum<T>() where T : struct
        {
            return ValueConverter.ToNullableEnum<T>(_value);
        }

        public T GetAsEnum<T>()
        {
            return GetAsEnumWithDefault<T>(default(T));
        }

        public T GetAsEnumWithDefault<T>(T defaultValue = default(T))
        {
            return ValueConverter.ToEnumWithDefault<T>(_value, defaultValue);
        }

        public void SetAsJson(object value)
        {
            _value = DataConverter.Serialize(value);
        }

        public T GetAsJson<T>()
        {
            string value = ValueConverter.ToNullableString(_value);
            return DataConverter.DeserializeAs<T>(value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null && _value == null) return true;
            if (obj == null || _value == null) return false;

            obj = obj is AnyValue ? ((AnyValue)obj)._value : obj;

            return ValueConverter.ToString(_value) == ValueConverter.ToString(obj);
        }

        public bool EqualsAs<T>(object obj)
        {
            if (obj == null && _value == null) return true;
            if (obj == null || _value == null) return false;

            obj = obj is AnyValue ? ((AnyValue)obj)._value : obj;

            var value1 = ValueConverter.ToType<T>(_value);
            var value2 = ValueConverter.ToType<T>(obj);

            if (value1 == null && value2 == null) return true;
            if (value1 == null || _value == null) return false;

            return ValueConverter.ToString(value1) == ValueConverter.ToString(value2);
        }

        public override string ToString()
        {
            return ValueConverter.ToString(_value);
        }

        public override int GetHashCode()
        {
            return _value != null ? _value.GetHashCode() : 0;
        }
    }
}