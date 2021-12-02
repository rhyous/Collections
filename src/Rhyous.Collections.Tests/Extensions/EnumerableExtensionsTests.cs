using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.Collections.Tests.Dictionaries
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        #region Nth to 10

        [TestMethod]
        public void EnumerableExtensions_Nth_Tests()
        {
            // Arrange
            var list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Act & Assert
            int i = 0;
            Assert.AreEqual(list[i++], list.First()); // Part of System.Linq
            Assert.AreEqual(list[i++], list.Second());
            Assert.AreEqual(list[i++], list.Third());
            Assert.AreEqual(list[i++], list.Fourth());
            Assert.AreEqual(list[i++], list.Fifth());
            Assert.AreEqual(list[i++], list.Sixth());
            Assert.AreEqual(list[i++], list.Seventh());
            Assert.AreEqual(list[i++], list.Eighth());
            Assert.AreEqual(list[i++], list.Ninth());
            Assert.AreEqual(list[i++], list.Tenth());
        }

        [TestMethod]
        public void EnumerableExtensions_Nth_Null_Tests()
        {
            // Arranage 
            IEnumerable<int> list = null;
            
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => { list.First(); });
            Assert.ThrowsException<ArgumentNullException>(() => { list.Second(); });
            Assert.ThrowsException<ArgumentNullException>(() => { list.Third(); });
            Assert.ThrowsException<ArgumentNullException>(() => { list.Fourth(); });
            Assert.ThrowsException<ArgumentNullException>(() => { list.Fifth(); });
            Assert.ThrowsException<ArgumentNullException>(() => { list.Sixth(); });
            Assert.ThrowsException<ArgumentNullException>(() => { list.Seventh(); });
            Assert.ThrowsException<ArgumentNullException>(() => { list.Eighth(); });
            Assert.ThrowsException<ArgumentNullException>(() => { list.Ninth(); });
            Assert.ThrowsException<ArgumentNullException>(() => { list.Tenth(); });
        }

        [TestMethod]
        public void EnumerableExtensions_Nth_Empty_Tests()
        {
            // Arranage 
            IEnumerable<int> list = new List<int>();

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => { list.First(); });
            Assert.ThrowsException<InvalidOperationException>(() => { list.Second(); });
            Assert.ThrowsException<InvalidOperationException>(() => { list.Third(); });
            Assert.ThrowsException<InvalidOperationException>(() => { list.Fourth(); });
            Assert.ThrowsException<InvalidOperationException>(() => { list.Fifth(); });
            Assert.ThrowsException<InvalidOperationException>(() => { list.Sixth(); });
            Assert.ThrowsException<InvalidOperationException>(() => { list.Seventh(); });
            Assert.ThrowsException<InvalidOperationException>(() => { list.Eighth(); });
            Assert.ThrowsException<InvalidOperationException>(() => { list.Ninth(); });
            Assert.ThrowsException<InvalidOperationException>(() => { list.Tenth(); });
        }
        #endregion

        #region None
        [TestMethod]
        [ListTNullOrEmpty(typeof(int))]
        public void EnumerableExtensions_None_IEnumerableNull_NoExpression_Test(List<int> list)
        {
            Assert.IsTrue(list.None());
        }

        [TestMethod]
        [ListTNullOrEmpty(typeof(int))]
        public void EnumerableExtensions_None_IEnumerableNull_WithExpression_Test(List<int> list)
        {
            Assert.IsTrue(list.None(i => i < 10));
        }
        
        [TestMethod]
        public void EnumerableExtensions_None_IEnumerableWithExpressionFalseTests()
        {
            // Arrange
            IEnumerable<int> list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Act & Assert
            Assert.IsFalse(list.None(i => i < 10));
        }

        [TestMethod]
        public void EnumerableExtensions_None_IEnumerableWithExpressionTrueTests()
        {
            // Arrange
            IEnumerable<int> list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Act & Assert
            Assert.IsTrue(list.None(i => i > 10));
        }
        #endregion

        #region None
        [TestMethod]
        [ListTNullOrEmpty(typeof(int))]
        public void EnumerableExtensions_NUllOrEmpty_IEnumerableNull_Test(List<int> list)
        {
            Assert.IsTrue(list.NullOrEmpty());
        }
        #endregion

        #region UnorderedEquals tests
        /// <summary>
        /// Scenario 1 - Both null
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_UnorderedEqualsBothNullTrueTest()
        {
            // Arrange
            IEnumerable<string> items1 = null;
            IEnumerable<string> items2 = null;

            // Act
            // Assert
            Assert.IsTrue(items1.UnorderedEquals(items2));
        }

        /// <summary>
        /// Scenario 2a - Left list is null, right  is instantiated.
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_UnorderedEqualsLeftNullFalseTest()
        {
            // Arrange
            IEnumerable<string> items1 = null;
            IEnumerable<string> items2 = new List<string> { "B", "C", "A" };

            // Act
            // Assert
            Assert.IsFalse(items1.UnorderedEquals(items2));
        }

        /// <summary>
        /// Scenario 2b - Right list is null, left is instantiated.
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_UnorderedEqualsRightNullFalseTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C" };
            IEnumerable<string> items2 = null;

            // Act
            // Assert
            Assert.IsFalse(items1.UnorderedEquals(items2));
        }

        /// <summary>
        /// Scenario 3 - Both lists are instantiated but empty
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_UnorderedEqualsBothEmptyTrueTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string>();
            IEnumerable<string> items2 = new List<string>();

            // Act
            // Assert
            Assert.IsTrue(items1.UnorderedEquals(items2));
        }

        /// <summary>
        /// Scenario 4a - Left list is empty, right list is populated.
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_UnorderedEqualsLeftIsEmptyFalseTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string>();
            IEnumerable<string> items2 = new List<string> { "B", "C", "A" };

            // Act
            // Assert
            Assert.IsFalse(items1.UnorderedEquals(items2));
        }
        
        /// <summary>
        /// Scenario 4b - Right list is empty, left list is populated.
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_UnorderedEqualsRightEmptyFalseTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C" };
            IEnumerable<string> items2 = new List<string>();

            // Act
            // Assert
            Assert.IsFalse(items1.UnorderedEquals(items2));
        }

        /// <summary>
        /// Scenaro 5 - Both are instantiated but have different number of items.
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_UnorderedEqualsSameDistinctItemsButNotSameFalseTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C" };
            IEnumerable<string> items2 = new List<string> { "B", "C", "A", "B" };

            // Act
            // Assert
            Assert.IsFalse(items1.UnorderedEquals(items2));
        }

        /// <summary>
        /// Scenario 6a - Lists same size and items are equal
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_UnorderedEqualsTrueTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C" };
            IEnumerable<string> items2 = new List<string> { "B", "C", "A" };

            // Act
            // Assert
            Assert.IsTrue(items1.UnorderedEquals(items2));
        }

        /// <summary>
        /// Scenario 6b - Lists same size but items are not equal
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_UnorderedEqualsFalseTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C" };
            IEnumerable<string> items2 = new List<string> { "B", "C", "D" };

            // Act
            // Assert
            Assert.IsFalse(items1.UnorderedEquals(items2));
        }

        /// <summary>
        /// Scenario 6c - Lists same size and items are equal and some items are null
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_UnorderedEqualsNullItemsTrueTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C", null, "E" };
            IEnumerable<string> items2 = new List<string> { "B", "C", "A", null, "E" };

            // Act
            // Assert
            Assert.IsTrue(items1.UnorderedEquals(items2));
        }
        
        /// <summary>
        /// Scenario 6d - Lists same size and items are equal and some items are duplicated
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_UnorderedEqualsDuplicateItemsTrueTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C", "B" };
            IEnumerable<string> items2 = new List<string> { "B", "C", "A", "B" };

            // Act
            // Assert
            Assert.IsTrue(items1.UnorderedEquals(items2));
        }

        /// <summary>
        /// Scenario 6e - Lists same size and items are not equal, one side has more nulls and less items
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_UnorderedEqualsDifferentNumberOfNullItemsFalseTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C", null, "E", "B" };
            IEnumerable<string> items2 = new List<string> { "B", "C", null, "A", null, "E" };

            // Act
            // Assert
            Assert.IsFalse(items1.UnorderedEquals(items2));
        }

        /// <summary>
        /// Scenario 7 - Lists same size and items are not strictly equal, but IEqualityComparer<T> makes them equal
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_UnorderedEqualsIgnoreCaseTrueTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "a", "b", "C", "B" };
            IEnumerable<string> items2 = new List<string> { "B", "c", "A", "b" };

            // Act
            // Assert
            Assert.IsTrue(items1.UnorderedEquals(items2, StringComparer.OrdinalIgnoreCase));
        }
        #endregion

        #region GetMisMatchedItems tests
        /// <summary>
        /// Scenario 1 - Both null
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_GetMisMatchedItemsBothNullTest()
        {
            // Arrange
            IEnumerable<string> items1 = null;
            IEnumerable<string> items2 = null;

            // Act
            var actual = items1.GetMismatchedItems(items2);

            // Assert
            Assert.AreEqual(0, actual.Left.Count);
            Assert.AreEqual(0, actual.Right.Count);
        }

        /// <summary>
        /// Scenario 2a - Left list is null, right  is instantiated.
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_GetMisMatchedItemsLeftNullTest()
        {
            // Arrange
            IEnumerable<string> items1 = null;
            IEnumerable<string> items2 = new List<string> { "B", "C", "A" };

            // Act
            var actual = items1.GetMismatchedItems(items2);

            // Assert
            Assert.AreEqual(0, actual.Left.Count);
            Assert.AreEqual(3, actual.Right.Count);
            Assert.IsTrue(items2.SequenceEqual(actual.Right));
        }

        /// <summary>
        /// Scenario 2b - Right list is null, left is instantiated.
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_GetMisMatchedItemsRightNullTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C" };
            IEnumerable<string> items2 = null;

            // Act
            var actual = items1.GetMismatchedItems(items2);

            // Assert
            Assert.AreEqual(3, actual.Left.Count);
            Assert.AreEqual(0, actual.Right.Count);
            Assert.IsTrue(items1.SequenceEqual(actual.Left));
        }

        /// <summary>
        /// Scenario 3 - Both lists are instantiated but empty
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_GetMisMatchedItemsBothEmptyTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string>();
            IEnumerable<string> items2 = new List<string>();

            // Act
            var actual = items1.GetMismatchedItems(items2);

            // Assert
            Assert.AreEqual(0, actual.Left.Count);
            Assert.AreEqual(0, actual.Right.Count);
        }

        /// <summary>
        /// Scenario 4a - Left list is empty, right list is populated.
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_GetMisMatchedItemsLeftIsEmptyTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string>();
            IEnumerable<string> items2 = new List<string> { "B", "C", "A" };

            // Act
            var actual = items1.GetMismatchedItems(items2);

            // Assert
            Assert.AreEqual(0, actual.Left.Count);
            Assert.AreEqual(3, actual.Right.Count);
            Assert.IsTrue(items2.SequenceEqual(actual.Right));
        }

        /// <summary>
        /// Scenario 4b - Right list is empty, left list is populated.
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_GetMisMatchedItemsRightIsEmptyTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C" };
            IEnumerable<string> items2 = new List<string>();

            // Act
            var actual = items1.GetMismatchedItems(items2);

            // Assert
            Assert.AreEqual(3, actual.Left.Count);
            Assert.AreEqual(0, actual.Right.Count);
            Assert.IsTrue(items1.SequenceEqual(actual.Left));
        }

        /// <summary>
        /// Scenario 5a - Lists same size and items are equal
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_GetMisMatchedItemsAllMatchTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C" };
            IEnumerable<string> items2 = new List<string> { "B", "C", "A" };

            // Act
            var actual = items1.GetMismatchedItems(items2);

            // Assert
            Assert.AreEqual(0, actual.Left.Count);
            Assert.AreEqual(0, actual.Right.Count);
        }

        /// <summary>
        /// Scenario 5b - Lists same size but items are not equal
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_GetMisMatchedItems1EachTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C" };
            IEnumerable<string> items2 = new List<string> { "B", "C", "D" };

            // Act
            var actual = items1.GetMismatchedItems(items2);

            // Assert
            Assert.AreEqual(1, actual.Left.Count);
            Assert.AreEqual("A", actual.Left[0]);
            Assert.AreEqual(1, actual.Right.Count);
            Assert.AreEqual("D", actual.Right[0]);
        }

        /// <summary>
        /// Scenario 5c - Lists same size and items are equal and some items are null
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_GetMisMatchedItemsNullItemsTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C", null, "E" };
            IEnumerable<string> items2 = new List<string> { "B", "C", "A", null, "E" };

            // Act
            var actual = items1.GetMismatchedItems(items2);

            // Assert
            Assert.AreEqual(0, actual.Left.Count);
            Assert.AreEqual(0, actual.Right.Count);
        }

        /// <summary>
        /// Scenario 5d - Lists same size and items are equal and some items are duplicated
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_GetMisMatchedItemsAllMatchDuplicateItemsTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C", "B" };
            IEnumerable<string> items2 = new List<string> { "B", "C", "A", "B" };

            // Act
            var actual = items1.GetMismatchedItems(items2);

            // Assert
            Assert.AreEqual(0, actual.Left.Count);
            Assert.AreEqual(0, actual.Right.Count);
        }

        /// <summary>
        /// Scenario 5d - Lists same size and items are equal and some items are duplicated
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_GetMisMatchedItemsAllMatchDuplicateItems_Right_Test()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C", "B" };
            IEnumerable<string> items2 = new List<string> { "B", "C", "A", "B", "B" };

            // Act
            var actual = items1.GetMismatchedItems(items2);

            // Assert
            Assert.AreEqual(0, actual.Left.Count);
            Assert.AreEqual(1, actual.Right.Count);
        }

        /// <summary>
        /// Scenario 5d - Lists same size and items are equal and some items are duplicated
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_GetMisMatchedItemsAllMatchDuplicateItems_Left_Test()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C", "B" };
            IEnumerable<string> items2 = new List<string> { "B", "C", "A", "B", "B" };

            // Act
            var actual = items2.GetMismatchedItems(items1);

            // Assert
            Assert.AreEqual(1, actual.Left.Count);
            Assert.AreEqual(0, actual.Right.Count);
        }

        /// <summary>
        /// Scenario 5e - Lists same size and items are not equal, one side has more nulls and less items
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_GetMisMatchedItemsDifferentNumberOfNullItemsTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C", null, "E", "B" };
            IEnumerable<string> items2 = new List<string> { "B", "C", null, "A", null, "E" };

            // Act
            var actual = items1.GetMismatchedItems(items2);

            // Assert
            Assert.AreEqual(1, actual.Left.Count);
            Assert.AreEqual("B", actual.Left[0]);
            Assert.AreEqual(1, actual.Right.Count);
            Assert.AreEqual(null, actual.Right[0]);
        }

        /// <summary>
        /// Scenario 6 - Lists same size and items are not strictly equal, but IEqualityComparer<T> makes them equal
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_GetMisMatchedItemsAllMatchIgnoreCaseTest()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "a", "b", "C", "B" };
            IEnumerable<string> items2 = new List<string> { "B", "c", "A", "b" };

            // Act
            var actual = items1.GetMismatchedItems(items2, StringComparer.OrdinalIgnoreCase);

            // Assert
            Assert.AreEqual(0, actual.Left.Count);
            Assert.AreEqual(0, actual.Right.Count);
        }

        /// <summary>
        /// Scenario 5e - Lists same size and items are not equal, one side has more nulls and less items
        /// </summary>
        [TestMethod]
        public void EnumerableExtensions_GetMisMatchedItemsDifferentNumberOfNullItems_MoreNullsOnLeft_Test()
        {
            // Arrange
            IEnumerable<string> items1 = new List<string> { "A", "B", "C", null, "E", "B" };
            IEnumerable<string> items2 = new List<string> { "B", "C", null, "A", null, "E" };

            // Act
            var actual = items2.GetMismatchedItems(items1);

            // Assert
            Assert.AreEqual(1, actual.Right.Count);
            Assert.AreEqual("B", actual.Right[0]);
            Assert.AreEqual(1, actual.Left.Count);
            Assert.AreEqual(null, actual.Left[0]);
        }
        #endregion
    }
}