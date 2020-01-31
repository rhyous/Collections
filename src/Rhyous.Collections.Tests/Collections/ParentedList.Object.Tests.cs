using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Rhyous.Collections.Tests.Collections
{
    [TestClass]
    public class ParentedListObjectTests
    {

        [TestMethod]
        public void Constructor_IEnumerable()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 1" };

            // Act
            var list = new ParentedList<TestClass>(parent, new[] { item1, item2 });

            // Assert
            Assert.AreEqual(parent, item1.Parent);
            Assert.AreEqual(parent, item2.Parent);
        }

        [TestMethod]
        public void InvalidConstructor()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };

            // Act
            // Assert
            Assert.ThrowsException<ArgumentException>(() => new ParentedList<TestClass>(parent, "Bogus"));
        }

        [TestMethod]
        public void AddSetsParent()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var parentedList = new ParentedList<TestClass>(parent);

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
            var parentedList = new ParentedList<TestClass>(parent);

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
            var parentedList = new ParentedList<TestClass>(parent);

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
            var parentedList = new ParentedList<TestClass>(parent);
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
            var parentedList = new ParentedList<TestClass>(parent);
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
            var parentedList = new ParentedList<TestClass>(parent);
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
            var parentedList = new ParentedList<TestClass>(parent);
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
            var parentedList = new ParentedList<TestClass>(parent);
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
            var parentedList = new ParentedList<TestClass>(parent);
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
            var parentedList = new ParentedList<TestClass>(parent);
            parentedList.AddRange(new[] { item1, item1 });

            // Act
            parentedList.Remove(item1);

            // Assert
            Assert.AreEqual(parent, item1.Parent);
        }

        [TestMethod]
        public void ClearRemovesParent()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var parentedList = new ParentedList<TestClass>(parent);
            parentedList.AddRange(new[] { item1, item2 });

            // Act
            parentedList.Clear();

            // Assert
            Assert.IsNull(item1.Parent);
            Assert.IsNull(item2.Parent);
        }

        [TestMethod]
        public void InsertRangeAddsParent()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var parentedList = new ParentedList<TestClass>(parent);

            // Act
            parentedList.InsertRange(0, new[] { item1, item2 });

            // Assert
            Assert.AreEqual(parent, item1.Parent);
            Assert.AreEqual(parent, item2.Parent);
        }

        [TestMethod]
        public void RemoveRangeRemovesParent()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var parentedList = new ParentedList<TestClass>(parent);
            parentedList.AddRange(new[] { item1, item2 });

            // Act
            parentedList.RemoveRange(0, 2);

            // Assert
            Assert.IsNull(item1.Parent);
            Assert.IsNull(item2.Parent);
        }

        [TestMethod]
        public void AddRangeOnIEnumerableIterationParentIsSet()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var parentedList = new ParentedList<TestClass>(parent);
            var list = new[] { item1, item2 };
            var enumerableThatCreatesANewInstanceEachIteration = list.Select(i => new TestClass { Id = i.Id, Name = i.Name });

            // Act
            parentedList.AddRange(enumerableThatCreatesANewInstanceEachIteration);

            // Assert
            Assert.IsNull(item1.Parent);
            Assert.IsNull(item2.Parent);
            Assert.AreEqual(parent, parentedList[0].Parent);
            Assert.AreEqual(parent, parentedList[1].Parent);
        }

        [TestMethod]
        public void RemoveParentNull()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var parentedList = new ParentedList<TestClass>(parent) { item1 };

            // Act
            parentedList.RemoveParent(null);

            // Assert
            Assert.AreEqual(parent, item1.Parent);
        }

        [TestMethod]
        public void RemoveParentItemNotInList()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var parentedList = new ParentedList<TestClass>(parent) { item1 };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };

            // Act
            parentedList.RemoveParent(item2);

            // Assert
            Assert.AreEqual(parent, item1.Parent);
        }
    }
}