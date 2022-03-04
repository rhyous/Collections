using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.Collections.Tests.Dictionaries
{
    [TestClass]
    public class SortedConcurrentDictionaryTests
    {
        [TestMethod]
        public void SortedConcurrentDictionary_IsSorted_ForEach_InOrder_Test()
        {
            // Arrange
            var dictionary = new SortedConcurrentDictionary<string, int>();
            dictionary.Add("b", 2);
            dictionary.Add("d", 4);
            dictionary.Add("a", 1);
            dictionary.Add("e", 5);
            dictionary.Add("c", 3);

            var list = new List<KeyValuePair<string, int>>();

            // Act
            foreach (var kvp in dictionary)
            {
                list.Add(kvp);
            }

            // Assert
            Assert.AreEqual("a", list[0].Key);
            Assert.AreEqual("b", list[1].Key);
            Assert.AreEqual("c", list[2].Key);
            Assert.AreEqual("d", list[3].Key);
            Assert.AreEqual("e", list[4].Key);
        }

        [TestMethod]
        public void SortedConcurrentDictionary_IsSorted_ToList_InOrder_Test()
        {
            // Arrange
            var dictionary = new SortedConcurrentDictionary<string, int>();
            dictionary.Add("a", 1);
            dictionary.Add("b", 2);
            dictionary.Add("d", 4);
            dictionary.Add("e", 5);
            dictionary.Add("c", 3);

            // Act
            var list = dictionary.ToList();

            // Assert
            Assert.AreEqual("a", list[0].Key);
            Assert.AreEqual("b", list[1].Key);
            Assert.AreEqual("c", list[2].Key);
            Assert.AreEqual("d", list[3].Key);
            Assert.AreEqual("e", list[4].Key);
        }

        [TestMethod]
        public void SortedConcurrentDictionary_Keys_IsSorted_Test()
        {
            // Arrange
            var dictionary = new SortedConcurrentDictionary<string, int>();
            dictionary.Add("a", 1);
            dictionary.Add("b", 2);
            dictionary.Add("c", 3);
            dictionary.Add("d", 4);
            dictionary.Add("e", 5);

            // Act
            var list = dictionary.Keys.ToList();

            // Assert
            Assert.AreEqual("a", list[0]);
            Assert.AreEqual("b", list[1]);
            Assert.AreEqual("c", list[2]);
            Assert.AreEqual("d", list[3]);
            Assert.AreEqual("e", list[4]);
        }

        [TestMethod]
        public void SortedConcurrentDictionary_Keys_IsSorted_ByCustomComparerTest()
        {
            // Arrange
            var customComparer = new CertainFirstCharactersLastComparer('@', '$');
            var dictionary = new SortedConcurrentDictionary<string, int>(customComparer);
            dictionary.Add("@aa", 1);
            dictionary.Add("$bb", 2);
            dictionary.Add("acc", 3);
            dictionary.Add("bdd", 4);
            dictionary.Add("cee", 5);

            // Act
            var list = dictionary.Keys.ToList();

            // Assert
            Assert.AreEqual("acc", list[0]);
            Assert.AreEqual("bdd", list[1]);
            Assert.AreEqual("cee", list[2]);
            Assert.AreEqual("@aa", list[3]);
            Assert.AreEqual("$bb", list[4]);
        }
    }

    public class CertainFirstCharactersLastComparer : IComparer<string>
    {
        public char[] _SortLastChars;
        public Dictionary<char, int> _CharIntValues;

        public CertainFirstCharactersLastComparer(params char[] sortLastChars)
        {
            _SortLastChars = sortLastChars;
            int i = 0;
            _CharIntValues = (_SortLastChars == null || !_SortLastChars.Any())
                           ? new Dictionary<char, int>()
                           : _SortLastChars.ToDictionary(c => c, c => i++);
        }
        public int Compare(string left, string right)
        {
            if (left is null || right is null   // Either is null
            || !left.Any() || !right.Any()      // Either is empty
            || left[0] == right[0]              // First characters are equal
            || left == right                    // strings are equal
            || (!_SortLastChars.Any(c => c == left[0])
             && !_SortLastChars.Any(c => c == right[0]))) // Neither starts with a last character
                return left.CompareTo(right);

            // Left has last Char but right doesn't
            if (_SortLastChars.Any(c => c == left[0]) && !_SortLastChars.Any(c => c == right[0]))
                return 1;
            // Right has last Char but left doesn't
            if (!_SortLastChars.Any(c => c == left[0]) && _SortLastChars.Any(c => c == right[0]))
                return -1;

            // They both have a last character here, but not the same one
            return _CharIntValues[left[0]].CompareTo(_CharIntValues[right[0]]);
        }
    }
}
