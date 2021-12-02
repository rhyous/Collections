namespace Rhyous.Collections
{
    /// <summary>An interface defining an object as treeable, which means it has a parent and child items.</summary>
    /// <typeparam name="T"></typeparam>
    public interface ITreeable<T> : IParent<T>, IChildren<T, T>
        where T : IParent<T>, IChildren<T, T>
    {
    }
}