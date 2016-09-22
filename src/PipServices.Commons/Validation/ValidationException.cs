using PipServices.Commons.Errors;
using System;

namespace PipServices.Commons.Validation
{
    public class ValidationException : BadRequestException
    {
        public ValidationException(Exception innerException) :
            this(null, null)
        {
        }

        public ValidationException(string correlationId, string message) :
            base(correlationId, "INVALID_DATA", message)
        {
        }
    }
}