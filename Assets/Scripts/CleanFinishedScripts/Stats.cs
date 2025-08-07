using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _currentMana;
    [SerializeField] private int _mana;
    [SerializeField] private int _health;
    [SerializeField] private int _spellPower;
    [SerializeField] private int _resistance;
    [SerializeField] private int _spiritPower;

    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public int CurrentMana { get => _currentMana; set => _currentMana = value; }
    public int Mana { get => _mana; set => _mana = value; }
    public int Health { get => _health; set => _health = value; }
    public int SpellPower { get => _spellPower; set => _spellPower = value; }
    public int Resistance { get => _resistance; set => _resistance = value; }
    public int SpiritPower { get => _spiritPower; set => _spiritPower = value; }
}
