using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;

namespace Rhyous.Collections.Tests.Extensions
{
    [TestClass]
    public partial class NameValueCollectionExtensionsTests
    {
        [TestMethod]
        public void GetValueCollectionNullTest()
        {
            // Arrange
            NameValueCollection collection = null;

            // Act
            var actual = collection.Get("Item1", -1);

            // Assert
            Assert.AreEqual(-1, actual);
        }

        [TestMethod]
        public void GetValueCollectionEmptyTest()
        {
            // Arrange
            NameValueCollection collection = null;

            // Act
            var actual = collection.Get(null, -1);

            // Assert
            Assert.AreEqual(-1, actual);
        }

        [TestMethod]
        public void GetValueCollectionSpacesAreValidTest()
        {
            // Arrange
            var collection = new NameValueCollection();
            collection.Add("  ", "1");

            // Act
            var actual = collection.Get("  ", 0);

            // Assert
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void GetValueByType()
        {
            // Arrange
            var collection = new NameValueCollection();
            collection.Add("Item1", "1");

            // Act
            var actual = collection.Get("Item1", 0);

            // Assert
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void Clone()
        {
            // Arrange
            var collection = new NameValueCollection();
            collection.Add("Item1", "1");
            collection.Add("Item2", "2");
            collection.Add("Item3", "3");

            // Act
            var actual = collection.Clone();

            // Assert
            CollectionAssert.AreEqual(collection, actual);
        }

        [TestMethod]
        public void CloneWithExclusions()
        {
            // Arrange
            var collection = new NameValueCollection();
            collection.Add("Item1", "1");
            collection.Add("Item2", "2");
            collection.Add("Item3", "3");

            // Act
            var actual = collection.Clone("item3");

            // Assert
            Assert.AreEqual(2, actual.Count);
        }

        [TestMethod]
        public void CloneWithExclusionsCaseSensitive()
        {
            // Arrange
            var collection = new NameValueCollection();
            collection.Add("Item1", "1");
            collection.Add("Item2", "2");
            collection.Add("Item3", "3");

            // Act
            var actual = collection.Clone(StringComparer.Ordinal, "item3");

            // Assert
            Assert.AreEqual(3, actual.Count);
        }
    }
}
