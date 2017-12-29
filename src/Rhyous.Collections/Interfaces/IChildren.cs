namespace Rhyous.Collections
{
    public interface IChildren<T, TParent>
        where T : IParent<TParent>
    {
        ParentedList<T, TParent> Children { get; set; }
    }
}
