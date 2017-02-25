using PipServices.Commons.Build;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Cache
{
    /// <summary>
    /// Default factory for cache components
    /// </summary>
    /// <seealso cref="PipServices.Commons.Build.IFactory" />
    public class DefaultCacheFactory : IFactory
    {
        public static Descriptor Descriptor = new Descriptor("pip-services-commons", "factory", "cache", "default", "1.0");
        public static Descriptor NullCacheDescriptor = new Descriptor("pip-services-commons", "cache", "null", "*", "1.0");
        public static Descriptor MemoryCacheDescriptor = new Descriptor("pip-services-commons", "cache", "memory", "*", "1.0");

        public bool CanCreate(object locator)
        {
            var descriptor = locator as Descriptor;

            if (descriptor == null)
                return false;

            if (descriptor.Match(NullCacheDescriptor))
                return true;

            if (descriptor.Match(MemoryCacheDescriptor))
                return true;

            return false;
        }

        public object Create(object locator)
        {
            var descriptor = locator as Descriptor;

            if (descriptor == null)
                return null;

            if (descriptor.Match(NullCacheDescriptor))
                return new NullCache();

            if (descriptor.Match(MemoryCacheDescriptor))
                return new MemoryCache();

            return null;
        }
    }
}
