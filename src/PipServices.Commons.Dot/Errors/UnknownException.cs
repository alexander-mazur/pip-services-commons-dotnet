﻿using System;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Class of errors related to unknown or unexpected errors.
    /// </summary>
    public class UnknownException : ApplicationException
    {
        public UnknownException(Exception innerException) : 
            base(ErrorCategory.Unknown, null, null, null)
        {
            Status = 500;

            WithCause(innerException);
        }

        public UnknownException(string correlationId = null, string code = null, string message = null, Exception innerException = null) :
            base(ErrorCategory.Unknown, correlationId, code, message)
        {
            Status = 500;

            WithCause(innerException);
        }
    }
}