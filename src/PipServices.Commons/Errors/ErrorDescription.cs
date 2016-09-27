using System;
using System.Runtime.Serialization;
using PipServices.Commons.Data;

namespace PipServices.Commons.Errors
{
    [DataContract]
    public class ErrorDescription
    {
        [DataMember(Name = "category")]
        public string Category { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "details")]
        public StringValueMap Details { get; set; }

        [DataMember(Name = "correlation_id")]
        public string CorrelationId { get; set; }

        [DataMember(Name = "cause")]
        public string Cause { get; set; }

        [DataMember(Name = "stack_trace")]
        public string StackTrace { get; set; }

        public ApplicationException CreateError()
        {
            ApplicationException error = null;
            if (Category == ErrorCategory.Unknown)
                error = new UnknownException(CorrelationId, Code, Message);
            else if (Category == ErrorCategory.Internal)
                error = new InternalException(CorrelationId, Code, Message);
            else if (Category == ErrorCategory.Misconfiguration)
                error = new ConfigException(CorrelationId, Code, Message);
            else if (Category == ErrorCategory.NoResponse)
                error = new ConnectionException(CorrelationId, Code, Message);
            else if (Category == ErrorCategory.FailedInvocation)
                error = new InvocationException(CorrelationId, Code, Message);
            else if (Category == ErrorCategory.NoFileAccess)
                error = new FileException(CorrelationId, Code, Message);
            else if (Category == ErrorCategory.BadRequest)
                error = new BadRequestException(CorrelationId, Code, Message);
            else if (Category == ErrorCategory.Unauthorized)
                error = new UnauthorizedException(CorrelationId, Code, Message);
            else if (Category == ErrorCategory.Conflict)
                error = new ConflictException(CorrelationId, Code, Message);
            else if (Category == ErrorCategory.NotFound)
                error = new NotFoundException(CorrelationId, Code, Message);
            else if (Category == ErrorCategory.Unsupported)
                error = new UnsupportedException(CorrelationId, Code, Message);
            else
            {
                error = new UnknownException(CorrelationId, Code, Message);
                error.Category = Category;
                error.Status = Status;
            }

            error
                .WithCause(Cause)
                .WithStackTrace(StackTrace);
            error.Details = Details;

            return error;
        }

        public static ErrorDescription From(ApplicationException ex)
        {
            return new ErrorDescription()
            {
                Category = ex.Category,
                Status = ex.Status,
                Code = ex.Code,
                Message = ex.Message,
                Details = ex.Details,
                CorrelationId = ex.CorrelationId,
                Cause = ex.Cause,
                StackTrace = ex.StackTrace
            };
        }

        public static ErrorDescription From(Exception ex)
        {
            return new ErrorDescription()
            {
                Category = ErrorCategory.Unknown,
                Status = 500,
                Code = "Unknown",
                Message = ex.Message,
                StackTrace = ex.StackTrace,
            };
        }
    }
}