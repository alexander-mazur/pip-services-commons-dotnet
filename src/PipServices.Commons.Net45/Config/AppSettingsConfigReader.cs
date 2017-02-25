using PipServices.Commons.Refer;
using System;
using System.Configuration;

namespace PipServices.Commons.Config
{
    public class AppSettingsConfigReader: IConfigReader
    {
        public AppSettingsConfigReader() {}

        public ConfigParams ReadConfig(string correlationId)
        {
            var result = new ConfigParams();

            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                var value = ConfigurationManager.AppSettings[key];
                result.SetAsObject(key, value);
            }

            return result;
        }

        public ConfigParams ReadConfigSection(string correlationId, string section)
        {
            var config = ReadConfig(correlationId);
            return config?.GetSection(section);
        }
    }
}
