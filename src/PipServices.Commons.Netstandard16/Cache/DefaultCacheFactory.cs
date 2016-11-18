using PipServices.Commons.Build;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Cache
{
    public class DefaultCacheFactory : IFactory, IDescriptable
    {
        private static readonly Descriptor ThisDescriptor = new Descriptor("pip-commons", "factory", "cache", "1.0");

        public bool CanCreate(object locator)
        {
            var descriptor = locator as Descriptor;

            if (descriptor == null)
                return false;

            if (descriptor.Match(NullCache.Descriptor))
                return true;

            if (descriptor.Match(MemoryCache.Descriptor))
                return true;

            return false;
        }

        public object Create(object locator)
        {
            var descriptor = locator as Descriptor;

            if (descriptor == null)
                return null;

            if (descriptor.Match(NullCache.Descriptor))
                return new NullCache();

            if (descriptor.Match(MemoryCache.Descriptor))
                return new MemoryCache();

            return null;
        }

        public Descriptor GetDescriptor()
        {
            return ThisDescriptor;
        }
    }
}
