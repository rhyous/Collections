using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.Collections
{
    /// <summary>Extension methods for <see cref="IDictionary{TKey, TValue}"/>.</summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// An extension method to allow adding a KeyValuePair to a dictionary.
        /// </summary>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="kvp">The KeyValuePair</param>
        public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, KeyValuePair<TKey, TValue> kvp)
        {
            if (dictionary == null)
                return;
            dictionary.Add(kvp.Key, kvp.Value);
        }

        /// <summary>
        /// An extension method to allow adding a range of KeyValuePairs to a dictionary.
        /// </summary>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="keyValuePairs">The KeyValuePair</param>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            if (dictionary == null || keyValuePairs == null)
                return;
            foreach (var kvp in keyValuePairs)
                dictionary.Add(kvp.Key, kvp.Value);
        }

        /// <summary>
        /// An extension method to allow adding a range of KeyValuePairs to a dictionary.
        /// </summary>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="keyValuePairs">The KeyValuePairs</param>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, params KeyValuePair<TKey, TValue>[] keyValuePairs)
        {
            dictionary.AddRange(keyValuePairs as IEnumerable<KeyValuePair<TKey, TValue>>);
        }

        /// <summary>
        /// Adds a value only if the key doesn't already exist.
        /// This allows for adding a null value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void AddIfNew<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null || dictionary.TryGetValue(key, out _))
                return;
            dictionary.Add(key, value);
        }

        /// <summary>
        /// Adds a value only if the key doesn't already exist.
        /// This prevents adding a null value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The instance of an IDictionary</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void AddIfNewAndNotNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null || value == null || dictionary.TryGetValue(key, out _))
                return;
            dictionary.Add(key, value);
        }

        /// <summary>
        /// Adds a value only if the key doesn't already exist.
        /// This prevents adding a null value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The instance of an IConcurrentDictionary.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>True if added to the dictionary, false otherwise</returns>
        public static bool TryAddIfNotNull<TKey, TValue>(this IConcurrentDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null || value == null)
                return false;
            return dictionary.TryAdd(key, value);
        }

        /// <summary>
        /// Adds a value only if the key doesn't exist, or if the key
        /// already exists, it replaces the value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void AddOrReplace<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null)
                return;
            if (dictionary.TryGetValue(key, out TValue existingValue))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }

        /// <summary>Gets the value or if the value doesn't exist, provides a default value using the method provided.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key to get the value for.</param>
        /// <param name="defaultValueProvider">Optional. The method to provide the default value.</param>
        /// <returns>If the key is found, the value assigned to that key is returned. Otherwise, the defaultValueProvider method returns the value. If the defaultValueProvider is null, then you will get default(TValue).</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> defaultValueProvider = null)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));
            return dictionary.TryGetValue(key, out TValue value) || defaultValueProvider == null
                ? value
                : defaultValueProvider.Invoke(key);
        }

        /// <summary>Gets the value or if the value doesn't exist, provides a default value using the method provided.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key to get the value for.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>If the key is found, the value assigned to that key is returned. Otherwise, the defaultValueProvider method returns the value. If the defaultValueProvider is null, then you will get default(TValue).</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));
            return dictionary.TryGetValue(key, out TValue value)
                ? value
                : defaultValue;
        }

        /// <summary>Gets the first value or if the value doesn't exist, provides a default value using the method provided.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="kvps">The key value pairs.</param>
        /// <param name="key">The key to get the value for.</param>
        /// <param name="defaultValueProvider">Optional. The method to provide the default value.</param>
        /// <returns>If the key is found, the value assigned to that key is returned. Otherwise, the defaultValueProvider method returns the value. If the defaultValueProvider is null, then you will get default(TValue).</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> kvps, TKey key, Func<TKey, TValue> defaultValueProvider = null)
        {
            if (kvps == null)
                throw new ArgumentNullException(nameof(kvps));
            var foundKvps = kvps.Where(kvp => EqualityComparer<TKey>.Default.Equals(kvp.Key, key)).ToList();
            if (foundKvps.Count == 0)
            {
                return (defaultValueProvider == null)
                     ? default
                     : defaultValueProvider.Invoke(key);
            }
            var value = foundKvps[0].Value;
            if (value == null)
            {
                return (defaultValueProvider == null)
                     ? default
                     : defaultValueProvider.Invoke(key);
            }
            return value;
        }

        /// <summary>Gets the first value or if the value doesn't exist, provides a default value using the method provided.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="kvps">The key value pairs.</param>
        /// <param name="key">The key to get the value for.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>If the key is found, the value assigned to that key is returned. Otherwise, the defaultValueProvider method returns the value. If the defaultValueProvider is null, then you will get default(TValue).</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> kvps, TKey key, TValue defaultValue)
        {
            if (kvps == null)
                throw new ArgumentNullException(nameof(kvps));
            var foundKvps = kvps.Where(kvp => EqualityComparer<TKey>.Default.Equals(kvp.Key, key)).ToList();
            return foundKvps.Count == 0 || foundKvps[0].Value == null
                 ? defaultValue
                 : foundKvps[0].Value;
        }
    }
}