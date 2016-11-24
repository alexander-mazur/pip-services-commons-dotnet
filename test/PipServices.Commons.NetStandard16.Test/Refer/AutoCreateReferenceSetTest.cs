using PipServices.Commons.Log;
using Xunit;

namespace PipServices.Commons.Refer
{
    public class AutoCreateReferenceSetTest
    {
        [Fact]
        public void TestAutoCreateComponent()
        {
            var refs = new AutoCreateReferenceSet();

            var factory = new DefaultLoggerFactory();
            refs.Put(factory);

            var logger = refs.GetOneRequired<ILogger>(new Descriptor("*", "logger", "*", "*", "*"));
            Assert.NotNull(logger);
        }
    }
}
