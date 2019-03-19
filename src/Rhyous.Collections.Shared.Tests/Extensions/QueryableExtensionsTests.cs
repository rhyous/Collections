using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Linq;

namespace Rhyous.Collections.Tests
{
    [TestClass]
    public class QueryableExtensionsTests
    {
        #region IfSkip
        [TestMethod]
        public void IQueryableExtensions_IfSkip_QueryableNull_Test()
        {
            // Arrange
            IQueryable<Person> queryable = null;

            // Act
            var actual = queryable.IfSkip(0);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void IQueryableExtensions_IfSkip_0_SkipsNone_Test()
        {
            // Arrange
            var queryable = new List<Person>
            {
                new Person { Name = "Jared"},
                new Person { Name = "Aiden"}
            }.AsQueryable();

            // Act
            var actual = queryable.IfSkip(0);
            var actualList = actual.ToList();

            // Assert
            Assert.AreEqual(2, actualList.Count);
            Assert.AreEqual("Jared", actualList[0].Name);
            Assert.AreEqual("Aiden", actualList[1].Name);
        }

        [TestMethod]
        public void IQueryableExtensions_IfSkip_Test()
        {
            // Arrange
            var queryable = new List<Person>
            {
                new Person { Name = "Jared"},
                new Person { Name = "Aiden"}
            }.AsQueryable();

            // Act
            var actual = queryable.Skip(1);
            var actualList = actual.ToList();

            // Assert
            Assert.AreEqual(1, actualList.Count);
            Assert.AreEqual("Aiden", actualList[0].Name);
        }
        #endregion

        #region IfTake
        [TestMethod]
        public void IQueryableExtensions_IfTake_QueryableNull_Test()
        {
            // Arrange
            IQueryable<Person> queryable = null;

            // Act
            var actual = queryable.IfTake(0);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void IQueryableExtensions_IfTake_0_TakesAll_Test()
        {
            // Arrange
            var queryable = new List<Person>
            {
                new Person { Name = "Jared"},
                new Person { Name = "Aiden"}
            }.AsQueryable();

            // Act
            var actual = queryable.IfTake(0);
            var actualList = actual.ToList();

            // Assert
            Assert.AreEqual(2, actualList.Count);
            Assert.AreEqual("Jared", actualList[0].Name);
            Assert.AreEqual("Aiden", actualList[1].Name);
        }

        [TestMethod]
        public void IQueryableExtensions_IfTake_Test()
        {
            // Arrange
            var queryable = new List<Person>
            {
                new Person { Name = "Jared"},
                new Person { Name = "Aiden"}
            }.AsQueryable();

            // Act
            var actual = queryable.Take(1);
            var actualList = actual.ToList();

            // Assert
            Assert.AreEqual(1, actualList.Count);
            Assert.AreEqual("Jared", actualList[0].Name);
        }
        #endregion
    }
}
