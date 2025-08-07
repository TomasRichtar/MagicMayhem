namespace TastyCore.Patterns.Observer
{
    public interface ISubject<T>
    {
        public void Notify(T args);
        public void Subscribe(IObserver<T> observer);
        public void Unsubscribe(IObserver<T> observer);
    }
}