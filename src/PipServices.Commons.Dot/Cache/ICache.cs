namespace PipServices.Commons.Cache
{
    // Todo: Add correlation ids

    /// <summary>
    /// Transient cache used to bypass persistence to increase system performance.
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Retrieves a value from cache by unique key.
        /// </summary>
        /// <param name="key">Unique key identifying a data object.</param>
        /// <returns>Cached value or null if the value is not found./returns>
        object Retrieve(string key);

        /// <summary>
        /// Stores an object identified by a unique key in cache.
        /// </summary>
        /// <param name="key">Unique key identifying a data object.</param>
        /// <param name="value">The data object to store.</param>
        /// <param name="timeout">Time to live for the object in milliseconds.</param>
        /// <returns><Cached object stored in the cache./returns>
        object Store(string key, object value, long timeout);

        /// <summary>
        /// Removes an object from cache.
        /// </summary>
        /// <param name="key">Unique key identifying the object.</param>
        void Remove(string key);

    }
}
