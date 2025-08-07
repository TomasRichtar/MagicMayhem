using UnityEngine;

namespace TastyCore.Patterns.Observer
{
    public abstract class MonoObserver<T> : MonoBehaviour, IObserver<T>
    {
        public abstract void OnNotify(T args);
    }
}