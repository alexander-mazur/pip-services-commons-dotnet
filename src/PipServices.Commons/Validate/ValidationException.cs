using PipServices.Commons.Errors;
using System.Collections.Generic;
using System.Text;

namespace PipServices.Commons.Validate
{
    public class ValidationException : BadRequestException
    {
        public ValidationException(string correlationId, List<ValidationResult> results):
            this(correlationId, ComposeMessage(results))
        {
            WithDetails("results", results);
        }

        public ValidationException(string correlationId, string message) :
            base(correlationId, "INVALID_DATA", message)
        {
        }

        public static string ComposeMessage(List<ValidationResult> results)
        {
            var builder = new StringBuilder();
            builder.Append("Validation failed");

            if (results != null && results.Count > 0)
            {
                var first = true;
                foreach (var result in results)
                {
                    if (result.Type != ValidationResultType.Information)
                    {
                        if (!first) builder.Append(": ");
                        else builder.Append(", ");
                        builder.Append(result.Message);
                        first = false;
                    }
                }
            }

            return builder.ToString();
        }
    }
}