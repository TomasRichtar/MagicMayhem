using RichiGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Blink : Spell
{
    private float _dashValue = 30;
    private Rigidbody _playerRb;
    private Transform _playerTransform;
    private PlayerMovementAdvance _playerMovementAdvance;

    public Blink(Transform transform, PlayerMovementAdvance playerMovementAdvance)
    {
        cooldown = 2.0f;
        _playerTransform = transform;
        _playerMovementAdvance = playerMovementAdvance;
    }
    protected override void Execute()
    {
        Debug.Log("Casting Blink!");
        if (_playerMovementAdvance.MovementDirection != Vector3.zero)
        {
            _playerTransform.position = _playerTransform.position + _playerMovementAdvance.MovementDirection * _dashValue;
        }
        else
        {
            _playerTransform.position = _playerTransform.position + _playerTransform.forward * _dashValue;
        }
    }
}
