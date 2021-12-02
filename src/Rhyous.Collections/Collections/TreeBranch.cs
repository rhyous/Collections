namespace Rhyous.Collections
{
    /// <summary>A branch of a Tree.</summary>
    /// <typeparam name="T">The type of item in the Tree.</typeparam>
    public class TreeBranch<T> : ITreeable<TreeBranch<T>>
    {
        /// <summary>The empty constructor.</summary>
        public TreeBranch() { }
        /// <summary>The constructor that takes in the item at this branch.</summary>
        public TreeBranch(T item) { Item = item; }

        /// <summary>The Parent TreeBranch{T} that this branch is a child of.</summary>
        public TreeBranch<T> Parent { get; set; }

        /// <summary>The item in this branch.</summary>
        public T Item { get; set; }

        /// <summary>The child TreeBranch{T} items.</summary>
        public ParentedList<TreeBranch<T>, TreeBranch<T>> Children
        {
            get { return _Children ?? (_Children = new ParentedList<TreeBranch<T>, TreeBranch<T>>()); }
            set { _Children = value; }
        } private ParentedList<TreeBranch<T>, TreeBranch<T>> _Children;
    }
}