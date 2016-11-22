﻿using PipServices.Commons.Refer;
using System;
using System.Configuration;

namespace PipServices.Commons.Config
{
    public class AppSettingsConfigReader: IConfigReader, IDescriptable
    {
        public static Descriptor Descriptor = new Descriptor("pip-commons", "config-reader", "app-settings", "1.0");

        public AppSettingsConfigReader() { }

        public Descriptor GetDescriptor()
        {
            throw new NotImplementedException();
        }

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
    }
}
