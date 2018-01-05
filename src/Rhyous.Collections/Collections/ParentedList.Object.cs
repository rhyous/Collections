using System;
using System.Collections.Generic;
using System.Reflection;

namespace Rhyous.Collections
{
    /// <summary>
    /// A list that automatically sets the parent when an item is added.
    /// </summary>
    /// <typeparam name="TItem">The type of item the list holds.</typeparam>
    /// <remarks>This inherits from ActionableList, the actions being to set the parent on Add and remove the parent on Remove.</remarks>
    public class ParentedList<TItem> : ActionableList<TItem>
    {
            #region Constructors
            public ParentedList()
            {
                base.AddAction = AddParent;
                base.RemoveAction = RemoveParent;
            }
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

        internal Type Type { get { return _Type ?? (_Type = typeof(TItem)); } }
        private Type _Type;

        internal PropertyInfo PropertyInfo { get { return _PropertyInfo ?? (_PropertyInfo = Type.GetPropertyInfo(ParentPopertyName)); } }
        private PropertyInfo _PropertyInfo;

        #region Parent
        public virtual object Parent { get; set; }
        public string ParentPopertyName { get; set; } = "Parent";

        public virtual void RemoveParent(TItem item)
        {
            if (item != null && !_List.Contains(item))
                PropertyInfo.SetValue(item, PropertyInfo.PropertyType.GetDefault());
        }

        public virtual void AddParent(TItem item)
        {
            if (item != null)
                PropertyInfo.SetValue(item, Parent);
        }
        #endregion
    }
}