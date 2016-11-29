using System;
using System.Runtime.Serialization;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Class of errors related to mistakes in microservice user-defined configuration.
    /// </summary>
#if CORE_NET
    [DataContract]
#else
    [Serializable]
#endif
    public class ConfigException : ApplicationException
    {
        public ConfigException(Exception innerException) 
            : base(ErrorCategory.Misconfiguration, null, null, null)
        {
            Status = 500;
            WithCause(innerException);
        }

        public ConfigException(string correlationId = null, string code = null, string message = null, Exception innerException = null) 
            : base(ErrorCategory.Misconfiguration, correlationId, code, message)
        {
            Status = 500;
            WithCause(innerException);
        }

#if !CORE_NET
        protected ConfigException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        { }
#endif

    }
}