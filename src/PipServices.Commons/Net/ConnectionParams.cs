using System.Collections.Generic;
using PipServices.Commons.Config;
using PipServices.Commons.Data;

namespace PipServices.Commons.Net
{
    // Todo: Move to pip-services-net
    public class ConnectionParams : ConfigParams
    {
        public ConnectionParams()
        { }

        public ConnectionParams(IDictionary<string, object> map) : base(map)
        { }

        public bool UseDiscovery()
        {
            return ContainsKey("discover") || ContainsKey("discovery");
        }

        public string GetDiscoveryName()
        {
            var discover = GetAsNullableString("discover");
            return discover ?? GetAsNullableString("discovery");
        }

        public void SetDiscoveryName(string value)
        {
            this["discover"] = value;
        }

        public string GetProtocol()
        {
            return GetAsNullableString("protocol");
        }

        public void SetProtocol(string value)
        {
            this["protocol"] = value;
        }

        public string GetHost()
        {
            var host = GetAsNullableString("host");
            host = host ?? GetAsNullableString("ip");
            return host;
        }

        public void SetHost(string value)
        {
            this["host"] = value;
        }

        public int GetPort()
        {
            return GetAsInteger("port");
        }

        public void SetPort(int value)
        {
            SetAsObject("port", value);
        }

        public string GetUsername()
        {
            return GetAsNullableString("username");
        }

        public void SetUsername(string value)
        {
            this["username"] = value;
        }

        public string getPassword()
        {
            return GetAsNullableString("password");
        }

        public void SetPassword(string password)
        {
            this["password"] = password;
        }

        public string GetUri()
        {
            return GetProtocol() + "://" + GetHost() + ":" + GetPort();
        }

        public new static ConnectionParams FromString(string line)
        {
            var map = StringValueMap.FromString(line);
            return new ConnectionParams(map);
        }
    }
}