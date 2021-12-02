namespace Rhyous.Collections
{

    /// <summary>An interface defining the signature of a NameValuePair.</summary>
    /// <typeparam name="T">The type of the value in the name value pair.</typeparam>
    public interface INameValuePair<T>
    {
        /// <summary>The name.</summary>
        string Name { get; set; }
        /// <summary>The value.</summary>
        T Value { get; set; }
    }
}