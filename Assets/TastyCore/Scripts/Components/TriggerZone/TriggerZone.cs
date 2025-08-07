using UnityEngine;

namespace TastyCore.Components.TriggerZone
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public abstract class TriggerZone : MonoBehaviour
    {
        public Rigidbody Rigidbody { get; private set; }
        public Collider Collider { get; private set; }

        private Transform _cachedTransform;

        public Transform Transform => _cachedTransform;
        public Vector3 Position => _cachedTransform.position;
        
        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Collider = GetComponent<Collider>();
        }

        public abstract void OnTriggerEnter(Collider other);
        public abstract void OnTriggerStay(Collider other);
        public abstract void OnTriggerExit(Collider other);
    }
}