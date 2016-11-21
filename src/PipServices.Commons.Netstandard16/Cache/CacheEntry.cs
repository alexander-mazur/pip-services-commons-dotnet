using System;

namespace PipServices.Commons.Cache
{
    /// <summary>
    /// Holds cached value for in-memory cache.
    /// </summary>
    public class CacheEntry
    {
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
            Expiration = Environment.TickCount + timeout;
        }

        public string Key { get; }
        public object Value { get; private set; }
        public long Expiration { get; private set; }

        public void SetValue(object value, long timeout)
        {
            Value = value;
            Expiration = Environment.TickCount + timeout;
        }

        /// <summary>
        /// Checks if the object expired.
        /// </summary>
        /// <returns><code>True</code> if expired.</returns>
        public bool IsExpired()
        {
            return Expiration < Environment.TickCount;
        }
    }
}