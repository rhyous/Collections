using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.Collections
{
    /// <summary>
    /// Used as a return type for IEnumerable<T>.GetMisMatchedItems().
    /// </summary>
    /// <typeparam name="T">The type of the lists being compared.</typeparam>
    public class MismatchedItems<T> : IEnumerable<T>
    {
        public List<T> Right
        {
            get { return _Right ?? (_Right = new List<T>()); }
            set { _Right = value; }
        } private List<T> _Right;

        public List<T> Left
        {
            get { return _Left ?? (_Left = new List<T>()); }
            set { _Left = value; }
        } private List<T> _Left;

        public int Count => Right.Count + Left.Count;

        public IEnumerator<T> GetEnumerator() => Left.Concat(Right).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
