using System;
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
    /// <remarks>Also, this is different than a List{T} in that all items must be unique, but internally, it wraps a list.</remarks>
    /// <remarks>Also, this is different than a HashSet{T} as items in a HashSet{T} cannot be indexed, but internally, it uses a HashSet{T}.</remarks>
    public class UniqueList<T> : IList<T>, IRangeableList<T>, IClearable, ICountable
    {
        #region Constructors

        /// <summary>The empty constructor.</summary>
        public UniqueList()
        {
            List = new RangeableList<T>();
            HashSet = new HashSet<T>();
        }


        /// <summary>The constructor that takes in an <see cref="IEqualityComparer{T}"/>.</summary>
        /// <param name="equalityComparer">An instance of <see cref="IEqualityComparer{T}"/>.</param>
        public UniqueList(IEqualityComparer<T> equalityComparer)
        {
            List = new RangeableList<T>();
            EqualityComparer = equalityComparer;
        }

        /// <summary>The constructor that takes in a capacity.</summary>
        /// <param name="capacity">The initial size capacity of the list.</param>
        public UniqueList(int capacity)
        {
            List = new List<T>(capacity);
            HashSet = new HashSet<T>();
        }

        /// <summary>The constructor that takes in a collection to initialize the list with. It calls Distinct on that collection to avoid duplicates.</summary>
        /// <param name="collection">The initial collection.</param>
        public UniqueList(IEnumerable<T> collection) : this()
        {
            AddRange(collection.Distinct());
        }

        /// <summary>
        /// The constructor that takes in a collection to initialize the list with and an <see cref="IEqualityComparer{T}"/>.
        /// It calls Distinct with the <see cref="IEqualityComparer{T}"/> as a parameter on that collection to avoid duplicates.
        /// </summary>
        /// <param name="collection">The initial collection.</param>
        /// <param name="equalityComparer">An instance of <see cref="IEqualityComparer{T}"/>.</param>
        public UniqueList(IEnumerable<T> collection, IEqualityComparer<T> equalityComparer) : this(equalityComparer)
        {
            AddRange(collection.Distinct(equalityComparer));
        }

        #endregion
        internal List<T> List { get; set; }
        internal HashSet<T> HashSet { get; set; }

        /// <summary>The <see cref="IEqualityComparer{T}"/> to use when comparing to items to see if they are the same.</summary>
        public IEqualityComparer<T> EqualityComparer
        {
            get { return _EqualityComparer ?? (_EqualityComparer = EqualityComparer<T>.Default); }
            set
            {
                if (List != null && List.Count > 0)
                   throw new InvalidOperationException("The EqualityComparer cannot be changed after items are added to the list.");
                _EqualityComparer = value;
                HashSet = new HashSet<T>(EqualityComparer);
            }
        } private IEqualityComparer<T> _EqualityComparer;

        /// <summary>Whether to throw an exception on duplicate or not. Attempts to add or insert a duplicate throws an exception if true or does nothing if false.</summary>
        public bool ThrowOnDuplicate { get; set; } = true;

        /// <summary>Checks whether an item is a duplicate or not.</summary>
        /// <param name="item">The item to check.</param>
        public bool IsDuplicate(T item) => HashSet.Contains(item);

        /// <summary>The count of items in the list.</summary>
        public int Count => List.Count;

        /// <summary>Checks if the list is read only.</summary>
        public bool IsReadOnly => ((IList<T>)List).IsReadOnly;

        /// <summary>The capacity of the list. Memory can be allocated for more capacity than there is count.</summary>
        public int Capacity { get => List.Capacity; set => List.Capacity = value; }

        /// <summary>The list index, so items can be obtained from the list by index.</summary>
        public T this[int index]
        {
            get { return List[index]; }
            set
            {
                var existingItem = List[index];

                // The same instance in memory, nothing to do
                if (ReferenceEquals(existingItem, value))
                    return;

                if (!EqualityComparer.Equals(existingItem, value) && IsDuplicate(value))
                { 
                    if (ThrowOnDuplicate)
                        throw new DuplicateItemException();
                    return;
                }

                HashSet.Remove(existingItem);
                HashSet.Add(value);
                List[index] = value;
                return;
            }
        }

        /// <summary>Gets the index of the item, or an item that is equal to it.</summary>
        /// <param name="item">The item to find the index of.</param>
        public int IndexOf(T item)
        {
            if (!HashSet.Contains(item))
                return -1;
            // Otherwise, use the Equality comparer, Big O(N)
            for (int i = 0; i < List.Count; i++)
            {
                if (EqualityComparer.Equals(List[i], item))
                    return i;
            }
            return -1;
        }

        /// <summary>Inserts the item at the specified index.</summary>
        /// <param name="index">The index indicated where to inser the item. All subsequent items have their index increased by one.</param>
        /// <param name="item">The item to insert.</param>
        public void Insert(int index, T item)
        {
            if (IsDuplicate(item))
            {
                if (ThrowOnDuplicate)
                    throw new DuplicateItemException();
                return;
            }
            List.Insert(index, item);
            HashSet.Add(item);
        }

        /// <summary>Removes the item at the specified index.</summary>
        /// <param name="index"></param>
        public void RemoveAt(int index) => List.RemoveAt(index);

        /// <summary>Adds an item to the list.</summary>
        /// <param name="item">The item to add.</param>
        public void Add(T item)
        {
            if (IsDuplicate(item))
            {
                if (ThrowOnDuplicate)
                    throw new DuplicateItemException();
                return;
            }
            List.Add(item);
            HashSet.Add(item);
        }

        /// <summary>Clears all items in the list.</summary>
        public void Clear()
        {
            List.Clear();
            HashSet.Clear();
        }

        /// <summary>Checks if the item is in the list.</summary>
        /// <param name="item">The item to check.</param>
        public bool Contains(T item) => HashSet.Contains(item);

        /// <summary>Copies this list to an array.</summary>
        /// <param name="array">The array to copy the items in this list to.</param>
        /// <param name="arrayIndex">The index in array at which copying begins. Index starts at 0.</param>
        public void CopyTo(T[] array, int arrayIndex) => List.CopyTo(array, arrayIndex);

        /// <summary>Gets the enumerator.</summary>
        public IEnumerator<T> GetEnumerator() => List.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => List.GetEnumerator();

        /// <summary>Removes an item from the list if found. All subsequent items in the list are moved down one index.</summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if removed. False if removal fails or the item was not found.</returns>
        public bool Remove(T item)
        {
            if (!HashSet.Contains(item))
                return false;
            if (!List.Remove(item))
            {
                for (int i = 0; i < List.Count; i++)
                {
                    if (EqualityComparer.Equals(List[i], item))
                    {
                        List.RemoveAt(i);
                        HashSet.Remove(item);
                        return true;
                    }
                }
            }
            return true;
        }

        #region IRangeableList
        /// <summary>Adds a range of items to the list.</summary>
        /// <param name="items">The range of items to add.</param>
        public void AddRange(IEnumerable<T> items)
        {
            items = BulkCheckDuplicates(items);
            List.AddRange(items);
            HashSet.AddRange(items);
        }

        /// <summary>Adds a range of items to this list starting at the specified index. All subsequent items in the list are moved up one index.</summary>
        /// <param name="index">The index to start inserting the items.</param>
        /// <param name="items">The IEnumerable{TItem} collection of items to add.</param>
        public void InsertRange(int index, IEnumerable<T> items)
        {
            items = BulkCheckDuplicates(items);
            List.InsertRange(index, items);
        }

        /// <summary>Gets a range of items from this list starting at the specified index.</summary>
        /// <param name="index">The index to start getting the items. Index starts at 0.</param>
        /// <param name="count">The number of items including and after the index to get.</param>
        /// <returns>An IRangeableList{TItem} collection of the range of items.</returns>
        public IRangeableList<T> GetRange(int index, int count) => ListExtensions.GetRange(this, index, count);

        /// <summary>Removes a range of items from this list starting at the specified index. All subsequent items, if any remain, will be reindexed.</summary>
        /// <param name="index">The index to start removing the items from. Index starts at 0.</param>
        /// <param name="count">The number of items including and after the index to remove.</param>
        public void RemoveRange(int index, int count) => List.RemoveRange(index, count);
        #endregion

        private IEnumerable<T> BulkCheckDuplicates(IEnumerable<T> items)
        {
            var itemList = items.ToList();
            var duplicates = items.Where(i => IsDuplicate(i)).ToList();
            if (duplicates.Any() && ThrowOnDuplicate)
                throw new DuplicateItemException();
            itemList.RemoveAny(duplicates);
            return itemList;
        }
    }
}
