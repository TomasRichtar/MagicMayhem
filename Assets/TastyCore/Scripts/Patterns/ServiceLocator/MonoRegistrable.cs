using UnityEngine;

namespace TastyCore.Patterns.ServiceLocator
{
    public abstract class MonoRegistrable : MonoBehaviour, IRegistrable
    {
        // Initialize class - Basically Ctor 
        //public abstract void Init();

        protected void Reset()
        {
            name = GetType().Name;
        }
    }
}