using System;
using System.IO;
using Newtonsoft.Json;
using PipServices.Commons.Errors;

namespace PipServices.Commons.Config
{
    public sealed class JsonConfigReader
    {
        public static object ReadObject(string correlationId, string path)
        {
            if (path == null)
                throw new ConfigException(correlationId, "NO_PATH", "Missing config file path");

            try
            {
                using (var reader = new StreamReader(File.OpenRead(path)))
                {
                    var json = reader.ReadToEnd();

                    return JsonConvert.DeserializeObject<dynamic>(json);
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
                    .WithCause(ex.Message);
            }
        }

        public static ConfigParams ReadConfig(string correlationId, string path)
        {
            var value = ReadObject(correlationId, path);
            return ConfigParams.FromObject(value);
        }
    }
}
