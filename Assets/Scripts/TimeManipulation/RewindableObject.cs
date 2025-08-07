using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RichiGames
{
    public abstract class RewindableObject : MonoBehaviour, IRewindable
    {
        
        [SerializeField] protected bool _ignoreTimeStop = false;

        // LinkedList is used for easier and optimized manipulation with First and Last items.
        protected LinkedList<RewindData> _rewindData = new LinkedList<RewindData>();

        protected virtual void Start()
        {

        }
        private void OnEnable()
        {
            TimeController.Instance.OnStartRewind += StopTime; // StartRewind would be the same as StopTime method
            TimeController.Instance.OnStopRewind += ContinueTime; // StopRewind would be the same as ContinueTime method
            TimeController.Instance.OnRewindStep += RewindStep;
            TimeController.Instance.OnRecordStep += RecordStep;
            if (_ignoreTimeStop == false)
            {
                TimeController.Instance.OnStopTime += StopTime;
                TimeController.Instance.OnContinueTime += ContinueTime;
            }
        }

        private void OnDisable()
        {
            // Disable on RewindableObjects is called after the destroy/desable on the Singlethon TimeControler 
            // This will be fixed in the GameManager script - ToDo
            if (TimeController.IsCreated)
            {
                TimeController.Instance.OnStartRewind -= StopTime; // StartRewind would be the same as StopTime method
                TimeController.Instance.OnStopRewind -= ContinueTime; // StopRewind would be the same as ContinueTime method
                TimeController.Instance.OnRewindStep -= RewindStep;
                TimeController.Instance.OnRecordStep -= RecordStep; 
                if (_ignoreTimeStop == false)
                {
                    TimeController.Instance.OnStopTime -= StopTime;
                    TimeController.Instance.OnContinueTime -= ContinueTime;
                }
            }
        }

        public abstract void RewindStep();

        public abstract void RecordStep();

        public abstract void StopTime();

        public abstract void ContinueTime();

    }
}
