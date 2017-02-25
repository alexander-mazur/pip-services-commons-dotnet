using PipServices.Commons.Refer;

namespace PipServices.Commons.Config
{
    public class MemoryConfigReader: IConfigReader, IReconfigurable
    {
        protected ConfigParams _config = new ConfigParams();

        public MemoryConfigReader(ConfigParams config = null)
        {
            _config = config ?? new ConfigParams();
        }

        public virtual void Configure(ConfigParams config)
        {
            _config = config;
        }

        public virtual ConfigParams ReadConfig(string correlationId)
        {
            return new ConfigParams(_config);
        }

        public virtual ConfigParams ReadConfigSection(string correlationId, string section)
        {
            return _config != null ? _config.GetSection(section) : null;
        }
    }
}
