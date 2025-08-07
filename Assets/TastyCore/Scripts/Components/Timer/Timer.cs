using System;

namespace TastyCore.Components.Timer
{
    public class Timer
    {
        private float _duration;
        private float _remainingDuration;
        private bool _onRepeat;
        private Action _callback;

        public float RemainingTime => _remainingDuration;
    
        public Timer(float duration, Action callback, bool repeat = false)
        {
            _duration = _remainingDuration = duration;
            _callback = callback;
            _onRepeat = repeat;
        }
        
        public bool Tick(float deltaTime)
        {
            if (_remainingDuration == 0) return false;

            _remainingDuration -= deltaTime;
            CheckEnd();
        
            return true;
        
            void CheckEnd()
            {
                if (_remainingDuration > 0) return;
                
                _callback.Invoke();
                
                _remainingDuration = _onRepeat
                    ? _duration
                    : 0;
            }
        }

        public void Reset()
        {
            _remainingDuration = _duration;
        }
        
        public void EndPrematurely(bool callback)
        {
            if(callback)
                _callback.Invoke();

            _remainingDuration = _onRepeat
                ? _duration
                : 0;
        }
    }
}