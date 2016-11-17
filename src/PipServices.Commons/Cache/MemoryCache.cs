using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using PipServices.Commons.Config;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Cache
{
    // Todo: Add correlation ids
    // Todo: Add ICleanable

    /// <summary>
    /// Local in-memory cache that can be used in non-scaled deployments or for testing.
    /// </summary>
    /// <remarks>
    /// This class is thread-safe.
    /// </remarks>
    public class MemoryCache : ICache, IDescriptable, IReconfigurable
    {
        public static Descriptor Descriptor { get; } = new Descriptor("pip-services-common", "cache", "memory",
            "1.0");

        private readonly long DefaultTimeout = 60000;
        private const long DefaultMaxSize = 1000;

        private readonly Dictionary<string, CacheEntry> _cache = new Dictionary<string, CacheEntry>();
        private long _timeout, _maxSize;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        /// <summary>
        /// Creates an instance of local in-memory cache.
        /// </summary>
        public MemoryCache()
        { }

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
        /// Removes an object from cache.
        /// </summary>
        /// <param name="key">Unique key identifying the object.</param>
        public void Remove(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            _lock.EnterWriteLock();
            try
            {
                _cache.Remove(key);
            }
            catch
            {
                // Ignore error
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Retrieves a value from cache by unique key.
        /// </summary>
        /// <param name="key">Unique key identifying a data object.</param>
        /// <returns>Cached value or null if the value is not found.</returns>
        public object Retrieve(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            _lock.EnterReadLock();
            try
            {
                CacheEntry entry;
                if (_cache.TryGetValue(key, out entry))
                {
                    if (entry.IsExpired())
                    {
                        Task.Factory.StartNew(() => { Remove(key); });
                        return null;
                    }
                }
                return entry?.Value;

            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Stores an object identified by a unique key in cache.
        /// </summary>
        /// <param name="key">Unique key identifying a data object.</param>
        /// <param name="value">The data object to store.</param>
        /// <param name="timeout">Time to live for the object in milliseconds.</param>
        /// <returns>Cached object stored in the cache.</returns>
        public object Store(string key, object value, long timeout)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            _lock.EnterWriteLock();
            try
            {
                CacheEntry entry;
                _cache.TryGetValue(key, out entry);
                timeout = timeout > 0 ? timeout : _timeout;

                if (value == null)
                {
                    if (entry != null)
                    {
                        _cache.Remove(key);
                    }
                    return null;
                }

                if (entry != null)
                {
                    entry.Value = value;
                }
                else
                {
                    _cache[key] = new CacheEntry(key, value, timeout);
                }

                // cleanup
                if (_maxSize > 0 && _cache.Count > _maxSize)
                {
                    Task.Factory.StartNew(() => Cleanup());
                }

                return value;

            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        private void Cleanup()
        {
            CacheEntry oldest = null;
            var keysToRemove = new List<string>();

            _lock.EnterWriteLock();
            try
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
            catch
            {
                // Ignore error. TODO: log??
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}