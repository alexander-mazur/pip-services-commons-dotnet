﻿using System.Collections.Generic;

namespace PipServices.Commons.Validate
{
    public class AndRule : IValidationRule
    {
        private IValidationRule[] _rules;

        public AndRule(params IValidationRule[] rules)
        {
            _rules = rules;
        }

        public void Validate(string path, Schema schema, object value, List<ValidationResult> results)
        {
            if (_rules == null) return;

            foreach (IValidationRule rule in _rules)
                rule.Validate(path, schema, value, results);
        }

    }
}
