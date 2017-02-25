using System;

namespace PipServices.Commons.Config
{
    public abstract class CachedConfigReader: IConfigReader, IReconfigurable
    {
        private long _lastRead = 0;
        private ConfigParams _config;

        public CachedConfigReader()
        {
            Timeout = 60000;
        }

        public long Timeout { get; set; }

        public virtual void Configure(ConfigParams config)
        {
            Timeout = config.GetAsLongWithDefault("timeout", Timeout);
        }

        protected abstract ConfigParams PerformReadConfig(string correlationId);

        public ConfigParams ReadConfig(string correlationId)
        {
            if (_config != null && Environment.TickCount < _lastRead + Timeout)
                return _config;

            _config = PerformReadConfig(correlationId);
            _lastRead = Environment.TickCount;

            return _config;
        }

        public ConfigParams ReadConfigSection(string correlationId, string section)
        {
            var config = ReadConfig(correlationId);
            return config != null ? config.GetSection(section) : null;
        }
    }
}
