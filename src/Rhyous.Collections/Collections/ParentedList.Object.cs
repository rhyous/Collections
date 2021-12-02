using System;
using System.Collections.Generic;
using System.Reflection;

namespace Rhyous.Collections
{
    /// <summary>
    /// A list that automatically sets the parent when an item is added.
    /// The parent is an unknown object.
    /// </summary>
    /// <typeparam name="TItem">The type of item the list holds.</typeparam>
    /// <remarks>This inherits from ActionableList, the actions being to set the parent on Add and remove the parent on Remove.</remarks>
    public class ParentedList<TItem> : ActionableList<TItem>
    {
        private const string DefaultParentProperty = "Parent";

        #region Constructors
        /// <summary>The empty constructor.</summary>
        public ParentedList()
        {
            base.AddAction = AddParent;
            base.RemoveAction = RemoveParent;
        }

        /// <summary>The constructor that takes in the TParent.</summary>
        /// <param name="parent">The parent item.</param>
        /// <param name="parentPropertyName">The property name of the Parent in the item's class.</param>
        public ParentedList(object parent, string parentPropertyName = DefaultParentProperty) : this()
        {
            Parent = parent;
            ParentPopertyName = parentPropertyName;
        }


        /// <summary>The constructor that takes in the TParent and an IEnumerable{TItem} set of items.</summary>
        /// <param name="items">The items to add when initializing the list.</param>
        /// <param name="parent">The parent item.</param>
        /// <param name="parentPropertyName">The property name of the Parent in the items.</param>
        public ParentedList(object parent, IEnumerable<TItem> items, string parentPropertyName = DefaultParentProperty) : this(parent, parentPropertyName)
        {
            AddRange(items);
        }
        #endregion

        internal Type Type { get { return _Type ?? (_Type = typeof(TItem)); } }

        private Type _Type;

        internal PropertyInfo PropertyInfo
        {
            get { return _PropertyInfo ?? (_PropertyInfo = Type.GetPropertyInfo(ParentPopertyName)); }
            private set { _PropertyInfo = value; }
        } private PropertyInfo _PropertyInfo;

        #region Parent
        /// <summary>The parent object of the items in the list.</summary>
        public virtual object Parent { get; set; }
        /// <summary>The property name of the Parent in the item's class.</summary>
        public string ParentPopertyName
        {
            get { return _ParentPropertyName; }
            set
            {
                if (Count > 0)
                    throw new InvalidOperationException($"The {nameof(ParentPopertyName)} cannot be changed after items are added.");
                if (Type.GetProperty(value) == null)
                    throw new ArgumentException($"The property {value} must be a valid property of {Type}");
                _ParentPropertyName = value;
                PropertyInfo = null; // Reset the PropertyInfo if the parent property changes.
            }
        } private string _ParentPropertyName = DefaultParentProperty;

        /// <summary>Removes a parent from an item. Used when an item is removed from the list.</summary>
        /// <param name="item">The item to remove the parent from.</param>
        public virtual void RemoveParent(TItem item)
        {
            if (item != null && !_List.Contains(item))
                PropertyInfo.SetValue(item, default);
        }

        /// <summary>Add a parent to an item. Used when an item is added to the list.</summary>
        /// <param name="item">The item to add the parent to.</param>
        public virtual void AddParent(TItem item)
        {
            if (item != null)
                PropertyInfo.SetValue(item, Parent);
        }
        #endregion
    }
}