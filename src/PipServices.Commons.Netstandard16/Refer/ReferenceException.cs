using PipServices.Commons.Errors;

namespace PipServices.Commons.Refer
{
    /// <summary>
    /// Exception thrown when required component is not found in references
    /// </summary>
    public class ReferenceException : InternalException
    {
        public ReferenceException()
            : this(null, null)
        {
        }

        public ReferenceException(string correlationId, object locator)
            : base(correlationId, "REF_ERROR", "Failed to obtain reference to " + locator)
        {
            WithDetails("locator", locator);
        }

        public ReferenceException(string correlationId, string message)
            : base(correlationId, "REF_ERROR", message)
        { }

        public ReferenceException(string correlationId, string code, string message)
            : base(correlationId, code, message)
        { }
    }
}
