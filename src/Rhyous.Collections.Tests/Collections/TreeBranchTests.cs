using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.Collections.Tests
{
    [TestClass]
    public class TreeBranchTests
    {
        [TestMethod]
        public void TreeBranch_Constructor_Test()
        {
            // Arrange
            // Act
            var branch = new TreeBranch<int>();

            // Assert
            Assert.IsNotNull(branch);
        }

        [TestMethod]
        public void TreeBranch_Constructor_Item_Test()
        {
            // Arrange
            // Act
            var branch = new TreeBranch<int>(12);

            // Assert
            Assert.IsNotNull(branch);
            Assert.AreEqual(12, branch.Item);
        }

        [TestMethod]
        public void TreeBranch_Children_Test()
        {
            // Arrange
            // Act
            var branch = new TreeBranch<int>(12) { Children = new ParentedList<TreeBranch<int>, TreeBranch<int>>() };

            // Assert
            Assert.IsNotNull(branch);
            Assert.IsNotNull(branch.Children);
            Assert.AreEqual(12, branch.Item);
        }
    }
}
