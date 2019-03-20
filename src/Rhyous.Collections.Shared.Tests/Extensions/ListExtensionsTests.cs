using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Linq;

namespace Rhyous.Collections.Tests
{
    [TestClass]
    public class ListExtensionsTests
    {
        #region Add
        [TestMethod]
        public void ListExtensions_Add_NullList_Test()
        {
            // Arrange
            IList<int> list = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => { list.Add(1, (int i) => { }); });
        }

        [TestMethod]
        public void ListExtensions_Add_List_ActionCalled_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            bool wasCalled = false;
            int actualItem = 0;

            // Act
            list.Add(4, (int i) => { wasCalled = true; actualItem = i; });

            // Assert
            Assert.AreEqual(4, list.Count);
            Assert.IsTrue(wasCalled);
            Assert.AreEqual(4, list.Last());
        }

        [TestMethod]
        public void ListExtensions_Add_List_ActionNull_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            Action<int> action = null;

            // Act
            list.Add(4, action);

            // Assert
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(4, list.Last());
        }
        #endregion

        #region Clear
        [TestMethod]
        public void ListExtensions_Clear_NullList_Test()
        {
            // Arrange
            IList<int> list = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => { list.Clear((int i) => { }); });
        }

        [TestMethod]
        public void ListExtensions_Clear_NullList_IEnumerable_Test()
        {
            // Arrange
            IList<int> list = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => { list.Clear((IEnumerable<int> i) => { }); });
        }

        [TestMethod]
        public void ListExtensions_Clear_List_ActionCalled_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            int timesCalled = 0;

            // Act
            list.Clear((int i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(0, list.Count);
            Assert.AreEqual(3, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_Clear_List_ActionNull_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            Action<int> action = null;

            // Act
            list.Clear(action);

            // Assert
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void ListExtensions_Clear_List_ActionNull_Enumerable_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            Action<IEnumerable<int>> action = null;

            // Act
            list.Clear(action);

            // Assert
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void ListExtensions_Clear_List_ActionCalled_EnumerableAction_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            int timesCalled = 0;

            // Act
            list.Clear((IEnumerable<int> i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(0, list.Count);
            Assert.AreEqual(1, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_Clear_List_ActionNull_EnumerableAction_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            Action<IEnumerable<int>> action = null;

            // Act
            list.Clear(action);

            // Assert
            Assert.AreEqual(0, list.Count);
        }

        #endregion

        #region Insert
        [TestMethod]
        public void ListExtensions_Insert_NullList_Test()
        {
            // Arrange
            IList<int> list = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => { list.Insert(1, 27, (int index, int item) => { }); });
        }

        [TestMethod]
        public void ListExtensions_Insert_ActionCalled_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 3, 9 };
            bool wasCalled = false;
            int actualIndex = -1;
            int actualItem = -1;

            // Act
            list.Insert(1, 27, (int index, int item) =>
            {
                wasCalled = true;
                actualIndex = index;
                actualItem = item;
            });

            // Assert
            Assert.AreEqual(3, list.Count);
            Assert.IsTrue(wasCalled);
            Assert.AreEqual(1, actualIndex);
            Assert.AreEqual(27, actualItem);
        }

        [TestMethod]
        public void ListExtensions_Insert_NullAction_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 3, 9 };
            Action<int, int> action = null;

            // Act
            list.Insert(1, 27, action);

            // Assert
            Assert.AreEqual(3, list.Count);
        }

        #endregion

        #region Remove
        [TestMethod]
        public void ListExtensions_Remove_NullList_Test()
        {
            // Arrange
            IList<int> list = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => { list.Remove(27, (int item) => { }); });
        }

        [TestMethod]
        public void ListExtensions_Remove_ActionCalled_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 3, 9, 27 };
            bool wasCalled = false;
            int actualItem = -1;

            // Act
            var actual = list.Remove(27, (int item) =>
            {
                wasCalled = true;
                actualItem = item;
            });

            // Assert
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(wasCalled);
            Assert.IsTrue(actual);
            Assert.AreEqual(27, actualItem);
        }

        [TestMethod]
        public void ListExtensions_Remove_NullAction_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 3, 9, 27 };
            Action<int> action = null;

            // Act
            var actual = list.Remove(27, action);

            // Assert
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ListExtensions_Remove_NotFound_ActionNotCalled_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 3, 9, 27 };
            bool wasCalled = false;
            int actualItem = -1;

            // Act
            var actual = list.Remove(81, (int item) =>
            {
                wasCalled = true;
                actualItem = item;
            });

            // Assert
            Assert.AreEqual(3, list.Count);
            Assert.IsFalse(wasCalled);
            Assert.IsFalse(actual);
            Assert.AreEqual(-1, actualItem);
        }
        #endregion

        #region RemoveAny
        [TestMethod]
        public void ListExtensions_RemoveAny_NullList_Test()
        {
            // Arrange
            IList<int> list = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                list.RemoveAny(new[] { 27, 28 }, (IEnumerable<int> items) => { });
            });
        }

        [TestMethod]
        public void ListExtensions_RemoveAny_ItemsNull_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 3, 9, 27 };
            bool wasCalled = false;
            IEnumerable<int> actualItems = null;

            // Act
            list.RemoveAny(null, (IEnumerable<int> items) =>
            {
                wasCalled = true;
                actualItems = items;
            });

            // Assert
            Assert.AreEqual(3, list.Count);
            Assert.IsFalse(wasCalled);
        }

        [TestMethod]
        public void ListExtensions_RemoveAny_ItemsEmpty_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 3, 9, 27 };
            bool wasCalled = false;
            IEnumerable<int> actualItems = null;

            // Act
            list.RemoveAny(new int[] { }, (IEnumerable<int> items) =>
            {
                wasCalled = true;
                actualItems = items;
            });

            // Assert
            Assert.AreEqual(3, list.Count);
            Assert.IsFalse(wasCalled);
        }

        [TestMethod]
        public void ListExtensions_RemoveAny_ActionCalled_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 3, 9, 27 };
            bool wasCalled = false;
            IEnumerable<int> actualItems = null;

            // Act
            list.RemoveAny(new[] { 3, 27 }, (IEnumerable<int> items) =>
            {
                wasCalled = true;
                actualItems = items;
            });

            // Assert
            Assert.AreEqual(1, list.Count);
            Assert.IsTrue(wasCalled);
            CollectionAssert.AreEqual(new[] { 3, 27 }, actualItems.ToArray());
        }

        [TestMethod]
        public void ListExtensions_RemoveAny_NullAction_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 3, 9, 27 };
            Action<IEnumerable<int>> action = null;

            // Act
            list.RemoveAny(new[] { 9, 27 }, action);

            // Assert
            Assert.AreEqual(1, list.Count);
        }


        #endregion

        #region RemoveAt
        [TestMethod]
        public void ListExtensions_RemoveAt_NullList_Test()
        {
            // Arrange
            IList<int> list = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => { list.RemoveAt(3, (int items) => { }); });
        }

        [TestMethod]
        public void ListExtensions_RemoveAt_ActionCalled_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 3, 9, 27 };
            bool wasCalled = false;
            int actualItem = -1;

            // Act
            list.RemoveAt(2, (int item) =>
            {
                wasCalled = true;
                actualItem = item;
            });

            // Assert
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(wasCalled);
            Assert.AreEqual(27, actualItem);
        }

        [TestMethod]
        public void ListExtensions_RemoveAt_IndexHigher_ActionNotCalled_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 3, 9, 27 };
            bool wasCalled = false;
            int actualItem = -1;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                list.RemoveAt(3, (int item) =>
                    {
                        wasCalled = true;
                        actualItem = item;
                    });
            });
            Assert.IsFalse(wasCalled);
            Assert.AreEqual(-1, actualItem);
        }

        [TestMethod]
        public void ListExtensions_RemoveAt_NullAction_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 3, 9, 27 };
            Action<int> action = null;

            // Act
            list.RemoveAt(2, action);

            // Assert
            Assert.AreEqual(2, list.Count);
        }

        #endregion

        #region AddRange
        [TestMethod]
        public void ListExtensions_AddRange_NullList_Test()
        {
            // Arrange
            IList<int> list = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                list.AddRange(new[] { 27, 28 }, (int item) => { });
            });
        }

        [TestMethod]
        public void ListExtensions_AddRange_NullList_IEnumerableAction_Test()
        {
            // Arrange
            IList<int> list = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                list.AddRange(new[] { 27, 28 }, (IEnumerable<int> items) => { });
            });
        }

        [TestMethod]
        public void ListExtensions_AddRange_List_ItemsNull_ActionNotCalled_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            int timesCalled = 0;

            // Act
            list.AddRange(new int[] { }, (int i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(0, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_AddRange_List_ItemsNull_ActionNotCalled_EnumerableAction_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            int timesCalled = 0;

            // Act
            list.AddRange(new int[] { }, (IEnumerable<int> i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(0, timesCalled);
        }


        [TestMethod]
        public void ListExtensions_AddRange_List_ActionCalled_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            int timesCalled = 0;

            // Act
            list.AddRange(new[] { 5, 7, 11 }, (int i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(3, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_AddRange_List_ActionNull_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            Action<int> action = null;

            // Act
            list.AddRange(new[] { 5, 7, 11 }, action);

            // Assert
            Assert.AreEqual(6, list.Count);
        }

        [TestMethod]
        public void ListExtensions_AddRange_List_ActionNull_Enumerable_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            Action<IEnumerable<int>> action = null;

            // Act
            list.AddRange(new[] { 5, 7, 11 }, action);

            // Assert
            Assert.AreEqual(6, list.Count);
        }

        [TestMethod]
        public void ListExtensions_AddRange_List_ActionCalled_EnumerableAction_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            int timesCalled = 0;

            // Act
            list.AddRange(new[] { 5, 7, 11 }, (IEnumerable<int> i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(1, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_AddRange_List_ActionNull_EnumerableAction_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            Action<IEnumerable<int>> action = null;

            // Act
            list.AddRange(new[] { 5, 7, 11 }, action);

            // Assert
            Assert.AreEqual(6, list.Count);
        }

        #endregion

        #region AddRange not a child of List<T>
        [TestMethod]
        public void ListExtensions_AddRange_List_ItemsNull_ActionNotCalled_NotListT_Test()
        {
            // Arrange
            IList<int> list = new UniqueList<int> { 1, 2, 3 };
            int timesCalled = 0;

            // Act
            list.AddRange(new int[] { }, (int i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(0, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_AddRange_List_ActionCalled_NotListT_Test()
        {
            // Arrange
            IList<int> list = new UniqueList<int> { 1, 2, 3 };
            int timesCalled = 0;

            // Act
            list.AddRange(new[] { 5, 7, 11 }, (int i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(3, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_AddRange_List_ActionNull_NotListT_Test()
        {
            // Arrange
            IList<int> list = new UniqueList<int> { 1, 2, 3 };
            Action<int> action = null;

            // Act
            list.AddRange(new[] { 5, 7, 11 }, action);

            // Assert
            Assert.AreEqual(6, list.Count);
        }

        [TestMethod]
        public void ListExtensions_AddRange_List_ActionCalled_EnumerableAction_NotListT_Test()
        {
            // Arrange
            IList<int> list = new UniqueList<int> { 1, 2, 3 };
            int timesCalled = 0;

            // Act
            list.AddRange(new[] { 5, 7, 11 }, (IEnumerable<int> i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(1, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_AddRange_List_ActionNull_EnumerableAction_NotListT_Test()
        {
            // Arrange
            IList<int> list = new UniqueList<int> { 1, 2, 3 };
            Action<IEnumerable<int>> action = null;

            // Act
            list.AddRange(new[] { 5, 7, 11 }, action);

            // Assert
            Assert.AreEqual(6, list.Count);
        }
        #endregion

        #region SetIndex
        [TestMethod]
        public void ListExtensions_SetIndex_NullList_Test()
        {
            // Arrange
            IList<int> list = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => { list.SetIndex(3, 27, (int i) => { }, (int i) => { }); });
        }

        [TestMethod]
        public void ListExtensions_SetIndex_List_AddActionCalled_RemoveActionCalled_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            bool addWasCalled = false;
            int addActualItem = 0;
            bool removeWasCalled = false;
            int removeActualItem = 0;

            // Act
            list.SetIndex(2, 4, (int i) => { addWasCalled = true; addActualItem = i; },
                                (int i) => { removeWasCalled = true; removeActualItem = i; });

            // Assert
            Assert.AreEqual(3, list.Count);
            Assert.IsTrue(addWasCalled);
            Assert.AreEqual(4, addActualItem);
            Assert.IsTrue(removeWasCalled);
            Assert.AreEqual(3, removeActualItem);
        }

        [TestMethod]
        public void ListExtensions_SetIndex_List_ActionNull_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            Action<int> action = null;

            // Act
            list.SetIndex(2, 4, action, action);

            // Assert
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(4, list.Last());
        }
        #endregion

        #region InsertRange
        [TestMethod]
        public void ListExtensions_InsertRange_NullList_Test()
        {
            // Arrange
            IList<int> list = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => { list.InsertRange(3, new[] { 27, 28 }, (IEnumerable<int> items) => { }); });
        }

        [TestMethod]
        public void ListExtensions_InsertRange_NullList_EnumerableAction_Test()
        {
            // Arrange
            IList<int> list = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => { list.InsertRange(3, new[] { 27, 28 }, (int index, int item) => { }); });
        }

        [TestMethod]
        public void ListExtensions_InsertRange_List_ItemsNull_ActionNotCalled_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            int timesCalled = 0;

            // Act
            list.InsertRange(1, new int[] { }, (int index, int i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(0, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_InsertRange_List_ItemsNull_Enumerable_ActionNotCalled_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            int timesCalled = 0;

            // Act
            list.InsertRange(1, new int[] { }, (IEnumerable<int> items) => { timesCalled++; });

            // Assert
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(0, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_InsertRange_List_ActionCalled_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            int timesCalled = 0;

            // Act
            list.InsertRange(1, new[] { 5, 7, 11 }, (int index, int i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(3, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_InsertRange_List_ActionNull_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            Action<int, int> action = null;

            // Act
            list.InsertRange(1, new[] { 5, 7, 11 }, action);

            // Assert
            Assert.AreEqual(6, list.Count);
        }

        [TestMethod]
        public void ListExtensions_InsertRange_List_ActionCalled_EnumerableAction_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            int timesCalled = 0;

            // Act
            list.InsertRange(1, new[] { 5, 7, 11 }, (IEnumerable<int> i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(1, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_InsertRange_List_ActionNull_EnumerableAction_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3 };
            Action<IEnumerable<int>> action = null;

            // Act
            list.InsertRange(1, new[] { 5, 7, 11 }, action);

            // Assert
            Assert.AreEqual(6, list.Count);
        }
        #endregion

        #region InsertRange not a child of List<T>
        [TestMethod]
        public void ListExtensions_InsertRange_List_ItemsNull_ActionNotCalled_NotListT_Test()
        {
            // Arrange
            IList<int> list = new UniqueList<int> { 1, 2, 3 };
            int timesCalled = 0;

            // Act
            list.InsertRange(1, new int[] { }, (int index, int i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(0, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_InsertRange_List_ActionCalled_NotListT_Test()
        {
            // Arrange
            IList<int> list = new UniqueList<int> { 1, 2, 3 };
            int timesCalled = 0;

            // Act
            list.InsertRange(1, new[] { 5, 7, 11 }, (int index, int i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(3, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_InsertRange_List_ActionNull_NotListT_Test()
        {
            // Arrange
            IList<int> list = new UniqueList<int> { 1, 2, 3 };
            Action<int, int> action = null;

            // Act
            list.InsertRange(1, new[] { 5, 7, 11 }, action);

            // Assert
            Assert.AreEqual(6, list.Count);
        }

        [TestMethod]
        public void ListExtensions_InsertRange_List_ActionCalled_EnumerableAction_NotListT_Test()
        {
            // Arrange
            IList<int> list = new UniqueList<int> { 1, 2, 3 };
            int timesCalled = 0;

            // Act
            list.InsertRange(1, new[] { 5, 7, 11 }, (IEnumerable<int> i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(1, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_InsertRange_List_ActionNull_EnumerableAction_NotListT_Test()
        {
            // Arrange
            IList<int> list = new UniqueList<int> { 1, 2, 3 };
            Action<IEnumerable<int>> action = null;

            // Act
            list.InsertRange(1, new[] { 5, 7, 11 }, action);

            // Assert
            Assert.AreEqual(6, list.Count);
        }
        #endregion

        #region GetRange
        [TestMethod]
        public void ListExtensions_GetRange_NullList_Test()
        {
            // Arrange
            IList<int> list = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => { list.GetRange(3, 8); });
        }

        [TestMethod]
        public void ListExtensions_GetRange_Test()
        {
            // Arrange
            IList<int> list = new List<int> { 2, 3, 5, 7, 11 };

            // Act
            var actual = list.GetRange(2, 2);

            // Assert
            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual(5, actual[0]);
            Assert.AreEqual(7, actual[1]);
            Assert.IsTrue(actual is IRangeableList<int>);

        }
        #endregion

        #region RemoveRange
        [TestMethod]
        public void ListExtensions_RemoveRange_NullList_Enumerable_Test()
        {
            // Arrange
            IList<int> list = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => { list.RemoveRange(3, 8, (IEnumerable<int> items) => { }); });
        }

        [TestMethod]
        public void ListExtensions_RemoveRange_NullList_Test()
        {
            // Arrange
            IList<int> list = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => { list.RemoveRange(3, 8, (int item) => { }); });
        }

        [TestMethod]
        public void ListExtensions_RemoveRange_List_ActionCalled__Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int timesCalled = 0;

            // Act
            list.RemoveRange(3, 6, (int i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(6, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_RemoveRange_List_ActionNull__Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Action<int> action = null;

            // Act
            list.RemoveRange(3, 6, action);

            // Assert
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void ListExtensions_RemoveRange_List_ActionCalled_EnumerableAction__Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int timesCalled = 0;

            // Act
            list.RemoveRange(3, 6, (IEnumerable<int> i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(1, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_RemoveRange_List_ActionNull_EnumerableAction__Test()
        {
            // Arrange
            IList<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Action<IEnumerable<int>> action = null;

            // Act
            list.RemoveRange(3, 6, action);

            // Assert
            Assert.AreEqual(3, list.Count);
        }

        #endregion

        #region RemoveRange not a child of List<T>

        [TestMethod]
        public void ListExtensions_RemoveRange_List_ActionCalled_NotListT_Test()
        {
            // Arrange
            IList<int> list = new UniqueList<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int timesCalled = 0;

            // Act
            list.RemoveRange(3, 6, (int i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(3, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_RemoveRange_List_ActionNull_NotListT_Test()
        {
            // Arrange
            IList<int> list = new UniqueList<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Action<int> action = null;

            // Act
            list.RemoveRange(3, 6, action);

            // Assert
            Assert.AreEqual(6, list.Count);
        }

        [TestMethod]
        public void ListExtensions_RemoveRange_List_ActionCalled_EnumerableAction_NotListT_Test()
        {
            // Arrange
            IList<int> list = new UniqueList<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int timesCalled = 0;

            // Act
            list.RemoveRange(3, 6, (IEnumerable<int> i) => { timesCalled++; });

            // Assert
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(1, timesCalled);
        }

        [TestMethod]
        public void ListExtensions_RemoveRange_List_ActionNull_EnumerableAction_NotListT_Test()
        {
            // Arrange
            IList<int> list = new UniqueList<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Action<IEnumerable<int>> action = null;

            // Act
            list.RemoveRange(3, 6, action);

            // Assert
            Assert.AreEqual(6, list.Count);
        }
        #endregion

        #region ToRangeableList
        [TestMethod]
        public void ListExtensions_ToRangeableList_Test()
        {
            // Arrange
            IEnumerable<int> list = new UniqueList<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Act
            var actual = list.ToRangeableList();

            // Assert
            Assert.IsTrue(actual is RangeableList<int>);
            Assert.AreEqual(9, actual.Count);
        }

        [TestMethod]
        public void ListExtensions_ToRangeableList_ListIsEmpty_Test()
        {
            // Arrange
            IEnumerable<int> list = new int[] { };

            // Act
            var actual = list.ToRangeableList();

            // Assert
            Assert.IsTrue(actual is RangeableList<int>);
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void ListExtensions_ToRangeableList_ListIsNull_Test()
        {
            // Arrange
            IEnumerable<int> list = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => { list.ToRangeableList(); });
        }
        #endregion

        #region ToRangeableList Cast
        [TestMethod]
        public void ListExtensions_ToRangeableList_Cast_Test()
        {
            // Arrange
            IEnumerable<B> list = new UniqueList<B> { new B() };

            // Act
            var actual = list.ToRangeableList<B, A>();

            // Assert
            Assert.IsTrue(actual is RangeableList<A>);
            Assert.AreEqual(1, actual.Count);
        }

        [TestMethod]
        public void ListExtensions_ToRangeableList_Cast_ListIsEmpty_Test()
        {
            // Arrange
            IEnumerable<B> list = new UniqueList<B> { };

            // Act
            var actual = list.ToRangeableList<B, A>();

            // Assert
            Assert.IsTrue(actual is RangeableList<A>);
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void ListExtensions_ToRangeableList_Cast_ListIsNull_Test()
        {
            // Arrange
            IEnumerable<B> list = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => { list.ToRangeableList<B, A>(); });
        }
        #endregion

    }
}

