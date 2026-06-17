using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{

    public AbilitySystem SpellSystem;

    private void Awake()
    {
        SpellSystem = new AbilitySystem();
    }

    private void Update()
    {
        SpellSystem.Tick(Time.deltaTime);
    }

    //public void AddSpell(AbilityCaster caster)
    //{
    //    SpellSystem.AddSpell(caster);
    //}
}
