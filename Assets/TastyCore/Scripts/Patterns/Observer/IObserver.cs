namespace TastyCore.Patterns.Observer
{
    public interface IObserver<T>
    {
        void OnNotify(T args);
    }
}