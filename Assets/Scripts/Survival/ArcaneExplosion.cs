using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneExplosion : AbilityBehaviour
{
    public override void Cast(Ability ability, List<AbilityModifier> abilityModifiers)
    {
        base.Cast(ability, abilityModifiers);
        Debug.Log("AOE");
    }
}
