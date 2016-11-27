using PipServices.Commons.Log;
using Xunit;

namespace PipServices.Commons.Refer
{
    public class AutoCreateReferencesTest
    {
        [Fact]
        public void TestAutoCreateComponent()
        {
            var refs = new AutoCreateReferences();

            var factory = new DefaultLoggerFactory();
            refs.Put(factory);

            var logger = refs.GetOneRequired<ILogger>(new Descriptor("*", "logger", "*", "*", "*"));
            Assert.NotNull(logger);
        }

        [Fact]
        public void TestStringLocator()
        {
            var refs = new AutoCreateReferences();

            var factory = new DefaultLoggerFactory();
            refs.Put(factory);

            var component = refs.GetOneOptional("ABC");
            Assert.Null(component);
        }

        [Fact]
        public void TestNullLocator()
        {
            var refs = new AutoCreateReferences();

            var factory = new DefaultLoggerFactory();
            refs.Put(factory);

            var component = refs.GetOneOptional(null);
            Assert.Null(component);
        }
    }
}
