using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RichiGames
{
    public class SpellController : MonoBehaviour
    {
        private PlayerAttackController _playerAttackController;
        private PlayerMovementAdvance _playerMovementAdvance;
        public float globalCooldown = 1.0f;
        private float lastCastTime = 0f;
        private Dictionary<KeyCode, Spell> keybinds = new Dictionary<KeyCode, Spell>();

        private void Awake()
        {
            _playerAttackController = GetComponent<PlayerAttackController>();
            _playerMovementAdvance = GetComponent<PlayerMovementAdvance>();
        }
        void Start()
        {
            keybinds.Add(KeyCode.Alpha1, new FallingAttack(_playerMovementAdvance.rb));
            keybinds.Add(KeyCode.Alpha2, new JumpAttack(_playerMovementAdvance.rb));
            keybinds.Add(KeyCode.Alpha3, new DashAttack(_playerMovementAdvance.rb, _playerMovementAdvance.transform));
            keybinds.Add(KeyCode.Alpha4, new Blink(_playerMovementAdvance.transform, _playerMovementAdvance));
        }

        void FixedUpdate()
        {
            if (Time.time - lastCastTime < globalCooldown)
                return;

            foreach (var keybind in keybinds)
            {
                if (Input.GetKeyDown(keybind.Key))
                {
                    keybind.Value.CastSpell();
                    lastCastTime = Time.time;

                    _playerAttackController.StopAttackState();
                }
            }
        }
    }
}
