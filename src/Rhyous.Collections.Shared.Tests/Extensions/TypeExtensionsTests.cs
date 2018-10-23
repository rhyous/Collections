using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Rhyous.Collections.Tests
{

    [TestClass]
    public class TypeExtensionsTests
    {
        #region IsCollection
        [TestMethod]
        public void TypeExtensions_IsCollection_Test()
        {
            Assert.IsTrue(typeof(ICollection).IsCollection());
        }

        [TestMethod]
        public void TypeExtensions_IsCollectionGeneric_Test()
        {
            Assert.IsTrue(typeof(ICollection<>).IsCollection());
        }

        [TestMethod]
        public void TypeExtensions_IsCollectionGenericString_Test()
        {
            Assert.IsTrue(typeof(ICollection<string>).IsCollection());
        }

        [TestMethod]
        public void TypeExtensions_IsCollectionConcrete_Test()
        {
            Assert.IsTrue(typeof(Collection<string>).IsCollection());
        }

        [TestMethod]
        public void TypeExtensions_IsCollectionConcreteGeneric_Test()
        {
            Assert.IsTrue(typeof(Collection<>).IsCollection());
        }


        [TestMethod]
        public void TypeExtensions_IsCollection_ListString_Test()
        {
            Assert.IsTrue(typeof(List<string>).IsCollection());
        }

        [TestMethod]
        public void TypeExtensions_IsCollection_ListGeneric_Test()
        {
            Assert.IsTrue(typeof(List<>).IsCollection());
        }
        #endregion

        #region IsEnumerable
        [TestMethod]
        public void TypeExtensions_IsEnumerable_Test()
        {
            Assert.IsTrue(typeof(IEnumerable).IsEnumerable());
        }

        [TestMethod]
        public void TypeExtensions_IsEnumerableGeneric_Test()
        {
            Assert.IsTrue(typeof(IEnumerable<>).IsEnumerable());
        }

        [TestMethod]
        public void TypeExtensions_IsEnumerableGenericString_Test()
        {
            Assert.IsTrue(typeof(IEnumerable<string>).IsEnumerable());
        }
        #endregion

        #region IsList
        [TestMethod]
        public void TypeExtensions_IsList_Test()
        {
            Assert.IsTrue(typeof(IList).IsList());
        }

        [TestMethod]
        public void TypeExtensions_IsListGeneric_Test()
        {
            Assert.IsTrue(typeof(IList<>).IsList());
        }

        [TestMethod]
        public void TypeExtensions_IsListGenericString_Test()
        {
            Assert.IsTrue(typeof(IList<string>).IsList());
        }

        [TestMethod]
        public void TypeExtensions_IsListConcrete_Test()
        {
            Assert.IsTrue(typeof(List<string>).IsList());
        }

        [TestMethod]
        public void TypeExtensions_IsListConcreteGeneric_Test()
        {
            Assert.IsTrue(typeof(List<>).IsList());
        }

        [TestMethod]
        public void TypeExtensions_IsList_ParentedList_Test()
        {
            Assert.IsTrue(typeof(ParentedList<string>).IsList());
        }
        #endregion

        #region IsDictionary
        [TestMethod]
        public void TypeExtensions_IsDictionary_Test()
        {
            Assert.IsTrue(typeof(IDictionary).IsDictionary());
        }

        [TestMethod]
        public void TypeExtensions_IsDictionaryGeneric_Test()
        {
            Assert.IsTrue(typeof(IDictionary<,>).IsDictionary());
        }

        [TestMethod]
        public void TypeExtensions_IsDictionaryGenericString_Test()
        {
            Assert.IsTrue(typeof(IDictionary<int,string>).IsDictionary());
        }

        [TestMethod]
        public void TypeExtensions_IsDictionaryConcrete_Test()
        {
            Assert.IsTrue(typeof(Dictionary<int,string>).IsDictionary());
        }

        [TestMethod]
        public void TypeExtensions_IsDictionaryConcreteGeneric_Test()
        {
            Assert.IsTrue(typeof(Dictionary<,>).IsDictionary());
        }

        [TestMethod]
        public void TypeExtensions_IsDictionary_NullSafeDictionary_Test()
        {
            Assert.IsTrue(typeof(NullSafeDictionary<,>).IsDictionary());
        }
        #endregion

        #region ToDictionary

        public enum TestEnum { A = 1, B, C };
        public enum TestEnumShort : short { D = 4, E, F };

        [TestMethod]
        public void TypeExtensions_ToDictionary_Test()
        {
            // Arrange
            var dict = typeof(TestEnum).ToDictionary<int>();

            // Act & Assert
            Assert.AreEqual(3, dict.Count);
            Assert.AreEqual(1, dict["A"]);
            Assert.AreEqual(2, dict["B"]);
            Assert.AreEqual(3, dict["C"]);
        }

        [TestMethod]
        public void TypeExtensions_ToDictionary_OtherType_Test()
        {
            // Arrange
            var dict = typeof(TestEnumShort).ToDictionary<short>();

            // Act & Assert
            Assert.AreEqual(3, dict.Count);
            Assert.AreEqual(4, dict["D"]);
            Assert.AreEqual(5, dict["E"]);
            Assert.AreEqual(6, dict["F"]);
        }

        [TestMethod]
        public void TypeExtensions_Dictionary_IgnoredCaseByDefault_Test()
        {
            // Arrange
            var dict = typeof(TestEnumShort).ToDictionary<short>();

            // Act & Assert
            Assert.AreEqual(3, dict.Count);
            Assert.AreEqual(4, dict["d"]);
            Assert.AreEqual(5, dict["e"]);
            Assert.AreEqual(6, dict["f"]);
        }

        [TestMethod]
        public void TypeExtensions_Dictionary_UsesComparer_Test()
        {
            // Arrange
            var dict = typeof(TestEnumShort).ToDictionary<short>(EqualityComparer<string>.Default);

            // Act & Assert
            Assert.IsFalse(dict.TryGetValue("d", out short _));
            Assert.IsFalse(dict.TryGetValue("e", out short _));
            Assert.IsFalse(dict.TryGetValue("f", out short _));
        }
        #endregion
    }
}