using Xunit;

namespace PipServices.Commons.Config
{
    public class YamlConfigReaderTest
    {
        [Fact]
        public void TestReadConfig()
        {
#if CORE_NET
            ConfigParams config = YamlConfigReader.ReadConfig(null, "../../data/config.yaml");
#else
            ConfigParams config = YamlConfigReader.ReadConfig(null, "../../../../data/config.yaml");
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

    }
}
