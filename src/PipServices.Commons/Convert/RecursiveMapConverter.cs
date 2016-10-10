using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.ComponentModel;
using PipServices.Commons.Data;

namespace PipServices.Commons.Convert
{
    // Todo: Restore and complete the implementation. Make sure it converts NewtonSoft and ExtensionObject 

    //public class RecursiveMapConverter
    //{
    //    private static DynamicMap ObjectToMap(object value)
    //    {
    //        if (value == null) return null;

    //        var result = new DynamicMap();
    //        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(value))
    //        {
    //            var propValue = prop.GetValue(value);

    //            // Recursive conversion
    //            propValue = ValueToMap(propValue);

    //            result.Add(prop.Name, propValue);
    //        }
    //        return result;
    //    }

    //    private static object[] ArrayToMap(IEnumerable<object> value)
    //    {
    //        var result = value as object[] ?? value.ToArray();

    //        for (var index = 0; index < result.Length; index++)
    //            result[index] = ValueToMap(result[index]);

    //        return result;
    //    }

    //    private static DynamicMap MapToMap(IDictionary<string, object> value)
    //    {
    //        var result = new DynamicMap();

    //        foreach (var key in value.Keys)
    //            result[key] = ValueToMap(value[key]);

    //        return result;
    //    }

    //    private static DynamicMap ObjectMapToMap(IDictionary<object, object> value)
    //    {
    //        var result = new DynamicMap();

    //        foreach (string key in value.Keys)
    //            result[key] = ValueToMap(value[key]);

    //        return result;
    //    }

    //    private static object ExtensionToMap(object value)
    //    {
    //        if (value == null) return null;

    //        var valueType = value.GetType().Name;

    //        // TODO: .NET Core does not support ExtensionDataObject
    //        // Convert extension objects
    //        //if (valueType == "ExtensionDataObject")
    //        //{
    //        //    var extResult = new DynamicMap();

    //        //    var membersProperty = typeof(ExtensionDataObject).GetProperty(
    //        //        "Members", BindingFlags.NonPublic | BindingFlags.Instance);
    //        //    var members = (IList)membersProperty.GetValue(value, null);

    //        //    foreach (var member in members)
    //        //    {
    //        //        var memberNameProperty = member.GetType().GetProperty("Name");
    //        //        var memberName = (string)memberNameProperty.GetValue(member, null);

    //        //        var memberValueProperty = member.GetType().GetProperty("Value");
    //        //        var memberValue = memberValueProperty.GetValue(member, null);
    //        //        memberValue = ExtensionToMap(memberValue);

    //        //        extResult.Add(memberName, memberValue);
    //        //    }

    //        //    return extResult;
    //        //}

    //        // Convert classes
    //        if (valueType.StartsWith("ClassDataNode"))
    //        {
    //            var classResult = new DynamicMap();

    //            var membersProperty = value.GetType().GetTypeInfo().GetProperty(
    //                "Members", BindingFlags.NonPublic | BindingFlags.Instance);
    //            var members = (IList)membersProperty.GetValue(value, null);

    //            foreach (var member in members)
    //            {
    //                var memberNameProperty = member.GetType().GetTypeInfo().GetProperty("Name");
    //                var memberName = (string)memberNameProperty.GetValue(member, null);

    //                var memberValueProperty = member.GetType().GetTypeInfo().GetProperty("Value");
    //                var memberValue = memberValueProperty.GetValue(member, null);
    //                memberValue = ExtensionToMap(memberValue);

    //                classResult.Add(memberName, memberValue);
    //            }

    //            return classResult;
    //        }

    //        // Convert collections and arrays
    //        if (valueType.StartsWith("CollectionDataNode"))
    //        {
    //            var itemsProperty = value.GetType().GetTypeInfo().GetProperty(
    //                "Items", BindingFlags.NonPublic | BindingFlags.Instance);
    //            var items = (IList)itemsProperty.GetValue(value, null);

    //            var arrayResult = new object[items.Count];

    //            for (var index = 0; index < items.Count; index++)
    //                arrayResult[index] = ExtensionToMap(items[index]);

    //            return arrayResult;
    //        }

    //        // Convert values
    //        if (valueType.StartsWith("DataNode"))
    //        {
    //            var dataValueProperty = value.GetType().GetTypeInfo().GetProperty("Value");
    //            var valueResult = dataValueProperty.GetValue(value, null);
    //            valueResult = ExtensionToMap(valueResult);
    //            return valueResult;
    //        }

    //        return value;
    //    }

    //    private static object ValueToMap(object value)
    //    {
    //        if (value == null) return null;

    //        // Skip converted values
    //        if (value is DynamicMap) return value;

    //        // Skip expected non-primitive values
    //        if (value is string || value is Type) return value;

    //        var valueType = value.GetType().GetTypeInfo();

    //        // Skip primitive values
    //        if (valueType.IsPrimitive || valueType.IsValueType) return value;
    //        // Skip Json.Net values
    //        if (valueType.Name == "JValue") return value;

    //        if (value is IDictionary<string, object>)
    //            return MapToMap((IDictionary<string, object>)value);

    //        if (value is IDictionary<object, object>)
    //            return ObjectMapToMap((IDictionary<object, object>)value);

    //        // Convert arrays
    //        if (value is IEnumerable<object> && valueType.Name != "JObject")
    //            return ArrayToMap((IEnumerable<object>)value);

    //        // TODO: .NET Core does not support ExtensionDataObject
    //        // Convert partial updates
    //        //if (value is PartialUpdates)
    //        //    return ExtensionToMap(((PartialUpdates)value).ExtensionData);

    //        return ObjectToMap(value);
    //    }

    //    public static DynamicMap ToNullableMap(object value)
    //    {
    //        return ValueToMap(value) as DynamicMap;
    //    }

    //    public static DynamicMap ToMap(object value)
    //    {
    //        return ValueToMap(value) as DynamicMap ?? new DynamicMap();
    //    }

    //    public static DynamicMap ToMapWithDefault(object value, DynamicMap defaultValue = null)
    //    {
    //        return ValueToMap(value) as DynamicMap ?? defaultValue;
    //    }
    //}
}