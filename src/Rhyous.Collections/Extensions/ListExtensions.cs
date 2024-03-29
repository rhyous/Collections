﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.Collections
{
    /// <summary>Extension methods for IList{T}.</summary>
    public static class ListExtensions
    {
        #region Add
        /// <summary>Provides an add method with an action on add.</summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="item">The item to add.</param>
        /// <param name="onAddAction">The action to run after the item is added.</param>
        public static void Add<T>(this IList<T> list, T item, Action<T> onAddAction = null)
        {
            list.ThrowIfNull(nameof(list));
            list.Add(item);
            onAddAction?.Invoke(item);
        }
        #endregion

        #region Clear
        /// <summary>Provides an clear method with an action.</summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="onClearAction">The action to run. It runs n times, once for each item cleared.</param>
        public static void Clear<T>(this IList<T> list, Action<T> onClearAction = null)
        {
            list.ThrowIfNull(nameof(list));
            var tmpList = list.ToList();
            list.Clear();
            foreach (var item in tmpList)
                onClearAction?.Invoke(item);
        }

        /// <summary>Provides an clear method with a bulk action.</summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="onClearAction">The action to run. It runs 1 time for all items cleared.</param>
        public static void Clear<T>(this IList<T> list, Action<IEnumerable<T>> onClearAction = null)
        {
            list.ThrowIfNull(nameof(list));
            var tmpList = list.ToList();
            list.Clear();
            onClearAction?.Invoke(tmpList);
        }
        #endregion

        #region Insert
        /// <summary>Provides an insert method with an action on insert.</summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index to insert.</param>
        /// <param name="item">The item to insert.</param>
        /// <param name="onInsertAction">The action to run after the item is inserted.</param>
        public static void Insert<T>(this IList<T> list, int index, T item, Action<int, T> onInsertAction = null)
        {
            list.ThrowIfNull(nameof(list));
            list.Insert(index, item);
            onInsertAction?.Invoke(index, item);
        }
        #endregion

        #region Remove
        /// <summary>Provides a remove method with an action on remove.</summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="item">The item to remove.</param>
        /// <param name="onRemoveAction">The action to run after the item is removed.</param>
        public static bool Remove<T>(this IList<T> list, T item, Action<T> onRemoveAction = null)
        {
            list.ThrowIfNull(nameof(list));
            var result = list.Remove(item);
            if (result)
                onRemoveAction?.Invoke(item);
            return result;
        }
        #endregion

        #region RemoveAny
        /// <summary>Provides a remove method for multiple items with an action on remove.</summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="items">The items to remove.</param>
        /// <param name="onRemoveAction">The action to run after the items are removed.</param>
        /// <remarks>It runs 1 time for all removed items. It only runs for removed items. Items not removed because they were not in the list do not run the action.</remarks>
        public static void RemoveAny<T>(this IList<T> list, IEnumerable<T> items, Action<IEnumerable<T>> onRemoveAction = null)
        {
            list.ThrowIfNull(nameof(list));
            if (items == null || !items.Any())
                return;
            var removed = new List<T>();
            foreach (var item in items)
            {
                if (list.Remove(item))
                    removed.Add(item);
            }
            if (onRemoveAction != null)
                onRemoveAction.Invoke(removed);
        }
        #endregion

        #region RemoveAt
        /// <summary>Provides a remove method with an action on remove.</summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index of the item to remove.</param>
        /// <param name="onRemoveAction">The action to run after the item is removed.</param>
        public static void RemoveAt<T>(this IList<T> list, int index, Action<T> onRemoveAction = null)
        {
            list.ThrowIfNull(nameof(list));
            var item = list[index];
            list.RemoveAt(index);
            onRemoveAction?.Invoke(item);
        }
        #endregion

        #region AddRange

        /// <summary>Provides and AddRange to IList. List has it but IList doesn't without this extension.</summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="items">The items to add.</param>
        /// <remarks>This doesn't throw an exception items is null.</remarks>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            list.ThrowIfNull(nameof(list));
            items.ThrowIfNull(nameof(items));
            if (!items.Any())
                return;
            if (list is List<T> concreteList)
            {
                concreteList.AddRange(items);
                return;
            }
            foreach (var item in items)
            {
                list.Add(item);
            }
        }

        /// <summary>Provides and AddRange with an action.</summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="items">The items to add.</param>
        /// <param name="onAddAction">The action to run after each item is added. By default there is no action.</param>
        /// <remarks>The action is run n times, once for each item.</remarks>
        /// <remarks>This doesn't throw an exception if items is null.</remarks>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items, Action<T> onAddAction)
        {
            list.ThrowIfNull(nameof(list));
            items.ThrowIfNull(nameof(items));
            if (!items.Any())
                return;
            var itemsList = items.ToList(); // So we only enumerate once
            if (list is List<T> concreteList)
            {
                concreteList.AddRange(itemsList);
                OnItemAction(itemsList, onAddAction);
                return;
            }
            foreach (var item in itemsList)
            {
                list.Add(item);
                onAddAction?.Invoke(item);
            }
        }

        /// <summary>Provides an AddRange with an action.</summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="items">The items to add.</param>
        /// <param name="onAddAction">The action to run after all items are added. By default there is no action.</param>
        /// <remarks>The action is run one time, once for all items.</remarks>
        /// <remarks>This doesn't throw an exception if items is null.</remarks>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items, Action<IEnumerable<T>> onAddAction)
        {
            list.ThrowIfNull(nameof(list));
            items.ThrowIfNull(nameof(items));
            if (!items.Any())
                return;
            var itemsList = items.ToList(); // So we only enumerate once
            if (list is List<T> concreteList)
            {
                concreteList.AddRange(itemsList);
                onAddAction?.Invoke(itemsList);
                return;
            }
            foreach (var item in itemsList)
            {
                list.Add(item);
            }
            onAddAction?.Invoke(itemsList);
        }
        #endregion

        #region SetIndex
        /// <summary>Sets the item at a specific index to the provided item.</summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index of the item to set.</param>
        /// <param name="item">The item.</param>
        /// <param name="onAddAction">Optional. An action to run on the added item.</param>
        /// <param name="onRemoveAction">Optional. An action to run on the removed item.</param>
        public static void SetIndex<T>(this IList<T> list, int index, T item, Action<T> onAddAction = null, Action<T> onRemoveAction = null)
        {
            list.ThrowIfNull(nameof(list));
            var removedItem = list[index];
            list[index] = item;
            onAddAction?.Invoke(item);
            onRemoveAction?.Invoke(removedItem);
        }
        #endregion

        #region InsertRange
        /// <summary>Adds insert range to IList.</summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index.</param>
        /// <param name="items">The items to insert.</param>
        public static void InsertRange<T>(this IList<T> list, int index, IEnumerable<T> items)
        {
            list.ThrowIfNull(nameof(list));
            items.ThrowIfNull(nameof(items));
            if (!items.Any())
                return;
            var itemsList = items.ToList();
            if (list is List<T> concreteList)
            {
                concreteList.InsertRange(index, itemsList);
                return;
            }
            for (int i = 0; i < itemsList.Count; i++)
            {
                list.Insert(i + index, itemsList[i]);
            }
        }

        /// <summary>Adds insert range to IList with an action after all are inserted.</summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index.</param>
        /// <param name="items">The items to insert.</param>
        /// <param name="onInsertAction">The action to run on insert.</param>
        /// <remarks>Runs the action one times, once for all added items.</remarks>
        public static void InsertRange<T>(this IList<T> list, int index, IEnumerable<T> items, Action<IEnumerable<T>> onInsertAction)
        {
            list.ThrowIfNull(nameof(list));
            items.ThrowIfNull(nameof(items));
            if (!items.Any())
                return;
            var itemsList = items.ToList();
            if (list is List<T> concreteList)
            {
                concreteList.InsertRange(index, itemsList);
                onInsertAction?.Invoke(itemsList);
                return;
            }
            for (int i = 0; i < itemsList.Count; i++)
            {
                list.Insert(i + index, itemsList[i]);
            }
            onInsertAction?.Invoke(itemsList);
        }

        /// <summary>Adds insert range to IList with an action after each is inserted.</summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index.</param>
        /// <param name="items">The items to insert.</param>
        /// <param name="onInsertAction">The action to run on insert.</param>
        /// <remarks>Runs the action n times, once for each added item.</remarks>
        public static void InsertRange<T>(this IList<T> list, int index, IEnumerable<T> items, Action<int, T> onInsertAction)
        {
            list.ThrowIfNull(nameof(list));
            items.ThrowIfNull(nameof(items));
            if (!items.Any())
                return;
            var itemsList = items.ToList();
            if (list is List<T> concreteList)
            {
                concreteList.InsertRange(index, itemsList);
                for (int i = 0; i < itemsList.Count; i++)
                    onInsertAction?.Invoke(i + index, itemsList[i]);
                return;
            }
            for (int i = 0; i < itemsList.Count; i++)
                list.Insert(i + index, itemsList[i], onInsertAction);
        }
        #endregion

        #region GetRange
        /// <summary>Gets a range from an IList{T}. Unlike List{T}, IList{T} does not define GetRange{T}.</summary>
        /// <typeparam name="T">The item type in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index to start getting the range.</param>
        /// <param name="count">The number of items to get.</param>
        /// <returns>An IRangeableList{T} of items.</returns>
        public static IRangeableList<T> GetRange<T>(this IList<T> list, int index, int count)
        {
            list.ThrowIfNull(nameof(list));
            var rangedList = new RangeableList<T>();
            for (int i = 0; i < count; i++)
            {
                rangedList.Add(list[i + index]);
            }
            return rangedList;
        }
        #endregion

        #region RemoveRange
        /// <summary>Removes a range from an IList{T}. Unlike List{T}, IList{T} does not have define RemoveRange{T}.</summary>
        /// <typeparam name="T">The item type in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index to start getting the range.</param>
        /// <param name="count">The number of items to get.</param>
        /// <param name="onRemoveAction">Optional. An action to perform on each item removed from the list before it is removed.</param>
        public static void RemoveRange<T>(this IList<T> list, int index, int count, Action<T> onRemoveAction = null)
        {
            list.ThrowIfNull(nameof(list));
            if (list is List<T> concreteList)
            {
                var range = GetRange(list, index, count);
                concreteList.RemoveRange(index, count);
                OnItemAction(range, onRemoveAction);
                return;
            }
            for (int i = index; i < count; i++)
            {
                var item = list[i];
                list.RemoveAt(i);
                onRemoveAction?.Invoke(item);
            }
        }

        /// <summary>Removes a range from an IList{T}. Unlike List{T}, IList{T} does not have define RemoveRange{T}.</summary>
        /// <typeparam name="T">The item type in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index to start getting the range.</param>
        /// <param name="count">The number of items to get.</param>
        /// <param name="onRemoveAction">Optional. A bulk action to perform on the range of items to remove from the list before they are removed.</param>
        public static void RemoveRange<T>(this IList<T> list, int index, int count, Action<IEnumerable<T>> onRemoveAction = null)
        {
            list.ThrowIfNull(nameof(list));
            if (list is List<T> concreteList)
            {
                var range = GetRange(list, index, count);
                concreteList.RemoveRange(index, count);
                onRemoveAction?.Invoke(range);
                return;
            }
            var removed = new List<T>();
            for (int i = index; i < count; i++)
            {
                removed.Add(list[i]);
                list.RemoveAt(i);
            }
            onRemoveAction?.Invoke(removed);
        }
        #endregion

        #region ToRangeableList
        /// <summary>
        /// Turns an IEnumerable, and more importantly IList, into a list that has range methods,
        /// but better than List, RangeableList's range methods are interface-based.
        /// </summary>
        /// <typeparam name="T">The type of the item in the list.</typeparam>
        /// <param name="list">The items.</param>
        /// <returns>A rangeable list.</returns>
        public static RangeableList<T> ToRangeableList<T>(this IEnumerable<T> list)
        {
            list.ThrowIfNull(nameof(list));
            return new RangeableList<T>(list);
        }

        /// <summary>
        /// Turns an IEnumerable, and more importantly IList, into a list that has range methods,
        /// but better than List, RangeableList's range methods are interface-based.
        /// </summary>
        /// <typeparam name="T">The type of the item in the list.</typeparam>
        /// <typeparam name="TCast">The type to try to cast the item to.</typeparam>
        /// <param name="list">The items.</param>
        /// <returns>A rangeable list.</returns>
        public static RangeableList<TCast> ToRangeableList<T, TCast>(this IEnumerable<T> list)
            where T : TCast
        {
            list.ThrowIfNull(nameof(list));
            return new RangeableList<TCast>(list.Select(i => (TCast)i));
        }
        #endregion

        #region Shuffle
        /// <summary>Shuffles the items in the list in place, so it is the same list object, but the items in the list appear at different indexes.</summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="list">The list to shuffle.</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            Shuffle(list, _RandomNumberGenerator);
        }

        /// <summary>Shuffles the items in the list in place, so it is the same list object, but the items in the list appear at different indexes.</summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="list">The list to shuffle.</param>
        /// <param name="randomNumberGenerator">The random number generator.</param>
        /// <remarks>This internal overload is used for unit tests.</remarks>
        internal static void Shuffle<T>(this IList<T> list, IRandomNumberGenerator randomNumberGenerator)
        {
            int nextLastIndex = list.Count;
            while (nextLastIndex > 1)
            {
                int randId = randomNumberGenerator.Next(nextLastIndex--);
                T value = list[randId];
                list[randId] = list[nextLastIndex];
                list[nextLastIndex] = value;
            }
        } private static readonly IRandomNumberGenerator _RandomNumberGenerator = new RandomNumberGenerator(new Random());

        internal interface IRandomNumberGenerator
        {
            int Next(int maxValue);
        }
        internal class RandomNumberGenerator : IRandomNumberGenerator
        {
            private readonly Random _Random;
            public RandomNumberGenerator(Random random)
            {
                _Random = random;
            }
            public int Next(int maxValue) => _Random.Next(maxValue);
        }
        #endregion

        internal static void OnItemAction<T>(IEnumerable<T> items, Action<T> onItemAction)
        {
            if (onItemAction != null)
            {
                foreach (var item in items)
                {
                    onItemAction(item);
                }
            }
        }
    }
}