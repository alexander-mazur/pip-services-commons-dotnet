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

        public ApplicationException() :
            this(null, null, null, null)
        { }

        public ApplicationException(string category = null, string correlationId = null, string code = null, string message = null) : 
            base(message ?? "Unknown error")
        {
            Category = category ?? ErrorCategory.Unknown;
            CorrelationId = correlationId;
            Code = code;
        }

        public string Category { get; set; }
        public string CorrelationId { get; set; }
        public string Cause { get; set; }
        public StringValueMap Details { get; set; }
        public string Code { get; set; } = "Unknown";
        public int Status { get; set; } = 500;

        public new string StackTrace
        {
            get { return _stackTrace ?? base.StackTrace; }
            set { _stackTrace = value; }
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

        public ApplicationException WithCause(Exception cause)
        {
            Cause = cause?.Message;
            return this;
        }

        public ApplicationException WithStatus(int status)
        {
            Status = status;
            return this;
        }

        public ApplicationException WithDetails(string key, object value)
        {
            Details = Details ?? new StringValueMap();
            Details.SetAsObject(key, value);
            return this;
        }

        public ApplicationException WithStackTrace(string stackTrace)
        {
            _stackTrace = stackTrace;
            return this;
        }

        public ApplicationException Wrap(Exception cause)
        {
            if (cause is ApplicationException)
                return (ApplicationException)cause;

            WithCause(cause);
            return this;
        }

        public static ApplicationException Wrap(ApplicationException error, Exception cause)
        {
            if (cause is ApplicationException)
                return (ApplicationException)cause;

            error.WithCause(cause);
            return error;
        }
    }
}