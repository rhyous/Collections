using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.Collections.Tests.Extensions
{
    [TestClass]
    public class ValueAccessorExtensionsTests
    {
        [TestMethod]
        public void DefaultIntTests()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(0, ValueAccessorExtensions.Default<int>());
        }

        [TestMethod]
        public void DefaultObjectTests()
        {
            // Arrange
            // Act
            // Assert
            Assert.IsNull(ValueAccessorExtensions.Default<object>());
        }

        [TestMethod]
        public void GetDefaultIntTests()
        {
            // Arrange
            var type = typeof(int);
            // Act
            // Assert
            Assert.AreEqual(0, type.GetDefault());
        }
        
        [TestMethod]
        public void GetDefaultObjectTests()
        {
            // Arrange
            var type = typeof(object);
            // Act
            // Assert
            Assert.IsNull(type.GetDefault());
        }
    }
}
