using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace PipServices.Commons.Reflect
{
    public static class TypeMatcher
    {
        public static bool MatchValue(object expectedType, object actualValue)
        {
            if (expectedType == null)
                return true;
            if (actualValue == null)
                throw new ArgumentNullException(nameof(actualValue), "Actual value cannot be null");

            return MatchType(expectedType, actualValue.GetType());
        }

        public static bool MatchType(object expectedType, Type actualType)
        {
            if (expectedType == null)
                return true;
            if (actualType == null)
                throw new ArgumentNullException(nameof(actualType), "Actual type cannot be null");

            var type = expectedType as Type;
            if (type != null)
                return type.GetTypeInfo().IsAssignableFrom(actualType);

            var str = expectedType as string;
            if (str != null)
                return MatchTypeByName(str, actualType);

            return false;
        }

        public static bool MatchTypeByName(string expectedType, Type actualType)
        {
            if (expectedType == null)
                return true;

            if (actualType == null)
                throw new ArgumentNullException(nameof(actualType), "Actual type cannot be null");

            expectedType = expectedType.ToLower();

            if (actualType.Name.Equals(expectedType, StringComparison.OrdinalIgnoreCase))
                return true;

            if (actualType.Name.Equals(expectedType, StringComparison.OrdinalIgnoreCase))
                return true;

            if (expectedType.Equals("object"))
                return true;

            if (expectedType.Equals("int") || expectedType.Equals("integer"))
                return actualType == typeof(int) || actualType == typeof(long);

            if (expectedType.Equals("long"))
                return actualType == typeof(long);

            if (expectedType.Equals("float"))
                return actualType == typeof(float) || actualType == typeof(double);

            if (expectedType.Equals("double"))
                return actualType == typeof(double);

            if (expectedType.Equals("string"))
                return actualType == typeof(string);

            if (expectedType.Equals("bool") || expectedType.Equals("boolean"))
                return actualType == typeof(bool);

            if (expectedType.Equals("date") || expectedType.Equals("datetime"))
                return actualType == typeof(DateTime) || actualType == typeof(DateTimeOffset);

            if (expectedType.Equals("timespan") || expectedType.Equals("duration"))
                return actualType == typeof(TimeSpan)
                       || actualType == typeof(int)
                       || actualType == typeof(float)
                       || actualType == typeof(double);

            if (expectedType.Equals("enum"))
                return actualType.GetTypeInfo().IsEnum;

            if (expectedType.Equals("map") || expectedType.Equals("dict") || expectedType.Equals("dictionary"))
            {
                var type = actualType.GetTypeInfo();
                return type.GetInterfaces().Contains(typeof(IDictionary))
                       || type.GetInterfaces().Contains(typeof(IDictionary<,>));
            }

            if (expectedType.Equals("array") || expectedType.Equals("list"))
            {
                var type = actualType.GetTypeInfo();
                return actualType.IsArray
                       || type.GetInterfaces().Contains(typeof(IList))
                       || type.GetInterfaces().Contains(typeof(IList<>));
            }

            if (expectedType.EndsWith("[]"))
            {
                // Todo: Check subtype
                var type = actualType.GetTypeInfo();
                return actualType.IsArray
                       || type.GetInterfaces().Contains(typeof(IList))
                       || type.GetInterfaces().Contains(typeof(IList<>));
            }

            return false;
        }
    }
}
