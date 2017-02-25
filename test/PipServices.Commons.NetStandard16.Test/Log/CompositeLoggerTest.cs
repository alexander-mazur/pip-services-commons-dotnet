using PipServices.Commons.Log;
using PipServices.Commons.Refer;
using Xunit;

namespace PipServices.Commons.Test.Log
{
    public sealed class CompositeLoggerTest
    {
        private CompositeLogger Log { get; set; }
        private LoggerFixture Fixture { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeLoggerTest"/> class.
        /// </summary>
        public CompositeLoggerTest()
        {
            Log = new CompositeLogger();

            var refs = References.FromTuples(
                DefaultLoggerFactory.ConsoleLoggerDescriptor, new ConsoleLogger(), 
                DefaultLoggerFactory.DiagnosticsLoggerDescriptor, new DiagnosticsLogger(),
                DefaultLoggerFactory.CompositeLoggerDescriptor, Log
            );
            Log.SetReferences(refs);

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