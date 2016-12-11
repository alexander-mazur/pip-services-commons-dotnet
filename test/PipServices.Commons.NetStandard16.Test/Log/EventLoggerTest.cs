﻿using PipServices.Commons.Test.Log;
using Xunit;

namespace PipServices.Commons.Log
{
    public sealed class EventLoggerTest
    {
        private ILogger Log { get; set; }
        private LoggerFixture Fixture { get; set; }

        public EventLoggerTest()
        {
            Log = new EventLogger();
            Fixture = new LoggerFixture(Log);
        }

        [Fact]
        public void TestLogLevel()
        {
            Fixture.TestLogLevel();
        }

        [Fact]
        public void TestSimpleLogging()
        {
            Fixture.TestSimpleLogging();
        }

        [Fact]
        public void TestErrorLogging()
        {
            Fixture.TestErrorLogging();
        }
    }
}
