using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderPattern
{
    public class Stat
    {
        public string Name { get; private set; }
        public float BaseValue { get; private set; }
        public float CurrentValue { get; set; }

        public Stat(Stats name, float baseValue)
        {
            Name = name.ToString();
            BaseValue = baseValue;
            CurrentValue = baseValue;
        }

        public void AddModifier(float value)
        {
            CurrentValue += value;
        }

        public void Reset()
        {
            CurrentValue = BaseValue;
        }
    }
}