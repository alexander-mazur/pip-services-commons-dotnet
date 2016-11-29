using System;
using PipServices.Commons.Data;
using System.Runtime.Serialization;

namespace PipServices.Commons.Errors
{
    /// <summary>
    /// Generic application exception.
    /// </summary>
#if CORE_NET
    [DataContract]
#else
    [Serializable]
#endif
    public class ApplicationException : Exception
    {
        private string _stackTrace;

        public ApplicationException() :
            this(null, null, null, null)
        { }

        public ApplicationException(string category = null, string correlationId = null, string code = null, string message = null) 
            : base(message ?? "Unknown error")
        {
            Code = "UNKNOWN";
            Status = 500;

            Category = category ?? ErrorCategory.Unknown;
            CorrelationId = correlationId;
            Code = code;
        }

#if !CORE_NET
        protected ApplicationException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
            Category = info.GetString("category");
            CorrelationId = info.GetString("correlation_id");
            Cause = info.GetString("cause");
            Code = info.GetString("code");
            Status = info.GetInt32("status");
            _stackTrace = info.GetString("stack_trace");
            Details = StringValueMap.FromString(info.GetString("details"));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("category", Category);
            info.AddValue("correlation_id", CorrelationId);
            info.AddValue("cause", Cause);
            info.AddValue("code", Code);
            info.AddValue("status", Status);
            info.AddValue("stack_trace", StackTrace);
            info.AddValue("details", Details != null ? Details.ToString() : null);
        }
#endif

#if CORE_NET
        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string CorrelationId { get; set; }

        [DataMember]
        public string Cause { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public StringValueMap Details { get; set; }

        [DataMember]
        public new string StackTrace
        {
            get { return _stackTrace ?? base.StackTrace; }
            set { _stackTrace = value; }
        }
#else
        public string Category { get; set; }
        public string CorrelationId { get; set; }
        public string Cause { get; set; }
        public string Code { get; set; }
        public int Status { get; set; }
        public StringValueMap Details { get; set; }

        public new string StackTrace
        {
            get { return _stackTrace ?? base.StackTrace; }
            set { _stackTrace = value; }
        }
#endif

        public ApplicationException WithCode(string code)
        {
            Code = code ?? "UNKNOWN";
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

        public static ApplicationException WrapException(ApplicationException error, Exception cause)
        {
            if (cause is ApplicationException)
                return (ApplicationException)cause;

            error.WithCause(cause);
            return error;
        }
    }
}