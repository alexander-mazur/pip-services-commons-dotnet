using System;
using System.Collections.Generic;
using System.Collections;
using PipServices.Commons.Data;
using PipServices.Commons.Convert;
using PipServices.Commons.Reflect;
using PipServices.Commons.Config;

namespace PipServices.Commons.Run
{
    public class Parameters : AnyValueMap
    {
        public Parameters()
        { }

        public Parameters(IDictionary values) : base(values)
        {
        }

        private static object GetCaseInsensitive(IDictionary map, string name)
        {
            foreach (var key in map.Keys)
            {
                if (string.Compare(key.ToString(), name, true) == 0)
                {
                    return map[key];
                }
            }
            return null;
        }

        public override object Get(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            var names = path.Split(new[] { "\\." }, StringSplitOptions.None);
            if (names == null || names.Length == 0)
            {
                return null;
            }
            if (names.Length == 1)
            {
                return GetCaseInsensitive(this, names[0]);
            }

            object result = this;
            foreach (var name in names)
            {
                if (result is IDictionary)
                {
                    result = GetCaseInsensitive((IDictionary)result, name);
                    if (result == null)
                    {
                        return null;
                    }
                }
                else if (result is IList)
                {
                    var list = (IList)result;
                    var index = IntegerConverter.ToNullableInteger(name);
                    if (index == null || index < 0 || index >= list.Count)
                    {
                        return null;
                    }
                    result = list[index.Value];
                }
            }

            return result;
        }

        public new object Add(string path, object value)
        {
            if (path == null) return null;

            var names = path.Split(new[] { "\\." }, StringSplitOptions.None);
            if (names == null || names.Length == 0)
            {
                return null;
            }
            if (names.Length == 1)
            {
                foreach (var key in Keys)
                {
                    if (string.Compare(key, names[0], true) == 0)
                    {
                        names[0] = key;
                        break;
                    }
                }
                base.Add(names[0], value);
                return value;
            }
            object container = this;
            for (var i = 0; i < names.Length - 1; i++)
            {
                var name = names[i];
                if (container is IDictionary)
                {
                    var mapContainer = (IDictionary)container;
                    container = GetCaseInsensitive(mapContainer, name);

                    if (container == null)
                    {
                        container = new Dictionary<string, object>();
                        mapContainer[name] = container;
                    }
                }
                else if (container is IList)
                {
                    var list = (IList)container;
                    var index = IntegerConverter.ToNullableInteger(name);

                    while (index != null && index.Value >= list.Count)
                    {
                        list.Add(null);
                    }

                    if (index != null && index.Value >= 0 && index.Value < list.Count)
                    {
                        container = list[index.Value];
                        if (container == null)
                        {
                            container = new Dictionary<string, object>();
                            list[index.Value] = container;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            if (!(container is IDictionary))
            {
                return null;
            }

            var map = (IDictionary<string, object>)container;
            map[names[names.Length - 1]] = value;
            return value;
        }

        public Parameters GetAsNullableParameters(string key)
        {
            var value = GetAsNullableMap(key);
            return value != null ? new Parameters(value) : null;
        }

        public Parameters GetAsParameters(string key)
        {
            var value = GetAsMap(key);
            return new Parameters(value);
        }

        public Parameters GetAsParametersWithDefault(string key, Parameters defaultValue)
        {
            var result = GetAsNullableParameters(key);
            return result ?? defaultValue;
        }

        public bool Contains(string key)
        {
            var path = StringConverter.ToNullableString(key);
            return Get(path) != null;
        }

        public Parameters Override(Parameters parameters)
        {
            return Override(parameters, false);
        }

        public Parameters Override(Parameters parameters, bool recursive)
        {
            return Parameters.Merge(new Parameters(parameters), this, recursive);
        }

        public static Parameters Merge(Parameters destination, IDictionary source, bool recursive)
        {
            if (destination == null) destination = new Parameters();
            if (source == null) return destination;
            foreach(var key in source.Keys)
            {
                var keyStr = StringConverter.ToString(key);
                if (destination.ContainsKey(keyStr))
                {
                    var configValue = destination.Get(keyStr);
                    var defaultValue = source[key];

                    if(recursive && configValue is IDictionary && defaultValue is IDictionary)
                    {
                        destination[keyStr] = Merge(new Parameters((IDictionary)configValue), (IDictionary)defaultValue, recursive);
                    }
                }
                else
                {
                    destination[keyStr] = source[key];
                }
            }
            return destination;
        }

        public Parameters SetDefaults(Parameters defaultParameters)
        {
            return SetDefaults(defaultParameters, false);
        }

        public Parameters SetDefaults(Parameters defaultParameters, bool recursive)
        {
            return Parameters.Merge(new Parameters(this), defaultParameters, recursive);
        }

        public void AssignTo(object value)
        {
            if (value == null || Count == 0) return;
            PropertyReflector.SetProperties(value, this);
        }

        public Parameters Pick(params string[] paths)
        {
            var result = new Parameters();
            foreach(var path in paths)
            {
                if (ContainsKey(path))
                {
                    result[path] = Get(path);
                }
            }
            return result;
        }

        public Parameters Omit(params string[] paths)
        {
            var result = new Parameters();
            foreach (var path in paths)
            {
                result.Remove(path);
            }
            return result;
        }

        public string ToJson()
        {
            return JsonConverter.ToJson(this);
        }

        public static new Parameters FromTuples(params object[] tuples)
        {
            return new Parameters(AnyValueMap.FromTuples(tuples));
        }

        public static Parameters MergeParams(params Parameters[] parameters)
        {
            return new Parameters(AnyValueMap.FromMaps(parameters));
        }

        public static Parameters FromJson(string json)
        {
            var map = JsonConverter.ToNullableMap(json);
            return new Parameters((IDictionary)map);
        }

        public static Parameters FromConfig(ConfigParams config)
        {
            return new Parameters(config);
        }
    }
}