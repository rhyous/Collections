using System;
using System.Collections;
using System.Collections.Generic;

namespace Rhyous.Collections
{
    /// <summary>An interface for ConcurrentDictionary</summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <remarks>As Microsoft did not give ConcurrentDictionary an interface, this does, but ConcurrentDictionary doesn't implement it; instead,
    /// use ConcurrentDictionaryWrapper.
    /// </remarks>
    public interface IConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        #region Concurrent Dictionary
        /// <inheritdoc/>
        bool IsEmpty { get; }
        /// <inheritdoc/>
        TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory);
        /// <inheritdoc/>
        TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory);
        /// <inheritdoc/>
        TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory);
        /// <inheritdoc/>
        TValue GetOrAdd(TKey key, TValue value);
        /// <inheritdoc/>
        KeyValuePair<TKey, TValue>[] ToArray();
        /// <inheritdoc/>
        bool TryAdd(TKey key, TValue value);
        /// <inheritdoc/>
        bool TryRemove(TKey key, out TValue value);
        /// <inheritdoc/>
        bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue);
        #endregion
    }
}