namespace Rhyous.Collections
{
    public class TreeBranch<T> : ITreeable<TreeBranch<T>>
    {
        public TreeBranch() { }
        public TreeBranch(T item) { Item = item; }

        public TreeBranch<T> Parent { get; set; }

        public T Item { get; set; }

        public ParentedList<TreeBranch<T>, TreeBranch<T>> Children
        {
            get { return _Children ?? (_Children = new ParentedList<TreeBranch<T>, TreeBranch<T>>()); }
            set { _Children = value; }
        } private ParentedList<TreeBranch<T>, TreeBranch<T>> _Children;
    }
}