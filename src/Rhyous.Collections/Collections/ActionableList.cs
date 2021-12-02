using System;
using System.Collections;
using System.Collections.Generic;

namespace Rhyous.Collections
{
    /// <summary>
    /// A list that automatically sets the parent when an item is added.
    /// </summary>
    /// <typeparam name="TItem">The type of item the list holds.</typeparam>
    public class ActionableList<TItem> : IRangeableList<TItem>
    {

        /// <summary>The underlying list, making it easier to implement IRangeableList{TItem} by wrapping it.</summary>
        protected readonly IRangeableList<TItem> _List;

        #region Constructors
        /// <summary>The empty constructor.</summary>
        protected ActionableList() { _List = new RangeableList<TItem>(); }

        /// <summary>The constructor that takes in the various actions.</summary>
        /// <param name="addAction">The action to perform when an item is added.</param>
        /// <param name="removeAction">The action to perform when an item is removed.</param>
        /// <param name="insertAction">Optional. The action to perform when an item is inserted. By default, it will use the AddAction.</param>
        /// <param name="capacity">Optional. Default is 0. The initial size of the list.</param>
        public ActionableList(Action<TItem> addAction, Action<TItem> removeAction, Action<int, TItem> insertAction = null, int capacity = 0) 
            : this()
        {
            AddAction = addAction;
            RemoveAction = removeAction;
            InsertAction = insertAction;
            Capacity = capacity;
        }

        /// <summary>The constructor that takes in the various actions.</summary>
        /// <param name="collection">An IEnumerable{TItem} collection of items to add to the list.</param>
        /// <param name="addAction">The action to perform when an item is added.</param>
        /// <param name="removeAction">The action to perform when an item is removed.</param>
        /// <param name="insertAction">Optional. The action to perform when an item is inserted. By default, it will use the AddAction.</param>
        public ActionableList(IEnumerable<TItem> collection, Action<TItem> addAction, Action<TItem> removeAction, Action<int, TItem> insertAction = null) 
            : this(addAction, removeAction, insertAction)
        {
            _List = new RangeableList<TItem>(collection);
        }
        #endregion
        
        /// <summary>The Action{TItem}, which is a method, that should be run on Add.</summary>
        public virtual Action<TItem> AddAction { get; protected set; }

        /// <summary>The Action{TItem}, which is a method, that should be run on Insert.</summary>
        public virtual Action<int, TItem> InsertAction
        {
            get { return _InsertAction ?? (_InsertAction = (index, item) => { AddAction?.Invoke(item); }); }
            protected set { _InsertAction = value; }
        } private Action<int, TItem> _InsertAction;

        /// <summary>The Action{TItem}, which is a method, that should be run on Remove.</summary>
        public virtual Action<TItem> RemoveAction { get; protected set; }

        #region IList<T>
        /// <summary>The indexer for accessing an item by index.</summary>
        public virtual TItem this[int index]
        {
            get => _List[index];
            set => _List.SetIndex(index, value, AddAction, RemoveAction);
        }


        /// <summary>The count of items in the list.</summary>
        public virtual int Count => _List.Count;

        /// <inheritdoc/>
        public virtual int Capacity { get => _List.Capacity; set => _List.Capacity = value; }

        /// <summary>Whether this list is read only.</summary>
        public virtual bool IsReadOnly => _List.IsReadOnly;

        /// <summary>Adds an item to the list. It will auto-run the AddAction.</summary>
        /// <param name="item">The item to add.</param>
        public virtual void Add(TItem item) => ListExtensions.Add(_List, item, AddAction);

        /// <summary>Clears the list.</summary>
        public virtual void Clear() => ListExtensions.Clear(_List, RemoveAction);

        /// <summary>Checks if this list contains an item.</summary>
        /// <param name="item">The item to check if it is contained in the list.</param>
        /// <returns>True if item is contained in the list, false otherwise.</returns>
        public virtual bool Contains(TItem item) => _List.Contains(item);

        /// <summary>Copies this list to an array.</summary>
        /// <param name="array">The array to copy the items in this list to.</param>
        /// <param name="arrayIndex">The index in array at which copying begins. Index starts at 0.</param>
        public virtual void CopyTo(TItem[] array, int arrayIndex) => _List.CopyTo(array, arrayIndex);

        /// <summary>Gets the enumerator.</summary>
        public virtual IEnumerator<TItem> GetEnumerator() => _List.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _List.GetEnumerator();


        /// <summary>Gets the index of an item if it is in the list.</summary>
        /// <param name="item">The item to check if it is contained in the list.</param>
        /// <returns>The index of the item if found. -1 if not found.</returns>
        public virtual int IndexOf(TItem item) => _List.IndexOf(item);

        /// <summary>Inserts an item at the index of the list. All subsequent items in the list are moved up one index.</summary>
        /// <param name="index">The index to insert the item.</param>
        /// <param name="item">The item to insert.</param>
        public virtual void Insert(int index, TItem item) => ListExtensions.Insert(_List, index, item, InsertAction);

        /// <summary>Removes an item from the list if found. The Remove action is auto-ran. All subsequent items in the list are moved down one index.</summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if removed. False if removal fails or the item was not found.</returns>
        public virtual bool Remove(TItem item) => ListExtensions.Remove(_List, item, RemoveAction);

        /// <summary>Removes an item from the list at the specified index. The Remove action is auto-ran. All subsequent items in the list are moved down one index.</summary>
        /// <param name="index">The index of the item to remove.</param>
        public virtual void RemoveAt(int index)
        {
            var item = _List[index];
            _List.RemoveAt(index);
            RemoveAction(item);
        }

        #endregion

        #region IRangeableList
        /// <summary>Adds a range of items to this list. The add action is called per added item.</summary>
        /// <param name="items">The IEnumerable{TItem} collection of items to add.</param>
        public virtual void AddRange(IEnumerable<TItem> items) => ListExtensions.AddRange(_List, items, AddAction);

        /// <summary>Adds a range of items to this list starting at the specified index. The add action is called per added item. All subsequent items in the list are moved up one index.</summary>
        /// <param name="index">The index to start inserting the items.</param>
        /// <param name="items">The IEnumerable{TItem} collection of items to add.</param>
        public virtual void InsertRange(int index, IEnumerable<TItem> items) => ListExtensions.InsertRange(_List, index, items, InsertAction);

        /// <inheritdoc/>
        public virtual IRangeableList<TItem> GetRange(int index, int count) => ListExtensions.GetRange(_List, index, count);

        /// <summary>Removes a range of items from this list starting at the specified index. The remove action is called per added item. All subsequent items, if any remain, will be reindexed.</summary>
        /// <param name="index">The index to start removing the items from. Index starts at 0.</param>
        /// <param name="count">The number of items including and after the index to remove.</param>
        public virtual void RemoveRange(int index, int count) => ListExtensions.RemoveRange(_List, index, count, RemoveAction);
        #endregion
    }
}