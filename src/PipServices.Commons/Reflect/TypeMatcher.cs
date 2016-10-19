using System;
using System.Collections;

namespace PipServices.Commons.Reflect
{
    public static class TypeMatcher
    {
        public static bool Match(object expectedType, object actualValue)
        {
            if (expectedType == null)
                return true;

            Type actualType = actualValue.GetType();

            if (expectedType is Type)
                // Todo: IsAssignable is not supported
                return ((Type)expectedType).Equals(actualType);

            if (expectedType.Equals(actualType))
                return true;

            if (expectedType is string)
                return MatchByName((string)expectedType, actualType);

            return false;
        }

        public static bool MatchByName(string expectedType, object actualValue)
        {
            Type actualType = actualValue.GetType();
            expectedType = expectedType.ToLower();

            if (actualType.Name.Equals(expectedType, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (expectedType == "object")
            {
                return true;
            }
            else if (expectedType == "int" || expectedType == "integer")
            {
                return (actualValue is int)
                    || (actualValue is int?)
                    || (actualValue is long)
                    || (actualValue is long?);
            }
            else if (expectedType == "long")
            {
                return (actualValue is long);
            }
            else if (expectedType == "float")
            {
                return (actualValue is float)
                    || (actualValue is float?)
                    || (actualValue is double)
                    || (actualValue is double?)
                    || (actualValue is decimal)
                    || (actualValue is decimal?);
            }
            else if (expectedType == "double")
            {
                return (actualValue is double)
                    || (actualValue is double?)
                    || (actualValue is decimal)
                    || (actualValue is decimal?);
            }
            else if (expectedType == "string")
            {
                return (actualValue is string);
            }
            else if (expectedType == "date" || expectedType == "datetime")
            {
                return (actualValue is DateTime)
                    || (actualValue is DateTime?)
                    || (actualValue is DateTimeOffset)
                    || (actualValue is DateTimeOffset?);
            }
            else if (expectedType == "timespan" || expectedType == "duration")
            {
                return (actualValue is TimeSpan)
                    || (actualValue is TimeSpan?);
            }
            else if (expectedType == "map" || expectedType == "dict" || expectedType == "dictionary")
            {
                return actualValue is IDictionary;
            }
            else if (expectedType == "array" || expectedType == "list" || expectedType.EndsWith("[]"))
            {
                return actualValue is IEnumerable;
            }
            else
            {
                return false;
            }
        }
    }
}
