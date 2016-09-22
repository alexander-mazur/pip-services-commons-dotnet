using System;
using System.Collections.Generic;
using PipServices.Commons.Convert;
using PipServices.Commons.Data;

namespace PipServices.Commons.Config
{
    public class ConfigParams : StringValueMap
    {
        public ConfigParams() { }

        public ConfigParams(IDictionary<string, string> content)
            : base(content)
        { }

        public ConfigParams(params object[] values)
        {
            SetTuples(values);
        }

        public override string Get(string key)
        {
            string value = TryGet(key);
            if (value == null)
                throw new NullReferenceException("Configuration parameter " + key + " is not defined");
            return value;
        }

        public ConfigParams Merge(ConfigParams anotherConfig)
        {
            var result = new ConfigParams(this);

            if (anotherConfig != null)
            {
                foreach (var entry in anotherConfig)
                {
                    result.Set(entry.Key, entry.Value);
                }
            }

            return result;
        }

        private bool IsShadow(string name)
        {
            return string.IsNullOrWhiteSpace(name) || name.StartsWith("#") || name.StartsWith("!");
        }

        public void AddSection(string section, IDictionary<string, string> content)
        {
            // "Shadow" section names starts with # or !
            section = IsShadow(section) ? string.Empty : section;

            foreach (var entry in content)
            {
                // Shadow key names
                var key = IsShadow(entry.Key) ? string.Empty : entry.Key;

                if (!string.IsNullOrWhiteSpace(section) && !string.IsNullOrWhiteSpace(key))
                    key = section + "." + key;
                else if (string.IsNullOrWhiteSpace(key))
                    key = section;

                Set(key, entry.Value);
            }
        }

        public ConfigParams GetSection(string section)
        {
            var result = new ConfigParams();
            var prefix = section + ".";

            foreach (var entry in this)
            {
                if (entry.Key.StartsWith(prefix))
                {
                    var key = entry.Key.Substring(prefix.Length);
                    var value = entry.Value;
                    result.Add(key, value);
                }
            }

            return result;
        }

        public override void Set(string key, object value)
        {
            string oldValue = TryGet(key);
            string newValue = ValueConverter.ToNullableString(value);

            // Override only if previous value is empty or the new is non-empty
            if (string.IsNullOrWhiteSpace(oldValue) || !string.IsNullOrWhiteSpace(newValue))
                this[key] = newValue;
        }
    }
}