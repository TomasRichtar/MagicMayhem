using KinematicCharacterController.Walkthrough.AddingImpulses;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using static RichiGames.SpellController;

namespace RichiGames
{
    public class SpellController : MonoBehaviour
    {
        public List<SpellStats> MySpells;

        private PlayerAttackController _playerAttackController;
        private PlayerMovementAdvance _playerMovementAdvance;
        public float globalCooldown = 1.0f;
        private float lastCastTime = 0f;
        private Dictionary<KeyCode, Spell> keybinds = new Dictionary<KeyCode, Spell>();

        [Header("FireBall")]
        [SerializeField] private GameObject _projectile;
        [SerializeField] private Transform _spellCastPosition;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float projectileForce = 20f;
        [SerializeField] private float upwardBoost = 0.2f;


        private void Awake()
        {
            _playerAttackController = GetComponent<PlayerAttackController>();
            _playerMovementAdvance = GetComponent<PlayerMovementAdvance>();
        }
        void Start()
        {
            keybinds.Add(KeyCode.Alpha1, new FireBall());
            /*
            keybinds.Add(KeyCode.Alpha1, new FallingAttack(_playerMovementAdvance.rb));
            keybinds.Add(KeyCode.Alpha2, new JumpAttack(_playerMovementAdvance.rb));
            keybinds.Add(KeyCode.Alpha3, new DashAttack(_playerMovementAdvance.rb, _playerMovementAdvance.transform));
            keybinds.Add(KeyCode.Alpha4, new Blink(_playerMovementAdvance.transform, _playerMovementAdvance));
            */
        }

        void FixedUpdate()
        {
            if (Time.time - lastCastTime < globalCooldown)
                return;

            foreach (var keybind in keybinds)
            {
                if (Input.GetKeyDown(keybind.Key))
                {
                    if (keybind.Value.IsInstant)
                    {
                        keybind.Value.CastSpell();
                        lastCastTime = Time.time;
                        _playerAttackController.StopAttackState();
                    }
                    else
                    {
                        PrepareCast();
                    }
                }
            }
        }
        public void PrepareCast()
        {
            Debug.Log("Show Cast Time bar");
            Debug.Log("Start precast anim");
            Debug.Log("Call the spell");
            CastProjectile();
        }

        private void CastProjectile()
        {
            Debug.Log("Casting projectile!");

            Rigidbody rb = Instantiate(_projectile, _spellCastPosition.position, Quaternion.identity).GetComponent<Rigidbody>();
            Vector3 dir = playerCamera.transform.forward;

            dir = (dir + Vector3.up * upwardBoost).normalized;

            rb.velocity = dir * projectileForce;
        }

        public class AreaEffect
        {
            public Vector2 StartPoint;
            public Vector2 EndPoint;
            public float Distance;
        }

        public void AreaEffectVisualization(AreaEffect areaEffect)
        {
            
            

        }
    }
}
