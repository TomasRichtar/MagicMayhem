using Chronos;
using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TastyCore.Utils;
using UnityEngine;

namespace RichiGames
{
    public class TimeController : SingletonMonoBehaviour<TimeController>
    {
        public event Action OnStartRewind;
        public event Action OnStopRewind;
        public event Action OnRewindStep;
        public event Action OnRecordStep;
        public event Action OnStopTime;
        public event Action OnContinueTime;

        [SerializeField][Tooltip("In seconds")] private float _rewindStorageLimit = 500;

        private bool _isRewinding = false;
        private bool _isStoppedTime = false;

        public float RewindStorageLimit { get => _rewindStorageLimit; }
        public bool IsStoppedTime { get => _isStoppedTime; }
        public bool IsRewinding { get => _isRewinding; }

        void Update()
        {
            if (!_isRewinding)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    StopTime();
                }
                if (Input.GetKeyUp(KeyCode.Q))
                {
                    ContinueTime();
                }
            }

            if (_isStoppedTime) return;

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                StartRewind();
            }
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                StopRewind();
            }
        }

        private void FixedUpdate()
        {
            if (_isStoppedTime) return;

            if (_isRewinding)
            {
                RewindStep();
            }
            else
            {
                RecordStep();
            }

        }

        private void RewindStep()
        {
            OnRewindStep?.Invoke();
        }

        private void RecordStep()
        {
            OnRecordStep?.Invoke();
        }

        private void StartRewind()
        {
            _isRewinding = true;
            OnStartRewind?.Invoke();
        }

        private void StopRewind()
        {
            _isRewinding = false;
            OnStopRewind?.Invoke();
        }

        private void StopTime()
        {
            _isStoppedTime = true;
            OnStopTime?.Invoke();
        }

        private void ContinueTime()
        {
            _isStoppedTime = false;
            OnContinueTime?.Invoke();
        }
    }
}
