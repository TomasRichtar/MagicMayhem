using FlyweightPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyweightPattern
{
    public class StatDefinitionFactory
    {
        private static Dictionary<string, StatDefinition> statDefinitions = new Dictionary<string, StatDefinition>();

        public static StatDefinition GetStatDefinition(string name, float baseValue)
        {
            if (!statDefinitions.ContainsKey(name))
            {
                statDefinitions[name] = new StatDefinition(name, baseValue);
            }
            return statDefinitions[name];
        }
    }
}
