using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RichiGames
{
    [RequireComponent(typeof(Rigidbody))]
    public class RewindableRigidBody : RewindableObject
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
            _rigBody.isKinematic = true;
        }

        public override void RecordStep()
        {
            if (_rewindData.Count > Mathf.Round(TimeController.Instance.RewindStorageLimit / Time.fixedDeltaTime))
            {
                _rewindData.RemoveLast();
            }

            RewindData pointInTime = new RewindData(
                transform.position,
                transform.rotation,
                _rigBody.velocity,
                _rigBody.angularVelocity,
                _rigBody.drag,
                _rigBody.angularDrag);

            _rewindData.AddFirst(pointInTime);
        }

        public override void RewindStep()
        {
            if (_rewindData.Count == 0)
            {
                return;
            } 

            RewindData pointInTime = _rewindData.First.Value;
            transform.position = pointInTime.Position;
            transform.rotation = pointInTime.Rotation;

            _rewindData.RemoveFirst();
        }
    }
}
