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

        ////    //// Vytvo?ení bojovníka
        ////    //Character warrior = director.CreateWarrior("Warrior");
        ////    //Debug.Log($"{warrior.Name} Health: {warrior.GetStat("Health").CurrentValue}");

        ////    // Vytvo?ení mága
        ////    character = director.CreateMage("Mage");
        ////    Debug.Log($"{character.Name} Mana: {character.GetStat("Mana").CurrentValue}");

        ////    // P?idání buffu k mágov? man?
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
