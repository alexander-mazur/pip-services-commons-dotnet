using System;
using System.IO;
using PipServices.Commons.Errors;
using YamlDotNet.Serialization;

namespace PipServices.Commons.Config
{
    public class YamlConfigReader: FileConfigReader
    {
        public YamlConfigReader(string path = null)
            : base(path)
        { }

        public object ReadObject(string correlationId)
        {
            if (Path == null)
                throw new ConfigException(correlationId, "NO_PATH", "Missing config file path");

            try
            {
                using (var reader = new StreamReader(File.OpenRead(Path)))
                {
                    var deserializer = new Deserializer();
                    return deserializer.Deserialize<dynamic>(reader);
                }
            }
            catch (Exception ex)
            {
                throw new FileException(
                    correlationId,
                    "READ_FAILED",
                    "Failed reading configuration " + Path + ": " + ex
                )
                .WithDetails("path", Path)
                .WithCause(ex);
            }
        }

        protected override ConfigParams PerformReadConfig(string correlationId)
        {
            var value = ReadObject(correlationId);
            return ConfigParams.FromValue(value);
        }

        public static object ReadObject(string correlationId, string path)
        {
            return new YamlConfigReader(path).ReadObject(correlationId);
        }

        public static ConfigParams ReadConfig(string correlationId, string path)
        {
            return new YamlConfigReader(path).ReadConfig(correlationId);
        }
    }
}
