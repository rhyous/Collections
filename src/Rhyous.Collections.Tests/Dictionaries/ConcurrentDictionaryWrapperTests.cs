using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.Collections.Tests.Dictionaries
{
    [TestClass]
    public class ConcurrentDictionaryWrapperTests
    {
        private IConcurrentDictionary<int, string> CreateConcurrentDictionary()
        {
            return new ConcurrentDictionaryWrapper<int, string>();
        }


        /// <summary>bool IsEmpty { get; }</summary>
        [TestMethod]
        public void ConcurrentDictionary_IsEmpty_Test()
        {
            // Arrange
            var cdict = CreateConcurrentDictionary();

            // Act & assert
            Assert.IsTrue(cdict.IsEmpty);
        }

        /// <summary>string AddOrUpdate(int key, Func<int, string> addValueFactory, Func<int, string, string> updateValueFactory);</summary>
        [TestMethod]
        public void ConcurrentDictionary_AddOrUpdate3_Empty_Test()
        {
            // Arrange
            var cdict = CreateConcurrentDictionary();
            var value = "A";

            // Act
            cdict.AddOrUpdate(1, (int key) => value, (int key, string input) => input);

            // Assert
            Assert.AreEqual("A", cdict[1]);
        }

        [TestMethod]
        public void ConcurrentDictionary_AddOrUpdate3_AllreadyAdded_KeepAdded_Test()
        {
            // Arrange
            var cdict = CreateConcurrentDictionary();
            var value = "A";
            cdict.AddOrUpdate(1, value, (int key, string input) => input);
            var newValue = "B";

            // Act
            cdict.AddOrUpdate(1, (int key) => newValue, (int key, string input) => input);

            // Assert
            Assert.AreEqual(value, cdict[1]);
        }

        [TestMethod]
        public void ConcurrentDictionary_AddOrUpdate3_AllreadyAdded_KeepUpated_Test()
        {
            // Arrange
            var cdict = CreateConcurrentDictionary();
            var value = "A";
            cdict.AddOrUpdate(1, value, (int key, string input) => input);
            var newValue = "B";

            // Act
            cdict.AddOrUpdate(1, (int key) => newValue, (int key, string input) => newValue);

            // Assert
            Assert.AreEqual(newValue, cdict[1]);
        }
        
        /// <summary>string AddOrUpdate(int key, string addValue, Func<int, string, string> updateValueFactory);</summary>
        [TestMethod]
        public void ConcurrentDictionary_AddOrUpdate_Empty_Test()
        {
            // Arrange
            var cdict = CreateConcurrentDictionary();

            // Act
            cdict.AddOrUpdate(1, "A", (int key, string input) => input);

            // Assert
            Assert.AreEqual("A", cdict[1]);
        }

        [TestMethod]
        public void ConcurrentDictionary_AddOrUpdate_AllreadyAdded_KeepAdded_Test()
        {
            // Arrange
            var cdict = CreateConcurrentDictionary();
            cdict.AddOrUpdate(1, "A", (int key, string input) => input);

            // Act
            cdict.AddOrUpdate(1, "B", (int key, string input) => input);

            // Assert
            Assert.AreEqual("A", cdict[1]);
        }

        [TestMethod]
        public void ConcurrentDictionary_AddOrUpdate_AllreadyAdded_KeepUpated_Test()
        {
            // Arrange
            var cdict = CreateConcurrentDictionary();
            cdict.AddOrUpdate(1, "A", (int key, string input) => input);

            // Act
            var newValue = "B";
            cdict.AddOrUpdate(1, newValue, (int key, string input) => newValue);

            // Assert
            Assert.AreEqual("B", cdict[1]);
        }
        /// <summary>string GetOrAdd(int key, Func<int, string> valueFactory);</summary>
        /// <summary>string GetOrAdd(int key, string value);</summary>
        /// <summary>KeyValuePair<int, string>[] ToArray();</summary>
        /// <summary>bool TryAdd(int key, string value);</summary>
        /// <summary>bool TryRemove(int key, out string value);</summary>
        /// <summary>bool TryUpdate(int key, string newValue, string comparisonValue);</summary>
    }
}
