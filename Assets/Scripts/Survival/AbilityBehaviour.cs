using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBehaviour : MonoBehaviour
{
    public virtual void Cast(Ability ability, List<AbilityModifier> abilityModifiers)
    {
        Debug.Log("CAST -> " + ability.Name);
    }
}
