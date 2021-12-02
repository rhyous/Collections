using System;

namespace Rhyous.Collections
{
    /// <summary>Extension methods for <see cref="IDictionaryDefaultValueProvider{TKey, TValue}"/>.</summary>
    public static class DictionaryDefaultValueProviderExtensions
    {

        /// <summary>Gets the value or if the value doesn't exist, provides a default value using the method provided.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key to get the value for.</param>
        /// <param name="defaultValueProvider">Optional. The method to provide the default value. If not found, the <see cref="IDictionaryDefaultValueProvider{TKey, TValue}"/>.DefaultValueProvider is used.</param>
        /// <returns>If the key is found, the value assigned to that key is returned. Otherwise, the defaultValueProvider method returns the value. If the defaultValueProvider is null, then you will get default(TValue).</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionaryDefaultValueProvider<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> defaultValueProvider = null)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));
            if (defaultValueProvider == null)
                defaultValueProvider = dictionary.DefaultValueProvider;
            return dictionary.TryGetValue(key, out TValue value) || defaultValueProvider == null
                ? value
                : defaultValueProvider.Invoke(key);
        }
    }
}
