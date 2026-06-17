using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RichiGames
{
    [RequireComponent(typeof(Rigidbody))]
    public class RewindableRigidBody : TimeObject
    {
        private Rigidbody _rigBody;

        protected override void Start()
        {
            base.Start();

            _rigBody = GetComponent<Rigidbody>();
        }
        public override void ContinueTime()
        {
            _rigBody.isKinematic = false;
            _rigBody.useGravity = true;

            RewindData pointInTime = _rewindData.First.Value;
            transform.position = pointInTime.Position;
            transform.rotation = pointInTime.Rotation;

            if (_rewindData.Count > 0)
            {
                _rigBody.linearVelocity = _rewindData.First.Value.Velocity;
                _rigBody.angularVelocity = _rewindData.First.Value.AngularVelocity;
                _rigBody.angularDamping = _rewindData.First.Value.AngularDrag;
                _rigBody.linearDamping = _rewindData.First.Value.Drag;
            }
        }

        public override void StopTime()
        {
            RewindData pointInTime = new RewindData(
                transform.position,
                transform.rotation,
                _rigBody.linearVelocity,
                _rigBody.angularVelocity,
                _rigBody.linearDamping,
                _rigBody.angularDamping);

            _rewindData.AddFirst(pointInTime);

            _rigBody.isKinematic = true;
        }
    }
}
