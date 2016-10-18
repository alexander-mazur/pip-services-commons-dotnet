using System.Collections;
using System.Collections.Generic;

namespace PipServices.Commons.Validate
{
    public class ArraySchema : Schema
    {
        public ArraySchema() { }

        public ArraySchema(object valueType)
        {
            ValueType = valueType;
        }

        public object ValueType { get; set; }

        protected internal override void PerformValidation(string path, object value, List<ValidationResult> results)
        {
            base.PerformValidation(path, value, results);

            if (value == null) return;

            var list = value as IEnumerable;
            if (list != null)
            {
                var index = 0;
                foreach (var element in list)
                {
                    var elementPath = string.IsNullOrWhiteSpace(path)
                        ? index.ToString() : path + "." + index;
                    PerformTypeValidation(elementPath, ValueType, element, results);
                    index++;
                }
            }
            else
            {
                results.Add(
                    new ValidationResult(
                        path,
                        ValidationResultType.Error,
                        "VALUE_ISNOT_ARRAY",
                        "Value type is expected to be List or array",
                        typeof(IEnumerable),
                        value.GetType()
                    )
                );
            }
        }
    }
}
