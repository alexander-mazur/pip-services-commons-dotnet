using PipServices.Commons.Reflect;
using System;
using System.Collections.Generic;

namespace PipServices.Commons.Validate
{
    public class ObjectSchema : Schema
    {
        private List<PropertySchema> _properties;
        private bool _allowUndefined = false;

        public ObjectSchema() { }

        public List<PropertySchema> Properties
        {
            get { return _properties; }
            set { _properties = value; }
        }

        public bool IsUndefinedAllowed
        {
            get { return _allowUndefined; }
            set { _allowUndefined = value; }
        }

        public ObjectSchema AllowUndefined(bool value)
        {
            _allowUndefined = value;
            return this;
        }

        public ObjectSchema WithProperty(PropertySchema schema)
        {
            _properties = _properties ?? new List<PropertySchema>();
            _properties.Add(schema);
            return this;
        }

        public ObjectSchema WithRequiredProperty(string name, object type, params IValidationRule[] rules)
        {
            _properties = _properties ?? new List<PropertySchema>();
            PropertySchema schema = new PropertySchema(name, type);
            schema.MakeRequired();
            return WithProperty(schema);
        }

        public ObjectSchema WithOptionalProperty(string name, object type, params IValidationRule[] rules)
        {
            _properties = _properties ?? new List<PropertySchema>();
            PropertySchema schema = new PropertySchema(name, type);
            schema.MakeOptional();
            return WithProperty(schema);
        }

        protected internal override void PerformValidation(string path, object value, List<ValidationResult> results)
        {
            base.PerformValidation(path, value, results);

            if (value == null) return;

            Dictionary<string, object> properties = PropertyReflector.GetProperties(value);

            // Process defined properties
            if (Properties != null)
            {
                foreach (var propertySchema in Properties)
                {
                    string processedName = null;

                    foreach (var entry in properties)
                    {
                        string propertyName = entry.Key;
                        object propertyValue = entry.Value;
                        // Find properties case insensitive
                        if (propertyName.Equals(propertySchema.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            propertySchema.PerformValidation(path, propertyValue, results);
                            processedName = propertyName;
                            break;
                        }
                    }

                    if (processedName == null)
                        propertySchema.PerformValidation(path, null, results);
                    else
                        properties.Remove(processedName);
                }
            }

            // Process unexpected properties
            foreach (var entry in properties)
            {
                string propertyPath = string.IsNullOrWhiteSpace(path)
                    ? entry.Key : path + "." + entry.Key;

                results.Add(
                    new ValidationResult(
                        propertyPath,
                        ValidationResultType.Warning,
                        "UNEXPECTED_PROPERTY",
                        "Found unexpected property " + entry.Key,
                        null,
                        entry.Key
                    )
                );
            }
        }
    }
}
