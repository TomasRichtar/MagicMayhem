using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Spell", menuName = "ScriptableObjects/Spell", order = 1)]
public class SpellStats : ScriptableObject
{
    public string Name;

    public float Damage;
    public DamageType DamageType;
    public AoEType AoEType;

    public GameObject Projectile;
}
