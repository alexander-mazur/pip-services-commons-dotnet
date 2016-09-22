using System;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Class of errors related to mistakes in microservice user-defined configuration.
    /// </summary>
    public class ConfigException : ApplicationException
    {
        public ConfigException(Exception innerException) :
            base(ErrorCategory.Misconfiguration, null, null, null, innerException)
        {
            Status = 500;
        }

        public ConfigException(string correlationId = null, string code = null, string message = null, Exception innerException = null) :
            base(ErrorCategory.Misconfiguration, correlationId, code, message, innerException)
        {
            Status = 500;
        }
    }
}