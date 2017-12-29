namespace Rhyous.Collections
{
    /// <summary>
    /// A Parent-Child tree. The item used in the tree must implement IParent and IChildren.
    /// </summary>
    /// <typeparam name="T">The type of the item in the tree.</typeparam>
    public class Tree<T>
        where T : IParent<T>, IChildren<T, T>
    {
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
