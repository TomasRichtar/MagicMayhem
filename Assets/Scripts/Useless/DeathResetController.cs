using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeathResetController : MonoBehaviour
{
    public event EventHandler<DeathResetArgs> OnDeathReset;
    public class DeathResetArgs : EventArgs
    {
        public GameObject Actor { get; set; }
    }

    private List<IDeathResetObject> deathResetObjects = new List<IDeathResetObject>();

    private void OnEnable()
    {
        var resettables = FindObjectsOfType<MonoBehaviour>().OfType<IDeathResetObject>();
        foreach (var resettable in resettables)
        {
            OnDeathReset += resettable.ResetAfterDeath;
            deathResetObjects.Add(resettable);
        }
    }

    private void OnDisable()
    {
        foreach (var resettable in deathResetObjects)
        {
            OnDeathReset -= resettable.ResetAfterDeath;
        }
        deathResetObjects.Clear();
    }
    public void DeathReset(GameObject iniciator)
    {
        DeathResetArgs e = new DeathResetArgs() { Actor = iniciator };
        OnDeathReset?.Invoke(this, e);
    }
}
