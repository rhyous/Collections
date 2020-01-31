using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;

namespace Rhyous.Collections.Shared.Tests.Models
{
    [TestClass]
    public class MismatchedItemsTests
    {
        [TestMethod]
        public void MismatchedItems_Right_LazyProp_NeverNull_Test()
        {
            // Arrange
            // Act
            var mi = new MismatchedItems<int>();

            // Assert
            Assert.IsNotNull(mi.Right);
        }

        [TestMethod]
        public void MismatchedItems_Right_Set_Test()
        {
            // Arrange
            var mi = new MismatchedItems<int>();
            var original = mi.Right;
            var newList = new List<int>();

            // Act
            mi.Right = newList;

            // Assert
            Assert.AreEqual(newList, mi.Right);
            Assert.AreNotEqual(original, mi.Right);
        }

        [TestMethod]
        public void MismatchedItems_Left_LazyProp_NeverNull_Test()
        {
            // Arrange
            // Act
            var mi = new MismatchedItems<int>();

            // Assert
            Assert.IsNotNull(mi.Left);
        }

        [TestMethod]
        public void MismatchedItems_Left_Set_Test()
        {
            // Arrange
            var mi = new MismatchedItems<int>();
            var original = mi.Left;
            var newList = new List<int>();

            // Act
            mi.Left = newList;

            // Assert
            Assert.AreEqual(newList, mi.Left);
            Assert.AreNotEqual(original, mi.Left);
        }

        [TestMethod]
        public void MismatchedItems_Count_Test()
        {
            // Arrange
            var mi = new MismatchedItems<int>();
            mi.Left.Add(1);
            mi.Right.Add(2);

            // Act
            // Assert
            Assert.AreEqual(2, mi.Count);
        }

        [TestMethod]
        public void MismatchedItems_IEnumerable_Test()
        {
            // Arrange
            var mi = new MismatchedItems<int>();
            mi.Left.Add(1);
            mi.Right.Add(2);
            var e = mi as IEnumerable;

            // Act
            // Assert
            int i = 1;
            foreach (var item in e)
            {
                Assert.AreEqual(i++, item);
            }
        }

        [TestMethod]
        public void MismatchedItems_IEnumerableT_Test()
        {
            // Arrange
            var mi = new MismatchedItems<int>();
            mi.Left.Add(1);
            mi.Right.Add(2);
            var e = mi as IEnumerable<int>;

            // Act
            // Assert
            int i = 1;
            foreach (var item in e)
            {
                Assert.AreEqual(i++, item);
            }
        }
    }
}
