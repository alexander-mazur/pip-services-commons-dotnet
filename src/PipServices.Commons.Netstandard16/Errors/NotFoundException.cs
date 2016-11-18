using System;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Class of errors related to access of missing objects.
    /// </summary>
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(Exception innerException) : 
            base(ErrorCategory.NotFound, null, null, null)
        {
            Status = 404;

            WithCause(innerException);
        }

        public NotFoundException(string correlationId = null, string code = null, string message = null, Exception innerException = null) :
            base(ErrorCategory.NotFound, correlationId, code, message)
        {
            Status = 404;

            WithCause(innerException);
        }
    }
}