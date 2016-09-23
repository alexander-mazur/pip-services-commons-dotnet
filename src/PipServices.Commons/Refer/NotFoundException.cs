using System;
using PipServices.Commons.Errors;

namespace PipServices.Commons.Refer
{
    public class ReferenceNotFoundException : NotFoundException
    {
        public ReferenceNotFoundException(Exception innerException) : 
            this(null, null)
        {
        }

        public ReferenceNotFoundException(string correlationId, Descriptor descriptor) :
            base(correlationId, "REF_NOT_FOUND", descriptor == null ? "" : "Reference to " + descriptor.ToString() + " was not found")
        {
            if (descriptor != null)
            {
                WithDetails(descriptor);
            }
        }
    }
}