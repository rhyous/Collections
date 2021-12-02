using System;
using System.Collections.Generic;

namespace Rhyous.Collections
{
    /// <summary>The interface for a dictionary that returns a default value when a key does not exist.</summary>
    public interface ISharedInterfaceDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        /// <summary>Gets a value from a key, but if the key doesn't exist, it returns a new instance of the value type.</summary>
        T GetValueOrNew<T>(TKey key) where T : class, TValue, new();

        /// <summary>Gets a value from a key, but if the key doesn't exist, it returns a new instance of the value type using the constructor provided.</summary>
        T GetValueOrNew<T, TInput>(TKey key, TInput input1, Func<TInput, T> constructor = null) where T : class, TValue, new();
        /// <summary>Gets a value from a key, but if the key doesn't exist, it returns a new instance of the value type using the constructor provided.</summary>
        T GetValueOrNew<T, T1Input, T2Input>(TKey key, T1Input input1, T2Input input2, Func<T1Input, T2Input, T> constructor = null) where T : class, TValue, new();
    }
}
