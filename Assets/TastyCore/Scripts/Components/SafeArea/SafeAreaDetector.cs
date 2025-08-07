using TastyCore.Patterns.ServiceLocator;
using UnityEngine;

namespace TastyCore.Components.SafeArea
{
    public class SafeAreaDetector : MonoRegistrable
    {
        public event SafeAreaChanged OnSafeAreaChanged;
        private Rect _safeArea;

        // Start is called before the first frame update
        private void Awake()
        {
            ServiceLocator.Register(this);
        }

        private void Start()
        {
            _safeArea = Screen.safeArea;
            OnSafeAreaChanged?.Invoke(_safeArea);
        }

        void Update()
        {
            if (_safeArea == Screen.safeArea) return;

            _safeArea = Screen.safeArea;
            OnSafeAreaChanged?.Invoke(_safeArea);
        }

        public delegate void SafeAreaChanged(Rect safeArea);
    }
}
