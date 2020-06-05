using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.Collections;
using System;
using System.Collections.Specialized;

namespace Rhyous.Collections.Tests.Extensions
{
    [TestClass]
    public partial class NameValueCollectionExtensionsTests
    {
        [TestMethod]
        public void NameValueCollectionExtensions_AppendToValue_Null_Collection_Throws()
        {
            // Arrange
            NameValueCollection collection = null;
            string key = "Key1";
            string append = "2";

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                collection.AppendToValue(key, append);
            });
        }

        [TestMethod]
        public void NameValueCollectionExtensions_AppendToValue_Null_Key_Throws()
        {
            // Arrange
            NameValueCollection collection = new NameValueCollection { { "Key1", "1" } };
            string key = null;
            string append = "2";
            string separator = ",";

            // Act
            // Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                collection.AppendToValue(key, append, separator);
            });
        }

        [TestMethod]
        public void NameValueCollectionExtensions_AppendToValue_Null_Append_DoesNothing()
        {
            // Arrange
            NameValueCollection collection = new NameValueCollection { { "Key1", "1" } };
            string key = "Key1";
            string append = null;
            var expected = "1";

            // Act
            collection.AppendToValue(key, append);
            var actual = collection.Get(key, "");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NameValueCollectionExtensions_AppendToValue_ValidParams()
        {
            // Arrange
            NameValueCollection collection = new NameValueCollection { { "Key1", "1" } };
            string key = "Key1";
            string append = "2";
            var expected = "1,2";
            string separator = ",";

            // Act
            collection.AppendToValue(key, append, separator);
            var actual = collection.Get(key, "");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NameValueCollectionExtensions_AppendToValue_KeyDoesNotExist()
        {
            // Arrange
            NameValueCollection collection = new NameValueCollection { { "Key1", "1" } };
            string key = "Key2";
            string append = "2";
            var expected = "2";

            // Act
            collection.AppendToValue(key, append);
            var actual = collection.Get(key, "");

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
