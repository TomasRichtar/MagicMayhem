using QInventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInstance
{
    public Ability Definition { get; }

    //private readonly ModifierContainer _modifiers;

    public AbilityInstance(Ability definition)
    {
        Definition = definition;
        //_modifiers = new ModifierContainer();
    }

    public float Damage => Definition.BaseDamage;
    //_modifiers.Calculate(StatType.Damage, Definition.BaseDamage);

    public float Cooldown => Definition.BaseCooldown;
        //_modifiers.Calculate(StatType.Cooldown, Definition.BaseCooldown);

    public float Range => Definition.BaseRadius;
        //_modifiers.Calculate(StatType.Range, Definition.BaseRange);

    //public void AddModifier(IAbilityModifier modifier)
    //{
    //    _modifiers.Add(modifier);
    //}

    public AbilityCastData CreateCastData()
    {
        return new AbilityCastData(
            Damage,
            Range
        );
    }
}
