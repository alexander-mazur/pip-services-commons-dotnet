﻿using PipServices.Commons.Reflect;
using System.Collections.Generic;

namespace PipServices.Commons.Validate
{
    public class AtLeastOneExistRule : IValidationRule
    {
        private readonly string[] _properties;

        public AtLeastOneExistRule(params string[] properties)
        {
            _properties = properties;
        }

        public void Validate(string path, Schema schema, object value, List<ValidationResult> results)
        {
            var found = new List<string>();
            foreach (var property in _properties)
            {
                var propertyValue = PropertyReflector.GetProperty(value, property);
                if (propertyValue != null)
                    found.Add(property);
            }

            if (found.Count == 0)
            {
                results.Add(
                    new ValidationResult(
                        path,
                        ValidationResultType.Error,
                        "VALUE_NULL",
                        "At least one property expected from " + _properties,
                        _properties,
                        null
                    )
                );
            }
        }
    }
}
