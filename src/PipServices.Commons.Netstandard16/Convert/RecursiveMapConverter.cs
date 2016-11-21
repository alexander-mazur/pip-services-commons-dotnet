﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace PipServices.Commons.Convert
{
    public class RecursiveMapConverter
    {
        private static IDictionary<string, object> ObjectToMap(object value)
        {
            if (value == null) return null;

            var result = new Dictionary<string, object>();
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(value))
            {
                var propValue = prop.GetValue(value);

                // Recursive conversion
                propValue = ValueToMap(propValue);

                result.Add(prop.Name, propValue);
            }
            return result;
        }

        private static object[] ArrayToMap(IEnumerable<object> value)
        {
            var result = value as object[] ?? value.ToArray();

            for (var index = 0; index < result.Length; index++)
                result[index] = ValueToMap(result[index]);

            return result;
        }

        private static IDictionary<string, object> MapToMap(IDictionary<string, object> value)
        {
            var result = new Dictionary<string, object>();

            foreach (var key in value.Keys)
                result[key] = ValueToMap(value[key]);

            return result;
        }

        private static IDictionary<string, object> ObjectMapToMap(IDictionary<object, object> value)
        {
            var result = new Dictionary<string, object>();

            foreach (string key in value.Keys)
                result[key] = ValueToMap(value[key]);

            return result;
        }

        private static object ExtensionToMap(object value)
        {
            if (value == null) return null;

            var valueType = value.GetType().Name;

            // TODO: .NET Core does not support ExtensionDataObject
            // Convert extension objects
#if !CORE_NET
            if (valueType == "ExtensionDataObject")
            {
                var extResult = new Dictionary<string, object>();

                var membersProperty = typeof(ExtensionDataObject).GetProperty(
                    "Members", BindingFlags.NonPublic | BindingFlags.Instance);
                var members = (IList)membersProperty.GetValue(value, null);

                foreach (var member in members)
                {
                    var memberNameProperty = member.GetType().GetProperty("Name");
                    var memberName = (string)memberNameProperty.GetValue(member, null);

                    var memberValueProperty = member.GetType().GetProperty("Value");
                    var memberValue = memberValueProperty.GetValue(member, null);
                    memberValue = ExtensionToMap(memberValue);

                    extResult.Add(memberName, memberValue);
                }

                return extResult;
            }
#endif

            // Convert classes
            if (valueType.StartsWith("ClassDataNode"))
            {
                var classResult = new Dictionary<string, object>();

                var membersProperty = value.GetType().GetTypeInfo().GetProperty(
                    "Members", BindingFlags.NonPublic | BindingFlags.Instance);
                var members = (IList)membersProperty.GetValue(value, null);

                foreach (var member in members)
                {
                    var memberNameProperty = member.GetType().GetTypeInfo().GetProperty("Name");
                    var memberName = (string)memberNameProperty.GetValue(member, null);

                    var memberValueProperty = member.GetType().GetTypeInfo().GetProperty("Value");
                    var memberValue = memberValueProperty.GetValue(member, null);
                    memberValue = ExtensionToMap(memberValue);

                    classResult.Add(memberName, memberValue);
                }

                return classResult;
            }

            // Convert collections and arrays
            if (valueType.StartsWith("CollectionDataNode"))
            {
                var itemsProperty = value.GetType().GetTypeInfo().GetProperty(
                    "Items", BindingFlags.NonPublic | BindingFlags.Instance);
                var items = (IList)itemsProperty.GetValue(value, null);

                var arrayResult = new object[items.Count];

                for (var index = 0; index < items.Count; index++)
                    arrayResult[index] = ExtensionToMap(items[index]);

                return arrayResult;
            }

            // Convert values
            if (valueType.StartsWith("DataNode"))
            {
                var dataValueProperty = value.GetType().GetTypeInfo().GetProperty("Value");
                var valueResult = dataValueProperty.GetValue(value, null);
                valueResult = ExtensionToMap(valueResult);
                return valueResult;
            }

            return value;
        }

        private static object ValueToMap(object value)
        {
            if (value == null) return null;

            // Skip expected non-primitive values
            if (value is string || value is Type) return value;

            var valueType = value.GetType().GetTypeInfo();

            // Skip primitive values
            if (valueType.IsPrimitive || valueType.IsValueType) return value;
            // Skip Json.Net values
            if (valueType.Name == "JValue") return value;

            if (value is IDictionary<string, object>)
                return MapToMap((IDictionary<string, object>)value);

            if (value is IDictionary<object, object>)
                return ObjectMapToMap((IDictionary<object, object>)value);

            // Convert arrays
            if (value is IEnumerable<object> && valueType.Name != "JObject")
                return ArrayToMap((IEnumerable<object>)value);

            // TODO: .NET Core does not support ExtensionDataObject
            // Convert partial updates
#if !CORE_NET
            if (value is IExtensibleDataObject)
                return ExtensionToMap(((IExtensibleDataObject)value).ExtensionData);
#endif

            return ObjectToMap(value);
        }

        public static IDictionary<string, object> ToNullableMap(object value)
        {
            return ValueToMap(value) as IDictionary<string, object>;
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