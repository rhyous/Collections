using System.Collections.Generic;

namespace Rhyous.Collections
{
    /// <summary>A list of INameValuePair{T} items.</summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <remarks>This is useful over a dictionary when the name is not a key, meaning the list must be able to contain duplicates.</remarks>
    public class NameValuePairList<T> : List<INameValuePair<T>>, IClearable, ICountable
    {
        /// <inheritdoc />
        public NameValuePairList() { }
        /// <inheritdoc />
        public NameValuePairList(int capacity) : base(capacity) { }
        /// <inheritdoc />
        public NameValuePairList(IEnumerable<INameValuePair<T>> collection) : base(collection) { }

        /// <summary>Taks the name and value, creates a NameValuePair{T}, and adds it to the list.</summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Add(string name, T value)
        {
            Add(new NameValuePair<T> { Name = name, Value = value });
        }
    }
}