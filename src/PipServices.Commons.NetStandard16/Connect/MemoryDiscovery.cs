using PipServices.Commons.Config;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PipServices.Commons.Connect
{
    public class MemoryDiscovery : IDiscovery, IReconfigurable
    {
        private List<DiscoveryItem> _items = new List<DiscoveryItem>();
        private object _lock = new object();

        public MemoryDiscovery() { }

        public MemoryDiscovery(ConfigParams config = null)
        {
            if (config != null) Configure(config);
        }

        private class DiscoveryItem
        {
            public string Key;
            public ConnectionParams Connection;
        }

        public virtual void Configure(ConfigParams config)
        {
            ReadConnections(config);
        }

        private void ReadConnections(ConfigParams connections)
        {
            lock (_lock)
            {
                _items.Clear();
                foreach (var entry in connections)
                {
                    var item = new DiscoveryItem()
                    {
                        Key = entry.Key,
                        Connection = ConnectionParams.FromString(entry.Value)
                    };
                    _items.Add(item);
                }
            }
        }

        public async Task RegisterAsync(string correlationId, string key, ConnectionParams connection)
        {
            lock (_lock)
            {
                var item = new DiscoveryItem()
                {
                    Key = key,
                    Connection = connection
                };
                _items.Add(item);
            }

            await Task.Delay(0);
        }

        public async Task<ConnectionParams> ResolveOneAsync(string correlationId, string key)
        {
            ConnectionParams connection = null;

            lock (_lock)
            {
                foreach (var item in _items)
                {
                    if (item.Key == key && item.Connection != null)
                    {
                        connection = item.Connection;
                        break;
                    }
                }
            }

            return await Task.FromResult(connection);
        }

        public async Task<List<ConnectionParams>> ResolveAllAsync(string correlationId, string key)
        {
            var connections = new List<ConnectionParams>();

            lock (_lock)
            {
                foreach (var item in _items)
                {
                    if (item.Key == key && item.Connection != null)
                        connections.Add(item.Connection);
                }
            }

            return await Task.FromResult(connections);
        }
    }
}
