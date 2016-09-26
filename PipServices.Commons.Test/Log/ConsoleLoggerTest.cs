using PipServices.Commons.Log;
using Xunit;

namespace PipServices.Commons.Test.Log
{
    public sealed class ConsoleLoggerTest
    {
        private ILogger Log { get; set; }
        private LoggerFixture Fixture { get; set; }

        public ConsoleLoggerTest()
        {
            Log = new ConsoleLogger();
            Fixture = new LoggerFixture(Log);
        }

        [Fact]
        public void TestLogLevel()
        {
            Fixture.TestLogLevel();
        }

        [Fact]
        public void TestTextOutput()
        {
            Fixture.TestTextOutput();
        }

        [Fact]
        public void TestMixedOutput()
        {
            Fixture.TestMixedOutput();
        }
    }
}
