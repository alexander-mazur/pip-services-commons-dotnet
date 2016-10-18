using System.Linq;
using System.Collections.Generic;

namespace PipServices.Commons.Validate
{
    public class Schema
    {
        public Schema() { }

        public Schema(bool required, List<IValidationRule> rules)
        {
            IsRequired = required;
            Rules = rules;
        }

        public bool IsRequired { get; set; }

        public List<IValidationRule> Rules { get; set; }

        public Schema MakeRequired()
        {
            IsRequired = true;
            return this;
        }

        public Schema MakeOptional()
        {
            IsRequired = false;
            return this;
        }

        public Schema WithRule(IValidationRule rule)
        {
            Rules = Rules ?? new List<IValidationRule>();
            Rules.Add(rule);
            return this;
        }

        protected internal virtual void PerformValidation(string path, object value, List<ValidationResult> results)
        {
            if (value == null)
            {
                // Check for required values
                if (IsRequired)
                    results.Add(
                        new ValidationResult(
                            path,
                            ValidationResultType.Error,
                            "VALUE_IS_NULL",
                            "value cannot be null",
                            "NOT NULL",
                            null
                        )
                    );
            }
            else
            {
                // Check validation rules
                if (Rules != null)
                {
                    foreach (var rule in Rules)
                        rule.Validate(path, this, value, results);
                }
            }
        }

        protected void PerformTypeValidation(string path, object type, object value, List<ValidationResult> results)
        {
            // If type it not defined then skip
            if (type == null) return;

            // Perform validation against schema
            var schema = type as Schema;
            if (schema != null)
            {
                schema.PerformValidation(path, value, results);
                return;
            }

            // If value is null then skip
            if (value == null) return;

            var valueType = value.GetType();

            // Match types
            if (valueType.Equals(type)) return;
            if (valueType.Name.Equals(type)) return;

            // Generate type mismatch error
            results.Add(
                new ValidationResult(
                    path,
                    ValidationResultType.Error,
                    "TYPE_MISMATCH",
                    "Expected type " + type + " but found " + valueType.Name,
                    type,
                    valueType.Name
                )
            );
        }

        public List<ValidationResult> Validate(object value)
        {
            var results = new List<ValidationResult>();
            PerformValidation("", value, results);
            return results;
        }

        public void ValidateAndThrowException(string correlationId, object value, bool strict = false)
        {
            var results = Validate(value);
            var hasErrors = results.Any(r => r.Type == ValidationResultType.Error);

            if (hasErrors)
                throw new ValidationException(correlationId, results);

            if (strict)
            {
                var hasWarnings = results.Any(r => r.Type == ValidationResultType.Warning);
                throw new ValidationException(correlationId, results);
            }
        }
    }
}