using System.Collections.Generic;

namespace Rhyous.Collections
{
    /// <summary>
    /// You can use this whenever you need an <see cref="IList{T}"/> but you then need to use a Range method
    /// such as: AddRange, InsertRange, GetRange, RemoveRange.
    /// There is an associated ToRangeableList extension method that will turn any <see cref="IEnumerable{T}"/>
    /// into a <see cref=" RangeableList<T>"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    public interface IRangeableList<T> : IList<T>
    {
        void AddRange(IEnumerable<T> items);

        void InsertRange(int index, IEnumerable<T> items);

        IRangeableList<T> GetRange(int index, int count);

        void RemoveRange(int index, int count);

        int Capacity { get; set; }
    }
}