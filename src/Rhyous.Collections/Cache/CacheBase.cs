using System.Threading.Tasks;

namespace Rhyous.Collections
{
    /// <summary>A base class for caching data.</summary>
    /// <typeparam name="T">The type of collection. Must be IClearable and ICountable.</typeparam>
    public abstract class CacheBase<T> : ICacheBase<T> where T : IClearable, ICountable
    {
        /// <summary>The underlying cache backing field</summary>
        protected readonly T _Cache;

        /// <summary>The Cache constructor. Must take in an instance of the cache collection.</summary>
        /// <param name="cache"></param>
        public CacheBase(T cache)
        {
            _Cache = cache;
        }

        /// <summary>Clears the cache. This will cause it to reload on next request.</summary>
        public virtual void Clear()
        {
            _Cache.Clear();
        }

        /// <summary>Provides the cache. Creates the cache if not created or empty.</summary>
        /// <param name="forceUpdate">Default = false. Updates the cache if true.</param>
        /// <returns>The cache.</returns>
        public virtual async Task<T> ProvideAsync(bool forceUpdate = false)
        {
            if (forceUpdate)
                Clear();
            if (_Cache.Count == 0)
                await CreateCacheAsync();
            return _Cache;
        }

        /// <summary>This creates the cache. This should only be called one time
        /// or whenever and update is forced.</summary>
        /// <returns></returns>
        protected abstract Task CreateCacheAsync();
    }
}
