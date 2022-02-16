using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Rhyous.Collections
{
    /// <summary>A wrapper around a ConcurrentDictionary that makes it sorted</summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <remarks>All it does is call OrderBy in the implementation of Enumerable.</remarks>
    public class SortedConcurrentDictionary<TKey, TValue> : IConcurrentDictionary<TKey, TValue>
    {
        private readonly IConcurrentDictionary<TKey, TValue> _CDict = new ConcurrentDictionaryWrapper<TKey, TValue>();

        /// <inheritdoc/>
        public TValue this[TKey key] { get => _CDict[key]; set => _CDict[key] = value; }

        /// <inheritdoc/>
        public bool IsEmpty => _CDict.IsEmpty;

        /// <inheritdoc/>
        public ICollection<TKey> Keys => _CDict.Keys.OrderBy(k => k).ToList();

        /// <inheritdoc/>
        public ICollection<TValue> Values => _CDict.Values;

        /// <inheritdoc/>
        public int Count => _CDict.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => _CDict.IsReadOnly;

        /// <inheritdoc/>
        public void Add(TKey key, TValue value) => _CDict.Add(key, value);

        /// <inheritdoc/>
        public void Add(KeyValuePair<TKey, TValue> item) => _CDict.Add(item);

        /// <inheritdoc/>
        public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
            => _CDict.AddOrUpdate(key, addValueFactory, updateValueFactory);

        /// <inheritdoc/>
        public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
            => _CDict.AddOrUpdate(key, addValue, updateValueFactory);

        /// <inheritdoc/>
        public void Clear() => _CDict.Clear();

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<TKey, TValue> item) => _CDict.Contains(item);

        /// <inheritdoc/>
        public bool ContainsKey(TKey key) => _CDict.ContainsKey(key);

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => _CDict.CopyTo(array, arrayIndex);

        /// <inheritdoc/>
        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory) => _CDict.GetOrAdd(key, valueFactory);

        /// <inheritdoc/>
        public TValue GetOrAdd(TKey key, TValue value) => _CDict.GetOrAdd(key, value);

        /// <inheritdoc/>
        public bool Remove(TKey key) => _CDict.Remove(key);

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<TKey, TValue> item) => _CDict.Remove(item);

        /// <inheritdoc/>
        public KeyValuePair<TKey, TValue>[] ToArray() => _CDict.ToArray();

        /// <inheritdoc/>
        public bool TryAdd(TKey key, TValue value) => _CDict.TryAdd(key, value);

        /// <inheritdoc/>
        public bool TryGetValue(TKey key, out TValue value) => _CDict.TryGetValue(key, out value);

        /// <inheritdoc/>
        public bool TryRemove(TKey key, out TValue value) => _CDict.TryRemove(key, out value);

        /// <inheritdoc/>
        public bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue) => _CDict.TryUpdate(key, newValue, comparisonValue);

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _CDict.OrderBy(kvp => kvp.Key).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}