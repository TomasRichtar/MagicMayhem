using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterStates
{
    public enum PlayerStates
    {
        Alive = 0,
        Death = 1,
    }
    public enum PlayerCombatStates
    {
        OutOfCombat = 0,
        InCombat = 1,
    }
}
