using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.Collections.Tests
{
    [TestClass]
    public class RangeableListTests
    {
        #region Constructors
        [TestMethod]
        public void RangeableList_Constructor_Capacity_Test()
        {
            // Arrange
            // Act
            var list = new RangeableList<int>(10);

            // Assert
            Assert.AreEqual(10, list.Capacity);
        }

        [TestMethod]
        public void RangeableList_Constructor_Test()
        {
            // Arrange
            // Act
            IRangeableList<int> list = new RangeableList<int>();

            // Assert
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void RangeableList_Constructor_Collection_Test()
        {
            // Arrange
            // Act
            IRangeableList<int> list = new RangeableList<int>(new int[] { 1, 2, 3 });

            // Assert
            Assert.AreEqual(3, list.Count);
        }
        #endregion

        #region AddRange
        [TestMethod]
        public void RangeableList_AddRange_Test()
        {
            // Arrange
            IRangeableList<int> list = new RangeableList<int>();

            // Act
            list.AddRange(new[] { 1, 2, 3 });

            // Assert
            Assert.AreEqual(3, list.Count);
        }
        #endregion

        #region GetRange
        [TestMethod]
        public void RangeableList_GetRange_Test()
        {
            // Arrange
            IRangeableList<int> list = new RangeableList<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            // Act
            var actual = list.GetRange(5,2);

            // Assert
            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual(5, actual[0]);
            Assert.AreEqual(6, actual[1]);
        }
        #endregion

        #region InsertRange
        [TestMethod]
        public void RangeableList_InsertRange_Test()
        {
            // Arrange
            IRangeableList<int> list = new RangeableList<int>(new[] { 0, 1, 2, 3, 7, 8, 9 });

            // Act
            list.InsertRange(4, new int[] { 4, 5, 6 });

            // Assert
            Assert.AreEqual(10, list.Count);
            for (int i = 0; i < 10; i++)
                Assert.AreEqual(i, list[i]);
        }
        #endregion

        #region RemoveRange
        [TestMethod]
        public void RangeableList_RemoveRange_Test()
        {
            // Arrange
            IRangeableList<int> list = new RangeableList<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            // Act
            list.RemoveRange(7, 3);

            // Assert
            Assert.AreEqual(7, list.Count);
            for (int i = 0; i < 7; i++)
                Assert.AreEqual(i, list[i]);
        }
        #endregion
    }
}