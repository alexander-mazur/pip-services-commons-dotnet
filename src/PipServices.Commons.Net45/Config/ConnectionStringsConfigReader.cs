﻿using PipServices.Commons.Refer;
using System;
using System.Configuration;

namespace PipServices.Commons.Config
{
    public class ConnectionStringsConfigReader : IConfigReader
    {
        public ConnectionStringsConfigReader() { }

        public ConfigParams ReadConfig(string correlationId)
        {
            var result = new ConfigParams();

            for (var index = 0; index < ConfigurationManager.ConnectionStrings.Count; index++)
            {
                var item = ConfigurationManager.ConnectionStrings[index];
                var key = item.Name;
                if (!string.IsNullOrWhiteSpace(item.ProviderName) && item.ProviderName.Length > 0)
                    key = item.ProviderName + "." + key;
                var value = item.ConnectionString;

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
