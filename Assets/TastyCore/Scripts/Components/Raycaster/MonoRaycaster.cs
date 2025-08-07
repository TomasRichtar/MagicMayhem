using System;
using System.Collections.Generic;
using System.Linq;
using TastyCore.Patterns.ServiceLocator;
using UnityEngine;

namespace TastyCore.Components.Raycaster
{
    public class MonoRaycaster : MonoRegistrable
    {
        [SerializeField] [Tooltip("If empty main camera will be assigned")]
        private Camera _raycastCamera;

        [Header("Raycaster Configuration")] [SerializeField]
        private RaycasterSource _raycasterSource;

        [SerializeField] private RaycasterType _raycasterType;
        [SerializeField] private float _raycastRadius = 0.5f;
        [SerializeField] private LayerMask _mask; // Objects has to be on Raycastable Layer

        [Header("Controls")] [SerializeField] private KeyCode _raycastClick;

        [SerializeField] [Tooltip("Maximum delay between OnClick & OnRelease")]
        private float _onReleaseDelay = 0;

        private DateTime _clickedTime;

        private GameObject _raycastObject;
        private IRaycastable _raycastComponent;

        private bool _active = false;

        #region PUBLIC

        public Ray MouseRay
        {
            get
            {
                if (!_active || _raycastCamera == null) return new Ray();
                return _raycasterSource switch
                {
                    RaycasterSource.ScreenCenter => _raycastCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)),
                    RaycasterSource.MousePosition => _raycastCamera.ScreenPointToRay(Input.mousePosition),
                    _ => new Ray()
                };
            }
        }

        public void Enable()
        {
            _active = true;
        }

        public void Disable()
        {
            _active = false;
            _raycastComponent = null;
        }

        #endregion

        private void Awake()
        {
            InitUpdate();

            if (_raycastCamera == null)
            {
                if (Camera.main == null)
                {
                    Debug.LogError("There is no active Camera");
                    Disable();
                    return;
                }

                _raycastCamera = Camera.main;
            }

            Enable();
            ServiceLocator.Register(this);
        }

        void Update()
        {
            if (!_active) return;

            var newRaycast = GetRaycastObject();
            if (_raycastObject != newRaycast)
            {
                ChangeRaycast(newRaycast);
            }

            if (_raycastComponent == null) return;
            _update[_raycasterType]();
        }

        #region Update

        private Dictionary<RaycasterType, RaycastUpdate> _update;

        private delegate void RaycastUpdate();

        private void InitUpdate()
        {
            _update = new Dictionary<RaycasterType, RaycastUpdate>
            {
                { RaycasterType.AlwaysOn, AlwaysOn_Update },
                { RaycasterType.OnClick, OnClick_Update },
                { RaycasterType.OnRelease, OnRelease_Update }
            };
        }

        private void AlwaysOn_Update()
        {
            /* Nothing needs to happen here */
        }

        private void OnClick_Update()
        {
            if (Input.GetKeyDown(_raycastClick))
                _raycastComponent.HandleRaycast();
        }

        private IRaycastable _keyDownComponent;

        private void OnRelease_Update()
        {
            if (Input.GetKeyDown(_raycastClick))
            {
                _clickedTime = DateTime.UtcNow;
                _keyDownComponent = _raycastComponent;
            }

            if (Input.GetKeyUp(_raycastClick))
            {
                var keyUpComponent = _raycastComponent;
                var delay = (DateTime.UtcNow - _clickedTime).TotalSeconds;

                if (keyUpComponent != _keyDownComponent || delay > _onReleaseDelay) return;

                _keyDownComponent.HandleRaycast();
                _keyDownComponent = null;
            }
        }

        #endregion

        #region Utils

        private void ChangeRaycast(GameObject newRaycast)
        {
            _raycastObject = newRaycast;

            if (_raycastComponent != null)
                _raycastComponent.RaycastExit();

            if (newRaycast == null)
            {
                _raycastComponent = null;
                return;
            }

            var newRaycastComponent = _raycastObject.GetComponent<IRaycastable>();

            _raycastComponent = newRaycastComponent;
            _raycastComponent.RaycastEnter();
        }

        private GameObject GetRaycastObject()
        {
            var sortedRaycast = SortedRaycast();
            if (!sortedRaycast.Any()) return default;

            return SortedRaycast()
                .Select(raycastHit => raycastHit.transform.gameObject)
                .FirstOrDefault();
        }

        /*
        private IRaycastable GetRaycastComponent()
        {
            return SortedRaycast()
                .SelectMany(raycastHit => raycastHit.transform.GetComponents<IRaycastable>())
                .FirstOrDefault();
        }
        */

        private RaycastHit[] _results = new RaycastHit[5];

        private IEnumerable<RaycastHit> SortedRaycast()
        {
            var size = Physics.SphereCastNonAlloc(MouseRay, _raycastRadius, _results, 20, _mask);
            if (size == 0) return Array.Empty<RaycastHit>();

            var distances = new float[size];
            for (var i = 0; i < size; i++)
            {
                distances[i] = _results[i].distance;
            }

            Array.Sort(distances, _results);
            return _results.Take(size);
        }

        #endregion
    }
}