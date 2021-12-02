namespace Rhyous.Collections
{
    /// <summary>
    /// A Parent-Child tree. The item used in the tree must implement IParent and IChildren.
    /// If your objects cannot implement IParent or IChildren, then use wrap your object 
    /// in a <see cref="TreeBranch{T}"/> so your new Tree is: Tree&lt;TreeBranch&lt;T&gt;&gt;.
    /// </summary>
    /// <typeparam name="T">The type of the item in the tree.</typeparam>
    public class Tree<T>
        where T : IParent<T>, IChildren<T, T>
    {
        /// <summary>The root item of the tree.</summary>
        public T Root
        {
            get { return _Root; }
            set
            {
                _Root = value;
                _Root.Children.Parent = value;
            }
        } private T _Root;
    }
}