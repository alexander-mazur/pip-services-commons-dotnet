using System.Threading;
using Xunit;

namespace PipServices.Commons.Config
{
    public class JsonConfigReaderTest
    {
        [Fact]
        public void TestReadConfig()
        {
#if CORE_NET
            ConfigParams config = JsonConfigReader.ReadConfig(null, "../../data/config.json");
#else
            ConfigParams config = JsonConfigReader.ReadConfig(null, "../../../../data/config.json");
#endif

            Assert.Equal(7, config.Count);
            Assert.Equal(123, config.GetAsInteger("Field1.Field11"));
            Assert.Equal("ABC", config.GetAsString("Field1.Field12"));
            Assert.Equal(123, config.GetAsInteger("Field2.0"));
            Assert.Equal("ABC", config.GetAsString("Field2.1"));
            Assert.Equal(543, config.GetAsInteger("Field2.2.Field21"));
            Assert.Equal("XYZ", config.GetAsString("Field2.2.Field22"));
            Assert.Equal(true, config.GetAsBoolean("Field3"));
        }

        [Fact]
        public void TestReadAfterTimeout()
        {
#if CORE_NET
            var reader = new JsonConfigReader("../../data/config.json");
#else
            var reader = new JsonConfigReader("../../../../data/config.json");
#endif
            reader.Timeout = 100;

            var config = reader.ReadConfig(null);
            Assert.Equal(7, config.Count);

            Thread.Sleep(500);

            var newConfig = reader.ReadConfig(null);
            Assert.Equal(config.Count, newConfig.Count);
        }
    }
}
