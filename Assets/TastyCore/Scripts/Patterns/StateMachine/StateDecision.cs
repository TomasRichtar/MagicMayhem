using UnityEngine;

namespace TastyCore.Patterns.StateMachine
{
    public abstract class StateDecision : ScriptableObject
    {
        public abstract bool Decide(BaseStateMachine state);
    }
}
