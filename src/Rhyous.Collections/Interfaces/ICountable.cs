namespace Rhyous.Collections
{
    /// <summary>An interface for any collection that can be counted.</summary>
    public interface ICountable
    {
        /// <summary>The count of items in this collection.</summary>
        int Count { get; }
    }
}