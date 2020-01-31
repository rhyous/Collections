using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Rhyous.Collections.Tests.Collections
{
    [TestClass]
    public class ActionableListTests
    {
        #region Constructors
        [TestMethod]
        public void ActionableList_Constructor_Test()
        {
            // Arrange
            Action<TestClass> addAction = (i) => { };
            Action<TestClass> removeAction = (i) => { };
            Action<int, TestClass> insertAction = (id, i) => { };

            // Act
            var list = new ActionableList<TestClass>(addAction, removeAction, insertAction);

            // Assert
            Assert.AreEqual(addAction, list.AddAction);
            Assert.AreEqual(removeAction, list.RemoveAction);
            Assert.AreEqual(insertAction, list.InsertAction);
        }

        [TestMethod]
        public void ActionableList_Constructor_NullInsertAction_Test()
        {
            // Arrange
            bool wasCalled = false;
            Action<TestClass> addAction = (i) => { wasCalled = true; };
            Action<TestClass> removeAction = (i) => { };
            Action<int, TestClass> insertAction = null;

            // Act
            var list = new ActionableList<TestClass>(addAction, removeAction, insertAction);
            list.InsertAction(1, new TestClass());

            // Assert
            Assert.AreEqual(addAction, list.AddAction);
            Assert.AreEqual(removeAction, list.RemoveAction);
            Assert.IsNotNull(list.InsertAction);
            Assert.IsTrue(wasCalled);
        }

        [TestMethod]
        public void ActionableList_Constructor_Capacity_Test()
        {
            // Arrange
            Action<TestClass> addAction = (i) => { };
            Action<TestClass> removeAction = (i) => { };
            Action<int, TestClass> insertAction = null;

            // Act
            var list = new ActionableList<TestClass>(addAction, removeAction, 10, insertAction);

            // Assert
            Assert.AreEqual(10, list.Capacity);
        }

        [TestMethod]
        public void ActionableList_Constructor_Enumerable_Test()
        {
            // Arrange
            Action<TestClass> addAction = (i) => { };
            Action<TestClass> removeAction = (i) => { };
            Action<int, TestClass> insertAction = null;
            var items = new List<TestClass> { new TestClass { }, new TestClass { } };

            // Act
            var list = new ActionableList<TestClass>(addAction, removeAction, items, insertAction);
            list.InsertAction(1, new TestClass());

            // Assert
            Assert.AreEqual(2, list.Count);
        }

        #endregion

        [TestMethod]
        public void ActionableList_SetCapacity_Test()
        {
            // Arrange
            Action<TestClass> addAction = (i) => { };
            Action<TestClass> removeAction = (i) => { };
            Action<int, TestClass> insertAction = null;
            var list = new ActionableList<TestClass>(addAction, removeAction, 10, insertAction);

            // Act
            list.Capacity = 20;

            // Assert
            Assert.AreEqual(20, list.Capacity);
        }

        [TestMethod]
        public void ActionableList_AddCallsAddAction_Test()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            bool addActionWasCalled = false;
            var ActionableList = new ActionableList<TestClass>((i) => { addActionWasCalled = true; }, null);

            // Act
            ActionableList.Add(item1);

            // Assert
            Assert.IsTrue(addActionWasCalled);
        }

        [TestMethod]
        public void ActionableList_AddRangeCallsActionPerItem_Test()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            int addActionCalls = 0;
            var ActionableList = new ActionableList<TestClass>((i) => { addActionCalls++; }, null);

            // Act
            ActionableList.AddRange(new[] { item1, item2 });

            // Assert
            Assert.AreEqual(2, addActionCalls);
        }

        [TestMethod]
        public void ActionableList_InsertCallsAddAction_Test()
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
        public void ActionableList_InsertCallsAddAction_Null_Test()
        {
            // Arrange
            var item1 = new TestClass { Id = 2, Name = "Item 1" };
            var ActionableList = new ActionableList<TestClass>(null, null);

            // Act
            ActionableList.Insert(0, item1);

            // Assert
            Assert.AreEqual(1, ActionableList.Count);
        }

        [TestMethod]
        public void ActionableList_IndexerCallsAddAction_Test()
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
        public void ActionableList_IndexerCallsRemoveAction_Test()
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
        public void ActionableList_RemoveAtCallsRemoveAction_Test()
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
        public void ActionableList_RemoveCallsRemovesAction_Test()
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
        public void ActionableList_ClearCallsRemoveAction_Test()
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
        public void ActionableList_InsertRangeCallsAddAction_Test()
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
        public void ActionableList_RemoveRangeRemovesParent_Test()
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

        [TestMethod]
        public void ActionableList_IsReadOnly_Test()
        {
            // Arrange
            var ActionableList = new ActionableList<TestClass>(null,null);

            // Act
            // Assert
            Assert.IsFalse(ActionableList.IsReadOnly);
        }

        [TestMethod]
        public void ActionableList_Contains_Test()
        {
            // Arrange
            var tc = new TestClass();
            var ActionableList = new ActionableList<TestClass>(null, null, new[] { tc });

            // Act
            // Assert
            Assert.IsTrue(ActionableList.Contains(tc));
        }

        [TestMethod]
        public void ActionableList_CopyTo_Test()
        {
            // Arrange
            var tc = new TestClass();
            var actionableList = new ActionableList<TestClass>(null, null, new[] { tc });
            TestClass[] array = new TestClass[1];

            // Act
            actionableList.CopyTo(array, 0);

            // Assert
            Assert.AreEqual(tc, array[0]);
        }

        [TestMethod]
        public void ActionableList_IEnumerable_Test()
        {
            // Arrange
            var list = new ActionableList<int>(null, null) { 1, 2 };
            var e = list as IEnumerable;

            // Act
            // Assert
            int i = 1;
            foreach (var item in e)
            {
                Assert.AreEqual(i++, item);
            }
        }

        [TestMethod]
        public void ActionableList_IEnumerableT_Test()
        {
            // Arrange
            var list = new ActionableList<int>(null, null) { 1, 2 };
            var e = list as IEnumerable<int>;

            // Act
            // Assert
            int i = 1;
            foreach (var item in e)
            {
                Assert.AreEqual(i++, item);
            }
        }


        #region GetRange
        [TestMethod]
        public void ActionableList_GetRange_Test()
        {
            // Arrange
            var list = new ActionableList<int>(null, null) { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Act
            var actual = list.GetRange(5, 2);

            // Assert
            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual(5, actual[0]);
            Assert.AreEqual(6, actual[1]);
        }
        #endregion

        #region GetRange
        [TestMethod]
        public void ActionableList_IndexOf_Test()
        {
            // Arrange
            var list = new ActionableList<int>(null, null) { 2, 3, 5, 7, 11, 13, 17 };

            // Act
            var actual = list.IndexOf(13);

            // Assert
            Assert.AreEqual(5, actual);
        }
        #endregion


    }
}