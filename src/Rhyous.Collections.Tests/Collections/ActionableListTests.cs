using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.Collections.Tests.Collections
{
    [TestClass]
    public class ActionableListObjectTests
    {
        [TestMethod]
        public void AddCallsAddAction()
        {
            // Arrange
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
            var item1 = new TestClass { Id = 2, Name = "Item 1" };
            bool addActionWasCalled = false;
            var ActionableList = new ActionableList<TestClass>((i) => { addActionWasCalled = true; }, null);

            // Act
            ActionableList.Insert(0, item1);

            // Assert
            Assert.IsTrue(addActionWasCalled);
        }


        [TestMethod]
        public void IndexerCallsAddAction()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            int addActionCalls = 0;
            var ActionableList = new ActionableList<TestClass>((i) => { addActionCalls++; }, null);
            ActionableList.Add(item1);

            // Act
            ActionableList[0] = item2;

            // Assert
            Assert.AreEqual(2, addActionCalls);
        }

        [TestMethod]
        public void IndexerCallsRemoveAction()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            int addActionCalls = 0;
            int removeActionCalls = 0;
            var ActionableList = new ActionableList<TestClass>((i) => { addActionCalls++; }, (i) => { removeActionCalls++; });
            ActionableList.Add(item1);

            // Act
            ActionableList[0] = item2;

            // Assert
            Assert.AreEqual(2, addActionCalls);
            Assert.AreEqual(1, removeActionCalls);
        }
        
        [TestMethod]
        public void RemoveAtCallsRemoveAction()
        {
            // Arrange
            var parent = new TestClass { Id = 0, Name = "Parent" };
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            int addActionCalls = 0;
            int removeActionCalls = 0;
            var ActionableList = new ActionableList<TestClass>((i) => { addActionCalls++; }, (i) => { removeActionCalls++; });
            ActionableList.AddRange(new[] { item1, item2 });
            Assert.AreEqual(0, removeActionCalls);

            // Act
            ActionableList.RemoveAt(1);

            // Assert
            Assert.AreEqual(2, addActionCalls);
            Assert.AreEqual(1, removeActionCalls);
        }
        
        [TestMethod]
        public void RemoveCallsRemovesAction()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            int addActionCalls = 0;
            int removeActionCalls = 0;
            var ActionableList = new ActionableList<TestClass>((i) => { addActionCalls++; }, (i) => { removeActionCalls++; });
            ActionableList.AddRange(new[] { item1, item2 });

            // Act
            ActionableList.Remove(item2);

            // Assert
            Assert.AreEqual(2, addActionCalls);
            Assert.AreEqual(1, removeActionCalls);
        }
        
        [TestMethod]
        public void ClearCallsRemoveAction()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            int addActionCalls = 0;
            int removeActionCalls = 0;
            var ActionableList = new ActionableList<TestClass>((i) => { addActionCalls++; }, (i) => { removeActionCalls++; });
            ActionableList.AddRange(new[] { item1, item2 });
            Assert.AreEqual(0, removeActionCalls);

            // Act
            ActionableList.Clear();

            // Assert
            Assert.AreEqual(2, addActionCalls);
            Assert.AreEqual(2, removeActionCalls);
        }

        [TestMethod]
        public void InsertRangeCallsAddAction()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            int addActionCalls = 0;
            int removeActionCalls = 0;
            var ActionableList = new ActionableList<TestClass>((i) => { addActionCalls++; }, (i) => { removeActionCalls++; });

            // Act
            ActionableList.InsertRange(0, new[] { item1, item2 });

            // Assert
            Assert.AreEqual(2, addActionCalls);
        }

        [TestMethod]
        public void RemoveRangeRemovesParent()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            int addActionCalls = 0;
            int removeActionCalls = 0;
            var ActionableList = new ActionableList<TestClass>((i) => { addActionCalls++; }, (i) => { removeActionCalls++; });
            ActionableList.AddRange(new[] { item1, item2 });

            // Act
            ActionableList.RemoveRange(0, 2);

            // Assert
            Assert.AreEqual(2, addActionCalls);
            Assert.AreEqual(2, removeActionCalls);
        }        
    }
}