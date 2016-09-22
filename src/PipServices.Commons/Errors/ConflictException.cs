using System;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Class of errors related to conflict in object versions between the user request and the server.
    /// </summary>
    public class ConflictException : ApplicationException
    {
        public ConflictException(Exception innerException) :
            base(ErrorCategory.Conflict, null, null, null, innerException)
        {
            Status = 409;
        }

        public ConflictException(string correlationId = null, string code = null, string message = null, Exception innerException = null) :
            base(ErrorCategory.Conflict, correlationId, code, message, innerException)
        {
            Status = 409;
        }
    }
}