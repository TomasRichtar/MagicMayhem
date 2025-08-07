using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RichiGames
{
    public class RewindableTransform : RewindableObject
    {
        public override void StopTime()
        {
            // I could update the position to the last recorded position, but it would not be optimized. 
            // I will be checking IsStoppedTime in each script that controls the movement that is not using RigidBody separately
            // and stopping the function instead of overriding the movement. - ToDo
        }

        public override void RecordStep()
        {
            if (_rewindData.Count > Mathf.Round(TimeController.Instance.RewindStorageLimit / Time.fixedDeltaTime))
            {
                _rewindData.RemoveLast();
            }

            RewindData pointInTime = new RewindData(transform.position, transform.rotation);
            _rewindData.AddFirst(pointInTime);
        }

        public override void RewindStep()
        {
            if (_rewindData.Count == 0) return;

            RewindData pointInTime = _rewindData.First.Value;
            transform.position = pointInTime.Position;
            transform.rotation = pointInTime.Rotation;

            _rewindData.RemoveFirst();
        }

        public override void ContinueTime()
        {
        }
    }
}
