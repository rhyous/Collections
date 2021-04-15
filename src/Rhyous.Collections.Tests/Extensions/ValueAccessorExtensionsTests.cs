using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.Collections.Tests
{
    [TestClass]
    public class ValueAccessorExtensionsTests
    {
        #region Comparison
        [TestMethod]
        public void ValueAccessorExtensions_StringComparison_DefaultValue_Test()
        {
            Assert.AreEqual(StringComparison.OrdinalIgnoreCase, ValueAccessorExtensions.Comparison);
            ValueAccessorExtensions.Comparison = StringComparison.InvariantCultureIgnoreCase;
            Assert.AreEqual(StringComparison.InvariantCultureIgnoreCase, ValueAccessorExtensions.Comparison);
            ValueAccessorExtensions.Comparison = StringComparison.OrdinalIgnoreCase;
            Assert.AreEqual(StringComparison.OrdinalIgnoreCase, ValueAccessorExtensions.Comparison);
        }
        #endregion

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

        #region GetPropertyInfo from Object

        [TestMethod]
        public void ValueAccessorExtensions_GetPropertyInfo_Test()
        {
            // Arrange
            var obj = new { Prop1 = "Value1" };

            // Act
            var value = obj.GetPropertyInfo("Prop1");

            // Assert
            Assert.IsTrue(value is PropertyInfo);
            Assert.AreEqual("Prop1", value.Name);
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetPropertyInfo_PropertyNotExists_Test()
        {
            // Arrange
            var obj = new { Prop1 = "Value1" };

            // Act
            var value = obj.GetPropertyInfo("Prop2");

            // Assert
            Assert.IsNull(value);
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetPropertyInfo_PropertyNotExists_NullObj_Test()
        {
            // Arrange
            var obj = new { Prop1 = "Value1" };
            obj = null;

            // Act
            var value = obj.GetPropertyInfo("Prop1");

            // Assert
            Assert.IsNull(value);
        }

        internal class PrivatePropertyClass
        {
            public PrivatePropertyClass(double percentage) { Percentage = percentage; }
            private double Percentage { get; set; }
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetPropertyInfo_PrivateProperty_Test()
        {
            // Arrange
            var obj = new PrivatePropertyClass(99.9);

            // Act
            var value = obj.GetPropertyInfo("Percentage");

            // Assert
            Assert.AreEqual("Percentage", value.Name);
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetPropertyInfo_PrivateProperty_CaseInsensitive_Test()
        {
            // Arrange
            var obj = new PrivatePropertyClass(99.9);

            // Act
            var value = obj.GetPropertyInfo("percentage");

            // Assert
            Assert.AreEqual("Percentage", value.Name);
        }

        #endregion

        #region GetPropertyInfo from type

        [TestMethod]
        public void ValueAccessorExtensions_GetPropertyInfo_FromType_Test()
        {
            // Arrange
            var obj = new { Prop1 = "Value1" };

            // Act
            var value = obj.GetType().GetPropertyInfo("Prop1");

            // Assert
            Assert.IsTrue(value is PropertyInfo);
            Assert.AreEqual("Prop1", value.Name);
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetPropertyInfo_FromType_PropertyNotExists_Test()
        {
            // Arrange
            var obj = new { Prop1 = "Value1" };

            // Act
            var value = obj.GetType().GetPropertyInfo("Prop2");

            // Assert
            Assert.IsNull(value);
        }


        [TestMethod]
        public void ValueAccessorExtensions_GetPropertyInfo_PropertyNotExists_NullType_Test()
        {
            // Arrange
            Type t = null;

            // Act
            var value = t.GetPropertyInfo("Prop1");

            // Assert
            Assert.IsNull(value);
        }

        [TestMethod]
        public void TypeExtensions_GetPropertyInfoCaseSafe_Test()
        {
            // Arrange
            var type = typeof(Person);
            var name = nameof(Person.Name);

            // Act
            var lowerResult = type.GetPropertyInfo(name.ToLower());
            var upperResult = type.GetPropertyInfo(name.ToUpper());
            var exactResult = type.GetPropertyInfo(name);

            // Assert
            Assert.AreEqual(name, lowerResult.Name);
            Assert.AreEqual(name, upperResult.Name);
            Assert.AreEqual(name, exactResult.Name);
        }

        /// <summary>
        /// If an Entity has this situation, you have to be case exact.
        /// </summary>
        [TestMethod]
        public void TypeExtensions_GetPropertyInfoCaseSafe_EntityWithCaseDifferentProps_Test()
        {
            // Arrange
            var pascalCase = nameof(EntityWithCaseDifferentProps.SomeId);
            var camelCase = nameof(EntityWithCaseDifferentProps.someId);
            var lower = nameof(EntityWithCaseDifferentProps.someId);
            var upper = lower.ToUpper();
            var type = typeof(EntityWithCaseDifferentProps);

            // Act
            var pascalResult= type.GetPropertyInfo(pascalCase);
            var camelResult = type.GetPropertyInfo(camelCase);
            var lowerResult = type.GetPropertyInfo(lower);
            Assert.ThrowsException<AmbiguousMatchException>(() =>
            {
                type.GetPropertyInfo(upper);
            });

            // Assert
            Assert.AreEqual(pascalCase, pascalResult.Name);
            Assert.AreEqual(camelCase, camelResult.Name);
            Assert.AreEqual(lower, lowerResult.Name);
        }

        #endregion

        #region GetPropertyValue returns object

        [TestMethod]
        public void ValueAccessorExtensions_GetPropertyValue_Test()
        {
            // Arrange
            var obj = new { Prop1 = "Value1" };

            // Act
            var value = obj.GetPropertyValue("Prop1");

            // Assert
            Assert.IsTrue(value is string);
            Assert.AreEqual("Value1", value.ToString());
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetPropertyValue_PropertyNotExists_Test()
        {
            // Arrange
            var obj = new { Prop1 = "Value1" };

            // Act
            var value = obj.GetPropertyValue("Prop2");

            // Assert
            Assert.IsNull(value);
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetPropertyValue_PropertyNotExists_Primitive_Test()
        {
            // Arrange
            var obj = new { Prop1 = 27 };

            // Act
            // Assert
            Assert.ThrowsException<NullReferenceException>(() =>
            {
                var value = (int)obj.GetPropertyValue("Prop2");
            });
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetPropertyValue_PropertyNotExists_PrimitiveWithDefault_Test()
        {
            // Arrange
            var obj = new { Prop1 = 27 };

            // Act
            var value = obj.GetPropertyValue("Prop2", 11);

            // Assert
            Assert.AreEqual(11, value);
        }


        #endregion

        #region GetPropertyValue Generic
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

        #region GetStaticPropertyValue returns object

        internal class AStatic { public static int S => 27; };

        [TestMethod]
        public void ValueAccessorExtensions_GetStaticPropertyValue_Test()
        {
            // Arrange
            var type = typeof(AStatic);

            // Act
            var value = type.GetStaticPropertyValue("S");

            // Assert
            Assert.IsTrue(value is int);
            Assert.AreEqual(27, value);
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetStaticPropertyValue_PropertyNotExists_Test()
        {
            // Arrange
            var type = typeof(AStatic);

            // Act
            var value = type.GetStaticPropertyValue("R");

            // Assert
            Assert.IsNull(value);
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetStaticPropertyValue_PropertyNotExists_Primitive_Test()
        {
            // Arrange
            var type = typeof(AStatic);

            // Act
            // Assert
            Assert.ThrowsException<NullReferenceException>(() =>
            {
                var value = (int)type.GetStaticPropertyValue("R");
            });
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetStaticPropertyValue_PropertyNotExists_PrimitiveWithDefault_Test()
        {
            // Arrange
            var type = typeof(AStatic);

            // Act
            var value = type.GetStaticPropertyValue("R", 11);

            // Assert
            Assert.AreEqual(11, value);
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetStaticPropertyValue_PropertyNotExists_NullType_Test()
        {
            // Arrange
            Type t = null;

            // Act
            var value = t.GetStaticPropertyValue("R");

            // Assert
            Assert.IsNull(value);
        }


        [TestMethod]
        public void ValueAccessorExtensions_GetStaticPropertyValue_PropertyNotExists_NullType_DefaultValue_Test()
        {
            // Arrange
            Type t = null;

            // Act
            var value = t.GetStaticPropertyValue("R", 11);

            // Assert
            Assert.AreEqual(11, value);
        }

        internal class BStatic : AStatic { }

        [TestMethod]
        public void ValueAccessorExtensions_GetStaticPropertyValue_PropertyNotExists_InheritedStatic_Test()
        {
            // Arrange
            Type t = typeof(BStatic);

            // Act
            var value = t.GetStaticPropertyValue("S");

            // Assert
            Assert.AreEqual(27, value);
        }
        #endregion

        #region GetFieldInfo from Object

        internal class TestField
        {
            private readonly int _i;
            public TestField(int i) { _i = i; }
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetFieldInfo_Test()
        {
            // Arrange
            var obj = new TestField(27);

            // Act
            var value = obj.GetFieldInfo("_i");

            // Assert
            Assert.IsTrue(value is FieldInfo);
            Assert.AreEqual("_i", value.Name);
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetFieldInfo_FieldNotExists_Test()
        {
            // Arrange
            var obj = new TestField(27);

            // Act
            var value = obj.GetFieldInfo("Prop2");

            // Assert
            Assert.IsNull(value);
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetFieldInfo_FieldNotExists_NullObj_Test()
        {
            // Arrange
            var obj = new TestField(27);
            obj = null;

            // Act
            var value = obj.GetFieldInfo("_i");

            // Assert
            Assert.IsNull(value);
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetFieldInfo_CaseInsensitive_Test()
        {
            // Arrange
            var obj = new TestField(27);
            obj = null;

            // Act
            var value = obj.GetFieldInfo("_I");

            // Assert
            Assert.IsNull(value);
        }

        #endregion

        #region GetFieldInfo from type

        [TestMethod]
        public void ValueAccessorExtensions_GetFieldInfo_FromType_Test()
        {
            // Arrange
            var obj = new TestField(27);

            // Act
            var value = obj.GetType().GetFieldInfo("_i");

            // Assert
            Assert.IsTrue(value is FieldInfo);
            Assert.AreEqual("_i", value.Name);
        }

        public class TestField2 { public long Tics = 100000000001; }

        [TestMethod]
        public void ValueAccessorExtensions_GetFieldInfo_FromType_Public_Test()
        {
            // Arrange
            var obj = new TestField2();            

            // Act
            var value = obj.GetType().GetFieldInfo("Tics");

            // Assert
            Assert.IsTrue(value is FieldInfo);
            Assert.AreEqual("Tics", value.Name);
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetFieldInfo_FromType_FieldNotExists_Test()
        {
            // Arrange
            var obj = new TestField(27);

            // Act
            var value = obj.GetType().GetFieldInfo("Prop2");

            // Assert
            Assert.IsNull(value);
        }


        [TestMethod]
        public void ValueAccessorExtensions_GetFieldInfo_FieldNotExists_NullType_Test()
        {
            // Arrange
            Type t = null;

            // Act
            var value = t.GetFieldInfo("_i");

            // Assert
            Assert.IsNull(value);
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetFieldInfo_ByType_CaseInsensitive_Test()
        {
            // Arrange
            var obj = new TestField(27);

            // Act
            var value = obj.GetType().GetFieldInfo("_I");

            // Assert
            Assert.IsTrue(value is FieldInfo);
            Assert.AreEqual("_i", value.Name);
        }

        #endregion

        #region GetFieldValue returns object

        [TestMethod]
        public void ValueAccessorExtensions_GetFieldValue_Test()
        {
            // Arrange
            var obj = new TestField(27);

            // Act
            var value = obj.GetFieldValue("_i");

            // Assert
            Assert.IsTrue(value is int);
            Assert.AreEqual(27, value);
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetFieldValue_FieldNotExists_Test()
        {
            // Arrange
            var obj = new TestField(27);

            // Act
            var value = obj.GetFieldValue("_i2");

            // Assert
            Assert.IsNull(value);
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetFieldValue_FieldNotExists_Primitive_Test()
        {
            // Arrange
            var obj = new TestField(27);

            // Act
            // Assert
            Assert.ThrowsException<NullReferenceException>(() =>
            {
                var value = (int)obj.GetFieldValue("f2");
            });
        }

        [TestMethod]
        public void ValueAccessorExtensions_GetFieldValue_FieldNotExists_PrimitiveWithDefault_Test()
        {
            // Arrange
            var obj = new TestField(27);

            // Act
            var value = obj.GetFieldValue("_f2", 11);

            // Assert
            Assert.AreEqual(11, value);
        }


        #endregion

    }
}
