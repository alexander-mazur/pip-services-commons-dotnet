using PipServices.Commons.Refer;
using System;
using System.Configuration;

namespace PipServices.Commons.Config
{
    public class ConnectionStringsConfigReader : IConfigReader, IDescriptable
    {
        public static Descriptor Descriptor = new Descriptor("pip-services-commons", "config-reader", "connection-strings", "default", "1.0");

        public ConnectionStringsConfigReader() { }

        public Descriptor GetDescriptor()
        {
            throw new NotImplementedException();
        }

        public ConfigParams ReadConfig(string correlationId)
        {
            var result = new ConfigParams();

            for (var index = 0; index < ConfigurationManager.ConnectionStrings.Count; index++)
            {
                var item = ConfigurationManager.ConnectionStrings[index];
                var key = item.Name;
                if (item.ProviderName != null && item.ProviderName.Length > 0)
                    key = item.ProviderName + "." + key;
                var value = item.ConnectionString;

                result.SetAsObject(key, value);
            }

            return result;
        }

        public ConfigParams ReadConfigSection(string correlationId, string section)
        {
<<<<<<< HEAD
            var config = ReadConfig(correlationId);
            return config != null ? config.GetSection(section) : null;
=======
            throw new NotImplementedException();
>>>>>>> b2b93d40d28d1cccf9548b812fe38f1bd350eb54
        }
    }
}
