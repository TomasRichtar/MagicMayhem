using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RichiGames
{
    public class RewindData
    {
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;
        public readonly Vector3 Velocity;
        public readonly Vector3 AngularVelocity;
        public readonly float Drag;
        public readonly float AngularDrag;
        public readonly float AnimatorTime;
        public readonly AnimatorStateInfo StateInfo;
        public readonly Dictionary<string, float> Parameters;
        public readonly float Number;
        public readonly float TimeBetweenAttacks;
        public readonly float Health;
        public readonly Dictionary<RewindableDestroy, bool> RewindableObjects;

        // Destroy
        public RewindData(Dictionary<RewindableDestroy, bool> rewindableObjects)
        {
            RewindableObjects = rewindableObjects;
        }

        // Float
        public RewindData(float number)
        {
            Number = number;
        }

        // Transform
        public RewindData(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        // Enemy
        public RewindData(Vector3 position, Quaternion rotation, float timeBetweenAttacks, float health)
        {
            Position = position;
            Rotation = rotation;
            TimeBetweenAttacks = timeBetweenAttacks;
            Health = health;
        }

        // RigidBody
        public RewindData(
            Vector3 position,
            Quaternion rotation,
            Vector3 velocity,
            Vector3 angularVelocity,
            float drag,
            float angularDrag)
        {
            Position = position;
            Rotation = rotation;
            Velocity = velocity;
            AngularVelocity = angularVelocity;
            Drag = drag;
            AngularDrag = angularDrag;
        }

        // Animation
        public RewindData(
            float animatorTime,
            AnimatorStateInfo stateInfo,
            Dictionary<string, float> parameters)
        {
            AnimatorTime = animatorTime;
            StateInfo = stateInfo;
            Parameters = parameters;
        }
    }
}
