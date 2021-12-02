using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Rhyous.Collections
{
    /// <summary>An exception to be used when a unique collection has duplicate items.</summary>
    [ExcludeFromCodeCoverage]
    class DuplicateItemException : Exception
    {
        /// <inheritdoc />
        public DuplicateItemException()
        {
        }

        /// <inheritdoc />
        public DuplicateItemException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public DuplicateItemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc />
        protected DuplicateItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
