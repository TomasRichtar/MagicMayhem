using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RichiGames
{
    public class AttackCollider : MonoBehaviour
    {
        [SerializeField] private BoxCollider _boxCollider;
        private bool _targetHit;
        private int _damage = 30;
        private bool _isEnabled;

        private void Update()
        {
            if (_isEnabled)
            {
                _boxCollider.enabled = _isEnabled;
            }
        }

        public void EnableCollider()
        {
            _isEnabled = true;
        }

        public void DisableCollider()
        {
            _isEnabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<BasicEnemy>())
            {
                //if (_targetHit)
                //    return;
                //else
                //    _targetHit = true;
                BasicEnemy enemy = other.gameObject.GetComponent<BasicEnemy>();
                enemy.TakeDamage(_damage);

                return;
            }

            if (other.gameObject.GetComponent<PlayerCharacter>())
            {
                //if (_targetHit)
                //    return;
                //else
                //    _targetHit = true;
                PlayerCharacter enemy = other.gameObject.GetComponent<PlayerCharacter>();

                enemy.TakeDamage(_damage);

                return;
            }

        }
    }
}
