using System;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Class of errors related to calls to unsupported functionality.
    /// </summary>
    public class UnsupportedException : ApplicationException
    {
        public UnsupportedException(Exception innerException) 
            : base(ErrorCategory.Unsupported, null, null, null)
        {
            Status = 500;
            WithCause(innerException);
        }

        public UnsupportedException(string correlationId = null, string code = null, string message = null, Exception innerException = null) 
            : base(ErrorCategory.Unsupported, correlationId, code, message)
        {
            Status = 500;
            WithCause(innerException);
        }
    }
}