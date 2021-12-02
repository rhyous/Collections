using System.Collections.Generic;

namespace Rhyous.Collections
{
    /// <summary>This is inherits <see cref="List{T}"/> but also implements the <see cref="IRangeableList{T}"/> interface.</summary>
    /// <typeparam name="T">The items in the list.</typeparam>
    public class RangeableList<T> : List<T>, IRangeableList<T>
    {
        /// <inheritdoc />
        public RangeableList()
        {
        }

        /// <inheritdoc />
        public RangeableList(int capacity) : base(capacity)
        {
        }

        /// <inheritdoc />
        public RangeableList(IEnumerable<T> collection) : base(collection)
        {
        }

        void IRangeableList<T>.AddRange(IEnumerable<T> items)
        {
            AddRange(items);
        }

        IRangeableList<T> IRangeableList<T>.GetRange(int index, int count)
        {
           return ListExtensions.GetRange(this, index, count);
        }

        void IRangeableList<T>.InsertRange(int index, IEnumerable<T> items)
        {
            InsertRange(index, items);
        }

        void IRangeableList<T>.RemoveRange(int index, int count)
        {
            RemoveRange(index, count);
        }
    }
}