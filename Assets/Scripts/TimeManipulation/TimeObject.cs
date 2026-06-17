using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RichiGames
{
    public abstract class TimeObject : MonoBehaviour, ITimeObject
    {
        
        [SerializeField] protected bool _ignoreTimeStop = false;

        // LinkedList is used for easier and optimized manipulation with First and Last items.
        protected LinkedList<RewindData> _rewindData = new LinkedList<RewindData>();

        protected virtual void Start()
        {

        }
        private void OnEnable()
        {
            if (_ignoreTimeStop == false)
            {
                TimeController.Instance.OnStopTime += StopTime;
                TimeController.Instance.OnContinueTime += ContinueTime;
            }
        }

        private void OnDisable()
        {
            if (TimeController.IsCreated)
            {
                if (_ignoreTimeStop == false)
                {
                    TimeController.Instance.OnStopTime -= StopTime;
                    TimeController.Instance.OnContinueTime -= ContinueTime;
                }
            }
        }

        public abstract void StopTime();

        public abstract void ContinueTime();

    }
}
