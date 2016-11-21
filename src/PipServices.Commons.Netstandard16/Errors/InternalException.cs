using System;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Class of errors related to internal system errors, programming mistakes, etc.
    /// </summary>
    public class InternalException : ApplicationException
    {
        public InternalException(Exception innerException) 
            : base(ErrorCategory.Internal, null, null, null)
        {
            Status = 500;
            WithCause(innerException);
        }

        public InternalException(string correlationId = null, string code = null, string message = null, Exception innerException = null) 
            : base(ErrorCategory.Internal, correlationId, code, message)
        {
            Status = 500;
            WithCause(innerException);
        }
    }
}