using System.Collections.Generic;
using System.Threading.Tasks;

namespace PipServices.Commons.Connect
{
    /// <summary>
    /// Service discovery component used to register connections of the services
    /// or to resolve connections to external services called by clients.
    /// </summary>
    public interface IDiscovery
    {
        /// <summary>
        /// Registers connection where API service binds to.
        /// </summary>
        /// <param name="correlationId">a unique transaction id to trace calls across components</param>
        /// <param name="key">a key to identify the connection</param>
        /// <param name="connection">the connection to be registered.</param>
        /// <param name="token"></param>
        Task RegisterAsync(string correlationId, string key, ConnectionParams connection);

        /// <summary>
        /// Resolves one connection from the list of service connections.
        /// </summary>
        /// <param name="correlationId">a unique transaction id to trace calls across components</param>
        /// <param name="key">a key locate a connection</param>
        /// <returns>a resolved connection.</returns>
        Task<ConnectionParams> ResolveOneAsync(string correlationId, string key);

        /// <summary>
        /// Resolves a list of connections from to be called by a client.
        /// </summary>
        /// <param name="correlationId">a unique transaction id to trace calls across components</param>
        /// <param name="key">a key locate connections</param>
        /// <returns>a list with resolved connections.</returns>
        Task<List<ConnectionParams>> ResolveAllAsync(string correlationId, string key);
    }
}
