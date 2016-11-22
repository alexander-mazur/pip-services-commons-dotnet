using System;
using System.IO;
using PipServices.Commons.Errors;
using YamlDotNet.Serialization;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Config
{
    public sealed class YamlConfigReader: FileConfigReader, IDescriptable
    {
        public static Descriptor Descriptor = new Descriptor("pip-commons", "config-reader", "yaml", "1.0");

        public YamlConfigReader(string path = null)
            : base(path)
        { }

        public Descriptor GetDescriptor()
        {
            return Descriptor;
        }

        public override ConfigParams ReadConfig(string correlationId)
        {
            return ReadConfig(correlationId, Path);
        }

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
