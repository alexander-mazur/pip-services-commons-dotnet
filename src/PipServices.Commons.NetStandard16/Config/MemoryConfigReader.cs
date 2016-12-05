using PipServices.Commons.Refer;

namespace PipServices.Commons.Config
{
    public class MemoryConfigReader: IConfigReader, IDescriptable, IReconfigurable
    {
        protected ConfigParams _config = new ConfigParams();

        public MemoryConfigReader(string name = null, ConfigParams config = null)
        {
            Name = name;
            _config = config ?? new ConfigParams();
        }

        public string Name { get; set; }

        public virtual Descriptor GetDescriptor()
        {
            return new Descriptor("pip-services-commons", "config-reader", "memory", Name ?? "default", "1.0");
        }

        public virtual void Configure(ConfigParams config)
        {
            Name = NameResolver.Resolve(config, Name);
            _config = config;
        }

        public virtual ConfigParams ReadConfig(string correlationId)
        {
            return _config;
        }
    }
}
