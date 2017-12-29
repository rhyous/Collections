using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.Collections
{
    /// <summary>
    /// A list that automatically sets the parent when an item is added.
    /// </summary>
    /// <typeparam name="TItem">The type of item the list holds.</typeparam>
    /// <typeparam name="TParent">The type of the parent of all the items.</typeparam>
    public class ParentedList<TItem, TParent> : IList<TItem>
        where TItem : IParent<TParent>
    {
        internal readonly IList<TItem> _List = new List<TItem>();

        public ParentedList() { }
        public ParentedList(TParent parent) { Parent = parent; }

        public virtual TParent Parent { get; set; }

        public virtual int Count => _List.Count;

        public virtual bool IsReadOnly => _List.IsReadOnly;

        public virtual TItem this[int index]
        {
            get { return _List[index]; }
            set
            {
                ConditionallyRemoveParent(index);
                _List[index] = value;
                _List[index].Parent = Parent;
            }
        }
        
        public virtual void Add(TItem item)
        {
            _List.Add(item);
            item.Parent = Parent;
        }

        public virtual void AddRange(IEnumerable<TItem> items)
        {
            ((List<TItem>)_List).AddRange(items);
            foreach (var item in items)
                item.Parent = Parent;
        }
                public void Insert(int index, TItem item)
        {
            _List.Insert(index, item);
            item.Parent = Parent;
        }

        public virtual void Clear() => _List.Clear();

        public virtual bool Contains(TItem item) => _List.Contains(item);

        public virtual void CopyTo(TItem[] array, int arrayIndex) => _List.CopyTo(array, arrayIndex);

        public virtual IEnumerator<TItem> GetEnumerator() => _List.GetEnumerator();

        public virtual bool Remove(TItem item)
        {
            var result = _List.Remove(item);
            if (result && !_List.Contains(item))
                item.Parent = default(TParent);
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
                    _List[index].Parent = default(TParent);
            }
        }
    }
}