using System;
using System.Linq;

namespace Rhyous.Collections
{
    /// <summary>Extension methods for Arrays.</summary>
    public static class ArrayExtensions
    {
        #region ToJson
        /// <summary>Converts a primitive array to Json. This does not serialize complex classes.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns>The array in json format.</returns>
        public static string ToJson<T>(this T[] array)
        // where T : INumber<T> -- Future
        {
            if (!typeof(T).IsPrimitive)
                throw new ArgumentException("The type of T must be a primitive.");
            if (array == null || array.Length == 0)
                return "[]";
            return $"[{string.Join(",", array.Select(b => b is string ? $"'b'" : b.ToString()))}]";
        }

        /// <summary>Converts a primitive array to Json. This does not serialize complex classes.</summary>
        /// <param name="array">A string array.</param>
        /// <returns>The string array in json format.</returns>
        public static string ToJson(this string[] array)
        {
            if (array == null || array.Length == 0)
                return "[]";
            return $"[{string.Join(",", array.Select(b => $"'{b}'"))}]";
        }

        /// <summary>Converts a primitive array to Json. This does not serialize complex classes.</summary>
        /// <param name="array">A Guid array</param>
        /// <returns>The Guid array in json format.</returns>
        public static string ToJson(this Guid[] array)
        {
            if (array == null || array.Length == 0)
                return "[]";
            return $"[{string.Join(",", array.Select(b => $"'{b}'"))}]";
        }
        #endregion

        #region Fill
        /// <summary>Fills an array so that every item in the array has the same value.</summary>
        public static T[] Fill<T>(this T[] array, T value)
        {
            if (array is null) { throw new ArgumentNullException(nameof(array)); }
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
            return array;
        }

        /// <summary>Fills a two dimensional array so that every item in the array has the same value.</summary>
        public static T[,] Fill<T>(this T[,] array, T value)
        {
            if (array is null) { throw new ArgumentNullException(nameof(array)); }
            var jLength = array.GetLength(0);
            var kLength = array.GetLength(1);
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < jLength; j++)
                    for (int k = 0; k < kLength; k++)
                        array[j, k] = value;
            return array;
        }

        /// <summary>Fills a three dimensional array so that every item in the array has the same value.</summary>
        public static T[,,] Fill<T>(this T[,,] array, T value)
        {
            if (array is null) { throw new ArgumentNullException(nameof(array)); }
            var jLength = array.GetLength(0);
            var kLength = array.GetLength(1);
            var lLength = array.GetLength(2);
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < jLength; j++)
                    for (int k = 0; k < kLength; k++)
                        for (int l = 0; l < lLength; l++)
                            array[j, k, l] = value;
            return array;
        }


        /// <summary>Fills a four dimensional array so that every item in the array has the same value.</summary>
        public static T[,,,] Fill<T>(this T[,,,] array, T value)
        {
            if (array is null) { throw new ArgumentNullException(nameof(array)); }
            var jLength = array.GetLength(0);
            var kLength = array.GetLength(1);
            var lLength = array.GetLength(2);
            var mLength = array.GetLength(3);
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < jLength; j++)
                    for (int k = 0; k < kLength; k++)
                        for (int l = 0; l < lLength; l++)
                            for (int m = 0; m < mLength; m++)
                                array[j, k, l, m] = value;
            return array;
        }

        /// <summary>Fills a five dimensional array so that every item in the array has the same value.</summary>
        public static T[,,,,] Fill<T>(this T[,,,,] array, T value)
        {
            if (array is null) { throw new ArgumentNullException(nameof(array)); }
            var jLength = array.GetLength(0);
            var kLength = array.GetLength(1);
            var lLength = array.GetLength(2);
            var mLength = array.GetLength(3);
            var nLength = array.GetLength(4);
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < jLength; j++)
                    for (int k = 0; k < kLength; k++)
                        for (int l = 0; l < lLength; l++)
                            for (int m = 0; m < mLength; m++)
                                for (int n = 0; n < nLength; n++)
                                    array[j, k, l, m, n] = value;
            return array;
        }
        #endregion
    }
}