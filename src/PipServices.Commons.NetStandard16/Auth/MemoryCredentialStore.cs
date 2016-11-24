using PipServices.Commons.Config;
using PipServices.Commons.Refer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PipServices.Commons.Auth
{
    public class MemoryCredentialStore : ICredentialStore, IReconfigurable, IDescriptable
    {
        private Dictionary<string, CredentialParams> _items = new Dictionary<string, CredentialParams>();
        private object _lock = new object();

        public MemoryCredentialStore() { }

        public MemoryCredentialStore(string name, ConfigParams credentials)
        {
            Name = name;
            Init(credentials);
        }

        public string Name { get; set; }

        public virtual Descriptor GetDescriptor()
        {
            return new Descriptor("pip-services-commons", "credential-store", "memory", Name ?? "default", "1.0");
        }

        public virtual void Configure(ConfigParams config)
        {
            Name = NameResolver.Resolve(config, Name);
            Init(config);
        }

        private void Init(ConfigParams credentials)
        {
            lock (_lock)
            {
                _items.Clear();
                foreach (var entry in credentials)
                    _items[entry.Key] = CredentialParams.FromString(entry.Value);
            }
        }

        public async Task StoreAsync(string correlationId, string key, CredentialParams credential)
        {
            lock (_lock)
            {
                if (credential != null)
                    _items[key] = credential;
                else
                    _items.Remove(key);
            }

            await Task.Delay(0);
        }

        public async Task<CredentialParams> LookupAsync(string correlationId, string key)
        {
            CredentialParams credential = null;

            lock (_lock)
            {
                _items.TryGetValue(key, out credential);
            }

            return await Task.FromResult(credential);
        }

    }
}
