﻿using PipServices.Commons.Reflect;
using System.Collections.Generic;

namespace PipServices.Commons.Validate
{
    public class PropertiesComparisonRule : IValidationRule
    {
        private string _property1;
        private string _property2;
        private string _operation;

        public PropertiesComparisonRule(string property1, string operation, string property2)
        {
            _property1 = property1;
            _operation = operation;
            _property2 = property2;
        }

        public void Validate(string path, Schema schema, object value, List<ValidationResult> results)
        {
            object value1 = PropertyReflector.GetProperty(value, _property1);
            object value2 = PropertyReflector.GetProperty(value, _property2);

            if (!ObjectComparator.Compare(value1, _operation, value2))
            {
                results.Add(
                    new ValidationResult(
                        path,
                        ValidationResultType.Error,
                        "PROPERTIES_NOT_MATCH",
                        "Property " + _property1 + " is expected to " + _operation + " property " + _property2,
                        value2,
                        value1
                    )
                );
            }
        }
    }
}