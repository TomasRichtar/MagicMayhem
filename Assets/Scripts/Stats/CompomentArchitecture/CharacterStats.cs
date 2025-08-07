using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompomentArchitecture
{
    public class CharacterStats : MonoBehaviour
    {
        public List<Stat> stats;
        private Dictionary<Stat, float> currentValues = new Dictionary<Stat, float>();
        public List<Modifier> modifiers;

        void Start()
        {
            // Inicializace základních stat?
            foreach (Stat stat in stats)
            {
                currentValues[stat] = stat.baseValue;
            }

            // Aplikace po?áte?ních modifikátor?
            ApplyModifiers();
            Debug.Log("Compoment: " );
            foreach (var item in currentValues)
            {
                Debug.Log(item.Key + " : " + item.Value);
            }
        }

        public void ApplyModifiers()
        {
            foreach (Modifier modifier in modifiers)
            {
                if (modifier.isPercentage)
                {
                    currentValues[modifier.affectedStat] *= 1 + modifier.value / 100f;
                }
                else
                {
                    currentValues[modifier.affectedStat] += modifier.value;
                }
            }
        }

        public float GetStatValue(Stat stat)
        {
            return currentValues.ContainsKey(stat) ? currentValues[stat] : 0;
        }

        public void AddModifier(Modifier modifier)
        {
            modifiers.Add(modifier);
            ApplyModifiers();
        }

        public void RemoveModifier(Modifier modifier)
        {
            modifiers.Remove(modifier);
            ApplyModifiers();
        }
    }

}
