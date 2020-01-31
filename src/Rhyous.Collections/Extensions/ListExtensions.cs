using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.Collections
{
    public static class ListExtensions
    {
        #region Add
        /// <summary>
        /// Provides an add method with an action on add.
        /// </summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="item">The item to add.</param>
        /// <param name="onAddAction">The action to run after the item is added.</param>
        public static void Add<T>(this IList<T> list, T item, Action<T> onAddAction = null)
        {
            ListIsNotNull(list, nameof(list));
            list.Add(item);
            onAddAction?.Invoke(item);
        }
        #endregion

        #region Clear
        /// <summary>
        /// Provides an clear method with an action.
        /// </summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="onClearAction">The action to run. It runs n times, once for each item cleared.</param>
        public static void Clear<T>(this IList<T> list, Action<T> onClearAction = null)
        {
            ListIsNotNull(list, nameof(list));
            var tmpList = list.ToList();
            list.Clear();
            foreach (var item in tmpList)
                onClearAction?.Invoke(item);
        }

        /// <summary>
        /// Provides an clear method with a bulk action.
        /// </summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="onClearAction">The action to run. It runs 1 time for all items cleared.</param>
        public static void Clear<T>(this IList<T> list, Action<IEnumerable<T>> onClearAction = null)
        {
            ListIsNotNull(list, nameof(list));
            var tmpList = list.ToList();
            list.Clear();
            onClearAction?.Invoke(tmpList);
        }
        #endregion

        #region Insert
        /// <summary>
        /// Provides an insert method with an action on insert.
        /// </summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index to insert.</param>
        /// <param name="item">The item to insert.</param>
        /// <param name="onInsertAction">The action to run after the item is inserted.</param>
        public static void Insert<T>(this IList<T> list, int index, T item, Action<int, T> onInsertAction = null)
        {
            ListIsNotNull(list, nameof(list));
            list.Insert(index, item);
            onInsertAction?.Invoke(index, item);
        }
        #endregion

        #region Remove
        /// <summary>
        /// Provides a remove method with an action on remove.
        /// </summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="item">The item to remove.</param>
        /// <param name="onRemoveAction">The action to run after the item is removed.</param>
        public static bool Remove<T>(this IList<T> list, T item, Action<T> onRemoveAction = null)
        {
            ListIsNotNull(list, nameof(list));
            var result = list.Remove(item);
            if (result)
                onRemoveAction?.Invoke(item);
            return result;
        }
        #endregion

        #region RemoveAny
        /// <summary>
        /// Provides a remove method for multiple items with an action on remove.
        /// </summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="items">The items to remove.</param>
        /// <param name="onRemoveAction">The action to run after the items are removed.</param>
        /// <remarks>It runs 1 time for all removed items. It only runs for removed items. Items not removed because they were not in the list do not run the action.</remarks>
        public static void RemoveAny<T>(this IList<T> list, IEnumerable<T> items, Action<IEnumerable<T>> onRemoveAction = null)
        {
            ListIsNotNull(list, nameof(list));
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
        /// <summary>
        /// Provides a remove method with an action on remove.
        /// </summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index of the item to remove.</param>
        /// <param name="onRemoveAction">The action to run after the item is removed.</param>
        public static void RemoveAt<T>(this IList<T> list, int index, Action<T> onRemoveAction = null)
        {
            ListIsNotNull(list, nameof(list));
            var item = list[index];
            list.RemoveAt(index);
            onRemoveAction?.Invoke(item);
        }
        #endregion

        #region AddRange

        /// <summary>
        /// Provides and AddRange to IList. List has it but IList doesn't without this extension.
        /// </summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="items">The items to add.</param>
        /// <remarks>This doesn't throw an exception items is null.</remarks>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            ListIsNotNull(list, nameof(list));
            ListIsNotNull(items, nameof(items));
            if (!items.Any())
                return;
            // ToList() is needed in case IEnumerable creates a new instance on iteration
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

        /// <summary>
        /// Provides and AddRange with an action.
        /// </summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="items">The items to add.</param>
        /// <param name="onAddAction">The action to run after the items are added.</param>
        /// <remarks>The action is run n times, once for each item.</remarks>
        /// <remarks>This doesn't throw an exception items is null.</remarks>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items, Action<T> onAddAction)
        {
            ListIsNotNull(list, nameof(list));
            ListIsNotNull(items, nameof(items));
            if (!items.Any())
                return;
            var itemsList = items.ToList();
            // ToList() is needed in case IEnumerable creates a new instance on iteration
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

        /// <summary>
        /// Provides and AddRange with an action.
        /// </summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="items">The items to add.</param>
        /// <param name="onAddAction">The action to run after the items are added.</param>
        /// <remarks>The action is run one times, once for all items.</remarks>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items, Action<IEnumerable<T>> onAddAction)
        {
            ListIsNotNull(list, nameof(list));
            ListIsNotNull(items, nameof(items));
            if (!items.Any())
                return;
            var itemsList = items.ToList();
            // ToList() is needed in case IEnumerable creates a new instance on iteration
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
        public static void SetIndex<T>(this IList<T> list, int index, T item, Action<T> onAddAction = null, Action<T> onRemoveAction = null)
        {
            ListIsNotNull(list, nameof(list));
            var removedItem = list[index];
            list[index] = item;
            onAddAction?.Invoke(item);
            onRemoveAction?.Invoke(removedItem);
        }
        #endregion

        #region InsertRange
        /// <summary>
        /// Adds insert range to IList.
        /// </summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index.</param>
        /// <param name="items">The items to insert.</param>
        /// <param name="onInsertAction">The action to run on insert.</param>
        /// <remarks>Runs the action one times, once for all added items.</remarks>
        public static void InsertRange<T>(this IList<T> list, int index, IEnumerable<T> items, Action<IEnumerable<T>> onInsertAction)
        {
            ListIsNotNull(list, nameof(list));
            ListIsNotNull(items, nameof(items));
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

        /// <summary>
        /// Adds insert range to IList.
        /// </summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index.</param>
        /// <param name="items">The items to insert.</param>
        /// <param name="onInsertAction">The action to run on insert.</param>
        /// <remarks>Runs the action n times, once for each added item.</remarks>
        public static void InsertRange<T>(this IList<T> list, int index, IEnumerable<T> items, Action<int, T> onInsertAction)
        {
            ListIsNotNull(list, nameof(list));
            ListIsNotNull(items, nameof(items));
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
        public static IRangeableList<T> GetRange<T>(this IList<T> list, int index, int count)
        {
            ListIsNotNull(list, nameof(list));
            var rangedList = new RangeableList<T>();
            for (int i = 0; i < count; i++)
            {
                rangedList.Add(list[i + index]);
            }
            return rangedList;
        }
        #endregion

        #region RemoveRange
        public static void RemoveRange<T>(this IList<T> list, int index, int count, Action<T> onRemoveAction = null)
        {
            ListIsNotNull(list, nameof(list));
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

        public static void RemoveRange<T>(this IList<T> list, int index, int count, Action<IEnumerable<T>> onRemoveAction = null)
        {
            ListIsNotNull(list, nameof(list));
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
            ListIsNotNull(list, nameof(list));
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
            ListIsNotNull(list, nameof(list));
            return new RangeableList<TCast>(list.Select(i => (TCast)i));
        }
        #endregion

        internal static void ListIsNotNull<T>(IEnumerable<T> list, string argument)
        {
            if (list == null)
                throw new ArgumentNullException(argument);
        }
        
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