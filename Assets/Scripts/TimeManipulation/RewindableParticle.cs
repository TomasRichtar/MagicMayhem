using StrategyPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RichiGames
{
    public class RewindableParticle : RewindableObject
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private bool _isDebugMode;

        public struct ParticleTrackedData
        {
            public bool IsActive;
            public float ParticleTime;
        }

        protected override void Start()
        {
            base.Start();
        }

        public override void StopTime()
        {
            _particleSystem.Stop();
        }

        public override void ContinueTime()
        {
            _particleSystem.Play();
        }

        public override void RecordStep()
        {
            RewindData pointInTime = new RewindData(_particleSystem.time);
            _rewindData.AddFirst(pointInTime);

            if (_isDebugMode)
            {
                Debug.Log("Record step: " + pointInTime.Number);
            }
        }

        public override void RewindStep()
        {
            if (_rewindData.Count == 0) return;

            RewindData rewindData = _rewindData.First.Value;

            _particleSystem.Simulate(rewindData.Number, true, true, true);

            _rewindData.RemoveFirst();

            if (_isDebugMode)
            {
                Debug.Log("Rewind step: " + rewindData.Number);
            }
        }
    }
}