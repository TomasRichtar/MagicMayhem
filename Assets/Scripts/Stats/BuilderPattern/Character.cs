using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderPattern
{
    public class Character
    {
        public string Name { get; private set; }
        public Dictionary<string, Stat> Stats { get; private set; }

        public Character(string name)
        {
            Name = name;
            Stats = new Dictionary<string, Stat>();
        }

        public void AddStat(Stats name, float baseValue)
        {
            Stats[name.ToString()] = new Stat(name, baseValue);
        }

        public Stat GetStat(string name)
        {
            return Stats.ContainsKey(name) ? Stats[name] : null;
        }
    }
}

