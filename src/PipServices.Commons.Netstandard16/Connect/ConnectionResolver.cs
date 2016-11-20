using PipServices.Commons.Config;
using PipServices.Commons.Errors;
using PipServices.Commons.Refer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PipServices.Commons.Connect
{
    public sealed class ConnectionResolver
    {
        private readonly IList<ConnectionParams> _connections = new List<ConnectionParams>();
        private IReferences _references;

        public ConnectionResolver(ConfigParams config = null, IReferences references = null)
        {
            if (config != null) Configure(config);
            if (references != null) SetReferences(references);
        }

        public void SetReferences(IReferences references)
        {
            _references = references;
        }

        public void Configure(ConfigParams config)
        {
            // Try to get multiple connections first
            var connections = config.GetSection("connections");

            if (connections.Count > 0)
            {
                var connectionSections = connections.GetSectionNames();

                foreach (var section in connectionSections)
                {
                    var connection = connections.GetSection(section);
                    _connections.Add(new ConnectionParams(connection));
                }
            }
            // Then try to get a single connection
            else
            {
                var connection = config.GetSection("connection");
                _connections.Add(new ConnectionParams(connection));
            }
        }

        public IEnumerable<ConnectionParams> GetAll()
        {
            return _connections;
        }

        public void Add(ConnectionParams connection)
        {
            _connections.Add(connection);
        }

        private async Task<bool> RegisterInDiscoveryAsync(string correlationId, ConnectionParams connection)
        {
            if (!connection.UseDiscovery) return false;

            var key = connection.DiscoveryKey;
            if (_references == null) return false;

            var components = _references.GetOptional(new Descriptor("*", "discovery", "*", "*"));
            if (components == null) return false;

            foreach (var component in components)
            {
                var discovery = component as IDiscovery;
                if (discovery != null)
                {
                    await discovery.RegisterAsync(correlationId, key, connection);
                }
            }

            return true;
        }

        public async Task RegisterAsync(string correlationId, ConnectionParams connection)
        {
            var result = await RegisterInDiscoveryAsync(correlationId, connection);

            if (result)
                _connections.Add(connection);
        }

        private async Task<ConnectionParams> ResolveInDiscoveryAsync(string correlationId, ConnectionParams connection)
        {
            if (connection.UseDiscovery == false)
                return null;

            var key = connection.DiscoveryKey;
            if (_references == null) return null;

            var components = _references.GetOptional(new Descriptor("*", "discovery", "*", "*"));
            if (components.Count == 0)
                throw new ConfigException(correlationId, "CANNOT_RESOLVE", "Discovery wasn't found to make resolution");

            foreach (var component in components)
            {
                var discovery = component as IDiscovery;
                if (discovery != null)
                {
                    var resolvedConnection = await discovery.ResolveOneAsync(correlationId, key);
                    if (resolvedConnection != null)
                        return resolvedConnection;
                }
            }

            return null;
        }

        public async Task<ConnectionParams> ResolveAsync(string correlationId)
        {
            if (_connections.Count == 0) return null;

            // Return connection that doesn't require discovery
            foreach (var connection in _connections)
            {
                if (!connection.UseDiscovery)
                    return connection;
            }

            // Return connection that require discovery
            foreach (var connection in _connections)
            {
                if (connection.UseDiscovery)
                {
                    var resolvedConnection = await ResolveInDiscoveryAsync(correlationId, connection);
                    if (resolvedConnection != null)
                    {
                        // Merge configured and new parameters
                        resolvedConnection = new ConnectionParams(ConfigParams.MergeConfigs(connection, resolvedConnection));
                        return resolvedConnection;
                    }
                }
            }

            return null;
        }

        private async Task<List<ConnectionParams>> ResolveAllInDiscoveryAsync(string correlationId, ConnectionParams connection)
        {
            var result = new List<ConnectionParams>();

            if (connection.UseDiscovery == false)
                return result;

            var key = connection.DiscoveryKey;
            if (_references == null) return null;

            var components = _references.GetOptional(new Descriptor("*", "discovery", "*", "*"));
            if (components.Count == 0)
                throw new ConfigException(correlationId, "CANNOT_RESOLVE", "Discovery wasn't found to make resolution");

            foreach (var component in components)
            {
                var discovery = component as IDiscovery;
                if (discovery != null)
                {
                    var resolvedConnections = await discovery.ResolveAllAsync(correlationId, key);
                    if (resolvedConnections != null)
                        result.AddRange(resolvedConnections);
                }
            }

            return result;
        }

        public async Task<List<ConnectionParams>> ResolveAllAsync(string correlationId)
        {
            var resolved = new List<ConnectionParams>();
            var toResolve = new List<ConnectionParams>();

            // Sort connections
            foreach (var connection in _connections)
            {
                if (connection.UseDiscovery)
                    toResolve.Add(connection);
                else
                    resolved.Add(connection);
            }

            // Resolve addresses that require that
            if (toResolve.Count <= 0)
                return resolved;

            foreach (var connection in toResolve)
            {
                var resolvedConnections = await ResolveAllInDiscoveryAsync(correlationId, connection);

                foreach (var resolvedConnection in resolvedConnections)
                {
                    // Merge configured and new parameters
                    var localResolvedConnection = new ConnectionParams(ConfigParams.MergeConfigs(connection, resolvedConnection));
                    resolved.Add(localResolvedConnection);
                }
            }

            return resolved;
        }
    }
}
