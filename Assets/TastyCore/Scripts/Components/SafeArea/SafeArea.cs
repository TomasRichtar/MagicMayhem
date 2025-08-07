using TastyCore.Enums;
using TastyCore.Patterns.ServiceLocator;
using UnityEngine;

namespace TastyCore.Components.SafeArea   
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeArea : MonoBehaviour
    {
        [SerializeField] private SafeAreaMode _safeAreaMode;
        
        private RectTransform _rectTransform;
        private Vector2 _minAnchor;
        private Vector2 _maxAnchor;

        private SafeAreaDetector _safeArea;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
        
        private void OnEnable()
        {
            InitProperties();
            
            if (_safeArea)
                _safeArea.OnSafeAreaChanged += SafeAreaChanged;
        }

        private void SafeAreaChanged(Rect safeArea)
        {
            _minAnchor = safeArea.position;
            _maxAnchor = _minAnchor + safeArea.size;
            
            switch (_safeAreaMode)
            {
                case SafeAreaMode.Full:
                    FullSaveArea();
                    break;

                case SafeAreaMode.Width:
                    OnlyWidthSaveArea();
                    break;

                case SafeAreaMode.Height:
                    OnlyHeightSafeArea();
                    break;
                
                default:
                    break;
            }
            
            _rectTransform.anchorMin = _minAnchor;
            _rectTransform.anchorMax = _maxAnchor;
        }


        private void InitProperties()
        {
            if (_safeArea == null)
            {
                _safeArea = ServiceLocator.Get<SafeAreaDetector>();
            }
        }

        private void OnDisable()
        {
            if (_safeArea)
                _safeArea.OnSafeAreaChanged -= SafeAreaChanged;
        }
        
        private void FullSaveArea()
        {
            _minAnchor.x /= Screen.width;
            _minAnchor.y /= Screen.height;

            _maxAnchor.x /= Screen.width;
            _maxAnchor.y /= Screen.height;
        }

        private void OnlyWidthSaveArea()
        {
            _minAnchor.x /= Screen.width;
            _minAnchor.y = 0;

            _maxAnchor.x /= Screen.width;
            _maxAnchor.y = 1;
        }

        private void OnlyHeightSafeArea()
        {
            _minAnchor.x = 0;
            _minAnchor.y  /= Screen.height;

            _maxAnchor.x = 1;
            _maxAnchor.y /= Screen.height;
        }
    }
}
