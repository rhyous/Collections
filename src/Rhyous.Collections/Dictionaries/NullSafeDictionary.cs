using System;
using System.Collections;
using System.Collections.Generic;

namespace Rhyous.Collections
{
    /// <summary>
    /// This class wraps a dictionary, but if a key that does not exist is provided to the indexer,
    /// then instead of an exception, Activate.CreateInstance() is returned.
    /// If you need to instantiate the object differently, inherit this class and override the DefaultValueProvider method.
    /// </summary>
    public class NullSafeDictionary<TKey, TValue> : IDictionaryDefaultValueProvider<TKey, TValue>
    {
        internal IDictionary<TKey, TValue> _Dictionary = new Dictionary<TKey, TValue>();


        /// <summary>The default constructor.</summary>
        public NullSafeDictionary() { }

        /// <summary>The constructor that takes in the default value provider method.</summary>
        /// <param name="defaultValueProviderMethod">A method that provides the default value.</param>
        public NullSafeDictionary(Func<TKey, TValue> defaultValueProviderMethod)
        {
            _DefaultValueProviderMethod = defaultValueProviderMethod; _DefaultValueProviderMethod = defaultValueProviderMethod;
        }

        /// <summary>
        /// This indexer does not throw an System.ArgumentNullException, but instead instantiates the object with the default empty constructor.
        /// If you need to instantiate the object differently, replace the DefaultValueProviderMethod or inherit and override.
        /// </summary>
        /// <param name="key">The key to access</param>
        /// <returns>The TValue at the TKey position, otherwise an object instantiated via the default empty constructor.</returns>
        public virtual TValue this[TKey key]
        {
            get
            {
                if (_Dictionary.TryGetValue(key, out TValue value))
                    return value;
                return _Dictionary[key] = DefaultValueProvider(key);
            }
            set { _Dictionary[key] = value; }
        }

        /// <summary>Adds a list of Keys and assigns them each the default value.</summary>
        /// <param name="keys">The keys to add.</param>
        public virtual void AddKeys(IEnumerable<TKey> keys)
        {
            foreach (var key in keys)
            {
                Add(key, DefaultValueProvider(key));
            }
        }

        /// <summary>Provides a default value.</summary>
        public virtual TValue DefaultValueProvider(TKey key) => DefaultValueProviderMethod(key);

        /// <summary>A method that provides the default value.</summary>
        public virtual Func<TKey, TValue> DefaultValueProviderMethod
        {
            get { return _DefaultValueProviderMethod ?? (_DefaultValueProviderMethod => CreateInstance()); }
            set { _DefaultValueProviderMethod = value; }
        } private Func<TKey, TValue> _DefaultValueProviderMethod;

        private static TValue CreateInstance()
        {
            if (typeof(TValue) == typeof(string))
                return (TValue)Activator.CreateInstance(typeof(TValue), new char[0]);
            return (TValue)Activator.CreateInstance(typeof(TValue));
        }
        #region Interface

        /// <summary>Return a collection of the keys.</summary>
        public virtual ICollection<TKey> Keys => _Dictionary.Keys;

        /// <summary>Return a collection of the values.</summary>
        public virtual ICollection<TValue> Values => _Dictionary.Values;

        /// <summary>Return the count of items in the dictionary.</summary>
        public virtual int Count => _Dictionary.Count;

        /// <summary>Returns whether the dictionary is readonly.</summary>
        public virtual bool IsReadOnly => _Dictionary.IsReadOnly;

        /// <summary>Gets an instance of the default value.</summary>
        public virtual TValue DefaultValue => default(TValue);

        /// <summary>Returns true if the key exists, false otherwise.</summary>
        /// <param name="key">The keys to check.</param>
        /// <returns>True if the key was found, false otherwise.</returns>
        public virtual bool ContainsKey(TKey key) => _Dictionary.ContainsKey(key);

        /// <summary>Adds the key and value to the dictionary.</summary>
        /// <param name="key">The key to add.</param>
        /// <param name="value">The value to add.</param>
        public virtual void Add(TKey key, TValue value) => _Dictionary.Add(key, value);

        /// <summary>Removes the key and value from the dictionary.</summary>
        /// <param name="key">The key to remove.</param>
        /// <returns>True if the key was removed, false if not removed or not found.</returns>
        public virtual bool Remove(TKey key) => _Dictionary.Remove(key);

        /// <summary>Tries to get the value, and returns true if the key exists, false otherwise. The default value provider is not called.</summary>
        /// <param name="key">The key to get.</param>
        /// <param name="value">The value to set using the out parameter.</param>
        /// <returns>True if the key exists, false otherwise.</returns>
        public virtual bool TryGetValue(TKey key, out TValue value) => _Dictionary.TryGetValue(key, out value);

        /// <summary>Add a KeyValuePair{TKey, TValue} to the dictionary.</summary>
        /// <param name="item">The KeyValuePair{TKey, TValue} to add.</param>
        public virtual void Add(KeyValuePair<TKey, TValue> item) => _Dictionary.Add(item);

        /// <summary>Clears all items from the dictionary.</summary>
        public virtual void Clear() => _Dictionary.Clear();

        /// <summary>Checks if a KeyValuePair{TKey, TValue} is in the dictionary.</summary>
        /// <param name="item">The KeyValuePair{TKey, TValue} to check.</param>
        /// <returns>True if the KeyValuePair{TKey, TValue} exists, false otherwise.</returns>
        public virtual bool Contains(KeyValuePair<TKey, TValue> item) => _Dictionary.Contains(item);

        /// <summary>Copies the items in the dictionary to an array..</summary>
        /// <param name="array">The array to copy the items to.</param>
        /// <param name="arrayIndex">The start index in the array.</param>
        public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => _Dictionary.CopyTo(array, arrayIndex);

        /// <summary>Removes a KeyValuePair{TKey, TValue} if it exists in the dictionary.</summary>
        /// <param name="item">The KeyValuePair{TKey, TValue} to remove.</param>
        /// <returns>True if the KeyValuePair{TKey, TValue} is removed, false if not removed or not found.</returns>
        public virtual bool Remove(KeyValuePair<TKey, TValue> item) => _Dictionary.Remove(item);

        /// <summary>The enumerator.</summary>
        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _Dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _Dictionary.GetEnumerator();
        #endregion
    }
}