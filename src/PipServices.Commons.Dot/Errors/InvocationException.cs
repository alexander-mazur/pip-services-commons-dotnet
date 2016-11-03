﻿using System;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Class of errors related to remote service calls.
    /// </summary>
    public class InvocationException : ApplicationException
    {
        public InvocationException(Exception innerException) : 
            base(ErrorCategory.FailedInvocation, null, null, null)
        {
            Status = 500;

            WithCause(innerException);
        }

        public InvocationException(string correlationId = null, string code = null, string message = null, Exception innerException = null) :
            base(ErrorCategory.FailedInvocation, correlationId, code, message)
        {
            Status = 500;

            WithCause(innerException);
        }
    }
}