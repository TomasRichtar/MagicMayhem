using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyweightPattern
{
    public class StatDefinition
    {
        public string Name { get; private set; }
        public float BaseValue { get; private set; }

        public StatDefinition(string name, float baseValue)
        {
            Name = name;
            BaseValue = baseValue;
        }
    }
}
