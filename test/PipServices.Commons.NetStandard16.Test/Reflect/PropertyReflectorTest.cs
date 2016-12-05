using PipServices.Commons.Reflect;
using System.Collections.Generic;
using Xunit;

namespace PipServices.Commons.Test.Reflect
{
    public class PropertyReflectorTest
    {
        [Fact]
        public void TestGetProperty()
        {
            TestClass obj = new TestClass();

            object value = PropertyReflector.GetProperty(obj, "privateField");
            Assert.Null(value);

            value = PropertyReflector.GetProperty(obj, "publicField");
            Assert.Equal("ABC", value);

            value = PropertyReflector.GetProperty(obj, "PublicProp");
            Assert.NotNull(value);
        }

        [Fact]
        public void TestGetProperties()
        {
            TestClass obj = new TestClass();
            List<string> names = PropertyReflector.GetPropertyNames(obj);
            Assert.Equal(2, names.Count);
            Assert.True(names.Contains("PublicField"));
            Assert.True(names.Contains("PublicProp"));

            Dictionary<string, object> map = PropertyReflector.GetProperties(obj);
            Assert.Equal(2, map.Count);
            Assert.Equal("ABC", map["PublicField"]);
            Assert.NotNull(map["PublicProp"]);
        }
    }
}
