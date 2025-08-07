using StrategyPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyPattern
{
    public class FlatStat : IStatCalculation
    {
        public float Calculate(float baseValue, float modifierValue)
        {
            return baseValue + modifierValue;
        }
    }
}

