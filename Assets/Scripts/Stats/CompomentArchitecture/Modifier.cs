using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompomentArchitecture
{
    [CreateAssetMenu(fileName = "NewModifier", menuName = "Stats/Modifier")]
    public class Modifier : ScriptableObject
    {
        public string modifierName;
        public Stat affectedStat;
        public float value; // P?idaná hodnota
        public bool isPercentage; // Pokud je modifikátor procentuální
    }
}
