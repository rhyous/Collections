using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.Collections.Tests.Collections
{
    [TestClass]
    public class ActionableListObjectTests
    {
        [TestMethod]
        public void AddSetsParent()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            bool addActionWasCalled = false;
            var ActionableList = new ActionableList<TestClass>((i)=> { addActionWasCalled = true; }, null);

            // Act
            ActionableList.Add(item1);

            // Assert
            Assert.IsTrue(addActionWasCalled);
        }

        [TestMethod]
        public void AddRangeCallsActionPerItem()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            int addActionCalls = 0;
            var ActionableList = new ActionableList<TestClass>((i) => { addActionCalls++; }, null);

            // Act
            ActionableList.AddRange(new[] { item1, item2 } );

            // Assert
            Assert.AreEqual(2, addActionCalls);
        }

        [TestMethod]
        public void InsertCallsAddAction()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 2, Name = "Item 1" };
            bool addActionWasCalled = false;
            var ActionableList = new ActionableList<TestClass>((i) => { addActionWasCalled = true; }, null);

            // Act
            ActionableList.Insert(0, item1);

            // Assert
            Assert.IsTrue(addActionWasCalled);
        }


        [TestMethod]
        public void IndexerSetsParent()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var ActionableList = new ActionableList<TestClass>(parent);
            ActionableList.Add(item1);

            // Act
            ActionableList[0] = item2;

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
            var ActionableList = new ActionableList<TestClass>(parent);
            ActionableList.Add(item1);

            // Act
            ActionableList[0] = item2;

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
            var ActionableList = new ActionableList<TestClass>(parent);
            ActionableList.AddRange(new[] { item1, item1 });

            // Act
            ActionableList[1] = item2;

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
            var ActionableList = new ActionableList<TestClass>(parent);
            ActionableList.AddRange(new[] { item1, item2 });

            // Act
            ActionableList.RemoveAt(1);

            // Assert
            Assert.IsNull(item2.Parent);
        }

        [TestMethod]
        public void RemoveAtDoesNotRemovesParentIfAddedToListUnderADifferentIndex()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var ActionableList = new ActionableList<TestClass>(parent);
            ActionableList.AddRange(new[] { item1, item1 });

            // Act
            ActionableList.RemoveAt(1);

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
            var ActionableList = new ActionableList<TestClass>(parent);
            ActionableList.AddRange(new[] { item1, item2 });

            // Act
            ActionableList.Remove(item2);

            // Assert
            Assert.IsNull(item2.Parent);
        }

        [TestMethod]
        public void RemoveDoesNotRemovesParentIfAddedToListMultipleTimes()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var ActionableList = new ActionableList<TestClass>(parent);
            ActionableList.AddRange(new[] { item1, item1 });

            // Act
            ActionableList.Remove(item1);

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
            var ActionableList = new ActionableList<TestClass>(parent);
            ActionableList.AddRange(new[] { item1, item2 });

            // Act
            ActionableList.Clear();

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
            var ActionableList = new ActionableList<TestClass>(parent);

            // Act
            ActionableList.InsertRange(0, new[] { item1, item2 });

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
            var ActionableList = new ActionableList<TestClass>(parent);
            ActionableList.AddRange(new[] { item1, item2 });

            // Act
            ActionableList.RemoveRange(0, 2);

            // Assert
            Assert.IsNull(item1.Parent);
            Assert.IsNull(item2.Parent);
        }
    }
}