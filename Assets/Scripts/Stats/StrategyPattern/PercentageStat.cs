using StrategyPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyPattern
{
    public class PercentageStat : IStatCalculation
    {
        public float Calculate(float baseValue, float modifierValue)
        {
            return baseValue * (1 + modifierValue / 100f);
        }
    }
}


