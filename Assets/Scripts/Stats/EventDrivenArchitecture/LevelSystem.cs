using EventDrivenArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventDrivenArchitecture
{
    public class LevelSystem : MonoBehaviour
    {
        public int level = 1;

        public void LevelUp()
        {
            level++;
            EventManager.TriggerEvent("ModifyStat", new StatModification("Health", 10 * level));
            EventManager.TriggerEvent("ModifyStat", new StatModification("Damage", 5 * level));
            Debug.Log($"Level up! Current level: {level}");
        }
    }
}

