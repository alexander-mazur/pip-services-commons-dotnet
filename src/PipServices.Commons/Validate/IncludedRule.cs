using System.Collections.Generic;

namespace PipServices.Commons.Validate
{
    public class IncludedRule : IValidationRule
    {
        public object[] _values;

        public IncludedRule(params object[] values)
        {
            _values = values;
        }

        public void Validate(string path, Schema schema, object value, List<ValidationResult> results)
        {
            bool found = false;
            foreach (var thisValue in _values)
            {
                if (thisValue != null && thisValue.Equals(value))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                results.Add(
                    new ValidationResult(
                        path,
                        ValidationResultType.Error,
                        "VALUE_NOT_INCLUDED",
                        "Value shall be one of " + _values,
                        _values,
                        value
                    )
                );
            }
        }
    }
}
