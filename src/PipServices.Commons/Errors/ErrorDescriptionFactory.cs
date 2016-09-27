using System;

namespace PipServices.Commons.Errors
{
    public static class ErrorDescriptionFactory
    {
        public static ErrorDescription Create(ApplicationException ex)
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

        public static ErrorDescription Create(Exception ex)
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
