using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RichiGames
{
    public interface IRewindable
    {
        abstract void RewindStep();
        abstract void RecordStep();
        abstract void StopTime();
        abstract void ContinueTime();
    }
}