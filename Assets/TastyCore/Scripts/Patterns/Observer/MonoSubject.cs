using System.Collections.Generic;
using UnityEngine;

namespace TastyCore.Patterns.Observer
{
    public abstract class MonoSubject<T> : MonoBehaviour, ISubject<T>
    {
        private List<IObserver<T>> _observers = new List<IObserver<T>>();
    
        public void Notify(T args)
        {
            foreach (var t in _observers)
            {
                t.OnNotify(args);
            }
        }
    
        public void Subscribe(IObserver<T> observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver<T> observer)
        {
            _observers.Remove(observer);
        }
    }
}