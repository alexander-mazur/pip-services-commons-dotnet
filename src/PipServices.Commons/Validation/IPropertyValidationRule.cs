﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipServices.Commons.Validation
{
    public interface IPropertyValidationRule
    {
        /// <summary>
        ///     Validates object property according to the schema and the rule.
        /// </summary>
        /// <param name="schema">a property schema this rule belongs to</param>
        /// <param name="value">the property value to be validated.</param>
        /// <returns>a list of validation errors or empty list if validation passed.</returns>
        IList<ValidationException> Validate(PropertySchema schema, object value);
    }
}