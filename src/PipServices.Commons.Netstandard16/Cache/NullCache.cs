using PipServices.Commons.Refer;
using System.Threading.Tasks;

namespace PipServices.Commons.Cache
{
    /// <summary>
    /// Null cache component that doesn't cache at all.
    /// It is primarily used in testing and can be temporarily
    /// used to disable cache for troubleshooting purposes.
    /// </summary>
    public class NullCache : ICache, IDescriptable
    {
        public static readonly Descriptor Descriptor = new Descriptor("pip-services-common", "cache", "null", "1.0");

        /// <summary>
        /// Gets the component descriptor.
        /// </summary>
        /// <returns>The component <see cref="Refer.Descriptor"/></returns>
        public Descriptor GetDescriptor()
        {
            return Descriptor;
        }

        /// <summary>
        /// Retrieves a value from cache by unique key.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="key">Unique key identifying a data object.</param>
        /// <returns>Cached value or null if the value is not found.</returns>
        public async Task<object> RetrieveAsync(string correlationId, string key)
        {
            return await Task.FromResult((object)null);
        }

        /// <summary>
        /// Stores an object identified by a unique key in cache.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="key">Unique key identifying a data object.</param>
        /// <param name="value">The data object to store.</param>
        /// <param name="timeout">Time to live for the object in milliseconds.</param>
        /// <returns>Cached object stored in the cache.</returns>
        public async Task<object> StoreAsync(string correlationId, string key, object value, long timeout)
        {
            return await Task.FromResult(value);
        }

        /// <summary>
        /// Removes an object from cache.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="key">Unique key identifying the object.</param>
        public async Task RemoveAsync(string correlationId, string key)
        {
            await Task.Delay(0);
        }
    }
}