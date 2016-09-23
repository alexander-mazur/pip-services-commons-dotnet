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

        public CreateException(string correlationId, Descriptor descriptor) :
            base(correlationId, "CANNOT_CREATE", descriptor == null ? "" : "Requested component " + descriptor.ToString() + " cannot be created")
        {
            if (descriptor != null)
            {
                WithDetails(descriptor);
            }
        }

        public CreateException(string correlationId, string message) :
            base(correlationId, "CANNOT_CREATE", message)
        {
        }
    }
}