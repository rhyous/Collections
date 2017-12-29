namespace Rhyous.Collections
{
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
