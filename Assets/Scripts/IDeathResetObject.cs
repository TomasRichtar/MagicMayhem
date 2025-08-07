using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DeathResetController;

public interface IDeathResetObject 
{
    void ResetAfterDeath(object sender, DeathResetArgs e);
}
