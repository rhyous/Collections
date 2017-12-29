using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.Collections.Tests.Collections
{
    [TestClass]
    public class ParentedListTests
    {
        [TestMethod]
        public void AddSetsParent()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var parentedList = new ParentedList<TestClass, TestClass>(parent);

            // Act
            parentedList.Add(item1);

            // Assert
            Assert.AreEqual(parent, item1.Parent);
        }

        [TestMethod]
        public void AddRangeSetsParent()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var parentedList = new ParentedList<TestClass, TestClass>(parent);

            // Act
            parentedList.AddRange(new[] { item1, item2 } );

            // Assert
            Assert.AreEqual(parent, item1.Parent);
            Assert.AreEqual(parent, item2.Parent);
        }

        [TestMethod]
        public void InsertSetsParent()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 2, Name = "Item 1" };
            var parentedList = new ParentedList<TestClass, TestClass>(parent);

            // Act
            parentedList.Insert(0, item1);

            // Assert
            Assert.AreEqual(parent, item1.Parent);
        }


        [TestMethod]
        public void IndexerSetsParent()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var parentedList = new ParentedList<TestClass, TestClass>(parent);
            parentedList.Add(item1);

            // Act
            parentedList[0] = item2;

            // Assert
            Assert.AreEqual(parent, item2.Parent);
        }

        [TestMethod]
        public void IndexerRemovesParent()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var parentedList = new ParentedList<TestClass, TestClass>(parent);
            parentedList.Add(item1);

            // Act
            parentedList[0] = item2;

            // Assert
            Assert.IsNull(item1.Parent);
        }

        [TestMethod]
        public void IndexerDoesNotRemovesParentIfAddedToListUnderADifferentIndex()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var parentedList = new ParentedList<TestClass, TestClass>(parent);
            parentedList.AddRange(new[] { item1, item1 });

            // Act
            parentedList[1] = item2;

            // Assert
            Assert.AreEqual(parent, item1.Parent);
        }

        [TestMethod]
        public void RemoveAtRemovesParent()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var parentedList = new ParentedList<TestClass, TestClass>(parent);
            parentedList.AddRange(new[] { item1, item2 });

            // Act
            parentedList.RemoveAt(1);

            // Assert
            Assert.IsNull(item2.Parent);
        }

        [TestMethod]
        public void RemoveAtDoesNotRemovesParentIfAddedToListUnderADifferentIndex()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var parentedList = new ParentedList<TestClass, TestClass>(parent);
            parentedList.AddRange(new[] { item1, item1 });

            // Act
            parentedList.RemoveAt(1);

            // Assert
            Assert.AreEqual(parent, item1.Parent);
        }


        [TestMethod]
        public void RemoveRemovesParent()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var parentedList = new ParentedList<TestClass, TestClass>(parent);
            parentedList.AddRange(new[] { item1, item2 });

            // Act
            parentedList.Remove(item2);

            // Assert
            Assert.IsNull(item2.Parent);
        }

        [TestMethod]
        public void RemoveDoesNotRemovesParentIfAddedToListMultipleTimes()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var parentedList = new ParentedList<TestClass, TestClass>(parent);
            parentedList.AddRange(new[] { item1, item1 });

            // Act
            parentedList.Remove(item1);

            // Assert
            Assert.AreEqual(parent, item1.Parent);
        }
    }
}
