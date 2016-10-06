using PipServices.Commons.Log;
using PipServices.Commons.Config;
using PipServices.Commons.Count;
using Xunit;

namespace PipServices.Commons.Test.Count
{
    public sealed class LogCountersTest
    {
        private readonly LogCounters _counters = new LogCounters();
        private readonly CountersFixture _fixture;

        public LogCountersTest()
        {
            ILogger log = new ConsoleLogger();

            _fixture = new CountersFixture(_counters);

            _counters.ClearAll();
        }

        [Fact]
        public void TestSimpleCounters()
        {
            _fixture.TestSimpleCounters();
        }

        [Fact]
        public void TestMeasureElapsedTime()
        {
            _fixture.TestMeasureElapsedTime();
        }
    }
}
