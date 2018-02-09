using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Rhyous.Collections.Tests.Dictionaries
{
    [TestClass]
    public class UniqueListTests
    {
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
            var list = new UniqueList<string> { "A" , "B" };

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
            var item2 = new TestClass { Id = 2, Name = "2"};
            var comparer = new IdEqualityComparer();
            var list = new UniqueList<TestClass>(new[] { item1, item2 }, comparer);

            // Act
            list[0] = item1dot1;

            // Assert
            Assert.AreEqual(list[0].Name, item1dot1.Name);
        }

        interface IId { int Id { get; set; } }
        class TestClass : IId
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        class IdEqualityComparer : IEqualityComparer<IId>
        {
            public bool Equals(IId x, IId y) => x.Id == y.Id;

            public int GetHashCode(IId obj) => throw new System.NotImplementedException();
        }
    }
}
