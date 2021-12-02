namespace Rhyous.Collections
{
    /// <summary>An interface defining the signature of item with a parent.</summary>
    /// <typeparam name="T">The type of the parent.</typeparam>
    public interface IParent<T>
    {    
        /// <summary>The parent.</summary>
        T Parent { get; set; }
    }
}