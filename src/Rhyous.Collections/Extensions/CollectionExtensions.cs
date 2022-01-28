using System;
using System.Collections;

namespace Rhyous.Collections
{
    /// <summary>Extension methods for ICollection.</summary>
    public static class CollectionExtensions
    {
        /// <summary>Returns the last index of a list.</summary>
        /// <param name="collection">The ICollection instance or object that inherits ICollection, such as any array or list.</param>
        /// <param name="throwIfNull">True by default. Whether to throw an ArgumentNullException if the collection is null or whether to return -1.</param>
        /// <returns>The last index of the collection or -1 if the collection is empty, or if it is null and throwIfNull is false.</returns>
        /// <exception cref="ArgumentNullException">An ArgumentNullException will be thrown if throwIfNull is true and the collection is null.</exception>
        public static int LastIndex(this ICollection collection, bool throwIfNull = true)
        {
            if (throwIfNull && collection is null) { throw new ArgumentNullException(nameof(collection)); };
            return collection?.Count - 1 ?? -1;
        }
    }
}