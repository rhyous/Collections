using System.Threading.Tasks;

namespace Rhyous.Collections
{
    /// <summary>A base class for caching data.</summary>
    /// <typeparam name="T">The type of collection. Must be IClearable and ICountable.</typeparam>
    public interface ICacheBase<T> where T : IClearable, ICountable
    {
        /// <summary>Clears the cache. This will cause it to reload on next request.</summary>
        void Clear();
        /// <summary>Provides the cache. Creates the cache if not created or empty.</summary>
        /// <param name="forceUpdate">Default = false. Updates the cache if true.</param>
        /// <returns>The cache.</returns>
        Task<T> ProvideAsync(bool forceUpdate = false);
    }
}