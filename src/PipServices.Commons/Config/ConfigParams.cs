using System;
using PipServices.Commons.Data;
using System.Collections.Generic;
using PipServices.Commons.Convert;
using PipServices.Commons.Reflect;

namespace PipServices.Commons.Config
{
    // Todo: FromObject shall be recursive (match java)

    /// <summary>
    /// Map with configuration parameters that use complex keys with dot notation and simple string values.
    /// </summary>
    public class ConfigParams : StringValueMap
    {
        /// <summary>
        /// Creates an instance of ConfigParams.
        /// </summary>
        public ConfigParams()
        { }

        /// <summary>
        /// Creates an instance of ConfigParams.
        /// </summary>
        /// <param name="content">Existing map to copy keys/values from.</param>
        public ConfigParams(IDictionary<string, object> content)
            : base(content)
        { }

        public IEnumerable<string> GetSectionNames()
        {
            var sections = new List<string>();

            foreach (var key in Keys)
            {
                var pos = key.IndexOf('.');

                var subKey = (pos > 0) ? key.Substring(0, pos) : key;

                // Perform case sensitive search
                var found = false;
                foreach (var section in sections)
                {
                    if (!section.Equals(subKey, StringComparison.CurrentCultureIgnoreCase))
                        continue;

                    found = true;
                    break;
                }

                if (!found)
                    sections.Add(subKey);
            }

            return sections;
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

        protected bool IsShadowName(string name)
        {
            return string.IsNullOrWhiteSpace(name) || name.StartsWith("#") || name.StartsWith("!");
        }

        public void AddSection(string section, ConfigParams sectionParams)
        {
            // "Shadow" section names starts with # or !
            section = IsShadowName(section) ? string.Empty : section;

            foreach (var entry in sectionParams)
            {
                // Shadow key names
                var key = IsShadowName(entry.Key) ? string.Empty : entry.Key;

                if (!string.IsNullOrWhiteSpace(section) && !string.IsNullOrWhiteSpace(key))
                {
                    key = section + "." + key;
                }
                else if (string.IsNullOrWhiteSpace(key))
                {
                    key = section;
                }

                this[key] = entry.Value;
            }
        }

        public ConfigParams Override(ConfigParams configParams)
        {
            var map = StringValueMap.FromMaps(this, configParams);
            return new ConfigParams(map);
        }


        public ConfigParams SetDefaults(ConfigParams defaultConfigParams)
        {
            var map = StringValueMap.FromMaps(defaultConfigParams, this);
            return new ConfigParams(map);
        }

        public new static ConfigParams FromTuples(params object[] tuples)
        {
            var map = StringValueMap.FromTuples(tuples);
            return new ConfigParams(map);
        }

        public static ConfigParams FromValue(object value)
        {
            var map = RecursiveObjectReader.GetProperties(value);
            return new ConfigParams(map);
        }

        public new static ConfigParams FromString(string line)
        {
            var map = StringValueMap.FromString(line);
            return new ConfigParams(map);
        }

        public static ConfigParams MergeConfigs(params IDictionary<string, object>[] configs)
        {
            var map = FromMaps(configs);
            return new ConfigParams(map);
        }

        public static ConfigParams FromObject(object value)
        {
            var map = MapConverter.ToMap(value);
            return new ConfigParams(map);
        }
    }
}