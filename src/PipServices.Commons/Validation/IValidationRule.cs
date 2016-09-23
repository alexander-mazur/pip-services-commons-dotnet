using System.Collections.Generic;

namespace PipServices.Commons.Validation
{
    public interface IValidationRule
    {
        /// <summary>
        /// Validates object according to the schema and the rule.
        /// </summary>
        /// <param name="schema">an object schema this rule belongs to</param>
        /// <param name="value">the object value to be validated.</param>
        /// <returns>a list of validation errors or empty list if validation passed.</returns>
        IList<ValidationException> Validate(Schema schema, object value);
    }
}
