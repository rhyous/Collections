using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.Collections
{
    /// <summary>Extensions for <see cref="IEnumerable{TSource}"/>.</summary>
    public static class EnumerableExtensions
    {
        /// <summary>Gets the second item out of an <see cref="IEnumerable{TSource}"/>.</summary>
        /// <typeparam name="TSource">The type of the items in the <see cref="IEnumerable{TSource}"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> instance.</param>
        /// <returns>The Second item.</returns>
        public static TSource Second<TSource>(this IEnumerable<TSource> source) => source.Skip(1).First();

        /// <summary>Gets the third item out of an <see cref="IEnumerable{TSource}"/>.</summary>
        /// <typeparam name="TSource">The type of the items in the <see cref="IEnumerable{TSource}"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> instance.</param>
        /// <returns>The Third item.</returns>
        public static TSource Third<TSource>(this IEnumerable<TSource> source) => source.Skip(2).First();

        /// <summary>Gets the fourth item out of an <see cref="IEnumerable{TSource}"/>.</summary>
        /// <typeparam name="TSource">The type of the items in the <see cref="IEnumerable{TSource}"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> instance.</param>
        /// <returns>The second item.</returns>
        public static TSource Fourth<TSource>(this IEnumerable<TSource> source) => source.Skip(3).First();

        /// <summary>Gets the fifth item out of an <see cref="IEnumerable{TSource}"/>.</summary>
        /// <typeparam name="TSource">The type of the items in the <see cref="IEnumerable{TSource}"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> instance.</param>
        /// <returns>The Fifth item.</returns>
        public static TSource Fifth<TSource>(this IEnumerable<TSource> source) => source.Skip(4).First();

        /// <summary>Gets the sixth item out of an <see cref="IEnumerable{TSource}"/>.</summary>
        /// <typeparam name="TSource">The type of the items in the <see cref="IEnumerable{TSource}"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> instance.</param>
        /// <returns>The Sixth item.</returns>
        public static TSource Sixth<TSource>(this IEnumerable<TSource> source) => source.Skip(5).First();

        /// <summary>Gets the seventh item out of an <see cref="IEnumerable{TSource}"/>.</summary>
        /// <typeparam name="TSource">The type of the items in the <see cref="IEnumerable{TSource}"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> instance.</param>
        /// <returns>The Seventh item.</returns>
        public static TSource Seventh<TSource>(this IEnumerable<TSource> source) => source.Skip(6).First();

        /// <summary>Gets the Eighth item out of an <see cref="IEnumerable{TSource}"/>.</summary>
        /// <typeparam name="TSource">The type of the items in the <see cref="IEnumerable{TSource}"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> instance.</param>
        /// <returns>The Eighth item.</returns>
        public static TSource Eighth<TSource>(this IEnumerable<TSource> source) => source.Skip(7).First();

        /// <summary>Gets the Ninth item out of an <see cref="IEnumerable{TSource}"/>.</summary>
        /// <typeparam name="TSource">The type of the items in the <see cref="IEnumerable{TSource}"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> instance.</param>
        /// <returns>The Ninth item.</returns>
        public static TSource Ninth<TSource>(this IEnumerable<TSource> source) => source.Skip(8).First();

        /// <summary>Gets the Tenth item out of an <see cref="IEnumerable{TSource}"/>.</summary>
        /// <typeparam name="TSource">The type of the items in the <see cref="IEnumerable{TSource}"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> instance.</param>
        /// <returns>The Tenth item.</returns>
        public static TSource Tenth<TSource>(this IEnumerable<TSource> source) => source.Skip(9).First();

        /// <summary>Checks if there are no items in an <see cref="IEnumerable{TSource}"/>.</summary>
        /// <typeparam name="TSource">The type of the items in the <see cref="IEnumerable{TSource}"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> instance.</param>
        /// <returns>True if the list has no items, false if it has items.</returns>
        public static bool None<TSource>(this IEnumerable<TSource> source)
        {
            return source == null || !source.Any();
        }

        /// <summary>Checks if there are no items in an <see cref="IEnumerable{TSource}"/> that match the predicate.</summary>
        /// <typeparam name="TSource">The type of the items in the <see cref="IEnumerable{TSource}"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> instance.</param>
        /// <param name="predicate">The predicate method to check.</param>
        /// <returns>True if the list has no items that match the predicate, false if it has items  that match the predicate.</returns>
        public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source == null || !source.Any(predicate);
        }

        /// <summary>Checks if the <see cref="IEnumerable{TSource}"/> is null or empty.</summary>
        /// <typeparam name="TSource">The type of the items in the <see cref="IEnumerable{TSource}"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> instance.</param>
        /// <returns>True if the list is null or empty, false otherwise.</returns>
        public static bool NullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source == null || !source.Any();
        }

        /// <summary>An algorithm to compare two IEnumerable{T} to see if they are have the same elements.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        /// <remarks> 10-100 Rule Exception - Algorithm</remarks>
        public static bool UnorderedEquals<T>(this IEnumerable<T> left, IEnumerable<T> right, IEqualityComparer<T> comparer = null)
        {
            if (left == null && right == null) // Scenario 1 - both null
                return true;
            if (left == null || right == null) // Scenario 2 - one null one not null
                return false;
            if (!left.Any() && !right.Any())   // Scenario 3 - Both are instantiated but empty lists
                return true;
            if (!left.Any() || !right.Any())   // Scenario 4 - Both are instantiated but only one is an empty list
                return false;
            var leftList = left.ToList();
            var rightList = right.ToList();
            if (leftList.Count != rightList.Count) // Scenario 5 - Both are instantiated but have different number of items.
                return false;
            var countDictionary = new Dictionary<T, int>(comparer);
            // Scenario 6 - We have to compare the actual elements
            foreach (var item in leftList)
            {
                if (item == null)
                    continue; // No action needed
                if (!countDictionary.TryGetValue(item, out int _))
                    countDictionary.Add(item, 1);
                else
                    ++countDictionary[item];
            }
            foreach (var item in rightList)
            {
                if (item == null)
                    continue; // No action needed
                if (!countDictionary.TryGetValue(item, out int _))
                    return false;
                --countDictionary[item];
            }
            return countDictionary.All(i => i.Value == 0);
            // Why no action is needed for nulls?
            // - If there are different number of items, the comparison is false.
            // - If there are the same number of items, but different number of nulls,
            //   countDictionary.All(i => i.Value == 0) will always be false.
        }

        /// <summary>An enum for Left or Right.</summary>
        public enum LeftOrRight 
        {
            /// <summary>Left</summary>
            Left,
            /// <summary>Right</summary>
            Right
        }

        /// <summary>Returns the differences between two unordered sequences of items.</summary>
        /// <typeparam name="T">The type of the items in the sequences to compare.</typeparam>
        /// <param name="left">The left sequence of items.</param>
        /// <param name="right">The right sequence of items.</param>
        /// <param name="comparer">A custom comparer.</param>
        /// <returns></returns>
        /// <remarks>10-100 Rule Exception - Algorithm</remarks>
        public static MismatchedItems<T> GetMismatchedItems<T>(this IEnumerable<T> left, IEnumerable<T> right, IEqualityComparer<T> comparer = null)
        {
            var mismatchedItems = new MismatchedItems<T>();
            if (left == null && right == null)
                return mismatchedItems;
            if (left == null)
            {
                mismatchedItems.Right.AddRange(right);
                return mismatchedItems;
            }
            if (right == null)
            {
                mismatchedItems.Left.AddRange(left);
                return mismatchedItems;
            }
            var leftList = left.ToList();
            var rightList = right.ToList();
            var leftDictionary = new Dictionary<T, int>(comparer);
            int nullCount = 0;
            foreach (var item in leftList)
            {
                if (item == null)
                {
                    nullCount++; // No action needed
                    continue;
                }
                if (!leftDictionary.TryGetValue(item, out int _))
                    leftDictionary.Add(item, 1);
                else
                    ++leftDictionary[item];
            }
            foreach (var item in rightList)
            {
                if (item == null)
                {
                    nullCount--; // No action needed
                    continue;
                }
                int value;
                if (!leftDictionary.TryGetValue(item, out value) || value < 1)
                {
                    mismatchedItems.Right.Add(item);
                    continue;
                }
                --leftDictionary[item];
            }
            mismatchedItems.Left.AddRange(leftDictionary.Where(i => i.Value > 0).Select(kvp => kvp.Key));
            if (nullCount == 0)
            {
                return mismatchedItems;
            }
            while (nullCount > 0)
            {
                mismatchedItems.Left.Add(default(T));
                --nullCount;
            }
            while (nullCount < 0)
            {
                mismatchedItems.Right.Add(default(T));
                ++nullCount;
            }
            return mismatchedItems;
        }
    }
}