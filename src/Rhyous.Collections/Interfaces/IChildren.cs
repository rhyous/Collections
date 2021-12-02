namespace Rhyous.Collections
{
    /// <summary>An interface for ojects that have children.</summary>
    /// <typeparam name="T">The type of the child items.</typeparam>
    public interface IChildren<T>
    {
        /// <summary>An property containing child items.</summary>
        ParentedList<T> Children { get; set; }
    }

    /// <summary>An property containing child items.</summary>
    /// <typeparam name="T">The type of the child items.</typeparam>
    /// <typeparam name="TParent">The type of the Parent of the items.</typeparam>
    public interface IChildren<T, TParent>
        where T : IParent<TParent>
    {
        /// <summary>An property containing child items.</summary>
        ParentedList<T, TParent> Children { get; set; }
    }
}
