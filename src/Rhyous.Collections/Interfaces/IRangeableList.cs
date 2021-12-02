using System.Collections.Generic;

namespace Rhyous.Collections
{
    /// <summary>
    /// You can use this whenever you need an <see cref="IList{T}"/> but you then need to use a Range method
    /// such as: AddRange, InsertRange, GetRange, RemoveRange.
    /// There is an associated ToRangeableList extension method that will turn any <see cref="IEnumerable{T}"/>
    /// into a <see cref=" RangeableList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    public interface IRangeableList<T> : IList<T>
    {
        /// <summary>Adds a range of items to this list.</summary>
        /// <param name="items">The IEnumerable{TItem} collection of items to add.</param>
        void AddRange(IEnumerable<T> items);

        /// <summary>Adds a range of items to this list starting at the specified index. All subsequent items in the list are moved up one index.</summary>
        /// <param name="index">The index to start inserting the items.</param>
        /// <param name="items">The IEnumerable{TItem} collection of items to add.</param>
        void InsertRange(int index, IEnumerable<T> items);

        /// <summary>Gets a range of items from this list starting at the specified index.</summary>
        /// <param name="index">The index to start getting the items. Index starts at 0.</param>
        /// <param name="count">The number of items including and after the index to get.</param>
        /// <returns>An IRangeableList{TItem} collection of the range of items.</returns>
        IRangeableList<T> GetRange(int index, int count);

        /// <summary>Removes a range of items from this list starting at the specified index. All subsequent items, if any remain, will be reindexed.</summary>
        /// <param name="index">The index to start removing the items from. Index starts at 0.</param>
        /// <param name="count">The number of items including and after the index to remove.</param>
        void RemoveRange(int index, int count);

        /// <summary>The size of the list. Space is allocated in memory for the size even if the list's Count is less than capacity.</summary>
        int Capacity { get; set; }
    }
}