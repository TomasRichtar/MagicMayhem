using RichiGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderPattern
{
    public class Mage : BasicEnemy
    {
        [SerializeField] private GameObject _projectile;
        [SerializeField] private Transform _spellCast;
        [SerializeField] private float _attackForce = 32;
        [SerializeField] private float _attackForceUp = 8;
        public RewindableObjectPoolManager RewindableObjectPoolManager;
        public override void CharacterSetUp()
        {
            CharacterDirector director = new CharacterDirector();
            _character = director.CreateMage();

            //Debug.Log("Character: " + _character.Name);

            //foreach (var item in _character.Stats)
            //{
            //    Debug.Log(item.Value.Name + " " + item.Value.CurrentValue);
            //}
        }

        public override void Attack()
        {
            FireBall();
        }

        public void FireBall()
        {
            Rigidbody rb = Instantiate(_projectile, _spellCast.position, Quaternion.identity).GetComponentInChildren<Rigidbody>();
            rb.AddForce(_spellCast.forward * _attackForce, ForceMode.Impulse);
            rb.AddForce(_spellCast.up * _attackForceUp, ForceMode.Impulse);
            RewindableObjectPoolManager.AddObject(rb.GetComponent<RewindableDestroy>());
        }

        ////if (Input.GetKeyDown(KeyCode.Space))
        ////{
        ////    _character.GetStat("Mana").AddModifier(50);
        ////    Debug.Log($"{_character.Name} Mana: {_character.GetStat("Mana").CurrentValue}");
        ////}
    }
}
