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
            dictionary.Add("a", 1);
            dictionary.Add("b", 2);
            dictionary.Add("c", 3);
            dictionary.Add("d", 4);
            dictionary.Add("e", 5);

            // Act
            var list = new List<KeyValuePair<string, int>>();
            foreach (var kvp in dictionary)
            {
                list.Add(kvp);
            }

            // Assert
            Assert.AreEqual(list[0].Key, "a");
            Assert.AreEqual(list[1].Key, "b");
            Assert.AreEqual(list[2].Key, "c");
            Assert.AreEqual(list[3].Key, "d");
            Assert.AreEqual(list[4].Key, "e");
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
            Assert.AreEqual(list[0], "a");
            Assert.AreEqual(list[1], "b");
            Assert.AreEqual(list[2], "c");
            Assert.AreEqual(list[3], "d");
            Assert.AreEqual(list[4], "e");
        }
    }
}
