using System.Collections.Generic;
using System.Threading.Tasks;

namespace PipServices.Commons.Net
{
    public interface IDiscovery
    {
        Task RegisterAsync(ConnectionParams endpoint);
        Task<ConnectionParams> ResolveAsync(IList<ConnectionParams> endpoints);
        Task<IList<ConnectionParams>> ResolveAllAsync(IList<ConnectionParams> endpoints);
    }
}