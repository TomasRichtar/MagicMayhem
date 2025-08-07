using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace TastyCore.Components.Timer
{
    public class MonoBehaviourTimer : MonoBehaviour
    {
        [SerializeField] private float _timerDuration;
        [SerializeField] private bool _repeat;

        [Space(10)] 
        [SerializeField] private UnityEvent _onTimerEnd;
        
        private Timer _timer;

        private void Start()
        {
            _timer = new Timer(5, 
                () => { _onTimerEnd?.Invoke(); }, 
                true);
        }

        private void Update() => _timer?.Tick(Time.deltaTime);
    }
}