using System;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Class of errors related to unauthorized access to secured objects or services.
    /// </summary>
    public class UnauthorizedException : ApplicationException
    {
        public UnauthorizedException(Exception innerException) : 
            base(ErrorCategory.Unauthorized, null, null, null)
        {
            Status = 401;

            WithCause(innerException);
        }

        public UnauthorizedException(string correlationId = null, string code = null, string message = null, Exception innerException = null) :
            base(ErrorCategory.Unauthorized, correlationId, code, message)
        {
            Status = 401;

            WithCause(innerException);
        }
    }
}