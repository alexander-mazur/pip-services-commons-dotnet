using System;
using System.Collections.Generic;
using PipServices.Commons.Convert;
using PipServices.Commons.Data;

namespace PipServices.Commons.Config
{
    /// <summary>
    /// Map with configuration parameters that use complex keys with dot notation and simple string values.
    /// </summary>
    public class ConfigParams : StringValueMap
    {
        /// <summary>
        /// Creates an instance of ConfigParams.
        /// </summary>
        public ConfigParams() { }

        /// <summary>
        /// Creates an instance of ConfigParams.
        /// </summary>
        /// <param name="content">Existing map to copy keys/values from.</param>
        public ConfigParams(IDictionary<string, string> content)
            : base(content)
        { }

        /// <summary>
        /// Gets a config parameter with the given key.
        /// </summary>
        /// <param name="key">The key of the config parameter.</param>
        /// <returns>A </returns>
        public override string Get(string key)
        {
            string value = TryGet(key);
            if (value == null)
                throw new NullReferenceException("Configuration parameter " + key + " is not defined");
            return value;
        }


        public override void Set(string key, object value)
        {
            string oldValue = TryGet(key);
            string newValue = StringConverter.ToNullableString(value);

            // Override only if previous value is empty or the new is non-empty
            if (string.IsNullOrWhiteSpace(oldValue) || !string.IsNullOrWhiteSpace(newValue))
                this[key] = newValue;
        }

        protected bool IsShadowName(string name)
        {
            return string.IsNullOrWhiteSpace(name) || name.StartsWith("#") || name.StartsWith("!");
        }

        public void AddSection(string section, IDictionary<string, string> content)
        {
            // "Shadow" section names starts with # or !
            section = IsShadowName(section) ? string.Empty : section;

            foreach (var entry in content)
            {
                // Shadow key names
                var key = IsShadowName(entry.Key) ? string.Empty : entry.Key;

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

        public ConfigParams Override(ConfigParams configParams)
        {
            throw new NotImplementedException();
        }


        public ConfigParams SetDefaults(ConfigParams defaultConfigParams)
        {
            throw new NotImplementedException();
        }

        public ConfigParams FromTuples(params object[] tuples)
        {
            throw new NotImplementedException();
        }

        public ConfigParams FromString(string line)
        {
            throw new NotImplementedException();
        }

        public ConfigParams MergeConfigs(ConfigParams config)
        {
            var result = new ConfigParams(this);

            if (config != null)
            {
                foreach (var entry in config)
                {
                    result.Set(entry.Key, entry.Value);
                }
            }

            return result;
        }
    }
}