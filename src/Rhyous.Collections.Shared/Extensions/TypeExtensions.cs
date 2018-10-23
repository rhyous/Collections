using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.Collections
{
    public static class TypeExtensions
    {
        public static bool IsCollection(this Type type)
        {
            return type == typeof(ICollection) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ICollection<>)) || type.GetInterface(nameof(ICollection)) != null || type.GetInterface(typeof(ICollection<>).FullName) != null;
        }

        public static bool IsEnumerable(this Type type)
        {
            return type == typeof(IEnumerable) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>)) || type.GetInterface(nameof(IEnumerable)) != null || type.GetInterface(typeof(IEnumerable<>).FullName) != null;
        }

        public static bool IsList(this Type type)
        {
            return type == typeof(IList) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IList<>)) || type.GetInterface(nameof(IList)) != null || type.GetInterface(typeof(IList<>).FullName) != null;
        }

        public static bool IsDictionary(this Type type)
        {
            return type == typeof(IDictionary) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IDictionary<,>)) || type.GetInterface(nameof(IDictionary)) != null || type.GetInterface(typeof(IDictionary<,>).FullName) != null;
        }

        public static Dictionary<string, T> ToDictionary<T>(this Type type, IEqualityComparer<string> comparer = null)
        {
            if (!type.IsEnum)
                return null;
            return Enum.GetValues(type).Cast<T>().ToDictionary(e => Enum.GetName(type, e), e => e, comparer ?? StringComparer.OrdinalIgnoreCase);
        }

        public static Dictionary<string, object> ToDictionary(this Type type, IEqualityComparer<string> comparer = null)
        {
            if (!type.IsEnum)
                return null;
            return Enum.GetValues(type).Cast<object>().ToDictionary(e => Enum.GetName(type, e), e => e, comparer ?? StringComparer.OrdinalIgnoreCase);
        }
    }
}