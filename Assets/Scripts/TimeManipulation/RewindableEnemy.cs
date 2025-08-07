using BuilderPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace RichiGames
{
    public class RewindableEnemy : RewindableObject
    {
        public float TimeBetweenAttacks;
        public Character Character;

        public override void ContinueTime()
        {
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
                 TimeBetweenAttacks,
                 Character.GetStat("Health").CurrentValue);

            _rewindData.AddFirst(pointInTime);
        }

        public override void RewindStep()
        {
            if (_rewindData.Count == 0)
            {
                return;
            }
            RewindData pointInTime = _rewindData.First.Value;
            TimeBetweenAttacks = pointInTime.Number;
            Character.GetStat("Health").CurrentValue = pointInTime.Health;
            transform.position = pointInTime.Position;
            transform.rotation = pointInTime.Rotation;

            _rewindData.RemoveFirst();
        }

        public override void StopTime()
        {
        }
    }
}
