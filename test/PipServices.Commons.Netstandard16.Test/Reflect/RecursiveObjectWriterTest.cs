using System;
using Xunit;
using PipServices.Commons.Reflect;
using PipServices.Commons.Convert;
using PipServices.Commons.Run;

namespace PipServices.Commons.Test.Reflect
{
    public sealed class RecursiveObjectWriterTest
    {
        [Fact]
        public void TestSetProperty()
        {
            var obj = JsonConverter.ToMap(
                "{ \"value1\": 123, \"value2\": { \"value21\": 111, \"value22\": 222 }, \"value3\": [ 444, { \"value311\": 555 } ] }"
            );

            //RecursiveObjectWriter.setProperty(obj, "", null);
            RecursiveObjectWriter.SetProperty(obj, "value1", "AAA");
            RecursiveObjectWriter.SetProperty(obj, "value2", "BBB");
            RecursiveObjectWriter.SetProperty(obj, "value3.1.value312", "CCC");
            RecursiveObjectWriter.SetProperty(obj, "value3.3", "DDD");
            RecursiveObjectWriter.SetProperty(obj, "value4.1", "EEE");

            var values = RecursiveObjectReader.GetProperties(obj);
            Assert.Equal(8, values.Count);
            Assert.Equal("AAA", values["value1"]);
            Assert.Equal("BBB", values["value2"]);
            Assert.False(values.ContainsKey("value2.value21"));
            Assert.Equal(444, (int)(long)values["value3.0"]);
            Assert.Equal(555, (int)(long)values["value3.1.value311"]);
            Assert.Equal("CCC", values["value3.1.value312"]);
            Assert.Null(values["value3.2"]);
            Assert.Equal("DDD", values["value3.3"]);
            Assert.Equal("EEE", values["value4.1"]);
        }

        [Fact]
        public void TestSetProperties()
        {
            var obj = JsonConverter.ToMap(
                "{ \"value1\": 123, \"value2\": { \"value21\": 111, \"value22\": 222 }, \"value3\": [ 444, { \"value311\": 555 } ] }"
            );

            var values = Parameters.FromTuples(
                //"", null,
                "value1", "AAA",
                "value2", "BBB",
                "value3.1.value312", "CCC",
                "value3.3", "DDD",
                "value4.1", "EEE"
            );
            RecursiveObjectWriter.SetProperties(obj, values);

            var resultValues = RecursiveObjectReader.GetProperties(obj);
            Assert.Equal(8, resultValues.Count);
            Assert.Equal("AAA", resultValues["value1"]);
            Assert.Equal("BBB", resultValues["value2"]);
            Assert.False(resultValues.ContainsKey("value2.value21"));
            Assert.Equal(444, (int)(long)resultValues["value3.0"]);
            Assert.Equal(555, (int)(long)resultValues["value3.1.value311"]);
            Assert.Equal("CCC", resultValues["value3.1.value312"]);
            Assert.Null(resultValues["value3.2"]);
            Assert.Equal("DDD", resultValues["value3.3"]);
            Assert.Equal("EEE", resultValues["value4.1"]);
        }
    }
}
