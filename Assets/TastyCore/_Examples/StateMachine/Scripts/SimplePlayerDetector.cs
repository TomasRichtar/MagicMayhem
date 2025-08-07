using System;
using UnityEngine;

namespace TastyCore._Examples.StateMachine
{
    [RequireComponent(typeof(SphereCollider))]
    public class SimplePlayerDetector : MonoBehaviour
    {
        [SerializeField] private string _detectionTag = "Player";
        [SerializeField] private float _detectionRange = 5f;

        public bool Detected { get; private set; }
        
        private void Awake()
        {
            var col = GetComponent<SphereCollider>();
            col.radius = _detectionRange;
            col.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_detectionTag)) return;
            Detected = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(_detectionTag)) return;
            Detected = false;
        }

        
        #if UNITY_EDITOR
        
        #region GIZMO

        [Header("Debug")] 
        [SerializeField] private bool _showGizmos;
        [SerializeField] [Range(1, 10)] private float _gizmoThickness;
        [SerializeField] private Color32 _gizmoColor;
        private void OnDrawGizmos()
        {
            if (!_showGizmos) return;
            
            UnityEditor.Handles.color = _gizmoColor;
            var drawTransform = transform;
            var drawPosition = drawTransform.position;
            UnityEditor.Handles.DrawWireDisc(drawPosition, drawTransform.up, _detectionRange,_gizmoThickness);
            UnityEditor.Handles.Label(drawPosition, $"Detection Range = { _detectionRange }");
        }

        #endregion
        
        #endif
    }
}