using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;
using Rhyous.UnitTesting;

namespace Rhyous.Collections.Tests
{
    public partial class NameValueCollectionExtensionsTests
    {
        [TestMethod]
        public void AppendToValue_CollectionNullTest()
        {
            // Arrange
            NameValueCollection collection = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                collection.AppendToValue("Item1", "value2");
            });
        }

        [TestMethod]
        [PrimitiveList(null, "")]
        public void AppendToValue_Key_NullOrEmptyTest(string key)
        {
            // Arrange
            var collection = new NameValueCollection();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                collection.AppendToValue(key, "value2");
            });
        }


        [TestMethod]
        [PrimitiveList(null, "")]
        public void AppendToValue_Collection_Empty_Value_NullOrEmptyTest(string value)
        {
            // Arrange
            var key = "key1";
            var collection = new NameValueCollection();

            // Act
                collection.AppendToValue(key, value);

            // Assert
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void AppendToValue_Key_SpacesAreValid_Test()
        {
            // Arrange
            var key = "  ";
            var collection = new NameValueCollection();
            collection.Add(key, "1");

            // Act
            collection.AppendToValue(key, "value2");

            // Assert
            Assert.AreEqual("1,value2", collection.Get(key));
        }

        [TestMethod]
        [PrimitiveList(null, "")]
        public void AppendToValue_CurrentValue_NullOrEmpty_IsReplaced(string currentValue)
        {
            // Arrange
            var key = "Item1";
            var collection = new NameValueCollection();
            collection.Add(key, currentValue);

            // Act
            collection.AppendToValue(key, "value2");

            // Assert
            Assert.AreEqual("value2", collection.Get(key));
        }
    }
}
