using System.Collections.Generic;
using UnityEngine;

namespace EventDrivenArchitecture
{
    public class StatManager : MonoBehaviour
    {
        public Dictionary<string, float> stats = new Dictionary<string, float>() {
        { "Health", 100 },
        { "Mana", 50 },
        { "Damage", 20 }
    };

        private void OnEnable()
        {
            EventManager.Subscribe("ModifyStat", OnStatModified);
        }

        private void OnDisable()
        {
            EventManager.Unsubscribe("ModifyStat", OnStatModified);
        }



        private void OnStatModified(object eventData)
        {
            StatModification mod = (StatModification)eventData;
            if (stats.ContainsKey(mod.statName))
            {
                stats[mod.statName] += mod.value;
                Debug.Log($"{mod.statName} modified by {mod.value}. New value: {stats[mod.statName]}");
            }
        }

        public float GetStat(string statName)
        {
            return stats.ContainsKey(statName) ? stats[statName] : 0;
        }
    }

    public class StatModification
    {
        public string statName;
        public float value;

        public StatModification(string statName, float value)
        {
            this.statName = statName;
            this.value = value;
        }
    }
}
