using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AbilityModifier", order = 1)]
public class AbilityModifier : ScriptableObject
{
    public string Name;

    public float Damage;
    public float Cooldown;
    public float Radius;

}
