using PipServices.Commons.Build;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Config
{
    public class DefaultConfigReaderFactory: Factory
    {
        public static readonly Descriptor Descriptor = new Descriptor("pip-services-commons", "factory", "config-reader", "default", "1.0");
        public static readonly Descriptor MemoryConfigReaderDescriptor = new Descriptor("pip-services-commons", "config-reader", "memory", "*", "1.0");
        public static readonly Descriptor JsonConfigReaderDescriptor = new Descriptor("pip-services-commons", "config-reader", "json", "*", "1.0");
        public static readonly Descriptor YamlConfigReaderDescriptor = new Descriptor("pip-services-commons", "config-reader", "yaml", "*", "1.0");

        public DefaultConfigReaderFactory()
        {
            RegisterAsType(MemoryConfigReaderDescriptor, typeof(MemoryConfigReader));
            RegisterAsType(JsonConfigReaderDescriptor, typeof(JsonConfigReader));
            RegisterAsType(YamlConfigReaderDescriptor, typeof(YamlConfigReader));
	    }
    }
}
