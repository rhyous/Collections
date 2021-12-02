using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.Collections
{
    /// <summary>Used as a return type for <see cref="M:EnumerableExtensions.GetMisMatchedItems"/>.</summary>
    /// <typeparam name="T">The type of the items in the lists being compared.</typeparam>
    public class MismatchedItems<T> : IEnumerable<T>
    {
        /// <summary>The list of items in the right enumerable that didn't match the items in the left enumerable.</summary>
        public List<T> Right
        {
            get { return _Right ?? (_Right = new List<T>()); }
            set { _Right = value; }
        } private List<T> _Right;

        /// <summary>The list of items in the left enumerable that didn't match the items in the right enumerable.</summary>
        public List<T> Left
        {
            get { return _Left ?? (_Left = new List<T>()); }
            set { _Left = value; }
        } private List<T> _Left;

        /// <summary>The total count of items that on both sides that didn't match.</summary>
        public int Count => Right.Count + Left.Count;

        /// <summary>The combined enumerator.</summary>
        public IEnumerator<T> GetEnumerator() => Left.Concat(Right).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
