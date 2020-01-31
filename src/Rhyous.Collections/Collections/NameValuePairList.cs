using System.Collections.Generic;

namespace Rhyous.Collections
{
    public class NameValuePairList<T> : List<INameValuePair<T>>
    {
        public NameValuePairList() { }
        public NameValuePairList(int capacity) : base(capacity) { }
        public NameValuePairList(IEnumerable<INameValuePair<T>> collection) : base(collection) { }

        public void Add(string name, T value)
        {
            Add(new NameValuePair<T> { Name = name, Value = value });
        }
    }
}