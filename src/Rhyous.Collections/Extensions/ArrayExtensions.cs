using System;

namespace Rhyous.Collections.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] Fill<T>(this T[] array, T value)
        {
            if (array is null) { throw new ArgumentNullException(nameof(array)); }
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
            return array;
        }

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
    }
}