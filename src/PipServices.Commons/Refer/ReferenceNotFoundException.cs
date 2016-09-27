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

        public ReferenceNotFoundException(string correlationId, object locator) :
            base(correlationId, "REF_NOT_FOUND", locator == null ? "" : "Reference to " + locator.ToString() + " was not found")
        {
            if (locator != null)
            {
                WithDetails("locator", locator);
            }
        }
    }
}