using System;
using PipServices.Commons.Errors;

namespace PipServices.Commons.Validate
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