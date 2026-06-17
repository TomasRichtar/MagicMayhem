using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RichiGames
{
    public class RewindableTransform : TimeObject
    {
        public override void StopTime()
        {
            RewindData pointInTime = new RewindData(transform.position, transform.rotation);
            _rewindData.AddFirst(pointInTime);
        }

        public override void ContinueTime()
        {
            RewindData pointInTime = _rewindData.First.Value;
            transform.position = pointInTime.Position;
            transform.rotation = pointInTime.Rotation;
        }
    }
}
