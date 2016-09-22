using System;
using PipServices.Commons.Errors;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Build
{
    public class CreateException : InternalException
    {
        public CreateException(Exception innerException) :
            this(null, (string)null)
        {
        }

        public CreateException(string correlationId, Locator locator) :
            base(correlationId, "CANNOT_CREATE", locator == null ? "" : "Requested component " + locator.ToString() + " cannot be created")
        {
            if (locator != null)
            {
                WithDetails(locator);
            }
        }

        public CreateException(string correlationId, string message) :
            base(correlationId, "CANNOT_CREATE", message)
        {
        }
    }
}