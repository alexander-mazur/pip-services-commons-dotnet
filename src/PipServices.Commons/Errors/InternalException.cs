using System;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Class of errors related to internal system errors, programming mistakes, etc.
    /// </summary>
    public class InternalException : ApplicationException
    {
        public InternalException(Exception innerException) : 
            base(ErrorCategory.Internal, null, null, null, innerException)
        {
            Status = 500;
        }

        public InternalException(string correlationId = null, string code = null, string message = null, Exception innerException = null) :
            base(ErrorCategory.Internal, correlationId, code, message, innerException)
        {
            Status = 500;
        }
    }
}