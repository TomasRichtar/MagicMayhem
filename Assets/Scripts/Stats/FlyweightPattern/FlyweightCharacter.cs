using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyweightPattern
{
    public class FlyweightCharacter : MonoBehaviour
    {
        [SerializeField] private Character character;

        void Start()
        {
            // Vytvo?ení postavy
            character = new Character("Warrior");
            character.AddStat("Health", 150);
            character.AddStat("Damage", 20);

            // P?ístup k hodnotám
            Debug.Log($"{character.Name} Health: {character.GetStat("Health").CurrentValue}");

            // Modifikace statu
            character.GetStat("Health").AddModifier(-20);
            Debug.Log($"{character.Name} Health after damage: {character.GetStat("Health").CurrentValue}");
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                character.GetStat("Health").AddModifier(-20);
                Debug.Log($"{character.Name} Health after damage: {character.GetStat("Health").CurrentValue}");
            }
        }
    }
}
