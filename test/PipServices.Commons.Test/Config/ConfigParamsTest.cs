using PipServices.Commons.Config;
using Xunit;

namespace PipServices.Commons.Test.Data
{
    public sealed class ConfigParamsTest
    {
        [Fact]
        public void TestConfigSections()
        {
            var config = ConfigParams.FromTuples(
                "Section1.Key1", "Value1",
                "Section1.Key2", "Value2",
                "Section1.Key3", "Value3"
            );
            Assert.Equal(3, config.Count);
            Assert.Equal("Value1", config.Get("Section1.Key1"));
            Assert.Equal("Value2", config.Get("Section1.Key2"));
            Assert.Equal("Value3", config.Get("Section1.Key3"));
            Assert.Null(config.Get("Section1.Key4"));

            var section2 = ConfigParams.FromTuples(
                "Key1", "ValueA",
                "Key2", "ValueB"
            );
            config.AddSection("Section2", section2);
            Assert.Equal(5, config.Count);
            Assert.Equal("ValueA", config.Get("Section2.Key1"));
            Assert.Equal("ValueB", config.Get("Section2.Key2"));

            var section1 = config.GetSection("Section1");
            Assert.Equal(3, section1.Count);
            Assert.Equal("Value1", section1.Get("Key1"));
            Assert.Equal("Value2", section1.Get("Key2"));
            Assert.Equal("Value3", section1.Get("Key3"));
        }

        //[Fact]
        //public void TestConfigFromAppSettings()
        //{
        //    var config = ConfigParams.FromAppSettings();
        //    Assert.Equal(5, config.Count);
        //    Assert.Equal("Value1", config.Get("Section1.Key1"));
        //    Assert.Equal("Value2", config.Get("Section1.Key2"));
        //    Assert.Equal("Value3", config.Get("Section1.Key3"));
        //    Assert.Equal("ValueA", config.Get("Section2.Key1"));
        //    Assert.Equal("ValueB", config.Get("Section2.Key2"));
        //}

        //[Fact]
        //public void TestConfigFromConnectionStrings()
        //{
        //    var config = ConfigParams.FromConnectionStrings();
        //    //Assert.AreEqual(5, config.Count);
        //    Assert.Equal("Value1", config.Get("Section1.Key1"));
        //    Assert.Equal("Value2", config.Get("Section1.Key2"));
        //    Assert.Equal("Value3", config.Get("Section1.Key3"));
        //    Assert.Equal("ValueA", config.Get("Section2.Key1"));
        //    Assert.Equal("ValueB", config.Get("Section2.Key2"));
        //}

        //[Fact]
        //public void TestConfigFromXmlFile()
        //{
        //    var config = ConfigParams.FromXmlFile("Local.xml");
        //    Assert.Equal(2, config.Count);
        //    Assert.Equal("ValueA", config.Get("Key1"));
        //    Assert.Equal("ValueB", config.Get("Key2"));
        //}
    }
}
