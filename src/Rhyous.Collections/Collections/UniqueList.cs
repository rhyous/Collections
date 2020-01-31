using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.Collections
{
    /// <summary>
    /// A list that enforces that all items are unique.     
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>For items that are not reference equals but should be equals, an Equality comparer can be set.</remarks>
    public class UniqueList<T> : IList<T>, IRangeableList<T>
    {
        #region Constructors

        public UniqueList() => List = new RangeableList<T>();

        public UniqueList(IEqualityComparer<T> equalityComparer) 
            : this() => EqualityComparer = equalityComparer;

        public UniqueList(int capacity) => List = new List<T>(capacity);

        public UniqueList(IEnumerable<T> collection) => List = new List<T>(collection?.Distinct());

        public UniqueList(IEnumerable<T> collection, IEqualityComparer<T> equalityComparer) 
            : this(collection) => EqualityComparer = equalityComparer;

        #endregion
        internal List<T> List { get; set; }
        
        public IEqualityComparer<T> EqualityComparer { get; set; }

        public bool ThrowOnDuplicate { get; set; } = true;

        public bool IsDuplicate(T item) => IndexOf(item) >= 0;

        public int Count => List.Count;

        public bool IsReadOnly => ((IList<T>)List).IsReadOnly;

        public int Capacity { get => List.Capacity; set => List.Capacity = value; }

        public T this[int index] {
            get { return List[index]; }
            set
            {
                var i = IndexOf(value);
                if (i == index || i == -1)
                {
                    List[index] = value;
                    return;
                }
                if (ThrowOnDuplicate)
                    throw new DuplicateItemException();
            }
        }

        public int IndexOf(T item)
        {
            if (EqualityComparer == null)
                return List.IndexOf(item);
            for (int i = 0; i < List.Count; i++)
            {
                if (EqualityComparer.Equals(List[i], item))
                    return i;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (!IsDuplicate(item))
            {
                List.Insert(index, item);
                return;
            }
            if (ThrowOnDuplicate)
                throw new DuplicateItemException();
        }

        public void RemoveAt(int index) => List.RemoveAt(index);

        public void Add(T item)
        {
            if (!IsDuplicate(item))
            {
                List.Add(item);
                return;
            }
            if (ThrowOnDuplicate)
                throw new DuplicateItemException();
        }

        public void AddRange(IEnumerable<T> items)
        {
            items = BulkCheckDuplicates(items);
            List.AddRange(items);
        }

        public void Clear() => List.Clear();

        public bool Contains(T item) => List.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => List.CopyTo(array, arrayIndex);

        public bool Remove(T item) => List.Remove(item);

        public IEnumerator<T> GetEnumerator() => List.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => List.GetEnumerator();

        public void InsertRange(int index, IEnumerable<T> items)
        {
            items = BulkCheckDuplicates(items);
            List.InsertRange(index, items);
        }

        private IEnumerable<T> BulkCheckDuplicates(IEnumerable<T> items)
        {
            var itemList = items.ToList();
            var duplicates = new List<T>();
            foreach (var item in items)
            {
                if (IsDuplicate(item))
                {
                    if (ThrowOnDuplicate)
                        throw new DuplicateItemException();
                    duplicates.Add(item);
                }
            }
            itemList.RemoveAny(duplicates);
            return itemList;
        }

        public IRangeableList<T> GetRange(int index, int count) => ListExtensions.GetRange(this, index, count);

        public void RemoveRange(int index, int count) => List.RemoveRange(index, count);
    }
}
