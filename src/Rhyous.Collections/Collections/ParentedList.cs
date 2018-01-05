using System.Collections.Generic;

namespace Rhyous.Collections
{
    /// <summary>
    /// A list that automatically sets the parent when an item is added.
    /// </summary>
    /// <typeparam name="TItem">The type of item the list holds.</typeparam>
    /// <typeparam name="TParent">The type of the parent of all the items.</typeparam>
    /// <remarks>This inherits from ActionableList, the actions being to set the parent on Add and remove the parent on Remove.</remarks>
    public class ParentedList<TItem, TParent> : ActionableList<TItem>
        where TItem : IParent<TParent>
    {
        //internal readonly List<TItem> _List = new List<TItem>();
        #region Constructors
        public ParentedList()
        {
            base.AddAction = AddParent;
            base.RemoveAction = RemoveParent;
        }
        public ParentedList(TParent parent) : this() { Parent = parent; }

        public ParentedList(TParent parent, IEnumerable<TItem> items) : this()
        {
            Parent = parent;
            AddRange(items);
        }
        #endregion

        #region Parent
        public virtual TParent Parent { get; set; }
        
        public virtual void RemoveParent(TItem item)
        {
            if (item != null && !_List.Contains(item))
                item.Parent = default(TParent);
        }

        public virtual void AddParent(TItem item)
        {
            if (item != null)
                item.Parent = Parent;
        }
        #endregion        
    }
}