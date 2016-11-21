using System;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Class of errors related to read/write file operations.
    /// </summary>
    public class FileException : ApplicationException
    {
        public FileException(Exception innerException) 
            : base(ErrorCategory.NoFileAccess, null, null, null)
        {
            Status = 500;
            WithCause(innerException);
        }

        public FileException(string correlationId = null, string code = null, string message = null, Exception innerException = null) 
            : base(ErrorCategory.NoFileAccess, correlationId, code, message)
        {
            Status = 500;
            WithCause(innerException);
        }
    }
}