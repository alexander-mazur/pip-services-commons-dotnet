using PipServices.Commons.Cache;
using Xunit;

namespace PipServices.Commons.Test.Cache
{
    public sealed class NullCacheTest
    {
        private NullCache _cache;
        
        public NullCacheTest()
        {
            _cache = new NullCache();
        }

        [Fact]
        public void GetDescriptor_ReturnsNullCache()
        {
            var descriptor = _cache.GetDescriptor();

            Assert.NotNull(descriptor);
            Assert.Equal("cache", descriptor.Type);
            Assert.Equal("null", descriptor.Id);
        }

        [Fact]
        public void Retrieve_ReturnsNull()
        {
            var val = _cache.Retrieve(null, "key1");

            Assert.Null(val);
        }

        [Fact]
        public void Store_ReturnsSameValue()
        {
            var key = "key1";
            var val = "value1";

            var storedVal = _cache.Store(null, key, val, 0);

            Assert.Equal(val, storedVal);
        }
    }
}
