using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.Collections
{
    /// <summary>Extension methods that apply to <see cref="Type"/>.</summary>
    public static class TypeExtensions
    {

        /// <summary>Returns true if the Type is or inherits from <see cref="ICollection"/> or an <see cref="ICollection{T}"/>.</summary>
        /// <param name="type">The type.</param>
        /// <returns>true if the Type is or inherits from <see cref="ICollection"/> or an <see cref="ICollection{T}"/>, false otherwise.</returns>
        public static bool IsCollection(this Type type)
        {
            return type == typeof(ICollection) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ICollection<>)) || type.GetInterface(nameof(ICollection)) != null || type.GetInterface(typeof(ICollection<>).FullName) != null;
        }

        /// <summary>Returns true if the Type is or inherits from <see cref="IEnumerable"/> or an <see cref="IEnumerable{T}"/>.</summary>
        /// <param name="type">The type.</param>
        /// <returns>true if the Type is or inherits from <see cref="IEnumerable"/> or an <see cref="IEnumerable{T}"/>, false otherwise.</returns>
        public static bool IsEnumerable(this Type type)
        {
            return type == typeof(IEnumerable) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>)) || type.GetInterface(nameof(IEnumerable)) != null /* Is the last 'or' statement unreacheable? ---> */ || type.GetInterface(typeof(IEnumerable<>).FullName) != null;
        }

        /// <summary>Returns true if the Type is or inherits from <see cref="IList"/> or an <see cref="IList{T}"/>.</summary>
        /// <param name="type">The type.</param>
        /// <returns>true if the Type is or inherits from <see cref="IList"/> or an <see cref="IList{T}"/>, false otherwise.</returns>
        public static bool IsList(this Type type)
        {
            return type == typeof(IList) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IList<>)) || type.GetInterface(nameof(IList)) != null || type.GetInterface(typeof(IList<>).FullName) != null;
        }

        /// <summary>Returns true if the Type is or inherits from <see cref="IDictionary"/> or an <see cref="IDictionary{TKey, TValue}"/>.</summary>
        /// <param name="type">The type.</param>
        /// <returns>true if the Type is or inherits from <see cref="IDictionary"/> or an <see cref="IDictionary{TKey, TValue}"/>, false otherwise.</returns>
        public static bool IsDictionary(this Type type)
        {
            return type == typeof(IDictionary) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IDictionary<,>)) || type.GetInterface(nameof(IDictionary)) != null || type.GetInterface(typeof(IDictionary<,>).FullName) != null;
        }

        /// <summary>Converts and enum type to a <see cref="T:IDictionary{string, TValue}"/>, where the enum name as a string is the key and the enum is the value.</summary>
        /// <typeparam name="T">The Type of the enum.</typeparam>
        /// <param name="type">The Type of the enum.</param>
        /// <param name="comparer">An <see cref="T:IEqualityComparer{string}"/> to compare the enum name as a string. If null, the <see cref="StringComparer.OrdinalIgnoreCase"/> is used.</param>
        /// <returns>A <see cref="T:IDictionary{string, TValue}"/> where the enum name as a string is the key and the enum is the value.</returns>
        /// <remarks>There is not generic constraint that will enforce that a Type is an enum.</remarks>
        public static Dictionary<string, T> ToDictionary<T>(this Type type, IEqualityComparer<string> comparer = null)
        {
            if (!type.IsEnum)
                return null;
            return Enum.GetValues(type).Cast<T>().ToDictionary(e => Enum.GetName(type, e), e => e, comparer ?? StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>Converts and enum type to a <see cref="T:IDictionary{string, TValue}"/>, where the enum name as a string is the key and the enum is the value.</summary>
        /// <param name="type">The Type of the enum.</param>
        /// <param name="comparer">An <see cref="T:IEqualityComparer{string}"/> to compare the enum name as a string. If null, the <see cref="StringComparer.OrdinalIgnoreCase"/> is used.</param>
        /// <returns>A <see cref="T:IDictionary{string, TValue}"/> where the enum name as a string is the key and the enum is the value.</returns>
        /// <remarks>There is not generic constraint that will enforce that a Type is an enum.</remarks>
        public static Dictionary<string, object> ToDictionary(this Type type, IEqualityComparer<string> comparer = null)
        {
            if (!type.IsEnum)
                return null;
            return Enum.GetValues(type).Cast<object>().ToDictionary(e => Enum.GetName(type, e), e => e, comparer ?? StringComparer.OrdinalIgnoreCase);
        }
    }
}