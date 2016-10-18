using System.Collections;
using System.Collections.Generic;

namespace PipServices.Commons.Validate
{
    public class MapSchema : Schema
    {
        public MapSchema() { }

        public MapSchema(object keyType, object valueType)
        {
            KeyType = keyType;
            ValueType = valueType;
        }

        public object KeyType { get; set; }
        public object ValueType { get; set; }

        protected internal override void PerformValidation(string path, object value, List<ValidationResult> results)
        {
            base.PerformValidation(path, value, results);

            if (value == null) return;

            if (value is IDictionary)
            {
                IDictionary map = (IDictionary)value;
                foreach (var key in map.Keys)
                {
                    string elementPath = string.IsNullOrWhiteSpace(path)
                        ? key.ToString() : path + "." + key;

                    PerformTypeValidation(elementPath, KeyType, key, results);
                    PerformTypeValidation(elementPath, ValueType, map[key], results);
                }
            }
            else
            {
                results.Add(
                    new ValidationResult(
                        path,
                        ValidationResultType.Error,
                        "VALUE_ISNOT_MAP",
                        "Value type is expected to be Dictionary",
                        typeof(IDictionary),
                        value.GetType()
                    )
                );
            }
        }
    }
}
