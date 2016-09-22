using System;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Class of errors related to access of missing objects.
    /// </summary>
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(Exception innerException) : 
            base(ErrorCategory.NotFound, null, null, null, innerException)
        {
            Status = 404;
        }

        public NotFoundException(string correlationId = null, string code = null, string message = null, Exception innerException = null) :
            base(ErrorCategory.NotFound, correlationId, code, message, innerException)
        {
            Status = 404;
        }
    }
}