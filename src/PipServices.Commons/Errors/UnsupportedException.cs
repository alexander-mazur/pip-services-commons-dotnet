using System;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Class of errors related to calls to unsupported functionality.
    /// </summary>
    public class UnsupportedException : ApplicationException
    {
        public UnsupportedException(Exception innerException) : 
            base(ErrorCategory.Unsupported, null, null, null, innerException)
        {
            Status = 500;
        }

        public UnsupportedException(string correlationId = null, string code = null, string message = null, Exception innerException = null) :
            base(ErrorCategory.Unsupported, correlationId, code, message, innerException)
        {
            Status = 500;
        }
    }
}