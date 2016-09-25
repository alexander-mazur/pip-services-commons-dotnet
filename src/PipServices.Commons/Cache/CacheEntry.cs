using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipServices.Commons.Cache
{
    /// <summary>
    /// Holds cached value for in-memory cache.
    /// </summary>
    public class CacheEntry
    {
        /// <summary>
        /// Gets the time of expiration, in ticks.
        /// </summary>
        public long Expiration { get; }
        public string Key { get; }
        public object Value { get; set; }

        /// <summary>
        /// Creates an instance of the cache entry.
        /// </summary>
        /// <param name="key">Unique key used to identify the value.</param>
        /// <param name="value">Cached value.</param>
        /// <param name="timeout">Time to live for the cached object in milliseconds.</param>
        public CacheEntry(string key, object value, long timeout)
        {
            Key = key;
            Value = value;
            Expiration = DateTime.Now.Ticks + timeout * TimeSpan.TicksPerMillisecond;
        }

        /// <summary>
        /// Checks if the object expired.
        /// </summary>
        /// <returns><code>True</code> if expired.</returns>
        public bool IsExpired()
        {
            return Expiration < DateTime.Now.Ticks;
        }
    }
}