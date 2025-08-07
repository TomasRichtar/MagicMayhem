using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyPattern
{
    public class StatManager : MonoBehaviour
    {
        public List<Stat> stats = new List<Stat>();

        void Start()
        {
            // P?íklad inicializace
            stats.Add(new Stat("Health", 100, new FlatStat()));
            stats.Add(new Stat("Mana", 50, new PercentageStat()));
        }

        public float GetStatValue(string statName, float modifierValue)
        {
            Stat stat = stats.Find(s => s.name == statName);
            if (stat != null)
            {
                return stat.GetValue(modifierValue);
            }
            Debug.LogWarning($"Stat {statName} not found!");
            return 0;
        }

        public void ChangeStatStrategy(string statName, IStatCalculation newStrategy)
        {
            Stat stat = stats.Find(s => s.name == statName);
            if (stat != null)
            {
                stat.SetStrategy(newStrategy);
            }
        }

        //Usage
        //StatManager manager = FindObjectOfType<StatManager>();

        //// Získání hodnoty statu s modifikátorem
        //float health = manager.GetStatValue("Health", 20);

        //// Zm?na strategie statu za b?hu
        //manager.ChangeStatStrategy("Mana", new FlatStat());

    }
}

