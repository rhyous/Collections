using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.Collections
{
    public static class ListExtensions
    {
        public static void Add<T>(this IList<T> list, T item, Action<T> onAddAction = null)
        {
            list.Add(item);
            onAddAction.Invoke(item);
        }

        public static void Clear<T>(this IList<T> list, Action<T> onClearAction = null)
        {
            var tmpList = list.ToList();
            list.Clear();
            foreach (var item in tmpList)
                onClearAction?.Invoke(item);
        }


        public static void Insert<T>(this IList<T> list, int index, T item, Action<int, T> onInsertAction = null)
        {
            list.Insert(index, item);
            onInsertAction?.Invoke(index, item);
        }

        public static bool Remove<T>(this IList<T> list, T item, Action<T> onRemoveAction = null)
        {
            var result = list.Remove(item);
            onRemoveAction?.Invoke(item);
            return result;
        }

        public static void RemoveAt<T>(this IList<T> list, int index, Action<T> onRemoveAction = null)
        {
            var item = list[index];
            list.RemoveAt(index);
            onRemoveAction?.Invoke(item);
        }

        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items, Action<T> onAddAction = null)
        {
            ListIsNotNull(list, "list");
            ListIsNotNull(items, "items");
            var concreteList = list as List<T>;
            if (concreteList != null)
            {
                // ToList() is needed in case IEnumerable creates a new instance on iteration
                var itemsList = items.ToList(); 
                concreteList.AddRange(itemsList);
                OnItemAction(itemsList, onAddAction);
                return;
            }
            foreach (var item in items)
            {
                list.Add(item);
                onAddAction?.Invoke(item);
            }
        }

        public static void SetIndex<T>(this IList<T> list, int index, T item, Action<T> onAddAction = null, Action<T> onRemoveAction = null)
        {
            var removedItem = list[index];
            list[index] = item;
            onAddAction?.Invoke(item);
            onRemoveAction?.Invoke(removedItem);
        }

        public static void InsertRange<T>(this IList<T> list, int index, IEnumerable<T> items, Action<T> onAddAction)
        {
            ListIsNotNull(list, "list");
            ListIsNotNull(items, "items");
            var concreteList = list as List<T>;
            if (concreteList != null)
            {
                var itemsList = items.ToList();
                concreteList.InsertRange(index, itemsList);
                OnItemAction(itemsList, onAddAction);
                return;
            }
            foreach (var item in items)
            {
                list.Add(item);
                onAddAction?.Invoke(item);
            }
        }

        public static IRangeableList<T> GetRange<T>(this IList<T> list, int index, int count)
        {
            ListIsNotNull(list, "list");
            var rangedList = new RangeableList<T>();
            for (int i = index; i < count; i++)
            {
                rangedList.Add(list[i]);
            }
            return rangedList;
        }

        public static void RemoveRange<T>(this IList<T> list, int index, int count, Action<T> onRemoveAction = null)
        {
            ListIsNotNull(list, "list");
            var concreteList = list as List<T>;
            if (concreteList != null)
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

        public static RangeableList<T> ToRangeableList<T>(IEnumerable<T> items)
        {
            return new RangeableList<T>(items);
        }

        public static RangeableList<TCast> ToRangeableList<T, TCast>(IEnumerable<T> items)
            where T : TCast
        {
            return new RangeableList<TCast>(items.Select(i=>(TCast)i));
        }
    }
}