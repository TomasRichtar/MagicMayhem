using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompomentArchitecture
{
    [CreateAssetMenu(fileName = "NewStat", menuName = "Stats/Stat")]
    public class Stat : ScriptableObject
    {
        public string statName;
        public float baseValue;
    }
}
