using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Rhyous.Collections
{
    /// <inheritdoc/>
    public class ConcurrentDictionaryWrapper<TKey, TValue> : ConcurrentDictionary<TKey, TValue>, IConcurrentDictionary<TKey, TValue>, IClearable, ICountable
    {
        #region Constructors
        /// <inheritdoc/>
        public ConcurrentDictionaryWrapper()
        {
        }

        /// <inheritdoc/>
        public ConcurrentDictionaryWrapper(IEnumerable<KeyValuePair<TKey, TValue>> collection) : base(collection)
        {
        }

        /// <inheritdoc/>
        public ConcurrentDictionaryWrapper(IEqualityComparer<TKey> comparer) : base(comparer)
        {
        }

        /// <inheritdoc/>
        public ConcurrentDictionaryWrapper(int concurrencyLevel, int capacity) : base(concurrencyLevel, capacity)
        {
        }

        /// <inheritdoc/>
        public ConcurrentDictionaryWrapper(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) : base(collection, comparer)
        {
        }

        /// <inheritdoc/>
        public ConcurrentDictionaryWrapper(int concurrencyLevel, IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) : base(concurrencyLevel, collection, comparer)
        {
        }

        /// <inheritdoc/>
        public ConcurrentDictionaryWrapper(int concurrencyLevel, int capacity, IEqualityComparer<TKey> comparer) : base(concurrencyLevel, capacity, comparer)
        {
        }
        #endregion
    }
}