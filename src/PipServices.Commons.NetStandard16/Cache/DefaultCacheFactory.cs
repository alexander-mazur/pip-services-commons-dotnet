using PipServices.Commons.Build;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Cache
{
    /// <summary>
    /// Default factory for cache components
    /// </summary>
    /// <seealso cref="PipServices.Commons.Build.IFactory" />
    public class DefaultCacheFactory : Factory
    {
        public static Descriptor Descriptor = new Descriptor("pip-services-commons", "factory", "cache", "default", "1.0");
        public static Descriptor NullCacheDescriptor = new Descriptor("pip-services-commons", "cache", "null", "*", "1.0");
        public static Descriptor MemoryCacheDescriptor = new Descriptor("pip-services-commons", "cache", "memory", "*", "1.0");

        public DefaultCacheFactory()
        {
            RegisterAsType(MemoryCacheDescriptor, typeof(MemoryCache));
            RegisterAsType(NullCacheDescriptor, typeof(NullCache));
	    }
    }
}
