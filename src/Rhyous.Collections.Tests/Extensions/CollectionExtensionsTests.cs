using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rhyous.Collections.Extensions;
using System;
using System.Collections.Generic;

namespace Rhyous.Collections.Tests.Extensions
{
    [TestClass]
    public class CollectionExtensionsTests
    {
        #region LastIndex
        [TestMethod]
        public void CollectionExtensions_LastIndex_Collection_Null_ThrowIfNull_Default_Throws()
        {
            // Arrange
            List<object> collection = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                collection.LastIndex();
            });
        }

        [TestMethod]
        public void CollectionExtensions_LastIndex_Collection_Null_ThrowIfNull_True_Throws()
        {
            // Arrange
            List<object> collection = null;
            bool throwIfNull = true;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                collection.LastIndex(throwIfNull);
            });
        }

        [TestMethod]
        public void CollectionExtensions_LastIndex_Collection_Null_ThrowIfNull_False_Returns_Minus1()
        {
            // Arrange
            List<object> collection = null;
            bool throwIfNull = false;

            // Act & Assert
            var result = collection.LastIndex(throwIfNull);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void CollectionExtensions_LastIndex_Collection_Empty_Returns_Minus1()
        {
            // Arrange
            var collection = new List<object>();

            // Act & Assert
            var result = collection.LastIndex();

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void CollectionExtensions_LastIndex_Collection_Populated_Returns_CountMinus1()
        {
            // Arrange
            var collection = new List<object> { 1, 2, 3, "a", "b", "c" };

            // Act & Assert
            var result = collection.LastIndex();

            // Assert
            Assert.AreEqual(collection.Count - 1, result);
        }
        #endregion
    }
}
