using PipServices.Commons.Config;
using PipServices.Commons.Refer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PipServices.Commons.Cache
{
    /// <summary>
    /// Local in-memory cache that can be used in non-scaled deployments or for testing.
    /// </summary>
    /// <remarks>
    /// This class is thread-safe.
    /// </remarks>
    public class MemoryCache : ICache, IDescriptable, IReconfigurable
    {
        public static readonly Descriptor Descriptor = new Descriptor("pip-services-common", "cache", "memory", "1.0");

        private readonly long DefaultTimeout = 60000;
        private const long DefaultMaxSize = 1000;

        private readonly Dictionary<string, CacheEntry> _cache = new Dictionary<string, CacheEntry>();
        private long _timeout, _maxSize;
        private readonly object _lock = new object();

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

        private void Cleanup()
        {
            CacheEntry oldest = null;
            var keysToRemove = new List<string>();

            lock (_lock)
            {
                foreach (var entry in _cache)
                {
                    if (entry.Value.IsExpired())
                    {
                        keysToRemove.Add(entry.Key);
                    }
                    if (oldest == null || oldest.Expiration > entry.Value.Expiration)
                    {
                        oldest = entry.Value;
                    }
                }

                foreach (var key in keysToRemove)
                {
                    _cache.Remove(key);
                }

                if (_cache.Count > _maxSize && oldest != null)
                {
                    _cache.Remove(oldest.Key);
                }
            }
        }

        /// <summary>
        /// Retrieves a value from cache by unique key.
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="key">Unique key identifying a data object.</param>
        /// <returns>Cached value or null if the value is not found.</returns>
        public async Task<object> RetrieveAsync(string correlationId, string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            await Task.Delay(0);

            lock (_lock)
            {
                CacheEntry entry;
                if (_cache.TryGetValue(key, out entry))
                {
                    if (entry.IsExpired())
                    {
                        _cache.Remove(key);
                        return null;
                    }

                    return entry.Value;
                }

                return null;
            }
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
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            lock (_lock)
            {
                CacheEntry entry;
                _cache.TryGetValue(key, out entry);
                timeout = timeout > 0 ? timeout : _timeout;

                if (value == null)
                {
                    if (entry != null)
                        _cache.Remove(key);
                    return null;
                }

                if (entry != null)
                    entry.SetValue(value, timeout);
                else
                    _cache[key] = new CacheEntry(key, value, timeout);

                // cleanup
                if (_maxSize > 0 && _cache.Count > _maxSize)
                    Cleanup();
            }

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

            lock (_lock)
            {
                _cache.Remove(key);
            }

            await Task.Delay(0);
        }

        public async Task ClearAsync(string correlationId)
        {
            lock (_lock)
            {
                _cache.Clear();
            }

            await Task.Delay(0);
        }
    }
}