using StrategyPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyPattern
{
    public class Stat
    {
        public string name;
        public float baseValue;
        private IStatCalculation calculationStrategy;

        public Stat(string name, float baseValue, IStatCalculation strategy)
        {
            this.name = name;
            this.baseValue = baseValue;
            this.calculationStrategy = strategy;
        }

        public float GetValue(float modifierValue)
        {
            return calculationStrategy.Calculate(baseValue, modifierValue);
        }

        public void SetStrategy(IStatCalculation newStrategy)
        {
            calculationStrategy = newStrategy;
        }
    }
}

