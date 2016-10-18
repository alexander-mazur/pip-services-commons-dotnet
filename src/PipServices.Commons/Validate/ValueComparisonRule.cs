using System.Collections.Generic;

namespace PipServices.Commons.Validate
{
    public class ValueComparisonRule : IValidationRule
    {
        private readonly string _operation;
        private readonly object _value;

        public ValueComparisonRule(string operation, object value)
        {
            _operation = operation;
            _value = value;
        }

        public void Validate(string path, Schema schema, object value, List<ValidationResult> results)
        {
            if (!ObjectComparator.Compare(value, _operation, _value))
            {
                results.Add(
                    new ValidationResult(
                        path,
                        ValidationResultType.Error,
                        "BAD_VALUE",
                        value + " is expected to " + _operation + " " + _value,
                        _operation + " " + _value,
                        value
                    )
                );
            }
        }
    }
}
