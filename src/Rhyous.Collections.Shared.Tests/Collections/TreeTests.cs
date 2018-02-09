using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.Collections.Tests.Collections
{
    [TestClass]
    public class TreeTests
    {
        [TestMethod]
        public void TestTreeableObject()
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

        [TestMethod]
        public void TestNonTreeableObject()
        {
            // Arrange
            var root = "Root node";
            var wrappedRoot = new TreeBranch<string>(root);
            var firstChild = "First child node";
            var wrappedFirstChild = new TreeBranch<string>(firstChild);
            var tree = new Tree<TreeBranch<string>> { Root = wrappedRoot };

            // Act
            tree.Root.Children.Add(wrappedFirstChild);

            // Assert
            Assert.AreEqual(root, tree.Root.Item);
            Assert.AreEqual(wrappedRoot, tree.Root);
            Assert.AreEqual(1, tree.Root.Children.Count);
            Assert.AreEqual(firstChild, tree.Root.Children[0].Item);
            Assert.AreEqual(wrappedFirstChild, tree.Root.Children[0]);
            Assert.AreEqual(wrappedFirstChild.Parent, tree.Root.Children[0].Parent);
        }
    }
}
