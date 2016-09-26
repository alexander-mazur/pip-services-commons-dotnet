﻿using PipServices.Commons.Log;
using Xunit;

namespace PipServices.Commons.Test.Log
{
    public sealed class LoggerFixture
    {
        private readonly ILogger _logger;

        public LoggerFixture(ILogger logger)
        {
            _logger = logger;
        }

        public void TestLogLevel()
        {
            Assert.True(_logger.Level >= LogLevel.None);
            Assert.True(_logger.Level <= LogLevel.Trace);
        }

        public void TestTextOutput()
        {
            _logger.Write(null, LogLevel.Fatal, null, "123", new object[] { "Fatal error..."});
            _logger.Write(null, LogLevel.Error, null, "123", new object[] { "Recoverable error..." });
            _logger.Write(null, LogLevel.Warn, null, "123", new object[] { "Warning..." });
            _logger.Write(null, LogLevel.Info, null, "123", new object[] { "Information message..." });
            _logger.Write(null, LogLevel.Debug, null, "123", new object[] { "Debug message..." });
            _logger.Write(null, LogLevel.Trace, null, "123", new object[] { "Trace message..." });
        }

        public void TestMixedOutput()
        {
            object obj = new { abc = "ABC" };

            _logger.Write(null, LogLevel.Fatal, null, "123", new[] { 123, "ABC", obj });
            _logger.Write(null, LogLevel.Error, null, "123", new[] { 123, "ABC", obj });
            _logger.Write(null, LogLevel.Warn, null, "123", new[] { 123, "ABC", obj });
            _logger.Write(null, LogLevel.Info, null, "123", new[] { 123, "ABC", obj });
            _logger.Write(null, LogLevel.Debug, null, "123", new[] { 123, "ABC", obj });
            _logger.Write(null, LogLevel.Trace, null, "123", new[] { 123, "ABC", obj });
        }
    }
}
