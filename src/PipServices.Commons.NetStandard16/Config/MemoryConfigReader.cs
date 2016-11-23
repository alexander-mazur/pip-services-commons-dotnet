using PipServices.Commons.Refer;

namespace PipServices.Commons.Config
{
    public class MemoryConfigReader: IConfigReader, IDescriptable, IReconfigurable
    {
        public static Descriptor Descriptor = new Descriptor("pip-commons", "config-reader", "memory", "1.0");
        protected ConfigParams _config = new ConfigParams();

        public MemoryConfigReader() { }

        public MemoryConfigReader(ConfigParams config)
        {
            _config = config ?? new ConfigParams();
        }

        public virtual Descriptor GetDescriptor()
        {
            return Descriptor;
        }

        public virtual void Configure(ConfigParams config)
        {
            _config = config;
        }

        public virtual ConfigParams ReadConfig(string correlationId)
        {
            return _config;
        }
    }
}
