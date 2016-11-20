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
            var value = _cache.StoreAsync(null, Key1, Value1, 1000).Result;
            value = _cache.StoreAsync(null, Key2, Value2, 1000).Result;
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

            var val1 = _cache.RetrieveAsync(null, Key1).Result;
            var val2 = _cache.RetrieveAsync(null, Key2).Result;

            Assert.NotNull(val1);
            Assert.Equal(Value1, val1);

            Assert.NotNull(val2);
            Assert.Equal(Value2, val2);
        }

        [Fact]
        public void Retrieve_BothValue_In1000ms_Fails()
        {
            Task.Delay(1000).Wait();

            var val1 = _cache.RetrieveAsync(null, Key1).Result;
            var val2 = _cache.RetrieveAsync(null, Key2).Result;

            //Assert.Null(val1);
            //Assert.Null(val2);
        }

        [Fact]
        public void Store_ReturnsSameValue()
        {
            var storedVal = _cache.StoreAsync(null, Key3, Value3, 0).Result;
            Assert.Equal(Value3, storedVal);
        }

        [Fact]
        public void Store_ValueIsStored()
        {
            var value = _cache.StoreAsync(null, Key3, Value3, 1000).Result;
            var val3 = _cache.RetrieveAsync(null, Key3).Result;

            Assert.NotNull(val3);
            Assert.Equal(Value3, val3);
        }

        [Fact]
        public void Remove_ValueIsRemoved()
        {
            _cache.RemoveAsync(null, Key1).Wait();

            var val1 = _cache.RetrieveAsync(null, Key1).Result;
            Assert.Null(val1);
        }

        [Fact]
        public void Configure_NewValueStaysFor1500ms_ButFailsFor2500ms()
        {
            var param = new Dictionary<string, object> { { "timeout", 2000 } };
            var config = new ConfigParams(param);

            _cache.Configure(config);

            var value = _cache.StoreAsync(null, Key3, Value3, 0).Result;
            var val3 = _cache.RetrieveAsync(null, Key3).Result;
            Assert.NotNull(val3);
            Assert.Equal(Value3, val3);

            Task.Delay(2500).Wait();

            val3 = _cache.RetrieveAsync(null, Key3).Result;
            Assert.Null(val3);
        }
    }
}
