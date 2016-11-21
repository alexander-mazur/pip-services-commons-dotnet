using PipServices.Commons.Config;
using PipServices.Commons.Refer;
using PipServices.Commons.Run;
using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace PipServices.Commons.Cache
{
    /// <summary>
    /// Local in-memory cache that can be used in non-scaled deployments or for testing.
    /// </summary>
    /// <remarks>
    /// This class is thread-safe.
    /// </remarks>
    public class MemoryCache : ICache, IDescriptable, IReconfigurable, ICleanable
    {
        public static readonly Descriptor Descriptor = new Descriptor("pip-services-common", "cache", "memory", "1.0");

        private readonly long DefaultTimeout = 60000;
        private const long DefaultMaxSize = 1000;

        private readonly System.Runtime.Caching.MemoryCache _standardCache = System.Runtime.Caching.MemoryCache.Default;

        private long _timeout, _maxSize;
        private readonly object _syncObject = new object();

        /// <summary>
        /// Initializes the components according to supplied configuration parameters.
        /// </summary>
        /// <param name="config">Configuration parameters.</param>
        public void Configure(ConfigParams config)
        {
            _timeout = config.GetAsLongWithDefault("timeout", DefaultTimeout);
            _maxSize = config.GetAsLongWithDefault("max_size", DefaultMaxSize);
        }

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
        public async Task<object> RetrieveAsync(string correlationId, string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            return await Task.FromResult(_standardCache.Get(key));
        }

        /// <summary>
        /// Stores an object identified by a unique key in cache.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="key">Unique key identifying a data object.</param>
        /// <param name="value">The data object to store.</param>
        /// <param name="timeout">Time to live for the object in milliseconds.</param>
        public async Task<object> StoreAsync(string correlationId, string key, object value, long timeout)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            // Shortcut to remove entry from the cache
            if (value == null)
            {
                if (_standardCache.Contains(key))
                    _standardCache.Remove(key);
                return null;
            }

            if (_maxSize <= _standardCache.GetCount())
            {
                lock (_syncObject)
                {
                    if (_maxSize <= _standardCache.GetCount())
                        _standardCache.Trim(5);
                }
            }

            timeout = timeout > 0 ? timeout : _timeout;

            _standardCache.Set(key, value, new CacheItemPolicy
            {
                SlidingExpiration = TimeSpan.FromMilliseconds(timeout)
            });

            return await Task.FromResult(value);
        }

        /// <summary>
        /// Removes an object from cache.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="key">Unique key identifying the object.</param>
        public async Task RemoveAsync(string correlationId, string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            _standardCache.Remove(key);
            await Task.Delay(0);
        }

        public async Task ClearAsync(string correlationId)
        {
            _standardCache.Trim(100);
            await Task.Delay(0);
        }
    }
}