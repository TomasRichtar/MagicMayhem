using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RichiGames
{
    [RequireComponent(typeof(Rigidbody))]
    public class RewindableProjectile : TimeObject
    {
        private Rigidbody _rigBody;
        private SphereCollider _collider;

        protected override void Start()
        {
            base.Start();

            _rigBody = GetComponent<Rigidbody>();
            _collider = GetComponent<SphereCollider>();
        }
        public override void ContinueTime()
        {
            _rigBody.isKinematic = false;
            _rigBody.useGravity = true;
            _collider.isTrigger = true;

            RewindData pointInTime = _rewindData.First.Value;
            transform.position = pointInTime.Position;
            transform.rotation = pointInTime.Rotation;

            if (_rewindData.Count > 0)
            {
                _rigBody.velocity = _rewindData.First.Value.Velocity;
                _rigBody.angularVelocity = _rewindData.First.Value.AngularVelocity;
                _rigBody.angularDrag = _rewindData.First.Value.AngularDrag;
                _rigBody.drag = _rewindData.First.Value.Drag;
            }
        }

        public override void StopTime()
        {
            RewindData pointInTime = new RewindData(
                transform.position,
                transform.rotation,
                _rigBody.velocity,
                _rigBody.angularVelocity,
                _rigBody.drag,
                _rigBody.angularDrag);

            _rewindData.AddFirst(pointInTime);

            _rigBody.isKinematic = true;
            _collider.isTrigger = false;
        }
    }
}
