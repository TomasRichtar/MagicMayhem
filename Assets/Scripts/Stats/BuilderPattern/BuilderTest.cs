using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderPattern
{
    public class BuilderTest : MonoBehaviour
    {
        ////[SerializeField] private Character character;
        ////void Start()
        ////{
        ////    CharacterDirector director = new CharacterDirector();

        ////    //// Vytvo?en� bojovn�ka
        ////    //Character warrior = director.CreateWarrior("Warrior");
        ////    //Debug.Log($"{warrior.Name} Health: {warrior.GetStat("Health").CurrentValue}");

        ////    // Vytvo?en� m�ga
        ////    character = director.CreateMage("Mage");
        ////    Debug.Log($"{character.Name} Mana: {character.GetStat("Mana").CurrentValue}");

        ////    // P?id�n� buffu k m�gov? man?
        ////    character.GetStat("Mana").AddModifier(50);
        ////    Debug.Log($"{character.Name} Mana after buff: {character.GetStat("Mana").CurrentValue}");
        ////}
        ////void Update()
        ////{
        ////    if (Input.GetKeyDown(KeyCode.Space))
        ////    {
        ////        character.GetStat("Mana").AddModifier(50);
        ////        Debug.Log($"{character.Name} Mana: {character.GetStat("Mana").CurrentValue}");
        ////    }
        ////}
    }
}
