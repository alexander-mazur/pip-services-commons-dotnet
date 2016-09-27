using System.Collections.Generic;

namespace PipServices.Commons.Net
{
    public interface IDiscovery
    {
        void Register(ConnectionParams endpoint);
        ConnectionParams Resolve(IList<ConnectionParams> endpoints);
        IList<ConnectionParams> ResolveAll(IList<ConnectionParams> endpoints);
    }
}