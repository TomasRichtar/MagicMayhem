using RichiGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    private AbilityCaster _caster;

    public void Initialize(AbilityCaster caster)
    {
        _caster = caster;
    }

    private void Update()
    {
        _caster?.Tick(Time.deltaTime);
    }
}
