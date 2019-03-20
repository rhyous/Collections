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
        protected internal readonly IRangeableList<TItem> _List;

        #region Constructors
        protected ActionableList() { _List = new RangeableList<TItem>(); }

        public ActionableList(Action<TItem> addAction, Action<TItem> removeAction, Action<int, TItem> insertAction = null) 
            : this()
        {
            AddAction = addAction;
            RemoveAction = removeAction;
            InsertAction = insertAction;
        }

        public ActionableList(Action<TItem> addAction, Action<TItem> removeAction, int capacity, Action<int, TItem> insertAction = null) 
            : this(addAction, removeAction, insertAction)
        {
            _List = new RangeableList<TItem>(capacity);
        }

        public ActionableList(Action<TItem> addAction, Action<TItem> removeAction, IEnumerable<TItem> collection, Action<int, TItem> insertAction = null) 
            : this(addAction, removeAction, insertAction)
        {
            _List = new RangeableList<TItem>(collection);
        }
        #endregion
        
        public virtual Action<TItem> AddAction { get; protected set; }

        public virtual Action<int, TItem> InsertAction
        {
            get { return _InsertAction ?? (_InsertAction = (index, item) => { AddAction?.Invoke(item); }); }
            protected set { _InsertAction = value; }
        } private Action<int, TItem> _InsertAction;

        public virtual Action<TItem> RemoveAction { get; protected set; }

        #region IList<T>
        public virtual TItem this[int index]
        {
            get => _List[index];
            set => _List.SetIndex(index, value, AddAction, RemoveAction);
        }

        public virtual int Count => _List.Count;

        public virtual int Capacity { get => _List.Capacity; set => _List.Capacity = value; }

        public virtual bool IsReadOnly => _List.IsReadOnly;

        public virtual void Add(TItem item) => ListExtensions.Add(_List, item, AddAction);

        public virtual void Clear() => ListExtensions.Clear(_List, RemoveAction);

        public virtual bool Contains(TItem item) => _List.Contains(item);

        public virtual void CopyTo(TItem[] array, int arrayIndex) => _List.CopyTo(array, arrayIndex);

        public virtual IEnumerator<TItem> GetEnumerator() => _List.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _List.GetEnumerator();

        public virtual int IndexOf(TItem item) => _List.IndexOf(item);

        public virtual void Insert(int index, TItem item) => ListExtensions.Insert(_List, index, item, InsertAction);

        public virtual bool Remove(TItem item) => ListExtensions.Remove(_List, item, RemoveAction);

        public virtual void RemoveAt(int index)
        {
            var item = _List[index];
            _List.RemoveAt(index);
            RemoveAction(item);
        }

        #endregion


        #region IRangeableList
        public virtual void AddRange(IEnumerable<TItem> items) => ListExtensions.AddRange(_List, items, AddAction);

        public virtual void InsertRange(int index, IEnumerable<TItem> items) => ListExtensions.InsertRange(_List, index, items, InsertAction);

        public virtual IRangeableList<TItem> GetRange(int index, int count) => ListExtensions.GetRange(_List, index, count);

        public virtual void RemoveRange(int index, int count) => ListExtensions.RemoveRange(_List, index, count, RemoveAction);
        #endregion
    }
}