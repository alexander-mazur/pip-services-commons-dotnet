﻿using System;
using System.IO;
using PipServices.Commons.Errors;
using PipServices.Commons.Convert;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Config
{
    public class JsonConfigReader: FileConfigReader, IDescriptable
    {
        public static Descriptor Descriptor = new Descriptor("pip-commons", "config-reader", "json", "1.0");

        public JsonConfigReader(string path = null)
            : base(path)
        { }

        public virtual Descriptor GetDescriptor()
        {
            return Descriptor;
        }

        public object ReadObject(string correlationId)
        {
            if (Path == null)
                throw new ConfigException(correlationId, "NO_PATH", "Missing config file path");

            try
            {
                using (var reader = new StreamReader(File.OpenRead(Path)))
                {
                    var json = reader.ReadToEnd();
                    return JsonConverter.ToNullableMap(json);
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

        public override ConfigParams ReadConfig(string correlationId)
        {
            var value = ReadObject(correlationId);
            return ConfigParams.FromValue(value);
        }

        public static object ReadObject(string correlationId, string path)
        {
            return new JsonConfigReader(path).ReadObject(correlationId);
        }

        public static ConfigParams ReadConfig(string correlationId, string path)
        {
            return new JsonConfigReader(path).ReadConfig(correlationId);
        }
    }
}
