using PipServices.Commons.Errors;
using System;

namespace PipServices.Commons.Refer
{
    public class ReferenceNotFoundException : NotFoundException
    {
        public ReferenceNotFoundException(Exception innerException) : 
            this(null, null)
        {
        }

        public ReferenceNotFoundException(string correlationId, Locator locator) :
            base(correlationId, "REF_NOT_FOUND", locator == null ? "" : "Reference to " + locator.ToString() + " was not found")
        {
            if (locator != null)
            {
                WithDetails(locator);
            }
        }
    }
}