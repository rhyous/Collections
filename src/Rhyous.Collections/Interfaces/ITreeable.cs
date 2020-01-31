namespace Rhyous.Collections
{
    public interface ITreeable<T> : IParent<T>, IChildren<T, T>
        where T : IParent<T>, IChildren<T, T>
    {
    }
}
