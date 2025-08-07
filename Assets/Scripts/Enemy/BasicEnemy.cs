using BuilderPattern;
using RichiGames;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace RichiGames
{
    public class BasicEnemy : Entity
    {
        [SerializeField] protected EnemyBehaviour _enemyBehaviour;
        [SerializeField] private AttackCollider _attackCollider;
        [SerializeField] float _attackDuration = 1.3f;
        [SerializeField] float _timeBtwAttack = 1.5f;

        protected RewindableEnemy _rewindableEnemy;

        private bool _canAttack = true;

        protected override void Awake()
        {
            base.Awake();
            _rewindableEnemy = GetComponent<RewindableEnemy>();
            _rewindableEnemy.Character = _character;
        }
        public virtual void Attack()
        {
            if (!_canAttack) return;

            _canAttack = false;
            _attackCollider.EnableCollider();
            StartCoroutine(AttackCooldown());
        }

        private IEnumerator AttackCooldown()
        {
            yield return new WaitForSeconds(_attackDuration);
            _attackCollider.DisableCollider();
            yield return new WaitForSeconds(_timeBtwAttack);
            _canAttack = true;
        }
    }
}
