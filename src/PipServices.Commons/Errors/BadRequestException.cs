using System;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Class of errors due to improper user requests, such as missing or wrong parameters.
    /// </summary>
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(Exception innerException) :
            base(ErrorCategory.BadRequest, null, null, null, innerException)
        {
            Status = 400;
        }

        public BadRequestException(string correlationId = null, string code = null, string message = null, Exception innerException = null) :
            base(ErrorCategory.BadRequest, correlationId, code, message, innerException)
        {
            Status = 400;
        }
    }
}