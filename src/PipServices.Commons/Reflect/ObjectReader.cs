using Newtonsoft.Json.Linq;
using PipServices.Commons.Convert;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PipServices.Commons.Reflect
{
    public static class ObjectReader
    {
        public static object GetValue(object obj)
        {
            if (obj is JValue)
            {
                var value = (JValue)obj;
                return value.Value;
            }
            else if (obj is JObject)
            {
                var thisObj = (JObject)obj;
                var map = new Dictionary<string, object>();
                foreach (var property in thisObj.Properties())
                    map[property.Name] = property.Value;
                return map;
            }
            else if (obj is JArray)
            {
                var thisObj = (JArray)obj;
                var list = new List<object>();
                foreach (var element in thisObj)
                    list.Add(element);
                return list;
            }
            else
            {
                return obj;
            }
        }

        public static bool HasProperty(object obj, string name)
        {
            if (obj == null)
                throw new NullReferenceException("Object cannot be null");
            if (name == null)
                throw new NullReferenceException("Property name cannot be null");

            if (obj is JObject)
            {
                var thisObj = (JObject)obj;
                foreach (var property in thisObj.Properties())
                {
                    if (name.Equals(property.Name, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                return false;
            }
            else if (obj is JArray)
            {
                var list = (JArray)obj;
                var index = IntegerConverter.ToNullableInteger(name);
                return index >= 0 && index < list.Count;
            }
            else if (obj is IDictionary)
            {
                var map = (IDictionary)obj;
                foreach (var key in map.Keys)
                {
                    if (name.Equals(key.ToString(), StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                return false;
            }
            else if (obj is IList)
            {
                var index = IntegerConverter.ToNullableInteger(name);
                var list = (IList)obj;
                return index >= 0 && index < list.Count;
            }
            else if (obj.GetType().IsArray)
            {
                var index = IntegerConverter.ToNullableInteger(name);
                if (index >= 0)
                {
                    var list = (IEnumerable)obj;
                    foreach (var value in list)
                    {
                        if (index == 0)
                            return true;
                        index--;
                    }
                }
                return false;
            }
            else
            {
                return PropertyReflector.HasProperty(obj, name);
            }
        }

        public static object GetProperty(object obj, string name)
        {
            if (obj == null)
                throw new NullReferenceException("Object cannot be null");
            if (name == null)
                throw new NullReferenceException("Property name cannot be null");

            if (obj is JObject)
            {
                var thisObj = (JObject)obj;
                foreach (var property in thisObj.Properties())
                {
                    if (name.Equals(property.Name, StringComparison.OrdinalIgnoreCase))
                        return property.Value;
                }
                return null;
            }
            else if (obj is JArray)
            {
                var list = (JArray)obj;
                var index = IntegerConverter.ToNullableInteger(name);
                return index >= 0 && index < list.Count ? list[index.Value] : null;
            }
            else if (obj is IDictionary)
            {
                var map = (IDictionary)obj;
                foreach (var key in map.Keys)
                {
                    if (name.Equals(key.ToString(), StringComparison.OrdinalIgnoreCase))
                        return map[key];
                }
                return null;
            }
            else if (obj is IList)
            {
                var index = IntegerConverter.ToNullableInteger(name);
                var list = (IList)obj;
                return index >= 0 && index < list.Count ? list[index.Value] : null;
            }
            else if (obj.GetType().IsArray)
            {
                var index = IntegerConverter.ToNullableInteger(name);
                if (index >= 0)
                {
                    var list = (IEnumerable)obj;
                    foreach (var value in list)
                    {
                        if (index == 0)
                            return value;
                        index--;
                    }
                }
                return null;
            }
            else
            {
                return PropertyReflector.GetProperty(obj, name);
            }
        }

        public static List<string> GetPropertyNames(object obj)
        {
            if (obj == null)
                throw new NullReferenceException("Object cannot be null");

            List<string> properties = new List<string>();

            if (obj is JObject)
            {
                var thisObj = (JObject)obj;
                foreach (var property in thisObj.Properties())
                {
                    properties.Add(property.Name);
                }
            }
            else if (obj is JArray)
            {
                var list = (JArray)obj;
                for (int index = 0; index < list.Count; index++)
                {
                    properties.Add(index.ToString());
                }
            }
            else if (obj is IDictionary)
            {
                var map = (IDictionary)obj;
                foreach (var key in map.Keys)
                {
                    properties.Add(key.ToString());
                }
            }
            else if (obj is IList)
            {
                var list = (IList)obj;
                for (int index = 0; index < list.Count; index++)
                {
                    properties.Add(index.ToString());
                }
            }
            else if (obj.GetType().IsArray)
            {
                var list = (IEnumerable)obj;
                var index = 0;
                foreach (var value in list)
                {
                    properties.Add(index.ToString());
                    index++;
                }
            }
            else
            {
                return PropertyReflector.GetPropertyNames(obj);
            }

            return properties;
        }

        public static Dictionary<string, object> GetProperties(object obj)
        {
            if (obj == null)
                throw new NullReferenceException("Object cannot be null");

            Dictionary<string, object> map = new Dictionary<string, object>();

            if (obj is JObject)
            {
                var thisObj = (JObject)obj;
                foreach (var property in thisObj.Properties())
                {
                    map[property.Name] = property.Value;
                }
            }
            else if (obj is JArray)
            {
                var list = (JArray)obj;
                for (int index = 0; index < list.Count; index++)
                {
                    map[index.ToString()] = list[index];
                }
            }
            else if (obj is IDictionary)
            {
                var thisMap = (IDictionary)obj;
                foreach (var key in thisMap.Keys)
                {
                    map[key.ToString()] = thisMap[key];
                }
            }
            else if (obj is IList)
            {
                var list = (IList)obj;
                for (int index = 0; index < list.Count; index++)
                {
                    map[index.ToString()] = list[index];
                }
            }
            else if (obj.GetType().IsArray)
            {
                var list = (IEnumerable)obj;
                var index = 0;
                foreach (var value in list)
                {
                    map[index.ToString()] = value;
                    index++;
                }
            }
            else
            {
                return PropertyReflector.GetProperties(obj);
            }

            return map;
        }
    }
}
