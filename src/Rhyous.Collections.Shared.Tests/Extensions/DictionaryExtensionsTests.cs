using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Linq;

namespace Rhyous.Collections.Tests
{
    [TestClass]
    public class DictionaryExtensionsTests
    {
        #region Add KeyValuePair<TKey, TValue>
        [TestMethod]
        public void DictionaryExtensions_Add_KeyValuePair_Null_Test()
        {
            // Arrange
            Dictionary<int, string> dictionary = null;
            int key = 27;
            string value = "some value";
            var kvp = new KeyValuePair<int, string>(key, value);

            // Act
            dictionary.Add(kvp);

            // Assert
            Assert.IsNull(dictionary);
        }

        [TestMethod]
        public void DictionaryExtensions_Add_KeyValuePair_Test()
        {
            // Arrange
            var dictionary = new Dictionary<int, string>();
            int key = 27;
            string value = "some value";
            var kvp = new KeyValuePair<int, string>(key, value);

            // Act
            dictionary.Add(kvp);

            // Assert
            Assert.AreEqual(1, dictionary.Count);
            Assert.AreEqual(27, dictionary.Keys.First());
            Assert.AreEqual("some value", dictionary[27]);
        }
        #endregion

        #region AddIfNew
        [TestMethod]
        public void DictionaryExtensions_AddIfNew_NullDictionary_Test()
        {
            // Arrange
            Dictionary<int, string> dictionary = null;

            // Act
            dictionary.AddIfNew(27, "value27"); // Does nothing

            // Assert
            Assert.IsNull(dictionary);
        }

        [TestMethod]
        public void DictionaryExtensions_AddIfNew_New_Test()
        {
            // Arrange
            var dictionary = new Dictionary<int, string>();

            // Act
            dictionary.AddIfNew(27, "value27");

            // Assert
            Assert.AreEqual(1, dictionary.Count);
            Assert.AreEqual(27, dictionary.Keys.First());
            Assert.AreEqual("value27", dictionary[27]);
        }

        [TestMethod]
        public void DictionaryExtensions_AddIfNew_NotNew_Test()
        {
            // Arrange
            var dictionary = new Dictionary<int, string>() { { 27, "pre-value27" } };

            // Act
            dictionary.AddIfNew(27, "value27");

            // Assert
            Assert.AreEqual(1, dictionary.Count);
            Assert.AreEqual(27, dictionary.Keys.First());
            Assert.AreEqual("pre-value27", dictionary[27]);
        }
        #endregion

        #region AddIfNewAndNotNull
        [TestMethod]
        public void DictionaryExtensions_AddIfNewAndNotNull_NullDictionary_Test()
        {
            // Arrange
            Dictionary<int, string> dictionary = null;

            // Act
            dictionary.AddIfNewAndNotNull(27, "value27"); // Does nothing

            // Assert
            Assert.IsNull(dictionary);
        }

        [TestMethod]
        public void DictionaryExtensions_AddIfNewAndNotNull_New_Null_Test()
        {
            // Arrange
            var dictionary = new Dictionary<int, string>();

            // Act
            dictionary.AddIfNewAndNotNull(27, null); // not added

            // Assert
            Assert.AreEqual(0, dictionary.Count);
        }

        [TestMethod]
        public void DictionaryExtensions_AddIfNewAndNotNull_New_Test()
        {
            // Arrange
            var dictionary = new Dictionary<int, string>();

            // Act
            dictionary.AddIfNewAndNotNull(27, "value27");

            // Assert
            Assert.AreEqual(1, dictionary.Count);
            Assert.AreEqual(27, dictionary.Keys.First());
            Assert.AreEqual("value27", dictionary[27]);
        }

        [TestMethod]
        public void DictionaryExtensions_AddIfNewAndNotNull_NotNew_Test()
        {
            // Arrange
            var dictionary = new Dictionary<int, string>() { { 27, "pre-value27" } };

            // Act
            dictionary.AddIfNewAndNotNull(27, "value27");

            // Assert
            Assert.AreEqual(1, dictionary.Count);
            Assert.AreEqual(27, dictionary.Keys.First());
            Assert.AreEqual("pre-value27", dictionary[27]);
        }
        #endregion

        #region AddOrReplace
        [TestMethod]
        public void DictionaryExtensions_AddOrReplace_NullDictionary_Test()
        {
            // Arrange
            Dictionary<int, string> dictionary = null;

            // Act
            dictionary.AddOrReplace(27, "value27"); // Does nothing

            // Assert
            Assert.IsNull(dictionary);
        }

        [TestMethod]
        public void DictionaryExtensions_AddOrReplace_New_Test()
        {
            // Arrange
            var dictionary = new Dictionary<int, string>();

            // Act
            dictionary.AddOrReplace(27, "value27");

            // Assert
            Assert.AreEqual(1, dictionary.Count);
            Assert.AreEqual(27, dictionary.Keys.First());
            Assert.AreEqual("value27", dictionary[27]);
        }

        [TestMethod]
        public void DictionaryExtensions_AddOrReplace_NotNew_Test()
        {
            // Arrange
            var dictionary = new Dictionary<int, string>() { { 27, "pre-value27" } };

            // Act
            dictionary.AddOrReplace(27, "value27");

            // Assert
            Assert.AreEqual(1, dictionary.Count);
            Assert.AreEqual(27, dictionary.Keys.First());
            Assert.AreEqual("value27", dictionary[27]);
        }

        [TestMethod]
        public void DictionaryExtensions_AddOrReplace_NotNew_Null_Test()
        {
            // Arrange
            var dictionary = new Dictionary<int, string>() { { 27, "pre-value27" } };

            // Act
            dictionary.AddOrReplace(27, null);

            // Assert
            Assert.AreEqual(1, dictionary.Count);
            Assert.AreEqual(27, dictionary.Keys.First());
            Assert.IsNull(dictionary[27]);
        }
        #endregion
    }
}
