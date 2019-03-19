using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.Collections.Tests
{
    [TestClass]
    public class ValueAccessorExtensionsTests
    {
        #region Default
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
        #endregion

        #region GetDefault
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
        #endregion

        #region GetPropertyValue
        [TestMethod]
        public void ValueAccessorExtensions_GetPropertyValue_CanUseAType_Test()
        {
            // Arrange
            var obj = new { MyProperty = "MyValue" };

            // Act
            var value = obj.GetPropertyValue<string>("MyProperty");

            // Assert
            Assert.AreEqual("MyValue", value);
            Assert.AreEqual(typeof(string), value.GetType());
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetPropertyValue_CanUseAValueType_Test()
        {
            // Arrange
            var obj = new { MyProperty = 1 };

            // Act
            var value = obj.GetPropertyValue<int>("MyProperty");

            // Assert
            Assert.AreEqual(1, value);
            Assert.AreEqual(typeof(int), value.GetType());
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetPropertyValue_Invalid_TWillReturnDefault_Test()
        {
            // Arrange
            var obj = new { MyProperty = "Some string" };

            // Act
            var value = obj.GetPropertyValue<int>("MyProperty");

            // Assert
            Assert.AreEqual(default(int), value);
            Assert.AreEqual(typeof(int), value.GetType());
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetPropertyValue_CanGetAnObjectFromAComplexObject_Test()
        {
            // Arrange
            var obj = new { MyProperty = new Person() { Name = "Matt" } };

            // Act
            var value = obj.GetPropertyValue<Person>("MyProperty");

            // Assert
            Assert.IsNotNull(value);
            Assert.AreEqual(typeof(Person), value.GetType());
        }
     
        #endregion
    }
}
