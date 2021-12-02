namespace Rhyous.Collections
{
    /// <summary>A class to model a name value pair.</summary>
    /// <typeparam name="T">The type of the value in the name value pair.</typeparam>
    public class NameValuePair<T> : INameValuePair<T>
    {
        /// <inheritdoc/>
        public string Name { get; set; }
        /// <inheritdoc/>
        public T Value { get; set; }
    }
}