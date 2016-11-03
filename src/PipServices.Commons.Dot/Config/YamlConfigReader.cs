using System;
using System.IO;
using PipServices.Commons.Errors;
using YamlDotNet.Serialization;

namespace PipServices.Commons.Config
{
    public sealed class YamlConfigReader
    {
        public static object ReadObject(string correlationId, string path)
        {
            if (path == null)
                throw new ConfigException(correlationId, "NO_PATH", "Missing config file path");

            try
            {
                using (var reader = new StreamReader(File.OpenRead(path)))
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
                    "Failed reading configuration " + path + ": " + ex

                    )
                    .WithDetails("path", path)
                    .WithCause(ex);
            }
        }

        public static ConfigParams ReadConfig(string correlationId, string path)
        {
            var value = ReadObject(correlationId, path);
            return ConfigParams.FromValue(value);
        }
    }
}
