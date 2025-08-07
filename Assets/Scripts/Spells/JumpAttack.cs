using RichiGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : Spell
{
    private float _jumpValue = 10;
    private Rigidbody playerRb;

    public JumpAttack(Rigidbody rb)
    {
        playerRb = rb;
        cooldown = 2.0f;
    }
    protected override void Execute()
    {
        Debug.Log("Casting Jump Attack!");
        playerRb.velocity = Vector3.zero;
        playerRb.AddForce(Vector3.up* _jumpValue, ForceMode.Impulse);
    }
}
