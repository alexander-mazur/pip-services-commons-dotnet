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
            return StringConverter.ToNullableString(_value);
        }

        public string GetAsString()
        {
            return GetAsStringWithDefault(null);
        }

        public string GetAsStringWithDefault(string defaultValue = null)
        {
            return StringConverter.ToStringWithDefault(_value, defaultValue);
        }

        public bool? GetAsNullableBoolean()
        {
            return BooleanConverter.ToNullableBoolean(_value);
        }

        public bool GetAsBoolean()
        {
            return GetAsBooleanWithDefault(false);
        }

        public bool GetAsBooleanWithDefault(bool defaultValue = false)
        {
            return BooleanConverter.ToBooleanWithDefault(_value, defaultValue);
        }

        public int? GetAsNullableInteger()
        {
            return IntegerConverter.ToNullableInteger(_value);
        }

        public int GetAsInteger()
        {
            return GetAsIntegerWithDefault(0);
        }

        public int GetAsIntegerWithDefault(int defaultValue = 0)
        {
            return IntegerConverter.ToIntegerWithDefault(_value, defaultValue);
        }

        public long? GetAsNullableLong()
        {
            return LongConverter.ToNullableLong(_value);
        }

        public long GetAsLong()
        {
            return GetAsLongWithDefault(0);
        }

        public long GetAsLongWithDefault(long defaultValue = 0)
        {
            return LongConverter.ToLongWithDefault(_value, defaultValue);
        }

        public float? GetAsNullableFloat()
        {
            return FloatConverter.ToNullableFloat(_value);
        }

        public float GetAsFloat()
        {
            return GetAsFloatWithDefault(0);
        }

        public float GetAsFloatWithDefault(float defaultValue = 0)
        {
            return FloatConverter.ToFloatWithDefault(_value, defaultValue);
        }

        public DateTime? GetAsNullableDateTime()
        {
            return DateTimeConverter.ToNullableDateTime(_value);
        }

        public DateTime GetAsDateTime()
        {
            return GetAsDateTimeWithDefault(new DateTime(0));
        }

        public DateTime GetAsDateTimeWithDefault(DateTime? defaultValue = null)
        {
            return DateTimeConverter.ToDateTimeWithDefault(_value, defaultValue);
        }

        public TimeSpan? GetAsNullableTimeSpan()
        {
            return TimeSpanConverter.ToNullableTimeSpan(_value);
        }

        public TimeSpan GetAsTimeSpan()
        {
            return GetAsTimeSpanWithDefault(new TimeSpan(0));
        }

        public TimeSpan GetAsTimeSpanWithDefault(TimeSpan? defaultValue = null)
        {
            return TimeSpanConverter.ToTimeSpanWithDefault(_value, defaultValue);
        }

        public T? GetAsNullableEnum<T>() where T : struct
        {
            return EnumConverter.ToNullableEnum<T>(_value);
        }

        public T GetAsEnum<T>()
        {
            return GetAsEnumWithDefault<T>(default(T));
        }

        public T GetAsEnumWithDefault<T>(T defaultValue = default(T))
        {
            return EnumConverter.ToEnumWithDefault<T>(_value, defaultValue);
        }

        public override bool Equals(object obj)
        {
            if (obj == null && _value == null) return true;
            if (obj == null || _value == null) return false;

            obj = obj is AnyValue ? ((AnyValue)obj)._value : obj;

            return StringConverter.ToString(_value) == StringConverter.ToString(obj);
        }

        public bool EqualsAs<T>(object obj)
        {
            if (obj == null && _value == null) return true;
            if (obj == null || _value == null) return false;

            obj = obj is AnyValue ? ((AnyValue)obj)._value : obj;

            var value1 = TypeConverter.ToType<T>(_value);
            var value2 = TypeConverter.ToType<T>(obj);

            if (value1 == null && value2 == null) return true;
            if (value1 == null || _value == null) return false;

            return StringConverter.ToString(value1) == StringConverter.ToString(value2);
        }

        public override string ToString()
        {
            return StringConverter.ToString(_value);
        }

        public override int GetHashCode()
        {
            return _value != null ? _value.GetHashCode() : 0;
        }
    }
}