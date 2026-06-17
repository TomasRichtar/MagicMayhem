using RichiGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Spell
{
    private float _jumpValue = 10;

    public FireBall()
    {
        cooldown = 2.0f;
    }

    protected override void Execute()
    {
        Debug.Log("FIREBALL");
    }

    //protected override void Execute()
    //{
    //    Debug.Log("Casting FireBall!");

    //    Rigidbody rb = Instantiate(_projectile, _spellCast.position, Quaternion.identity).GetComponent<Rigidbody>();

    //    rb.velocity = ThrowData.ThrowVelocity;
    //}
}
