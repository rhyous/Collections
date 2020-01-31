namespace Rhyous.Collections
{
    public interface INameValuePair<T>
    {
        string Name { get; set; }
        T Value { get; set; }
    }
}
