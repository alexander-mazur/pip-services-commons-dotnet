using PipServices.Commons.Errors;

namespace PipServices.Commons.Build
{
    /// <summary>
    /// Exception thrown when a component cannot be created by a factory.
    /// </summary>
    public class CreateException : InternalException
    {
        public CreateException()
            : this(null, null)
        { }

        public CreateException(string correlationId, object locator) 
            : base(correlationId, "CANNOT_CREATE", "Requested component " + locator + " cannot be created")
        {
            WithDetails("locator", locator);
        }

        public CreateException(string correlationId, string message) 
            : base(correlationId, "CANNOT_CREATE", message)
        { }
    }
}