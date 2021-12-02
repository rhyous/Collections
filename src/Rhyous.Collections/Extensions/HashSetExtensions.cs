using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.Collections
{
    /// <summary>Extension methods for HashSet{T}.</summary>
    public static class HashSetExtensions
    {
        /// <summary>Provides an AddRange with an action.</summary>
        /// <typeparam name="T">The type of item in the HashSet.</typeparam>
        /// <param name="hashSet">The list.</param>
        /// <param name="items">The items to add.</param>
        public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> items)
        {
            hashSet.ThrowIfNull(nameof(hashSet));
            items.ThrowIfNull(nameof(items));
            var itemsList = items.ToList(); //Enumerate only once
            if (!items.Any())
                return;
            foreach (var item in itemsList)
            {
                hashSet.Add(item);
            }
        }

        /// <summary>Provides an AddRange with an action.</summary>
        /// <typeparam name="T">The type of item in the HashSet.</typeparam>
        /// <param name="hashSet">The list.</param>
        /// <param name="items">The items to add.</param>
        /// <param name="onAddAction">Optional. The action to run after the items are added. By default there is no action.</param>
        /// <remarks>The action is run one times, once for all items.</remarks>
        public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> items, Action<IEnumerable<T>> onAddAction)
        {
            hashSet.ThrowIfNull(nameof(hashSet));
            items.ThrowIfNull(nameof(items));
            var itemsList = items.ToList(); //Enumerate only once
            if (!items.Any())
                return;
            foreach (var item in itemsList)
            {
                hashSet.Add(item);
            }
            onAddAction?.Invoke(itemsList);
        }
    }
}
