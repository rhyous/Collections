using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.Collections.Tests
{
    [TestClass]
    public class DictionaryDefaultValueProviderExtensionsTests
    {
        #region GetValueOrDefault
        [TestMethod]
        public void DictionaryDefaultValueProviderExtensions_NullDictionary_Test()
        {
            // Arrange
            NullSafeDictionary<int, string> dictionary = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => { dictionary.GetValueOrDefault(27); }); // Does nothing
        }

        [TestMethod]
        public void DictionaryDefaultValueProviderExtensions_GetValueOrDefault_DictionaryHasDefaultValueProvider_Test()
        {
            // Arrange
            NullSafeDictionary<int, string> dictionary = new NullSafeDictionary<int, string>((int i) => { return i.ToString(); });

            // Act
            var actual = dictionary.GetValueOrDefault(27); // Does nothing

            // Assert
            Assert.AreEqual("27", actual);
        }

        [TestMethod]
        public void DictionaryDefaultValueProviderExtensions_DefaultValueProviderInMethod_Test()
        {
            // Arrange
            NullSafeDictionary<int, string> dictionary = new NullSafeDictionary<int, string>();

            // Act
            var actual = dictionary.GetValueOrDefault(27, (int i) => { return i.ToString(); }); // Does nothing

            // Assert
            Assert.AreEqual("27", actual);
        }

        [TestMethod]
        public void DictionaryDefaultValueProviderExtensions_NoDefaultValueProvider_Test()
        {
            // Arrange
            NullSafeDictionary<int, string> dictionary = new NullSafeDictionary<int, string>();

            // Act
            var actual = dictionary.GetValueOrDefault(27); // Does nothing

            // Assert
            Assert.AreEqual("", actual);
        }

        [TestMethod]
        public void DictionaryDefaultValueProviderExtensions_ValueExists_Test()
        {
            // Arrange
            Func<int, string> method = (int i) => { return i.ToString(); };
            NullSafeDictionary<int, string> dictionary = new NullSafeDictionary<int, string>(method) { { 27, "ExistingValue" } };

            // Act
            var actual = dictionary.GetValueOrDefault(27, method); // Does nothing

            // Assert
            Assert.AreEqual("ExistingValue", actual);
        }
        #endregion


        #region GetValueOrDefault IEnumerable<KeyValuePair<TKey, TValue>> Tests

        [TestMethod]
        public void DictionaryExtensions_EnumerableKvp_GetValueOrDefault_NullEnumerable_Throws_Test()
        {
            // Arrange
            List<KeyValuePair<int, string>> enumerable = null;
            var defaultValue = "someDefaultValue";

            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentNullException>(() =>
            {
                enumerable.GetValueOrDefault(27, defaultValue);
            });
            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: kvps", ex.Message);
        }

        [TestMethod]
        public void DictionaryExtensions_EnumerableKvp_GetValueOrDefault_KeyExistButValueNull_Test()
        {
            // Arrange
            var enumerable = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(27, null)
            };
            var defaultValue = "someDefaultValue";

            // Act
            var actual = enumerable.GetValueOrDefault(27, defaultValue);

            // Assert
            Assert.AreEqual(defaultValue, actual);
        }

        [TestMethod]
        public void DictionaryExtensions_EnumerableKvp_GetValueOrDefault_KeyExists_Test()
        {
            // Arrange
            var enumerable = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(27, "value27")
            };

            // Act
            var actual = enumerable.GetValueOrDefault(27);

            // Assert
            Assert.AreEqual("value27", actual);
        }

        [TestMethod]
        public void DictionaryExtensions_EnumerableKvp_GetValueOrDefault_KeyDoesNotExist_Test()
        {
            // Arrange
            var enumerable = new List<KeyValuePair<int, string>>();

            // Act
            var actual = enumerable.GetValueOrDefault(27);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void DictionaryExtensions_EnumerableKvp_GetValueOrDefault_KeyDoesNotExist_WithDefaultValue_Test()
        {
            // Arrange
            var enumerable = new List<KeyValuePair<int, string>>();

            // Act
            var actual = enumerable.GetValueOrDefault(27, "defaultValue");

            // Assert
            Assert.AreEqual("defaultValue", actual);
        }

        #endregion
    }
}
