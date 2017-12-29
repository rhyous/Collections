using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.Collections.Tests.Collections
{
    [TestClass]
    public class TreeTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var root = new TestClass { Id = 1, Name = "Root node" };
            var firstChild = new TestClass { Id = 1, Name = "First child node" };
            var tree = new Tree<TestClass> { Root = root };
            
            // Act
            tree.Root.Children.Add(firstChild);

            // Assert
            Assert.AreEqual(root, tree.Root);
            Assert.AreEqual(1, tree.Root.Children.Count);
            Assert.AreEqual(firstChild, tree.Root.Children[0]);
            Assert.AreEqual(firstChild.Parent, tree.Root.Children[0].Parent);
        }
    }
}
