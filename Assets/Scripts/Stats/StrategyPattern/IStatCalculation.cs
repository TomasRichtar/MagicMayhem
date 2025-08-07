using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyPattern
{
    public interface IStatCalculation
    {
        float Calculate(float baseValue, float modifierValue);
    }
}
