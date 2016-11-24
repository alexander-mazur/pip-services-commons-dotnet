using System.Collections.Generic;
using PipServices.Commons.Config;
using PipServices.Commons.Refer;
using System.Threading.Tasks;

namespace PipServices.Commons.Auth
{
    public sealed class CredentialResolver
    {
        private readonly List<CredentialParams> _credentials = new List<CredentialParams>();
        private IReferences _references = null;

        public CredentialResolver(ConfigParams config = null, IReferences references = null)
        {
            if (config != null) Configure(config);
            if (references != null) SetReferences(references);
        }

        public void SetReferences(IReferences references)
        {
            _references = references;
        }

        public void Configure(ConfigParams config, bool configAsDefault = true)
        {
            _credentials.AddRange(CredentialParams.ManyFromConfig(config, configAsDefault));
        }

        public List<CredentialParams> GetAll()
        {
            return _credentials;
        }

        public void Add(CredentialParams connection)
        {
            _credentials.Add(connection);
        }

        private async Task<CredentialParams> LookupInStoresAsync(string correlationId, CredentialParams credential)
        {
            if (credential.UseCredentialStore == false) return null;

            var key = credential.StoreKey;
            if (_references == null) return null;

            var components = _references.GetOptional(new Descriptor("*", "credential_store", "*", "*"));
            if (components.Count == 0)
                throw new ReferenceException(correlationId, "Credential store wasn't found to make lookup");

            foreach (var component in components)
            {
                var store = component as ICredentialStore;
                if (store != null)
                {
                    var resolvedCredential = await store.LookupAsync(correlationId, key);
                    if (resolvedCredential != null)
                        return resolvedCredential;
                }
            }

            return null;
        }

        public async Task<CredentialParams> LookupAsync(string correlationId)
        {
            if (_credentials.Count == 0) return null;

            // Return connection that doesn't require discovery
            foreach (var credential in _credentials)
            {
                if (!credential.UseCredentialStore)
                    return credential;
            }

            // Return connection that require discovery
            foreach (var credential in _credentials)
            {
                if (credential.UseCredentialStore)
                {
                    var resolvedConnection = await LookupInStoresAsync(correlationId, credential);
                    if (resolvedConnection != null)
                        return resolvedConnection;
                }
            }

            return null;
        }
    }
}
