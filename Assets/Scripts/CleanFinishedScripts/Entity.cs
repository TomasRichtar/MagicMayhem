using System;
using UnityEngine;
using BuilderPattern;
using Microlight.MicroBar;

namespace RichiGames
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] protected Character _character;

        [SerializeField] private bool _isImmuneToDamage = false;

        [Header("Animation")]
        [SerializeField] protected Animator _animator;

        [Header("Particles")]
        [SerializeField] protected ParticleSystem _attackParticle;
        [SerializeField] protected Transform _attackParticlePosition;

        [SerializeField] private MicroBar _healthBar;
        protected Transform _playerTransform;

        private RewindableDestroy _rewindableDestroy;
        private RewindableObjectPoolManager _rewindableObjectPoolManager;

        public bool IsImmuneToDamage { get => _isImmuneToDamage; }
        public Character Character { get => _character; }
        public Animator Animator { get => _animator; }
        public ParticleSystem AttackParticle { get => _attackParticle; }
        public Transform AttackParticlePosition { get => _attackParticlePosition; }

        protected virtual void Awake()
        {
            CharacterSetUp();
            _rewindableDestroy = GetComponent<RewindableDestroy>();
            _rewindableObjectPoolManager = FindFirstObjectByType<RewindableObjectPoolManager>();
            _playerTransform = FindFirstObjectByType<PlayerCharacter>().transform;

            if (_healthBar)
            {
                _healthBar.Initialize(_character.GetStat("Health").BaseValue);
            }
        }

        public virtual void CharacterSetUp()
        {
        }

        public void TakeDamage(int damage)
        {
            if (_isImmuneToDamage) return;

            _character.GetStat("Health").CurrentValue = Math.Max(0, _character.GetStat("Health").CurrentValue - damage);

            if (_healthBar != null)
            {
                _healthBar.UpdateBar(_character.GetStat("Health").CurrentValue);
            }

            Debug.Log(_character.Name+ ": Damage Taken: " + damage + " current HP: " + _character.GetStat("Health").CurrentValue);

            if (_character.GetStat("Health").CurrentValue <= 0)
            {
                Death();
            }
        }

        public void RestoreHealth(int value)
        {
            _character.GetStat("Health").CurrentValue =
                Math.Min(_character.GetStat("Health").BaseValue,
                _character.GetStat("Health").CurrentValue + value);

            if (_healthBar != null)
            {
                _healthBar.UpdateBar(_character.GetStat("Health").CurrentValue);
            }

            Debug.Log("Restored: " + value + " current HP: " + _character.GetStat("Health").CurrentValue);
        }

        public virtual void Death()
        {
            //Anim

            //Last call
            if (_rewindableDestroy)
            {
                _rewindableObjectPoolManager.DestroyObject(_rewindableDestroy);
            }

            if (_character.Name == "Player")
            {
                Debug.Log("Player has died");
            }

            //Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }
}
