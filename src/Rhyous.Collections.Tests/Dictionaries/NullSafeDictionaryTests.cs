using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.Collections.Tests
{
    [TestClass]
    public class NullSafeDictionaryTests
    {
        #region Constructors
        [TestMethod]
        public void NullSafeDictionary_Constructor_DefaultValueProvider_Test()
        {
            // Arrange
            Func<string, int> provider = (s) => { return -1; };
            var dict = new NullSafeDictionary<string, int>(provider);

            // Act
            var value = dict["a"];

            // Assert
            Assert.AreEqual(provider, dict.DefaultValueProviderMethod);
            Assert.AreEqual(-1, value);
        }
        #endregion

        #region Indexer test
        [TestMethod]
        public void NullSafeDictionary_DefaultValueTest()
        {
            // Arrange
            var dict = new NullSafeDictionary<string, int>();

            // Act
            var value = dict["a"];

            // Assert
            Assert.AreEqual(0, value);
        }

        [TestMethod]
        public void NullSafeDictionary_DefaultValueFromCustomMethodTest()
        {
            // Arrange
            var dict = new NullSafeDictionary<string, int>((key) => { return -1; });

            // Act
            var value = dict["a"];

            // Assert
            Assert.AreEqual(-1, value);
        }

        [TestMethod]
        public void NullSafeDictionary_ItemExists_Test()
        {
            // Arrange
            var dict = new NullSafeDictionary<string, int>((key) => { return -1; })
            {
                { "a", 27 }
            };

            // Act
            var value = dict["a"];

            // Assert
            Assert.AreEqual(27, value);
        }

        [TestMethod]
        public void NullSafeDictionary_Indexer_Set_Test()
        {
            // Arrange
            var dict = new NullSafeDictionary<string, int>((key) => { return -1; })
            {
                { "a", 27 }
            };

            // Act
            dict["a"] = 100;

            // Assert
            Assert.AreEqual(100, dict["a"]);
        }
        #endregion

        #region Enumerable
        [TestMethod]
        public void NullSafeDictionary_IEnumerable_Test()
        {
            // Arrange
            var list = new NullSafeDictionary<int, string>() { { 1, "a" }, { 2, "b" } };
            var e = list as IEnumerable;

            // Act
            // Assert
            int i = 1;
            foreach (KeyValuePair<int,string> kvp in e)
            {
                Assert.AreEqual(i++, kvp.Key);
            }
        }

        [TestMethod]
        public void NullSafeDictionary_IEnumerableT_Test()
        {
            // Arrange
            var list = new NullSafeDictionary<int, string>() { { 1, "a" }, { 2, "b" } };
            var e = list as IEnumerable<KeyValuePair<int, string>>;

            // Act
            // Assert
            int i = 1;
            foreach (KeyValuePair<int, string> kvp in e)
            {
                Assert.AreEqual(i++, kvp.Key);
            }
        }
        #endregion

        #region AddKeys
        [TestMethod]
        public void NullSafeDictionary_DefaultValueAddKeysTest()
        {
            // Arrange
            var dict = new NullSafeDictionary<string, int>((key) => { return -1; });
            var array = new[] { "A", "B", "C" };

            // Act
            dict.AddKeys(array);

            // Assert
            for (int i = 0; i < dict.Keys.Count; i++)
            {
                Assert.AreEqual(-1, dict.Values.Skip(i).First());
            }
        }
        #endregion

        #region DefaultValudeProviderMethod
        [TestMethod]
        public void NullSafeDictionary_DefaultValudeProviderMethod_Set_Test()
        {
            // Arrange
            Func<string, int> method = (key) => { return -1; };
            var dict = new NullSafeDictionary<string, int>();

            // Act
            dict.DefaultValueProviderMethod = method;

            // Assert
            Assert.AreEqual(method, dict.DefaultValueProviderMethod);
        }
        #endregion

        #region Contains
        [TestMethod]
        public void NullSafeDictionary_Contains_Test()
        {
            // Arrange
            Func<string, int> method = (key) => { return -1; };
            var dict = new NullSafeDictionary<int, string>() { { 1, "a" }, { 2, "b" } };

            // Act
            var actual = dict.Contains(new KeyValuePair<int, string>(1, "a"));

            // Assert
            Assert.IsTrue(actual);
        }
        #endregion

        #region ContainsKey
        [TestMethod]
        public void NullSafeDictionary_ContainsKey_Test()
        {
            // Arrange
            Func<string, int> method = (key) => { return -1; };
            var dict = new NullSafeDictionary<int, string>() { { 1, "a" }, { 2, "b" } };

            // Act
            var actual = dict.ContainsKey(2);

            // Assert
            Assert.IsTrue(actual);
        }
        #endregion

        #region Remove
        [TestMethod]
        public void NullSafeDictionary_Remove_ByKeyValuePair_Test()
        {
            // Arrange
            Func<string, int> method = (key) => { return -1; };
            var dict = new NullSafeDictionary<int, string>() { { 1, "a" }, { 2, "b" } };

            // Act
            var actual = dict.Remove(new KeyValuePair<int, string>(1, "a"));

            // Assert
            Assert.AreEqual(1, dict.Count);
        }

        [TestMethod]
        public void NullSafeDictionary_Remove_ByKey_Test()
        {
            // Arrange
            Func<string, int> method = (key) => { return -1; };
            var dict = new NullSafeDictionary<int, string>() { { 1, "a" }, { 2, "b" } };

            // Act
            var actual = dict.Remove(2);

            // Assert
            Assert.AreEqual(1, dict.Count);
        }
        #endregion

        #region Clear
        [TestMethod]
        public void NullSafeDictionary_Clear_Test()
        {
            // Arrange
            var dict = new NullSafeDictionary<int, string>() { { 1, "a" }, { 2, "b" } };

            // Act
            dict.Clear();

            // Assert
            Assert.AreEqual(0, dict.Count);
        }
        #endregion

        #region Add
        [TestMethod]
        public void NullSafeDictionary_Add_Test()
        {
            // Arrange
            var dict = new NullSafeDictionary<int, string>() { { 1, "a" }, { 2, "b" } };

            // Act
            dict.Add(3, "c");

            // Assert
            Assert.AreEqual(3, dict.Count);
        }

        [TestMethod]
        public void NullSafeDictionary_Add_KeyValuePair_Test()
        {
            // Arrange
            var dict = new NullSafeDictionary<int, string>() { { 1, "a" }, { 2, "b" } };
            var kvp = new KeyValuePair<int, string>(3, "c");

            // Act
            dict.Add(kvp);

            // Assert
            Assert.AreEqual(3, dict.Count);
        }
        #endregion

        #region CopyTo
        [TestMethod]
        public void NullSafeDictionary_CopyTo_Test()
        {
            // Arrange
            var dict = new NullSafeDictionary<int, string>() { { 1, "a" }, { 2, "b" } };
            var array = new KeyValuePair<int, string>[2];

            // Act
            dict.CopyTo(array, 0);

            // Assert
            Assert.AreEqual(2, array.Length);
        }
        #endregion

        #region IsReadOnly
        [TestMethod]
        public void NullSafeDictionary_IsReadOnly_Test()
        {
            // Arrange
            var dict = new NullSafeDictionary<int, string>() { { 1, "a" }, { 2, "b" } };

            // Act
            // Assert
            Assert.IsFalse(dict.IsReadOnly);
        }
        #endregion

        #region GetDefaultValue
        [TestMethod]
        public void NullSafeDictionary_DefaultValue_Test()
        {
            // Arrange
            var dict = new NullSafeDictionary<int, long>();

            // Act
            // Assert
            Assert.AreEqual(0, dict.DefaultValue);
        }

        [TestMethod]
        public void NullSafeDictionary_DefaultValue_String_Test()
        {
            // Arrange
            var dict = new NullSafeDictionary<string, string>();

            // Act
            // Assert
            Assert.IsNull(dict.DefaultValue);
        }
        #endregion
    }
}