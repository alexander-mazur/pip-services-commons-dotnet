using PipServices.Commons.Build;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Connect
{
    public class DefaultDiscoveryFactory: Factory
    {
        public static readonly Descriptor Descriptor = new Descriptor("pip-services-commons", "factory", "discovery", "default", "1.0");
        public static readonly Descriptor MemoryDiscoveryDescriptor = new Descriptor("pip-services-commons", "discovery", "memory", "*", "1.0");

        public DefaultDiscoveryFactory()
        {
            RegisterAsType(MemoryDiscoveryDescriptor, typeof(MemoryDiscovery));
	    }	
    }
}
