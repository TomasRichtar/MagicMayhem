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
        public event Action OnStopTime;
        public event Action OnContinueTime;

        private bool _isStoppedTime = false;

        public bool IsStoppedTime { get => _isStoppedTime; }

        void Update()
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

        private void FixedUpdate()
        {
            if (_isStoppedTime) return;
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
