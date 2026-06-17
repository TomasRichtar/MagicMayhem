using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public readonly struct AbilityCastData
{
    public readonly float Damage;
    public readonly float Range;

    public AbilityCastData(float damage, float range)
    {
        Damage = damage;
        Range = range;
    }
}
