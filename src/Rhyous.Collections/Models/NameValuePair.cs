namespace Rhyous.Collections
{
    public class NameValuePair<T> : INameValuePair<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }
    }
}