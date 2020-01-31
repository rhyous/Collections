using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Rhyous.Collections.Tests.Dictionaries
{
    [TestClass]
    public class UniqueListTests
    {
        #region Checking Duplicates
        [TestMethod]
        public void AddingDuplicateThrowsDuplicateItemException()
        {
            // Arrange
            var list = new UniqueList<string> { "A" };

            // Act
            // Assert
            Assert.ThrowsException<DuplicateItemException>(() => list.Add("A"));
        }

        [TestMethod]
        public void InsertingDuplicateThrowsDuplicateItemException()
        {
            // Arrange
            var list = new UniqueList<string> { "A" };

            // Act
            // Assert
            Assert.ThrowsException<DuplicateItemException>(() => list.Insert(0, "A"));
        }

        [TestMethod]
        public void AddingDuplicateDoesNothing()
        {
            // Arrange
            var list = new UniqueList<string> { "A" };
            list.ThrowOnDuplicate = false;

            // Act
            list.Add("A");

            // Assert
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void SettingDuplicateViaIndexThrows()
        {
            // Arrange
            var list = new UniqueList<string> { "A", "B" };

            // Act
            // Assert
            Assert.ThrowsException<DuplicateItemException>(() => list[1] = "A");
        }

        [TestMethod]
        public void SettingDuplicateViaIndexIsAllowedIfSame()
        {
            // Arrange
            var list = new UniqueList<string> { "A", "B" };

            // Act
            list[0] = "A";

            // Assert
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void SettingDuplicateViaIndexIsAllowedIfSameAccordingToEqualityComparer()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "1" };
            var item1dot1 = new TestClass { Id = 1, Name = "1.1" };
            var item2 = new TestClass { Id = 2, Name = "2" };
            var comparer = new IdEqualityComparer();
            var list = new UniqueList<TestClass>(new[] { item1, item2 }, comparer);

            // Act
            list[0] = item1dot1;

            // Assert
            Assert.AreEqual(list[0].Name, item1dot1.Name);
        }

        interface IId { int Id { get; set; } }
        internal class TestClass : IId
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        class IdEqualityComparer : IEqualityComparer<IId>
        {
            public bool Equals(IId x, IId y) => x.Id == y.Id;

            public int GetHashCode(IId obj) => throw new System.NotImplementedException();
        }
        #endregion

        #region Constructors
        [TestMethod]
        public void UniqueList_Constructor_Test()
        {
            // Arrange
            // Act
            var list = new UniqueList<TestClass>();

            // Assert
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void UniqueList_Constructor_Capacity_Test()
        {
            // Arrange
            // Act
            var list = new UniqueList<TestClass>(10);

            // Assert
            Assert.AreEqual(10, list.Capacity);
        }

        [TestMethod]
        public void UniqueList_Constructor_Enumerable_Test()
        {
            // Arrange
            var items = new List<TestClass> { new TestClass { }, new TestClass { } };

            // Act
            var list = new UniqueList<TestClass>(items);

            // Assert
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void UniqueList_Constructor_NullEnumerable_Test()
        {
            // Arrange
            IEnumerable<TestClass> items = null;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => { new UniqueList<TestClass>(items); });            
        }

        [TestMethod]
        public void UniqueList_Constructor_CustomEqualityComparer_Test()
        {
            // Arrange
            var comparer = new IdEqualityComparer();

            // Act
            var list = new UniqueList<TestClass>(comparer);

            // Assert
            Assert.AreEqual(comparer, list.EqualityComparer);
        }

        #endregion

        [TestMethod]
        public void UniqueList_SetCapacity_Test()
        {
            // Arrange
            var list = new UniqueList<TestClass>(10);

            // Act
            list.Capacity = 20;

            // Assert
            Assert.AreEqual(20, list.Capacity);
        }

        [TestMethod]
        public void UniqueList_Add_Test()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var uniqueList = new UniqueList<TestClass>();

            // Act
            uniqueList.Add(item1);

            // Assert
            Assert.AreEqual(1, uniqueList.Count);
        }

        [TestMethod]
        public void UniqueList_AddRange_Test()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var uniqueList = new UniqueList<TestClass>();

            // Act
            uniqueList.AddRange(new[] { item1, item2 });

            // Assert
            Assert.AreEqual(2, uniqueList.Count);
        }

        [TestMethod]
        public void UniqueList_AddRange_IgnoreDuplicates_Test()
        {
            // Arrange
            var list = new UniqueList<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            list.ThrowOnDuplicate = false;

            // Act
            list.AddRange(new[] { 0, 5, 10 });

            // Assert
            Assert.AreEqual(11, list.Count);
        }

        [TestMethod]
        public void UniqueList_AddRange_ThrowOnDuplicate_Test()
        {
            // Arrange
            var list = new UniqueList<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Act
            // Assert
            Assert.ThrowsException<DuplicateItemException>(() => 
            {
                list.AddRange(new[] { 0, 5, 10 });
            });
        }

        [TestMethod]
        public void UniqueList_Insert_Test()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var uniqueList = new UniqueList<TestClass>();

            // Act
            uniqueList.Insert(0, item1);

            // Assert
            Assert.AreEqual(1, uniqueList.Count);
        }

        [TestMethod]
        public void UniqueList_RemoveAt_Test()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var uniqueList = new UniqueList<TestClass>();
            uniqueList.AddRange(new[] { item1, item2 });

            // Act
            uniqueList.RemoveAt(1);

            // Assert
            Assert.AreEqual(1, uniqueList.Count);
        }

        [TestMethod]
        public void UniqueList_Remove_Test()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var uniqueList = new UniqueList<TestClass>();
            uniqueList.AddRange(new[] { item1, item2 });

            // Act
            uniqueList.Remove(item2);

            // Assert
            Assert.AreEqual(1, uniqueList.Count);
        }

        [TestMethod]
        public void UniqueList_Clear_Test()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var uniqueList = new UniqueList<TestClass>();
            uniqueList.AddRange(new[] { item1, item2 });

            // Act
            uniqueList.Clear();

            // Assert
            Assert.AreEqual(0, uniqueList.Count);
        }

        [TestMethod]
        public void UniqueList_InsertRange_Test()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var item3 = new TestClass { Id = 3, Name = "Item 3" };
            var item4 = new TestClass { Id = 4, Name = "Item 4" };
            var uniqueList = new UniqueList<TestClass> { item3, item4 };

            // Act
            uniqueList.InsertRange(0, new[] { item1, item2 });

            // Assert
            Assert.AreEqual(4, uniqueList.Count);
        }

        [TestMethod]
        public void UniqueList_RemoveRange_Test()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var item3 = new TestClass { Id = 3, Name = "Item 3" };
            var item4 = new TestClass { Id = 4, Name = "Item 4" };
            var uniqueList = new UniqueList<TestClass> { item1, item2 , item3, item4 };

            // Act
            uniqueList.RemoveRange(0, 2);

            // Assert
            Assert.AreEqual(2, uniqueList.Count);
        }

        [TestMethod]
        public void UniqueList_IsReadOnly_Test()
        {
            // Arrange
            var uniqueList = new UniqueList<TestClass>();

            // Act
            // Assert
            Assert.IsFalse(uniqueList.IsReadOnly);
        }

        [TestMethod]
        public void UniqueList_Contains_Test()
        {
            // Arrange
            var tc = new TestClass();
            var uniqueList = new UniqueList<TestClass>(new[] { tc });

            // Act
            // Assert
            Assert.IsTrue(uniqueList.Contains(tc));
        }

        [TestMethod]
        public void UniqueList_CopyTo_Test()
        {
            // Arrange
            var tc = new TestClass();
            var uniqueList = new UniqueList<TestClass>(new[] { tc });
            TestClass[] array = new TestClass[1];

            // Act
            uniqueList.CopyTo(array, 0);

            // Assert
            Assert.AreEqual(tc, array[0]);
        }

        [TestMethod]
        public void UniqueList_IEnumerable_Test()
        {
            // Arrange
            var list = new UniqueList<int>() { 1, 2 };
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
        public void UniqueList_IEnumerableT_Test()
        {
            // Arrange
            var list = new UniqueList<int>() { 1, 2 };
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
        public void UniqueList_GetRange_Test()
        {
            // Arrange
            var list = new UniqueList<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Act
            var actual = list.GetRange(5, 2);

            // Assert
            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual(5, actual[0]);
            Assert.AreEqual(6, actual[1]);
        }
        #endregion

        #region IndexOf
        [TestMethod]
        public void UniqueList_IndexOf_Test()
        {
            // Arrange
            var list = new UniqueList<int>() { 2, 3, 5, 7, 11, 13, 17 };

            // Act
            var actual = list.IndexOf(13);

            // Assert
            Assert.AreEqual(5, actual);
        }

        [TestMethod]
        public void UniqueList_IndexOf_NotFound_Test()
        {
            // Arrange
            var list = new UniqueList<int>() { 2, 3, 5, 7, 11, 13, 17 };

            // Act
            var actual = list.IndexOf(19);

            // Assert
            Assert.AreEqual(-1, actual);
        }

        [TestMethod]
        public void UniqueList_IndexOf_EqualityComparer_NotFound_Test()
        {
            // Arrange
            var item1 = new TestClass { Id = 1, Name = "Item 1" };
            var item2 = new TestClass { Id = 2, Name = "Item 2" };
            var item3 = new TestClass { Id = 3, Name = "Item 3" };
            var item4 = new TestClass { Id = 4, Name = "Item 4" };
            var item5 = new TestClass { Id = 5, Name = "Item 5" };
            var list = new UniqueList<TestClass>(new IdEqualityComparer()) { item1, item2, item3, item4 };

            // Act
            var actual = list.IndexOf(item5);

            // Assert
            Assert.AreEqual(-1, actual);
        }
        #endregion
    }
}