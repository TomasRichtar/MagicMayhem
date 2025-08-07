using RichiGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : Spell
{
    private float _dashValue = 30;
    private Rigidbody _playerRb;
    private Transform _playerTransform;

    public DashAttack(Rigidbody rb, Transform transform)
    {
        _playerRb = rb;
        cooldown = 2.0f;
        _playerTransform = transform;
    }
    protected override void Execute()
    {
        Debug.Log("Casting DashAttack!");
        _playerRb.velocity = Vector3.zero;
        _playerRb.AddForce(_playerTransform.forward * _dashValue, ForceMode.Impulse);
    }
}
