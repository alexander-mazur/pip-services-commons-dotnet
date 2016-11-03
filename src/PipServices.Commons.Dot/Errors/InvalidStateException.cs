﻿using System;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Class of errors related to operations called in wrong component state.
    /// For example, business calls when the component is not ready.
    /// </summary>
    public class InvalidStateException : ApplicationException
    {
        public InvalidStateException(Exception innerException) : 
            base(ErrorCategory.InvalidState, null, null, null)
        {
            Status = 500;

            WithCause(innerException);
        }

        public InvalidStateException(string correlationId = null, string code = null, string message = null, Exception innerException = null) :
            base(ErrorCategory.InvalidState, correlationId, code, message)
        {
            Status = 500;

            WithCause(innerException);
        }
    }
}