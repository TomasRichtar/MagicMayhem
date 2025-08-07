using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DeathResetController;

public class SpellController : MonoBehaviour, IDeathResetObject
{
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    public void ResetAfterDeath(object sender, DeathResetArgs e)
    {
        Debug.Log("AnotherController reset triggered by: " + e.Actor.name);
    }
}
