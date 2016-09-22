using System;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Class of errors related to connection to remote services.
    /// </summary>
    public class ConnectionException : ApplicationException
    {
        public ConnectionException(Exception innerException) : 
            base(ErrorCategory.NoResponse, null, null, null, innerException)
        {
            Status = 500;
        }

        public ConnectionException(string correlationId = null, string code = null, string message = null, Exception innerException = null) :
            base(ErrorCategory.NoResponse, correlationId, code, message, innerException)
        {
            Status = 500;
        }
    }
}