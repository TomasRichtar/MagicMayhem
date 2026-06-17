using StrategyPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RichiGames
{
    public class RewindableParticle : TimeObject
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
            RewindData pointInTime = new RewindData(_particleSystem.time);
            _rewindData.AddFirst(pointInTime);
        }

        public override void ContinueTime()
        {
            _particleSystem.Play();
            RewindData rewindData = _rewindData.First.Value;

            _particleSystem.Simulate(rewindData.Number, true, true, true);

            _rewindData.RemoveFirst();
        }
    }
}