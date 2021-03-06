﻿using System.Collections.Generic;
using PipServices.Commons.Config;
using PipServices.Commons.Data;

namespace PipServices.Commons.Connect
{
    /// <summary>
    /// Connection parameters as set in component configuration or retrieved by discovery service.
    /// It contains service protocol, host, port number, route, database name, timeouts 
    /// and additional configuration parameters.
    /// </summary>
    public class ConnectionParams : ConfigParams
    {
        /// <summary>
        /// Creates an empty instance of connection parameters.
        /// </summary>
        public ConnectionParams() { }

        /// <summary>
        /// Create an instance of service address with free-form configuration map.
        /// </summary>
        /// <param name="map">a map with the address configuration parameters.</param>
        public ConnectionParams(IDictionary<string, string> map)
            : base(map)
        { }

        /// <summary>
        /// Checks if discovery registration or resolution shall be performed.
        /// The discovery is requested when 'discover' parameter contains
        /// a non-empty string that represents the discovery name.
        /// </summary>
        public bool UseDiscovery
        {
            get { return ContainsKey("discovery_key"); }
        }

        /// <summary>
        /// Key under which the connection shall be registered or resolved by discovery service. 
        /// </summary>
        public string DiscoveryKey
        {
            get { return GetAsNullableString("discovery_key"); }
            set { this["discovery_key"] = value; }
        }

        /// <summary>
        /// Gets or sets the connection protocol
        /// </summary>
        public string Protocol
        {
            get { return GetAsNullableString("protocol") ?? "http"; }
            set { this["protocol"] = value; }
        }

        /// <summary>
        /// Gets the connection protocol
        /// </summary>
        /// <param name="defaultValue">defaultValue the default protocol</param>
        /// <returns>the connection protocol</returns>
        public string GetProtocol(string defaultValue)
        {
            return GetAsStringWithDefault("protocol", defaultValue);
        }

        /// <summary>
        /// Gets or sets the service host name or IP address.
        /// </summary>
        public string Host
        {
            get
            {
                var host = GetAsNullableString("host");
                host = host ?? GetAsNullableString("ip");
                return string.IsNullOrWhiteSpace(host) ? "localhost" : host;
            }
            set
            {
                this["host"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the service port number
        /// </summary>
        public int Port
        {
            get { return GetAsInteger("port"); }
            set { SetAsObject("port", value); }
        }

        /// <summary>
        /// Gets the endpoint uri constructed from protocol, host and port
        /// and returned as <protocol>://<host | ip>:<port>
        /// </summary>
        public string Uri
        {
            get { return Protocol + "://" + Host + ":" + Port; }
        }

        public new static ConnectionParams FromString(string line)
        {
            var map = StringValueMap.FromString(line);
            return new ConnectionParams(map);
        }

        public static List<ConnectionParams> ManyFromConfig(ConfigParams config, bool configAsDefault = true)
        {
            var result = new List<ConnectionParams>();

            // Try to get multiple connections first
            var connections = config.GetSection("connections");

            if (connections.Count > 0)
            {
                var connectionSections = connections.GetSectionNames();

                foreach (var section in connectionSections)
                {
                    var connection = connections.GetSection(section);
                    result.Add(new ConnectionParams(connection));
                }
            }
            // Then try to get a single connection
            else
            {
                var connection = config.GetSection("connection");
                if (connection.Count > 0)
                    result.Add(new ConnectionParams(connection));
                // Apply default if possible
                else if (configAsDefault)
                    result.Add(new ConnectionParams(config));
            }

            return result;
        }

        public static ConnectionParams FromConfig(ConfigParams config, bool configAsDefault = true)
        {
            var connections = ManyFromConfig(config, configAsDefault);
            return connections.Count > 0 ? connections[0] : null;
        }
    }
}