using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCaster : MonoBehaviour
{
    public GameObject Caster;
    public Ability Ability;
    public List<AbilityModifier> AbilityModifiers = new List<AbilityModifier>();

    private readonly IAbilityExecutor _executor;

    private float _cooldownTimer;

    public AbilityCaster(Ability ability, GameObject caster)
    {
        Ability = ability;
        Caster = caster;
    }
    public void AddAbilityModifier(AbilityModifier abilityModifier)
    {
        AbilityModifiers.Add(abilityModifier);
    }

    public void Tick(float deltaTime)
    {
        _cooldownTimer -= deltaTime;

        if (_cooldownTimer <= 0f)
        {
            Cast();
            _cooldownTimer = Ability.BaseCooldown;
        }
    }

    private void Cast()
    {
        AbilityBehaviour abilityBehaviour = Instantiate(
            Ability.AbilityObject,
            Caster.transform.position + new Vector3(0,2,0),
            Quaternion.identity).GetComponent<AbilityBehaviour>();

        abilityBehaviour.Cast(Ability, AbilityModifiers);
    }

    
}
