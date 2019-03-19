using System;

namespace Rhyous.Collections
{
    public static class DictionaryDefaultValueProviderExtensions
    {
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
