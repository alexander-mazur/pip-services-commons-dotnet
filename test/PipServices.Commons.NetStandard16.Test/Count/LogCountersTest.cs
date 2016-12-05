using PipServices.Commons.Log;
using PipServices.Commons.Config;
using PipServices.Commons.Count;
using PipServices.Commons.Refer;
using Xunit;

namespace PipServices.Commons.Test.Count
{
    public sealed class LogCountersTest
    {
        private readonly LogCounters _counters = new LogCounters();
        private readonly CountersFixture _fixture;

        public LogCountersTest()
        {
            var log = new ConsoleLogger();
            var refs = References.FromList(log);

            _counters.SetReferences(refs);

            _fixture = new CountersFixture(_counters);
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
