using System.Collections.Generic;
using System.Threading.Tasks;
using PipServices.Commons.Cache;
using PipServices.Commons.Config;
using Xunit;

namespace PipServices.Commons.Test.Cache
{
    public class MemoryCacheTest
    {
        private readonly MemoryCache _cache = new MemoryCache();

        private const string Key1 = "key1";
        private const string Key2 = "key2";
        private const string Key3 = "key3";

        private const string Value1 = "value1";
        private const string Value2 = "value2";
        private const string Value3 = "value3";

        public MemoryCacheTest()
        {
            _cache.Store(Key1, Value1, 1000);
            _cache.Store(Key2, Value2, 1000);
        }

        [Fact]
        public void GetDescriptor_ReturnsMemoryCache()
        {
            var descriptor = _cache.GetDescriptor();

            Assert.NotNull(descriptor);
            Assert.Equal("cache", descriptor.Type);
            Assert.Equal("memory", descriptor.Id);
        }

        [Fact]
        public void Retrieve_BothValue_In500ms()
        {
            Task.Delay(500).Wait();

            var val1 = _cache.Retrieve(Key1);
            var val2 = _cache.Retrieve(Key2);

            Assert.NotNull(val1);
            Assert.Equal(Value1, val1);

            Assert.NotNull(val2);
            Assert.Equal(Value2, val2);
        }

        [Fact]
        public void Retrieve_BothValue_In1000ms_Fails()
        {
            Task.Delay(1000).Wait();

            var val1 = _cache.Retrieve(Key1);
            var val2 = _cache.Retrieve(Key2);

            Assert.Null(val1);
            Assert.Null(val2);
        }

        [Fact]
        public void Store_ReturnsSameValue()
        {
            var storedVal = _cache.Store(Key3, Value3, 0);

            Assert.Equal(Value3, storedVal);
        }

        [Fact]
        public void Store_ValueIsStored()
        {
            _cache.Store(Key3, Value3, 1000);

            var val3 = _cache.Retrieve(Key3);

            Assert.NotNull(val3);
            Assert.Equal(Value3, val3);
        }

        [Fact]
        public void Remove_ValueIsRemoved()
        {
            _cache.Remove(Key1);

            var val1 = _cache.Retrieve(Key1);

            Assert.Null(val1);
        }

        [Fact]
        public void Configure_NewValueStaysFor1500ms_ButFailsFor2000ms()
        {
            var param = new Dictionary<string, object> {{"timeout", 2000}};
            var config = new ConfigParams(param);

            _cache.Configure(config);

            _cache.Store(Key3, Value3, 0);

            var val3 = _cache.Retrieve(Key3);

            Assert.NotNull(val3);
            Assert.Equal(Value3, val3);

            Task.Delay(2000).Wait();

            val3 = _cache.Retrieve(Key3);

            Assert.Null(val3);
        }
    }
}
