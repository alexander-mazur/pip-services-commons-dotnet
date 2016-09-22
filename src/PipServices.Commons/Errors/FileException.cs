using System;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Class of errors related to read/write file operations.
    /// </summary>
    public class FileException : ApplicationException
    {
        public FileException(Exception innerException) : 
            base(ErrorCategory.NoFileAccess, null, null, null, innerException)
        {
            Status = 500;
        }

        public FileException(string correlationId = null, string code = null, string message = null, Exception innerException = null) :
            base(ErrorCategory.NoFileAccess, correlationId, code, message, innerException)
        {
            Status = 500;
        }
    }
}