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
        #region Constructors

        /// <summary>The empty constructor.</summary>
        public ParentedList()
        {
            base.AddAction = AddParent;
            base.RemoveAction = RemoveParent;
        }


        /// <summary>The constructor that takes in the TParent.</summary>
        /// <param name="parent">The parent item.</param>
        public ParentedList(TParent parent) : this() { Parent = parent; }

        /// <summary>The constructor that takes in the TParent and an IEnumerable{TItem} set of items.</summary>
        /// <param name="parent">The parent item.</param>
        /// <param name="items">The items to add when initializing the list.</param>
        public ParentedList(TParent parent, IEnumerable<TItem> items) : this()
        {
            Parent = parent;
            AddRange(items);
        }
        #endregion

        #region Parent
        /// <summary>The TParent of the items in the list.</summary>
        public virtual TParent Parent { get; set; }

        /// <summary>Removes a parent from an item. Used when an item is removed from the list.</summary>
        /// <param name="item">The item to remove the parent from.</param>
        public virtual void RemoveParent(TItem item)
        {
            if (item != null && !_List.Contains(item))
                item.Parent = default(TParent);
        }

        /// <summary>Add a parent to an item. Used when an item is added to the list.</summary>
        /// <param name="item">The item to add the parent to.</param>
        public virtual void AddParent(TItem item)
        {
            if (item != null)
                item.Parent = Parent;
        }
        #endregion        
    }
}