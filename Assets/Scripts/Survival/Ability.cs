using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Ability", order = 1)]
    public class Ability : ScriptableObject
    {
        public string Name;

        public float BaseDamage;
        public float BaseCooldown;
        public float BaseRadius;

        public GameObject AbilityObject;

        //public SpellCastType CastType;
    }
