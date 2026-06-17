using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RichiGames
{
    public interface ITimeObject
    {
        abstract void StopTime();
        abstract void ContinueTime();
    }
}