using RichiGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAttack : Spell
{
    private float _jumpValue = 20;
    private Rigidbody playerRb;

    public FallingAttack(Rigidbody rb)
    {
        playerRb = rb;
        cooldown = 2.0f;
    }
    protected override void Execute()
    {
        Debug.Log("Casting FallingAttack!");
        playerRb.velocity = Vector3.zero;
        playerRb.AddForce(Vector3.down * _jumpValue, ForceMode.Impulse);
    }
}
