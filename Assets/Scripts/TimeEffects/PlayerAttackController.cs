using Microlight.MicroBar;
using RichiGames;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private PlayerMovementAdvance _playerMovementAdvance;
    [SerializeField] private MicroBar _staminaBar;

    public float chainAttackWindow = 0.5f;
    public float attackDelay = 0.2f;
    private int chainAttackIndex = 0;
    private float lastAttackTime = 0f;
    private float lastInputTime = 0f;

    private Coroutine attackCoroutine;
    private void Awake()
    {
        _playerMovementAdvance = GetComponent<PlayerMovementAdvance>();
        _staminaBar.Initialize(100);
    }
    void Update()
    {
        if (_staminaBar.CurrentValue < _staminaBar.MaxValue)
        {
            _staminaBar.UpdateBar(_staminaBar.CurrentValue + 0.2f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastInputTime < attackDelay)
                return;

            if (_staminaBar.CurrentValue < 20) return;

            if (_staminaBar.CurrentValue < 50 && _playerMovementAdvance.grounded == false) return;

            lastInputTime = Time.time;
            _playerMovementAdvance.state = PlayerMovementAdvance.MovementState.attacking;
            _playerMovementAdvance.rb.velocity = Vector3.zero;

            if (_playerMovementAdvance.grounded)
            {
                if (Time.time - lastAttackTime <= chainAttackWindow && chainAttackIndex < 4)
                {
                    ChainAttack();
                }
                else
                {
                    NormalAttack();
                }
            }
            else
            {
                FallingAttack();
            }

            if (attackCoroutine != null) StopCoroutine(attackCoroutine);
            attackCoroutine = StartCoroutine(ResetAttackState());
        }
    }

    IEnumerator ResetAttackState()
    {
        float elapsedTime = 0f;

        while (elapsedTime < attackDelay)
        {
            if (!_playerMovementAdvance.grounded)
            {
                _playerMovementAdvance.rb.velocity = Vector3.zero;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (_playerMovementAdvance.grounded)
        {
            _playerMovementAdvance.state = PlayerMovementAdvance.MovementState.walking;
        }
        else
        {
            _playerMovementAdvance.state = PlayerMovementAdvance.MovementState.air;
        }
    }
    public void StopAttackState()
    {
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);

        if (_playerMovementAdvance.grounded)
        {
            _playerMovementAdvance.state = PlayerMovementAdvance.MovementState.walking;
        }
        else
        {
            _playerMovementAdvance.state = PlayerMovementAdvance.MovementState.air;
        }
    }

    void NormalAttack()
    {
        chainAttackIndex = 1;
        lastAttackTime = Time.time;

        _staminaBar.UpdateBar(_staminaBar.CurrentValue - 20);
        // Animation
        _playerMovementAdvance.animator.SetTrigger("Attack");
    }

    void ChainAttack()
    {
        chainAttackIndex++;
        lastAttackTime = Time.time;

        if (chainAttackIndex >= 4)
        {
            chainAttackIndex = 0;
        }

        _staminaBar.UpdateBar(_staminaBar.CurrentValue - 20);

        // Animation
        _playerMovementAdvance.animator.SetTrigger("Attack");
    }

    void FallingAttack()
    {
        Debug.Log("Falling Attack");
        chainAttackIndex = 0;
        _playerMovementAdvance.rb.velocity = Vector3.zero;

        _staminaBar.UpdateBar(_staminaBar.CurrentValue - 50);
        // Animation
        _playerMovementAdvance.animator.SetTrigger("Attack");
    }
}
