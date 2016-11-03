using System;
using PipServices.Commons.Errors;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Build
{
    /// <summary>
    /// Exception thrown when a component cannot be created by a factory.
    /// </summary>
    public class CreateException : InternalException
    {
        public CreateException(Exception innerException) :
            this(null, (string)null)
        {
        }

        public CreateException(string correlationId, object locator) :
            base(correlationId, "CANNOT_CREATE", "Requested component " + (locator == null ? "" : locator.ToString()) + " cannot be created")
        {
            WithDetails("locator", locator);
        }

        public CreateException(string correlationId, string message) :
            base(correlationId, "CANNOT_CREATE", message)
        {
        }
    }
}