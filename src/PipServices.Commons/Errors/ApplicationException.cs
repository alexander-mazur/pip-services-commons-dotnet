using System;
using PipServices.Commons.Data;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Generic application exception.
    /// </summary>
    public class ApplicationException : Exception
    {
        private string _stackTrace;

        public string Category { get; set; }        public string CorrelationId { get; set; }
        public string Cause { get; set; }
        public StringValueMap Details { get; set; }
        public string Code { get; set; } = "Unknown";
        public int Status { get; set; } = 500;
        public new string StackTrace
        {
            get { return _stackTrace ?? base.StackTrace; }
            set { _stackTrace = value; }
        }

        public ApplicationException(Exception innerException) :
            this(null, null, null, null, innerException)
        { }

        public ApplicationException(string category = null, string correlationId = null, string code = null, string message = null, Exception innerException = null) : 
            base(message ?? "Unknown error", innerException)
        {
            Category = category ?? ErrorCategory.Unknown;
            CorrelationId = correlationId;
            Code = code;
        }

        public ApplicationException WithCode(string code)
        {
            Code = code;
            return this;
        }

        public ApplicationException WithCorrelationId(string correlationId)
        {
            CorrelationId = correlationId;
            return this;
        }

        public ApplicationException WithCause(string cause)
        {
            Cause = cause;
            return this;
        }

        public ApplicationException WithStatus(int status)
        {
            Status = status;
            return this;
        }

        public ApplicationException WithDetails(string key, object value)
        {
            if(Details == null)
            {
                Details = new StringValueMap();
            }
            Details.SetAsObject(key, value);
            return this;
        }

        public ApplicationException WithStackTrace(string stackTrace)
        {
            _stackTrace = stackTrace;
            return this;
        }
    }
}