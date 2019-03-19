using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Linq;

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
            NullSafeDictionary<int, string> dictionary = new NullSafeDictionary<int, string>((int i)=> { return i.ToString(); });

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
            Func<int,string> method = (int i) => { return i.ToString(); };
            NullSafeDictionary<int, string> dictionary = new NullSafeDictionary<int, string>(method) { { 27, "ExistingValue" } };

            // Act
            var actual = dictionary.GetValueOrDefault(27, method); // Does nothing

            // Assert
            Assert.AreEqual("ExistingValue", actual);
        }
        #endregion

    }
}
