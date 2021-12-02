using System;

namespace Rhyous.Collections
{
    /// <summary>Extension methods that apply to any object</summary>
    public static class ObjectExtensions
    {
        /// <summary>Throws an ArgumentNullException if the object is null.</summary>
        public static void ThrowIfNull(this object o, string argument)
        {
            if (o == null) { throw new ArgumentNullException(argument); }
        }
    }
}