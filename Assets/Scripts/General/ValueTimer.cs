using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RichiGames;

public class ValueTimer
{
    private float _currentTimeBetweenAction;

    public float UpdateTimer(float timeBetweenAction)
    {
        _currentTimeBetweenAction += Time.deltaTime;
        if (_currentTimeBetweenAction > timeBetweenAction)
        {
            return 0;
        }
        return _currentTimeBetweenAction;
    }
}
