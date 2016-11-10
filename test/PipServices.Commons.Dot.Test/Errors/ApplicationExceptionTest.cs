﻿using System;
using PipServices.Commons.Errors;
using Xunit;

namespace PipServices.Commons.Test.Errors
{
    public sealed class ApplicationExceptionTest
    {
        private readonly Commons.Errors.ApplicationException _appEx;
        private readonly Exception _ex;

        private const string Category = "category";
        private const string CorrelationId = "correlationId";
        private const string Code = "code";
        private const string Message = "message";

        public ApplicationExceptionTest()
        {
            _ex = new Exception("Couse exception");

            _appEx = new Commons.Errors.ApplicationException(Category, CorrelationId, Code, Message);
        }

        [Fact]
        public void Constructor_WithCouse_IsOk()
        {
            var ex = new Exception();

            var appEx = new Commons.Errors.ApplicationException();
            appEx.WithCause(ex);

            Assert.Equal(ex.Message, appEx.Cause);
        }

        [Fact]
        public void Constructor_CheckParameters_IsOk()
        {
            Assert.Equal(Category, _appEx.Category);
            Assert.Equal(CorrelationId, _appEx.CorrelationId);
            Assert.Equal(Code, _appEx.Code);
            Assert.Equal(Message, _appEx.Message);
        }

        [Fact]
        public void WithCode_Check_IsOk()
        {
            var newCode = "newCode";

            var appEx = _appEx.WithCode(newCode);

            Assert.Equal(_appEx, appEx);
            Assert.Equal(newCode, appEx.Code);
        }

        [Fact]
        public void WithCorrelationId_Check_IsOk()
        {
            var newCorrelationId = "newCorrelationId";

            var appEx = _appEx.WithCorrelationId(newCorrelationId);

            Assert.Equal(_appEx, appEx);
            Assert.Equal(newCorrelationId, appEx.CorrelationId);
        }

        [Fact]
        public void WithCause_Check_IsOk()
        {
            var newCause = new Exception("newCause");

            var appEx = _appEx.WithCause(newCause);

            Assert.Equal(_appEx, appEx);
            Assert.Equal(newCause.Message, appEx.Cause);
        }

        [Fact]
        public void WithStatus_Check_IsOk()
        {
            var newStatus = 777;

            var appEx = _appEx.WithStatus(newStatus);

            Assert.Equal(_appEx, appEx);
            Assert.Equal(newStatus, appEx.Status);
        }

        [Fact]
        public void WithDetails_Check_IsOk()
        {
            var key = "key";
            var obj = new object();

            var appEx = _appEx.WithDetails(key, obj);

            var newObj = appEx.Details.GetAsObject(key);

            Assert.Equal(_appEx, appEx);
            Assert.Same(obj, newObj);
        }

        [Fact]
        public void WithStackTrace_Check_IsOk()
        {
            var newTrace = "newTrace";

            var appEx = _appEx.WithStackTrace(newTrace);

            Assert.Equal(_appEx, appEx);
            Assert.Equal(newTrace, appEx.StackTrace);
        }
    }
}
