using System.Collections.Generic;

namespace PipServices.Commons.Validate
{
    public class OrRule : IValidationRule
    {
        private IValidationRule[] _rules;

        public OrRule(params IValidationRule[] rules)
        {
            _rules = rules;
        }

        public void Validate(string path, Schema schema, object value, List<ValidationResult> results)
        {
            if (_rules == null) return;

            List<ValidationResult> localResults = new List<ValidationResult>();

            foreach (IValidationRule rule in _rules)
                rule.Validate(path, schema, value, localResults);

            if (localResults.Count == _rules.Length)
                results.AddRange(localResults);
        }
    }
}
