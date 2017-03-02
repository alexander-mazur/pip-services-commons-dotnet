using PipServices.Commons.Build;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Auth
{
    public class DefaultCredentialStoreFactory: Factory
    {
        public static readonly Descriptor Descriptor = new Descriptor("pip-services-commons", "factory", "credential-store", "default", "1.0");
        public static readonly Descriptor MemoryCredentialStoreDescriptor = new Descriptor("pip-services-commons", "credential-store", "memory", "*", "1.0");

        public DefaultCredentialStoreFactory()
        {
            RegisterAsType(MemoryCredentialStoreDescriptor, typeof(MemoryCredentialStore));
	    }	
    }
}
