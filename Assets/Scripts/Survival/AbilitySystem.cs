using RichiGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private List<Ability> _baseAbilities = new();

    [SerializeField]
    private List<AbilityCaster> _activeAbilityCasters = new();


    private void Start()
    {
        foreach (var item in _baseAbilities)
        {
            AddSpell(item);
        }
    }
    private void Update()
    {
        Tick(Time.deltaTime);
    }

    public void Tick(float deltaTime)
    {
        foreach (var ability in _activeAbilityCasters)
        {
            ability.Tick(deltaTime);
        }
    }

    public void AddSpell(Ability ability)
    {
        AbilityCaster abilityCaster = new AbilityCaster(ability, _player);

        _activeAbilityCasters.Add(abilityCaster);
    }
}
