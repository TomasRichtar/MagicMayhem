using EventDrivenArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

namespace EventDrivenArchitecture
{
    public class BuffSystem : MonoBehaviour
    {
        public void ApplyBuff(string statName, float value)
        {
            StatModification buff = new StatModification(statName, value);
            EventManager.TriggerEvent("ModifyStat", buff);
            Debug.Log($"ApplyBuff: {buff}");
        }
    }
}

