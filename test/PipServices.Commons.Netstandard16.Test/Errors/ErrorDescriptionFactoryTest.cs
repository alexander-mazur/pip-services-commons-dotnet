﻿using System;
using PipServices.Commons.Errors;
using Xunit;

using ApplicationException = PipServices.Commons.Errors.ApplicationException;

namespace PipServices.Commons.Test.Errors
{
    public class ErrorDescriptionFactoryTest
    {
        [Fact]
        public void Create_FromApplicationException_IsOk()
        {
            var key = "key";
            var details = "details";

            var ex = new ApplicationException("category", "correlationId", "code", "message")
            {
                Status = 777,
                Cause = "cause",
                StackTrace = "stackTrace"
            };
            ex.WithDetails(key, details);

            var descr = ErrorDescriptionFactory.Create(ex);

            Assert.NotNull(descr);
            Assert.Equal(ex.Category, descr.Category);
            Assert.Equal(ex.CorrelationId, descr.CorrelationId);
            Assert.Equal(ex.Code, descr.Code);
            Assert.Equal(ex.Message, descr.Message);
            Assert.Equal(ex.Status, descr.Status);
            Assert.Equal(ex.Cause, descr.Cause);
            Assert.Equal(ex.StackTrace, descr.StackTrace);
            Assert.Equal(ex.Details, descr.Details);
        }

        [Fact]
        public void Create_FromException_IsOk()
        {
            var ex = new Exception("message");

            var descr = ErrorDescriptionFactory.Create(ex);

            Assert.NotNull(descr);
            Assert.Equal(ErrorCategory.Unknown, descr.Category);
            Assert.Equal("Unknown", descr.Code);
            Assert.Equal(ex.Message, descr.Message);
            Assert.Equal(500, descr.Status);
            Assert.Equal(ex.StackTrace, descr.StackTrace);
        }
    }
}
