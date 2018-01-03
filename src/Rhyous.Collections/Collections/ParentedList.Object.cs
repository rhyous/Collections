using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rhyous.Collections
{
    /// <summary>
    /// A list that automatically sets the parent when an item is added.
    /// </summary>
    /// <typeparam name="TItem">The type of item the list holds.</typeparam>
    public class ParentedList<TItem> : IList<TItem>
    {
        internal readonly IList<TItem> _List = new List<TItem>();

        #region Constructors
        public ParentedList() { }

        public ParentedList(object parent, string parentPropertyName = "Parent") : this()
        {
            Parent = parent;
            ParentPopertyName = parentPropertyName;
            if (Type.GetProperty(ParentPopertyName) == null)
                throw new ArgumentException($"The property {parentPropertyName} must be a valid property of {Type}");
        }

        public ParentedList(object parent, IEnumerable<TItem> items, string parentPropertyName = "Parent") : this(parent, parentPropertyName)
        {
            AddRange(items);
        }
        #endregion

        internal Type Type { get {return _Type ?? (_Type = typeof(TItem)); } }
        private Type _Type;

        internal PropertyInfo PropertyInfo {get { return _PropertyInfo ?? (_PropertyInfo = Type.GetPropertyInfo(ParentPopertyName)); } }
        private PropertyInfo _PropertyInfo;

        public string ParentPopertyName { get; set; } = "Parent";
        
        public virtual object Parent { get; set; }

        public virtual int Count => _List.Count;

        public virtual bool IsReadOnly => _List.IsReadOnly;

        public virtual TItem this[int index]
        {
            get { return _List[index]; }
            set
            {
                ConditionallyRemoveParent(index);
                _List[index] = value;
                SetParent(value, Parent);
            }
        }

        internal void SetParent(TItem item, object parent) => PropertyInfo.SetValue(item, parent);
        internal void RemoveParent(TItem item) => PropertyInfo.SetValue(item, PropertyInfo.PropertyType.GetDefault());

        public virtual void Add(TItem item)
        {
            _List.Add(item);
            SetParent(item, Parent);
        }

        public virtual void AddRange(IEnumerable<TItem> items)
        {
            ((List<TItem>)_List).AddRange(items);
            foreach (var item in items)
                SetParent(item, Parent);
        }
        public void Insert(int index, TItem item)
        {
            _List.Insert(index, item);
            SetParent(item, Parent);
        }

        public virtual void Clear()
        {
            foreach (var item in this)
            {
                if (item != null)
                    RemoveParent(item);
            }
            _List.Clear();
        }

        public virtual bool Contains(TItem item) => _List.Contains(item);

        public virtual void CopyTo(TItem[] array, int arrayIndex) => _List.CopyTo(array, arrayIndex);

        public virtual IEnumerator<TItem> GetEnumerator() => _List.GetEnumerator();

        public virtual bool Remove(TItem item)
        {
            var result = _List.Remove(item);
            if (result && !_List.Contains(item))
                RemoveParent(item);
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public virtual int IndexOf(TItem item) => _List.IndexOf(item);

        public virtual void RemoveAt(int index)
        {
            ConditionallyRemoveParent(index);
            _List.RemoveAt(index);
        }

        protected internal virtual void ConditionallyRemoveParent(int index)
        {
            if (index >= 0 && index < _List.Count && _List[index] != null)
            {
                var ids = Enumerable.Range(0, _List.Count).Where(i => _List[i].Equals(_List[index])).ToList();
                if (ids.Count == 1 && ids[0] == index)
                    RemoveParent(_List[index]);
            }
        }
    }
}